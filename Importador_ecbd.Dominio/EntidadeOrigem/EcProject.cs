using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_project` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcProject
{
    public int Id { get; set; }
    public int ProjectCategoryId { get; set; }
    public int InstitutionId { get; set; }
    public int AnnouncementId { get; set; }
    public int? AffiliatedTradeFairId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Introduction { get; set; }
    public string Objectives { get; set; } = string.Empty;
    public string? Methodology { get; set; }
    public string? Results { get; set; }
    public int Status { get; set; }
    public int PresentationType { get; set; }
    public string? DescriptionCancel { get; set; }
    public bool IsParticipantsInSpecialEducationModality { get; set; }
    public string Keywords { get; set; } = string.Empty;
    public bool IsDoesInPreInscription { get; set; }
    public string? Bibliography { get; set; }
    public string? VideoUrl { get; set; }
    public string? CredenceLetterLink { get; set; }
    public int ConfirmationStatus { get; set; }
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? Ideiatec { get; set; }
    public string? Summary { get; set; }
    public int? IdTheme { get; set; }
    public int? ProjectThemeId { get; set; }
}
