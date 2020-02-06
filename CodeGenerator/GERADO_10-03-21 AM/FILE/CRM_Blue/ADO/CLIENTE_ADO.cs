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
/// Classe de acesso ao banco de dados da tabela CLIENTE
/// </summary>
namespace CRM_Blue.ADO
{
    public class CLIENTE_ADO : BaseADO
    {
        public CLIENTE_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela CLIENTE
        /// </summary>
        public CLIENTE Carregar(CLIENTE filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("CLIENTE_CARREGAR");

            if (filtro != null)
            {
                if (filtro.CLIENTE_ID != null)
                {
                    db.AddInParameter(cmd, "@CLIENTE_ID", DbType.Int32, filtro.CLIENTE_ID);
                }
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
                CLIENTE item = this.Popular(dataTable.Rows[0]);
                item.AGENCIA = new AGENCIA_ADO().Popular(dataTable.Rows[0]);
                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela CLIENTE
        /// </summary>
        public Paginacao<CLIENTE> Listar(CLIENTE filtro, int? pagina, int? registrosPorPagina, CLIENTE_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<CLIENTE> registros = new Paginacao<CLIENTE>();
            List<CLIENTE> itens = new List<CLIENTE>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("CLIENTE_LISTAR");

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
                if (filtro.CLIENTE_ID != null)
                {
                    db.AddInParameter(cmd, "@CLIENTE_ID", DbType.Int32, filtro.CLIENTE_ID);
                }
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
                CLIENTE item = this.Popular(row);
                item.AGENCIA = new AGENCIA_ADO().Popular(row);
                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela CLIENTE
        /// </summary>
        public CLIENTE Inserir(CLIENTE item)
        {
			DbCommand cmd = db.GetStoredProcCommand("CLIENTE_INSERIR");
            db.AddOutParameter(cmd, "@CLIENTE_ID", DbType.Int32, item.CLIENTE_ID);
            db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, item.AGENCIA_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            item.CLIENTE_ID = (Int32)db.GetParameterValue(cmd, "@CLIENTE_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela CLIENTE
        /// </summary>
        public CLIENTE Atualizar(CLIENTE item)
        {
			DbCommand cmd = db.GetStoredProcCommand("CLIENTE_ATUALIZAR");
            db.AddInParameter(cmd, "@CLIENTE_ID", DbType.Int32, item.CLIENTE_ID);
            db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, item.AGENCIA_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela CLIENTE
        /// </summary>
        public void Excluir(CLIENTE filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("CLIENTE_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.CLIENTE_ID != null)
                {
                    db.AddInParameter(cmd, "@CLIENTE_ID", DbType.Int32, filtro.CLIENTE_ID);
                }
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
        public CLIENTE Popular(DataRow row)
        {
            CLIENTE reg = new CLIENTE();
            reg.CLIENTE_ID = ((row["CLIENTE_CLIENTE_ID"] != DBNull.Value)?Convert.ToInt32(row["CLIENTE_CLIENTE_ID"].ToString()):0);
            reg.AGENCIA_ID = ((row["CLIENTE_AGENCIA_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["CLIENTE_AGENCIA_ID"].ToString()):null);
            reg.NOME = ((row["CLIENTE_NOME"] != DBNull.Value)?row["CLIENTE_NOME"].ToString():String.Empty);
            return reg;
        }

    }
}
