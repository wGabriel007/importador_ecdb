using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_responsible` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcResponsible
{
    public int Id { get; set; }
    public int InstitutionUserId { get; set; }
    public string ResppnsibleName { get; set; } = string.Empty;
    public string ResponsibleEmail { get; set; } = string.Empty;
}
