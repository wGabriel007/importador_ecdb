using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_scheduling` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcScheduling
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartHour { get; set; }
    public DateTime EndHour { get; set; }
    public int Type { get; set; }
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
