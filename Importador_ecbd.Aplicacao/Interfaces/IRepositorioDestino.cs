namespace Importador_ecbd.Aplicacao.Interfaces;

/// <summary>
/// Contrato de escrita no banco de dados NOVO (destino da importação).
/// É genérico: serve para qualquer entidade mapeada no ContextoDestino
/// (Pais, Estado, Cidade, Usuario, Projeto...).
///
/// Fluxo usado pelo ServicoImportacao:
///   1. ObterChavesExistentesAsync  -> descobre o que já está no destino (para não duplicar)
///   2. Adicionar                   -> marca os registros para inserção
///   3. SalvarAlteracoesAsync       -> grava de verdade
///
/// IMPORTANTE: SalvarAlteracoesAsync sempre limpa o ChangeTracker, com sucesso
/// ou com erro. Assim um registro que falhou NÃO fica preso no contexto
/// contaminando os próximos SaveChanges (era o bug que fazia a importação
/// "parar" de importar a partir do primeiro erro).
///
/// Implementado em Infraestrutura/Repositorios/RepositorioDestino.cs.
/// </summary>
public interface IRepositorioDestino
{
    /// <summary>
    /// Retorna as chaves primárias (em texto, ex.: "15" ou "3|7" para chave
    /// composta) de todos os registros que já existem na tabela de destino.
    /// </summary>
    Task<HashSet<string>> ObterChavesExistentesAsync<T>() where T : class;

    /// <summary>Monta a chave em texto de uma entidade, no mesmo formato de ObterChavesExistentesAsync.</summary>
    string ObterChave<T>(T entidade) where T : class;

    /// <summary>Nome da tabela de destino mapeada para a entidade.</summary>
    string ObterNomeTabela<T>() where T : class;

    /// <summary>Marca um registro para inserção (só grava em SalvarAlteracoesAsync).</summary>
    void Adicionar<T>(T entidade) where T : class;

    /// <summary>
    /// Salva no banco de destino as alterações pendentes.
    /// Em caso de falha lança ErroAoSalvarException já com o motivo classificado.
    /// </summary>
    Task<int> SalvarAlteracoesAsync();
}
