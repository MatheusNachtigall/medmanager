using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela CIDADE
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class CIDADE
    {
        private Int32? _cIDADE_ID;
        private String _nOME;

        public Int32? CIDADE_ID
        {
            get { return _cIDADE_ID; }
            set { _cIDADE_ID = value; }
        }

        public String NOME
        {
            get { return _nOME; }
            set { _nOME = value; }
        }

        public CIDADE()
        {
        }
    }
}
