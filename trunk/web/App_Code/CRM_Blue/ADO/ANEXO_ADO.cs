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
/// Classe de acesso ao banco de dados da tabela ANEXO
/// </summary>
namespace CRM_Blue.ADO
{
    public class ANEXO_ADO : BaseADO
    {
        public ANEXO_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela ANEXO
        /// </summary>
        public ANEXO Carregar(ANEXO filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("ANEXO_CARREGAR");

            if (filtro != null)
            {
                if (filtro.ANEXO_ID != null)
                {
                    db.AddInParameter(cmd, "@ANEXO_ID", DbType.Int32, filtro.ANEXO_ID);
                }
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.TIPO != null)
                {
                    db.AddInParameter(cmd, "@TIPO", DbType.Int32, filtro.TIPO);
                }
                if (!String.IsNullOrEmpty(filtro.ARQUIVO))
                {
                    db.AddInParameter(cmd, "@ARQUIVO", DbType.String, filtro.ARQUIVO);
                }
                if (filtro.ORDEM != null)
                {
                    db.AddInParameter(cmd, "@ORDEM", DbType.Int32, filtro.ORDEM);
                }
            }
            DataTable dataTable = db.ExecuteDataTable(cmd);

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (dataTable.Rows.Count > 0)
            {
                ANEXO item = this.Popular(dataTable.Rows[0]);
                item.PLANTAO = new PLANTAO_ADO().Popular(dataTable.Rows[0]);
                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela ANEXO
        /// </summary>
        public Paginacao<ANEXO> Listar(ANEXO filtro, int? pagina, int? registrosPorPagina, ANEXO_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<ANEXO> registros = new Paginacao<ANEXO>();
            List<ANEXO> itens = new List<ANEXO>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("ANEXO_LISTAR");

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
                if (filtro.ANEXO_ID != null)
                {
                    db.AddInParameter(cmd, "@ANEXO_ID", DbType.Int32, filtro.ANEXO_ID);
                }
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.TIPO != null)
                {
                    db.AddInParameter(cmd, "@TIPO", DbType.Int32, filtro.TIPO);
                }
                if (!String.IsNullOrEmpty(filtro.ARQUIVO))
                {
                    db.AddInParameter(cmd, "@ARQUIVO", DbType.String, filtro.ARQUIVO);
                }
                if (filtro.ORDEM != null)
                {
                    db.AddInParameter(cmd, "@ORDEM", DbType.Int32, filtro.ORDEM);
                }
            }

            DataTable dataTable = db.ExecuteDataTable(cmd);
            totalRegistros = (Int32)db.GetParameterValue(cmd, "@totalRegistros");

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            foreach (DataRow row in dataTable.Rows)
            {
                ANEXO item = this.Popular(row);
                item.PLANTAO = new PLANTAO_ADO().Popular(row);
                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela ANEXO
        /// </summary>
        public ANEXO Inserir(ANEXO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("ANEXO_INSERIR");
            db.AddOutParameter(cmd, "@ANEXO_ID", DbType.Int32, item.ANEXO_ID);
            db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, item.PLANTAO_ID);
            db.AddInParameter(cmd, "@TIPO", DbType.Int32, item.TIPO);
            db.AddInParameter(cmd, "@ARQUIVO", DbType.String, item.ARQUIVO);
            db.AddInParameter(cmd, "@ORDEM", DbType.Int32, item.ORDEM);
            db.ExecuteNonQuery(cmd);
            item.ANEXO_ID = (Int32)db.GetParameterValue(cmd, "@ANEXO_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela ANEXO
        /// </summary>
        public ANEXO Atualizar(ANEXO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("ANEXO_ATUALIZAR");
            db.AddInParameter(cmd, "@ANEXO_ID", DbType.Int32, item.ANEXO_ID);
            db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, item.PLANTAO_ID);
            db.AddInParameter(cmd, "@TIPO", DbType.Int32, item.TIPO);
            db.AddInParameter(cmd, "@ARQUIVO", DbType.String, item.ARQUIVO);
            db.AddInParameter(cmd, "@ORDEM", DbType.Int32, item.ORDEM);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela ANEXO
        /// </summary>
        public void Excluir(ANEXO filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("ANEXO_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.ANEXO_ID != null)
                {
                    db.AddInParameter(cmd, "@ANEXO_ID", DbType.Int32, filtro.ANEXO_ID);
                }
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.TIPO != null)
                {
                    db.AddInParameter(cmd, "@TIPO", DbType.Int32, filtro.TIPO);
                }
                if (!String.IsNullOrEmpty(filtro.ARQUIVO))
                {
                    db.AddInParameter(cmd, "@ARQUIVO", DbType.String, filtro.ARQUIVO);
                }
                if (filtro.ORDEM != null)
                {
                    db.AddInParameter(cmd, "@ORDEM", DbType.Int32, filtro.ORDEM);
                }
			}
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        /// <summary>
        /// Popula os dados
        /// </summary>
        public ANEXO Popular(DataRow row)
        {
            ANEXO reg = new ANEXO();
            reg.ANEXO_ID = ((row["ANEXO_ANEXO_ID"] != DBNull.Value)?Convert.ToInt32(row["ANEXO_ANEXO_ID"].ToString()):0);
            reg.PLANTAO_ID = ((row["ANEXO_PLANTAO_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["ANEXO_PLANTAO_ID"].ToString()):null);
            reg.TIPO = ((row["ANEXO_TIPO"] != DBNull.Value)?(int?)Convert.ToInt32(row["ANEXO_TIPO"].ToString()):null);
            reg.ARQUIVO = ((row["ANEXO_ARQUIVO"] != DBNull.Value)?row["ANEXO_ARQUIVO"].ToString():String.Empty);
            reg.ORDEM = ((row["ANEXO_ORDEM"] != DBNull.Value)?(int?)Convert.ToInt32(row["ANEXO_ORDEM"].ToString()):null);
            return reg;
        }

    }
}
