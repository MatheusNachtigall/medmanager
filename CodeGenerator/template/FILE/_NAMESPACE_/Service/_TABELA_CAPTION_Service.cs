using System;
using System.Collections.Generic;
using System.Web;
using #NAMESPACE#.ADO;
using #NAMESPACE#.Entity;
using #NAMESPACE#.Enumerator;

/// <summary>
/// Camada de Servico da tabela #TABELA_CAPTION#
/// </summary>
namespace #NAMESPACE#.Service
{
    public class #TABELA_CAPTION#_Service
    {
        protected #TABELA_CAPTION#_ADO _#TABELA#_ADO;

        public #TABELA_CAPTION#_Service()
        {
            this._#TABELA#_ADO = new #TABELA_CAPTION#_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela #TABELA_CAPTION#
        /// </summary>
        public #TABELA_CAPTION# Carregar(#TABELA_CAPTION# filtro)
        {
            try
            {
                return this._#TABELA#_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela #TABELA_CAPTION# pelo ID
        /// </summary>
        public #TABELA_CAPTION# Carregar(#PK_TIPO# id)
        {
            try
            {
				#TABELA_CAPTION# filtro = new #TABELA_CAPTION#();
                filtro.#PK# = id;
                return this._#TABELA#_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela #TABELA_CAPTION#
        /// </summary>
        public Paginacao<#TABELA_CAPTION#> Listar(#TABELA_CAPTION# filtro, int pagina, int registrosPorPagina, #TABELA_CAPTION#_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._#TABELA#_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<#TABELA_CAPTION#> Listar(#TABELA_CAPTION# filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._#TABELA#_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<#TABELA_CAPTION#> Listar(#TABELA_CAPTION# filtro, #TABELA_CAPTION#_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._#TABELA#_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<#TABELA_CAPTION#> Listar(int pagina, int registrosPorPagina, #TABELA_CAPTION#_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._#TABELA#_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<#TABELA_CAPTION#> Listar(#TABELA_CAPTION# filtro)
        {
            try
            {
                return this._#TABELA#_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<#TABELA_CAPTION#> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._#TABELA#_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<#TABELA_CAPTION#> Listar(#TABELA_CAPTION#_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._#TABELA#_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<#TABELA_CAPTION#> Listar()
        {
            try
            {
                return this._#TABELA#_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Listar();", ex);
                throw ex;
            }
        }

		public #TABELA_CAPTION# Inserir(#TABELA_CAPTION# item)
        {
            try
            {
                return this._#TABELA#_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public #TABELA_CAPTION# Atualizar(#TABELA_CAPTION# item)
        {
            try
            {
                return this._#TABELA#_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(#TABELA_CAPTION# filtro)
        {
            try
            {
                this._#TABELA#_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("#TABELA_CAPTION#_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}