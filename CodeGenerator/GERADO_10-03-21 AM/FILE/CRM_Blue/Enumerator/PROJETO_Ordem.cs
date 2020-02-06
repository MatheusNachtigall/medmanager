using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela PROJETO
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum PROJETO_Ordem
    {
        [Description("PROJETO_ID")] PROJETO_ID = 0,
        [Description("USUARIO_ID")] USUARIO_ID = 1,
        [Description("AGENCIA_ID")] AGENCIA_ID = 2,
        [Description("CLIENTE_ID")] CLIENTE_ID = 3,
        [Description("NOME")] NOME = 4,
        [Description("DESCRICAO")] DESCRICAO = 5,
        [Description("VALOR")] VALOR = 6,
        [Description("SOLICITANTE")] SOLICITANTE = 7,
        [Description("STATUS")] STATUS = 8,
        [Description("DATA_PROSPECCAO")] DATA_PROSPECCAO = 9,
        [Description("DATA_CONTRATACAO")] DATA_CONTRATACAO = 10,
        [Description("LOCAL_TRABALHO")] LOCAL_TRABALHO = 11,
        [Description("GARANTIA_DIAS")] GARANTIA_DIAS = 12,
        [Description("VALIDADE_DIAS")] VALIDADE_DIAS = 13,
        [Description("PRAZO_DIAS")] PRAZO_DIAS = 14,
        [Description("HORAS")] HORAS = 15,
        [Description("PRAZO_PAGAMENTO_DIAS")] PRAZO_PAGAMENTO_DIAS = 16,
        [Description("DATA_CADASTRO")] DATA_CADASTRO = 17
    }
}
