using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela INSS
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum INSS_Ordem
    {
        [Description("INSS_ID")] INSS_ID = 0,
        [Description("PLANTAO_ID")] PLANTAO_ID = 1,
        [Description("VALOR")] VALOR = 2,
        [Description("DATA_INSS")] DATA_INSS = 3,
        [Description("DATA_CADASTRO")] DATA_CADASTRO = 4
    }
}
