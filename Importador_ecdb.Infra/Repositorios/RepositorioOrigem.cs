using Dominio.Origem;
using Importador_ecbd.Aplicacao.Interfaces;
using Infraestrutura.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorios;

/// <summary>
/// Implementação de leitura do banco de dados ANTIGO. Cada método
/// busca todos os registros de uma tabela.
/// </summary>
public class RepositorioOrigem : IRepositorioOrigem
{
    private readonly ContextoOrigem _contexto;

    public RepositorioOrigem(ContextoOrigem contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<EcAffiArea>> ObterAffiAreaAsync()
    {
        return await _contexto.EcAffiAreas.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcAffiliatedTradeFair>> ObterAffiliatedTradeFairAsync()
    {
        return await _contexto.EcAffiliatedTradeFairs.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcAnnoAffi>> ObterAnnoAffiAsync()
    {
        return await _contexto.EcAnnoAffis.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcAnnouncement>> ObterAnnouncementAsync()
    {
        return await _contexto.EcAnnouncements.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcAppRole>> ObterAppRoleAsync()
    {
        return await _contexto.EcAppRoles.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcAppUser>> ObterAppUserAsync()
    {
        return await _contexto.EcAppUsers.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcApproleclaims>> ObterApproleclaimsAsync()
    {
        return await _contexto.EcApproleclaims.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcAppuserlogins>> ObterAppuserloginsAsync()
    {
        return await _contexto.EcAppuserlogins.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcAppuserroles>> ObterAppuserrolesAsync()
    {
        return await _contexto.EcAppuserroles.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcAppusertokens>> ObterAppusertokensAsync()
    {
        return await _contexto.EcAppusertokens.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcArea>> ObterAreaAsync()
    {
        return await _contexto.EcAreas.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcAspuserclaims>> ObterAspuserclaimsAsync()
    {
        return await _contexto.EcAspuserclaims.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcCat>> ObterCatAsync()
    {
        return await _contexto.EcCats.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcCatTheme>> ObterCatThemeAsync()
    {
        return await _contexto.EcCatThemes.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcCertificate>> ObterCertificateAsync()
    {
        return await _contexto.EcCertificates.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcCheckingPresence>> ObterCheckingPresenceAsync()
    {
        return await _contexto.EcCheckingPresences.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcCity>> ObterCityAsync()
    {
        return await _contexto.EcCitys.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcCountry>> ObterCountryAsync()
    {
        return await _contexto.EcCountrys.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcCriterion>> ObterCriterionAsync()
    {
        return await _contexto.EcCriterions.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcEmail>> ObterEmailAsync()
    {
        return await _contexto.EcEmails.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcEvalCrit>> ObterEvalCritAsync()
    {
        return await _contexto.EcEvalCrits.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcEvaluation>> ObterEvaluationAsync()
    {
        return await _contexto.EcEvaluations.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcEvaluator>> ObterEvaluatorAsync()
    {
        return await _contexto.EcEvaluators.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcEvaluatorAnnouncement>> ObterEvaluatorAnnouncementAsync()
    {
        return await _contexto.EcEvaluatorAnnouncements.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcExperiment>> ObterExperimentAsync()
    {
        return await _contexto.EcExperiments.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcExperimentClass>> ObterExperimentClassAsync()
    {
        return await _contexto.EcExperimentClass.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcInstitution>> ObterInstitutionAsync()
    {
        return await _contexto.EcInstitutions.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcInstitutionUser>> ObterInstitutionUserAsync()
    {
        return await _contexto.EcInstitutionUsers.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcLevels>> ObterLevelsAsync()
    {
        return await _contexto.EcLevels.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcLog>> ObterLogAsync()
    {
        return await _contexto.EcLogs.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcPeoplegroup>> ObterPeoplegroupAsync()
    {
        return await _contexto.EcPeoplegroups.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcPermissions>> ObterPermissionsAsync()
    {
        return await _contexto.EcPermissions.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcProjArea>> ObterProjAreaAsync()
    {
        return await _contexto.EcProjAreas.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcProjImage>> ObterProjImageAsync()
    {
        return await _contexto.EcProjImages.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcProjPart>> ObterProjPartAsync()
    {
        return await _contexto.EcProjParts.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcProject>> ObterProjectAsync()
    {
        return await _contexto.EcProjects.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcRegEduAut>> ObterRegEduAutAsync()
    {
        return await _contexto.EcRegEduAuts.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcRequestProjectMessage>> ObterRequestProjectMessageAsync()
    {
        return await _contexto.EcRequestProjectMessages.AsNoTracking().ToListAsync();
    }

    public async Task<int> ContarPorTabelaAsync(string tabela)
    {
        if (string.IsNullOrWhiteSpace(tabela))
            return 0;

        // Protege contra caracteres estranhos na tabela (apenas alfanumérico, underscore e ponto)
        // embora aqui o nome venha de código fixo, esta validação reduz risco de injeção.
        foreach (var c in tabela)
        {
            if (!char.IsLetterOrDigit(c) && c != '_' && c != '.' && c != '`')
                throw new ArgumentException("Nome de tabela contém caracteres inválidos.", nameof(tabela));
        }

        var conn = _contexto.Database.GetDbConnection();
        try
        {
            if (conn.State != System.Data.ConnectionState.Open)
                await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            // Usa crase para nomes com letras minúsculas e underscores; tabela deve ser passada sem crases
            cmd.CommandText = $"SELECT COUNT(*) FROM `{tabela}`;";
            var result = await cmd.ExecuteScalarAsync();
            if (result == null || result == DBNull.Value)
                return 0;
            return Convert.ToInt32(result);
        }
        finally
        {
            // Não fecha a conexão explicitamente para deixar o DbContext/Provider gerenciar o pool
        }
    }

    public async Task<List<EcResponsible>> ObterResponsibleAsync()
    {
        return await _contexto.EcResponsibles.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcRolePermission>> ObterRolePermissionAsync()
    {
        return await _contexto.EcRolePermissions.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcScheduling>> ObterSchedulingAsync()
    {
        return await _contexto.EcSchedulings.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcState>> ObterStateAsync()
    {
        return await _contexto.EcStates.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcTheme>> ObterThemeAsync()
    {
        return await _contexto.EcThemes.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcUserRole>> ObterUserRoleAsync()
    {
        return await _contexto.EcUserRoles.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcUserpeoplegroup>> ObterUserpeoplegroupAsync()
    {
        return await _contexto.EcUserpeoplegroups.AsNoTracking().ToListAsync();
    }

    public async Task<List<EcWorkloads>> ObterWorkloadsAsync()
    {
        return await _contexto.EcWorkloads.AsNoTracking().ToListAsync();
    }
}