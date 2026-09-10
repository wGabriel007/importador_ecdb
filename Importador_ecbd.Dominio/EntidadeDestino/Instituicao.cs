using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_instituicao` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class Instituicao
{
    public int Id { get; set; }
    public int? GreId { get; set; }
    public int? CidadeId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Documento { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string TelefoneCorporativo { get; set; } = string.Empty;
    public int? Tipologia { get; set; }
    public int? TipoRede { get; set; }
    public int? Tipo { get; set; }
    public string Email { get; set; } = string.Empty;
    public int? TipoEscola { get; set; }
    public decimal? Idhm { get; set; }
    public decimal? Ideb { get; set; }
    public int Status { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}
