using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Apresentacao;

partial class TelaPrincipal
{
    private IContainer? components = null;
    private Button BotaoIniciarImportacao;
    private ProgressBar BarraProgresso;
    private Label LabelStatus;

    /// <summary>
    /// Método gerado manualmente para inicializar componentes mínimos necessários.
    /// </summary>
    private void InitializeComponent()
    {
        BotaoIniciarImportacao = new Button();
        BarraProgresso = new ProgressBar();
        LabelStatus = new Label();
        SuspendLayout();
        // 
        // BotaoIniciarImportacao
        // 
        BotaoIniciarImportacao.Location = new Point(228, 378);
        BotaoIniciarImportacao.Name = "BotaoIniciarImportacao";
        BotaoIniciarImportacao.Size = new Size(150, 23);
        BotaoIniciarImportacao.TabIndex = 0;
        BotaoIniciarImportacao.Text = "Iniciar importação";
        BotaoIniciarImportacao.Click += BotaoIniciarImportacao_Click;
        // 
        // BarraProgresso
        // 
        BarraProgresso.Location = new Point(103, 352);
        BarraProgresso.Name = "BarraProgresso";
        BarraProgresso.Size = new Size(400, 20);
        BarraProgresso.TabIndex = 1;
        // 
        // LabelStatus
        // 
        LabelStatus.Location = new Point(275, 407);
        LabelStatus.Name = "LabelStatus";
        LabelStatus.Size = new Size(47, 25);
        LabelStatus.TabIndex = 2;
        LabelStatus.Text = "Pronto";
        // 
        // TelaPrincipal
        // 
        ClientSize = new Size(624, 441);
        Controls.Add(BotaoIniciarImportacao);
        Controls.Add(BarraProgresso);
        Controls.Add(LabelStatus);
        Name = "TelaPrincipal";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Importador ECDB";
        ResumeLayout(false);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}
