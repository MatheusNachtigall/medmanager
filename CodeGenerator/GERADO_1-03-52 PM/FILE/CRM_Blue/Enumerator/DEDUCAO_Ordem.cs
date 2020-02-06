using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela DEDUCAO
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum DEDUCAO_Ordem
    {
        [Description("DEDUCAO_ID")] DEDUCAO_ID = 0,
        [Description("PLANTAO_ID")] PLANTAO_ID = 1,
        [Description("DEDUCAO_TIPO_ID")] DEDUCAO_TIPO_ID = 2,
        [Description("VALOR")] VALOR = 3,
        [Description("DATA_DEDUCAO")] DATA_DEDUCAO = 4
    }
}
