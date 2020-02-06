using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela INSS
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class INSS
    {
        private Int32? _iNSS_ID;
        private Int32? _pLANTAO_ID;
        private PLANTAO _PLANTAO;
        private Decimal? _vALOR;
        private DateTime? _dATA_CADASTRO;

        public Int32? INSS_ID
        {
            get { return _iNSS_ID; }
            set { _iNSS_ID = value; }
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

        public Decimal? VALOR
        {
            get { return _vALOR; }
            set { _vALOR = value; }
        }

        public DateTime? DATA_CADASTRO
        {
            get { return _dATA_CADASTRO; }
            set { _dATA_CADASTRO = value; }
        }

        public INSS()
        {
        }
    }
}
