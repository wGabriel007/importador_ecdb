using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_proj_image` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcProjImage
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public byte[] File { get; set; } = Array.Empty<byte>();
    public string Description { get; set; } = string.Empty;
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
