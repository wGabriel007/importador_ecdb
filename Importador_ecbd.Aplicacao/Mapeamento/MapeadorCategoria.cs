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
    public static Categoria Mapear(EcCat origem)
    {
        return new Categoria
        {
            Id = origem.Id,
            Nome = origem.Name,
            Status = origem.StatusDefault,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
