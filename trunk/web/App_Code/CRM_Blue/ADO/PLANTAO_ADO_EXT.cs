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
/// Classe de acesso ao banco de dados da tabela PLANTAO
/// </summary>
namespace CRM_Blue.ADO
{
    public class PLANTAO_ADO_EXT : BaseADO
    {
        public PLANTAO_ADO_EXT()
        {
        }

        /// <summary>
        /// Lista registros da tabela PLANTAO
        /// </summary>
        public Paginacao<PLANTAO> Listar(PLANTAO filtro, int? pagina, int? registrosPorPagina, PLANTAO_Ordem? ordem, OrdemTipo? ordemTipo, DateTime DATA_FIM)
        {
            Paginacao<PLANTAO> registros = new Paginacao<PLANTAO>();
            List<PLANTAO> itens = new List<PLANTAO>();
            Int32 totalRegistros = 0;

            DbCommand cmd = db.GetStoredProcCommand("PLANTAO_LISTAR_DATA_RANGE");

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
                if (filtro.PLANTAO_ID != null)
                {
                    db.AddInParameter(cmd, "@PLANTAO_ID", DbType.Int32, filtro.PLANTAO_ID);
                }
                if (filtro.HOSPITAL_ID != null)
                {
                    db.AddInParameter(cmd, "@HOSPITAL_ID", DbType.Int32, filtro.HOSPITAL_ID);
                }
                if (filtro.VALOR != null)
                {
                    db.AddInParameter(cmd, "@VALOR", DbType.Decimal, filtro.VALOR);
                }
                if (filtro.CNPJ != null)
                {
                    db.AddInParameter(cmd, "@CNPJ", DbType.Boolean, filtro.CNPJ);
                }
                if (filtro.INSS != null)
                {
                    db.AddInParameter(cmd, "@INSS", DbType.Boolean, filtro.INSS);
                }
                if (filtro.DATA != null)
                {
                    db.AddInParameter(cmd, "@DATA_PLANTAO_INI", DbType.DateTime, filtro.DATA);
                }
                if (DATA_FIM != null)
                {
                    db.AddInParameter(cmd, "@DATA_PLANTAO_FIM", DbType.DateTime, DATA_FIM);
                }
                if (filtro.DATA_PAGAMENTO != null)
                {
                    db.AddInParameter(cmd, "@DATA_PAGAMENTO", DbType.DateTime, filtro.DATA_PAGAMENTO);
                }
                if (filtro.DATA_CADASTRO != null)
                {
                    db.AddInParameter(cmd, "@DATA_CADASTRO", DbType.DateTime, filtro.DATA_CADASTRO);
                }
                if (filtro.RECEBIDO != null)
                {
                    db.AddInParameter(cmd, "@RECEBIDO", DbType.Boolean, filtro.RECEBIDO);
                }
            }

            DataTable dataTable = db.ExecuteDataTable(cmd);
            totalRegistros = (Int32)db.GetParameterValue(cmd, "@totalRegistros");

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            foreach (DataRow row in dataTable.Rows)
            {
                PLANTAO item = this.Popular(row);
                item.HOSPITAL = new HOSPITAL_ADO().Popular(row);
                itens.Add(item);
            }

            registros.Total = totalRegistros;
            registros.Itens = itens;
            registros.ItensPorPagina = ((registrosPorPagina != null)?Convert.ToInt32(registrosPorPagina):0);

            return registros;
        }

        /// <summary>
        /// Popula os dados
        /// </summary>
        public PLANTAO Popular(DataRow row)
        {
            PLANTAO reg = new PLANTAO();
            reg.PLANTAO_ID = ((row["PLANTAO_PLANTAO_ID"] != DBNull.Value)?Convert.ToInt32(row["PLANTAO_PLANTAO_ID"].ToString()):0);
            reg.HOSPITAL_ID = ((row["PLANTAO_HOSPITAL_ID"] != DBNull.Value)?(int?)Convert.ToInt32(row["PLANTAO_HOSPITAL_ID"].ToString()):null);
            reg.VALOR = ((row["PLANTAO_VALOR"] != DBNull.Value)?Convert.ToDecimal(row["PLANTAO_VALOR"].ToString()):0);
            reg.CNPJ = ((row["PLANTAO_CNPJ"] != DBNull.Value)?(Boolean?)Convert.ToBoolean(row["PLANTAO_CNPJ"].ToString()):null);
            reg.INSS = ((row["PLANTAO_INSS"] != DBNull.Value)?(Boolean?)Convert.ToBoolean(row["PLANTAO_INSS"].ToString()):null);
            reg.DATA = ((row["PLANTAO_DATA_PLANTAO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["PLANTAO_DATA_PLANTAO"].ToString()):null);
            reg.DATA_PAGAMENTO = ((row["PLANTAO_DATA_PAGAMENTO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["PLANTAO_DATA_PAGAMENTO"].ToString()):null);
            reg.DATA_CADASTRO = ((row["PLANTAO_DATA_CADASTRO"] != DBNull.Value)?(DateTime?)Convert.ToDateTime(row["PLANTAO_DATA_CADASTRO"].ToString()):null);
            reg.RECEBIDO = ((row["PLANTAO_RECEBIDO"] != DBNull.Value)?(Boolean?)Convert.ToBoolean(row["PLANTAO_RECEBIDO"].ToString()):null);
            return reg;
        }

    }
}
