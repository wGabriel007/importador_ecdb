using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_experiment_class` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcExperimentClass
{
    public int Id { get; set; }
    public int ExperimentId { get; set; }
    public int SchedulingId { get; set; }
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
