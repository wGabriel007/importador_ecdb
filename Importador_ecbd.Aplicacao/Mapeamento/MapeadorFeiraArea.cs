using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_affi_area (origem) em FeiraArea (destino).</summary>
public static class MapeadorFeiraArea
{
    public static FeiraArea Mapear(EcAffiArea origem, MapaDeIds ids)
    {
        return new FeiraArea
        {
            FeiraAfiliadaId    = ids.Traduzir<FeiraAfiliada>(origem.AffiliatedTradeFairsId),
            AreaConhecimentoId = ids.Traduzir<AreaConhecimento>(origem.KnowledgeAreasId)
        };
    }
}
