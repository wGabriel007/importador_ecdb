using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_criterion` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcCriterion
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int Type { get; set; }
    public int? ProjectCategoryId { get; set; }
    public int Weight { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int StatusDefault { get; set; }
}
