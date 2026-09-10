using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_log` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcLog
{
    public int Id { get; set; }
    public int? ApplicationUserId { get; set; }
    public string? ObjectJsonModel { get; set; }
    public string ObjectType { get; set; } = string.Empty;
    public string MessageAction { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
    public DateTime CreatedAt { get; set; }
}
