using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Classe base da camada ADO
/// </summary>
namespace #NAMESPACE#.ADO
{
    public class BaseADO
    {
        protected Database db = new Database();

        public BaseADO()
        {
        }
    }
}