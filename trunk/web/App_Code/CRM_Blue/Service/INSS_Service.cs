using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela INSS
/// </summary>
namespace CRM_Blue.Service
{
    public class INSS_Service
    {
        protected INSS_ADO _INSS_ADO;

        public INSS_Service()
        {
            this._INSS_ADO = new INSS_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela INSS
        /// </summary>
        public INSS Carregar(INSS filtro)
        {
            try
            {
                return this._INSS_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela INSS pelo ID
        /// </summary>
        public INSS Carregar(Int32 id)
        {
            try
            {
				INSS filtro = new INSS();
                filtro.INSS_ID = id;
                return this._INSS_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela INSS
        /// </summary>
        public Paginacao<INSS> Listar(INSS filtro, int pagina, int registrosPorPagina, INSS_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._INSS_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<INSS> Listar(INSS filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._INSS_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<INSS> Listar(INSS filtro, INSS_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._INSS_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<INSS> Listar(int pagina, int registrosPorPagina, INSS_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._INSS_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<INSS> Listar(INSS filtro)
        {
            try
            {
                return this._INSS_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<INSS> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._INSS_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<INSS> Listar(INSS_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._INSS_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<INSS> Listar()
        {
            try
            {
                return this._INSS_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Listar();", ex);
                throw ex;
            }
        }

		public INSS Inserir(INSS item)
        {
            try
            {
                return this._INSS_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public INSS Atualizar(INSS item)
        {
            try
            {
                return this._INSS_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(INSS filtro)
        {
            try
            {
                this._INSS_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("INSS_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
