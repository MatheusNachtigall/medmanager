using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela FATURAMENTO
/// </summary>
namespace CRM_Blue.Service
{
    public class FATURAMENTO_Service
    {
        protected FATURAMENTO_ADO _FATURAMENTO_ADO;

        public FATURAMENTO_Service()
        {
            this._FATURAMENTO_ADO = new FATURAMENTO_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela FATURAMENTO
        /// </summary>
        public FATURAMENTO Carregar(FATURAMENTO filtro)
        {
            try
            {
                return this._FATURAMENTO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela FATURAMENTO pelo ID
        /// </summary>
        public FATURAMENTO Carregar(Int32 id)
        {
            try
            {
				FATURAMENTO filtro = new FATURAMENTO();
                filtro.FATURAMENTO_ID = id;
                return this._FATURAMENTO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela FATURAMENTO
        /// </summary>
        public Paginacao<FATURAMENTO> Listar(FATURAMENTO filtro, int pagina, int registrosPorPagina, FATURAMENTO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._FATURAMENTO_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<FATURAMENTO> Listar(FATURAMENTO filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._FATURAMENTO_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<FATURAMENTO> Listar(FATURAMENTO filtro, FATURAMENTO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._FATURAMENTO_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<FATURAMENTO> Listar(int pagina, int registrosPorPagina, FATURAMENTO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._FATURAMENTO_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<FATURAMENTO> Listar(FATURAMENTO filtro)
        {
            try
            {
                return this._FATURAMENTO_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<FATURAMENTO> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._FATURAMENTO_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<FATURAMENTO> Listar(FATURAMENTO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._FATURAMENTO_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<FATURAMENTO> Listar()
        {
            try
            {
                return this._FATURAMENTO_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Listar();", ex);
                throw ex;
            }
        }

		public FATURAMENTO Inserir(FATURAMENTO item)
        {
            try
            {
                return this._FATURAMENTO_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public FATURAMENTO Atualizar(FATURAMENTO item)
        {
            try
            {
                return this._FATURAMENTO_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(FATURAMENTO filtro)
        {
            try
            {
                this._FATURAMENTO_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("FATURAMENTO_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
