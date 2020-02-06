using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela CIDADE
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum CIDADE_Ordem
    {
        [Description("CIDADE_ID")] CIDADE_ID = 0,
        [Description("NOME")] NOME = 1
    }
}
