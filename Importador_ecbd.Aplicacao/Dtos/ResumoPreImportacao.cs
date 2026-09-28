namespace Importador_ecbd.Aplicacao.Dtos;

/// <summary>
/// Resumo mostrado na tela de confirmação, antes de iniciar a
/// importação, quantas linhas existem em cada tabela de origem.
/// </summary>
public class ResumoPreImportacao
{
    public List<ContagemTabela> Tabelas { get; set; } = new();
}

public class ContagemTabela
{
    public string NomeTabela { get; set; } = string.Empty;

    /// <summary>Quantidade de linhas na origem. -1 quando não foi possível contar.</summary>
    public int QuantidadeRegistros { get; set; }

    /// <summary>Tabela de destino, ou null se a tabela ainda não é importada.</summary>
    public string? TabelaDestino { get; set; }

    /// <summary>Mensagem de erro caso a contagem tenha falhado.</summary>
    public string? Erro { get; set; }

    public bool SeraImportada => TabelaDestino != null;
}
