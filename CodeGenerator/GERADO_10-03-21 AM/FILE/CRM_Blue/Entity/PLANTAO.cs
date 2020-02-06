using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela PLANTAO
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class PLANTAO
    {
        private Int32? _pLANTAO_ID;
        private Int32? _hOSPITAL_ID;
        private HOSPITAL _HOSPITAL;
        private Decimal? _vALOR;
        private Boolean? _cNPJ;
        private Boolean? _iNSS;
        private DateTime? _dATA_PLANTAO;
        private DateTime? _dATA_PAGAMENTO;
        private DateTime? _dATA_CADASTRO;
        private Boolean? _rECEBIDO;

        public Int32? PLANTAO_ID
        {
            get { return _pLANTAO_ID; }
            set { _pLANTAO_ID = value; }
        }

        public Int32? HOSPITAL_ID
        {
            get { return _hOSPITAL_ID; }
            set { _hOSPITAL_ID = value; }
        }

        public HOSPITAL HOSPITAL
        {
            get { return _HOSPITAL; }
            set { _HOSPITAL = value; }
        }

        public Decimal? VALOR
        {
            get { return _vALOR; }
            set { _vALOR = value; }
        }

        public Boolean? CNPJ
        {
            get { return _cNPJ; }
            set { _cNPJ = value; }
        }

        public Boolean? INSS
        {
            get { return _iNSS; }
            set { _iNSS = value; }
        }

        public DateTime? DATA_PLANTAO
        {
            get { return _dATA_PLANTAO; }
            set { _dATA_PLANTAO = value; }
        }

        public DateTime? DATA_PAGAMENTO
        {
            get { return _dATA_PAGAMENTO; }
            set { _dATA_PAGAMENTO = value; }
        }

        public DateTime? DATA_CADASTRO
        {
            get { return _dATA_CADASTRO; }
            set { _dATA_CADASTRO = value; }
        }

        public Boolean? RECEBIDO
        {
            get { return _rECEBIDO; }
            set { _rECEBIDO = value; }
        }

        public PLANTAO()
        {
        }
    }
}
