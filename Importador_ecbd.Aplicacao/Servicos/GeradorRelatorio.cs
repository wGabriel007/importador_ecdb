using System.Text;
using Importador_ecbd.Aplicacao.Dtos;
using Importador_ecbd.Dominio.Enums;

namespace Importador_ecbd.Aplicacao.Servicos;

/// <summary>
/// Transforma o <see cref="ResultadoImportacao"/> em um relatório de texto
/// legível (mostrado na tela de resultado e salvo em arquivo .txt).
/// </summary>
public static class GeradorRelatorio
{
    public static string Gerar(ResultadoImportacao r)
    {
        var sb = new StringBuilder();
        var duracao = r.Fim > r.Inicio ? r.Fim - r.Inicio : TimeSpan.Zero;

        sb.AppendLine("RELATÓRIO DE IMPORTAÇÃO ECDB");
        sb.AppendLine($"Início: {r.Inicio:dd/MM/yyyy HH:mm:ss}   Fim: {r.Fim:dd/MM/yyyy HH:mm:ss}   Duração: {duracao:hh\\:mm\\:ss}");
        sb.AppendLine(new string('=', 90));
        sb.AppendLine($"Registros importados ........ {r.TotalRegistrosImportados}");
        sb.AppendLine($"Já existiam (pulados) ....... {r.TotalRegistrosJaExistentes}");
        sb.AppendLine($"Registros com erro .......... {r.RegistrosComErro.Count}");
        sb.AppendLine($"Tabelas não importadas ...... {r.TabelasNaoImportadas.Count}");
        sb.AppendLine();

        sb.AppendLine("POR TABELA");
        sb.AppendLine($"{"Origem",-28} {"Destino",-28} {"Lidos",8} {"Import.",8} {"Já exist.",9} {"Erros",7}");
        sb.AppendLine(new string('-', 90));
        foreach (var t in r.Tabelas)
            sb.AppendLine($"{t.TabelaOrigem,-28} {t.TabelaDestino,-28} {t.Lidos,8} {t.Importados,8} {t.JaExistentes,9} {t.ComErro,7}");
        sb.AppendLine();

        if (r.Avisos.Count > 0)
        {
            sb.AppendLine("AVISOS");
            sb.AppendLine(new string('-', 90));
            foreach (var a in r.Avisos)
                sb.AppendLine($"• {a}");
            sb.AppendLine();
        }

        if (r.TabelasNaoImportadas.Count > 0)
        {
            sb.AppendLine("TABELAS NÃO IMPORTADAS");
            sb.AppendLine(new string('-', 90));
            foreach (var t in r.TabelasNaoImportadas.OrderBy(t => t.Motivo))
                sb.AppendLine($"[{Descrever(t.Motivo)}] {t.NomeTabela}: {t.Descricao}");
            sb.AppendLine();
        }

        if (r.RegistrosComErro.Count > 0)
        {
            sb.AppendLine("REGISTROS COM ERRO (agrupados por tabela e motivo)");
            sb.AppendLine(new string('-', 90));
            foreach (var grupo in r.RegistrosComErro.GroupBy(e => (e.NomeTabela, e.Motivo)))
            {
                sb.AppendLine($"{grupo.Key.NomeTabela} — {Descrever(grupo.Key.Motivo)} — {grupo.Count()} registro(s)");
                foreach (var e in grupo)
                    sb.AppendLine($"    {e.IdentificadorRegistro}: {e.Descricao}");
                sb.AppendLine();
            }
        }

        if (r.TabelasVazias.Count > 0)
            sb.AppendLine($"Tabelas vazias na origem: {string.Join(", ", r.TabelasVazias)}");

        return sb.ToString();
    }

    public static string Descrever(EnumMotivoFalha motivo) => motivo switch
    {
        EnumMotivoFalha.ViolacaoDeChaveEstrangeira => "Chave estrangeira inválida",
        EnumMotivoFalha.ChaveDuplicada             => "Chave duplicada",
        EnumMotivoFalha.ValorObrigatorioAusente    => "Valor obrigatório ausente",
        EnumMotivoFalha.TipoDeDadoIncompativel     => "Tipo/tamanho de dado incompatível",
        EnumMotivoFalha.ColunaNaoExisteNoDestino   => "Coluna não existe no destino",
        EnumMotivoFalha.TabelaNaoExisteNoDestino   => "Tabela não existe no destino",
        EnumMotivoFalha.FalhaNaLeituraDaOrigem     => "Falha ao ler a origem",
        EnumMotivoFalha.TabelaSemMapeamento        => "Sem mapeamento",
        EnumMotivoFalha.ErroExecucao               => "Erro de execução",
        _                                          => "Erro desconhecido"
    };
}
