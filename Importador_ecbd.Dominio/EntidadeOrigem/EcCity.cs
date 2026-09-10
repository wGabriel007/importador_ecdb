using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_city` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcCity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? IdState { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int? StatusDefault { get; set; }
}
