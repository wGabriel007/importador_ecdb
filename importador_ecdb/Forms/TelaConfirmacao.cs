using System.Windows.Forms;
using Importador_ecbd.Aplicacao.Dtos;

namespace Apresentacao;

public class TelaConfirmacao : Form
{
    private readonly ResumoPreImportacao _resumo;

    public TelaConfirmacao(ResumoPreImportacao resumo)
    {
        _resumo = resumo;
        Text = "Confirmar importação";

        // UI mínima para compilação: botão OK e Cancel
        var botaoOk = new Button { Text = "OK", DialogResult = DialogResult.OK, Left = 10, Top = 10 };
        var botaoCancelar = new Button { Text = "Cancelar", DialogResult = DialogResult.Cancel, Left = 100, Top = 10 };

        Controls.Add(botaoOk);
        Controls.Add(botaoCancelar);

        AcceptButton = botaoOk;
        CancelButton = botaoCancelar;
        StartPosition = FormStartPosition.CenterParent;
        Width = 300;
        Height = 200;
    }
}
