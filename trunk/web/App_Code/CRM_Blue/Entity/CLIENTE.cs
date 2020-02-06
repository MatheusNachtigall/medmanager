using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela CLIENTE
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class CLIENTE
    {
        private Int32? _cLIENTE_ID;
        private Int32? _aGENCIA_ID;
        private AGENCIA _AGENCIA;
        private String _nOME;

        public Int32? CLIENTE_ID
        {
            get { return _cLIENTE_ID; }
            set { _cLIENTE_ID = value; }
        }

        public Int32? AGENCIA_ID
        {
            get { return _aGENCIA_ID; }
            set { _aGENCIA_ID = value; }
        }

        public AGENCIA AGENCIA
        {
            get { return _AGENCIA; }
            set { _AGENCIA = value; }
        }

        public String NOME
        {
            get { return _nOME; }
            set { _nOME = value; }
        }

        public CLIENTE()
        {
        }
    }
}
