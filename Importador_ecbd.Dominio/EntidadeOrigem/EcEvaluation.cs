using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_evaluation` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcEvaluation
{
    public int Id { get; set; }
    public int EvaluatorAnnouncementId { get; set; }
    public int ProjectId { get; set; }
    public int Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int StatusDefault { get; set; }
    public int Status { get; set; }
    public int AcceptanceEvaluationStatus { get; set; }
    public string? Commentary { get; set; }
}
