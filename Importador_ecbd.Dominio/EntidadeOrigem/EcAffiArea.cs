using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_affi_area` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcAffiArea
{
    public int AffiliatedTradeFairsId { get; set; }
    public int KnowledgeAreasId { get; set; }
}
