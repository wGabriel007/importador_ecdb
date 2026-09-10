using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_gre` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class Gre
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Status { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}
