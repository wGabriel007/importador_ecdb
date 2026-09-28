using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte ec_anno_affi (origem) em EditalFeira (destino).
/// origem.AnnouncementId não é usado — o edital foi removido do
/// modelo novo, então essa referência não existe mais no destino.
/// </summary>
public static class MapeadorEditalFeira
{
    public static EditalFeira Mapear(EcAnnoAffi origem, MapaDeIds ids)
    {
        return new EditalFeira
        {
            IdOrigem           = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            FeiraAfiliadaId    = ids.Traduzir<FeiraAfiliada>(origem.AffiliatedTradeFairId),
            ConfirmacaoStatus  = origem.ConfirmationStatus,
            EdicaoParticipacao = origem.ParticipationEdition,
            Status             = origem.StatusDefault,
            CriadoEm           = origem.CreatedAt,
            AtualizadoEm       = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
