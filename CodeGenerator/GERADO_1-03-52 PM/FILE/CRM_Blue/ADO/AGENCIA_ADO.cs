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
/// Classe de acesso ao banco de dados da tabela AGENCIA
/// </summary>
namespace CRM_Blue.ADO
{
    public class AGENCIA_ADO : BaseADO
    {
        public AGENCIA_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela AGENCIA
        /// </summary>
        public AGENCIA Carregar(AGENCIA filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("AGENCIA_CARREGAR");

            if (filtro != null)
            {
                if (filtro.AGENCIA_ID != null)
                {
                    db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, filtro.AGENCIA_ID);
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
                AGENCIA item = this.Popular(dataTable.Rows[0]);

                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela AGENCIA
        /// </summary>
        public Paginacao<AGENCIA> Listar(AGENCIA filtro, int? pagina, int? registrosPorPagina, AGENCIA_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<AGENCIA> registros = new Paginacao<AGENCIA>();
            List<AGENCIA> itens = new List<AGENCIA>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("AGENCIA_LISTAR");

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
                if (filtro.AGENCIA_ID != null)
                {
                    db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, filtro.AGENCIA_ID);
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
                AGENCIA item = this.Popular(row);

                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela AGENCIA
        /// </summary>
        public AGENCIA Inserir(AGENCIA item)
        {
			DbCommand cmd = db.GetStoredProcCommand("AGENCIA_INSERIR");
            db.AddOutParameter(cmd, "@AGENCIA_ID", DbType.Int32, item.AGENCIA_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            item.AGENCIA_ID = (Int32)db.GetParameterValue(cmd, "@AGENCIA_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela AGENCIA
        /// </summary>
        public AGENCIA Atualizar(AGENCIA item)
        {
			DbCommand cmd = db.GetStoredProcCommand("AGENCIA_ATUALIZAR");
            db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, item.AGENCIA_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela AGENCIA
        /// </summary>
        public void Excluir(AGENCIA filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("AGENCIA_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.AGENCIA_ID != null)
                {
                    db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, filtro.AGENCIA_ID);
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
        public AGENCIA Popular(DataRow row)
        {
            AGENCIA reg = new AGENCIA();
            reg.AGENCIA_ID = ((row["AGENCIA_AGENCIA_ID"] != DBNull.Value)?Convert.ToInt32(row["AGENCIA_AGENCIA_ID"].ToString()):0);
            reg.NOME = ((row["AGENCIA_NOME"] != DBNull.Value)?row["AGENCIA_NOME"].ToString():String.Empty);
            return reg;
        }

    }
}
