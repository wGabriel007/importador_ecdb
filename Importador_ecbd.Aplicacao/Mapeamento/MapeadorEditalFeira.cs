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
    public static EditalFeira Mapear(EcAnnoAffi origem)
    {
        return new EditalFeira
        {
            Id = origem.Id,
            FeiraAfiliadaId = origem.AffiliatedTradeFairId,
            ConfirmacaoStatus = origem.ConfirmationStatus,
            EdicaoParticipacao = origem.ParticipationEdition,
            Status = origem.StatusDefault,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
