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
/// Classe de acesso ao banco de dados da tabela USUARIO
/// </summary>
namespace CRM_Blue.ADO
{
    public class USUARIO_ADO : BaseADO
    {
        public USUARIO_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela USUARIO
        /// </summary>
        public USUARIO Carregar(USUARIO filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("USUARIO_CARREGAR");

            if (filtro != null)
            {
                if (filtro.USUARIO_ID != null)
                {
                    db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, filtro.USUARIO_ID);
                }
                if (!String.IsNullOrEmpty(filtro.NOME))
                {
                    db.AddInParameter(cmd, "@NOME", DbType.String, filtro.NOME);
                }
                if (!String.IsNullOrEmpty(filtro.EMAIL))
                {
                    db.AddInParameter(cmd, "@EMAIL", DbType.String, filtro.EMAIL);
                }
                if (!String.IsNullOrEmpty(filtro.SENHA))
                {
                    db.AddInParameter(cmd, "@SENHA", DbType.String, filtro.SENHA);
                }
            }
            DataTable dataTable = db.ExecuteDataTable(cmd);

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (dataTable.Rows.Count > 0)
            {
                USUARIO item = this.Popular(dataTable.Rows[0]);

                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela USUARIO
        /// </summary>
        public Paginacao<USUARIO> Listar(USUARIO filtro, int? pagina, int? registrosPorPagina, USUARIO_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<USUARIO> registros = new Paginacao<USUARIO>();
            List<USUARIO> itens = new List<USUARIO>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("USUARIO_LISTAR");

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
                if (filtro.USUARIO_ID != null)
                {
                    db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, filtro.USUARIO_ID);
                }
                if (!String.IsNullOrEmpty(filtro.NOME))
                {
                    db.AddInParameter(cmd, "@NOME", DbType.String, filtro.NOME);
                }
                if (!String.IsNullOrEmpty(filtro.EMAIL))
                {
                    db.AddInParameter(cmd, "@EMAIL", DbType.String, filtro.EMAIL);
                }
                if (!String.IsNullOrEmpty(filtro.SENHA))
                {
                    db.AddInParameter(cmd, "@SENHA", DbType.String, filtro.SENHA);
                }
            }

            DataTable dataTable = db.ExecuteDataTable(cmd);
            totalRegistros = (Int32)db.GetParameterValue(cmd, "@totalRegistros");

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            foreach (DataRow row in dataTable.Rows)
            {
                USUARIO item = this.Popular(row);

                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela USUARIO
        /// </summary>
        public USUARIO Inserir(USUARIO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("USUARIO_INSERIR");
            db.AddOutParameter(cmd, "@USUARIO_ID", DbType.Int32, item.USUARIO_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.AddInParameter(cmd, "@EMAIL", DbType.String, item.EMAIL);
            db.AddInParameter(cmd, "@SENHA", DbType.String, item.SENHA);
            db.ExecuteNonQuery(cmd);
            item.USUARIO_ID = (Int32)db.GetParameterValue(cmd, "@USUARIO_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela USUARIO
        /// </summary>
        public USUARIO Atualizar(USUARIO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("USUARIO_ATUALIZAR");
            db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, item.USUARIO_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.AddInParameter(cmd, "@EMAIL", DbType.String, item.EMAIL);
            db.AddInParameter(cmd, "@SENHA", DbType.String, item.SENHA);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela USUARIO
        /// </summary>
        public void Excluir(USUARIO filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("USUARIO_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.USUARIO_ID != null)
                {
                    db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, filtro.USUARIO_ID);
                }
                if (!String.IsNullOrEmpty(filtro.NOME))
                {
                    db.AddInParameter(cmd, "@NOME", DbType.String, filtro.NOME);
                }
                if (!String.IsNullOrEmpty(filtro.EMAIL))
                {
                    db.AddInParameter(cmd, "@EMAIL", DbType.String, filtro.EMAIL);
                }
                if (!String.IsNullOrEmpty(filtro.SENHA))
                {
                    db.AddInParameter(cmd, "@SENHA", DbType.String, filtro.SENHA);
                }
			}
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        /// <summary>
        /// Popula os dados
        /// </summary>
        public USUARIO Popular(DataRow row)
        {
            USUARIO reg = new USUARIO();
            reg.USUARIO_ID = ((row["USUARIO_USUARIO_ID"] != DBNull.Value)?Convert.ToInt32(row["USUARIO_USUARIO_ID"].ToString()):0);
            reg.NOME = ((row["USUARIO_NOME"] != DBNull.Value)?row["USUARIO_NOME"].ToString():String.Empty);
            reg.EMAIL = ((row["USUARIO_EMAIL"] != DBNull.Value)?row["USUARIO_EMAIL"].ToString():String.Empty);
            reg.SENHA = ((row["USUARIO_SENHA"] != DBNull.Value)?row["USUARIO_SENHA"].ToString():String.Empty);
            return reg;
        }

    }
}
