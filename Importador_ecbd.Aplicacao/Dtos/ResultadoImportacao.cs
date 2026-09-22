using Importador_ecbd.Dominio.Enums;

namespace Importador_ecbd.Aplicacao.Dtos;

/// <summary>
/// Guarda o resultado completo de uma importação: quantos registros
/// foram importados com sucesso e a lista detalhada de tudo que falhou
/// com motivo. Para a tela de resultados.
/// </summary>
public class ResultadoImportacao
{
    public int TotalTabelasProcessadas {  get; set; }
    public int TotalRegistrosImportados { get; set; }
    public List<string> TabelasVazias { get; set; } = new();
    public List<ItemNaoImportado> TabelasNaoImportadas { get; set; } = new();
    public List<ItemNaoImportado> ColunasNaoImportadas { get; set; } = new();
    public List<ItemNaoImportado> RegistrosComErro {  get; set; }    = new();
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