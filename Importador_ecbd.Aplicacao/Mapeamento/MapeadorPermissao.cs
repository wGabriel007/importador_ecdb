using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte ec_permissions (origem) em Permissao (destino).
/// NOTA: origem.Order e origem.Header não têm coluna correspondente
/// em Permissao no destino — informação de ordenação/cabeçalho de
/// menu é perdida nesta importação.
/// </summary>
public static class MapeadorPermissao
{
    public static Permissao Mapear(EcPermissions origem, MapaDeIds ids)
    {
        return new Permissao
        {
            IdOrigem = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            PermissaoPaiId = ids.TraduzirOpcional<Permissao>(origem.ParentId),
            Titulo = origem.Title ?? string.Empty,
            Icone = origem.Icon ?? string.Empty,
            Acao = origem.ActionName ?? string.Empty,
            NomeControlador = origem.ControllerName ?? string.Empty,
            Status = origem.StatusDefault,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
