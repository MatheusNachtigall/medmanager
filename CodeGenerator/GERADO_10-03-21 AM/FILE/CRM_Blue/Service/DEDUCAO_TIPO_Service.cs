using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela DEDUCAO_TIPO
/// </summary>
namespace CRM_Blue.Service
{
    public class DEDUCAO_TIPO_Service
    {
        protected DEDUCAO_TIPO_ADO _DEDUCAO_TIPO_ADO;

        public DEDUCAO_TIPO_Service()
        {
            this._DEDUCAO_TIPO_ADO = new DEDUCAO_TIPO_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela DEDUCAO_TIPO
        /// </summary>
        public DEDUCAO_TIPO Carregar(DEDUCAO_TIPO filtro)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela DEDUCAO_TIPO pelo ID
        /// </summary>
        public DEDUCAO_TIPO Carregar(Int32 id)
        {
            try
            {
				DEDUCAO_TIPO filtro = new DEDUCAO_TIPO();
                filtro.DEDUCAO_TIPO_ID = id;
                return this._DEDUCAO_TIPO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela DEDUCAO_TIPO
        /// </summary>
        public Paginacao<DEDUCAO_TIPO> Listar(DEDUCAO_TIPO filtro, int pagina, int registrosPorPagina, DEDUCAO_TIPO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<DEDUCAO_TIPO> Listar(DEDUCAO_TIPO filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<DEDUCAO_TIPO> Listar(DEDUCAO_TIPO filtro, DEDUCAO_TIPO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<DEDUCAO_TIPO> Listar(int pagina, int registrosPorPagina, DEDUCAO_TIPO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<DEDUCAO_TIPO> Listar(DEDUCAO_TIPO filtro)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<DEDUCAO_TIPO> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<DEDUCAO_TIPO> Listar(DEDUCAO_TIPO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<DEDUCAO_TIPO> Listar()
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Listar();", ex);
                throw ex;
            }
        }

		public DEDUCAO_TIPO Inserir(DEDUCAO_TIPO item)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public DEDUCAO_TIPO Atualizar(DEDUCAO_TIPO item)
        {
            try
            {
                return this._DEDUCAO_TIPO_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(DEDUCAO_TIPO filtro)
        {
            try
            {
                this._DEDUCAO_TIPO_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("DEDUCAO_TIPO_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
