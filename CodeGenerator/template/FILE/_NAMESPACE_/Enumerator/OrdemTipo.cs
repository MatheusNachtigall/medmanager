using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

/// <summary>
/// Enumerador do tipo de ordena��o
/// </summary>
namespace #NAMESPACE#.Enumerator
{
    public enum OrdemTipo
    {
        [Description("Ascendente")] Ascendente = 0,
        [Description("Descendente")] Descendente = 1
    }
}