using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_usuario` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class Usuario
{
    public int Id { get; set; }
    public int? CidadeId { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public int Genero { get; set; }
    public string? Documento { get; set; }
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public DateTime? DataNascimento { get; set; }
    public int Pontuacao { get; set; }
    public int EmailConfirmado { get; set; }
    public int Status { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}
