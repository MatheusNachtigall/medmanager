using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela HOSPITAL
/// </summary>
namespace CRM_Blue.Service
{
    public class HOSPITAL_Service
    {
        protected HOSPITAL_ADO _HOSPITAL_ADO;

        public HOSPITAL_Service()
        {
            this._HOSPITAL_ADO = new HOSPITAL_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela HOSPITAL
        /// </summary>
        public HOSPITAL Carregar(HOSPITAL filtro)
        {
            try
            {
                return this._HOSPITAL_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela HOSPITAL pelo ID
        /// </summary>
        public HOSPITAL Carregar(Int32 id)
        {
            try
            {
				HOSPITAL filtro = new HOSPITAL();
                filtro.HOSPITAL_ID = id;
                return this._HOSPITAL_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela HOSPITAL
        /// </summary>
        public Paginacao<HOSPITAL> Listar(HOSPITAL filtro, int pagina, int registrosPorPagina, HOSPITAL_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._HOSPITAL_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<HOSPITAL> Listar(HOSPITAL filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._HOSPITAL_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<HOSPITAL> Listar(HOSPITAL filtro, HOSPITAL_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._HOSPITAL_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<HOSPITAL> Listar(int pagina, int registrosPorPagina, HOSPITAL_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._HOSPITAL_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<HOSPITAL> Listar(HOSPITAL filtro)
        {
            try
            {
                return this._HOSPITAL_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<HOSPITAL> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._HOSPITAL_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<HOSPITAL> Listar(HOSPITAL_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._HOSPITAL_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<HOSPITAL> Listar()
        {
            try
            {
                return this._HOSPITAL_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Listar();", ex);
                throw ex;
            }
        }

		public HOSPITAL Inserir(HOSPITAL item)
        {
            try
            {
                return this._HOSPITAL_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public HOSPITAL Atualizar(HOSPITAL item)
        {
            try
            {
                return this._HOSPITAL_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(HOSPITAL filtro)
        {
            try
            {
                this._HOSPITAL_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("HOSPITAL_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
