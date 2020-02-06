using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela HOSPITAL
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class HOSPITAL
    {
        private Int32? _hOSPITAL_ID;
        private Int32? _cIDADE_ID;
        private CIDADE _CIDADE;
        private String _nOME;

        public Int32? HOSPITAL_ID
        {
            get { return _hOSPITAL_ID; }
            set { _hOSPITAL_ID = value; }
        }

        public Int32? CIDADE_ID
        {
            get { return _cIDADE_ID; }
            set { _cIDADE_ID = value; }
        }

        public CIDADE CIDADE
        {
            get { return _CIDADE; }
            set { _CIDADE = value; }
        }

        public String NOME
        {
            get { return _nOME; }
            set { _nOME = value; }
        }

        public HOSPITAL()
        {
        }
    }
}
