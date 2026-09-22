using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_institution (origem) em Instituicao (destino).</summary>
public static class MapeadorInstituicao
{
    public static Instituicao Mapear(EcInstitution origem)
    {
        return new Instituicao
        {
            Id = origem.Id,
            GreId = origem.RegionalEducationAuthorityId,
            // TODO: a origem guarda City/State/Country como texto livre
            // (não como Id numérico) — não dá pra preencher CidadeId
            // automaticamente. Precisa de um passo de correspondência
            // por nome contra a tabela Cidade antes de importar, ou
            // aceitar que essas instituições fiquem sem cidade.
            CidadeId = null,
            Nome = origem.Name,
            Documento = origem.Document,
            Endereco = origem.Address,
            Telefone = origem.SecondPhoneNumber ?? string.Empty,
            TelefoneCorporativo = origem.CorporativePhone,
            Tipologia = origem.Typology,
            TipoRede = origem.NetworkType,
            Tipo = origem.Type,
            Email = origem.ContactEmailAddress,
            TipoEscola = origem.TypeSchool,
            Idhm = origem.IDHM,
            Ideb = origem.IDEB,
            Status = origem.StatusDefault,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
