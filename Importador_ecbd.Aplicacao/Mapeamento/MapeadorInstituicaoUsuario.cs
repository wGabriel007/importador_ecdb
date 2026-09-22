using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_institution_user (origem) em InstituicaoUsuario (destino).</summary>
public static class MapeadorInstituicaoUsuario
{
    public static InstituicaoUsuario Mapear(EcInstitutionUser origem, MapaDeIds ids)
    {
        return new InstituicaoUsuario
        {
            IdOrigem = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            InstituicaoId = ids.Traduzir<Instituicao>(origem.InstitutionId),
            UsuarioId = ids.Traduzir<Usuario>(origem.ApplicationUserId),
            Referencia = origem.Reference,
            Tipo = origem.Type,
            NivelAcademico = origem.AcademicLevel,
            JaParticipouCj = origem.HasParticipatedPreviousCJ,
            AnoParticipacaoCj = origem.PreviousCJ,
            PossueExperienciaEmFeiras = origem.HasExternalFairExperience,
            ExperienciaDeFeiras = origem.ExternalFairExperience
        };
    }
}
