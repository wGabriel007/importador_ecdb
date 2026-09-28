# Correções do importador ECDB

## Por que o importador não importava todos os dados

### 1. Efeito cascata do ChangeTracker (causa principal)
Cada registro fazia `Add()` e depois `SaveChanges()`. Quando um `SaveChanges()` falhava
(FK inválida, campo obrigatório, chave duplicada), o EF Core **mantinha a entidade quebrada
no ChangeTracker** como `Added`. No registro seguinte, o `SaveChanges()` tentava gravar de novo
a entidade quebrada junto com a nova, e as duas falhavam. Como o `ContextoDestino` é o mesmo
na importação inteira, **um único erro fazia todos os registros seguintes falharem**,
inclusive os das próximas tabelas.

**Correção:** `RepositorioDestino.SalvarAlteracoesAsync()` agora chama
`ChangeTracker.Clear()` no `finally`, tanto no sucesso quanto na falha.

### 2. Chave estrangeira `?? 0`
`Cidade.EstadoId = origem.IdState ?? 0` e `Projeto.FeiraAfiliadaId = ... ?? 0` gravavam o Id 0,
que não existe, e a FK falhava. Esses campos agora são `int?` e recebem `null` quando a origem
é nula.

### 3. Tabelas que referenciam a si mesmas
`ec_permissions` (ParentId) e `ec_area` (MainAreaId): se um filho viesse antes do pai, a FK
falhava. Agora os registros são gravados por nível: primeiro os pais, depois os filhos.

### 4. Tabela inteira perdida por um NULL
Se uma única linha tinha NULL numa coluna de texto que o modelo considerava obrigatória,
o `ToListAsync()` estourava e **a tabela inteira** ficava de fora. As propriedades `string`
das entidades de origem agora aceitam `null`, e os mapeadores usam `?? string.Empty` onde o
destino exige texto.
Também foram adicionados:
- `ConvertZeroDateTime = true` na conexão de origem (datas `0000-00-00` não quebram mais a leitura);
- `EnableDetailedErrors()`: se ainda faltar algo, o relatório diz exatamente qual coluna falhou
  (ex.: `'EcCat.StatusDefault' ... the actual value was null`). Nesse caso, basta tornar essa
  propriedade anulável (`int?`) na entidade de origem.

### 5. Relatório com nome errado
O relatório usava o nome do método da lambda (`<>b__2`) como nome da tabela.

## Ids novos no destino, com o Id antigo guardado

Cada registro ganha um **Id novo**, gerado pelo `AUTO_INCREMENT` do banco novo, e o
**Id antigo** fica na coluna **`iIdOrigem`** da própria tabela:

```
ec_tb_cidade
iId  | iIdOrigem | iEstadoId | sNome
1000 |     1     |    1000   | Recife     <- iEstadoId é o Id NOVO do estado
```

Como funciona:
- **Coluna criada automaticamente:** antes de importar cada tabela, o importador confere
  se o `iId` é `AUTO_INCREMENT` e cria `iIdOrigem` (com índice) se ela não existir. O que
  foi alterado aparece em **AVISOS** no relatório. Se preferir criar as colunas antes, use
  `scripts/adicionar_iIdOrigem.sql`.
- **Chaves estrangeiras traduzidas:** o `MapaDeIds` guarda o de-para (Id antigo → Id novo)
  de cada tabela já importada, e os mapeadores trocam toda FK (ex.:
  `EstadoId = ids.TraduzirOpcional<Estado>(origem.IdState)`).
- **Tabelas associativas** (`ec_tb_feira_area`, `ec_tb_funcao_permissao`,
  `ec_tb_permissao_usuario`) não têm Id próprio: a chave delas passa a ser formada pelos
  Ids novos das tabelas que elas ligam.
- **Pai inexistente:** se o registro aponta para um pai que não foi importado, ele não é
  gravado, e o relatório diz qual referência faltou (ex.: `Referência para Pais com Id de
  origem 99, que não foi importado(a)`).
- **Tabelas auto-referenciadas** (`ec_permissions`, `ec_area`) são importadas nível a
  nível, porque o filho precisa do Id novo do pai.
- **Rodar de novo:** o que já tem `iIdOrigem` no destino é pulado.
  ⚠️ Linhas que já estavam no destino **sem** `iIdOrigem` (ex.: vindas da versão antiga do
  importador, que copiava o Id) não são reconhecidas e seriam duplicadas. **Limpe o destino
  antes da primeira importação com esta versão.**
- **Buracos na numeração** (ex.: 36301, 36303) são normais: quando o MySQL desfaz um lote
  com erro, ele não reaproveita os valores do `AUTO_INCREMENT`.
- **Confirmar:** `Projeto.TemaProjeto` (origem `ProjectThemeId`) está sendo tratado como
  referência a `ec_theme`. Se apontar para outra tabela, ajuste em `MapeadorProjeto`.

## Outras melhorias
- **`ServicoImportacao` genérico:** os 19 métodos quase idênticos viraram uma rotina só
  (`ImportarTabelaAsync`). Para adicionar uma tabela nova, basta uma linha em `MontarEtapas()`.
- **Gravação em lotes de 500:** muito mais rápida. Se um lote falhar, ele é regravado registro
  a registro para isolar só os ruins (teste: 20 mil cidades em cerca de 5 s).
- **Pode rodar de novo:** registros que já existem no destino são pulados em vez de gerar erro
  de duplicidade.
- **Erro classificado pelo código do MySQL:** FK, duplicado, obrigatório, tipo/tamanho etc.
- **Tela de confirmação** mostra a contagem de cada tabela e para onde ela vai; as tabelas
  sem mapeamento aparecem em cinza.
- **Tela de resultado** mostra um relatório legível (por tabela: lidos / importados /
  já existentes / erros) e salva automaticamente em `relatorios/importacao_*.txt`.
- **Resumo pré-importação** usa `COUNT(*)` em todas as tabelas (antes carregava tabelas
  inteiras na memória só para contar) e não quebra se uma tabela não existir.
- **Erros inesperados na tela principal** agora aparecem numa mensagem em vez de fechar o programa.
- **Timeouts** de 600 s (origem) e 300 s (destino) para tabelas grandes.

## Tabelas que continuam sem importar (precisa de decisão)
Não existe entidade nem tabela de destino para estas: `ec_announcement`, `ec_evaluation`,
`ec_evaluator`, `ec_evaluator_announcement`, `ec_eval_crit`, `ec_certificate`, `ec_proj_part`,
`ec_proj_area`, `ec_proj_image`, `ec_request_project_message`, `ec_cat_theme`, `ec_scheduling`,
`ec_experiment*`, `ec_workloads`, `ec_checking_presence`, `ec_peoplegroup`, `ec_userpeoplegroup`,
`ec_user_role`, `ec_levels`, `ec_responsible`, `ec_email`, `ec_log` e as tabelas do Identity.
Elas aparecem no relatório como **"Sem mapeamento"**, com a quantidade de registros.

Os campos com `TODO` nos mapeadores (cidade da instituição e da feira, `Alcance`, `Periodo`,
`Tipo` da área) continuam indo como `null` até existir a tabela de conversão texto → código.

## Segurança
O `appsettings.json` com usuário e senha estava no repositório público.
1. **Troque a senha do usuário `devbd`.**
2. Tire o arquivo do Git (o `.gitignore` já foi ajustado):
   `git rm --cached importador_ecdb/Forms/appsettings.json`
3. Use o `appsettings.example.json` como modelo.
   A senha continua no histórico do Git, por isso trocar a senha é o passo que realmente resolve.
