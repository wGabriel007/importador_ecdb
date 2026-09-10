using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_state` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcState
{
    public int Id { get; set; }
    public int? IdCountry { get; set; }
    public string? Name { get; set; }
    public string Acronym { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? StatusDefault { get; set; }
}
