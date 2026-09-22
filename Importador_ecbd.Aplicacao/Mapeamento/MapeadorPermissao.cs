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
    public static Permissao Mapear(EcPermissions origem)
    {
        return new Permissao
        {
            Id = origem.Id,
            PermissaoPaiId = origem.ParentId,
            Titulo = origem.Title,
            Icone = origem.Icon,
            Acao = origem.ActionName,
            NomeControlador = origem.ControllerName,
            Status = origem.StatusDefault,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
