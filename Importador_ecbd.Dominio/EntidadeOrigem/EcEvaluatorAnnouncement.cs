using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_evaluator_announcement` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcEvaluatorAnnouncement
{
    public int Id { get; set; }
    public int EvaluatorId { get; set; }
    public int AnnouncementId { get; set; }
    public int? OrientationProjectId { get; set; }
    public int EvaluateProjectCategoryId { get; set; }
    public bool HasInterestedInPhoneGroup { get; set; }
    public string ParticipationFormat { get; set; } = string.Empty;
    public string EvaluateType { get; set; } = string.Empty;
    public int Status { get; set; }
    public string SelectedParticipationDatesWithPeriodsAsString { get; set; } = string.Empty;
    public int? StatusDefault { get; set; }
    public int ReleaseStatus { get; set; }
}
