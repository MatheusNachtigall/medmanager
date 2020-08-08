using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela PLANTAO
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum PLANTAO_Ordem
    {
        [Description("PLANTAO_ID")] PLANTAO_ID = 0,
        [Description("HOSPITAL_ID")] HOSPITAL_ID = 1,
        [Description("VALOR")] VALOR = 2,
        [Description("CNPJ")] CNPJ = 3,
        [Description("INSS")] INSS = 4,
        [Description("DATA")] DATA = 5,
        [Description("HORARIO")] HORARIO = 6,
        [Description("PERIODO")] PERIODO = 7,
        [Description("DATA_PAGAMENTO")] DATA_PAGAMENTO = 8,
        [Description("DATA_CADASTRO")] DATA_CADASTRO = 9,
        [Description("RECEBIDO")] RECEBIDO = 10
    }
}
