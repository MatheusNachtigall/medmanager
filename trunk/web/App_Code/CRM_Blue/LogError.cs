using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.IO;

/// <summary>
/// Classe responsável pelo log de erros
/// </summary>
namespace CRM_Blue
{
    public class LogError
    {
        public LogError() { }

        public static void GravarErro(String Origem, Exception err)
        {
            try
            {
                StreamWriter ErrorLog;
                string FileName = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ErrorLogFile"]);
                if (!File.Exists(FileName))
                {
                    ErrorLog = File.CreateText(FileName);
                }
				else 
				{
					ErrorLog = new StreamWriter(FileName, true);
				}
                ErrorLog.WriteLine("Data: " + DateTime.Now.ToString());
                ErrorLog.WriteLine("Origem: " + Origem);
                ErrorLog.WriteLine("Mensagem: " + err);
                ErrorLog.WriteLine("-------------------------------------------");
                ErrorLog.WriteLine(" ");
                ErrorLog.Close();
                ErrorLog = null;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
