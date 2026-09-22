using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_country (origem) em Pais (destino).</summary>
public static class MapeadorPais
{
    public static Pais Mapear(EcCountry origem, MapaDeIds ids)
    {
        return new Pais
        {
            IdOrigem = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            Nome = origem.Name ?? string.Empty,
            Status = origem.StatusDefault ?? 0,
            CriadoEm = origem.CreatedAt ?? DateTime.UtcNow,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt ?? DateTime.UtcNow
        };
    }
}
