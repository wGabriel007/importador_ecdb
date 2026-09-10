using System;

namespace Dominio.Origem;

/// <summary>
/// Representa a tabela `ec_appuserroles` do banco de dados ANTIGO (origem
/// da importação). Mapeamento 1:1 da estrutura existente, sem regra de
/// negócio — gerado automaticamente a partir do dump SQL.
/// </summary>
public class EcAppuserroles
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}
