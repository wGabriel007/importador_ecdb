using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte ec_criterion (origem) em Criterio (destino).
/// NOTA: origem.Type (resumo/apresentação) não tem coluna
/// correspondente em Criterio no destino — essa informação se perde
/// nesta importação, a menos que a coluna seja adicionada.
/// </summary>
public static class MapeadorCriterio
{
    public static Criterio Mapear(EcCriterion origem, MapaDeIds ids)
    {
        return new Criterio
        {
            IdOrigem     = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            CategoriaId  = ids.TraduzirOpcional<Categoria>(origem.ProjectCategoryId),
            Nome         = origem.Name ?? string.Empty,
            Descricao    = origem.Description,
            Peso         = origem.Weight,
            Status       = origem.StatusDefault,
            CriadoEm     = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
