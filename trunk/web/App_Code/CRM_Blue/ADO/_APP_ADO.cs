using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace CRM_Blue.ADO
{
	public class _APP_ADO : BaseADO
	{
		public _APP_ADO()
		{
		}

		public List<ListaRelatorio> GET_RELATORIO_PAGAMENTO_DATA(RelatorioFiltro filtro)
		{
			try
			{
				DbCommand cmd = db.GetStoredProcCommand("_GET_RELATORIO_PAGAMENTO_DATA");
				db.AddInParameter(cmd, "@DATA_INI", DbType.DateTime, filtro.DATA_INI);
				db.AddInParameter(cmd, "@DATA_FIM", DbType.DateTime, filtro.DATA_FIM);
				DataTable dataTable = db.ExecuteDataTable(cmd);
				cmd.Connection.Close();
				cmd.Connection.Dispose();
				List<ListaRelatorio> retorno = new List<ListaRelatorio>();
				foreach (DataRow row in dataTable.Rows)
				{
					ListaRelatorio item = new ListaRelatorio();
					item.mes = ((row["MES"] != DBNull.Value) ? Convert.ToInt32(row["MES"].ToString()) : 0);
					item.ano = ((row["ANO"] != DBNull.Value) ? Convert.ToInt32(row["ANO"].ToString()) : 0);
					item.mes_nome = ((row["MES_NOME"] != DBNull.Value) ? row["MES_NOME"].ToString() : String.Empty);
					item.ano_mes_dia = ((row["ANO_MES_DIA"] != DBNull.Value) ? row["ANO_MES_DIA"].ToString() : String.Empty);
					item.mes_ano = ((row["MES_ANO"] != DBNull.Value) ? row["MES_ANO"].ToString() : String.Empty);
					item.valor_mes = ((row["VALOR_MES"] != DBNull.Value) ? Convert.ToDecimal(row["VALOR_MES"].ToString()) : 0);
					item.valor_mes_inss = ((row["VALOR_MES_INSS"] != DBNull.Value) ? Convert.ToDecimal(row["VALOR_MES_INSS"].ToString()) : 0);

					retorno.Add(item);
				}
				return retorno;
			}
			catch (Exception ex)
			{
				LogError.GravarErro("GET_RELATORIO_PAGAMENTO_DATA", ex);
				return new List<ListaRelatorio>();
			}
		}

		public List<ListaRelatorio> GET_RELATORIO_CONTRATACAO_DATA(RelatorioFiltro filtro)
		{
			try
			{
				DbCommand cmd = db.GetStoredProcCommand("_GET_RELATORIO_CONTRATACAO_DATA");
				db.AddInParameter(cmd, "@DATA_INI", DbType.DateTime, filtro.DATA_INI);
				db.AddInParameter(cmd, "@DATA_FIM", DbType.DateTime, filtro.DATA_FIM);
				DataTable dataTable = db.ExecuteDataTable(cmd);
				cmd.Connection.Close();
				cmd.Connection.Dispose();
				List<ListaRelatorio> retorno = new List<ListaRelatorio>();
				foreach (DataRow row in dataTable.Rows)
				{
					ListaRelatorio item = new ListaRelatorio();
					item.mes = ((row["MES"] != DBNull.Value) ? Convert.ToInt32(row["MES"].ToString()) : 0);
					item.ano = ((row["ANO"] != DBNull.Value) ? Convert.ToInt32(row["ANO"].ToString()) : 0);
					item.mes_nome = ((row["MES_NOME"] != DBNull.Value) ? row["MES_NOME"].ToString() : String.Empty);
					item.ano_mes_dia = ((row["ANO_MES_DIA"] != DBNull.Value) ? row["ANO_MES_DIA"].ToString() : String.Empty);
					item.mes_ano = ((row["MES_ANO"] != DBNull.Value) ? row["MES_ANO"].ToString() : String.Empty);
					item.valor_mes = ((row["VALOR_MES"] != DBNull.Value) ? Convert.ToDecimal(row["VALOR_MES"].ToString()) : 0);

					retorno.Add(item);
				}
				return retorno;
			}
			catch (Exception ex)
			{
				LogError.GravarErro("GET_RELATORIO_CONTRATACAO_DATA", ex);
				return new List<ListaRelatorio>();
			}
		}

		public List<ListaRelatorio> GET_RELATORIO_FATURAMENTO_DATA(RelatorioFiltro filtro)
		{
			try
			{
				DbCommand cmd = db.GetStoredProcCommand("_GET_RELATORIO_FATURAMENTO_DATA");
				db.AddInParameter(cmd, "@DATA_INI", DbType.DateTime, filtro.DATA_INI);
				db.AddInParameter(cmd, "@DATA_FIM", DbType.DateTime, filtro.DATA_FIM);
				DataTable dataTable = db.ExecuteDataTable(cmd);
				cmd.Connection.Close();
				cmd.Connection.Dispose();
				List<ListaRelatorio> retorno = new List<ListaRelatorio>();
				foreach (DataRow row in dataTable.Rows)
				{
					ListaRelatorio item = new ListaRelatorio();
					item.mes = ((row["MES"] != DBNull.Value) ? Convert.ToInt32(row["MES"].ToString()) : 0);
					item.ano = ((row["ANO"] != DBNull.Value) ? Convert.ToInt32(row["ANO"].ToString()) : 0);
					item.mes_nome = ((row["MES_NOME"] != DBNull.Value) ? row["MES_NOME"].ToString() : String.Empty);
					item.ano_mes_dia = ((row["ANO_MES_DIA"] != DBNull.Value) ? row["ANO_MES_DIA"].ToString() : String.Empty);
					item.mes_ano = ((row["MES_ANO"] != DBNull.Value) ? row["MES_ANO"].ToString() : String.Empty);
					item.valor_mes = ((row["VALOR_MES"] != DBNull.Value) ? Convert.ToDecimal(row["VALOR_MES"].ToString()) : 0);

					retorno.Add(item);
				}
				return retorno;
			}
			catch (Exception ex)
			{
				LogError.GravarErro("GET_RELATORIO_FATURAMENTO_DATA", ex);
				return new List<ListaRelatorio>();
			}
		}

		public List<ListaRelatorio> GET_RELATORIO_RECEBIMENTO_DATA(RelatorioFiltro filtro)
		{
			try
			{
				DbCommand cmd = db.GetStoredProcCommand("_GET_RELATORIO_RECEBIMENTO_DATA");
				db.AddInParameter(cmd, "@DATA_INI", DbType.DateTime, filtro.DATA_INI);
				db.AddInParameter(cmd, "@DATA_FIM", DbType.DateTime, filtro.DATA_FIM);
				DataTable dataTable = db.ExecuteDataTable(cmd);
				cmd.Connection.Close();
				cmd.Connection.Dispose();
				List<ListaRelatorio> retorno = new List<ListaRelatorio>();
				foreach (DataRow row in dataTable.Rows)
				{
					ListaRelatorio item = new ListaRelatorio();
					item.mes = ((row["MES"] != DBNull.Value) ? Convert.ToInt32(row["MES"].ToString()) : 0);
					item.ano = ((row["ANO"] != DBNull.Value) ? Convert.ToInt32(row["ANO"].ToString()) : 0);
					item.mes_nome = ((row["MES_NOME"] != DBNull.Value) ? row["MES_NOME"].ToString() : String.Empty);
					item.ano_mes_dia = ((row["ANO_MES_DIA"] != DBNull.Value) ? row["ANO_MES_DIA"].ToString() : String.Empty);
					item.mes_ano = ((row["MES_ANO"] != DBNull.Value) ? row["MES_ANO"].ToString() : String.Empty);
					item.valor_mes = ((row["VALOR_MES"] != DBNull.Value) ? Convert.ToDecimal(row["VALOR_MES"].ToString()) : 0);

					retorno.Add(item);
				}
				return retorno;
			}
			catch (Exception ex)
			{
				LogError.GravarErro("GET_RELATORIO_RECEBIMENTO_DATA", ex);
				return new List<ListaRelatorio>();
			}
		}

	}
}