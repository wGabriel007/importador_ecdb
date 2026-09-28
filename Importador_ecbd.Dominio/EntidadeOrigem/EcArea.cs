using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_area` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcArea
{
    public int Id { get; set; }
    public int? MainAreaId { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
