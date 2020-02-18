using System;
using System.Web.Script.Serialization;
using CRM_Blue;
using CRM_Blue.ADO;
using CRM_Blue.Entity;
using CRM_Blue.Service;

public partial class WS : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        PROCESSA_ACTION();
    }

	private async void PROCESSA_ACTION()
	{
        try
        {
            Response.AppendHeader("Access-Control-Allow-Headers", "Content-Type");
            Response.AppendHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.AppendHeader("Access-Control-Allow-Credentials", "true");
            string[] request_origin = Request.Headers.GetValues("Origin");
            if (request_origin != null)
            {
                if (request_origin.Length > 0)
                {
                    Response.AppendHeader("Access-Control-Allow-Origin", request_origin[0]);
                }
            }
            string data = Request.Form["data"];
            if (!String.IsNullOrEmpty(data))
            {
                WS_Input input = new JavaScriptSerializer().Deserialize<WS_Input>(data);
                await Services.PROCESSA_ACTION(null, null, input);
            }
        }
        catch (Exception ex)
        {
            LogError.GravarErro("WS.PROCESSA_ACTION", ex);
            Response.Write(new JavaScriptSerializer().Serialize(new { sucesso = false, motivo = "SISTEMA_INDISPONIVEL" }));
        }
	}
}