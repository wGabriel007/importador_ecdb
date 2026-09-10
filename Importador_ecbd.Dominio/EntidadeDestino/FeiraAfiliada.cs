using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_feira_afiliada` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class FeiraAfiliada
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public int? CidadeId { get; set; }
    public int? InstituicaoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? Endereco { get; set; }
    public int? Alcance { get; set; }
    public int? EscolasParticipantes { get; set; }
    public int? AvaliadoNaFeira { get; set; }
    public DateTime? DataInicioRealizacao { get; set; }
    public DateTime? DataRealizacao { get; set; }
    public DateTime? DataFinalRealizacao { get; set; }
    public int? Periodo { get; set; }
    public int? QuantidadeProjetos { get; set; }
    public string? DescricaoProjeto { get; set; }
    public string? DescricaoProcessoSelecao { get; set; }
    public int? TipoParticipacaoEstudante { get; set; }
    public int? JaParticipou { get; set; }
    public string? DescricaoParticipacaoAno { get; set; }
    public string? GrauEstudante { get; set; }
    public int Status { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}
