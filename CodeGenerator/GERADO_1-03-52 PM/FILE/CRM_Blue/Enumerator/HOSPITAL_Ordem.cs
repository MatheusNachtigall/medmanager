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
        [Description("CIDADE_ID")] CIDADE_ID = 1,
        [Description("NOME")] NOME = 2
    }
}
