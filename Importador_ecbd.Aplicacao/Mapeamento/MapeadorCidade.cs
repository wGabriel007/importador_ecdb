using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_city (origem) em Cidade (destino).</summary>
public static class MapeadorCidade
{
    public static Cidade Mapear(EcCity origem)
    {
        return new Cidade
        {
            Id = origem.Id,
            EstadoId = origem.IdState, // null = sem estado (antes ia 0, que não existe e quebrava a FK)
            Nome = origem.Name ?? string.Empty,
            Status = origem.StatusDefault ?? 0,
            CriadoEm = origem.CreatedAt ?? DateTime.UtcNow,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt ?? DateTime.UtcNow
        };
    }
}
