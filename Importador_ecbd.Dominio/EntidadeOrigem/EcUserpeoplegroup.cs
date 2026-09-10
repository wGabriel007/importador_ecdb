using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_userpeoplegroup` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcUserpeoplegroup
{
    public int Id { get; set; }
    public int PeopleGroupId { get; set; }
    public int ApplicationUserId { get; set; }
    public int CheckedStatus { get; set; }
}
