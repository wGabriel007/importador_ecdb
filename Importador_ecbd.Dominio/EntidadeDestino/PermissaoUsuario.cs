using System;

namespace Dominio.Destino;

/// <summary>
/// Representa a tabela `ec_tb_permissao_usuario` do banco de dados NOVO (destino
/// da importação). Propriedades em PascalCase limpo (sem o prefixo húngaro
/// original) — gerado automaticamente a partir do dump SQL.
/// </summary>
public class PermissaoUsuario
{
    public int UsuarioId { get; set; }
    public int AppPermissao { get; set; }
}
