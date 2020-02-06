using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela USUARIO
/// </summary>
namespace CRM_Blue.Service
{
    public class USUARIO_Service
    {
        protected USUARIO_ADO _USUARIO_ADO;

        public USUARIO_Service()
        {
            this._USUARIO_ADO = new USUARIO_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela USUARIO
        /// </summary>
        public USUARIO Carregar(USUARIO filtro)
        {
            try
            {
                return this._USUARIO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela USUARIO pelo ID
        /// </summary>
        public USUARIO Carregar(Int32 id)
        {
            try
            {
				USUARIO filtro = new USUARIO();
                filtro.USUARIO_ID = id;
                return this._USUARIO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela USUARIO
        /// </summary>
        public Paginacao<USUARIO> Listar(USUARIO filtro, int pagina, int registrosPorPagina, USUARIO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._USUARIO_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<USUARIO> Listar(USUARIO filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._USUARIO_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<USUARIO> Listar(USUARIO filtro, USUARIO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._USUARIO_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<USUARIO> Listar(int pagina, int registrosPorPagina, USUARIO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._USUARIO_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<USUARIO> Listar(USUARIO filtro)
        {
            try
            {
                return this._USUARIO_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<USUARIO> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._USUARIO_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<USUARIO> Listar(USUARIO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._USUARIO_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<USUARIO> Listar()
        {
            try
            {
                return this._USUARIO_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Listar();", ex);
                throw ex;
            }
        }

		public USUARIO Inserir(USUARIO item)
        {
            try
            {
                return this._USUARIO_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public USUARIO Atualizar(USUARIO item)
        {
            try
            {
                return this._USUARIO_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(USUARIO filtro)
        {
            try
            {
                this._USUARIO_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("USUARIO_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
