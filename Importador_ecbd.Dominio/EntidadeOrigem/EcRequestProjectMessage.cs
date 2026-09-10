using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_request_project_message` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcRequestProjectMessage
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int RequesterId { get; set; }
    public int AnswererId { get; set; }
    public string RequestMessage { get; set; } = string.Empty;
    public string AwnserMessage { get; set; } = string.Empty;
    public int Type { get; set; }
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
