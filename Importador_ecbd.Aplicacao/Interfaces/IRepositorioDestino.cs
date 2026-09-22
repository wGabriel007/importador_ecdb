using Dominio.Destino;

namespace Importador_ecbd.Aplicacao.Interfaces;

/// <summary>
/// Contrato de escrita no banco de dados NOVO (destino da importação).
/// Cada método "Adicionar" prepara um registro para inserção; a gravação
/// efetiva no banco só acontece ao chamar SalvarAlteracoesAsync — assim o
/// ServicoImportacao controla quando cada registro é salvo (um a um,
/// para isolar erros, conforme explicado no guia da etapa 4.6).
/// Implementado em Infraestrutura/Repositorios/RepositorioDestino.cs.
/// </summary>
public interface IRepositorioDestino
{
    // ---- Localização ----
    Task AdicionarPaisAsync(Pais pais);
    Task AdicionarEstadoAsync(Estado estado);
    Task AdicionarCidadeAsync(Cidade cidade);

    // ---- Identidade e acesso ----
    Task AdicionarPapelAsync(Papel papel);
    Task AdicionarUsuarioAsync(Usuario usuario);
    Task AdicionarPermissaoAsync(Permissao permissao);
    Task AdicionarPermissaoUsuarioAsync(PermissaoUsuario permissaoUsuario);
    Task AdicionarAppPermissaoAsync(AppPermissao appPermissao);
    Task AdicionarFuncaoPermissaoAsync(FuncaoPermissao funcaoPermissao);

    // ---- Estrutura educacional ----
    Task AdicionarGreAsync(Gre gre);
    Task AdicionarInstituicaoAsync(Instituicao instituicao);
    Task AdicionarInstituicaoUsuarioAsync(InstituicaoUsuario instituicaoUsuario);

    // ---- Taxonomias ----
    Task AdicionarAreaConhecimentoAsync(AreaConhecimento areaConhecimento);
    Task AdicionarCategoriaAsync(Categoria categoria);
    Task AdicionarTemaAsync(Tema tema);
    Task AdicionarCriterioAsync(Criterio criterio);

    // ---- Feiras afiliadas ----
    Task AdicionarFeiraAfiliadaAsync(FeiraAfiliada feiraAfiliada);
    Task AdicionarFeiraAreaAsync(FeiraArea feiraArea);
    Task AdicionarEditalFeiraAsync(EditalFeira editalFeira);

    // ---- Projetos ----
    Task AdicionarProjetoAsync(Projeto projeto);

    /// <summary>Salva no banco de destino as alterações pendentes.</summary>
    Task<int> SalvarAlteracoesAsync();
}