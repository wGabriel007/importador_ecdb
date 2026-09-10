using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_funcao_permissao` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class FuncaoPermissao
{
    public int AppPermissaoId { get; set; }
    public int PermissaoId { get; set; }
}
