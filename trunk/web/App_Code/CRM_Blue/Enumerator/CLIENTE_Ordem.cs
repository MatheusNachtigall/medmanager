using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela CLIENTE
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum CLIENTE_Ordem
    {
        [Description("CLIENTE_ID")] CLIENTE_ID = 0,
        [Description("AGENCIA_ID")] AGENCIA_ID = 1,
        [Description("NOME")] NOME = 2
    }
}
