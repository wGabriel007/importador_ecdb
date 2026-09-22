using Dominio.Origem;
using Microsoft.EntityFrameworkCore;


namespace Infraestrutura.Contextos;

/// <summary>
/// Representa a conexão com o banco de dados Antigo (Origem).
/// </summary>
public class ContextoOrigem : DbContext
{
    public ContextoOrigem(DbContextOptions<ContextoOrigem> options) : base(options)
    {
        // Define timeout padrão de comandos em segundos para evitar timeouts curtos
        try
        {
            Database.SetCommandTimeout(60);
        }
        catch
        {
            // Silencia falhas aqui; provider pode não suportar ou será gerenciado em tempo de execução
        }
    }


    public DbSet<EcAffiArea> EcAffiAreas => Set<EcAffiArea>();
    public DbSet<EcAffiliatedTradeFair> EcAffiliatedTradeFairs => Set<EcAffiliatedTradeFair>();
    public DbSet<EcAnnoAffi> EcAnnoAffis => Set<EcAnnoAffi>();
    public DbSet<EcAnnouncement> EcAnnouncements => Set<EcAnnouncement>();
    public DbSet<EcAppRole> EcAppRoles => Set<EcAppRole>();
    public DbSet<EcAppUser> EcAppUsers => Set<EcAppUser>();
    public DbSet<EcApproleclaims> EcApproleclaims => Set<EcApproleclaims>();
    public DbSet<EcAppuserlogins> EcAppuserlogins => Set<EcAppuserlogins>();
    public DbSet<EcAppuserroles> EcAppuserroles => Set<EcAppuserroles>();
    public DbSet<EcAppusertokens> EcAppusertokens => Set<EcAppusertokens>();
    public DbSet<EcArea> EcAreas => Set<EcArea>();
    public DbSet<EcAspuserclaims> EcAspuserclaims => Set<EcAspuserclaims>();
    public DbSet<EcCat> EcCats => Set<EcCat>();
    public DbSet<EcCatTheme> EcCatThemes => Set<EcCatTheme>();
    public DbSet<EcCertificate> EcCertificates => Set<EcCertificate>();
    public DbSet<EcCheckingPresence> EcCheckingPresences => Set<EcCheckingPresence>();
    public DbSet<EcCity> EcCitys => Set<EcCity>();
    public DbSet<EcCountry> EcCountrys => Set<EcCountry>();
    public DbSet<EcCriterion> EcCriterions => Set<EcCriterion>();
    public DbSet<EcEmail> EcEmails => Set<EcEmail>();
    public DbSet<EcEvalCrit> EcEvalCrits => Set<EcEvalCrit>();
    public DbSet<EcEvaluation> EcEvaluations => Set<EcEvaluation>();
    public DbSet<EcEvaluator> EcEvaluators => Set<EcEvaluator>();
    public DbSet<EcEvaluatorAnnouncement> EcEvaluatorAnnouncements => Set<EcEvaluatorAnnouncement>();
    public DbSet<EcExperiment> EcExperiments => Set<EcExperiment>();
    public DbSet<EcExperimentClass> EcExperimentClass => Set<EcExperimentClass>();
    public DbSet<EcInstitution> EcInstitutions => Set<EcInstitution>();
    public DbSet<EcInstitutionUser> EcInstitutionUsers => Set<EcInstitutionUser>();
    public DbSet<EcLevels> EcLevels => Set<EcLevels>();
    public DbSet<EcLog> EcLogs => Set<EcLog>();
    public DbSet<EcPeoplegroup> EcPeoplegroups => Set<EcPeoplegroup>();
    public DbSet<EcPermissions> EcPermissions => Set<EcPermissions>();
    public DbSet<EcProjArea> EcProjAreas => Set<EcProjArea>();
    public DbSet<EcProjImage> EcProjImages => Set<EcProjImage>();
    public DbSet<EcProjPart> EcProjParts => Set<EcProjPart>();
    public DbSet<EcProject> EcProjects => Set<EcProject>();
    public DbSet<EcRegEduAut> EcRegEduAuts => Set<EcRegEduAut>();
    public DbSet<EcRequestProjectMessage> EcRequestProjectMessages => Set<EcRequestProjectMessage>();
    public DbSet<EcResponsible> EcResponsibles => Set<EcResponsible>();
    public DbSet<EcRolePermission> EcRolePermissions => Set<EcRolePermission>();
    public DbSet<EcScheduling> EcSchedulings => Set<EcScheduling>();
    public DbSet<EcState> EcStates => Set<EcState>();
    public DbSet<EcTheme> EcThemes => Set<EcTheme>();
    public DbSet<EcUserRole> EcUserRoles => Set<EcUserRole>();
    public DbSet<EcUserpeoplegroup> EcUserpeoplegroups => Set<EcUserpeoplegroup>();
    public DbSet<EcWorkloads> EcWorkloads => Set<EcWorkloads>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EcAffiArea>(entidade =>
        {
            entidade.ToTable("ec_affi_area");
            entidade.HasKey(e => new { e.AffiliatedTradeFairsId, e.KnowledgeAreasId });
        });

        modelBuilder.Entity<EcAffiliatedTradeFair>().ToTable("ec_affiliated_trade_fair");

        modelBuilder.Entity<EcAnnoAffi>().ToTable("ec_anno_affi");

        modelBuilder.Entity<EcAnnouncement>().ToTable("ec_announcement");

        modelBuilder.Entity<EcAppRole>().ToTable("ec_app_role");

        modelBuilder.Entity<EcAppUser>().ToTable("ec_app_user");

        modelBuilder.Entity<EcApproleclaims>().ToTable("ec_approleclaims");

        modelBuilder.Entity<EcAppuserlogins>(entidade =>
        {
            entidade.ToTable("ec_appuserlogins");
            entidade.HasKey(e => new { e.LoginProvider, e.ProviderKey });
        });

        modelBuilder.Entity<EcAppuserroles>(entidade =>
        {
            entidade.ToTable("ec_appuserroles");
            entidade.HasKey(e => new { e.UserId, e.RoleId });
        });

        modelBuilder.Entity<EcAppusertokens>(entidade =>
        {
            entidade.ToTable("ec_appusertokens");
            entidade.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
        });

        modelBuilder.Entity<EcArea>().ToTable("ec_area");

        modelBuilder.Entity<EcAspuserclaims>().ToTable("ec_aspuserclaims");

        modelBuilder.Entity<EcCat>().ToTable("ec_cat");

        modelBuilder.Entity<EcCatTheme>(entidade =>
        {
            entidade.ToTable("ec_cat_theme");
            entidade.HasNoKey();
        });

        modelBuilder.Entity<EcCertificate>().ToTable("ec_certificate");

        modelBuilder.Entity<EcCheckingPresence>().ToTable("ec_checking_presence");

        modelBuilder.Entity<EcCity>().ToTable("ec_city");

        modelBuilder.Entity<EcCountry>().ToTable("ec_country");

        modelBuilder.Entity<EcCriterion>().ToTable("ec_criterion");

        modelBuilder.Entity<EcEmail>().ToTable("ec_email");

        modelBuilder.Entity<EcEvalCrit>().ToTable("ec_eval_crit");

        modelBuilder.Entity<EcEvaluation>().ToTable("ec_evaluation");

        modelBuilder.Entity<EcEvaluator>().ToTable("ec_evaluator");

        modelBuilder.Entity<EcEvaluatorAnnouncement>().ToTable("ec_evaluator_announcement");

        modelBuilder.Entity<EcExperiment>().ToTable("ec_experiment");

        modelBuilder.Entity<EcExperimentClass>().ToTable("ec_experiment_class");

        modelBuilder.Entity<EcInstitution>().ToTable("ec_institution");

        modelBuilder.Entity<EcInstitutionUser>().ToTable("ec_institution_user");

        modelBuilder.Entity<EcLevels>().ToTable("ec_levels");

        modelBuilder.Entity<EcLog>().ToTable("ec_log");

        modelBuilder.Entity<EcPeoplegroup>().ToTable("ec_peoplegroup");

        modelBuilder.Entity<EcPermissions>().ToTable("ec_permissions");

        modelBuilder.Entity<EcProjArea>(entidade =>
        {
            entidade.ToTable("ec_proj_area");
            entidade.HasKey(e => new { e.AreasId, e.ProjectsId });
        });

        modelBuilder.Entity<EcProjImage>().ToTable("ec_proj_image");

        modelBuilder.Entity<EcProjPart>().ToTable("ec_proj_part");

        modelBuilder.Entity<EcProject>().ToTable("ec_project");

        modelBuilder.Entity<EcRegEduAut>().ToTable("ec_reg_edu_aut");

        modelBuilder.Entity<EcRequestProjectMessage>().ToTable("ec_request_project_message");

        modelBuilder.Entity<EcResponsible>().ToTable("ec_responsible");

        modelBuilder.Entity<EcRolePermission>(entidade =>
        {
            entidade.ToTable("ec_role_permission");
            entidade.HasKey(e => new { e.ApplicationRolesId, e.PermissionsId });
        });

        modelBuilder.Entity<EcScheduling>().ToTable("ec_scheduling");

        modelBuilder.Entity<EcState>().ToTable("ec_state");

        modelBuilder.Entity<EcTheme>().ToTable("ec_theme");

        modelBuilder.Entity<EcUserRole>(entidade =>
        {
            entidade.ToTable("ec_user_role");
            entidade.HasKey(e => new { e.ApplicationRolesId, e.ApplicationUsersId });
        });

        modelBuilder.Entity<EcUserpeoplegroup>().ToTable("ec_userpeoplegroup");

        modelBuilder.Entity<EcWorkloads>().ToTable("ec_workloads");
    }
}
