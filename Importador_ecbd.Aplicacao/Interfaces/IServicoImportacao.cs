using Importador_ecbd.Aplicacao.Dtos;

namespace Importador_ecbd.Aplicacao.Interfaces;

/// <summary>
/// Contrato de importação responsavel por toda a lógica de importação.
/// a camada de UI só conhece essa interface.
/// </summary>
public interface IServicoImportacao
{
    /// <summary>
    /// Conta os registros de cada tabela de origem, para a tela de confirmação.
    /// </summary>
    Task<ResumoPreImportacao> ObterResumoPreImportacaoAsync();

    /// <summary>
    /// Executa a importação completa.
    /// </summary>
    Task<ResultadoImportacao> ImportarAsync();
}

/// <summary>
/// Informação de progresso reportada durante a importação.
/// </summary>
public class ProgressoImportacao
{
    public int TabelaAtualIndice {  get; set; }
    public int TotalTabelas { get; set; }
    public string NomeTabelaAtual {  get; set; } = string.Empty;
}