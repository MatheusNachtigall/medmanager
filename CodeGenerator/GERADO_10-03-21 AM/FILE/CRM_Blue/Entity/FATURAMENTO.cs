using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Entidade da tabela FATURAMENTO
/// </summary>
namespace CRM_Blue.Entity
{
    [Serializable()]
    public class FATURAMENTO
    {
        private Int32? _fATURAMENTO_ID;
        private Int32? _uSUARIO_ID;
        private USUARIO _USUARIO;
        private Int32? _pROJETO_ID;
        private PROJETO _PROJETO;
        private Decimal? _vALOR;
        private Int32? _sTATUS;
        private DateTime? _dATA_FATURAMENTO;
        private DateTime? _dATA_RECEBIMENTO;
        private DateTime? _dATA_CADASTRO;

        public Int32? FATURAMENTO_ID
        {
            get { return _fATURAMENTO_ID; }
            set { _fATURAMENTO_ID = value; }
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

        public Int32? PROJETO_ID
        {
            get { return _pROJETO_ID; }
            set { _pROJETO_ID = value; }
        }

        public PROJETO PROJETO
        {
            get { return _PROJETO; }
            set { _PROJETO = value; }
        }

        public Decimal? VALOR
        {
            get { return _vALOR; }
            set { _vALOR = value; }
        }

        public Int32? STATUS
        {
            get { return _sTATUS; }
            set { _sTATUS = value; }
        }

        public DateTime? DATA_FATURAMENTO
        {
            get { return _dATA_FATURAMENTO; }
            set { _dATA_FATURAMENTO = value; }
        }

        public DateTime? DATA_RECEBIMENTO
        {
            get { return _dATA_RECEBIMENTO; }
            set { _dATA_RECEBIMENTO = value; }
        }

        public DateTime? DATA_CADASTRO
        {
            get { return _dATA_CADASTRO; }
            set { _dATA_CADASTRO = value; }
        }

        public FATURAMENTO()
        {
        }
    }
}
