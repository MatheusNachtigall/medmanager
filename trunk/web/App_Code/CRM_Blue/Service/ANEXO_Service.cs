using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela ANEXO
/// </summary>
namespace CRM_Blue.Service
{
    public class ANEXO_Service
    {
        protected ANEXO_ADO _ANEXO_ADO;

        public ANEXO_Service()
        {
            this._ANEXO_ADO = new ANEXO_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela ANEXO
        /// </summary>
        public ANEXO Carregar(ANEXO filtro)
        {
            try
            {
                return this._ANEXO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela ANEXO pelo ID
        /// </summary>
        public ANEXO Carregar(Int32 id)
        {
            try
            {
				ANEXO filtro = new ANEXO();
                filtro.ANEXO_ID = id;
                return this._ANEXO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela ANEXO
        /// </summary>
        public Paginacao<ANEXO> Listar(ANEXO filtro, int pagina, int registrosPorPagina, ANEXO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._ANEXO_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<ANEXO> Listar(ANEXO filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._ANEXO_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<ANEXO> Listar(ANEXO filtro, ANEXO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._ANEXO_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<ANEXO> Listar(int pagina, int registrosPorPagina, ANEXO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._ANEXO_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<ANEXO> Listar(ANEXO filtro)
        {
            try
            {
                return this._ANEXO_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<ANEXO> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._ANEXO_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<ANEXO> Listar(ANEXO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._ANEXO_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<ANEXO> Listar()
        {
            try
            {
                return this._ANEXO_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Listar();", ex);
                throw ex;
            }
        }

		public ANEXO Inserir(ANEXO item)
        {
            try
            {
                return this._ANEXO_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public ANEXO Atualizar(ANEXO item)
        {
            try
            {
                return this._ANEXO_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(ANEXO filtro)
        {
            try
            {
                this._ANEXO_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("ANEXO_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
