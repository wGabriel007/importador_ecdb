using System;
using System.Windows.Forms;
using Importador_ecbd.Aplicacao.Interfaces;

namespace Apresentacao;

public partial class TelaPrincipal : Form
{
    private readonly IServicoImportacao _servicoImportacao;

    public TelaPrincipal(IServicoImportacao servicoImportacao)
    {
        InitializeComponent();
        _servicoImportacao = servicoImportacao;
    }

    private async void BotaoIniciarImportacao_Click(object sender, EventArgs e)
    {
        BotaoIniciarImportacao.Enabled = false;

        // "async void" em evento: qualquer exceção não tratada aqui fecha o programa.
        // Por isso o try/catch envolve tudo e mostra o erro para o usuário.
        try
        {
            LabelStatus.Text = "Contando registros da origem...";
            var resumo = await _servicoImportacao.ObterResumoPreImportacaoAsync();

            using var telaConfirmacao = new TelaConfirmacao(resumo);
            var resultadoDialogo = telaConfirmacao.ShowDialog(this);

            if (resultadoDialogo != DialogResult.OK)
            {
                LabelStatus.Text = "Importação cancelada pelo usuário.";
                return;
            }

            BarraProgresso.Value = 0;

            var progresso = new Progress<ProgressoImportacao>(p =>
            {
                BarraProgresso.Maximum = Math.Max(1, p.TotalTabelas);
                BarraProgresso.Value = Math.Clamp(p.TabelaAtualIndice, 0, BarraProgresso.Maximum);
                LabelStatus.Text = $"Importando {p.NomeTabelaAtual}... ({p.TabelaAtualIndice}/{p.TotalTabelas})";
            });

            var resultado = await _servicoImportacao.ImportarAsync(progresso);

            LabelStatus.Text = $"Importação concluída: {resultado.TotalRegistrosImportados} importados, " +
                               $"{resultado.RegistrosComErro.Count} com erro.";

            using var telaResultado = new TelaResultado(resultado);
            telaResultado.ShowDialog(this);
        }
        catch (Exception ex)
        {
            LabelStatus.Text = "Erro na importação.";
            MessageBox.Show(this,
                $"Ocorreu um erro inesperado:\n\n{ex.Message}\n\n{ex.InnerException?.Message}",
                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            BotaoIniciarImportacao.Enabled = true;
        }
    }
}
