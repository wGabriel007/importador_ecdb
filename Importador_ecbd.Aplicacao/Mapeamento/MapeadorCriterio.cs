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
    public static Criterio Mapear(EcCriterion origem)
    {
        return new Criterio
        {
            Id = origem.Id,
            CategoriaId = origem.ProjectCategoryId,
            Nome = origem.Name,
            Descricao = origem.Description,
            Peso = origem.Weight,
            Status = origem.StatusDefault,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
