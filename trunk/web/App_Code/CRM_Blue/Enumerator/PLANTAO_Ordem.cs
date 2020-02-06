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
        [Description("DATA_PLANTAO")] DATA_PLANTAO = 5,
        [Description("DATA_PAGAMENTO")] DATA_PAGAMENTO = 6,
        [Description("DATA_CADASTRO")] DATA_CADASTRO = 7,
        [Description("RECEBIDO")] RECEBIDO = 8
    }
}
