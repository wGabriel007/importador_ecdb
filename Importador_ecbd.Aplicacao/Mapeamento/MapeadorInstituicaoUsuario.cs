using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_institution_user (origem) em InstituicaoUsuario (destino).</summary>
public static class MapeadorInstituicaoUsuario
{
    public static InstituicaoUsuario Mapear(EcInstitutionUser origem)
    {
        return new InstituicaoUsuario
        {
            Id = origem.Id,
            InstituicaoId = origem.InstitutionId,
            UsuarioId = origem.ApplicationUserId,
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
