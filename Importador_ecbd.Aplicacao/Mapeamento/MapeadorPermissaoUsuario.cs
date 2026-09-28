using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_appuserroles (origem) em PermissaoUsuario (destino).</summary>
public static class MapeadorPermissaoUsuario
{
    public static PermissaoUsuario Mapear(EcAppuserroles origem, MapaDeIds ids)
    {
        return new PermissaoUsuario
        {
            UsuarioId    = ids.Traduzir<Usuario>(origem.UserId),
            AppPermissao = ids.Traduzir<global::Dominio.Destino.AppPermissao>(origem.RoleId)
        };
    }
}
