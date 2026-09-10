using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_permissao` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class Permissao
{
    public int Id { get; set; }
    public int? PermissaoPaiId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Icone { get; set; } = string.Empty;
    public string Acao { get; set; } = string.Empty;
    public string NomeControlador { get; set; } = string.Empty;
    public int Status { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}
