using Dominio.Destino;
using Dominio.Origem;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>Converte ec_institution (origem) em Instituicao (destino).</summary>
public static class MapeadorInstituicao
{
    public static Instituicao Mapear(EcInstitution origem, MapaDeIds ids)
    {
        return new Instituicao
        {
            IdOrigem = origem.Id, // o Id NOVO é gerado pelo banco (AUTO_INCREMENT)
            GreId = ids.TraduzirOpcional<Gre>(origem.RegionalEducationAuthorityId),
            // TODO: a origem guarda City/State/Country como texto livre
            // (não como Id numérico) — não dá pra preencher CidadeId
            // automaticamente. Precisa de um passo de correspondência
            // por nome contra a tabela Cidade antes de importar, ou
            // aceitar que essas instituições fiquem sem cidade.
            CidadeId = null,
            Nome = origem.Name ?? string.Empty,
            Documento = origem.Document ?? string.Empty,
            Endereco = origem.Address ?? string.Empty,
            Telefone = origem.SecondPhoneNumber ?? string.Empty,
            TelefoneCorporativo = origem.CorporativePhone ?? string.Empty,
            Tipologia = origem.Typology,
            TipoRede = origem.NetworkType,
            Tipo = origem.Type,
            Email = origem.ContactEmailAddress ?? string.Empty,
            TipoEscola = origem.TypeSchool,
            Idhm = origem.IDHM,
            Ideb = origem.IDEB,
            Status = origem.StatusDefault,
            CriadoEm = origem.CreatedAt,
            AtualizadoEm = origem.UpdatedAt ?? origem.CreatedAt
        };
    }
}
