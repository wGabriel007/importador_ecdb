using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// Converte ec_app_user (origem) em Usuario (destino).
/// (Substitui a versão anterior deste mapeador — atualizado para bater
/// com os campos reais de Usuario, que usa Status/CriadoEm em vez de
/// Ativo/DataCriacao.)
/// </summary>
public static class MapeadorUsuario
{
    public static Usuario Mapear(EcAppUser origem)
    {
        return new Usuario
        {
            Id = origem.Id,
            CidadeId = origem.CityId ?? origem.IdCity, 
            NomeCompleto = origem.Fullname,
            Genero = origem.GenderIdentify,
            Documento = origem.Document,
            Email = origem.Email ?? string.Empty,
            SenhaHash = origem.PasswordHash ?? string.Empty,
            Telefone = origem.PhoneNumber,
            DataNascimento = origem.BirthDay,
            Pontuacao = origem.Score,
            EmailConfirmado = origem.EmailConfirmed ? 1 : 0,
            Status = origem.Status,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
