using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte ec_cat (origem) em Categoria (destino).
/// NOTA: a Categoria de destino não tem coluna de categoria-pai —
/// a hierarquia (FatherCategoryId) da origem é perdida nesta
/// importação. Se a hierarquia for necessária, adicione a coluna
/// no banco de destino antes de importar.
/// </summary>
public static class MapeadorCategoria
{
    public static Categoria Mapear(EcCat origem, MapaDeIds ids)
    {
        return new Categoria
        {
            IdOrigem     = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            Nome         = origem.Name ?? string.Empty,
            Status       = origem.StatusDefault,
            CriadoEm     = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
