using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_estado` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class Estado : IEntidadeComIdOrigem
{
    /// <summary>Id NOVO (AUTO_INCREMENT do destino).</summary>
    public int Id { get; set; }

    /// <summary>Id do registro no banco de origem (coluna iIdOrigem).</summary>
    public int? IdOrigem { get; set; }

    public int PaisId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Sigla { get; set; } = string.Empty;
    public int Status { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}
