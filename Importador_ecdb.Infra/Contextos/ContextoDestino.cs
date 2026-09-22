using Dominio.Destino;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Contextos;

/// <summary>
/// Representa a conexão com o banco de dados Novo (destino)
/// </summary>
public class ContextoDestino : DbContext
{
    public ContextoDestino(DbContextOptions<ContextoDestino> options) : base(options)
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

    public DbSet<AppPermissao> AppPermissaos => Set<AppPermissao>();
    public DbSet<AreaConhecimento> AreaConhecimentos => Set<AreaConhecimento>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Cidade> Cidades => Set<Cidade>();
    public DbSet<Criterio> Criterios => Set<Criterio>();
    public DbSet<EditalFeira> EditalFeiras => Set<EditalFeira>();
    public DbSet<Estado> Estados => Set<Estado>();
    public DbSet<FeiraAfiliada> FeiraAfiliadas => Set<FeiraAfiliada>();
    public DbSet<FeiraArea> FeiraAreas => Set<FeiraArea>();
    public DbSet<FuncaoPermissao> FuncaoPermissaos => Set<FuncaoPermissao>();
    public DbSet<Gre> Gres => Set<Gre>();
    public DbSet<Instituicao> Instituicaos => Set<Instituicao>();
    public DbSet<InstituicaoUsuario> InstituicaoUsuarios => Set<InstituicaoUsuario>();
    public DbSet<Pais> Pais => Set<Pais>();
    public DbSet<Papel> Papels => Set<Papel>();
    public DbSet<Permissao> Permissaos => Set<Permissao>();
    public DbSet<PermissaoUsuario> PermissaoUsuarios => Set<PermissaoUsuario>();
    public DbSet<Projeto> Projetos => Set<Projeto>();
    public DbSet<Tema> Temas => Set<Tema>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppPermissao>(entidade =>
        {
            entidade.ToTable("ec_tb_app_permissao");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Descricao).HasColumnName("sDescricao");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<AreaConhecimento>(entidade =>
        {
            entidade.ToTable("ec_tb_area_conhecimento");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.AreaPrincipalId).HasColumnName("iAreaPrincipalId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Tipo).HasColumnName("iTipo");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<Categoria>(entidade =>
        {
            entidade.ToTable("ec_tb_categoria");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<Cidade>(entidade =>
        {
            entidade.ToTable("ec_tb_cidade");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.EstadoId).HasColumnName("iEstadoId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<Criterio>(entidade =>
        {
            entidade.ToTable("ec_tb_criterio");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.CategoriaId).HasColumnName("iCategoriaId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Descricao).HasColumnName("sDescricao");
            entidade.Property(e => e.Peso).HasColumnName("iPeso");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<EditalFeira>(entidade =>
        {
            entidade.ToTable("ec_tb_edital_feira");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.FeiraAfiliadaId).HasColumnName("iFeiraAfiliadaId");
            entidade.Property(e => e.ConfirmacaoStatus).HasColumnName("iConfirmacaoStatus");
            entidade.Property(e => e.EdicaoParticipacao).HasColumnName("sEdicaoParticipacao");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<Estado>(entidade =>
        {
            entidade.ToTable("ec_tb_estado");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.PaisId).HasColumnName("iPaisId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Sigla).HasColumnName("sSigla");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<FeiraAfiliada>(entidade =>
        {
            entidade.ToTable("ec_tb_feira_afiliada");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.UsuarioId).HasColumnName("iUsuarioId");
            entidade.Property(e => e.CidadeId).HasColumnName("iCidadeId");
            entidade.Property(e => e.InstituicaoId).HasColumnName("iInstituicaoId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Endereco).HasColumnName("sEndereco");
            entidade.Property(e => e.Alcance).HasColumnName("iAlcance");
            entidade.Property(e => e.EscolasParticipantes).HasColumnName("iEscolasParticipantes");
            entidade.Property(e => e.AvaliadoNaFeira).HasColumnName("iAvaliadoNaFeira");
            entidade.Property(e => e.DataInicioRealizacao).HasColumnName("dtDataInicioRealizacao");
            entidade.Property(e => e.DataRealizacao).HasColumnName("dtDataRealizacao");
            entidade.Property(e => e.DataFinalRealizacao).HasColumnName("dtDataFinalRealizacao");
            entidade.Property(e => e.Periodo).HasColumnName("iPeriodo");
            entidade.Property(e => e.QuantidadeProjetos).HasColumnName("iQuantidadeProjetos");
            entidade.Property(e => e.DescricaoProjeto).HasColumnName("sDescricaoProjeto");
            entidade.Property(e => e.DescricaoProcessoSelecao).HasColumnName("sDescricaoProcessoSelecao");
            entidade.Property(e => e.TipoParticipacaoEstudante).HasColumnName("iTipoParticipacaoEstudante");
            entidade.Property(e => e.JaParticipou).HasColumnName("iJaParticipou");
            entidade.Property(e => e.DescricaoParticipacaoAno).HasColumnName("sDescricaoParticipacaoAno");
            entidade.Property(e => e.GrauEstudante).HasColumnName("iGrauEstudante");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<FeiraArea>(entidade =>
        {
            entidade.ToTable("ec_tb_feira_area");
            entidade.HasKey(e => new { e.FeiraAfiliadaId, e.AreaConhecimentoId });
            entidade.Property(e => e.FeiraAfiliadaId).HasColumnName("iFeiraAfiliadaId");
            entidade.Property(e => e.AreaConhecimentoId).HasColumnName("iAreaConhecimentoId");
        });

        modelBuilder.Entity<FuncaoPermissao>(entidade =>
        {
            entidade.ToTable("ec_tb_funcao_permissao");
            entidade.HasKey(e => new { e.AppPermissaoId, e.PermissaoId });
            entidade.Property(e => e.AppPermissaoId).HasColumnName("iAppPermissaoId");
            entidade.Property(e => e.PermissaoId).HasColumnName("iPermissaoId");
        });

        modelBuilder.Entity<Gre>(entidade =>
        {
            entidade.ToTable("ec_tb_gre");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<Instituicao>(entidade =>
        {
            entidade.ToTable("ec_tb_instituicao");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.GreId).HasColumnName("iGreId");
            entidade.Property(e => e.CidadeId).HasColumnName("iCidadeId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Documento).HasColumnName("sDocumento");
            entidade.Property(e => e.Endereco).HasColumnName("sEndereco");
            entidade.Property(e => e.Telefone).HasColumnName("sTelefone");
            entidade.Property(e => e.TelefoneCorporativo).HasColumnName("sTelefoneCorporativo");
            entidade.Property(e => e.Tipologia).HasColumnName("iTipologia");
            entidade.Property(e => e.TipoRede).HasColumnName("iTipoRede");
            entidade.Property(e => e.Tipo).HasColumnName("iTipo");
            entidade.Property(e => e.Email).HasColumnName("sEmail");
            entidade.Property(e => e.TipoEscola).HasColumnName("iTipoEscola");
            entidade.Property(e => e.Idhm).HasColumnName("iIdhm");
            entidade.Property(e => e.Ideb).HasColumnName("iIdeb");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<InstituicaoUsuario>(entidade =>
        {
            entidade.ToTable("ec_tb_instituicao_usuario");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.InstituicaoId).HasColumnName("iInstituicaoId");
            entidade.Property(e => e.UsuarioId).HasColumnName("iUsuarioId");
            entidade.Property(e => e.Referencia).HasColumnName("sReferencia");
            entidade.Property(e => e.Tipo).HasColumnName("iTipo");
            entidade.Property(e => e.NivelAcademico).HasColumnName("iNivelAcademico");
            entidade.Property(e => e.JaParticipouCj).HasColumnName("iJaParticipouCj");
            entidade.Property(e => e.AnoParticipacaoCj).HasColumnName("sAnoParticipacaoCj");
            entidade.Property(e => e.PossueExperienciaEmFeiras).HasColumnName("iPossueExperienciaEmFeiras");
            entidade.Property(e => e.ExperienciaDeFeiras).HasColumnName("sExperienciaDeFeiras");
        });

        modelBuilder.Entity<Pais>(entidade =>
        {
            entidade.ToTable("ec_tb_pais");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<Papel>(entidade =>
        {
            entidade.ToTable("ec_tb_papel");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Descricao).HasColumnName("sDescricao");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<Permissao>(entidade =>
        {
            entidade.ToTable("ec_tb_permissao");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.PermissaoPaiId).HasColumnName("iPermissaoPaiId");
            entidade.Property(e => e.Titulo).HasColumnName("sTitulo");
            entidade.Property(e => e.Icone).HasColumnName("sIcone");
            entidade.Property(e => e.Acao).HasColumnName("sAcao");
            entidade.Property(e => e.NomeControlador).HasColumnName("sNomeControlador");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<PermissaoUsuario>(entidade =>
        {
            entidade.ToTable("ec_tb_permissao_usuario");
            entidade.HasKey(e => new { e.UsuarioId, e.AppPermissao });
            entidade.Property(e => e.UsuarioId).HasColumnName("iUsuarioId");
            entidade.Property(e => e.AppPermissao).HasColumnName("iAppPermissao");
        });

        modelBuilder.Entity<Projeto>(entidade =>
        {
            entidade.ToTable("ec_tb_projeto");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.CategoriaId).HasColumnName("iCategoriaId");
            entidade.Property(e => e.InstituicaoId).HasColumnName("iInstituicaoId");
            entidade.Property(e => e.FeiraAfiliadaId).HasColumnName("iFeiraAfiliadaId");
            entidade.Property(e => e.Titulo).HasColumnName("sTitulo");
            entidade.Property(e => e.Introducao).HasColumnName("sIntroducao");
            entidade.Property(e => e.Objetivo).HasColumnName("sObjetivo");
            entidade.Property(e => e.Metodologia).HasColumnName("sMetodologia");
            entidade.Property(e => e.Resultado).HasColumnName("sResultado");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.TipoApresentacao).HasColumnName("iTipoApresentacao");
            entidade.Property(e => e.DescricaoCancelamento).HasColumnName("sDescricaoCancelamento");
            entidade.Property(e => e.EducacaoEspecial).HasColumnName("iEducacaoEspecial");
            entidade.Property(e => e.PalavraChave).HasColumnName("sPalavraChave");
            entidade.Property(e => e.PreInscricao).HasColumnName("iPreInscricao");
            entidade.Property(e => e.Bibliografia).HasColumnName("sBibliografia");
            entidade.Property(e => e.VideoUrl).HasColumnName("sVideoUrl");
            entidade.Property(e => e.LinkCarta).HasColumnName("sLinkCarta");
            entidade.Property(e => e.StatusConfirmacao).HasColumnName("iStatusConfirmacao");
            entidade.Property(e => e.IdeaiaTec).HasColumnName("iIdeaiaTec");
            entidade.Property(e => e.Sumario).HasColumnName("sSumario");
            entidade.Property(e => e.Tema).HasColumnName("iTema");
            entidade.Property(e => e.TemaProjeto).HasColumnName("iTemaProjeto");
        });

        modelBuilder.Entity<Tema>(entidade =>
        {
            entidade.ToTable("ec_tb_tema");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.Nome).HasColumnName("sNome");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });

        modelBuilder.Entity<Usuario>(entidade =>
        {
            entidade.ToTable("ec_tb_usuario");
            entidade.HasKey(e => e.Id);
            entidade.Property(e => e.Id).HasColumnName("iId");
            entidade.Property(e => e.CidadeId).HasColumnName("iCidadeId");
            entidade.Property(e => e.NomeCompleto).HasColumnName("sNomeCompleto");
            entidade.Property(e => e.Genero).HasColumnName("iGenero");
            entidade.Property(e => e.Documento).HasColumnName("sDocumento");
            entidade.Property(e => e.Email).HasColumnName("sEmail");
            entidade.Property(e => e.SenhaHash).HasColumnName("sSenhaHash");
            entidade.Property(e => e.Telefone).HasColumnName("sTelefone");
            entidade.Property(e => e.DataNascimento).HasColumnName("dtDataNascimento");
            entidade.Property(e => e.Pontuacao).HasColumnName("iPontuacao");
            entidade.Property(e => e.EmailConfirmado).HasColumnName("iEmailConfirmado");
            entidade.Property(e => e.Status).HasColumnName("iStatus");
            entidade.Property(e => e.CriadoEm).HasColumnName("dtCriadoEm");
            entidade.Property(e => e.AtualizadoEm).HasColumnName("dtAtualizadoEm");
        });
    }
}