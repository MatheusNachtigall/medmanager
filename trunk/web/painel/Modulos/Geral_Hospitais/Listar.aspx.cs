﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM_Blue.Entity;
using CRM_Blue.Service;
using CRM_Blue.Enumerator;
using CRM_Blue;
using System.Text;
using Utilities;

public partial class Manager_Modulos_Geral_Hospitais_Listar : System.Web.UI.Page
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
        txtNome.Text = String.Empty;
		ddlNome.SelectedIndex = 0;
		txtCidade.Text = String.Empty;
		ddlCidade.SelectedIndex = 0;
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
            HOSPITAL item = (HOSPITAL)e.Item.DataItem;
            ((Literal)e.Item.FindControl("ltlID")).Text = ((HiddenField)e.Item.FindControl("hdnID")).Value = item.HOSPITAL_ID.ToString();
			((Literal)e.Item.FindControl("ltlNome")).Text = item.NOME;
            ((Literal)e.Item.FindControl("ltlCidade")).Text = item.CIDADE.ToString();
			((Literal)e.Item.FindControl("ltlCor")).Text = item.COR.ToString();
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
		HOSPITAL filtro = new HOSPITAL();
        filtro.NOME = String.Concat("%", txtNome.Text, "%");
		filtro.CIDADE = String.Concat("%", txtCidade.Text, "%");
		Session["HOSPITAL_Filtro"] = filtro;
    }

    private void CarregarLista()
    {
        CarregarFiltro();
        HOSPITAL_Ordem Ordem = (Session["HOSPITAL_Ordem"] != null) ? (HOSPITAL_Ordem)Session["HOSPITAL_Ordem"] : HOSPITAL_Ordem.NOME;
        OrdemTipo OrdemTipo = (Session["HOSPITAL_Ordem"] != null) ? (OrdemTipo)Session["HOSPITAL_Ordem_Tipo"] : OrdemTipo.Ascendente;
        Paginacao<HOSPITAL> pagLista = new HOSPITAL_Service().Listar((HOSPITAL)Session["HOSPITAL_Filtro"], this.Pagina, 10, Ordem, OrdemTipo);
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
		ddlNome.DataSource = new HOSPITAL_Service().Listar(HOSPITAL_Ordem.NOME, OrdemTipo.Ascendente);
		ddlNome.DataTextField = "NOME";
		ddlNome.DataValueField = "HOSPITAL_ID";
		ddlNome.DataBind();
		ddlNome.Items.Insert(0, new ListItem { Value = "", Text = "" });

		ddlCidade.DataSource = new HOSPITAL_Service().Listar(HOSPITAL_Ordem.CIDADE, OrdemTipo.Ascendente);
		ddlCidade.DataTextField = "CIDADE";
		ddlCidade.DataValueField = "CIDADE";
		ddlCidade.DataBind();
		ddlCidade.Items.Insert(0, new ListItem { Value = "", Text = "" });
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