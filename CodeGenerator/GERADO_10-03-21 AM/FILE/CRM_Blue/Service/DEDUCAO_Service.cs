using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela DEDUCAO
/// </summary>
namespace CRM_Blue.Service
{
    public class DEDUCAO_Service
    {
        protected DEDUCAO_ADO _DEDUCAO_ADO;

        public DEDUCAO_Service()
        {
            this._DEDUCAO_ADO = new DEDUCAO_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela DEDUCAO
        /// </summary>
        public DEDUCAO Carregar(DEDUCAO filtro)
        {
            try
            {
                return this._DEDUCAO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela DEDUCAO pelo ID
        /// </summary>
        public DEDUCAO Carregar(Int32 id)
        {
            try
            {
				DEDUCAO filtro = new DEDUCAO();
                filtro.DEDUCAO_ID = id;
                return this._DEDUCAO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela DEDUCAO
        /// </summary>
        public Paginacao<DEDUCAO> Listar(DEDUCAO filtro, int pagina, int registrosPorPagina, DEDUCAO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._DEDUCAO_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<DEDUCAO> Listar(DEDUCAO filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._DEDUCAO_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<DEDUCAO> Listar(DEDUCAO filtro, DEDUCAO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._DEDUCAO_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<DEDUCAO> Listar(int pagina, int registrosPorPagina, DEDUCAO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._DEDUCAO_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<DEDUCAO> Listar(DEDUCAO filtro)
        {
            try
            {
                return this._DEDUCAO_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<DEDUCAO> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._DEDUCAO_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<DEDUCAO> Listar(DEDUCAO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._DEDUCAO_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<DEDUCAO> Listar()
        {
            try
            {
                return this._DEDUCAO_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Listar();", ex);
                throw ex;
            }
        }

		public DEDUCAO Inserir(DEDUCAO item)
        {
            try
            {
                return this._DEDUCAO_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public DEDUCAO Atualizar(DEDUCAO item)
        {
            try
            {
                return this._DEDUCAO_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(DEDUCAO filtro)
        {
            try
            {
                this._DEDUCAO_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
