using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Classe responsável pela Paginação
/// </summary>
namespace #NAMESPACE#
{
    [Serializable]
    public class Paginacao<T>
    {
        private List<T> _itens;
        private int _total;
        private int _itensPorPagina;

        public Paginacao() { }

        public Paginacao(List<T> itens, int total)
        {
            _itens = itens;
            _total = total;
        }

        public List<T> Itens
        {
            get { return _itens; }
            set { _itens = value; }
        }

        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public int ItensPorPagina
        {
            get { return _itensPorPagina; }
            set { _itensPorPagina = value; }
        }

        public int Paginas
        {
            get
            {
                return (int)Math.Ceiling((double)_total / (double)_itensPorPagina);
            }
        }
    }
}