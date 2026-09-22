using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_country (origem) em Pais (destino).</summary>
public static class MapeadorPais
{
    public static Pais Mapear(EcCountry origem)
    {
        return new Pais
        {
            Id = origem.Id, // preserva o Id original — ver nota sobre FKs no guia
            Nome = origem.Name ?? string.Empty,
            Status = origem.StatusDefault ?? 0,
            CriadoEm = origem.CreatedAt ?? DateTime.UtcNow,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt ?? DateTime.UtcNow
        };
    }
}
