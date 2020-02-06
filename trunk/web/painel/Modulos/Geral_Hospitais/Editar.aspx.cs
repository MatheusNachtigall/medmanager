﻿using CRM_Blue.Service;
using CRM_Blue.Entity;
using CRM_Blue.Enumerator;
using System;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utilities;
using System.Collections.Generic;

public partial class Manager_Modulos_Geral_Hospitais_Editar : System.Web.UI.Page
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
			if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
				phExcluir.Visible = true;
				this.LoadForm();
            }
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (this.ValidaForm())
        {
			HOSPITAL_Service service = new HOSPITAL_Service();
            HOSPITAL item = null;
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                item = this.getCampos(new HOSPITAL());
                item = service.Inserir(item);
            }
            else
            {
                HOSPITAL filtro = new HOSPITAL();
                filtro.HOSPITAL_ID = Convert.ToInt32(Request.QueryString["id"]);
                item = this.getCampos(service.Carregar(filtro));
                service.Atualizar(item);

            }

            Response.Redirect("Listar.aspx?s=1");
        }
        else
        {
            phFeedback.Visible = true;
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Listar.aspx");
    }

	protected void btnVoltar_Click(object sender, EventArgs e)
	{
		int pid = Convert.ToInt32(Request.QueryString["id"]);
		Response.Redirect("Editar.aspx?ID=" + pid);
	}

	protected void btnExcluir_Click(object sender, EventArgs e)
	{

		//if (!String.IsNullOrEmpty(Request.QueryString["id"]))
		//{
		//	USUARIO_Service service = new USUARIO_Service();
		//	USUARIO filtro = new USUARIO();
		//	filtro.USUARIO_ID = Convert.ToInt32(Request.QueryString["id"]);
		//	bool error = false;
		//	string msgError = "Erro: Ainda existem ";
		//	List<string> errList = new List<string>();

		//	List<PROJETO> projetos = new PROJETO_Service().Listar(new PROJETO() { USUARIO_ID = filtro.USUARIO_ID });
		//	if (projetos.Count > 0)
		//	{
		//		error = true;
		//		errList.Add("Projetos");
		//	}

		//	List<FATURAMENTO> clientes = new FATURAMENTO_Service().Listar(new FATURAMENTO() { USUARIO_ID = filtro.USUARIO_ID });
		//	if (clientes.Count > 0)
		//	{
		//		error = true;
		//		errList.Add("Faturamentos");
		//	}

		//	if (error)
		//	{
		//		msgError += String.Join(" ,", errList.ToArray());
		//		msgError += " ligados a esse Usuário.";

		//		int place = msgError.LastIndexOf(",");
		//		if (place != -1)
		//		{
		//			msgError = msgError.Remove(place, 1).Insert(place, "e ");
		//		}

		//		msgFeedbackExcluir.Text = msgError;
		//		phFeedbackExcluir.Visible = true;
		//		msgFeedbackExcluir.CssClass += " error";
		//		return;
		//	}
		//	else
		//	{
		//		service.Excluir(filtro);
		//		Response.Redirect("Listar.aspx?s=1");
		//	}

		//}
	}
	#endregion

	#region Métodos
	private bool ValidaForm()
    {
        ltlObs.Text = "";
        if (string.IsNullOrEmpty(txtNome.Text)) ltlObs.Text += "Informe o Nome <br />";
		if (string.IsNullOrEmpty(ddlCidade.SelectedValue)) ltlObs.Text += "Informe a Cidade <br />";
		return string.IsNullOrEmpty(ltlObs.Text) ? true : false;
    }

    private HOSPITAL getCampos(HOSPITAL item)
    {
		item.NOME = txtNome.Text;
		item.CIDADE_ID = !String.IsNullOrEmpty(ddlCidade.SelectedValue) ? (Int32?)Convert.ToInt32(ddlCidade.SelectedValue) : null;
		return item;
    }

    private void setCampos(HOSPITAL item)
    {
        txtNome.Text = item.NOME;
		ddlCidade.SelectedValue = Convert.ToString(item.CIDADE_ID);
	}

	private void LoadForm()
    {
		HOSPITAL filtro = new HOSPITAL();
        filtro.HOSPITAL_ID = Convert.ToInt32(Request.QueryString["id"]);
        this.setCampos(new HOSPITAL_Service().Carregar(filtro));
    }

	private void CarregarCombos()
	{
		ddlCidade.DataSource = new CIDADE_Service().Listar(CIDADE_Ordem.NOME, OrdemTipo.Ascendente);
		ddlCidade.DataTextField = "NOME";
		ddlCidade.DataValueField = "CIDADE_ID";
		ddlCidade.DataBind();
		ddlCidade.Items.Insert(0, new ListItem { Value = "", Text = "" });
	}
	#endregion
}