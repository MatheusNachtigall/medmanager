using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador para ordenação da tabela USUARIO
/// </summary>
namespace CRM_Blue.Enumerator
{
    public enum USUARIO_Ordem
    {
        [Description("USUARIO_ID")] USUARIO_ID = 0,
        [Description("NOME")] NOME = 1,
        [Description("EMAIL")] EMAIL = 2,
        [Description("SENHA")] SENHA = 3
    }
}
