using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Importador_ecbd.Aplicacao.Dtos;

namespace Apresentacao;

/// <summary>
/// Mostra, antes de importar, quantos registros cada tabela da origem tem
/// e para onde cada uma vai — inclusive as que NÃO serão importadas.
/// </summary>
public class TelaConfirmacao : Form
{
    public TelaConfirmacao(ResumoPreImportacao resumo)
    {
        Text = "Confirmar importação";
        StartPosition = FormStartPosition.CenterParent;
        Width = 760;
        Height = 560;
        MinimizeBox = false;

        var importadas = resumo.Tabelas.Where(t => t.SeraImportada).ToList();
        var naoImportadas = resumo.Tabelas.Where(t => !t.SeraImportada && t.QuantidadeRegistros > 0).ToList();

        var cabecalho = new Label
        {
            Dock = DockStyle.Top,
            Height = 48,
            Padding = new Padding(8),
            Text = $"{importadas.Count} tabela(s) serão importadas ({importadas.Where(t => t.QuantidadeRegistros > 0).Sum(t => t.QuantidadeRegistros)} registros).\n" +
                   (naoImportadas.Count > 0
                       ? $"{naoImportadas.Count} tabela(s) com dados ainda NÃO têm mapeamento e ficarão de fora (em cinza)."
                       : "Todas as tabelas com dados têm mapeamento.")
        };

        var grade = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            BackgroundColor = SystemColors.Window
        };
        grade.Columns.Add("Origem", "Tabela de origem");
        grade.Columns.Add("Registros", "Registros");
        grade.Columns.Add("Destino", "Tabela de destino");
        grade.Columns["Registros"]!.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        foreach (var t in resumo.Tabelas)
        {
            var registros = t.QuantidadeRegistros >= 0 ? t.QuantidadeRegistros.ToString("N0") : "erro";
            var destino = t.TabelaDestino ?? "— não será importada —";
            var linha = grade.Rows[grade.Rows.Add(t.NomeTabela, registros, destino)];

            if (t.Erro != null)
            {
                linha.DefaultCellStyle.ForeColor = Color.Firebrick;
                linha.Cells[1].ToolTipText = t.Erro;
            }
            else if (!t.SeraImportada)
            {
                linha.DefaultCellStyle.ForeColor = Color.Gray;
            }
        }

        var painelBotoes = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 44,
            FlowDirection = FlowDirection.RightToLeft,
            Padding = new Padding(6)
        };
        var botaoOk = new Button { Text = "Importar", DialogResult = DialogResult.OK, Width = 110, Height = 28 };
        var botaoCancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Width = 110, Height = 28 };
        painelBotoes.Controls.Add(botaoOk);
        painelBotoes.Controls.Add(botaoCancelar);

        Controls.Add(grade);
        Controls.Add(cabecalho);
        Controls.Add(painelBotoes);

        AcceptButton = botaoOk;
        CancelButton = botaoCancelar;
    }
}
