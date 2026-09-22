using System;
using System.Windows.Forms;

namespace Apresentacao
{
    public class TelaResultado : Form
    {
        private readonly object _resultado;

        public TelaResultado(object resultado)
        {
            _resultado = resultado;
            Text = "Resultado da Importação";
            Width = 600;
            Height = 400;

            var txt = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical
            };

            txt.Text = BuildText(resultado);
            Controls.Add(txt);
        }

        private string BuildText(object resultado)
        {
            if (resultado == null) return "Nenhum resultado.";
            try
            {
                return System.Text.Json.JsonSerializer.Serialize(resultado, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            }
            catch
            {
                return resultado.ToString();
            }
        }
    }
}
