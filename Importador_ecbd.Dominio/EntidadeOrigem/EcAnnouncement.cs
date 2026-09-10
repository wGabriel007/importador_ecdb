using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_announcement` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcAnnouncement
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Status { get; set; }
    public DateTime InscriptionEndDate { get; set; }
    public DateTime InscriptionStartDate { get; set; }
    public DateTime PreInscriptionEndDate { get; set; }
    public DateTime PreInscriptionStartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ChangeStudentsInProjectFinalDate { get; set; }
    public DateTime AffiliatedTradeFairEndInscriptionDate { get; set; }
    public DateTime AffiliatedTradeFairExceptionProjectsSubmitsEndDate { get; set; }
    public DateTime AffiliatedTradeFairExceptionProjectsSubmitsStartDate { get; set; }
    public DateTime AffiliatedTradeFairStartInscriptionDate { get; set; }
    public DateTime EndConfirmationProjectsDate { get; set; }
    public DateTime EndProjectRelocationDate { get; set; }
    public DateTime EndSendVideoDate { get; set; }
    public DateTime ShareClassifiedProjectsDate { get; set; }
    public DateTime StartConfirmationProjectsDate { get; set; }
    public DateTime StartProjectRelocationDate { get; set; }
    public DateTime StartSendVideoDate { get; set; }
    public DateTime AffiliatedTradeFairEndProjectsSubmitsDate { get; set; }
    public DateTime AffiliatedTradeFairStartProjectsSubmitsDate { get; set; }
    public DateTime EvaluatorInscriptionEndDate { get; set; }
    public DateTime EvaluatorInscriptionStartDate { get; set; }
    public DateTime? EvaluateResumesEndDate { get; set; }
    public DateTime? EvaluateResumesStartDate { get; set; }
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
