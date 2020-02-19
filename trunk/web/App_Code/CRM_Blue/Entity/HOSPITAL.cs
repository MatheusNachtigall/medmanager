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
        private String _nOME;
        private String _cIDADE;
        private String _cOR;

        public Int32? HOSPITAL_ID
        {
            get { return _hOSPITAL_ID; }
            set { _hOSPITAL_ID = value; }
        }

        public String NOME
        {
            get { return _nOME; }
            set { _nOME = value; }
        }

        public String CIDADE
        {
            get { return _cIDADE; }
            set { _cIDADE = value; }
        }

        public String COR
        {
            get { return _cOR; }
            set { _cOR = value; }
        }

        public HOSPITAL()
        {
        }
    }
}
