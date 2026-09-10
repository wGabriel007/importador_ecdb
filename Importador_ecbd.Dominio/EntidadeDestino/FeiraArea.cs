using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_feira_area` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class FeiraArea
{
    public int FeiraAfiliadaId { get; set; }
    public int AreaConhecimentoId { get; set; }
}
