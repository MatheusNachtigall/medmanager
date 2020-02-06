using System;
using System.Collections.Generic;
using System.Web;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;

/// <summary>
/// Camada de Servico da tabela PLANTAO
/// </summary>
namespace CRM_Blue.Service
{
    public class PLANTAO_Service
    {
        protected PLANTAO_ADO _PLANTAO_ADO;

        public PLANTAO_Service()
        {
            this._PLANTAO_ADO = new PLANTAO_ADO();
        }

        /// <summary>
        /// Carrega um registro da tabela PLANTAO
        /// </summary>
        public PLANTAO Carregar(PLANTAO filtro)
        {
            try
            {
                return this._PLANTAO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Carregar(filtro);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Carrega um registro da tabela PLANTAO pelo ID
        /// </summary>
        public PLANTAO Carregar(Int32 id)
        {
            try
            {
				PLANTAO filtro = new PLANTAO();
                filtro.PLANTAO_ID = id;
                return this._PLANTAO_ADO.Carregar(filtro);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Carregar(id);", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Lista registros da tabela PLANTAO
        /// </summary>
        public Paginacao<PLANTAO> Listar(PLANTAO filtro, int pagina, int registrosPorPagina, PLANTAO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._PLANTAO_ADO.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<PLANTAO> Listar(PLANTAO filtro, int pagina, int registrosPorPagina)
        {
            try
            {
                return this._PLANTAO_ADO.Listar(filtro, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Listar(filtro, pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<PLANTAO> Listar(PLANTAO filtro, PLANTAO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._PLANTAO_ADO.Listar(filtro, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Listar(filtro, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public Paginacao<PLANTAO> Listar(int pagina, int registrosPorPagina, PLANTAO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._PLANTAO_ADO.Listar(null, pagina, registrosPorPagina, ordem, ordemTipo);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Listar(pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<PLANTAO> Listar(PLANTAO filtro)
        {
            try
            {
                return this._PLANTAO_ADO.Listar(filtro, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Listar(filtro);", ex);
                throw ex;
            }
        }

        public Paginacao<PLANTAO> Listar(int pagina, int registrosPorPagina)
        {
            try
            {
                return this._PLANTAO_ADO.Listar(null, pagina, registrosPorPagina, null, null);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Listar(pagina, registrosPorPagina);", ex);
                throw ex;
            }
        }

        public List<PLANTAO> Listar(PLANTAO_Ordem ordem, OrdemTipo ordemTipo)
        {
            try
            {
                return this._PLANTAO_ADO.Listar(null, null, null, ordem, ordemTipo).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Listar(ordem, ordemTipo);", ex);
                throw ex;
            }
        }

        public List<PLANTAO> Listar()
        {
            try
            {
                return this._PLANTAO_ADO.Listar(null, null, null, null, null).Itens;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Listar();", ex);
                throw ex;
            }
        }

		public PLANTAO Inserir(PLANTAO item)
        {
            try
            {
                return this._PLANTAO_ADO.Inserir(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Inserir(item);", ex);
                throw ex;
            }
        }

		public PLANTAO Atualizar(PLANTAO item)
        {
            try
            {
                return this._PLANTAO_ADO.Atualizar(item);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Atualizar(item);", ex);
                throw ex;
            }
        }

		public Boolean Excluir(PLANTAO filtro)
        {
            try
            {
                this._PLANTAO_ADO.Excluir(filtro);
				return true;
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service.Excluir(item);", ex);
                throw ex;
            }
        }
    }
}
