using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_certificate` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcCertificate
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int AnnouncementId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public byte[] Background { get; set; } = Array.Empty<byte>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int StatusDefault { get; set; }
}
