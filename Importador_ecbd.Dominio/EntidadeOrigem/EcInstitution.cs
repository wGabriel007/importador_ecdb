using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_institution` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcInstitution
{
    public int Id { get; set; }
    public int? RegionalEducationAuthorityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string CorporativePhone { get; set; } = string.Empty;
    public string? SecondPhoneNumber { get; set; }
    public int Type { get; set; }
    public int NetworkType { get; set; }
    public string ContactEmailAddress { get; set; } = string.Empty;
    public int StatusDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public decimal? IDEB { get; set; }
    public decimal? IDHM { get; set; }
    public int? TypeSchool { get; set; }
    public int? IsActiveMec { get; set; }
    public int? Typology { get; set; }
}
