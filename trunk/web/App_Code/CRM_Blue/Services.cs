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
						if (input.action.Equals("INSERIR_PLANTAO")) ret = INSERIR_PLANTAO(input);
						if (input.action.Equals("TESTE_CONNECT")) ret = TESTE_CONNECT(input);
						if (input.action.Equals("LOAD_PLANTOES")) ret = LOAD_PLANTOES(input);
						//if (input.action.Equals("LOAD_HOSPITAIS")) ret = LOAD_HOSPITAIS(input);
						if (input.action.Equals("GRAFICO_MES")) ret = GRAFICO_MES(input);
						if (input.action.Equals("MARCAR_PLANTAO_RECEBIDO")) ret = MARCAR_PLANTAO_RECEBIDO(input);
						if (input.action.Equals("EXCLUIR_PLANTAO")) ret = EXCLUIR_PLANTAO(input);
						//if (input.action.Equals("INSERIR_PLANTAO")) ret = INSERIR_PLANTAO(input);
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
                    PLANTAO_ID = lstPlantao[i].PLANTAO_ID,
                    HOSPITAL_ID = lstPlantao[i].HOSPITAL_ID,
					HOSPITAL = lstPlantao[i].HOSPITAL.NOME,
					VALOR = lstPlantao[i].VALOR,
					DATA = ((DateTime)lstPlantao[i].DATA_PLANTAO).ToString("MM-dd-yyyy"),
					COR = lstPlantao[i].HOSPITAL.COR,
                    RECEBIDO = lstPlantao[i].RECEBIDO
				});
			}

			String ret = new JavaScriptSerializer().Serialize(new { sucesso = true , lstPlantao = lstRetorno });
			return ret;
		}


		//public static string LOAD_HOSPITAIS(WS_Input ws_input)
		//{
		//	//LOGIN_DATA input = new JavaScriptSerializer().Deserialize<LOGIN_DATA>(ws_input.data);
		//	List<PLANTAO> lstPlantao = new PLANTAO_Service().Listar();

		//	List<object> lstRetorno = new List<object>();
		//	for (int i = 0; i < lstPlantao.Count; i++)
		//	{
		//		lstRetorno.Add(new
		//		{
		//			HOSPITAL = lstPlantao[i].HOSPITAL.NOME,
		//			VALOR = lstPlantao[i].VALOR,
		//			DATA = ((DateTime)lstPlantao[i].DATA_PLANTAO).ToString("MM-dd-yyyy"),
		//			COR = lstPlantao[i].HOSPITAL.COR
		//		});
		//	}

		//	String ret = new JavaScriptSerializer().Serialize(new { sucesso = true, lstPlantao = lstRetorno });
		//	return ret;
		//}


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

        public class INSERIR_PLANTAO_DATA
        {
            public int HOSPITAL_ID { get; set; }
            public string DATA_PLANTAO { get; set; }
            public string DATA_PAGAMENTO { get; set; }
            public string VALOR { get; set; }
            public bool INSS { get; set; }
            public bool CNPJ { get; set; }
        }
        public static string INSERIR_PLANTAO(WS_Input ws_input)
        {
            INSERIR_PLANTAO_DATA input = new JavaScriptSerializer().Deserialize<INSERIR_PLANTAO_DATA>(ws_input.data);
            PLANTAO plantao = new PLANTAO();
            String ret = "";
            try
            {
                plantao.HOSPITAL_ID = input.HOSPITAL_ID;
                plantao.VALOR = Convert.ToDecimal(input.VALOR);
                plantao.DATA_PLANTAO = DateTime.ParseExact(input.DATA_PLANTAO, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                plantao.DATA_PAGAMENTO = DateTime.ParseExact(input.DATA_PAGAMENTO, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                plantao.DATA_CADASTRO = DateTime.Now;
                plantao.INSS = input.INSS;
                plantao.CNPJ = input.CNPJ;

                PLANTAO_Service pService = new PLANTAO_Service();
                pService.Inserir(plantao);
                ret = new JavaScriptSerializer().Serialize(new { sucesso = true });
            }
            catch (Exception)
            {
                ret = new JavaScriptSerializer().Serialize(new { sucesso = false });
            }
            return ret;
        }

        public class MARCAR_PLANTAO_RECEBIDO_DATA
        {
            public int HOSPITAL_ID { get; set; }
        }
        public static string MARCAR_PLANTAO_RECEBIDO(WS_Input ws_input)
        {
            MARCAR_PLANTAO_RECEBIDO_DATA input = new JavaScriptSerializer().Deserialize<MARCAR_PLANTAO_RECEBIDO_DATA>(ws_input.data);
            PLANTAO_Service pService = new PLANTAO_Service();
            PLANTAO plantao = pService.Carregar(input.HOSPITAL_ID);
            String ret = "";
            try
            {
                plantao.RECEBIDO = true;
                pService.Atualizar(plantao);
                ret = new JavaScriptSerializer().Serialize(new { sucesso = true });
            }
            catch (Exception)
            {
                ret = new JavaScriptSerializer().Serialize(new { sucesso = false , motivo = "Não foi possível atualizar o plantão."});
            }
            return ret;
        }

        public class EXCLUIR_PLANTAO_DATA
        {
            public int HOSPITAL_ID { get; set; }
        }
        public static string EXCLUIR_PLANTAO(WS_Input ws_input)
        {
            MARCAR_PLANTAO_RECEBIDO_DATA input = new JavaScriptSerializer().Deserialize<MARCAR_PLANTAO_RECEBIDO_DATA>(ws_input.data);
            PLANTAO_Service pService = new PLANTAO_Service();
            PLANTAO plantao = pService.Carregar(input.HOSPITAL_ID);
            String ret = "";
            try
            {
                plantao.RECEBIDO = true;
                pService.Excluir(plantao);
                ret = new JavaScriptSerializer().Serialize(new { sucesso = true });
            }
            catch (Exception)
            {
                ret = new JavaScriptSerializer().Serialize(new { sucesso = false, motivo = "Não foi possível excluir o plantão." });
            }
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