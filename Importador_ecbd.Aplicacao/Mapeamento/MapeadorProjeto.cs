using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte ec_project (origem) em Projeto (destino).
/// origem.AnnouncementId não é usado (edital removido do modelo novo).
/// ATENÇÃO: Projeto.FeiraAfiliadaId no destino é obrigatório (não
/// nullable), mas na origem é opcional — projetos sem feira afiliada
/// vão falhar ao salvar e aparecer na lista de erros da importação
/// (comportamento esperado, não um bug do mapeador).
/// </summary>
public static class MapeadorProjeto
{
    public static Projeto Mapear(EcProject origem)
    {
        return new Projeto
        {
            Id = origem.Id,
            CategoriaId = origem.ProjectCategoryId,
            InstituicaoId = origem.InstitutionId,
            FeiraAfiliadaId = origem.AffiliatedTradeFairId ?? 0,
            Titulo = origem.Title,
            Introducao = origem.Introduction ?? string.Empty,
            Objetivo = origem.Objectives,
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
            Tema = origem.IdTheme,
            TemaProjeto = origem.ProjectThemeId
        };
    }
}
