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

    private const string ColunaIdOrigem = "iIdOrigem";

    public async Task<string?> PrepararColunaIdOrigemAsync<T>() where T : class
    {
        var tipo = ObterTipoEntidade<T>();
        var tabela = tipo.GetTableName()!;
        var colunaId = tipo.FindPrimaryKey()!.Properties.Single()
            .GetColumnName(StoreObjectIdentifier.Table(tabela, tipo.GetSchema()))!;

        // 1. O Id novo depende do AUTO_INCREMENT: sem ele o INSERT falharia (ou gravaria 0)
        var extra = await ExecutarEscalarAsync(
            "SELECT EXTRA FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @tabela AND COLUMN_NAME = @coluna",
            ("@tabela", tabela), ("@coluna", colunaId));

        if (extra == null)
            throw new InvalidOperationException($"A tabela `{tabela}` (ou a coluna `{colunaId}`) não existe no banco de destino.");

        if (!extra.ToString()!.Contains("auto_increment", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException(
                $"A coluna `{tabela}`.`{colunaId}` não é AUTO_INCREMENT, então o banco não consegue gerar os Ids novos. " +
                $"Ajuste com: ALTER TABLE `{tabela}` MODIFY `{colunaId}` INT NOT NULL AUTO_INCREMENT;");

        // 2. Coluna iIdOrigem
        var existe = Convert.ToInt32(await ExecutarEscalarAsync(
            "SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @tabela AND COLUMN_NAME = @coluna",
            ("@tabela", tabela), ("@coluna", ColunaIdOrigem)));

        if (existe > 0)
            return null;

        var linhasAntes = Convert.ToInt32(await ExecutarEscalarAsync($"SELECT COUNT(*) FROM `{tabela}`"));

        await _contexto.Database.ExecuteSqlRawAsync(
            $"ALTER TABLE `{tabela}` ADD COLUMN `{ColunaIdOrigem}` INT NULL COMMENT 'Id do registro no banco de origem', " +
            $"ADD INDEX `ix_{tabela}_{ColunaIdOrigem}` (`{ColunaIdOrigem}`)");

        var aviso = $"Coluna `{ColunaIdOrigem}` criada em `{tabela}`.";
        if (linhasAntes > 0)
            aviso += $" ATENÇÃO: a tabela já tinha {linhasAntes} linha(s) sem Id de origem — se vieram de uma importação " +
                     "anterior, elas serão importadas de novo (duplicadas). Limpe a tabela antes, se for o caso.";
        return aviso;
    }

    public async Task<Dictionary<int, int>> ObterMapaIdsAsync<T>() where T : class
    {
        var tipo = ObterTipoEntidade<T>();
        var tabela = tipo.GetTableName()!;
        var colunaId = tipo.FindPrimaryKey()!.Properties.Single()
            .GetColumnName(StoreObjectIdentifier.Table(tabela, tipo.GetSchema()))!;

        var mapa = new Dictionary<int, int>();
        var conexao = _contexto.Database.GetDbConnection();
        var abriuAqui = conexao.State != System.Data.ConnectionState.Open;
        try
        {
            if (abriuAqui)
                await conexao.OpenAsync();

            await using var comando = conexao.CreateCommand();
            comando.CommandText = $"SELECT `{ColunaIdOrigem}`, `{colunaId}` FROM `{tabela}` WHERE `{ColunaIdOrigem}` IS NOT NULL ORDER BY `{colunaId}`";
            comando.CommandTimeout = 300;

            await using var leitor = await comando.ExecuteReaderAsync();
            while (await leitor.ReadAsync())
                mapa.TryAdd(Convert.ToInt32(leitor.GetValue(0)), Convert.ToInt32(leitor.GetValue(1)));
        }
        finally
        {
            if (abriuAqui)
                await conexao.CloseAsync();
        }
        return mapa;
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

    private async Task<object?> ExecutarEscalarAsync(string sql, params (string Nome, object Valor)[] parametros)
    {
        var conexao = _contexto.Database.GetDbConnection();
        var abriuAqui = conexao.State != System.Data.ConnectionState.Open;
        try
        {
            if (abriuAqui)
                await conexao.OpenAsync();

            await using var comando = conexao.CreateCommand();
            comando.CommandText = sql;
            foreach (var (nome, valor) in parametros)
            {
                var p = comando.CreateParameter();
                p.ParameterName = nome;
                p.Value = valor;
                comando.Parameters.Add(p);
            }

            var resultado = await comando.ExecuteScalarAsync();
            return resultado is DBNull ? null : resultado;
        }
        finally
        {
            if (abriuAqui)
                await conexao.CloseAsync();
        }
    }

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
