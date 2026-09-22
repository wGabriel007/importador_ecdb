using System.Globalization;
using Importador_ecbd.Aplicacao.Excecoes;
using Importador_ecbd.Aplicacao.Interfaces;
using Importador_ecbd.Dominio.Enums;
using Infraestrutura.Contextos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MySqlConnector;

namespace Infraestrutura.Repositorios;

/// <summary>
/// Implementação de escrita no banco de dados NOVO. Genérica: usa os
/// metadados do EF Core (ContextoDestino) para descobrir tabela e chave
/// de qualquer entidade.
/// </summary>
public class RepositorioDestino : IRepositorioDestino
{
    private readonly ContextoDestino _contexto;

    public RepositorioDestino(ContextoDestino contexto)
    {
        _contexto = contexto;
    }

    public string ObterNomeTabela<T>() where T : class
        => ObterTipoEntidade<T>().GetTableName() ?? typeof(T).Name;

    public string ObterChave<T>(T entidade) where T : class
    {
        var chave = ObterTipoEntidade<T>().FindPrimaryKey()!;
        return MontarChave(chave.Properties.Select(p => p.PropertyInfo!.GetValue(entidade)));
    }

    public async Task<HashSet<string>> ObterChavesExistentesAsync<T>() where T : class
    {
        var tipo = ObterTipoEntidade<T>();
        var tabela = tipo.GetTableName()!;
        var colunas = tipo.FindPrimaryKey()!.Properties
            .Select(p => $"`{p.GetColumnName(StoreObjectIdentifier.Table(tabela, tipo.GetSchema()))}`")
            .ToList();

        var chaves = new HashSet<string>();
        var conexao = _contexto.Database.GetDbConnection();
        var abriuAqui = conexao.State != System.Data.ConnectionState.Open;

        try
        {
            if (abriuAqui)
                await conexao.OpenAsync();

            await using var comando = conexao.CreateCommand();
            // Busca só as colunas da chave (leve, mesmo em tabelas grandes)
            comando.CommandText = $"SELECT {string.Join(", ", colunas)} FROM `{tabela}`";
            comando.CommandTimeout = 300;

            await using var leitor = await comando.ExecuteReaderAsync();
            while (await leitor.ReadAsync())
            {
                var valores = new object?[colunas.Count];
                for (int i = 0; i < colunas.Count; i++)
                    valores[i] = leitor.IsDBNull(i) ? null : leitor.GetValue(i);

                chaves.Add(MontarChave(valores));
            }
        }
        finally
        {
            if (abriuAqui)
                await conexao.CloseAsync();
        }

        return chaves;
    }

    public void Adicionar<T>(T entidade) where T : class
    {
        _contexto.Set<T>().Add(entidade);
    }

    public async Task<int> SalvarAlteracoesAsync()
    {
        try
        {
            return await _contexto.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            var erroMySql = EncontrarErroMySql(ex);
            var mensagem = erroMySql?.Message ?? ex.InnerException?.Message ?? ex.Message;
            throw new ErroAoSalvarException(mensagem, ClassificarErro(erroMySql), ex);
        }
        finally
        {
            // CORREÇÃO PRINCIPAL: sem isso, uma entidade que falhou continua no
            // ChangeTracker como "Added" e é reenviada em TODOS os SaveChanges
            // seguintes, fazendo todos os registros depois dela falharem também.
            // Limpar após sucesso também mantém a memória baixa em tabelas grandes.
            _contexto.ChangeTracker.Clear();
        }
    }

    // ------------------------------------------------------------------

    private IEntityType ObterTipoEntidade<T>() where T : class
        => _contexto.Model.FindEntityType(typeof(T))
           ?? throw new InvalidOperationException($"A entidade {typeof(T).Name} não está mapeada no ContextoDestino.");

    private static string MontarChave(IEnumerable<object?> valores)
        => string.Join("|", valores.Select(v => Convert.ToString(v, CultureInfo.InvariantCulture) ?? "<null>"));

    private static MySqlException? EncontrarErroMySql(Exception ex)
    {
        for (Exception? atual = ex; atual != null; atual = atual.InnerException)
        {
            if (atual is MySqlException mysql)
                return mysql;
        }
        return null;
    }

    /// <summary>
    /// Traduz o código de erro do MySQL para o motivo mostrado no relatório.
    /// Lista de códigos: https://dev.mysql.com/doc/mysql-errors/8.0/en/server-error-reference.html
    /// </summary>
    private static EnumMotivoFalha ClassificarErro(MySqlException? erro)
    {
        return erro?.Number switch
        {
            1452 or 1451 or 1216 or 1217 => EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
            1062 or 1586                 => EnumMotivoFalha.ChaveDuplicada,
            1048 or 1364                 => EnumMotivoFalha.ValorObrigatorioAusente,
            1264 or 1265 or 1292 or 1366 or 1406 => EnumMotivoFalha.TipoDeDadoIncompativel,
            1054                         => EnumMotivoFalha.ColunaNaoExisteNoDestino,
            1146                         => EnumMotivoFalha.TabelaNaoExisteNoDestino,
            _                            => EnumMotivoFalha.ErroDesconhecido
        };
    }
}
