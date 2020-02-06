using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class _SVN : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		Process.Start(new ProcessStartInfo(@"C:\Program Files\TortoiseSVN\bin\svn.exe", @"cleanup " + Server.MapPath("~/") + " --non-interactive"));
		Process.Start(new ProcessStartInfo(@"C:\Program Files\TortoiseSVN\bin\svn.exe", @"update " + Server.MapPath("~/") + " --non-interactive"));
	}
}