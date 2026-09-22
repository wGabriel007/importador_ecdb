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
    public string? Name { get; set; }
    public string? Range { get; set; }
    public string? City { get; set; }
    public string? StudentDegrees { get; set; }
    public string? State { get; set; }
    public int ParticipationSchoolsQuantity { get; set; }
    public string? Address { get; set; }
    public string? CriationProjectsPeriod { get; set; }
    public DateTime EndRealizationDate { get; set; }
    public bool IsProjectsEvaluatedInFair { get; set; }
    public string? KnowledgeAreasInformalText { get; set; }
    public string? OrganizerInstitutionName { get; set; }
    public int ParticipationProjectsQuantity { get; set; }
    public string? Period { get; set; }
    public string? ProjectsEvaluatedInFairDescription { get; set; }
    public string? RealizationDateAsString { get; set; }
    public string? SelectWorksProcessDescription { get; set; }
    public DateTime StartRealizationDate { get; set; }
    public int StudentParticipationType { get; set; }
    public string? AffiliatedTradeFairRelationshipsDescription { get; set; }
    public string? Country { get; set; }
    public string? ParticipatedAnotherYearsDescription { get; set; }
    public bool ParticpatedAnotherYears { get; set; }
    public string? StudentDegreesAsString { get; set; }
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
