using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_proj_part` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcProjPart
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public int InstitutionUserId { get; set; }
    public int Type { get; set; }
}
