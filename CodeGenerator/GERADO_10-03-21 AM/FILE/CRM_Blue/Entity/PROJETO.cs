using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela PROJETO
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class PROJETO
    {
        private Int32? _pROJETO_ID;
        private Int32? _uSUARIO_ID;
        private USUARIO _USUARIO;
        private Int32? _aGENCIA_ID;
        private AGENCIA _AGENCIA;
        private Int32? _cLIENTE_ID;
        private CLIENTE _CLIENTE;
        private String _nOME;
        private String _dESCRICAO;
        private Decimal? _vALOR;
        private String _sOLICITANTE;
        private Int32? _sTATUS;
        private DateTime? _dATA_PROSPECCAO;
        private DateTime? _dATA_CONTRATACAO;
        private Int32? _lOCAL_TRABALHO;
        private Int32? _gARANTIA_DIAS;
        private Int32? _vALIDADE_DIAS;
        private Int32? _pRAZO_DIAS;
        private Int32? _hORAS;
        private Int32? _pRAZO_PAGAMENTO_DIAS;
        private DateTime? _dATA_CADASTRO;
        private String _eSCOPO;

        public Int32? PROJETO_ID
        {
            get { return _pROJETO_ID; }
            set { _pROJETO_ID = value; }
        }

        public Int32? USUARIO_ID
        {
            get { return _uSUARIO_ID; }
            set { _uSUARIO_ID = value; }
        }

        public USUARIO USUARIO
        {
            get { return _USUARIO; }
            set { _USUARIO = value; }
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

        public Int32? CLIENTE_ID
        {
            get { return _cLIENTE_ID; }
            set { _cLIENTE_ID = value; }
        }

        public CLIENTE CLIENTE
        {
            get { return _CLIENTE; }
            set { _CLIENTE = value; }
        }

        public String NOME
        {
            get { return _nOME; }
            set { _nOME = value; }
        }

        public String DESCRICAO
        {
            get { return _dESCRICAO; }
            set { _dESCRICAO = value; }
        }

        public Decimal? VALOR
        {
            get { return _vALOR; }
            set { _vALOR = value; }
        }

        public String SOLICITANTE
        {
            get { return _sOLICITANTE; }
            set { _sOLICITANTE = value; }
        }

        public Int32? STATUS
        {
            get { return _sTATUS; }
            set { _sTATUS = value; }
        }

        public DateTime? DATA_PROSPECCAO
        {
            get { return _dATA_PROSPECCAO; }
            set { _dATA_PROSPECCAO = value; }
        }

        public DateTime? DATA_CONTRATACAO
        {
            get { return _dATA_CONTRATACAO; }
            set { _dATA_CONTRATACAO = value; }
        }

        public Int32? LOCAL_TRABALHO
        {
            get { return _lOCAL_TRABALHO; }
            set { _lOCAL_TRABALHO = value; }
        }

        public Int32? GARANTIA_DIAS
        {
            get { return _gARANTIA_DIAS; }
            set { _gARANTIA_DIAS = value; }
        }

        public Int32? VALIDADE_DIAS
        {
            get { return _vALIDADE_DIAS; }
            set { _vALIDADE_DIAS = value; }
        }

        public Int32? PRAZO_DIAS
        {
            get { return _pRAZO_DIAS; }
            set { _pRAZO_DIAS = value; }
        }

        public Int32? HORAS
        {
            get { return _hORAS; }
            set { _hORAS = value; }
        }

        public Int32? PRAZO_PAGAMENTO_DIAS
        {
            get { return _pRAZO_PAGAMENTO_DIAS; }
            set { _pRAZO_PAGAMENTO_DIAS = value; }
        }

        public DateTime? DATA_CADASTRO
        {
            get { return _dATA_CADASTRO; }
            set { _dATA_CADASTRO = value; }
        }

        public String ESCOPO
        {
            get { return _eSCOPO; }
            set { _eSCOPO = value; }
        }

        public PROJETO()
        {
        }
    }
}
