using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_checking_presence` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcCheckingPresence
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AnnouncementId { get; set; }
    public DateTime DataAndHoursCheckingPresence { get; set; }
    public int StatusCheckingPresence { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int StatusDefault { get; set; }
}
