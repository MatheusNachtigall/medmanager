using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela CLIENTE
/// </summary>
namespace CRM_Blue.Service
{
    public class CLIENTE_Service
    {
        protected CLIENTE_ADO _CLIENTE_ADO;

        public CLIENTE_Service()
        {
            this._CLIENTE_ADO = new CLIENTE_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela CLIENTE
        /// </summary>
        public CLIENTE Carregar(CLIENTE filtro)
        {
            try
            {
                return this._CLIENTE_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela CLIENTE pelo ID
        /// </summary>
        public CLIENTE Carregar(Int32 id)
        {
            try
            {
				CLIENTE filtro = new CLIENTE();
                filtro.CLIENTE_ID = id;
                return this._CLIENTE_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela CLIENTE
        /// </summary>
        public Paginacao<CLIENTE> Listar(CLIENTE filtro, int pagina, int registrosPorPagina, CLIENTE_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._CLIENTE_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<CLIENTE> Listar(CLIENTE filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._CLIENTE_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<CLIENTE> Listar(CLIENTE filtro, CLIENTE_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._CLIENTE_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<CLIENTE> Listar(int pagina, int registrosPorPagina, CLIENTE_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._CLIENTE_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<CLIENTE> Listar(CLIENTE filtro)
        {
            try
            {
                return this._CLIENTE_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<CLIENTE> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._CLIENTE_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<CLIENTE> Listar(CLIENTE_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._CLIENTE_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<CLIENTE> Listar()
        {
            try
            {
                return this._CLIENTE_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Listar();", ex);
                throw ex;
            }
        }

		public CLIENTE Inserir(CLIENTE item)
        {
            try
            {
                return this._CLIENTE_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public CLIENTE Atualizar(CLIENTE item)
        {
            try
            {
                return this._CLIENTE_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(CLIENTE filtro)
        {
            try
            {
                this._CLIENTE_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("CLIENTE_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
