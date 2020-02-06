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
/// Classe de acesso ao banco de dados da tabela PROJETO
/// </summary>
namespace CRM_Blue.ADO
{
    public class PROJETO_ADO : BaseADO
    {
        public PROJETO_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela PROJETO
        /// </summary>
        public PROJETO Carregar(PROJETO filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("PROJETO_CARREGAR");

            if (filtro != null)
            {
                if (filtro.PROJETO_ID != null)
                {
                    db.AddInParameter(cmd, "@PROJETO_ID", DbType.Int32, filtro.PROJETO_ID);
                }
                if (filtro.USUARIO_ID != null)
                {
                    db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, filtro.USUARIO_ID);
                }
                if (filtro.AGENCIA_ID != null)
                {
                    db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, filtro.AGENCIA_ID);
                }
                if (filtro.CLIENTE_ID != null)
                {
                    db.AddInParameter(cmd, "@CLIENTE_ID", DbType.Int32, filtro.CLIENTE_ID);
                }
                if (!String.IsNullOrEmpty(filtro.NOME))
                {
                    db.AddInParameter(cmd, "@NOME", DbType.String, filtro.NOME);
                }
                if (!String.IsNullOrEmpty(filtro.DESCRICAO))
                {
                    db.AddInParameter(cmd, "@DESCRICAO", DbType.String, filtro.DESCRICAO);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (!String.IsNullOrEmpty(filtro.SOLICITANTE))
                {
                    db.AddInParameter(cmd, "@SOLICITANTE", DbType.String, filtro.SOLICITANTE);
                }
                if (filtro.STATUS != null)
                {
                    db.AddInParameter(cmd, "@STATUS", DbType.Int32, filtro.STATUS);
                }
                if (filtro.DATA_PROSPECCAO != null)
                {
                    db.AddInParameter(cmd, "@DATA_PROSPECCAO", DbType.DateTime, filtro.DATA_PROSPECCAO);
                }
                if (filtro.DATA_CONTRATACAO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CONTRATACAO", DbType.DateTime, filtro.DATA_CONTRATACAO);
                }
                if (filtro.LOCAL_TRABALHO != null)
                {
                    db.AddInParameter(cmd, "@LOCAL_TRABALHO", DbType.Int32, filtro.LOCAL_TRABALHO);
                }
                if (filtro.GARANTIA_DIAS != null)
                {
                    db.AddInParameter(cmd, "@GARANTIA_DIAS", DbType.Int32, filtro.GARANTIA_DIAS);
                }
                if (filtro.VALIDADE_DIAS != null)
                {
                    db.AddInParameter(cmd, "@VALIDADE_DIAS", DbType.Int32, filtro.VALIDADE_DIAS);
                }
                if (filtro.PRAZO_DIAS != null)
                {
                    db.AddInParameter(cmd, "@PRAZO_DIAS", DbType.Int32, filtro.PRAZO_DIAS);
                }
                if (filtro.HORAS != null)
                {
                    db.AddInParameter(cmd, "@HORAS", DbType.Int32, filtro.HORAS);
                }
                if (filtro.PRAZO_PAGAMENTO_DIAS != null)
                {
                    db.AddInParameter(cmd, "@PRAZO_PAGAMENTO_DIAS", DbType.Int32, filtro.PRAZO_PAGAMENTO_DIAS);
                }
                if (filtro.DATA_CADASTRO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, filtro.DATA_CADASTRO);
                }
                if (!String.IsNullOrEmpty(filtro.ESCOPO))
                {
                    db.AddInParameter(cmd, "@ESCOPO", DbType.String, filtro.ESCOPO);
                }
            }
            DataTable dataTable = db.ExecuteDataTable(cmd);

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (dataTable.Rows.Count > 0)
            {
                PROJETO item = this.Popular(dataTable.Rows[0]);
                item.USUARIO = new USUARIO_ADO().Popular(dataTable.Rows[0]);
                item.AGENCIA = new AGENCIA_ADO().Popular(dataTable.Rows[0]);
                item.CLIENTE = new CLIENTE_ADO().Popular(dataTable.Rows[0]);
                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela PROJETO
        /// </summary>
        public Paginacao<PROJETO> Listar(PROJETO filtro, int? pagina, int? registrosPorPagina, PROJETO_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<PROJETO> registros = new Paginacao<PROJETO>();
            List<PROJETO> itens = new List<PROJETO>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("PROJETO_LISTAR");

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
                if (filtro.PROJETO_ID != null)
                {
                    db.AddInParameter(cmd, "@PROJETO_ID", DbType.Int32, filtro.PROJETO_ID);
                }
                if (filtro.USUARIO_ID != null)
                {
                    db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, filtro.USUARIO_ID);
                }
                if (filtro.AGENCIA_ID != null)
                {
                    db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, filtro.AGENCIA_ID);
                }
                if (filtro.CLIENTE_ID != null)
                {
                    db.AddInParameter(cmd, "@CLIENTE_ID", DbType.Int32, filtro.CLIENTE_ID);
                }
                if (!String.IsNullOrEmpty(filtro.NOME))
                {
                    db.AddInParameter(cmd, "@NOME", DbType.String, filtro.NOME);
                }
                if (!String.IsNullOrEmpty(filtro.DESCRICAO))
                {
                    db.AddInParameter(cmd, "@DESCRICAO", DbType.String, filtro.DESCRICAO);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (!String.IsNullOrEmpty(filtro.SOLICITANTE))
                {
                    db.AddInParameter(cmd, "@SOLICITANTE", DbType.String, filtro.SOLICITANTE);
                }
                if (filtro.STATUS != null)
                {
                    db.AddInParameter(cmd, "@STATUS", DbType.Int32, filtro.STATUS);
                }
                if (filtro.DATA_PROSPECCAO != null)
                {
                    db.AddInParameter(cmd, "@DATA_PROSPECCAO", DbType.DateTime, filtro.DATA_PROSPECCAO);
                }
                if (filtro.DATA_CONTRATACAO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CONTRATACAO", DbType.DateTime, filtro.DATA_CONTRATACAO);
                }
                if (filtro.LOCAL_TRABALHO != null)
                {
                    db.AddInParameter(cmd, "@LOCAL_TRABALHO", DbType.Int32, filtro.LOCAL_TRABALHO);
                }
                if (filtro.GARANTIA_DIAS != null)
                {
                    db.AddInParameter(cmd, "@GARANTIA_DIAS", DbType.Int32, filtro.GARANTIA_DIAS);
                }
                if (filtro.VALIDADE_DIAS != null)
                {
                    db.AddInParameter(cmd, "@VALIDADE_DIAS", DbType.Int32, filtro.VALIDADE_DIAS);
                }
                if (filtro.PRAZO_DIAS != null)
                {
                    db.AddInParameter(cmd, "@PRAZO_DIAS", DbType.Int32, filtro.PRAZO_DIAS);
                }
                if (filtro.HORAS != null)
                {
                    db.AddInParameter(cmd, "@HORAS", DbType.Int32, filtro.HORAS);
                }
                if (filtro.PRAZO_PAGAMENTO_DIAS != null)
                {
                    db.AddInParameter(cmd, "@PRAZO_PAGAMENTO_DIAS", DbType.Int32, filtro.PRAZO_PAGAMENTO_DIAS);
                }
                if (filtro.DATA_CADASTRO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, filtro.DATA_CADASTRO);
                }
                if (!String.IsNullOrEmpty(filtro.ESCOPO))
                {
                    db.AddInParameter(cmd, "@ESCOPO", DbType.String, filtro.ESCOPO);
                }
            }

            DataTable dataTable = db.ExecuteDataTable(cmd);
            totalRegistros = (Int32)db.GetParameterValue(cmd, "@totalRegistros");

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            foreach (DataRow row in dataTable.Rows)
            {
                PROJETO item = this.Popular(row);
                item.USUARIO = new USUARIO_ADO().Popular(row);
                item.AGENCIA = new AGENCIA_ADO().Popular(row);
                item.CLIENTE = new CLIENTE_ADO().Popular(row);
                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela PROJETO
        /// </summary>
        public PROJETO Inserir(PROJETO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("PROJETO_INSERIR");
            db.AddOutParameter(cmd, "@PROJETO_ID", DbType.Int32, item.PROJETO_ID);
            db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, item.USUARIO_ID);
            db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, item.AGENCIA_ID);
            db.AddInParameter(cmd, "@CLIENTE_ID", DbType.Int32, item.CLIENTE_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.AddInParameter(cmd, "@DESCRICAO", DbType.String, item.DESCRICAO);
            db.AddInParameter(cmd, "@VALOR", DbType.Decimal, item.VALOR);
            db.AddInParameter(cmd, "@SOLICITANTE", DbType.String, item.SOLICITANTE);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, item.STATUS);
            db.AddInParameter(cmd, "@DATA_PROSPECCAO", DbType.DateTime, item.DATA_PROSPECCAO);
            db.AddInParameter(cmd, "@DATA_CONTRATACAO", DbType.DateTime, item.DATA_CONTRATACAO);
            db.AddInParameter(cmd, "@LOCAL_TRABALHO", DbType.Int32, item.LOCAL_TRABALHO);
            db.AddInParameter(cmd, "@GARANTIA_DIAS", DbType.Int32, item.GARANTIA_DIAS);
            db.AddInParameter(cmd, "@VALIDADE_DIAS", DbType.Int32, item.VALIDADE_DIAS);
            db.AddInParameter(cmd, "@PRAZO_DIAS", DbType.Int32, item.PRAZO_DIAS);
            db.AddInParameter(cmd, "@HORAS", DbType.Int32, item.HORAS);
            db.AddInParameter(cmd, "@PRAZO_PAGAMENTO_DIAS", DbType.Int32, item.PRAZO_PAGAMENTO_DIAS);
            db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, item.DATA_CADASTRO);
            db.AddInParameter(cmd, "@ESCOPO", DbType.String, item.ESCOPO);
            db.ExecuteNonQuery(cmd);
            item.PROJETO_ID = (Int32)db.GetParameterValue(cmd, "@PROJETO_ID");
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela PROJETO
        /// </summary>
        public PROJETO Atualizar(PROJETO item)
        {
			DbCommand cmd = db.GetStoredProcCommand("PROJETO_ATUALIZAR");
            db.AddInParameter(cmd, "@PROJETO_ID", DbType.Int32, item.PROJETO_ID);
            db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, item.USUARIO_ID);
            db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, item.AGENCIA_ID);
            db.AddInParameter(cmd, "@CLIENTE_ID", DbType.Int32, item.CLIENTE_ID);
            db.AddInParameter(cmd, "@NOME", DbType.String, item.NOME);
            db.AddInParameter(cmd, "@DESCRICAO", DbType.String, item.DESCRICAO);
            db.AddInParameter(cmd, "@VALOR", DbType.Decimal, item.VALOR);
            db.AddInParameter(cmd, "@SOLICITANTE", DbType.String, item.SOLICITANTE);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, item.STATUS);
            db.AddInParameter(cmd, "@DATA_PROSPECCAO", DbType.DateTime, item.DATA_PROSPECCAO);
            db.AddInParameter(cmd, "@DATA_CONTRATACAO", DbType.DateTime, item.DATA_CONTRATACAO);
            db.AddInParameter(cmd, "@LOCAL_TRABALHO", DbType.Int32, item.LOCAL_TRABALHO);
            db.AddInParameter(cmd, "@GARANTIA_DIAS", DbType.Int32, item.GARANTIA_DIAS);
            db.AddInParameter(cmd, "@VALIDADE_DIAS", DbType.Int32, item.VALIDADE_DIAS);
            db.AddInParameter(cmd, "@PRAZO_DIAS", DbType.Int32, item.PRAZO_DIAS);
            db.AddInParameter(cmd, "@HORAS", DbType.Int32, item.HORAS);
            db.AddInParameter(cmd, "@PRAZO_PAGAMENTO_DIAS", DbType.Int32, item.PRAZO_PAGAMENTO_DIAS);
            db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, item.DATA_CADASTRO);
            db.AddInParameter(cmd, "@ESCOPO", DbType.String, item.ESCOPO);
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela PROJETO
        /// </summary>
        public void Excluir(PROJETO filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("PROJETO_EXCLUIR");
            if (filtro != null)
            {
                if (filtro.PROJETO_ID != null)
                {
                    db.AddInParameter(cmd, "@PROJETO_ID", DbType.Int32, filtro.PROJETO_ID);
                }
                if (filtro.USUARIO_ID != null)
                {
                    db.AddInParameter(cmd, "@USUARIO_ID", DbType.Int32, filtro.USUARIO_ID);
                }
                if (filtro.AGENCIA_ID != null)
                {
                    db.AddInParameter(cmd, "@AGENCIA_ID", DbType.Int32, filtro.AGENCIA_ID);
                }
                if (filtro.CLIENTE_ID != null)
                {
                    db.AddInParameter(cmd, "@CLIENTE_ID", DbType.Int32, filtro.CLIENTE_ID);
                }
                if (!String.IsNullOrEmpty(filtro.NOME))
                {
                    db.AddInParameter(cmd, "@NOME", DbType.String, filtro.NOME);
                }
                if (!String.IsNullOrEmpty(filtro.DESCRICAO))
                {
                    db.AddInParameter(cmd, "@DESCRICAO", DbType.String, filtro.DESCRICAO);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (!String.IsNullOrEmpty(filtro.SOLICITANTE))
                {
                    db.AddInParameter(cmd, "@SOLICITANTE", DbType.String, filtro.SOLICITANTE);
                }
                if (filtro.STATUS != null)
                {
                    db.AddInParameter(cmd, "@STATUS", DbType.Int32, filtro.STATUS);
                }
                if (filtro.DATA_PROSPECCAO != null)
                {
                    db.AddInParameter(cmd, "@DATA_PROSPECCAO", DbType.DateTime, filtro.DATA_PROSPECCAO);
                }
                if (filtro.DATA_CONTRATACAO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CONTRATACAO", DbType.DateTime, filtro.DATA_CONTRATACAO);
                }
                if (filtro.LOCAL_TRABALHO != null)
                {
                    db.AddInParameter(cmd, "@LOCAL_TRABALHO", DbType.Int32, filtro.LOCAL_TRABALHO);
                }
                if (filtro.GARANTIA_DIAS != null)
                {
                    db.AddInParameter(cmd, "@GARANTIA_DIAS", DbType.Int32, filtro.GARANTIA_DIAS);
                }
                if (filtro.VALIDADE_DIAS != null)
                {
                    db.AddInParameter(cmd, "@VALIDADE_DIAS", DbType.Int32, filtro.VALIDADE_DIAS);
                }
                if (filtro.PRAZO_DIAS != null)
                {
                    db.AddInParameter(cmd, "@PRAZO_DIAS", DbType.Int32, filtro.PRAZO_DIAS);
                }
                if (filtro.HORAS != null)
                {
                    db.AddInParameter(cmd, "@HORAS", DbType.Int32, filtro.HORAS);
                }
                if (filtro.PRAZO_PAGAMENTO_DIAS != null)
                {
                    db.AddInParameter(cmd, "@PRAZO_PAGAMENTO_DIAS", DbType.Int32, filtro.PRAZO_PAGAMENTO_DIAS);
                }
                if (filtro.DATA_CADASTRO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, filtro.DATA_CADASTRO);
                }
                if (!String.IsNullOrEmpty(filtro.ESCOPO))
                {
                    db.AddInParameter(cmd, "@ESCOPO", DbType.String, filtro.ESCOPO);
                }
			}
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        /// <summary>
        /// Popula os dados
        /// </summary>
        public PROJETO Popular(DataRow row)
        {
            PROJETO reg = new PROJETO();
            reg.PROJETO_ID = ((row["PROJETO_PROJETO_ID"] != DBNull.Value)?Convert.ToInt32(row["PROJETO_PROJETO_ID"].ToString()):0);
            reg.USUARIO_ID = ((row["PROJETO_USUARIO_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_USUARIO_ID"].ToString()):null);
            reg.AGENCIA_ID = ((row["PROJETO_AGENCIA_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_AGENCIA_ID"].ToString()):null);
            reg.CLIENTE_ID = ((row["PROJETO_CLIENTE_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_CLIENTE_ID"].ToString()):null);
            reg.NOME = ((row["PROJETO_NOME"] != DBNull.Value)?row["PROJETO_NOME"].ToString():String.Empty);
            reg.DESCRICAO = ((row["PROJETO_DESCRICAO"] != DBNull.Value)?row["PROJETO_DESCRICAO"].ToString():String.Empty);
            reg.VALOR = ((row["PROJETO_VALOR"] != DBNull.Value)?(decimal?)Convert.ToDecimal(row["PROJETO_VALOR"].ToString()):null);
            reg.SOLICITANTE = ((row["PROJETO_SOLICITANTE"] != DBNull.Value)?row["PROJETO_SOLICITANTE"].ToString():String.Empty);
            reg.STATUS = ((row["PROJETO_STATUS"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_STATUS"].ToString()):null);
            reg.DATA_PROSPECCAO = ((row["PROJETO_DATA_PROSPECCAO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["PROJETO_DATA_PROSPECCAO"].ToString()):null);
            reg.DATA_CONTRATACAO = ((row["PROJETO_DATA_CONTRATACAO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["PROJETO_DATA_CONTRATACAO"].ToString()):null);
            reg.LOCAL_TRABALHO = ((row["PROJETO_LOCAL_TRABALHO"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_LOCAL_TRABALHO"].ToString()):null);
            reg.GARANTIA_DIAS = ((row["PROJETO_GARANTIA_DIAS"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_GARANTIA_DIAS"].ToString()):null);
            reg.VALIDADE_DIAS = ((row["PROJETO_VALIDADE_DIAS"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_VALIDADE_DIAS"].ToString()):null);
            reg.PRAZO_DIAS = ((row["PROJETO_PRAZO_DIAS"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_PRAZO_DIAS"].ToString()):null);
            reg.HORAS = ((row["PROJETO_HORAS"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_HORAS"].ToString()):null);
            reg.PRAZO_PAGAMENTO_DIAS = ((row["PROJETO_PRAZO_PAGAMENTO_DIAS"] != DBNull.Value)?(int?)Convert.ToInt32(row["PROJETO_PRAZO_PAGAMENTO_DIAS"].ToString()):null);
            reg.DATA_CADASTRO = ((row["PROJETO_DATA_CADASTRO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["PROJETO_DATA_CADASTRO"].ToString()):null);
            reg.ESCOPO = ((row["PROJETO_ESCOPO"] != DBNull.Value)?row["PROJETO_ESCOPO"].ToString():String.Empty);
            return reg;
        }

    }
}
