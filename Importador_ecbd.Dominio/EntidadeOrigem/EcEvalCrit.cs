using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_eval_crit` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcEvalCrit
{
    public int Id { get; set; }
    public int EvaluationId { get; set; }
    public int CriterionId { get; set; }
    public int Note { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int StatusDefault { get; set; }
}
