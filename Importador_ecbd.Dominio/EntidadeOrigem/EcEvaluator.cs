using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_evaluator` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcEvaluator
{
    public int Id { get; set; }
    public int ApplicationUserId { get; set; }
    public int FormationAreaId { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? LattesReferenceLink { get; set; }
    public string? ActivityAreaDescription { get; set; }
    public string? RelatedOrganOrinstitutionDescription { get; set; }
    public bool HasParticipatedOnScienceYoungPreviously { get; set; }
    public int Degree { get; set; }
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
