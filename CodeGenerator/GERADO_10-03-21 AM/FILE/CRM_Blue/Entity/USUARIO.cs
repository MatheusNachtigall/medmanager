using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela USUARIO
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class USUARIO
    {
        private Int32? _uSUARIO_ID;
        private String _nOME;
        private String _eMAIL;
        private String _sENHA;

        public Int32? USUARIO_ID
        {
            get { return _uSUARIO_ID; }
            set { _uSUARIO_ID = value; }
        }

        public String NOME
        {
            get { return _nOME; }
            set { _nOME = value; }
        }

        public String EMAIL
        {
            get { return _eMAIL; }
            set { _eMAIL = value; }
        }

        public String SENHA
        {
            get { return _sENHA; }
            set { _sENHA = value; }
        }

        public USUARIO()
        {
        }
    }
}
