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
    public int QuatidadeRegistros { get; set; }
}
