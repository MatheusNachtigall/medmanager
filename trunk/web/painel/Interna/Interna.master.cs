using CRM_Blue.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Manager_Interna_Interna : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        USUARIO session = null;
		try
		{
			session = (USUARIO)Session["usuario"];

        }
		catch (Exception)
		{
		}

        if (session == null)
        {
            Response.Redirect("~/painel/Default.aspx");
			return;
        }
    }
}
