using Importador_ecbd.Dominio.Enums;

namespace Importador_ecbd.Aplicacao.Dtos;

/// <summary>
/// Guarda o resultado completo de uma importação: quantos registros
/// foram importados com sucesso e a lista detalhada de tudo que falhou
/// com motivo. Para a tela de resultados.
/// </summary>
public class ResultadoImportacao
{
    public DateTime Inicio { get; set; } = DateTime.Now;
    public DateTime Fim { get; set; }

    public int TotalTabelasProcessadas {  get; set; }
    public int TotalRegistrosImportados { get; set; }

    /// <summary>Registros que já estavam no destino (mesma chave) e foram pulados.</summary>
    public int TotalRegistrosJaExistentes { get; set; }

    /// <summary>Números por tabela: lidos, importados, já existentes e com erro.</summary>
    public List<ResumoTabelaImportada> Tabelas { get; set; } = new();

    public List<string> TabelasVazias { get; set; } = new();
    public List<ItemNaoImportado> TabelasNaoImportadas { get; set; } = new();
    public List<ItemNaoImportado> ColunasNaoImportadas { get; set; } = new();
    public List<ItemNaoImportado> RegistrosComErro {  get; set; }    = new();
}

/// <summary>
/// Contagem de uma tabela depois da importação.
/// </summary>
public class ResumoTabelaImportada
{
    public string TabelaOrigem { get; set; } = string.Empty;
    public string TabelaDestino { get; set; } = string.Empty;
    public int Lidos { get; set; }
    public int Importados { get; set; }
    public int JaExistentes { get; set; }
    public int ComErro { get; set; }
}

/// <summary>
/// Um item (tabela, coluna ou dados) que nao pôde ser importado,
/// junto com motivo e descrição detalhada.
/// </summary>
public class ItemNaoImportado
{
    public string NomeTabela { get; set; } = string.Empty;
    public string? IdentificadorRegistro { get; set; }
    public EnumMotivoFalha Motivo {  get; set; }
    public string Descricao { get; set; } = string.Empty;
}
