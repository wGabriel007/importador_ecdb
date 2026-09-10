using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_email` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcEmail
{
    public int Id { get; set; }
    public string? Sender { get; set; }
    public string? Email { get; set; }
    public string? Sender_password { get; set; }
    public string? Server { get; set; }
    public int Port { get; set; }
    public bool Ssl { get; set; }
    public string? Header { get; set; }
    public string? Body { get; set; }
    public string? Language { get; set; }
    public int Sent { get; set; }
    public int Errors { get; set; }
    public int Send_status { get; set; }
    public int Type { get; set; }
    public int StatusDefault { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
