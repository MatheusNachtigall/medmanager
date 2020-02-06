using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Text;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;
using CRM_Blue.Service;
using Utilities;

/// <summary>
/// Classe de acesso ao banco de dados da tabela DEDUCAO
/// </summary>
namespace CRM_Blue.ADO
{
    public class DEDUCAO_ADO : BaseADO
    {
        public DEDUCAO_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela DEDUCAO
        /// </summary>
        public DEDUCAO Carregar(DEDUCAO filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_CARREGAR");

            if (filtro != null)
            {
                if (filtro.DEDUCAO_ID != null)
                {
                    db.AddInParameter(cmd, "@DEDUCAO_ID", DbType.Int32, filtro.DEDUCAO_ID);
                }
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.DEDUCAO_TIPO_ID != null)
                {
                    db.AddInParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, filtro.DEDUCAO_TIPO_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.DATA_DEDUCAO != null)
                {
                    db.AddInParameter(cmd, "@DATA_DEDUCAO", DbType.DateTime, filtro.DATA_DEDUCAO);
                }
            }
            DataTable dataTable = db.ExecuteDataTable(cmd);

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (dataTable.Rows.Count > 0)
            {
                DEDUCAO item = this.Popular(dataTable.Rows[0]);
                item.PLANTAO = new PLANTAO_ADO().Popular(dataTable.Rows[0]);
                item.DEDUCAO_TIPO = new DEDUCAO_TIPO_ADO().Popular(dataTable.Rows[0]);
                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela DEDUCAO
        /// </summary>
        public Paginacao<DEDUCAO> Listar(DEDUCAO filtro, int? pagina, int? registrosPorPagina, DEDUCAO_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<DEDUCAO> registros = new Paginacao<DEDUCAO>();
            List<DEDUCAO> itens = new List<DEDUCAO>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_LISTAR");

            if (pagina != null)
            {
                db.AddInParameter(cmd, "@pagina", DbType.Int32, pagina);
            }
            if (registrosPorPagina != null)
            {
                db.AddInParameter(cmd, "@registrosPorPagina", DbType.Int32, registrosPorPagina);
            }
            if (ordem != null)
            {
                db.AddInParameter(cmd, "@ordemCampo", DbType.String, Enumerators.GetDescription(ordem));
            }
            if (ordemTipo != null)
            {
                db.AddInParameter(cmd, "@ordemTipo", DbType.String, ((ordemTipo == OrdemTipo.Ascendente)?"ASC":"DESC"));
            }
            db.AddOutParameter(cmd, "@totalRegistros", DbType.Int32, totalRegistros);

            if (filtro != null)
            {
                if (filtro.DEDUCAO_ID != null)
                {
                    db.AddInParameter(cmd, "@DEDUCAO_ID", DbType.Int32, filtro.DEDUCAO_ID);
                }
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.DEDUCAO_TIPO_ID != null)
                {
                    db.AddInParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, filtro.DEDUCAO_TIPO_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.DATA_DEDUCAO != null)
                {
                    db.AddInParameter(cmd, "@DATA_DEDUCAO", DbType.DateTime, filtro.DATA_DEDUCAO);
                }
            }

            DataTable dataTable = db.ExecuteDataTable(cmd);
            totalRegistros = (Int32)db.GetParameterValue(cmd, "@totalRegistros");

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            foreach (DataRow row in dataTable.Rows)
            {
                DEDUCAO item = this.Popular(row);
                item.PLANTAO = new PLANTAO_ADO().Popular(row);
                item.DEDUCAO_TIPO = new DEDUCAO_TIPO_ADO().Popular(row);
                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela DEDUCAO
        /// </summary>
        public DEDUCAO Inserir(DEDUCAO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_INSERIR");
            db.AddOutParameter(cmd, "@DEDUCAO_ID", DbType.Int32, item.DEDUCAO_ID);
            db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, item.PLANTAO_ID);
            db.AddInParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, item.DEDUCAO_TIPO_ID);
            db.AddInParameter(cmd, "@VALOR", DbType.Decimal, item.VALOR);
            db.AddInParameter(cmd, "@DATA_DEDUCAO", DbType.DateTime, item.DATA_DEDUCAO);
            db.ExecuteNonQuery(cmd);
            item.DEDUCAO_ID = (Int32)db.GetParameterValue(cmd, "@DEDUCAO_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela DEDUCAO
        /// </summary>
        public DEDUCAO Atualizar(DEDUCAO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_ATUALIZAR");
            db.AddInParameter(cmd, "@DEDUCAO_ID", DbType.Int32, item.DEDUCAO_ID);
            db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, item.PLANTAO_ID);
            db.AddInParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, item.DEDUCAO_TIPO_ID);
            db.AddInParameter(cmd, "@VALOR", DbType.Decimal, item.VALOR);
            db.AddInParameter(cmd, "@DATA_DEDUCAO", DbType.DateTime, item.DATA_DEDUCAO);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela DEDUCAO
        /// </summary>
        public void Excluir(DEDUCAO filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.DEDUCAO_ID != null)
                {
                    db.AddInParameter(cmd, "@DEDUCAO_ID", DbType.Int32, filtro.DEDUCAO_ID);
                }
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.DEDUCAO_TIPO_ID != null)
                {
                    db.AddInParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, filtro.DEDUCAO_TIPO_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.DATA_DEDUCAO != null)
                {
                    db.AddInParameter(cmd, "@DATA_DEDUCAO", DbType.DateTime, filtro.DATA_DEDUCAO);
                }
			}
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        /// <summary>
        /// Popula os dados
        /// </summary>
        public DEDUCAO Popular(DataRow row)
        {
            DEDUCAO reg = new DEDUCAO();
            reg.DEDUCAO_ID = ((row["DEDUCAO_DEDUCAO_ID"] != DBNull.Value)?Convert.ToInt32(row["DEDUCAO_DEDUCAO_ID"].ToString()):0);
            reg.PLANTAO_ID = ((row["DEDUCAO_PLANTAO_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["DEDUCAO_PLANTAO_ID"].ToString()):null);
            reg.DEDUCAO_TIPO_ID = ((row["DEDUCAO_DEDUCAO_TIPO_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["DEDUCAO_DEDUCAO_TIPO_ID"].ToString()):null);
            reg.VALOR = ((row["DEDUCAO_VALOR"] != DBNull.Value)?Convert.ToDecimal(row["DEDUCAO_VALOR"].ToString()):0);
            reg.DATA_DEDUCAO = ((row["DEDUCAO_DATA_DEDUCAO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["DEDUCAO_DATA_DEDUCAO"].ToString()):null);
            return reg;
        }

    }
}
