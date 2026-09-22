using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_area (origem) em AreaConhecimento (destino).</summary>
public static class MapeadorAreaConhecimento
{
    public static AreaConhecimento Mapear(EcArea origem)
    {
        // ATENÇÃO: ec_area.StatusDefault veio como texto (string) no
        // dump original — provável erro de tipo no banco antigo (todas
        // as outras tabelas usam int). Convertendo com segurança aqui.
        int status = int.TryParse(origem.StatusDefault, out var s) ? s : 0;

        return new AreaConhecimento
        {
            Id = origem.Id,
            AreaPrincipalId = origem.MainAreaId,
            Nome = origem.Name,
            // TODO: origem.Type é texto livre, destino.Tipo é int (enum).
            // Precisa de uma tabela de conversão texto -> código.
            Tipo = null,
            Status = status,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
