using Importador_ecbd.Aplicacao.Interfaces;
using Dominio.Destino;
using Importador_ecbd.Aplicacao.Excecoes;
using Infraestrutura.Contextos;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.Repositorios;

/// <summary>
/// Implementação de escrita no banco de dados NOVO. Cada método
/// "Adicionar" só marca o registro para inserção (AddAsync) — a
/// gravação de verdade só acontece em SalvarAlteracoesAsync.
/// </summary>
public class RepositorioDestino : IRepositorioDestino
{
    private readonly ContextoDestino _contexto;

    public RepositorioDestino(ContextoDestino contexto)
    {
        _contexto = contexto;
    }

    public async Task AdicionarPaisAsync(Pais pais)
    {
        await _contexto.Pais.AddAsync(pais);
    }

    public async Task AdicionarEstadoAsync(Estado estado)
    {
        await _contexto.Estados.AddAsync(estado);
    }

    public async Task AdicionarCidadeAsync(Cidade cidade)
    {
        await _contexto.Cidades.AddAsync(cidade);
    }

    public async Task AdicionarPapelAsync(Papel papel)
    {
        await _contexto.Papels.AddAsync(papel);
    }

    public async Task AdicionarUsuarioAsync(Usuario usuario)
    {
        await _contexto.Usuarios.AddAsync(usuario);
    }

    public async Task AdicionarPermissaoAsync(Permissao permissao)
    {
        await _contexto.Permissaos.AddAsync(permissao);
    }

    public async Task AdicionarPermissaoUsuarioAsync(PermissaoUsuario permissaoUsuario)
    {
        await _contexto.PermissaoUsuarios.AddAsync(permissaoUsuario);
    }

    public async Task AdicionarAppPermissaoAsync(AppPermissao appPermissao)
    {
        await _contexto.AppPermissaos.AddAsync(appPermissao);
    }

    public async Task AdicionarFuncaoPermissaoAsync(FuncaoPermissao funcaoPermissao)
    {
        await _contexto.FuncaoPermissaos.AddAsync(funcaoPermissao);
    }

    public async Task AdicionarGreAsync(Gre gre)
    {
        await _contexto.Gres.AddAsync(gre);
    }

    public async Task AdicionarInstituicaoAsync(Instituicao instituicao)
    {
        await _contexto.Instituicaos.AddAsync(instituicao);
    }

    public async Task AdicionarInstituicaoUsuarioAsync(InstituicaoUsuario instituicaoUsuario)
    {
        await _contexto.InstituicaoUsuarios.AddAsync(instituicaoUsuario);
    }

    public async Task AdicionarAreaConhecimentoAsync(AreaConhecimento areaConhecimento)
    {
        await _contexto.AreaConhecimentos.AddAsync(areaConhecimento);
    }

    public async Task AdicionarCategoriaAsync(Categoria categoria)
    {
        await _contexto.Categorias.AddAsync(categoria);
    }

    public async Task AdicionarTemaAsync(Tema tema)
    {
        await _contexto.Temas.AddAsync(tema);
    }

    public async Task AdicionarCriterioAsync(Criterio criterio)
    {
        await _contexto.Criterios.AddAsync(criterio);
    }

    public async Task AdicionarFeiraAfiliadaAsync(FeiraAfiliada feiraAfiliada)
    {
        await _contexto.FeiraAfiliadas.AddAsync(feiraAfiliada);
    }

    public async Task AdicionarFeiraAreaAsync(FeiraArea feiraArea)
    {
        await _contexto.FeiraAreas.AddAsync(feiraArea);
    }

    public async Task AdicionarEditalFeiraAsync(EditalFeira editalFeira)
    {
        await _contexto.EditalFeiras.AddAsync(editalFeira);
    }

    public async Task AdicionarProjetoAsync(Projeto projeto)
    {
        await _contexto.Projetos.AddAsync(projeto);
    }

    public async Task<int> SalvarAlteracoesAsync()
    {
        try
        {
            return await _contexto.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new ErroAoSalvarException(
                ex.InnerException?.Message ?? ex.Message, ex);
        }
    }
}