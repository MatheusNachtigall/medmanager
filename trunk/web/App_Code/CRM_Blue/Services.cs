using System;
using System.Collections.Generic;

namespace CRM_Blue
{

	public class ListaRelatorio
	{
		public int mes { get; set; }
		public int ano { get; set; }
		public string mes_nome { get; set; }
		public string ano_mes_dia { get; set; }
		public string mes_ano { get; set; }
		public decimal valor_mes { get; set; }
		public decimal valor_mes_inss { get; set; }
	}

	[Serializable()]
	public class RelatorioFiltro
	{
		public DateTime DATA_INI { get; set; }
		public DateTime DATA_FIM { get; set; }

	}

}