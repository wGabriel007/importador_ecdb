using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte ec_affiliated_trade_fair (origem) em FeiraAfiliada (destino).
/// Vários campos ficaram como TODO porque a origem guarda como texto
/// livre o que o destino espera como código (enum) ou Id — listados
/// nos comentários abaixo.
/// </summary>
public static class MapeadorFeiraAfiliada
{
    public static FeiraAfiliada Mapear(EcAffiliatedTradeFair origem)
    {
        return new FeiraAfiliada
        {
            Id = origem.Id,
            UsuarioId = origem.OrganizerId,
            // TODO: origem guarda City/State/Country como texto livre,
            // não como Id — precisa de correspondência por nome.
            CidadeId = null,
            InstituicaoId = origem.InstitutionId,
            Nome = origem.Name,
            Endereco = origem.Address,
            // TODO: origem.Range é texto livre ("Municipal", "Estadual"...),
            // destino.Alcance é int (enum) — precisa de tabela de conversão.
            Alcance = null,
            EscolasParticipantes = origem.ParticipationSchoolsQuantity,
            AvaliadoNaFeira = origem.IsProjectsEvaluatedInFair ? 1 : 0,
            DataInicioRealizacao = origem.StartRealizationDate,
            DataFinalRealizacao = origem.EndRealizationDate,
            // TODO: origem.Period é texto livre, destino.Periodo é int (enum).
            Periodo = null,
            QuantidadeProjetos = origem.ParticipationProjectsQuantity,
            DescricaoProcessoSelecao = origem.SelectWorksProcessDescription,
            TipoParticipacaoEstudante = origem.StudentParticipationType,
            JaParticipou = origem.ParticpatedAnotherYears ? 1 : 0,
            DescricaoParticipacaoAno = origem.ParticipatedAnotherYearsDescription,
            // TODO: sem coluna de origem clara para GrauEstudante.
            GrauEstudante = null,
            Status = origem.StatusDefault,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
