using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Classe base da camada ADO
/// </summary>
namespace CRM_Blue.ADO
{
    public class BaseADO
    {
        protected Database db = new Database();

        public BaseADO()
        {
        }
    }
}
