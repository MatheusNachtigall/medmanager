using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela DEDUCAO
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class DEDUCAO
    {
        private Int32? _dEDUCAO_ID;
        private Int32? _pLANTAO_ID;
        private PLANTAO _PLANTAO;
        private Int32? _dEDUCAO_TIPO_ID;
        private DEDUCAO_TIPO _DEDUCAO_TIPO;
        private Decimal? _vALOR;
        private DateTime? _dATA_DEDUCAO;

        public Int32? DEDUCAO_ID
        {
            get { return _dEDUCAO_ID; }
            set { _dEDUCAO_ID = value; }
        }

        public Int32? PLANTAO_ID
        {
            get { return _pLANTAO_ID; }
            set { _pLANTAO_ID = value; }
        }

        public PLANTAO PLANTAO
        {
            get { return _PLANTAO; }
            set { _PLANTAO = value; }
        }

        public Int32? DEDUCAO_TIPO_ID
        {
            get { return _dEDUCAO_TIPO_ID; }
            set { _dEDUCAO_TIPO_ID = value; }
        }

        public DEDUCAO_TIPO DEDUCAO_TIPO
        {
            get { return _DEDUCAO_TIPO; }
            set { _DEDUCAO_TIPO = value; }
        }

        public Decimal? VALOR
        {
            get { return _vALOR; }
            set { _vALOR = value; }
        }

        public DateTime? DATA_DEDUCAO
        {
            get { return _dATA_DEDUCAO; }
            set { _dATA_DEDUCAO = value; }
        }

        public DEDUCAO()
        {
        }
    }
}
