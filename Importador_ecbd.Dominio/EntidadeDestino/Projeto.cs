using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_projeto` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class Projeto
{
    public int Id { get; set; }
    public int CategoriaId { get; set; }
    public int InstituicaoId { get; set; }
    public int FeiraAfiliadaId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Introducao { get; set; } = string.Empty;
    public string Objetivo { get; set; } = string.Empty;
    public string Metodologia { get; set; } = string.Empty;
    public string Resultado { get; set; } = string.Empty;
    public int? Status { get; set; }
    public int? TipoApresentacao { get; set; }
    public string? DescricaoCancelamento { get; set; }
    public int? EducacaoEspecial { get; set; }
    public string? PalavraChave { get; set; }
    public int? PreInscricao { get; set; }
    public string? Bibliografia { get; set; }
    public string? VideoUrl { get; set; }
    public string? LinkCarta { get; set; }
    public int? StatusConfirmacao { get; set; }
    public int? IdeaiaTec { get; set; }
    public string? Sumario { get; set; }
    public int? Tema { get; set; }
    public int? TemaProjeto { get; set; }
}
