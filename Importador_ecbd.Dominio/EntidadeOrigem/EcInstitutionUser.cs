using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_institution_user` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcInstitutionUser
{
    public int Id { get; set; }
    public int InstitutionId { get; set; }
    public int ApplicationUserId { get; set; }
    public string Reference { get; set; } = string.Empty;
    public int Type { get; set; }
    public string RegistrationId { get; set; } = string.Empty;
    public int? AcademicLevel { get; set; }
    public int? HasParticipatedPreviousCJ { get; set; }
    public string? PreviousCJ { get; set; }
    public int? HasExternalFairExperience { get; set; }
    public string? ExternalFairExperience { get; set; }
}
