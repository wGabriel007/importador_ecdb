using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_workloads` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcWorkloads
{
    public int Id { get; set; }
    public int ApplicationRoleId { get; set; }
    public int AnnouncementId { get; set; }
    public int HoursQuantity { get; set; }
    public int Type { get; set; }
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
