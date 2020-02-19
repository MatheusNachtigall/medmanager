using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela HOSPITAL
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum HOSPITAL_Ordem
    {
        [Description("HOSPITAL_ID")] HOSPITAL_ID = 0,
        [Description("NOME")] NOME = 1,
        [Description("CIDADE")] CIDADE = 2,
        [Description("COR")] COR = 3
    }
}
