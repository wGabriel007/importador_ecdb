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
            EstadoId = origem.IdState ?? 0,
            Nome = origem.Name,
            Status = origem.StatusDefault ?? 0,
            CriadoEm = origem.CreatedAt ?? DateTime.UtcNow,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt ?? DateTime.UtcNow
        };
    }
}
