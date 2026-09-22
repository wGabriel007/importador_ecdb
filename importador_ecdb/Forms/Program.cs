using Importador_ecbd.Aplicacao.Interfaces;
using Importador_ecbd.Aplicacao.Servicos;
using Infraestrutura.Contextos;
using Infraestrutura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Apresentacao;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Tenta localizar appsettings.json no diretório de saída e em diretórios ascendentes
        var baseDir = AppContext.BaseDirectory;
        var configFilePath = FindAppSettings(baseDir);

        IConfigurationRoot configuracao;

        if (configFilePath != null)
        {
            configuracao = new ConfigurationBuilder()
                .AddJsonFile(configFilePath, optional: false, reloadOnChange: true)
                .Build();
        }
        else
        {
            // Não encontrou arquivo; carrega configuração vazia para permitir fallback por variáveis de ambiente
            configuracao = new ConfigurationBuilder().Build();
        }

        // Valida se as connection strings estão presentes; falha rápida com mensagem legível
        var connOrigem = configuracao.GetConnectionString("BancoOrigem");
        var connDestino = configuracao.GetConnectionString("BancoDestino");

        if (string.IsNullOrWhiteSpace(connOrigem) || string.IsNullOrWhiteSpace(connDestino))
        {
            var msg = configFilePath == null
                ? "Arquivo de configuração 'appsettings.json' não encontrado no diretório de saída nem em diretórios ascendentes.\nInclua o arquivo no diretório de saída (Copy to Output Directory) ou configure as connection strings via variáveis de ambiente." 
                : "Arquivo de configuração encontrado, mas faltando 'BancoOrigem'/'BancoDestino'.\nVerifique as connection strings no appsettings.json.";

            MessageBox.Show(msg, "Configuração ausente", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var servicos = new ServiceCollection();

        // Determina a versão do servidor: primeiro tenta ler configuração, depois
        // tenta detectar rapidamente (com timeout curto) e por fim aplica um fallback.
        ServerVersion GetServerVersion(string connectionString)
        {
            // Preferência por configuração explícita (ex: "8.0.32" ou "8.0")
            var verCfg = configuracao["MySql:ServerVersion"] ?? configuracao["ServerVersion"];
            if (!string.IsNullOrWhiteSpace(verCfg) && Version.TryParse(verCfg, out var parsedVer))
            {
                return new MySqlServerVersion(parsedVer);
            }

            try
            {
                // Tenta detectar rápido usando timeout reduzido para evitar longos bloqueios na inicialização
                var builder = new MySqlConnector.MySqlConnectionStringBuilder(connectionString)
                {
                    ConnectionTimeout = 10 // segundos
                };

                return ServerVersion.AutoDetect(builder.ConnectionString);
            }
            catch (Exception exShort)
            {
                // Se a detecção rápida falhar, tenta uma segunda vez com timeout maior antes de aplicar fallback
                try
                {
                    var builder2 = new MySqlConnector.MySqlConnectionStringBuilder(connectionString)
                    {
                        ConnectionTimeout = 30 // segundos
                    };

                    return ServerVersion.AutoDetect(builder2.ConnectionString);
                }
                catch (Exception exLong)
                {
                    // Fallback permissivo para evitar falha na inicialização; registra/avisa o usuário.
                    var fallback = new MySqlServerVersion(new Version(8, 0, 32));
                    MessageBox.Show($"Não foi possível detectar a versão do servidor MySQL (detecções curta e longa falharam).\nUsando versão padrão {fallback.Version}.\nErro curto: {exShort.Message}\nErro longo: {exLong.Message}\nRecomenda-se informar 'MySql:ServerVersion' no appsettings.json ou corrigir a conexão.", "Detecção de versão", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return fallback;
                }
            }
        }

        var servidorVersao = GetServerVersion(connOrigem);

        servicos.AddDbContext<ContextoOrigem>(opcoes =>
            opcoes.UseMySql(
                connOrigem,
                servidorVersao,
                mySqlOptions => mySqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));

        // Se origem e destino usam servidores diferentes, tenta detectar para o destino também;
        // caso contrário reutiliza a versão já obtida.
        var servidorVersaoDestino = servidorVersao;
        if (!string.Equals(connOrigem, connDestino, StringComparison.Ordinal))
        {
            servidorVersaoDestino = GetServerVersion(connDestino);
        }

        servicos.AddDbContext<ContextoDestino>(opcoes =>
            opcoes.UseMySql(
                connDestino,
                servidorVersaoDestino,
                mySqlOptions => mySqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));

        // Logging mínimo para capturar eventos do EF Core e diagnósticos de conexão
        // Não exige providers adicionais aqui; apenas define o nível mínimo.
        servicos.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Information));

        servicos.AddScoped<IRepositorioOrigem, RepositorioOrigem>();
        servicos.AddScoped<IRepositorioDestino, RepositorioDestino>();
        servicos.AddScoped<IServicoImportacao, ServicoImportacao>();

        servicos.AddTransient<TelaPrincipal>();

        var provedor = servicos.BuildServiceProvider();

        Application.Run(provedor.GetRequiredService<TelaPrincipal>());
    }

    private static string FindAppSettings(string startDir)
    {
        try
        {
            var dir = new DirectoryInfo(startDir);
            for (int i = 0; i < 8 && dir != null; i++)
            {
                var candidate = Path.Combine(dir.FullName, "appsettings.json");
                if (File.Exists(candidate))
                    return candidate;
                dir = dir.Parent;
            }

            // Tenta o diretório da assembly (caso diferente)
            var asmDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (!string.IsNullOrEmpty(asmDir))
            {
                var candidate = Path.Combine(asmDir, "appsettings.json");
                if (File.Exists(candidate))
                    return candidate;
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}