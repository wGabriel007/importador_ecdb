namespace Importador_ecbd.Dominio.Enums;

/// <summary>
/// Enum que representa o motivo da falha no formulario.
/// </summary>
public enum EnumMotivoFalha
{
    /// <summary>
    /// Se caso a tabela não existir no Banco.
    /// </summary>
    TabelaNaoExisteNoDestino,   

    /// <summary>
    /// Se caso a coluna não existir no Banco.
    /// </summary>
    ColunaNaoExisteNoDestino,  
    
    /// <summary>
    /// Caso algum valor obrigatório esteja ausente no Banco.
    /// </summary>
    ValorObrigatorioAusente,

    /// <summary>
    /// Caso algum valor tenha violação de chave estrangeira.
    /// </summary>
    ViolacaoDeChaveEstrangeira,

    /// <summary>
    /// Caso o tipo do Dado for incompativel no Banco.
    /// </summary>
    TipoDeDadoIncompativel,

    /// <summary>
    /// Caso algum erro desconhecido.
    /// </summary>
    ErroDesconhecido,

    /// <summary>
    /// Caso algum erro de execução
    /// </summary>
    ErroExecucao
}
