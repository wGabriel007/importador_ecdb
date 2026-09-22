namespace Dominio.Destino;

/// <summary>
/// Entidade do banco NOVO que tem Id próprio (gerado pelo AUTO_INCREMENT do destino)
/// e guarda o Id que o registro tinha no banco ANTIGO na coluna <c>iIdOrigem</c>.
///
/// Tabelas associativas (FeiraArea, FuncaoPermissao, PermissaoUsuario) não
/// implementam: a chave delas é composta pelos Ids novos das tabelas que ligam.
/// </summary>
public interface IEntidadeComIdOrigem
{
    /// <summary>Id NOVO, gerado pelo banco de destino.</summary>
    int Id { get; set; }

    /// <summary>Id que o registro tinha no banco de origem (coluna iIdOrigem).</summary>
    int? IdOrigem { get; set; }
}
