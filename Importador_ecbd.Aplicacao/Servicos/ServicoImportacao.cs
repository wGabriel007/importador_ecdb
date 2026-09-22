using Importador_ecbd.Aplicacao.Interfaces;
using Importador_ecbd.Aplicacao.Dtos;
using Importador_ecbd.Aplicacao.Excecoes;
using Importador_ecbd.Aplicacao.Interfaces;
using Importador_ecbd.Aplicacao.Mapeamento;
using Importador_ecbd.Dominio.Enums;


namespace Importador_ecbd.Aplicacao.Servicos;

/// <summary>
/// Implementação do serviço de importação. Cordena a leitura do
/// banco de origem, o mapeamento e a gravação do banco de destino,
/// registrando tudo que não pôde ser importado em vez de interromper.
/// </summary>
public class ServicoImportacao : IServicoImportacao
{
    private readonly IRepositorioOrigem  _repositorioOrigem;
    private readonly IRepositorioDestino _repositorioDestino;

    public ServicoImportacao(
        IRepositorioOrigem  repositorioOrigem,
        IRepositorioDestino repositorioDestino)
    {
        _repositorioOrigem  = repositorioOrigem;
        _repositorioDestino = repositorioDestino;
    }

    public async Task<ResumoPreImportacao> ObterResumoPreImportacaoAsync()
    {
        var resumo = new ResumoPreImportacao();
        // Lista centralizada de tabelas a contar para o resumo pré-importação.
        var tabelas = new[]
        {
            "ec_affi_area",
            "ec_affiliated_trade_fair",
            "ec_anno_affi",
            "ec_announcement",
            "ec_app_role",
            "ec_app_user",
            "ec_approleclaims",
            "ec_appuserlogins",
            "ec_appuserroles",
            "ec_appusertokens",
            "ec_area",
            "ec_aspuserclaims",
            "ec_cat",
            "ec_cat_theme",
            "ec_certificate",
            "ec_checking_presence",
            "ec_city",
            "ec_country",
            "ec_criterion",
            "ec_email",
            "ec_eval_crit",
            "ec_evaluation",
            "ec_evaluator",
            "ec_evaluator_announcement",
            "ec_experiment",
            "ec_experiment_class",
            "ec_institution",
            "ec_institution_user",
            "ec_levels",
            "ec_log",
            "ec_peoplegroup",
            "ec_permissions",
            "ec_proj_area",
            "ec_proj_image",
            "ec_proj_part",
            "ec_project",
            "ec_reg_edu_aut",
            "ec_request_project_message"
        };

        foreach (var tabela in tabelas)
        {
            var count = await _repositorioOrigem.ContarPorTabelaAsync(tabela);
            resumo.Tabelas.Add(new ContagemTabela { NomeTabela = tabela, QuantidadeRegistros = count });
        }

        var ecResponsible = await _repositorioOrigem.ObterResponsibleAsync();
        resumo.Tabelas.Add(new ContagemTabela
        {
            NomeTabela = "ec_responsible",
            QuantidadeRegistros = ecResponsible.Count
        });

        var ecRolePermission = await _repositorioOrigem.ObterRolePermissionAsync();
        resumo.Tabelas.Add(new ContagemTabela
        {
            NomeTabela = "ec_role_permission",
            QuantidadeRegistros = ecRolePermission.Count
        });

        var ecScheduling = await _repositorioOrigem.ObterSchedulingAsync();
        resumo.Tabelas.Add(new ContagemTabela
        {
            NomeTabela = "ec_scheduling",
            QuantidadeRegistros = ecScheduling.Count
        });

        var ecState = await _repositorioOrigem.ObterStateAsync();
        resumo.Tabelas.Add(new ContagemTabela
        {
            NomeTabela = "ec_state",
            QuantidadeRegistros = ecState.Count
        });

        var ecTheme = await _repositorioOrigem.ObterThemeAsync();
        resumo.Tabelas.Add(new ContagemTabela
        {
            NomeTabela = "ec_theme",
            QuantidadeRegistros = ecTheme.Count
        });

        var ecUserRole = await _repositorioOrigem.ObterUserRoleAsync();
        resumo.Tabelas.Add(new ContagemTabela
        {
            NomeTabela = "ec_user_role",
            QuantidadeRegistros = ecUserRole.Count
        });

        var ecUserpeoplegroup = await _repositorioOrigem.ObterUserpeoplegroupAsync();
        resumo.Tabelas.Add(new ContagemTabela
        {
            NomeTabela = "ec_userpeoplegroup",
            QuantidadeRegistros = ecUserpeoplegroup.Count
        });

        var ecWorkloads = await _repositorioOrigem.ObterWorkloadsAsync();
        resumo.Tabelas.Add(new ContagemTabela
        {
            NomeTabela = "ec_workloads",
            QuantidadeRegistros = ecWorkloads.Count
        });

        return resumo;
    }

    public async Task<ResultadoImportacao> ImportarAsync(IProgress<ProgressoImportacao> progresso)
    {
        var resultado = new ResultadoImportacao();

        /// Lista de "Etapas" de importação. Cada etapa é uma tabela.
        var etapas = new List<Func<Task>>
        {
            () => ImportarPaisAsync(resultado),
            () => ImportarEstadoAsync(resultado),
            () => ImportarCidadeAsync(resultado),
            () => ImportarUsuarioAsync(resultado),
            () => ImportarGreAsync(resultado),
            () => ImportarInstituicaoAsync(resultado),
            () => ImportarInstituicaoUsuarioAsync(resultado),
            () => ImportarAreaConhecimentoAsync(resultado),
            () => ImportarCategoriaAsync(resultado),
            () => ImportarTemaAsync(resultado),
            () => ImportarCriterioAsync(resultado),
            () => ImportarFeiraAfiliadaAsync(resultado),
            () => ImportarFeiraAreaAsync(resultado),
            () => ImportarEditalFeiraAsync(resultado),
            () => ImportarProjetoAsync(resultado),
            () => ImportarPermissaoAsync(resultado),
            () => ImportarAppPermissaoAsync(resultado),
            () => ImportarFuncaoPermissaoAsync(resultado),
            () => ImportarPermissaoUsuarioAsync(resultado)
        };

        for (int i = 0; i < etapas.Count; i++)
        {
            var etapa = etapas[i];
            var metodoNome = etapa.Method.Name ?? $"Etapa_{i + 1}";
            var nomeTabelaAmigavel = metodoNome.Replace("Importar", string.Empty).Replace("Async", string.Empty).ToLowerInvariant();

            // Reporta progresso antes de iniciar a etapa
            progresso.Report(new ProgressoImportacao
            {
                TabelaAtualIndice = i + 1,
                TotalTabelas = etapas.Count,
                NomeTabelaAtual = metodoNome
            });

            try
            {
                // Executa a etapa. Cada etapa pode atualizar 'resultado' com detalhes.
                await etapa();

                // Marca tabela como processada com sucesso
                resultado.TotalTabelasProcessadas++;
            }
            catch (Exception ex)
            {
                // Registra falha da etapa para aparecer no relatório final
                resultado.TabelasNaoImportadas.Add(new ItemNaoImportado
                {
                    NomeTabela = nomeTabelaAmigavel,
                    Motivo = EnumMotivoFalha.ErroExecucao,
                    Descricao = ex.ToString()
                });

                // Também registra como erro genérico de registro para permitir exibição detalhada
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = nomeTabelaAmigavel,
                    Motivo = EnumMotivoFalha.ErroExecucao,
                    Descricao = ex.Message
                });

                // Continua para próxima etapa
                continue;
            }
        }

        return resultado;
    }

    /// <summary>Importa ec_country (origem) para Pais (destino).</summary>
    private async Task ImportarPaisAsync(ResultadoImportacao resultado)
    {
        var ecCountrysOrigem = await _repositorioOrigem.ObterCountryAsync();

        if (ecCountrysOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_country");
            return;
        }

        foreach (var item in ecCountrysOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorPais.Mapear(item);
                await _repositorioDestino.AdicionarPaisAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_country",
                    IdentificadorRegistro = $"ec_country.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_country",
                    IdentificadorRegistro = $"ec_country.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_state (origem) para Estado (destino).</summary>
    private async Task ImportarEstadoAsync(ResultadoImportacao resultado)
    {
        var ecStatesOrigem = await _repositorioOrigem.ObterStateAsync();

        if (ecStatesOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_state");
            return;
        }

        foreach (var item in ecStatesOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorEstado.Mapear(item);
                await _repositorioDestino.AdicionarEstadoAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_state",
                    IdentificadorRegistro = $"ec_state.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_state",
                    IdentificadorRegistro = $"ec_state.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_city (origem) para Cidade (destino).</summary>
    private async Task ImportarCidadeAsync(ResultadoImportacao resultado)
    {
        var ecCitysOrigem = await _repositorioOrigem.ObterCityAsync();

        if (ecCitysOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_city");
            return;
        }

        foreach (var item in ecCitysOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorCidade.Mapear(item);
                await _repositorioDestino.AdicionarCidadeAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_city",
                    IdentificadorRegistro = $"ec_city.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_city",
                    IdentificadorRegistro = $"ec_city.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_app_user (origem) para Usuario (destino).</summary>
    private async Task ImportarUsuarioAsync(ResultadoImportacao resultado)
    {
        var ecAppUsersOrigem = await _repositorioOrigem.ObterAppUserAsync();

        if (ecAppUsersOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_app_user");
            return;
        }

        foreach (var item in ecAppUsersOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorUsuario.Mapear(item);
                await _repositorioDestino.AdicionarUsuarioAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_app_user",
                    IdentificadorRegistro = $"ec_app_user.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_app_user",
                    IdentificadorRegistro = $"ec_app_user.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_reg_edu_aut (origem) para Gre (destino).</summary>
    private async Task ImportarGreAsync(ResultadoImportacao resultado)
    {
        var ecRegEduAutsOrigem = await _repositorioOrigem.ObterRegEduAutAsync();

        if (ecRegEduAutsOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_reg_edu_aut");
            return;
        }

        foreach (var item in ecRegEduAutsOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorGre.Mapear(item);
                await _repositorioDestino.AdicionarGreAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_reg_edu_aut",
                    IdentificadorRegistro = $"ec_reg_edu_aut.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_reg_edu_aut",
                    IdentificadorRegistro = $"ec_reg_edu_aut.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_institution (origem) para Instituicao (destino).</summary>
    private async Task ImportarInstituicaoAsync(ResultadoImportacao resultado)
    {
        var ecInstitutionsOrigem = await _repositorioOrigem.ObterInstitutionAsync();

        if (ecInstitutionsOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_institution");
            return;
        }

        foreach (var item in ecInstitutionsOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorInstituicao.Mapear(item);
                await _repositorioDestino.AdicionarInstituicaoAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_institution",
                    IdentificadorRegistro = $"ec_institution.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_institution",
                    IdentificadorRegistro = $"ec_institution.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_institution_user (origem) para InstituicaoUsuario (destino).</summary>
    private async Task ImportarInstituicaoUsuarioAsync(ResultadoImportacao resultado)
    {
        var ecInstitutionUsersOrigem = await _repositorioOrigem.ObterInstitutionUserAsync();

        if (ecInstitutionUsersOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_institution_user");
            return;
        }

        foreach (var item in ecInstitutionUsersOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorInstituicaoUsuario.Mapear(item);
                await _repositorioDestino.AdicionarInstituicaoUsuarioAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_institution_user",
                    IdentificadorRegistro = $"ec_institution_user.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_institution_user",
                    IdentificadorRegistro = $"ec_institution_user.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_area (origem) para AreaConhecimento (destino).</summary>
    private async Task ImportarAreaConhecimentoAsync(ResultadoImportacao resultado)
    {
        var ecAreasOrigem = await _repositorioOrigem.ObterAreaAsync();

        if (ecAreasOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_area");
            return;
        }

        foreach (var item in ecAreasOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorAreaConhecimento.Mapear(item);
                await _repositorioDestino.AdicionarAreaConhecimentoAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_area",
                    IdentificadorRegistro = $"ec_area.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_area",
                    IdentificadorRegistro = $"ec_area.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_cat (origem) para Categoria (destino).</summary>
    private async Task ImportarCategoriaAsync(ResultadoImportacao resultado)
    {
        var ecCatsOrigem = await _repositorioOrigem.ObterCatAsync();

        if (ecCatsOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_cat");
            return;
        }

        foreach (var item in ecCatsOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorCategoria.Mapear(item);
                await _repositorioDestino.AdicionarCategoriaAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_cat",
                    IdentificadorRegistro = $"ec_cat.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_cat",
                    IdentificadorRegistro = $"ec_cat.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_theme (origem) para Tema (destino).</summary>
    private async Task ImportarTemaAsync(ResultadoImportacao resultado)
    {
        var ecThemesOrigem = await _repositorioOrigem.ObterThemeAsync();

        if (ecThemesOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_theme");
            return;
        }

        foreach (var item in ecThemesOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorTema.Mapear(item);
                await _repositorioDestino.AdicionarTemaAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_theme",
                    IdentificadorRegistro = $"ec_theme.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_theme",
                    IdentificadorRegistro = $"ec_theme.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_criterion (origem) para Criterio (destino).</summary>
    private async Task ImportarCriterioAsync(ResultadoImportacao resultado)
    {
        var ecCriterionsOrigem = await _repositorioOrigem.ObterCriterionAsync();

        if (ecCriterionsOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_criterion");
            return;
        }

        foreach (var item in ecCriterionsOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorCriterio.Mapear(item);
                await _repositorioDestino.AdicionarCriterioAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_criterion",
                    IdentificadorRegistro = $"ec_criterion.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_criterion",
                    IdentificadorRegistro = $"ec_criterion.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_affiliated_trade_fair (origem) para FeiraAfiliada (destino).</summary>
    private async Task ImportarFeiraAfiliadaAsync(ResultadoImportacao resultado)
    {
        var ecAffiliatedTradeFairsOrigem = await _repositorioOrigem.ObterAffiliatedTradeFairAsync();

        if (ecAffiliatedTradeFairsOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_affiliated_trade_fair");
            return;
        }

        foreach (var item in ecAffiliatedTradeFairsOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorFeiraAfiliada.Mapear(item);
                await _repositorioDestino.AdicionarFeiraAfiliadaAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_affiliated_trade_fair",
                    IdentificadorRegistro = $"ec_affiliated_trade_fair.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_affiliated_trade_fair",
                    IdentificadorRegistro = $"ec_affiliated_trade_fair.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_affi_area (origem) para FeiraArea (destino).</summary>
    private async Task ImportarFeiraAreaAsync(ResultadoImportacao resultado)
    {
        var ecAffiAreasOrigem = await _repositorioOrigem.ObterAffiAreaAsync();

        if (ecAffiAreasOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_affi_area");
            return;
        }

        foreach (var item in ecAffiAreasOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorFeiraArea.Mapear(item);
                await _repositorioDestino.AdicionarFeiraAreaAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_affi_area",
                    IdentificadorRegistro = $"ec_affi_area (FeiraId={item.AffiliatedTradeFairsId}, AreaId={item.KnowledgeAreasId})",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_affi_area",
                    IdentificadorRegistro = $"ec_affi_area (FeiraId={item.AffiliatedTradeFairsId}, AreaId={item.KnowledgeAreasId})",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_anno_affi (origem) para EditalFeira (destino).</summary>
    private async Task ImportarEditalFeiraAsync(ResultadoImportacao resultado)
    {
        var ecAnnoAffisOrigem = await _repositorioOrigem.ObterAnnoAffiAsync();

        if (ecAnnoAffisOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_anno_affi");
            return;
        }

        foreach (var item in ecAnnoAffisOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorEditalFeira.Mapear(item);
                await _repositorioDestino.AdicionarEditalFeiraAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_anno_affi",
                    IdentificadorRegistro = $"ec_anno_affi.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_anno_affi",
                    IdentificadorRegistro = $"ec_anno_affi.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_project (origem) para Projeto (destino).</summary>
    private async Task ImportarProjetoAsync(ResultadoImportacao resultado)
    {
        var ecProjectsOrigem = await _repositorioOrigem.ObterProjectAsync();

        if (ecProjectsOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_project");
            return;
        }

        foreach (var item in ecProjectsOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorProjeto.Mapear(item);
                await _repositorioDestino.AdicionarProjetoAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_project",
                    IdentificadorRegistro = $"ec_project.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_project",
                    IdentificadorRegistro = $"ec_project.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_permissions (origem) para Permissao (destino).</summary>
    private async Task ImportarPermissaoAsync(ResultadoImportacao resultado)
    {
        var ecPermissionsOrigem = await _repositorioOrigem.ObterPermissionsAsync();

        if (ecPermissionsOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_permissions");
            return;
        }

        foreach (var item in ecPermissionsOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorPermissao.Mapear(item);
                await _repositorioDestino.AdicionarPermissaoAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_permissions",
                    IdentificadorRegistro = $"ec_permissions.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_permissions",
                    IdentificadorRegistro = $"ec_permissions.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_app_role (origem) para AppPermissao (destino).</summary>
    private async Task ImportarAppPermissaoAsync(ResultadoImportacao resultado)
    {
        var ecAppRolesOrigem = await _repositorioOrigem.ObterAppRoleAsync();

        if (ecAppRolesOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_app_role");
            return;
        }

        foreach (var item in ecAppRolesOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorAppPermissao.Mapear(item);
                await _repositorioDestino.AdicionarAppPermissaoAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_app_role",
                    IdentificadorRegistro = $"ec_app_role.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_app_role",
                    IdentificadorRegistro = $"ec_app_role.Id = {item.Id}",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_role_permission (origem) para FuncaoPermissao (destino).</summary>
    private async Task ImportarFuncaoPermissaoAsync(ResultadoImportacao resultado)
    {
        var ecRolePermissionsOrigem = await _repositorioOrigem.ObterRolePermissionAsync();

        if (ecRolePermissionsOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_role_permission");
            return;
        }

        foreach (var item in ecRolePermissionsOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorFuncaoPermissao.Mapear(item);
                await _repositorioDestino.AdicionarFuncaoPermissaoAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_role_permission",
                    IdentificadorRegistro = $"ec_role_permission (PapelId={item.ApplicationRolesId}, PermissaoId={item.PermissionsId})",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_role_permission",
                    IdentificadorRegistro = $"ec_role_permission (PapelId={item.ApplicationRolesId}, PermissaoId={item.PermissionsId})",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }

    /// <summary>Importa ec_appuserroles (origem) para PermissaoUsuario (destino).</summary>
    private async Task ImportarPermissaoUsuarioAsync(ResultadoImportacao resultado)
    {
        var ecAppuserrolesOrigem = await _repositorioOrigem.ObterAppuserrolesAsync();

        if (ecAppuserrolesOrigem.Count == 0)
        {
            resultado.TabelasVazias.Add("ec_appuserroles");
            return;
        }

        foreach (var item in ecAppuserrolesOrigem)
        {
            try
            {
                var entidadeDestino = MapeadorPermissaoUsuario.Mapear(item);
                await _repositorioDestino.AdicionarPermissaoUsuarioAsync(entidadeDestino);
                await _repositorioDestino.SalvarAlteracoesAsync();
                resultado.TotalRegistrosImportados++;
            }
            catch (ErroAoSalvarException ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_appuserroles",
                    IdentificadorRegistro = $"ec_appuserroles (UsuarioId={item.UserId}, PapelId={item.RoleId})",
                    Motivo = EnumMotivoFalha.ViolacaoDeChaveEstrangeira,
                    Descricao = ex.Message
                });
            }
            catch (Exception ex)
            {
                resultado.RegistrosComErro.Add(new ItemNaoImportado
                {
                    NomeTabela = "ec_appuserroles",
                    IdentificadorRegistro = $"ec_appuserroles (UsuarioId={item.UserId}, PapelId={item.RoleId})",
                    Motivo = EnumMotivoFalha.ErroDesconhecido,
                    Descricao = ex.Message
                });
            }
        }
    }
}

