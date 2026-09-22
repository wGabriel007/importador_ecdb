using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_appuserroles (origem) em PermissaoUsuario (destino).</summary>
public static class MapeadorPermissaoUsuario
{
    public static PermissaoUsuario Mapear(EcAppuserroles origem)
    {
        return new PermissaoUsuario
        {
            UsuarioId = origem.UserId,
            AppPermissao = origem.RoleId
        };
    }
}
