using Importador_ecbd.Dominio.Enums;

namespace Importador_ecbd.Aplicacao.Excecoes;

/// <summary>
/// O registro aponta (pela chave estrangeira) para um registro pai que não
/// existe no destino — ou nunca existiu na origem, ou falhou ao ser importado —
/// ou a chave veio vazia numa coluna obrigatória.
/// </summary>
public class ReferenciaNaoImportadaException : Exception
{
    public string Entidade { get; }
    public int? IdOrigem { get; }

    public EnumMotivoFalha Motivo => IdOrigem.HasValue
        ? EnumMotivoFalha.ViolacaoDeChaveEstrangeira
        : EnumMotivoFalha.ValorObrigatorioAusente;

    public ReferenciaNaoImportadaException(string entidade, int? idOrigem)
        : base(idOrigem.HasValue
            ? $"Referência para {entidade} com Id de origem {idOrigem}, que não foi importado(a) — não existe na origem ou falhou na importação."
            : $"Referência obrigatória para {entidade} veio vazia (NULL) na origem.")
    {
        Entidade = entidade;
        IdOrigem = idOrigem;
    }
}
