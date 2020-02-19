using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Utilities;
using System.Globalization;
using System.Text.RegularExpressions;
using CRM_Blue;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;
using CRM_Blue.Service;




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

	public class Services
	{
		public static string[] free_actions = new string[] { "CONSULTAR_CPF", "CADASTRO", "LOGIN", "ESQUECI_A_SENHA", "BUSCAR_PRESTADOR", "BUSCAR_PESSOA", "GRAVA_PRESTADOR_FAKE" };
		public static async Task PROCESSA_ACTION(WebSocket webSocket, CancellationToken? cancellationToken, WS_Input input)
		{
			String jsonRet = String.Empty;

			bool valid = true;
			if (Array.IndexOf(free_actions, input.action) == -1)
			{
				//if (!GetToken(input.ambiente, input.id).Equals(input.token))
				//{
				//	valid = false;
				//}
			}

			if (valid)
			{
				String ret = String.Empty;

				if (String.IsNullOrEmpty(ret))
				{
					try
					{
						if (input.action.Equals("LOGIN")) ret = LOGIN(input);
						if (input.action.Equals("TESTE_CONNECT")) ret = TESTE_CONNECT(input);
						if (input.action.Equals("LOAD_PLANTOES")) ret = LOAD_PLANTOES(input);
						if (input.action.Equals("GRAFICO_MES")) ret = GRAFICO_MES(input);
						
						
					}
					catch (Exception ex)
					{
						LogError.GravarErro("Services.PROCESSA_ACTION", ex);
						throw;
					}
				}

				jsonRet = new JavaScriptSerializer().Serialize(new { sucesso = true, hash = input.hash, data = ret });
			}
			else
			{
				jsonRet = new JavaScriptSerializer().Serialize(new { sucesso = false, motivo = "SESSAO_INVALIDA" });
			}

			if (webSocket == null)
			{
				HttpContext.Current.Response.Write(jsonRet);
			}
			else
			{
				await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonRet)), WebSocketMessageType.Text, true, (CancellationToken)cancellationToken);
			}
		}

		

		public class LOGIN_DATA
		{
			public string cpf { get; set; }
		}
		public static string LOGIN(WS_Input ws_input)
		{
			//LOGIN_DATA input = new JavaScriptSerializer().Deserialize<LOGIN_DATA>(ws_input.data);

			//String ret = new JavaScriptSerializer().Serialize(new { status = (item != null) ? 1 : 0 });
			//return ret;
			return "";
		}
		public static string TESTE_CONNECT(WS_Input ws_input)
		{
			//LOGIN_DATA input = new JavaScriptSerializer().Deserialize<LOGIN_DATA>(ws_input.data);

			String ret = new JavaScriptSerializer().Serialize(new { sucesso = true });
			return ret;
		}
		public static string LOAD_PLANTOES(WS_Input ws_input)
		{
			//LOGIN_DATA input = new JavaScriptSerializer().Deserialize<LOGIN_DATA>(ws_input.data);
			List<PLANTAO> lstPlantao = new PLANTAO_Service().Listar();

			List<object> lstRetorno = new List<object>();
			for (int i = 0; i < lstPlantao.Count; i++)
			{
				lstRetorno.Add(new
				{
					HOSPITAL = lstPlantao[i].HOSPITAL.NOME,
					VALOR = lstPlantao[i].VALOR,
					COR = lstPlantao[i].HOSPITAL.COR
				});
			}

			String ret = new JavaScriptSerializer().Serialize(new { sucesso = true , lstPlantao = lstRetorno });
			return ret;
		}

		public static string GRAFICO_MES(WS_Input ws_input)
		{
			DateTime date = DateTime.Now;
			DateTime firstDayOfMonth = new DateTime(date.Year, date.Month, 1).AddMinutes(-1);
			DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddMinutes(-1);

			PLANTAO filtro = new PLANTAO();
			filtro.DATA_PLANTAO = firstDayOfMonth;

			List<PLANTAO> lstPlantaoMes = new PLANTAO_Service_EXT().Listar(filtro, lastDayOfMonth);
			
			List<object> lstRetorno = new List<object>();
			for (int i = 0; i < lstPlantaoMes.Count; i++)
			{
				lstRetorno.Add(new
				{
					HOSPITAL = lstPlantaoMes[i].HOSPITAL.NOME,
					VALOR = lstPlantaoMes[i].VALOR,
					COR = lstPlantaoMes[i].HOSPITAL.COR
				});
			}

			String ret = new JavaScriptSerializer().Serialize(new { sucesso = true , lstPlantao = lstRetorno });
			return ret;
		}

	}

	public class WS_Input
	{
		public int ambiente { get; set; }
		public string action { get; set; }
		public long id { get; set; }
		public string token { get; set; }
		public string data { get; set; }
		public string hash { get; set; }
		public bool reenvio { get; set; }
	}

}