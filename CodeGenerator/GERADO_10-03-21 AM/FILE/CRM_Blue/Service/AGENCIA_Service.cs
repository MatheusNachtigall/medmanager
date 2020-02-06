using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela AGENCIA
/// </summary>
namespace CRM_Blue.Service
{
    public class AGENCIA_Service
    {
        protected AGENCIA_ADO _AGENCIA_ADO;

        public AGENCIA_Service()
        {
            this._AGENCIA_ADO = new AGENCIA_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela AGENCIA
        /// </summary>
        public AGENCIA Carregar(AGENCIA filtro)
        {
            try
            {
                return this._AGENCIA_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela AGENCIA pelo ID
        /// </summary>
        public AGENCIA Carregar(Int32 id)
        {
            try
            {
				AGENCIA filtro = new AGENCIA();
                filtro.AGENCIA_ID = id;
                return this._AGENCIA_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela AGENCIA
        /// </summary>
        public Paginacao<AGENCIA> Listar(AGENCIA filtro, int pagina, int registrosPorPagina, AGENCIA_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._AGENCIA_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<AGENCIA> Listar(AGENCIA filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._AGENCIA_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<AGENCIA> Listar(AGENCIA filtro, AGENCIA_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._AGENCIA_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<AGENCIA> Listar(int pagina, int registrosPorPagina, AGENCIA_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._AGENCIA_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<AGENCIA> Listar(AGENCIA filtro)
        {
            try
            {
                return this._AGENCIA_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<AGENCIA> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._AGENCIA_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<AGENCIA> Listar(AGENCIA_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._AGENCIA_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<AGENCIA> Listar()
        {
            try
            {
                return this._AGENCIA_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Listar();", ex);
                throw ex;
            }
        }

		public AGENCIA Inserir(AGENCIA item)
        {
            try
            {
                return this._AGENCIA_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public AGENCIA Atualizar(AGENCIA item)
        {
            try
            {
                return this._AGENCIA_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(AGENCIA filtro)
        {
            try
            {
                this._AGENCIA_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("AGENCIA_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
