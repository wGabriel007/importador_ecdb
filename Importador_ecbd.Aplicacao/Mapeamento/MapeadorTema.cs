using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_theme (origem) em Tema (destino).</summary>
public static class MapeadorTema
{
    public static Tema Mapear(EcTheme origem)
    {
        return new Tema
        {
            Id = origem.Id,
            Nome = origem.Name ?? string.Empty,
            Status = origem.StatusDefault ?? 0,
            CriadoEm = origem.CreatedAt ?? DateTime.UtcNow,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt ?? DateTime.UtcNow
        };
    }
}
