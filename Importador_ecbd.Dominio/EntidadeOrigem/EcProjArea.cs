using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_proj_area` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcProjArea
{
    public int AreasId { get; set; }
    public int ProjectsId { get; set; }
}
