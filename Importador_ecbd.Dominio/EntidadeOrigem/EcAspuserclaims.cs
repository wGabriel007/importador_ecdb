using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_aspuserclaims` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcAspuserclaims
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? ClaimType { get; set; }
    public string? ClaimValue { get; set; }
}
