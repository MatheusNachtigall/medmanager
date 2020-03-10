using System;
using System.IO;
using System.Web;
using Utilities;
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
            string acao = Request.Form["ACAO"];
            if (!String.IsNullOrEmpty(acao))
            {
                if (acao.Equals("UPLOAD_MEDIA"))
                {
                    if (Request.Files.Count > 0)
                    {
                        string path = "";
                        string humanFriendlyFileName = Text.ToHumanFriendly(Path.GetFileNameWithoutExtension(Request.Files[0].FileName), "_");
                        string ext = Path.GetExtension(Request.Files[0].FileName);
                        //string fileName = String.Concat("TMP_", Request.Form["WS_USUARIO_ID"], "_", Request.Form["ORDEM"], "_", humanFriendlyFileName, ext);
                        string fileName = String.Concat("TMP_", Request.Form["ORDEM"], "_", humanFriendlyFileName, ext);
                        if (".PDF .MP4 .M4V .OGV .MPEG .MPG .WMV .MOV .OGM .WEBM .ASX .AVI .JPG .JPEG .PNG .DIB .WEBP .JPEG .SVGZ .GIF .ICO .SVG .TIF .XBM .BMP .JFIF .PJPEG .PJP .TIFF".IndexOf(ext.ToUpper()) != -1)
                        {
                            path = HttpContext.Current.Server.MapPath("~/Uploads/");
                        }
                        else
                        {
                            Response.Write(new JavaScriptSerializer().Serialize(new { sucesso = false, motivo = "FORMATO_INCORRETO" }));
                            return;
                        }

                        if (File.Exists(String.Concat(path, fileName)))
                        {
                            File.Delete(String.Concat(path, fileName));
                        }
                        Request.Files[0].SaveAs(String.Concat(path, fileName));
                        //Response.Write(new JavaScriptSerializer().Serialize(new { sucesso = true, fileName = fileName }));
                        return;
                    }
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
            //Response.Write(new JavaScriptSerializer().Serialize(new { sucesso = false, motivo = "SISTEMA_INDISPONIVEL" }));
        }
	}
}