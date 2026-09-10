using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_affiliated_trade_fair` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcAffiliatedTradeFair
{
    public int Id { get; set; }
    public int OrganizerId { get; set; }
    public int? InstitutionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Range { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string? StudentDegrees { get; set; }
    public string State { get; set; } = string.Empty;
    public int ParticipationSchoolsQuantity { get; set; }
    public string Address { get; set; } = string.Empty;
    public string CriationProjectsPeriod { get; set; } = string.Empty;
    public DateTime EndRealizationDate { get; set; }
    public bool IsProjectsEvaluatedInFair { get; set; }
    public string KnowledgeAreasInformalText { get; set; } = string.Empty;
    public string OrganizerInstitutionName { get; set; } = string.Empty;
    public int ParticipationProjectsQuantity { get; set; }
    public string Period { get; set; } = string.Empty;
    public string? ProjectsEvaluatedInFairDescription { get; set; }
    public string RealizationDateAsString { get; set; } = string.Empty;
    public string SelectWorksProcessDescription { get; set; } = string.Empty;
    public DateTime StartRealizationDate { get; set; }
    public int StudentParticipationType { get; set; }
    public string? AffiliatedTradeFairRelationshipsDescription { get; set; }
    public string Country { get; set; } = string.Empty;
    public string? ParticipatedAnotherYearsDescription { get; set; }
    public bool ParticpatedAnotherYears { get; set; }
    public string? StudentDegreesAsString { get; set; }
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
