using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_anno_affi` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcAnnoAffi
{
    public int Id { get; set; }
    public int AnnouncementId { get; set; }
    public int AffiliatedTradeFairId { get; set; }
    public int ConfirmationStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int StatusDefault { get; set; }
    public string ParticipationEdition { get; set; } = string.Empty;
}
