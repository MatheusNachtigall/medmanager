using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM_Blue.Entity;
using CRM_Blue.Service;
using CRM_Blue.Enumerator;
using CRM_Blue;
using System.Text;
using Utilities;
using System.IO;
using TemplateEngine.Docx;
using System.Web;
using CRM_Blue.ADO;
using Newtonsoft.Json;


public partial class Manager_Modulos_Relatorios_Faturamento_Listar : System.Web.UI.Page
{
    #region Eventos
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
            Response.Redirect("../../Default.aspx");
            Response.End();
        }

		if (!Page.IsPostBack)
        {
            this.CarregarCombos();
            this.CarregarLista();
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.CarregarLista();
		ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}
    
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
		ddlDataIni.SelectedIndex = 0;
		ddlDataFim.SelectedIndex = 0;
		this.CarregarLista();
		ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}

    protected void rptTable_ItemDataBound(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ListaRelatorio item = (ListaRelatorio)e.Item.DataItem;
            ((HiddenField)e.Item.FindControl("hdnID")).Value = string.Concat(item.mes.ToString(), item.ano.ToString());
			((Literal)e.Item.FindControl("ltlMes")).Text = string.Concat(item.mes_nome.ToString(), "/", item.ano.ToString());
			((Literal)e.Item.FindControl("ltlValor")).Text = (item.valor_mes == null) ? String.Empty : ((Decimal)item.valor_mes).ToString("0.00");
		}
    }
    #endregion

    #region Métodos
    private void CarregarFiltro()
    {
		RelatorioFiltro filtro = new RelatorioFiltro();

		var ini = DateTime.Now.AddYears(-1);
		filtro.DATA_INI = String.IsNullOrEmpty(ddlDataIni.SelectedValue) ? new DateTime(ini.Year, ini.Month, 1) : DateTime.Parse(ddlDataIni.SelectedValue);
		filtro.DATA_FIM = String.IsNullOrEmpty(ddlDataFim.SelectedValue) ? DateTime.Now : DateTime.Parse(ddlDataFim.SelectedValue).AddMonths(1);

		Session["RELATORIO_Filtro"] = filtro;
    }

    private void CarregarLista()
    {
        CarregarFiltro();

		List<ListaRelatorio> lista = new _APP_ADO().GET_RELATORIO_FATURAMENTO_DATA( (RelatorioFiltro)Session["RELATORIO_Filtro"] );

		hdDados.Value = JsonConvert.SerializeObject(lista);
		hdNome.Value = "Faturamento";

		lista.Reverse();
		rptTable.DataSource = lista;
        rptTable.DataBind();

		pnlNenhumRegistro.Visible = (lista.Count == 0);
        pnlRegistrosEncontrados.Visible = (lista.Count != 0);
        ltlQuantidadeRegistrosEncontrados.Text = lista.Count.ToString();

    }

	private void CarregarCombos()
	{
		RelatorioFiltro filtro = new RelatorioFiltro();
		var ini = DateTime.Now.AddYears(-1);
		filtro.DATA_INI = new DateTime(ini.Year, ini.Month, 1);
		filtro.DATA_FIM = DateTime.Now;

		List<ListaRelatorio> lista = new _APP_ADO().GET_RELATORIO_FATURAMENTO_DATA(filtro);

		ddlDataIni.DataSource = lista;
		ddlDataIni.DataTextField = "MES_ANO";
		ddlDataIni.DataValueField = "ANO_MES_DIA";
		ddlDataIni.DataBind();
		ddlDataIni.Items.Insert(0, new System.Web.UI.WebControls.ListItem { Value = "", Text = "" });

		ddlDataFim.DataSource = lista;
		ddlDataFim.DataTextField = "MES_ANO";
		ddlDataFim.DataValueField = "ANO_MES_DIA";
		ddlDataFim.DataBind();
		ddlDataFim.Items.Insert(0, new System.Web.UI.WebControls.ListItem { Value = "", Text = "" });
	}

	#endregion
}