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

public partial class Manager_Modulos_Plantoes_Cadastros_Listar : System.Web.UI.Page
{
    #region Variáveis
    private int Pagina;
    private int TotalPagina;
    private int PaginaExibir;
	#endregion

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
		//ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}
    
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
		ddlHospital.SelectedIndex = 0;
		ddlCNPJ.SelectedIndex = 0;
		ddlINSS.SelectedIndex = 0;
		ddlRecebido.SelectedIndex = 0;
		ddlDataIni.SelectedIndex = 0;
		this.CarregarLista();
		//ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}

	protected void btnConfirmarPagamento_Click(object sender, CommandEventArgs e)
	{
		PLANTAO_Service pService = new PLANTAO_Service();
		PLANTAO plantao = pService.Carregar(Convert.ToInt32(e.CommandArgument));
		plantao.RECEBIDO = true;
		pService.Atualizar(plantao);
		this.CarregarLista();
	}


	protected void lnkPagina_Click(object sender, EventArgs e)
    {
        this.Pagina = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        this.CarregarLista();
		//ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}

    protected void rptTable_ItemDataBound(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PLANTAO item = (PLANTAO)e.Item.DataItem;
			((Literal)e.Item.FindControl("ltlID")).Text = ((HiddenField)e.Item.FindControl("hdnID")).Value = item.PLANTAO_ID.ToString();
			((Literal)e.Item.FindControl("ltlHospital")).Text = item.HOSPITAL.NOME;
			((Literal)e.Item.FindControl("ltlValor")).Text = (item.VALOR == null) ? String.Empty : ((Decimal)item.VALOR).ToString("0.00");
			((Literal)e.Item.FindControl("ltlDataPlantao")).Text = item.DATA.Value.ToString("dd/MM/yyyy");
			((Literal)e.Item.FindControl("ltlCNPJ")).Text = (item.CNPJ == true) ? "Sim" : "Não";
			((Literal)e.Item.FindControl("ltlINSS")).Text = (item.INSS == true) ? "Sim" : "Não";
			((Literal)e.Item.FindControl("ltlRecebido")).Text = (item.RECEBIDO == true) ? "Sim" : "Não";
			((LinkButton)e.Item.FindControl("btnConfirmarPagamento")).CommandArgument = item.PLANTAO_ID.ToString();
			if(item.RECEBIDO == true)
			{
				((LinkButton)e.Item.FindControl("btnConfirmarPagamento")).CssClass = "hidden";
			}

		}
	}

    protected void rptPagina_ItemDataBound(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int item = (int)e.Item.DataItem;
            LinkButton lkbPagina = (LinkButton)e.Item.FindControl("lkbPagina");
            lkbPagina.Text = item.ToString();
            lkbPagina.CommandArgument = item.ToString();
            if (item == this.Pagina) lkbPagina.CssClass = "on";
        }
    }
    #endregion

    #region Métodos
    private void CarregarFiltro()
    {
        PLANTAO filtro = new PLANTAO();
		filtro.HOSPITAL_ID = String.IsNullOrEmpty(ddlHospital.SelectedValue) ? null : (Int32?)Convert.ToInt32(ddlHospital.SelectedValue);
		filtro.INSS = String.IsNullOrEmpty(ddlINSS.SelectedValue) ? null : (bool?)((ddlINSS.SelectedValue == "1") ? true : false);
		filtro.CNPJ = String.IsNullOrEmpty(ddlCNPJ.SelectedValue) ? null : (bool?)((ddlCNPJ.SelectedValue == "1") ? true : false);
		filtro.RECEBIDO = String.IsNullOrEmpty(ddlRecebido.SelectedValue) ? null : (bool?)((ddlRecebido.SelectedValue == "1") ? true : false);
		
		Session["PLANTAO_Filtro"] = filtro;


		//RelatorioFiltro filtro2 = new RelatorioFiltro();

		//filtro2.DATA_INI = String.IsNullOrEmpty(ddlDataIni.SelectedValue) ? new DateTime(ini.Year, ini.Month, 1) : DateTime.Parse(ddlDataIni.SelectedValue);
		//filtro2.DATA_FIM = filtro2.DATA_INI.AddMonths(1);
		//Session["RELATORIO_Filtro"] = filtro2;

	}

    private void CarregarLista()
    {
        CarregarFiltro();

		PLANTAO filtro = (PLANTAO)Session["PLANTAO_Filtro"];
		


		PLANTAO_Ordem Ordem = (Session["PLANTAO_Ordem"] != null) ? (PLANTAO_Ordem)Session["PLANTAO_Ordem"] : PLANTAO_Ordem.DATA;
		OrdemTipo OrdemTipo = (Session["PLANTAO_Ordem"] != null) ? (OrdemTipo)Session["PLANTAO_Ordem_Tipo"] : OrdemTipo.Descendente;
		Paginacao<PLANTAO> pagLista = null;

		if (!String.IsNullOrEmpty(ddlDataIni.SelectedValue))
		{
			OrdemTipo = (Session["PLANTAO_Ordem"] != null) ? (OrdemTipo)Session["PLANTAO_Ordem_Tipo"] : OrdemTipo.Ascendente;
			filtro.DATA = DateTime.Parse(ddlDataIni.SelectedValue).AddMinutes(-1);
			DateTime DATA_FIM = DateTime.Parse(ddlDataIni.SelectedValue).AddMonths(1).AddMinutes(-1);
			pagLista = new PLANTAO_Service_EXT().Listar((PLANTAO)Session["PLANTAO_Filtro"], this.Pagina, 100, Ordem, OrdemTipo, DATA_FIM);
			phValorTotal.Visible = true;
			decimal valTotal = 0;
			for (int i = 0; i < pagLista.Itens.Count; i++)
			{
				valTotal += pagLista.Itens[i].VALOR.Value;
			}
			ltlValorTotal.Text = valTotal.ToString();
		}
		else
		{
			phValorTotal.Visible = false;
			pagLista = new PLANTAO_Service().Listar((PLANTAO)Session["PLANTAO_Filtro"], this.Pagina, 10, Ordem, OrdemTipo);
		}
		

		rptTable.DataSource = pagLista.Itens;
        rptTable.DataBind();
        pnlNenhumRegistro.Visible = (pagLista.Itens.Count == 0);
        pnlRegistrosEncontrados.Visible = (pagLista.Itens.Count != 0);
        ltlQuantidadeRegistrosEncontrados.Text = pagLista.Total.ToString();
        this.TotalPagina = pagLista.Paginas;
        this.PaginaExibir = 9;
        this.CarregarPaginacao();
    }

	private void CarregarCombos()
	{
		ddlHospital.DataSource = new HOSPITAL_Service().Listar(HOSPITAL_Ordem.NOME, OrdemTipo.Ascendente);
		ddlHospital.DataTextField = "NOME";
		ddlHospital.DataValueField = "HOSPITAL_ID";
		ddlHospital.DataBind();
		ddlHospital.Items.Insert(0, new System.Web.UI.WebControls.ListItem { Value = "", Text = "" });


		RelatorioFiltro filtro = new RelatorioFiltro();
		var ini = DateTime.Now.AddYears(-1);
		filtro.DATA_INI = new DateTime(ini.Year, ini.Month, 1);
		filtro.DATA_FIM = DateTime.Now.AddYears(1); ;

		List<ListaRelatorio> lista = new _APP_ADO().GET_RELATORIO_PAGAMENTO_DATA(filtro);

		ddlDataIni.DataSource = lista;
		ddlDataIni.DataTextField = "MES_ANO";
		ddlDataIni.DataValueField = "ANO_MES_DIA";
		ddlDataIni.DataBind();
		ddlDataIni.Items.Insert(0, new System.Web.UI.WebControls.ListItem { Value = "", Text = "" });
	}


	public void CarregarPaginacao()
    {
        if (this.Pagina < 1)
        {
            this.Pagina = 1;
        }
        if (this.Pagina > this.TotalPagina)
        {
            this.Pagina = this.TotalPagina;
        }

        if ((this.PaginaExibir % 2) == 0)
        {
            this.PaginaExibir--;
        }
        int PaginasLado = (this.PaginaExibir - 1) / 2;
        int PaginaInicial = 0;
        int PaginaFinal = 0;
        if (this.TotalPagina > 1)
        {
            if (this.TotalPagina <= this.PaginaExibir)
            {
                PaginaInicial = 1;
                PaginaFinal = this.TotalPagina;
            }
            else
            {
                if ((this.TotalPagina - this.Pagina) >= PaginasLado)
                {
                    if (this.Pagina <= PaginasLado)
                    {
                        PaginaInicial = 1;
                        PaginaFinal = this.Pagina + PaginasLado + (PaginasLado - this.Pagina) + 1;
                        PaginaFinal = (PaginaFinal > this.TotalPagina) ? this.TotalPagina : PaginaFinal;
                    }
                    else
                    {
                        PaginaInicial = this.Pagina - PaginasLado;
                        PaginaFinal = this.Pagina + PaginasLado;
                    }
                }
                else
                {
                    PaginaInicial = this.Pagina - (PaginasLado + (PaginasLado - (this.TotalPagina - this.Pagina)));
                    PaginaFinal = this.TotalPagina;
                }
            }
        }
        if (this.TotalPagina > 1)
        {
            pnlPaginacao.Visible = true;
            lkbPrimeira.Attributes["style"] = "visibility:" + ((this.Pagina != 1) ? "visible" : "hidden");
            lkbAnterior.Attributes["style"] = "visibility:" + ((this.Pagina != 1) ? "visible" : "hidden");
            lkbProxima.Attributes["style"] = "visibility:" + ((this.Pagina < this.TotalPagina) ? "visible" : "hidden");
            lkbUltima.Attributes["style"] = "visibility:" + ((this.Pagina < this.TotalPagina) ? "visible" : "hidden");
            lkbPrimeira.CommandArgument = "1";
            lkbAnterior.CommandArgument = (this.Pagina - 1).ToString();
            lkbProxima.CommandArgument = (this.Pagina + 1).ToString();
            lkbUltima.CommandArgument = this.TotalPagina.ToString();
            List<int> lstPagina = new List<int>();
            for (int i = PaginaInicial; i <= PaginaFinal; i++)
            {
                lstPagina.Add(i);
            }
            rptPagina.DataSource = lstPagina;
            rptPagina.DataBind();
        }
        else
        {
            pnlPaginacao.Visible = false;
        }
    }
    #endregion
}