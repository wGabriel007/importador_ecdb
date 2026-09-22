using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte ec_project (origem) em Projeto (destino).
/// origem.AnnouncementId não é usado (edital removido do modelo novo).
/// Projeto sem feira afiliada na origem fica com FeiraAfiliadaId = null.
/// </summary>
public static class MapeadorProjeto
{
    public static Projeto Mapear(EcProject origem, MapaDeIds ids)
    {
        return new Projeto
        {
            IdOrigem = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            CategoriaId = ids.Traduzir<Categoria>(origem.ProjectCategoryId),
            InstituicaoId = ids.Traduzir<Instituicao>(origem.InstitutionId),
            FeiraAfiliadaId = ids.TraduzirOpcional<FeiraAfiliada>(origem.AffiliatedTradeFairId), // null = projeto sem feira
            Titulo = origem.Title ?? string.Empty,
            Introducao = origem.Introduction ?? string.Empty,
            Objetivo = origem.Objectives ?? string.Empty,
            Metodologia = origem.Methodology ?? string.Empty,
            Resultado = origem.Results ?? string.Empty,
            Status = origem.Status,
            TipoApresentacao = origem.PresentationType,
            DescricaoCancelamento = origem.DescriptionCancel,
            EducacaoEspecial = origem.IsParticipantsInSpecialEducationModality ? 1 : 0,
            PalavraChave = origem.Keywords,
            PreInscricao = origem.IsDoesInPreInscription ? 1 : 0,
            Bibliografia = origem.Bibliography,
            VideoUrl = origem.VideoUrl,
            LinkCarta = origem.CredenceLetterLink,
            StatusConfirmacao = origem.ConfirmationStatus,
            IdeaiaTec = origem.Ideiatec,
            Sumario = origem.Summary,
            Tema = ids.TraduzirOpcional<global::Dominio.Destino.Tema>(origem.IdTheme),
            // ATENÇÃO: assumindo que ProjectThemeId também aponta para ec_theme — confirmar
            TemaProjeto = ids.TraduzirOpcional<global::Dominio.Destino.Tema>(origem.ProjectThemeId)
        };
    }
}
