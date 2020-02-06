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
/// Classe de acesso ao banco de dados da tabela FATURAMENTO
/// </summary>
namespace CRM_Blue.ADO
{
    public class FATURAMENTO_ADO : BaseADO
    {
        public FATURAMENTO_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela FATURAMENTO
        /// </summary>
        public FATURAMENTO Carregar(FATURAMENTO filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("FATURAMENTO_CARREGAR");

            if (filtro != null)
            {
                if (filtro.FATURAMENTO_ID != null)
                {
                    db.AddInParameter(cmd, "@FATURAMENTO_ID", DbType.Int32, filtro.FATURAMENTO_ID);
                }
                if (filtro.USUARIO_ID != null)
                {
                    db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, filtro.USUARIO_ID);
                }
                if (filtro.PROJETO_ID != null)
                {
                    db.AddInParameter(cmd, "@PROJETO_ID", DbType.Int32, filtro.PROJETO_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.STATUS != null)
                {
                    db.AddInParameter(cmd, "@STATUS", DbType.Int32, filtro.STATUS);
                }
                if (filtro.DATA_FATURAMENTO != null)
                {
                    db.AddInParameter(cmd, "@DATA_FATURAMENTO", DbType.DateTime, filtro.DATA_FATURAMENTO);
                }
                if (filtro.DATA_RECEBIMENTO != null)
                {
                    db.AddInParameter(cmd, "@DATA_RECEBIMENTO", DbType.DateTime, filtro.DATA_RECEBIMENTO);
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
                FATURAMENTO item = this.Popular(dataTable.Rows[0]);
                item.USUARIO = new USUARIO_ADO().Popular(dataTable.Rows[0]);
                item.PROJETO = new PROJETO_ADO().Popular(dataTable.Rows[0]);
                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela FATURAMENTO
        /// </summary>
        public Paginacao<FATURAMENTO> Listar(FATURAMENTO filtro, int? pagina, int? registrosPorPagina, FATURAMENTO_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<FATURAMENTO> registros = new Paginacao<FATURAMENTO>();
            List<FATURAMENTO> itens = new List<FATURAMENTO>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("FATURAMENTO_LISTAR");

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
                if (filtro.FATURAMENTO_ID != null)
                {
                    db.AddInParameter(cmd, "@FATURAMENTO_ID", DbType.Int32, filtro.FATURAMENTO_ID);
                }
                if (filtro.USUARIO_ID != null)
                {
                    db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, filtro.USUARIO_ID);
                }
                if (filtro.PROJETO_ID != null)
                {
                    db.AddInParameter(cmd, "@PROJETO_ID", DbType.Int32, filtro.PROJETO_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.STATUS != null)
                {
                    db.AddInParameter(cmd, "@STATUS", DbType.Int32, filtro.STATUS);
                }
                if (filtro.DATA_FATURAMENTO != null)
                {
                    db.AddInParameter(cmd, "@DATA_FATURAMENTO", DbType.DateTime, filtro.DATA_FATURAMENTO);
                }
                if (filtro.DATA_RECEBIMENTO != null)
                {
                    db.AddInParameter(cmd, "@DATA_RECEBIMENTO", DbType.DateTime, filtro.DATA_RECEBIMENTO);
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
                FATURAMENTO item = this.Popular(row);
                item.USUARIO = new USUARIO_ADO().Popular(row);
                item.PROJETO = new PROJETO_ADO().Popular(row);
                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela FATURAMENTO
        /// </summary>
        public FATURAMENTO Inserir(FATURAMENTO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("FATURAMENTO_INSERIR");
            db.AddOutParameter(cmd, "@FATURAMENTO_ID", DbType.Int32, item.FATURAMENTO_ID);
            db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, item.USUARIO_ID);
            db.AddInParameter(cmd, "@PROJETO_ID", DbType.Int32, item.PROJETO_ID);
            db.AddInParameter(cmd, "@VALOR", DbType.Decimal, item.VALOR);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, item.STATUS);
            db.AddInParameter(cmd, "@DATA_FATURAMENTO", DbType.DateTime, item.DATA_FATURAMENTO);
            db.AddInParameter(cmd, "@DATA_RECEBIMENTO", DbType.DateTime, item.DATA_RECEBIMENTO);
            db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, item.DATA_CADASTRO);
            db.ExecuteNonQuery(cmd);
            item.FATURAMENTO_ID = (Int32)db.GetParameterValue(cmd, "@FATURAMENTO_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela FATURAMENTO
        /// </summary>
        public FATURAMENTO Atualizar(FATURAMENTO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("FATURAMENTO_ATUALIZAR");
            db.AddInParameter(cmd, "@FATURAMENTO_ID", DbType.Int32, item.FATURAMENTO_ID);
            db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, item.USUARIO_ID);
            db.AddInParameter(cmd, "@PROJETO_ID", DbType.Int32, item.PROJETO_ID);
            db.AddInParameter(cmd, "@VALOR", DbType.Decimal, item.VALOR);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, item.STATUS);
            db.AddInParameter(cmd, "@DATA_FATURAMENTO", DbType.DateTime, item.DATA_FATURAMENTO);
            db.AddInParameter(cmd, "@DATA_RECEBIMENTO", DbType.DateTime, item.DATA_RECEBIMENTO);
            db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, item.DATA_CADASTRO);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela FATURAMENTO
        /// </summary>
        public void Excluir(FATURAMENTO filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("FATURAMENTO_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.FATURAMENTO_ID != null)
                {
                    db.AddInParameter(cmd, "@FATURAMENTO_ID", DbType.Int32, filtro.FATURAMENTO_ID);
                }
                if (filtro.USUARIO_ID != null)
                {
                    db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, filtro.USUARIO_ID);
                }
                if (filtro.PROJETO_ID != null)
                {
                    db.AddInParameter(cmd, "@PROJETO_ID", DbType.Int32, filtro.PROJETO_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.STATUS != null)
                {
                    db.AddInParameter(cmd, "@STATUS", DbType.Int32, filtro.STATUS);
                }
                if (filtro.DATA_FATURAMENTO != null)
                {
                    db.AddInParameter(cmd, "@DATA_FATURAMENTO", DbType.DateTime, filtro.DATA_FATURAMENTO);
                }
                if (filtro.DATA_RECEBIMENTO != null)
                {
                    db.AddInParameter(cmd, "@DATA_RECEBIMENTO", DbType.DateTime, filtro.DATA_RECEBIMENTO);
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
        public FATURAMENTO Popular(DataRow row)
        {
            FATURAMENTO reg = new FATURAMENTO();
            reg.FATURAMENTO_ID = ((row["FATURAMENTO_FATURAMENTO_ID"] != DBNull.Value)?Convert.ToInt32(row["FATURAMENTO_FATURAMENTO_ID"].ToString()):0);
            reg.USUARIO_ID = ((row["FATURAMENTO_USUARIO_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["FATURAMENTO_USUARIO_ID"].ToString()):null);
            reg.PROJETO_ID = ((row["FATURAMENTO_PROJETO_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["FATURAMENTO_PROJETO_ID"].ToString()):null);
            reg.VALOR = ((row["FATURAMENTO_VALOR"] != DBNull.Value)?(decimal?)Convert.ToDecimal(row["FATURAMENTO_VALOR"].ToString()):null);
            reg.STATUS = ((row["FATURAMENTO_STATUS"] != DBNull.Value)?(int?)Convert.ToInt32(row["FATURAMENTO_STATUS"].ToString()):null);
            reg.DATA_FATURAMENTO = ((row["FATURAMENTO_DATA_FATURAMENTO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["FATURAMENTO_DATA_FATURAMENTO"].ToString()):null);
            reg.DATA_RECEBIMENTO = ((row["FATURAMENTO_DATA_RECEBIMENTO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["FATURAMENTO_DATA_RECEBIMENTO"].ToString()):null);
            reg.DATA_CADASTRO = ((row["FATURAMENTO_DATA_CADASTRO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["FATURAMENTO_DATA_CADASTRO"].ToString()):null);
            return reg;
        }

    }
}
