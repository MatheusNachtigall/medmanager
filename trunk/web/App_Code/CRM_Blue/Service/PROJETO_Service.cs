using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela PROJETO
/// </summary>
namespace CRM_Blue.Service
{
    public class PROJETO_Service
    {
        protected PROJETO_ADO _PROJETO_ADO;

        public PROJETO_Service()
        {
            this._PROJETO_ADO = new PROJETO_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela PROJETO
        /// </summary>
        public PROJETO Carregar(PROJETO filtro)
        {
            try
            {
                return this._PROJETO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela PROJETO pelo ID
        /// </summary>
        public PROJETO Carregar(Int32 id)
        {
            try
            {
				PROJETO filtro = new PROJETO();
                filtro.PROJETO_ID = id;
                return this._PROJETO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela PROJETO
        /// </summary>
        public Paginacao<PROJETO> Listar(PROJETO filtro, int pagina, int registrosPorPagina, PROJETO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._PROJETO_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<PROJETO> Listar(PROJETO filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._PROJETO_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<PROJETO> Listar(PROJETO filtro, PROJETO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._PROJETO_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<PROJETO> Listar(int pagina, int registrosPorPagina, PROJETO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._PROJETO_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<PROJETO> Listar(PROJETO filtro)
        {
            try
            {
                return this._PROJETO_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<PROJETO> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._PROJETO_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<PROJETO> Listar(PROJETO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._PROJETO_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<PROJETO> Listar()
        {
            try
            {
                return this._PROJETO_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Listar();", ex);
                throw ex;
            }
        }

		public PROJETO Inserir(PROJETO item)
        {
            try
            {
                return this._PROJETO_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public PROJETO Atualizar(PROJETO item)
        {
            try
            {
                return this._PROJETO_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(PROJETO filtro)
        {
            try
            {
                this._PROJETO_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PROJETO_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
