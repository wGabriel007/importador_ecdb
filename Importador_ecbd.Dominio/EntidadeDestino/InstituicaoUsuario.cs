using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_instituicao_usuario` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class InstituicaoUsuario
{
    public int Id { get; set; }
    public int? InstituicaoId { get; set; }
    public int? UsuarioId { get; set; }
    public string? Referencia { get; set; }
    public int? Tipo { get; set; }
    public int? NivelAcademico { get; set; }
    public int? JaParticipouCj { get; set; }
    public string? AnoParticipacaoCj { get; set; }
    public int? PossueExperienciaEmFeiras { get; set; }
    public string? ExperienciaDeFeiras { get; set; }
}
