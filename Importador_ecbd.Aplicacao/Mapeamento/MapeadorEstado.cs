using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_state (origem) em Estado (destino).</summary>
public static class MapeadorEstado
{
    public static Estado Mapear(EcState origem, MapaDeIds ids)
    {
        return new Estado
        {
            IdOrigem     = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            PaisId       = ids.Traduzir<Pais>(origem.IdCountry),
            Nome         = origem.Name ?? string.Empty,
            Sigla        = origem.Acronym ?? string.Empty,
            Status       = origem.StatusDefault ?? 0,
            CriadoEm     = origem.CreatedAt ?? DateTime.UtcNow,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt ?? DateTime.UtcNow
        };
    }
}
