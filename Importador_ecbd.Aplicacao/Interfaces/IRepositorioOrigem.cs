using Dominio.Origem;
using Importador_ecbd.Aplicacao.Dtos;

namespace Importador_ecbd.Aplicacao.Interfaces;

public interface IRepositorioOrigem
{
    /// <summary>
    /// Contrato de leitura do banco de dados ANTIGO (origem da importação).
    /// Cada método busca todos os registros de uma tabela específica.
    /// Implementado em Infraestrutura/Repositorios/RepositorioOrigem.cs.
    /// </summary>
    public interface IRepositorioOrigem
    {
        // ---- Localização ----
        Task<List<EcCountry>> ObterCountryAsync();
        Task<List<EcState>> ObterStateAsync();
        Task<List<EcCity>> ObterCityAsync();

        // ---- Identidade e acesso ----
        Task<List<EcAppRole>> ObterAppRoleAsync();
        Task<List<EcAppUser>> ObterAppUserAsync();
        Task<List<EcApproleclaims>> ObterApproleclaimsAsync();
        Task<List<EcAppuserlogins>> ObterAppuserloginsAsync();
        Task<List<EcAppuserroles>> ObterAppuserrolesAsync();
        Task<List<EcAppusertokens>> ObterAppusertokensAsync();
        Task<List<EcAspuserclaims>> ObterAspuserclaimsAsync();
        Task<List<EcPermissions>> ObterPermissionsAsync();
        Task<List<EcRolePermission>> ObterRolePermissionAsync();
        Task<List<EcUserRole>> ObterUserRoleAsync();
        Task<List<EcLevels>> ObterLevelsAsync();

        // ---- Estrutura educacional ----
        Task<List<EcRegEduAut>> ObterRegEduAutAsync();
        Task<List<EcInstitution>> ObterInstitutionAsync();
        Task<List<EcInstitutionUser>> ObterInstitutionUserAsync();
        Task<List<EcResponsible>> ObterResponsibleAsync();

        // ---- Taxonomias ----
        Task<List<EcArea>> ObterAreaAsync();
        Task<List<EcCat>> ObterCatAsync();
        Task<List<EcCatTheme>> ObterCatThemeAsync();
        Task<List<EcTheme>> ObterThemeAsync();
        Task<List<EcCriterion>> ObterCriterionAsync();

        // ---- Editais e feiras afiliadas ----
        Task<List<EcAnnouncement>> ObterAnnouncementAsync();
        Task<List<EcAffiliatedTradeFair>> ObterAffiliatedTradeFairAsync();
        Task<List<EcAffiArea>> ObterAffiAreaAsync();
        Task<List<EcAnnoAffi>> ObterAnnoAffiAsync();

        // ---- Projetos ----
        Task<List<EcProject>> ObterProjectAsync();
        Task<List<EcProjArea>> ObterProjAreaAsync();
        Task<List<EcProjImage>> ObterProjImageAsync();
        Task<List<EcProjPart>> ObterProjPartAsync();
        Task<List<EcRequestProjectMessage>> ObterRequestProjectMessageAsync();

        // ---- Avaliação e certificação ----
        Task<List<EcEvaluator>> ObterEvaluatorAsync();
        Task<List<EcEvaluatorAnnouncement>> ObterEvaluatorAnnouncementAsync();
        Task<List<EcEvaluation>> ObterEvaluationAsync();
        Task<List<EcEvalCrit>> ObterEvalCritAsync();
        Task<List<EcCertificate>> ObterCertificateAsync();
        Task<List<EcWorkloads>> ObterWorkloadsAsync();
        Task<List<EcCheckingPresence>> ObterCheckingPresenceAsync();

        // ---- Agendamento e experimentos ----
        Task<List<EcScheduling>> ObterSchedulingAsync();
        Task<List<EcExperiment>> ObterExperimentAsync();
        Task<List<EcExperimentClass>> ObterExperimentClassAsync();
        Task<List<EcPeoplegroup>> ObterPeoplegroupAsync();
        Task<List<EcUserpeoplegroup>> ObterUserpeoplegroupAsync();

        // ---- Sistema ----
        Task<List<EcEmail>> ObterEmailAsync();
        Task<List<EcLog>> ObterLogAsync();
    }
}
