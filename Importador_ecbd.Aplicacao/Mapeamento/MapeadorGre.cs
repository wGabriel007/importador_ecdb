using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_reg_edu_aut (origem) em Gre (destino).</summary>
public static class MapeadorGre
{
    public static Gre Mapear(EcRegEduAut origem, MapaDeIds ids)
    {
        return new Gre
        {
            IdOrigem     = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            Nome         = origem.Name ?? string.Empty,
            Status       = origem.StatusDefault,
            CriadoEm     = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
