using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Importador_ecbd.Aplicacao.Dtos;
using Importador_ecbd.Aplicacao.Servicos;

namespace Apresentacao;

/// <summary>
/// Mostra o relatório da importação em texto legível e salva uma cópia
/// automática em arquivo .txt ao lado do executável.
/// </summary>
public class TelaResultado : Form
{
    private readonly string _relatorio;

    public TelaResultado(ResultadoImportacao resultado)
    {
        _relatorio = GeradorRelatorio.Gerar(resultado);

        Text = "Resultado da importação";
        StartPosition = FormStartPosition.CenterParent;
        Width = 1000;
        Height = 650;

        var texto = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            Dock = DockStyle.Fill,
            ScrollBars = ScrollBars.Both,
            WordWrap = false,
            Font = new Font(FontFamily.GenericMonospace, 9f),
            Text = _relatorio.Replace("\r\n", "\n").Replace("\n", Environment.NewLine)
        };

        var caminhoAutomatico = SalvarAutomaticamente();

        var rodape = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 44,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(6)
        };
        var botaoFechar = new Button { Text = "Fechar", DialogResult = DialogResult.OK, Width = 110, Height = 28 };
        var botaoSalvar = new Button { Text = "Salvar como...", Width = 110, Height = 28 };
        botaoSalvar.Click += (_, _) => SalvarComo();
        var aviso = new Label
        {
            AutoSize = true,
            Padding = new Padding(0, 8, 12, 0),
            Text = caminhoAutomatico != null ? $"Relatório salvo em: {caminhoAutomatico}" : "Não foi possível salvar o relatório automaticamente."
        };
        rodape.Controls.Add(botaoFechar);
        rodape.Controls.Add(botaoSalvar);
        rodape.Controls.Add(aviso);

        Controls.Add(texto);
        Controls.Add(rodape);
        AcceptButton = botaoFechar;
    }

    private string? SalvarAutomaticamente()
    {
        try
        {
            var pasta = Path.Combine(AppContext.BaseDirectory, "relatorios");
            Directory.CreateDirectory(pasta);
            var caminho = Path.Combine(pasta, $"importacao_{DateTime.Now:yyyyMMdd_HHmmss}.txt");
            File.WriteAllText(caminho, _relatorio);
            return caminho;
        }
        catch
        {
            return null;
        }
    }

    private void SalvarComo()
    {
        using var dialogo = new SaveFileDialog
        {
            Filter = "Texto (*.txt)|*.txt",
            FileName = $"importacao_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
        };

        if (dialogo.ShowDialog(this) == DialogResult.OK)
            File.WriteAllText(dialogo.FileName, _relatorio);
    }
}
