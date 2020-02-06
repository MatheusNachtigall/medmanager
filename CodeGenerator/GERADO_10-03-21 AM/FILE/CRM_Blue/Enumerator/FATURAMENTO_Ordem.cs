using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela FATURAMENTO
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum FATURAMENTO_Ordem
    {
        [Description("FATURAMENTO_ID")] FATURAMENTO_ID = 0,
        [Description("USUARIO_ID")] USUARIO_ID = 1,
        [Description("PROJETO_ID")] PROJETO_ID = 2,
        [Description("VALOR")] VALOR = 3,
        [Description("STATUS")] STATUS = 4,
        [Description("DATA_FATURAMENTO")] DATA_FATURAMENTO = 5,
        [Description("DATA_RECEBIMENTO")] DATA_RECEBIMENTO = 6,
        [Description("DATA_CADASTRO")] DATA_CADASTRO = 7
    }
}
