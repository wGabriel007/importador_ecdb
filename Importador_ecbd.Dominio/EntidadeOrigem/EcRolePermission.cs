using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_role_permission` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcRolePermission
{
    public int ApplicationRolesId { get; set; }
    public int PermissionsId { get; set; }
}
