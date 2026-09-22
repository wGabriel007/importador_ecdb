namespace Importador_ecbd.Aplicacao.Excecoes;

/// <summary>
/// Erro ao tentar salvar um registro no banco de destino.
/// Trata falhas de persistencia sem precisar enxergar o EF Core.
/// </summary>
public class ErroAoSalvarException : Exception
{
    public ErroAoSalvarException(string mensagem, Exception? erroOriginal = null)
        : base(mensagem, erroOriginal) { }
}
