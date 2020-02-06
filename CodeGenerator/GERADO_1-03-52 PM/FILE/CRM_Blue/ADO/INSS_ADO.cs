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
/// Classe de acesso ao banco de dados da tabela INSS
/// </summary>
namespace CRM_Blue.ADO
{
    public class INSS_ADO : BaseADO
    {
        public INSS_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela INSS
        /// </summary>
        public INSS Carregar(INSS filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("INSS_CARREGAR");

            if (filtro != null)
            {
                if (filtro.INSS_ID != null)
                {
                    db.AddInParameter(cmd, "@INSS_ID", DbType.Int32, filtro.INSS_ID);
                }
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.DATA_INSS != null)
                {
                    db.AddInParameter(cmd, "@DATA_INSS", DbType.DateTime, filtro.DATA_INSS);
                }
                if (filtro.DATA_CADASTRO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, filtro.DATA_CADASTRO);
                }
            }
            DataTable dataTable = db.ExecuteDataTable(cmd);

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (dataTable.Rows.Count > 0)
            {
                INSS item = this.Popular(dataTable.Rows[0]);
                item.PLANTAO = new PLANTAO_ADO().Popular(dataTable.Rows[0]);
                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela INSS
        /// </summary>
        public Paginacao<INSS> Listar(INSS filtro, int? pagina, int? registrosPorPagina, INSS_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<INSS> registros = new Paginacao<INSS>();
            List<INSS> itens = new List<INSS>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("INSS_LISTAR");

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
                if (filtro.INSS_ID != null)
                {
                    db.AddInParameter(cmd, "@INSS_ID", DbType.Int32, filtro.INSS_ID);
                }
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.DATA_INSS != null)
                {
                    db.AddInParameter(cmd, "@DATA_INSS", DbType.DateTime, filtro.DATA_INSS);
                }
                if (filtro.DATA_CADASTRO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, filtro.DATA_CADASTRO);
                }
            }

            DataTable dataTable = db.ExecuteDataTable(cmd);
            totalRegistros = (Int32)db.GetParameterValue(cmd, "@totalRegistros");

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            foreach (DataRow row in dataTable.Rows)
            {
                INSS item = this.Popular(row);
                item.PLANTAO = new PLANTAO_ADO().Popular(row);
                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela INSS
        /// </summary>
        public INSS Inserir(INSS item)
        {
			DbCommand cmd = db.GetStoredProcCommand("INSS_INSERIR");
            db.AddOutParameter(cmd, "@INSS_ID", DbType.Int32, item.INSS_ID);
            db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, item.PLANTAO_ID);
            db.AddInParameter(cmd, "@VALOR", DbType.Decimal, item.VALOR);
            db.AddInParameter(cmd, "@DATA_INSS", DbType.DateTime, item.DATA_INSS);
            db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, item.DATA_CADASTRO);
            db.ExecuteNonQuery(cmd);
            item.INSS_ID = (Int32)db.GetParameterValue(cmd, "@INSS_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela INSS
        /// </summary>
        public INSS Atualizar(INSS item)
        {
			DbCommand cmd = db.GetStoredProcCommand("INSS_ATUALIZAR");
            db.AddInParameter(cmd, "@INSS_ID", DbType.Int32, item.INSS_ID);
            db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, item.PLANTAO_ID);
            db.AddInParameter(cmd, "@VALOR", DbType.Decimal, item.VALOR);
            db.AddInParameter(cmd, "@DATA_INSS", DbType.DateTime, item.DATA_INSS);
            db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, item.DATA_CADASTRO);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela INSS
        /// </summary>
        public void Excluir(INSS filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("INSS_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.INSS_ID != null)
                {
                    db.AddInParameter(cmd, "@INSS_ID", DbType.Int32, filtro.INSS_ID);
                }
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.DATA_INSS != null)
                {
                    db.AddInParameter(cmd, "@DATA_INSS", DbType.DateTime, filtro.DATA_INSS);
                }
                if (filtro.DATA_CADASTRO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, filtro.DATA_CADASTRO);
                }
			}
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        /// <summary>
        /// Popula os dados
        /// </summary>
        public INSS Popular(DataRow row)
        {
            INSS reg = new INSS();
            reg.INSS_ID = ((row["INSS_INSS_ID"] != DBNull.Value)?Convert.ToInt32(row["INSS_INSS_ID"].ToString()):0);
            reg.PLANTAO_ID = ((row["INSS_PLANTAO_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["INSS_PLANTAO_ID"].ToString()):null);
            reg.VALOR = ((row["INSS_VALOR"] != DBNull.Value)?(decimal?)Convert.ToDecimal(row["INSS_VALOR"].ToString()):null);
            reg.DATA_INSS = ((row["INSS_DATA_INSS"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["INSS_DATA_INSS"].ToString()):null);
            reg.DATA_CADASTRO = ((row["INSS_DATA_CADASTRO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["INSS_DATA_CADASTRO"].ToString()):null);
            return reg;
        }

    }
}
