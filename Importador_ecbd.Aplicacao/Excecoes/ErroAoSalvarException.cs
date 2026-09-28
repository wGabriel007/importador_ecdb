using Importador_ecbd.Dominio.Enums;

namespace Importador_ecbd.Aplicacao.Excecoes;

/// <summary>
/// Lançada quando o banco de destino recusa a gravação de um registro.
/// Carrega o motivo já classificado (FK, duplicado, obrigatório, tipo...)
/// para o relatório ficar preciso.
/// </summary>
public class ErroAoSalvarException : Exception
{
    public EnumMotivoFalha Motivo { get; }

    public ErroAoSalvarException(string mensagem, Exception? inner = null)
        : this(mensagem, EnumMotivoFalha.ErroDesconhecido, inner) { }

    public ErroAoSalvarException(string mensagem, EnumMotivoFalha motivo, Exception? inner = null)
        : base(mensagem, inner)
    {
        Motivo = motivo;
    }
}
