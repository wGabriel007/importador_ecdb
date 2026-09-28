using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_city (origem) em Cidade (destino).</summary>
public static class MapeadorCidade
{
    public static Cidade Mapear(EcCity origem, MapaDeIds ids)
    {
        return new Cidade
        {
            IdOrigem     = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            EstadoId     = ids.TraduzirOpcional<Estado>(origem.IdState),
            Nome         = origem.Name ?? string.Empty,
            Status       = origem.StatusDefault ?? 0,
            CriadoEm     = origem.CreatedAt ?? DateTime.UtcNow,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt ?? DateTime.UtcNow
        };
    }
}
