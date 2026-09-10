using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_edital_feira` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EditalFeira
{
    public int Id { get; set; }
    public int FeiraAfiliadaId { get; set; }
    public int? ConfirmacaoStatus { get; set; }
    public string? EdicaoParticipacao { get; set; }
    public int Status { get; set; }
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
}
