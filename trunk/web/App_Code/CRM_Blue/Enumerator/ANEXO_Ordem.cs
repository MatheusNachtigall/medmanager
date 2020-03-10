using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela ANEXO
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum ANEXO_Ordem
    {
        [Description("ANEXO_ID")] ANEXO_ID = 0,
        [Description("PLANTAO_ID")] PLANTAO_ID = 1,
        [Description("TIPO")] TIPO = 2,
        [Description("ARQUIVO")] ARQUIVO = 3,
        [Description("ORDEM")] ORDEM = 4
    }
}
