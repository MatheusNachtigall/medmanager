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
using System.IO;

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
		public static string[] free_actions = new string[] {"CADASTRO", "LOGIN", "ESQUECI_A_SENHA" };
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
						if (input.action.Equals("LOAD_PLANTOES")) ret = LOAD_PLANTOES(input);
						//if (input.action.Equals("LOAD_HOSPITAIS")) ret = LOAD_HOSPITAIS(input);
						if (input.action.Equals("GRAFICO_MES")) ret = GRAFICO_MES(input);
						if (input.action.Equals("MARCAR_PLANTAO_RECEBIDO")) ret = MARCAR_PLANTAO_RECEBIDO(input);
						if (input.action.Equals("EXCLUIR_PLANTAO")) ret = EXCLUIR_PLANTAO(input);
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

		public static string LOAD_PLANTOES(WS_Input ws_input)
		{
			//LOGIN_DATA input = new JavaScriptSerializer().Deserialize<LOGIN_DATA>(ws_input.data);
			List <PLANTAO> lstPlantao = new PLANTAO_Service().Listar();
            List<ANEXO> lstAnexo = null;
            ANEXO_Service aService = new ANEXO_Service();
			List<object> lstRetorno = new List<object>();
			for (int i = 0; i < lstPlantao.Count; i++)
			{
                lstAnexo = aService.Listar(new ANEXO() { PLANTAO_ID = lstPlantao[i].PLANTAO_ID }, ANEXO_Ordem.ORDEM, OrdemTipo.Ascendente);
                lstRetorno.Add(new
                {
                    PLANTAO_ID = lstPlantao[i].PLANTAO_ID,
                    HOSPITAL_ID = lstPlantao[i].HOSPITAL_ID,
                    HOSPITAL = lstPlantao[i].HOSPITAL.NOME,
                    VALOR = (Math.Round((double)lstPlantao[i].VALOR, 2)),
                    DATA = ((DateTime)lstPlantao[i].DATA).ToString("MM-dd-yyyy"),
                    HORARIO = lstPlantao[i].HORARIO,
                    PERIODO = lstPlantao[i].PERIODO,
                    CNPJ = lstPlantao[i].CNPJ,
                    INSS = lstPlantao[i].INSS,
                    COR = lstPlantao[i].HOSPITAL.COR,
                    RECEBIDO = lstPlantao[i].RECEBIDO,
                    ANEXO = lstAnexo
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
			filtro.DATA = firstDayOfMonth;

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
            public string[] DATA { get; set; }
            public string[] HORARIO { get; set; }
            public int[] PERIODO { get; set; }
            public string[] VALOR { get; set; }
            public bool INSS { get; set; }
            public bool CNPJ { get; set; }
            public string[] MEDIA { get; set; }
            public int PLANTAO_ID { get; set; }
        }
        public static string INSERIR_PLANTAO(WS_Input ws_input)
        {
            INSERIR_PLANTAO_DATA input = new JavaScriptSerializer().Deserialize<INSERIR_PLANTAO_DATA>(ws_input.data);

            PLANTAO plantao = null;
            PLANTAO_Service pService = new PLANTAO_Service();

            for (int i = 0; i < input.DATA.Length; i++)
            {
                try
                {
                    plantao = new PLANTAO();
                    plantao.HOSPITAL_ID = input.HOSPITAL_ID;
                    plantao.DATA = DateTime.ParseExact(input.DATA[i], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    plantao.HORARIO = input.HORARIO[i];
                    plantao.PERIODO = input.PERIODO[i];
                    plantao.VALOR = Convert.ToDecimal(input.VALOR[i]);
                    plantao.DATA_PAGAMENTO = null;
                    plantao.DATA_CADASTRO = DateTime.Now;
                    plantao.INSS = input.INSS;
                    plantao.CNPJ = input.CNPJ;
                    plantao.RECEBIDO = false;

                    plantao = pService.Inserir(plantao);
                }
                catch (Exception)
                {
                    return new JavaScriptSerializer().Serialize(new { sucesso = false });
                }
            }
            return new JavaScriptSerializer().Serialize(new { sucesso = true });
        }


        public static string EDITAR_PLANTAO(WS_Input ws_input)
        {
            String ret;
            INSERIR_PLANTAO_DATA input = new JavaScriptSerializer().Deserialize<INSERIR_PLANTAO_DATA>(ws_input.data);

            PLANTAO plantao = null;
            PLANTAO_Service pService = new PLANTAO_Service();

            if (input.DATA.Length > 1)
            {
                ret = new JavaScriptSerializer().Serialize(new { sucesso = false });
            }
            else 
            {
                plantao = pService.Carregar(input.PLANTAO_ID);
                try
                {
                    plantao.HOSPITAL_ID = input.HOSPITAL_ID;
                    plantao.DATA = DateTime.ParseExact(input.DATA[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    plantao.HORARIO = input.HORARIO[0];
                    plantao.PERIODO = input.PERIODO[0];
                    plantao.VALOR = Convert.ToDecimal(input.VALOR[0]);
                    plantao.DATA_PAGAMENTO = null;
                    plantao.DATA_CADASTRO = DateTime.Now;
                    plantao.INSS = input.INSS;
                    plantao.CNPJ = input.CNPJ;
                    plantao.RECEBIDO = false;
                    plantao = pService.Atualizar(plantao);
                    ret = new JavaScriptSerializer().Serialize(new { sucesso = true });
                }
                catch (Exception)
                {
                    ret = new JavaScriptSerializer().Serialize(new { sucesso = false });
                }
            }
            return ret;
        }


        //public static string INSERIR_PLANTAO(WS_Input ws_input)
        //{
        //    String ret = "";
        //    INSERIR_PLANTAO_DATA input = new JavaScriptSerializer().Deserialize<INSERIR_PLANTAO_DATA>(ws_input.data);

        //    PLANTAO plantao = null;
        //    PLANTAO_Service pService = new PLANTAO_Service();

        //    if (input.DATA.Length > 1)
        //    {
        //        for (int i = 0; i < input.DATA.Length; i++)
        //        {
        //            try
        //            {
        //                plantao = new PLANTAO();
        //                plantao.HOSPITAL_ID = input.HOSPITAL_ID;
        //                plantao.DATA = DateTime.ParseExact(input.DATA[i], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //                plantao.HORARIO = input.HORARIO[i];
        //                plantao.PERIODO = input.PERIODO[i];
        //                plantao.VALOR = Convert.ToDecimal(input.VALOR[i]);
        //                plantao.DATA_PAGAMENTO = null;
        //                plantao.DATA_CADASTRO = DateTime.Now;
        //                plantao.INSS = input.INSS;
        //                plantao.CNPJ = input.CNPJ;
        //                plantao.RECEBIDO = false;

        //                plantao = pService.Inserir(plantao);
        //            }
        //            catch (Exception)
        //            {
        //                ret = new JavaScriptSerializer().Serialize(new { sucesso = false });
        //            }
        //        }
        //    }


        //    if (input.PLANTAO_ID == 0)
        //    {
        //        plantao = new PLANTAO();
        //    }
        //    else
        //    {
        //        plantao = pService.Carregar(input.PLANTAO_ID);
        //    }
        //    try
        //    {
        //        plantao.HOSPITAL_ID = input.HOSPITAL_ID;
        //        plantao.VALOR = Convert.ToDecimal(input.VALOR);
        //        plantao.DATA = DateTime.ParseExact(input.DATA, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        //        plantao.DATA_PAGAMENTO = null;
        //        plantao.PERIODO = input.PERIODO;
        //        plantao.DATA_CADASTRO = DateTime.Now;
        //        plantao.INSS = input.INSS;
        //        plantao.CNPJ = input.CNPJ;
        //        plantao.RECEBIDO = false;

        //        if (input.PLANTAO_ID == 0)
        //        {
        //            plantao = pService.Inserir(plantao);
        //        }
        //        else
        //        {
        //            plantao = pService.Atualizar(plantao);
        //        }

        //        ret = new JavaScriptSerializer().Serialize(new { sucesso = true });
        //    }
        //    catch (Exception)
        //    {
        //        ret = new JavaScriptSerializer().Serialize(new { sucesso = false });
        //    }

        //    if (input.PLANTAO_ID != 0)
        //    {
        //        List<ANEXO> oldAnexos = null;
        //        oldAnexos = new ANEXO_Service().Listar(new ANEXO() { PLANTAO_ID = input.PLANTAO_ID });

        //        //Se existem anexos anteriores vinculados a esse plantao...excluir
        //        if (oldAnexos != null)
        //        {
        //            new ANEXO_Service().Excluir(new ANEXO() { PLANTAO_ID = input.PLANTAO_ID });
        //        }
        //    }

        //    if (input.MEDIA != null)
        //    {
        //        if (input.MEDIA.Length > 0)
        //        {
        //            for (int i_media = 0; i_media < input.MEDIA.Length; i_media++)
        //            {
        //                //TMP_<ID>_<ORDEM>_<FILENAME>
        //                string ext = Path.GetExtension(input.MEDIA[i_media]).ToUpper();
        //                int sizeToRemove = 4; // 'TMP_'
        //                string FileName = input.MEDIA[i_media].Remove(0, sizeToRemove); //<ORDEM>_<FILENAME>.JPG
        //                int anexo_ordem = Int32.Parse(FileName.Substring(0, FileName.IndexOf("_")));
        //                FileName = FileName.Remove(0, (FileName.IndexOf("_") + 1)); //<FILENAME>.JPG
        //                //string newFileName = String.Concat(plantao.PLANTAO_ID.ToString(), "_", tempFileName);
        //                int anexo_tipo = 0;
        //                string temp_path = HttpContext.Current.Server.MapPath("~/Uploads/");
        //                string path = String.Concat(HttpContext.Current.Server.MapPath("~/Uploads/"), plantao.PLANTAO_ID, '/');
        //                try
        //                {
        //                    if (!Directory.Exists(path))
        //                    {
        //                        Directory.CreateDirectory(path);
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    LogError.GravarErro("Creating Directory: ", ex);
        //                }

        //                //TODO: CRIAR UMA PASTA PARA CADA USUARIO E DEPOIS UMA PASTA PARA CADA PLANTAO
        //                if (".JPG .JPEG .PNG .DIB .WEBP .JPEG .SVGZ .GIF .ICO .SVG .TIF .XBM .BMP .JFIF .PJPEG .PJP .TIFF".IndexOf(ext) != -1)
        //                {
        //                    //path = HttpContext.Current.Server.MapPath("~/Uploads/Imagem/");
        //                    anexo_tipo = 1;
        //                }
        //                if (".MP4 .M4V .OGV .MPEG .MPG .WMV .MOV .OGM .WEBM .ASX .AVI".IndexOf(ext) != -1)
        //                {
        //                    //path = HttpContext.Current.Server.MapPath("~/Uploads/Video/");
        //                    anexo_tipo = 2;
        //                }
        //                if (".PDF".IndexOf(ext) != -1)
        //                {
        //                    //path = HttpContext.Current.Server.MapPath("~/Uploads/Pdf/");
        //                    anexo_tipo = 3;
        //                }

        //                if (File.Exists(String.Concat(temp_path, input.MEDIA[i_media])))
        //                {
        //                    try
        //                    {
        //                        File.Move(String.Concat(temp_path, input.MEDIA[i_media]), String.Concat(path, FileName));
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        LogError.GravarErro("Saving FILE: ", ex);
        //                    }
        //                }

        //                ANEXO anexo = new ANEXO();
        //                anexo.PLANTAO_ID = plantao.PLANTAO_ID;
        //                anexo.TIPO = anexo_tipo;
        //                anexo.ARQUIVO = FileName;
        //                anexo.ORDEM = anexo_ordem;
        //                new ANEXO_Service().Inserir(anexo);
        //            }
        //        }
        //    }
        //    return ret;
        //}






        public class MARCAR_PLANTAO_RECEBIDO_DATA
        {
            public int PLANTAO_ID { get; set; }
        }
        public static string MARCAR_PLANTAO_RECEBIDO(WS_Input ws_input)
        {
            MARCAR_PLANTAO_RECEBIDO_DATA input = new JavaScriptSerializer().Deserialize<MARCAR_PLANTAO_RECEBIDO_DATA>(ws_input.data);
            PLANTAO_Service pService = new PLANTAO_Service();
            PLANTAO plantao = pService.Carregar(input.PLANTAO_ID);
            String ret = "";
            try
            {
                plantao.RECEBIDO = true;
                pService.Atualizar(plantao);
                ret = new JavaScriptSerializer().Serialize(new { sucesso = true });
            }
            catch (Exception)
            {
                ret = new JavaScriptSerializer().Serialize(new { sucesso = false , motivo = "N�o foi poss�vel atualizar o plant�o."});
            }
            return ret;
        }

        public class EXCLUIR_PLANTAO_DATA
        {
            public int PLANTAO_ID { get; set; }
        }
        public static string EXCLUIR_PLANTAO(WS_Input ws_input)
        {
            EXCLUIR_PLANTAO_DATA input = new JavaScriptSerializer().Deserialize<EXCLUIR_PLANTAO_DATA>(ws_input.data);
            String ret = "";
            try
            {
                new ANEXO_Service().Excluir(new ANEXO() { PLANTAO_ID = input.PLANTAO_ID });
                new PLANTAO_Service().Excluir(new PLANTAO() { PLANTAO_ID = input.PLANTAO_ID });

                ret = new JavaScriptSerializer().Serialize(new { sucesso = true });
            }
            catch (Exception)
            {
                ret = new JavaScriptSerializer().Serialize(new { sucesso = false, motivo = "N�o foi poss�vel excluir o plant�o." });
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