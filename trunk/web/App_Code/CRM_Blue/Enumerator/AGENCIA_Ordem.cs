using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela AGENCIA
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum AGENCIA_Ordem
    {
        [Description("AGENCIA_ID")] AGENCIA_ID = 0,
        [Description("NOME")] NOME = 1
    }
}
