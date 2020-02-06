using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela AGENCIA
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class AGENCIA
    {
        private Int32? _aGENCIA_ID;
        private String _nOME;

        public Int32? AGENCIA_ID
        {
            get { return _aGENCIA_ID; }
            set { _aGENCIA_ID = value; }
        }

        public String NOME
        {
            get { return _nOME; }
            set { _nOME = value; }
        }

        public AGENCIA()
        {
        }
    }
}
