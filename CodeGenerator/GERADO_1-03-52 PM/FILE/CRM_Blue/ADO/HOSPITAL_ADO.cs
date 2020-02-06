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
/// Classe de acesso ao banco de dados da tabela HOSPITAL
/// </summary>
namespace CRM_Blue.ADO
{
    public class HOSPITAL_ADO : BaseADO
    {
        public HOSPITAL_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela HOSPITAL
        /// </summary>
        public HOSPITAL Carregar(HOSPITAL filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("HOSPITAL_CARREGAR");

            if (filtro != null)
            {
                if (filtro.HOSPITAL_ID != null)
                {
                    db.AddInParameter(cmd, "@HOSPITAL_ID", DbType.Int32, filtro.HOSPITAL_ID);
                }
                if (filtro.CIDADE_ID != null)
                {
                    db.AddInParameter(cmd, "@CIDADE_ID", DbType.Int32, filtro.CIDADE_ID);
                }
                if (!String.IsNullOrEmpty(filtro.NOME))
                {
                    db.AddInParameter(cmd, "@NOME", DbType.String, filtro.NOME);
                }
            }
            DataTable dataTable = db.ExecuteDataTable(cmd);

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (dataTable.Rows.Count > 0)
            {
                HOSPITAL item = this.Popular(dataTable.Rows[0]);
                item.CIDADE = new CIDADE_ADO().Popular(dataTable.Rows[0]);
                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela HOSPITAL
        /// </summary>
        public Paginacao<HOSPITAL> Listar(HOSPITAL filtro, int? pagina, int? registrosPorPagina, HOSPITAL_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<HOSPITAL> registros = new Paginacao<HOSPITAL>();
            List<HOSPITAL> itens = new List<HOSPITAL>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("HOSPITAL_LISTAR");

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
                if (filtro.HOSPITAL_ID != null)
                {
                    db.AddInParameter(cmd, "@HOSPITAL_ID", DbType.Int32, filtro.HOSPITAL_ID);
                }
                if (filtro.CIDADE_ID != null)
                {
                    db.AddInParameter(cmd, "@CIDADE_ID", DbType.Int32, filtro.CIDADE_ID);
                }
                if (!String.IsNullOrEmpty(filtro.NOME))
                {
                    db.AddInParameter(cmd, "@NOME", DbType.String, filtro.NOME);
                }
            }

            DataTable dataTable = db.ExecuteDataTable(cmd);
            totalRegistros = (Int32)db.GetParameterValue(cmd, "@totalRegistros");

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            foreach (DataRow row in dataTable.Rows)
            {
                HOSPITAL item = this.Popular(row);
                item.CIDADE = new CIDADE_ADO().Popular(row);
                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela HOSPITAL
        /// </summary>
        public HOSPITAL Inserir(HOSPITAL item)
        {
			DbCommand cmd = db.GetStoredProcCommand("HOSPITAL_INSERIR");
            db.AddOutParameter(cmd, "@HOSPITAL_ID", DbType.Int32, item.HOSPITAL_ID);
            db.AddInParameter(cmd, "@CIDADE_ID", DbType.Int32, item.CIDADE_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            item.HOSPITAL_ID = (Int32)db.GetParameterValue(cmd, "@HOSPITAL_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela HOSPITAL
        /// </summary>
        public HOSPITAL Atualizar(HOSPITAL item)
        {
			DbCommand cmd = db.GetStoredProcCommand("HOSPITAL_ATUALIZAR");
            db.AddInParameter(cmd, "@HOSPITAL_ID", DbType.Int32, item.HOSPITAL_ID);
            db.AddInParameter(cmd, "@CIDADE_ID", DbType.Int32, item.CIDADE_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela HOSPITAL
        /// </summary>
        public void Excluir(HOSPITAL filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("HOSPITAL_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.HOSPITAL_ID != null)
                {
                    db.AddInParameter(cmd, "@HOSPITAL_ID", DbType.Int32, filtro.HOSPITAL_ID);
                }
                if (filtro.CIDADE_ID != null)
                {
                    db.AddInParameter(cmd, "@CIDADE_ID", DbType.Int32, filtro.CIDADE_ID);
                }
                if (!String.IsNullOrEmpty(filtro.NOME))
                {
                    db.AddInParameter(cmd, "@NOME", DbType.String, filtro.NOME);
                }
			}
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        /// <summary>
        /// Popula os dados
        /// </summary>
        public HOSPITAL Popular(DataRow row)
        {
            HOSPITAL reg = new HOSPITAL();
            reg.HOSPITAL_ID = ((row["HOSPITAL_HOSPITAL_ID"] != DBNull.Value)?Convert.ToInt32(row["HOSPITAL_HOSPITAL_ID"].ToString()):0);
            reg.CIDADE_ID = ((row["HOSPITAL_CIDADE_ID"] != DBNull.Value)?Convert.ToInt32(row["HOSPITAL_CIDADE_ID"].ToString()):0);
            reg.NOME = ((row["HOSPITAL_NOME"] != DBNull.Value)?row["HOSPITAL_NOME"].ToString():String.Empty);
            return reg;
        }

    }
}
