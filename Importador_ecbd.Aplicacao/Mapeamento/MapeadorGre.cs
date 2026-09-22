using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_reg_edu_aut (origem) em Gre (destino).</summary>
public static class MapeadorGre
{
    public static Gre Mapear(EcRegEduAut origem)
    {
        return new Gre
        {
            Id = origem.Id,
            Nome = origem.Name,
            Status = origem.StatusDefault,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
