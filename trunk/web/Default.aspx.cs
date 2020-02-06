using System;

public partial class _Manager_Default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Response.Redirect("painel/Default.aspx", true);
	}
}