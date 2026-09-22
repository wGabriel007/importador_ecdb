using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_state (origem) em Estado (destino).</summary>
public static class MapeadorEstado
{
    public static Estado Mapear(EcState origem)
    {
        return new Estado
        {
            Id = origem.Id,
            PaisId = origem.IdCountry ?? 0, // Pais preserva o mesmo Id, então isso continua válido
            Nome = origem.Name ?? string.Empty,
            Sigla = origem.Acronym,
            Status = origem.StatusDefault ?? 0,
            CriadoEm = origem.CreatedAt ?? DateTime.UtcNow,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt ?? DateTime.UtcNow
        };
    }
}
