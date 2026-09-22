using Importador_ecbd.Aplicacao.Dtos;
using Importador_ecbd.Aplicacao.Excecoes;
using Importador_ecbd.Aplicacao.Interfaces;
using Importador_ecbd.Aplicacao.Mapeamento;
using Importador_ecbd.Dominio.Enums;
using Dominio.Destino;

namespace Importador_ecbd.Aplicacao.Servicos;

/// <summary>
/// Implementação do serviço de importação. Coordena a leitura do
/// banco de origem, o mapeamento e a gravação do banco de destino,
/// registrando tudo que não pôde ser importado em vez de interromper.
///
/// Como cada tabela é importada (ver ImportarTabelaAsync):
///   1. Lê todos os registros da origem.
///   2. Mapeia cada um para a entidade de destino.
///   3. Pula os que já existem no destino (pode rodar a importação de novo sem duplicar).
///   4. Grava em lotes de <see cref="TamanhoLote"/>. Se um lote falhar, regrava
///      aquele lote registro a registro para isolar só os que têm problema.
///
/// IDs: o banco de destino gera um Id NOVO (AUTO_INCREMENT) para cada registro e o
/// Id antigo fica na coluna iIdOrigem. Toda chave estrangeira é traduzida de Id
/// antigo para Id novo pelo <see cref="MapaDeIds"/> (ex.: Cidade.EstadoId).
/// </summary>
public class ServicoImportacao : IServicoImportacao
{
    /// <summary>Registros gravados por SaveChanges. Lote grande = mais rápido.</summary>
    private const int TamanhoLote = 500;

    /// <summary>
    /// Todas as tabelas que existem no banco de origem. As que não aparecem
    /// em <see cref="MontarEtapas"/> são listadas no relatório como "sem mapeamento".
    /// </summary>
    private static readonly string[] TodasTabelasOrigem =
    {
        "ec_affi_area", "ec_affiliated_trade_fair", "ec_anno_affi", "ec_announcement",
        "ec_app_role", "ec_app_user", "ec_approleclaims", "ec_appuserlogins",
        "ec_appuserroles", "ec_appusertokens", "ec_area", "ec_aspuserclaims",
        "ec_cat", "ec_cat_theme", "ec_certificate", "ec_checking_presence",
        "ec_city", "ec_country", "ec_criterion", "ec_email",
        "ec_eval_crit", "ec_evaluation", "ec_evaluator", "ec_evaluator_announcement",
        "ec_experiment", "ec_experiment_class", "ec_institution", "ec_institution_user",
        "ec_levels", "ec_log", "ec_peoplegroup", "ec_permissions",
        "ec_proj_area", "ec_proj_image", "ec_proj_part", "ec_project",
        "ec_reg_edu_aut", "ec_request_project_message", "ec_responsible", "ec_role_permission",
        "ec_scheduling", "ec_state", "ec_theme", "ec_user_role",
        "ec_userpeoplegroup", "ec_workloads"
    };

    private readonly IRepositorioOrigem  _repositorioOrigem;
    private readonly IRepositorioDestino _repositorioDestino;

    public ServicoImportacao(
        IRepositorioOrigem  repositorioOrigem,
        IRepositorioDestino repositorioDestino)
    {
        _repositorioOrigem  = repositorioOrigem;
        _repositorioDestino = repositorioDestino;
    }

    /// <summary>Uma etapa da importação = uma tabela de origem indo para uma tabela de destino.</summary>
    private sealed record Etapa(string TabelaOrigem, string TabelaDestino, Func<ResultadoImportacao, MapaDeIds, Task> Executar);

    /// <summary>
    /// Lista de etapas NA ORDEM de importação. A ordem importa: uma tabela só pode
    /// vir depois das tabelas para as quais ela tem chave estrangeira
    /// (ex.: Estado depois de Pais, Cidade depois de Estado...).
    /// </summary>
    private List<Etapa> MontarEtapas() => new()
    {
        // ---- Localização ----
        Criar("ec_country", _repositorioOrigem.ObterCountryAsync, MapeadorPais.Mapear, o => $"Id = {o.Id}"),
        Criar("ec_state",   _repositorioOrigem.ObterStateAsync,   MapeadorEstado.Mapear, o => $"Id = {o.Id}"),
        Criar("ec_city",    _repositorioOrigem.ObterCityAsync,    MapeadorCidade.Mapear, o => $"Id = {o.Id}"),

        // ---- Usuários e estrutura educacional ----
        Criar("ec_app_user",         _repositorioOrigem.ObterAppUserAsync,         MapeadorUsuario.Mapear,            o => $"Id = {o.Id}"),
        Criar("ec_reg_edu_aut",      _repositorioOrigem.ObterRegEduAutAsync,       MapeadorGre.Mapear,                o => $"Id = {o.Id}"),
        Criar("ec_institution",      _repositorioOrigem.ObterInstitutionAsync,     MapeadorInstituicao.Mapear,        o => $"Id = {o.Id}"),
        Criar("ec_institution_user", _repositorioOrigem.ObterInstitutionUserAsync, MapeadorInstituicaoUsuario.Mapear, o => $"Id = {o.Id}"),

        // ---- Taxonomias ----
        // ec_area referencia ela mesma (MainAreaId): importa as áreas principais antes das subáreas
        Criar("ec_area",      _repositorioOrigem.ObterAreaAsync,      MapeadorAreaConhecimento.Mapear, o => $"Id = {o.Id}",
              id: o => o.Id, paiId: o => o.MainAreaId),
        Criar("ec_cat",       _repositorioOrigem.ObterCatAsync,       MapeadorCategoria.Mapear, o => $"Id = {o.Id}"),
        Criar("ec_theme",     _repositorioOrigem.ObterThemeAsync,     MapeadorTema.Mapear,      o => $"Id = {o.Id}"),
        Criar("ec_criterion", _repositorioOrigem.ObterCriterionAsync, MapeadorCriterio.Mapear,  o => $"Id = {o.Id}"),

        // ---- Feiras afiliadas ----
        Criar("ec_affiliated_trade_fair", _repositorioOrigem.ObterAffiliatedTradeFairAsync, MapeadorFeiraAfiliada.Mapear, o => $"Id = {o.Id}"),
        Criar("ec_affi_area", _repositorioOrigem.ObterAffiAreaAsync, MapeadorFeiraArea.Mapear,
              o => $"FeiraId = {o.AffiliatedTradeFairsId}, AreaId = {o.KnowledgeAreasId}"),
        Criar("ec_anno_affi", _repositorioOrigem.ObterAnnoAffiAsync, MapeadorEditalFeira.Mapear, o => $"Id = {o.Id}"),

        // ---- Projetos ----
        Criar("ec_project", _repositorioOrigem.ObterProjectAsync, MapeadorProjeto.Mapear, o => $"Id = {o.Id}"),

        // ---- Permissões ----
        // ec_permissions referencia ela mesma (ParentId): importa os menus pai antes dos filhos
        Criar("ec_permissions", _repositorioOrigem.ObterPermissionsAsync, MapeadorPermissao.Mapear, o => $"Id = {o.Id}",
              id: o => o.Id, paiId: o => o.ParentId),
        Criar("ec_app_role",        _repositorioOrigem.ObterAppRoleAsync,        MapeadorAppPermissao.Mapear, o => $"Id = {o.Id}"),
        Criar("ec_role_permission", _repositorioOrigem.ObterRolePermissionAsync, MapeadorFuncaoPermissao.Mapear,
              o => $"PapelId = {o.ApplicationRolesId}, PermissaoId = {o.PermissionsId}"),
        Criar("ec_appuserroles",    _repositorioOrigem.ObterAppuserrolesAsync,   MapeadorPermissaoUsuario.Mapear,
              o => $"UsuarioId = {o.UserId}, PapelId = {o.RoleId}"),
    };

    private Etapa Criar<TOrigem, TDestino>(
        string tabelaOrigem,
        Func<Task<List<TOrigem>>> lerOrigem,
        Func<TOrigem, MapaDeIds, TDestino> mapear,
        Func<TOrigem, string> identificar,
        Func<TOrigem, int>? id = null,
        Func<TOrigem, int?>? paiId = null)
        where TOrigem : class
        where TDestino : class
    {
        var tabelaDestino = _repositorioDestino.ObterNomeTabela<TDestino>();
        return new Etapa(tabelaOrigem, tabelaDestino, (resultado, ids) =>
            ImportarTabelaAsync(resultado, ids, tabelaOrigem, tabelaDestino, lerOrigem, mapear, identificar, id, paiId));
    }

    // ==================================================================
    // Resumo pré-importação
    // ==================================================================

    public async Task<ResumoPreImportacao> ObterResumoPreImportacaoAsync()
    {
        var resumo = new ResumoPreImportacao();
        var destinoPorOrigem = MontarEtapas().ToDictionary(e => e.TabelaOrigem, e => e.TabelaDestino);

        foreach (var tabela in TodasTabelasOrigem)
        {
            var contagem = new ContagemTabela
            {
                NomeTabela = tabela,
                TabelaDestino = destinoPorOrigem.GetValueOrDefault(tabela)
            };

            try
            {
                // COUNT(*) direto no banco: não carrega as linhas na memória
                contagem.QuantidadeRegistros = await _repositorioOrigem.ContarPorTabelaAsync(tabela);
            }
            catch (Exception ex)
            {
                // Uma tabela que não existe não pode derrubar o resumo inteiro
                contagem.QuantidadeRegistros = -1;
                contagem.Erro = ex.Message;
            }

            resumo.Tabelas.Add(contagem);
        }

        // Tabelas importadas primeiro, na ordem da importação
        var ordem = MontarEtapas().Select(e => e.TabelaOrigem).ToList();
        resumo.Tabelas = resumo.Tabelas
            .OrderBy(t => t.SeraImportada ? ordem.IndexOf(t.NomeTabela) : int.MaxValue)
            .ThenBy(t => t.NomeTabela)
            .ToList();

        return resumo;
    }

    // ==================================================================
    // Importação
    // ==================================================================

    public async Task<ResultadoImportacao> ImportarAsync(IProgress<ProgressoImportacao> progresso)
    {
        var resultado = new ResultadoImportacao { Inicio = DateTime.Now };
        var etapas = MontarEtapas();
        var ids = new MapaDeIds(); // de-para Id antigo → Id novo, compartilhado entre as tabelas

        for (int i = 0; i < etapas.Count; i++)
        {
            var etapa = etapas[i];

            progresso.Report(new ProgressoImportacao
            {
                TabelaAtualIndice = i + 1,
                TotalTabelas = etapas.Count,
                NomeTabelaAtual = $"{etapa.TabelaOrigem} → {etapa.TabelaDestino}"
            });

            try
            {
                await etapa.Executar(resultado, ids);
                resultado.TotalTabelasProcessadas++;
            }
            catch (Exception ex)
            {
                // Erro inesperado na etapa (ex.: conexão caiu). Registra e segue para a próxima.
                resultado.TabelasNaoImportadas.Add(new ItemNaoImportado
                {
                    NomeTabela = etapa.TabelaOrigem,
                    Motivo = EnumMotivoFalha.ErroExecucao,
                    Descricao = MensagemCompleta(ex)
                });
            }
        }

        await RegistrarTabelasSemMapeamentoAsync(resultado, etapas);

        resultado.Fim = DateTime.Now;
        return resultado;
    }

    /// <summary>
    /// Importa uma tabela inteira. Nunca lança exceção por causa de um registro:
    /// cada falha vira um item em resultado.RegistrosComErro e a importação segue.
    /// </summary>
    private async Task ImportarTabelaAsync<TOrigem, TDestino>(
        ResultadoImportacao resultado,
        MapaDeIds ids,
        string tabelaOrigem,
        string tabelaDestino,
        Func<Task<List<TOrigem>>> lerOrigem,
        Func<TOrigem, MapaDeIds, TDestino> mapear,
        Func<TOrigem, string> identificar,
        Func<TOrigem, int>? id,
        Func<TOrigem, int?>? paiId)
        where TOrigem : class
        where TDestino : class
    {
        var resumo = new ResumoTabelaImportada { TabelaOrigem = tabelaOrigem, TabelaDestino = tabelaDestino };
        resultado.Tabelas.Add(resumo);

        void RegistrarErro(TOrigem item, EnumMotivoFalha motivo, string descricao)
        {
            resumo.ComErro++;
            resultado.RegistrosComErro.Add(new ItemNaoImportado
            {
                NomeTabela = tabelaOrigem,
                IdentificadorRegistro = $"{tabelaOrigem} ({identificar(item)})",
                Motivo = motivo,
                Descricao = descricao
            });
        }

        // Tabela com Id próprio (Id novo + iIdOrigem) ou associativa (chave composta)?
        var temIdProprio = typeof(IEntidadeComIdOrigem).IsAssignableFrom(typeof(TDestino));

        // 1. Leitura da origem
        List<TOrigem> registros;
        try
        {
            registros = await lerOrigem();
        }
        catch (Exception ex)
        {
            resultado.TabelasNaoImportadas.Add(new ItemNaoImportado
            {
                NomeTabela = tabelaOrigem,
                Motivo = EnumMotivoFalha.FalhaNaLeituraDaOrigem,
                Descricao = MensagemCompleta(ex)
            });
            return;
        }

        resumo.Lidos = registros.Count;

        // 2. O que já está no destino
        HashSet<string>? chavesExistentes = null;
        if (temIdProprio)
        {
            var aviso = await _repositorioDestino.PrepararColunaIdOrigemAsync<TDestino>();
            if (aviso != null)
                resultado.Avisos.Add(aviso);

            // Carrega o de-para mesmo com a origem vazia: outras tabelas podem precisar dele
            ids.Carregar<TDestino>(await _repositorioDestino.ObterMapaIdsAsync<TDestino>());
        }
        else
        {
            chavesExistentes = await _repositorioDestino.ObterChavesExistentesAsync<TDestino>();
        }

        if (registros.Count == 0)
        {
            resultado.TabelasVazias.Add(tabelaOrigem);
            return;
        }

        // 3. Níveis (só tabelas que referenciam elas mesmas): nível 0 = sem pai,
        //    nível 1 = filho de um nível 0... Cada nível só é MAPEADO depois que o
        //    anterior foi gravado, porque o filho precisa do Id NOVO do pai.
        var niveis = (id != null && paiId != null)
            ? CalcularNiveis(registros, id, paiId).GroupBy(par => par.Value, par => par.Key).OrderBy(g => g.Key).Select(g => g.ToList())
            : new[] { registros };

        var jaVistosNestaExecucao = new HashSet<string>();

        foreach (var registrosDoNivel in niveis)
        {
            var pendentes = new List<(TOrigem Origem, TDestino Destino)>();

            foreach (var item in registrosDoNivel)
            {
                TDestino entidade;
                try
                {
                    entidade = mapear(item, ids);
                }
                catch (ReferenciaNaoImportadaException ex)
                {
                    RegistrarErro(item, ex.Motivo, ex.Message);
                    continue;
                }
                catch (Exception ex)
                {
                    RegistrarErro(item, EnumMotivoFalha.TipoDeDadoIncompativel, $"Erro ao converter o registro: {ex.Message}");
                    continue;
                }

                string chave;
                bool jaExiste;
                if (entidade is IEntidadeComIdOrigem comId)
                {
                    var idOrigem = comId.IdOrigem!.Value;
                    chave = idOrigem.ToString();
                    jaExiste = ids.Contem<TDestino>(idOrigem);
                }
                else
                {
                    chave = _repositorioDestino.ObterChave(entidade);
                    jaExiste = chavesExistentes!.Contains(chave);
                }

                if (jaExiste)
                {
                    resumo.JaExistentes++;
                    continue;
                }

                if (!jaVistosNestaExecucao.Add(chave))
                {
                    RegistrarErro(item, EnumMotivoFalha.ChaveDuplicada,
                        $"Registro duplicado na origem: outro registro de {tabelaOrigem} já gerou a chave '{chave}' em {tabelaDestino}.");
                    continue;
                }

                pendentes.Add((item, entidade));
            }

            // 4. Gravação em lotes
            foreach (var lote in pendentes.Chunk(TamanhoLote))
            {
                foreach (var p in lote)
                    _repositorioDestino.Adicionar(p.Destino);

                try
                {
                    await _repositorioDestino.SalvarAlteracoesAsync();
                    foreach (var p in lote)
                        RegistrarIdGerado(ids, p.Destino);
                    resumo.Importados += lote.Length;
                    continue;
                }
                catch (ErroAoSalvarException)
                {
                    // Algum registro do lote tem problema. O lote inteiro foi desfeito
                    // (transação) e o repositório já limpou o contexto; regrava um a um
                    // para salvar os bons e isolar os ruins.
                }

                foreach (var p in lote)
                {
                    // Descarta o Id que o EF possa ter preenchido na tentativa desfeita
                    if (p.Destino is IEntidadeComIdOrigem comId)
                        comId.Id = 0;

                    _repositorioDestino.Adicionar(p.Destino);
                    try
                    {
                        await _repositorioDestino.SalvarAlteracoesAsync();
                        RegistrarIdGerado(ids, p.Destino);
                        resumo.Importados++;
                    }
                    catch (ErroAoSalvarException ex)
                    {
                        RegistrarErro(p.Origem, ex.Motivo, ex.Message);
                    }
                }
            }
        }

        resultado.TotalRegistrosImportados += resumo.Importados;
        resultado.TotalRegistrosJaExistentes += resumo.JaExistentes;
    }

    /// <summary>Depois do INSERT o EF preenche o Id gerado pelo banco: guarda Id antigo → Id novo.</summary>
    private static void RegistrarIdGerado<TDestino>(MapaDeIds ids, TDestino entidade)
    {
        if (entidade is IEntidadeComIdOrigem comId && comId.IdOrigem.HasValue)
            ids.Registrar<TDestino>(comId.IdOrigem.Value, comId.Id);
    }

    /// <summary>
    /// Para tabelas auto-referenciadas: calcula a profundidade de cada registro
    /// na árvore, para gravar os pais antes dos filhos.
    /// Pai que não está na lista (já existe no destino ou não existe) conta como raiz.
    /// Ciclos (A pai de B, B pai de A) são cortados para não entrar em loop.
    /// </summary>
    private static Dictionary<TOrigem, int> CalcularNiveis<TOrigem>(
        List<TOrigem> registros, Func<TOrigem, int> id, Func<TOrigem, int?> paiId)
        where TOrigem : class
    {
        var porId = new Dictionary<int, TOrigem>();
        foreach (var r in registros)
            porId.TryAdd(id(r), r);

        var niveisPorId = new Dictionary<int, int>();

        int Nivel(TOrigem r, HashSet<int> visitando)
        {
            var meuId = id(r);
            if (niveisPorId.TryGetValue(meuId, out var conhecido))
                return conhecido;

            var pai = paiId(r);
            int nivel;
            if (pai == null || pai == meuId || !porId.TryGetValue(pai.Value, out var registroPai) || !visitando.Add(meuId))
                nivel = 0;
            else
                nivel = Nivel(registroPai, visitando) + 1;

            niveisPorId[meuId] = nivel;
            return nivel;
        }

        var resultado = new Dictionary<TOrigem, int>();
        foreach (var r in registros)
            resultado[r] = Nivel(r, new HashSet<int>());
        return resultado;
    }

    /// <summary>
    /// Tabelas da origem com dados que não têm etapa de importação: aparecem no
    /// relatório para ninguém achar que foram importadas.
    /// </summary>
    private async Task RegistrarTabelasSemMapeamentoAsync(ResultadoImportacao resultado, List<Etapa> etapas)
    {
        var mapeadas = etapas.Select(e => e.TabelaOrigem).ToHashSet();

        foreach (var tabela in TodasTabelasOrigem.Where(t => !mapeadas.Contains(t)))
        {
            int quantidade;
            try
            {
                quantidade = await _repositorioOrigem.ContarPorTabelaAsync(tabela);
            }
            catch
            {
                continue; // tabela nem existe na origem
            }

            if (quantidade <= 0)
                continue;

            resultado.TabelasNaoImportadas.Add(new ItemNaoImportado
            {
                NomeTabela = tabela,
                Motivo = EnumMotivoFalha.TabelaSemMapeamento,
                Descricao = $"{quantidade} registro(s) na origem não foram importados: ainda não existe tabela/mapeamento de destino para '{tabela}'."
            });
        }
    }

    /// <summary>Junta a mensagem da exceção com as internas (o motivo real costuma estar na InnerException).</summary>
    private static string MensagemCompleta(Exception ex)
    {
        var mensagens = new List<string>();
        for (Exception? atual = ex; atual != null; atual = atual.InnerException)
        {
            if (!mensagens.Contains(atual.Message))
                mensagens.Add(atual.Message);
        }
        return string.Join(" → ", mensagens);
    }
}
