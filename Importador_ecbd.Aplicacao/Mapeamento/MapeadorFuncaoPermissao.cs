using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_role_permission (origem) em FuncaoPermissao (destino).</summary>
public static class MapeadorFuncaoPermissao
{
    public static FuncaoPermissao Mapear(EcRolePermission origem)
    {
        return new FuncaoPermissao
        {
            AppPermissaoId = origem.ApplicationRolesId,
            PermissaoId = origem.PermissionsId
        };
    }
}
