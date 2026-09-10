using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_app_user` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcAppUser
{
    public int Id { get; set; }
    public string Fullname { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public int Score { get; set; }
    public int? LevelId { get; set; }
    public string? Email { get; set; }
    public string? NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PasswordHash { get; set; }
    public string? SecurityStamp { get; set; }
    public string? ConcurrencyStamp { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public DateTime BirthDay { get; set; }
    public int GenderIdentify { get; set; }
    public string? NormalizedUserName { get; set; }
    public string? UserName { get; set; }
    public string? Country { get; set; }
    public int? IdCountry { get; set; }
    public int? IdState { get; set; }
    public int? IdCity { get; set; }
    public int Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? CityId { get; set; }
    public int? StateId { get; set; }
    public int? IdRace { get; set; }
}
