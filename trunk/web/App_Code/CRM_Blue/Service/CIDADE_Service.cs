using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela CIDADE
/// </summary>
namespace CRM_Blue.Service
{
    public class CIDADE_Service
    {
        protected CIDADE_ADO _CIDADE_ADO;

        public CIDADE_Service()
        {
            this._CIDADE_ADO = new CIDADE_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela CIDADE
        /// </summary>
        public CIDADE Carregar(CIDADE filtro)
        {
            try
            {
                return this._CIDADE_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela CIDADE pelo ID
        /// </summary>
        public CIDADE Carregar(Int32 id)
        {
            try
            {
				CIDADE filtro = new CIDADE();
                filtro.CIDADE_ID = id;
                return this._CIDADE_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela CIDADE
        /// </summary>
        public Paginacao<CIDADE> Listar(CIDADE filtro, int pagina, int registrosPorPagina, CIDADE_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._CIDADE_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<CIDADE> Listar(CIDADE filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._CIDADE_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<CIDADE> Listar(CIDADE filtro, CIDADE_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._CIDADE_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<CIDADE> Listar(int pagina, int registrosPorPagina, CIDADE_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._CIDADE_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<CIDADE> Listar(CIDADE filtro)
        {
            try
            {
                return this._CIDADE_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<CIDADE> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._CIDADE_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<CIDADE> Listar(CIDADE_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._CIDADE_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<CIDADE> Listar()
        {
            try
            {
                return this._CIDADE_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Listar();", ex);
                throw ex;
            }
        }

		public CIDADE Inserir(CIDADE item)
        {
            try
            {
                return this._CIDADE_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public CIDADE Atualizar(CIDADE item)
        {
            try
            {
                return this._CIDADE_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(CIDADE filtro)
        {
            try
            {
                this._CIDADE_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CIDADE_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
