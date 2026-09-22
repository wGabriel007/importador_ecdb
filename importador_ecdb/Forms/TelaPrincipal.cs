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

        var resumo = await _servicoImportacao.ObterResumoPreImportacaoAsync();

        using var telaConfirmacao = new TelaConfirmacao(resumo);
        var resultadoDialogo = telaConfirmacao.ShowDialog(this);

        if (resultadoDialogo != DialogResult.OK)
        {
            BotaoIniciarImportacao.Enabled = true;
            LabelStatus.Text = "Importação cancelada pelo usuário.";
            return;
        }

        BarraProgresso.Value = 0;

        var progresso = new Progress<ProgressoImportacao>(p =>
        {
            // Define o máximo dinamicamente com base no TotalTabelas reportado pelo serviço.
            try
            {
                if (BarraProgresso.Maximum != p.TotalTabelas)
                    BarraProgresso.Maximum = Math.Max(1, p.TotalTabelas);
            }
            catch { }

            // Ajusta o valor de forma segura
            var value = Math.Clamp(p.TabelaAtualIndice, 0, BarraProgresso.Maximum);
            BarraProgresso.Value = value;
            LabelStatus.Text = $"Importando {p.NomeTabelaAtual}... ({p.TabelaAtualIndice}/{p.TotalTabelas})";
        });

        var resultado = await _servicoImportacao.ImportarAsync(progresso);

        LabelStatus.Text = "Importação concluída.";
        BotaoIniciarImportacao.Enabled = true;

        using var telaResultado = new TelaResultado(resultado);
        telaResultado.ShowDialog(this);
    }
}