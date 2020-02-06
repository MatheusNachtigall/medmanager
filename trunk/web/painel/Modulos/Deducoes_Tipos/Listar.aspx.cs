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
using System.Web;

public partial class Manager_Modulos_Deducoes_Tipos_Listar : System.Web.UI.Page
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
		ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}
    
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
		txtNome.Text = "";
		ddlNome.SelectedIndex = 0;
		this.CarregarLista();
		ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}

	

	protected void lnkPagina_Click(object sender, EventArgs e)
    {
        this.Pagina = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        this.CarregarLista();
		ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}

    protected void rptTable_ItemDataBound(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DEDUCAO_TIPO item = (DEDUCAO_TIPO)e.Item.DataItem;
			((Literal)e.Item.FindControl("ltlID")).Text = ((HiddenField)e.Item.FindControl("hdnID")).Value = item.DEDUCAO_TIPO_ID.ToString();
			((Literal)e.Item.FindControl("ltlNome")).Text = item.NOME;
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
        DEDUCAO_TIPO filtro = new DEDUCAO_TIPO();
		filtro.NOME = String.Concat("%", txtNome.Text, "%");
		Session["DEDUCAO_TIPO_Filtro"] = filtro;
	}

    private void CarregarLista()
    {
        CarregarFiltro();
        DEDUCAO_TIPO_Ordem Ordem = (Session["DEDUCAO_TIPO_Ordem"] != null) ? (DEDUCAO_TIPO_Ordem)Session["DEDUCAO_TIPO_Ordem"] : DEDUCAO_TIPO_Ordem.DEDUCAO_TIPO_ID;
        OrdemTipo OrdemTipo = (Session["DEDUCAO_TIPO_Ordem"] != null) ? (OrdemTipo)Session["DEDUCAO_TIPO_Ordem_Tipo"] : OrdemTipo.Ascendente;
        Paginacao<DEDUCAO_TIPO> pagLista = new DEDUCAO_TIPO_Service().Listar((DEDUCAO_TIPO)Session["DEDUCAO_TIPO_Filtro"], this.Pagina, 10, Ordem, OrdemTipo);
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
		ddlNome.DataSource = new DEDUCAO_TIPO_Service().Listar(DEDUCAO_TIPO_Ordem.NOME, OrdemTipo.Ascendente);
		ddlNome.DataTextField = "NOME";
		ddlNome.DataValueField = "DEDUCAO_TIPO_ID";
		ddlNome.DataBind();
		ddlNome.Items.Insert(0, new ListItem { Value = "", Text = "" });
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