using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Text;
using #NAMESPACE#.Entity;
using #NAMESPACE#.Enumerator;
using #NAMESPACE#.Service;
using Utilities;

/// <summary>
/// Classe de acesso ao banco de dados da tabela #TABELA_CAPTION#
/// </summary>
namespace #NAMESPACE#.ADO
{
    public class #TABELA_CAPTION#_ADO : BaseADO
    {
        public #TABELA_CAPTION#_ADO()
        {
        }

        /// <summary>
        /// Carrega um registro da tabela #TABELA_CAPTION#
        /// </summary>
        public #TABELA_CAPTION# Carregar(#TABELA_CAPTION# filtro)
        {
            DbCommand cmd = db.GetStoredProcCommand("#STORED_PROCEDURE_CARREGAR#");

            if (filtro != null)
            {
#FILTROS#
            }
            DataTable dataTable = db.ExecuteDataTable(cmd);

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            if (dataTable.Rows.Count > 0)
            {
                #TABELA_CAPTION# item = this.Popular(dataTable.Rows[0]);
#POPULAR_RELACIONAMENTOS_CARREGAR#
                return item;
            }

            return null;
        }

        /// <summary>
        /// Lista registros da tabela #TABELA_CAPTION#
        /// </summary>
        public Paginacao<#TABELA_CAPTION#> Listar(#TABELA_CAPTION# filtro, int? pagina, int? registrosPorPagina, #TABELA_CAPTION#_Ordem? ordem, OrdemTipo? ordemTipo)
        {
            Paginacao<#TABELA_CAPTION#> registros = new Paginacao<#TABELA_CAPTION#>();
            List<#TABELA_CAPTION#> itens = new List<#TABELA_CAPTION#>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("#STORED_PROCEDURE_LISTAR#");

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
#FILTROS#
            }

            DataTable dataTable = db.ExecuteDataTable(cmd);
            totalRegistros = (Int32)db.GetParameterValue(cmd, "@totalRegistros");

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            foreach (DataRow row in dataTable.Rows)
            {
                #TABELA_CAPTION# item = this.Popular(row);
#POPULAR_RELACIONAMENTOS_LISTAR#
                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

		/// <summary>
        /// Inseri um registro na tabela #TABELA_CAPTION#
        /// </summary>
        public #TABELA_CAPTION# Inserir(#TABELA_CAPTION# item)
        {
			DbCommand cmd = db.GetStoredProcCommand("#STORED_PROCEDURE_INSERIR#");
#INSERIR_SET_ID#
#PARAMETROS#
            db.ExecuteNonQuery(cmd);
#INSERIR_GET_ID#
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Atualiza um registro da tabela #TABELA_CAPTION#
        /// </summary>
        public #TABELA_CAPTION# Atualizar(#TABELA_CAPTION# item)
        {
			DbCommand cmd = db.GetStoredProcCommand("#STORED_PROCEDURE_ATUALIZAR#");
#ATUALIZAR_SET_ID#
#PARAMETROS#
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return item;
        }

		/// <summary>
        /// Exclui um registro da tabela #TABELA_CAPTION#
        /// </summary>
        public void Excluir(#TABELA_CAPTION# filtro)
        {
			DbCommand cmd = db.GetStoredProcCommand("#STORED_PROCEDURE_EXCLUIR#");
            if (filtro != null)
            {
#FILTROS#
			}
            db.ExecuteNonQuery(cmd);
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }

        /// <summary>
        /// Popula os dados
        /// </summary>
        public #TABELA_CAPTION# Popular(DataRow row)
        {
            #TABELA_CAPTION# reg = new #TABELA_CAPTION#();
#POPULAR_CAMPOS#
            return reg;
        }

    }
}