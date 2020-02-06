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
/// Classe de acesso ao banco de dados da tabela CIDADE
/// </summary>
namespace CRM_Blue.ADO
{
    public class CIDADE_ADO : BaseADO
    {
        public CIDADE_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela CIDADE
        /// </summary>
        public CIDADE Carregar(CIDADE filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("CIDADE_CARREGAR");

            if (filtro != null)
            {
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
                CIDADE item = this.Popular(dataTable.Rows[0]);

                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela CIDADE
        /// </summary>
        public Paginacao<CIDADE> Listar(CIDADE filtro, int? pagina, int? registrosPorPagina, CIDADE_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<CIDADE> registros = new Paginacao<CIDADE>();
            List<CIDADE> itens = new List<CIDADE>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("CIDADE_LISTAR");

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
                CIDADE item = this.Popular(row);

                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela CIDADE
        /// </summary>
        public CIDADE Inserir(CIDADE item)
        {
			DbCommand cmd = db.GetStoredProcCommand("CIDADE_INSERIR");
            db.AddOutParameter(cmd, "@CIDADE_ID", DbType.Int32, item.CIDADE_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            item.CIDADE_ID = (Int32)db.GetParameterValue(cmd, "@CIDADE_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela CIDADE
        /// </summary>
        public CIDADE Atualizar(CIDADE item)
        {
			DbCommand cmd = db.GetStoredProcCommand("CIDADE_ATUALIZAR");
            db.AddInParameter(cmd, "@CIDADE_ID", DbType.Int32, item.CIDADE_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela CIDADE
        /// </summary>
        public void Excluir(CIDADE filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("CIDADE_EXCLUIR");
            if (filtro != null)
            {
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
        public CIDADE Popular(DataRow row)
        {
            CIDADE reg = new CIDADE();
            reg.CIDADE_ID = ((row["CIDADE_CIDADE_ID"] != DBNull.Value)?Convert.ToInt32(row["CIDADE_CIDADE_ID"].ToString()):0);
            reg.NOME = ((row["CIDADE_NOME"] != DBNull.Value)?row["CIDADE_NOME"].ToString():String.Empty);
            return reg;
        }

    }
}
