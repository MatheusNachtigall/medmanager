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
    public class PLANTAO_Service_EXT
    {
        protected PLANTAO_ADO_EXT _PLANTAO_ADO_EXT;

        public PLANTAO_Service_EXT()
        {
            this._PLANTAO_ADO_EXT = new PLANTAO_ADO_EXT();
        }

        /// <summary>
        /// Lista registros da tabela PLANTAO
        /// </summary>
        public Paginacao<PLANTAO> Listar(PLANTAO filtro, int pagina, int registrosPorPagina, PLANTAO_Ordem ordem, OrdemTipo ordemTipo, DateTime DATA_FIM)
        {
            try
            {
                return this._PLANTAO_ADO_EXT.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo, DATA_FIM);
            }
            catch (Exception ex)
            {
                LogError.GravarErro("PLANTAO_Service_EXT.Listar(filtro, pagina, registrosPorPagina, ordem, ordemTipo);", ex);
                throw ex;
            }
        }

		public List<PLANTAO> Listar(PLANTAO filtro, PLANTAO_Ordem ordem, OrdemTipo ordemTipo, DateTime DATA_FIM)
		{
			try
			{
				return this._PLANTAO_ADO_EXT.Listar(filtro, null, null, ordem, ordemTipo, DATA_FIM).Itens;
			}
			catch (Exception ex)
			{
				LogError.GravarErro("PLANTAO_Service.Listar(filtro, ordem, ordemTipo);", ex);
				throw ex;
			}
		}

		public List<PLANTAO> Listar(PLANTAO filtro, DateTime DATA_FIM)
		{
			try
			{
				return this._PLANTAO_ADO_EXT.Listar(filtro, null, null, null, null, DATA_FIM).Itens;
			}
			catch (Exception ex)
			{
				LogError.GravarErro("PLANTAO_Service.Listar(filtro);", ex);
				throw ex;
			}
		}
	}
}
