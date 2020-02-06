using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela DEDUCAO_TIPO
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class DEDUCAO_TIPO
    {
        private Int32? _dEDUCAO_TIPO_ID;
        private String _nOME;

        public Int32? DEDUCAO_TIPO_ID
        {
            get { return _dEDUCAO_TIPO_ID; }
            set { _dEDUCAO_TIPO_ID = value; }
        }

        public String NOME
        {
            get { return _nOME; }
            set { _nOME = value; }
        }

        public DEDUCAO_TIPO()
        {
        }
    }
}
