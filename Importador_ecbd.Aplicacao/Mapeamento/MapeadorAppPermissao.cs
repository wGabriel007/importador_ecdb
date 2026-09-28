using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte ec_app_role (origem) em AppPermissao (destino) — decisão
/// confirmada: ec_app_role mapeia para AppPermissao, não para Papel.
/// </summary>
public static class MapeadorAppPermissao
{
    public static AppPermissao Mapear(EcAppRole origem, MapaDeIds ids)
    {
        return new AppPermissao
        {
            IdOrigem     = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            Nome         = origem.Name ?? string.Empty,
            Descricao    = origem.Description,
            Status       = origem.Status,
            CriadoEm     = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt
        };
    }
}
