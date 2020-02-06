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

public partial class Manager_Modulos_Clientes_Cadastros_Listar : System.Web.UI.Page
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
		Session["CLIENTE_Filtro"] = null;
		txtNome.Text = String.Empty;
		ddlAgencia.SelectedIndex = 0;
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
            CLIENTE item = (CLIENTE)e.Item.DataItem;
			((Literal)e.Item.FindControl("ltlID")).Text = ((HiddenField)e.Item.FindControl("hdnID")).Value = item.CLIENTE_ID.ToString();
			((Literal)e.Item.FindControl("ltlNome")).Text = item.NOME;
            ((Literal)e.Item.FindControl("ltlAgencia")).Text = item.AGENCIA.NOME;
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
        CLIENTE filtro = new CLIENTE();
        filtro.NOME = String.Concat("%", txtNome.Text, "%");
		filtro.AGENCIA_ID = String.IsNullOrEmpty(ddlAgencia.SelectedValue) ? null : (Int32?)Convert.ToInt32(ddlAgencia.SelectedValue);
		Session["CLIENTE_Filtro"] = filtro;
    }

    private void CarregarLista()
    {
        CarregarFiltro();
        CLIENTE_Ordem Ordem = (Session["CLIENTE_Ordem"] != null) ? (CLIENTE_Ordem)Session["CLIENTE_Ordem"] : CLIENTE_Ordem.NOME;
        OrdemTipo OrdemTipo = (Session["CLIENTE_Ordem"] != null) ? (OrdemTipo)Session["CLIENTE_Ordem_Tipo"] : OrdemTipo.Ascendente;
        Paginacao<CLIENTE> pagLista = new CLIENTE_Service().Listar((CLIENTE)Session["CLIENTE_Filtro"], this.Pagina, 10, Ordem, OrdemTipo);
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
		ddlAgencia.DataSource = new AGENCIA_Service().Listar(AGENCIA_Ordem.NOME, OrdemTipo.Ascendente);
		ddlAgencia.DataTextField = "NOME";
		ddlAgencia.DataValueField = "AGENCIA_ID";
		ddlAgencia.DataBind();
		ddlAgencia.Items.Insert(0, new ListItem { Value = "", Text = "" });
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