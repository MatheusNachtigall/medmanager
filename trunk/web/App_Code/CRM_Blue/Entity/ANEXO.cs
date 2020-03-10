using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela ANEXO
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class ANEXO
    {
        private Int32? _aNEXO_ID;
        private Int32? _pLANTAO_ID;
        private PLANTAO _PLANTAO;
        private Int32? _tIPO;
        private String _aRQUIVO;
        private Int32? _oRDEM;

        public Int32? ANEXO_ID
        {
            get { return _aNEXO_ID; }
            set { _aNEXO_ID = value; }
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

        public Int32? TIPO
        {
            get { return _tIPO; }
            set { _tIPO = value; }
        }

        public String ARQUIVO
        {
            get { return _aRQUIVO; }
            set { _aRQUIVO = value; }
        }

        public Int32? ORDEM
        {
            get { return _oRDEM; }
            set { _oRDEM = value; }
        }

        public ANEXO()
        {
        }
    }
}
