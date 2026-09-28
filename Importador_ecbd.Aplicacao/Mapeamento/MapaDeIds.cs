using Importador_ecbd.Aplicacao.Excecoes;

namespace Importador_ecbd.Aplicacao.Mapeamento;

/// <summary>
/// "De-para" em memória: Id antigo (origem) → Id novo (destino), por entidade.
///
/// É preenchido de dois jeitos:
///   • no início de cada tabela, com o que já está no destino (colunas iId / iIdOrigem);
///   • a cada registro gravado, com o Id que o AUTO_INCREMENT acabou de gerar.
///
/// Os mapeadores usam <see cref="Traduzir{T}(int)"/> para trocar as chaves
/// estrangeiras: ex.: Cidade.EstadoId = ids.Traduzir&lt;Estado&gt;(origem.IdState).
/// </summary>
public class MapaDeIds
{
    private readonly Dictionary<Type, Dictionary<int, int>> _mapas = new();

    private Dictionary<int, int> MapaDe<T>()
    {
        if (!_mapas.TryGetValue(typeof(T), out var mapa))
        {
            mapa = new Dictionary<int, int>();
            _mapas[typeof(T)] = mapa;
        }
        return mapa;
    }

    /// <summary>Carrega pares (Id origem → Id destino) que já existem no banco de destino.</summary>
    public void Carregar<T>(IReadOnlyDictionary<int, int> origemParaDestino)
    {
        var mapa = MapaDe<T>();
        foreach (var (idOrigem, idDestino) in origemParaDestino)
            mapa[idOrigem] = idDestino;
    }

    public void Registrar<T>(int idOrigem, int idDestino) => MapaDe<T>()[idOrigem] = idDestino;

    public bool Contem<T>(int idOrigem) => MapaDe<T>().ContainsKey(idOrigem);

    /// <summary>Traduz uma chave estrangeira obrigatória. Lança se o registro pai não foi importado.</summary>
    public int Traduzir<T>(int idOrigem)
    {
        if (MapaDe<T>().TryGetValue(idOrigem, out var idDestino))
            return idDestino;

        throw new ReferenciaNaoImportadaException(typeof(T).Name, idOrigem);
    }

    /// <summary>
    /// Traduz uma chave estrangeira obrigatória no destino que pode vir nula da origem.
    /// Nulo vira erro "valor obrigatório ausente" para aquele registro.
    /// </summary>
    public int Traduzir<T>(int? idOrigem)
    {
        if (!idOrigem.HasValue)
            throw new ReferenciaNaoImportadaException(typeof(T).Name, null);
        return Traduzir<T>(idOrigem.Value);
    }

    /// <summary>Traduz uma chave estrangeira opcional: null continua null.</summary>
    public int? TraduzirOpcional<T>(int? idOrigem)
        => idOrigem.HasValue ? Traduzir<T>(idOrigem.Value) : null;
}
