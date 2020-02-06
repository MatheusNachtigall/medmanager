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
/// Classe de acesso ao banco de dados da tabela DEDUCAO_TIPO
/// </summary>
namespace CRM_Blue.ADO
{
    public class DEDUCAO_TIPO_ADO : BaseADO
    {
        public DEDUCAO_TIPO_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela DEDUCAO_TIPO
        /// </summary>
        public DEDUCAO_TIPO Carregar(DEDUCAO_TIPO filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_TIPO_CARREGAR");

            if (filtro != null)
            {
                if (filtro.DEDUCAO_TIPO_ID != null)
                {
                    db.AddInParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, filtro.DEDUCAO_TIPO_ID);
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
                DEDUCAO_TIPO item = this.Popular(dataTable.Rows[0]);

                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela DEDUCAO_TIPO
        /// </summary>
        public Paginacao<DEDUCAO_TIPO> Listar(DEDUCAO_TIPO filtro, int? pagina, int? registrosPorPagina, DEDUCAO_TIPO_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<DEDUCAO_TIPO> registros = new Paginacao<DEDUCAO_TIPO>();
            List<DEDUCAO_TIPO> itens = new List<DEDUCAO_TIPO>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_TIPO_LISTAR");

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
                if (filtro.DEDUCAO_TIPO_ID != null)
                {
                    db.AddInParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, filtro.DEDUCAO_TIPO_ID);
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
                DEDUCAO_TIPO item = this.Popular(row);

                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela DEDUCAO_TIPO
        /// </summary>
        public DEDUCAO_TIPO Inserir(DEDUCAO_TIPO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_TIPO_INSERIR");
            db.AddOutParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, item.DEDUCAO_TIPO_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            item.DEDUCAO_TIPO_ID = (Int32)db.GetParameterValue(cmd, "@DEDUCAO_TIPO_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela DEDUCAO_TIPO
        /// </summary>
        public DEDUCAO_TIPO Atualizar(DEDUCAO_TIPO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_TIPO_ATUALIZAR");
            db.AddInParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, item.DEDUCAO_TIPO_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela DEDUCAO_TIPO
        /// </summary>
        public void Excluir(DEDUCAO_TIPO filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("DEDUCAO_TIPO_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.DEDUCAO_TIPO_ID != null)
                {
                    db.AddInParameter(cmd, "@DEDUCAO_TIPO_ID", DbType.Int32, filtro.DEDUCAO_TIPO_ID);
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
        public DEDUCAO_TIPO Popular(DataRow row)
        {
            DEDUCAO_TIPO reg = new DEDUCAO_TIPO();
            reg.DEDUCAO_TIPO_ID = ((row["DEDUCAO_TIPO_DEDUCAO_TIPO_ID"] != DBNull.Value)?Convert.ToInt32(row["DEDUCAO_TIPO_DEDUCAO_TIPO_ID"].ToString()):0);
            reg.NOME = ((row["DEDUCAO_TIPO_NOME"] != DBNull.Value)?row["DEDUCAO_TIPO_NOME"].ToString():String.Empty);
            return reg;
        }

    }
}
