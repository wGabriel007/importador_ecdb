using Dominio.Destino;
using Dominio.Origem;


namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte o registro da tabela de origem para a tabela de destino.
/// </summary>
public static class MapeadorUsuario
{
    public static Usuario Mapear (EcAppUser origem)
    {
        return new Usuario
        {
            CidadeId = origem.CityId,
            NomeCompleto = origem.Fullname,
            Documento = origem.Document,
            Email = origem.Email ?? string.Empty,
            SenhaHash = origem.PasswordHash ?? string.Empty,
            Telefone = origem.PhoneNumber,
            Pontuacao = origem.Score,
            Status = origem.Status, 
            CriadoEm = origem.CreatedAt
        };
    }
}
