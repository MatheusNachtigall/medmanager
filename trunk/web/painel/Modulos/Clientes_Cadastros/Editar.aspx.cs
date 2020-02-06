using CRM_Blue.Service;
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

public partial class Manager_Modulos_Clientes_Cadastros_Editar : System.Web.UI.Page
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
            CLIENTE_Service service = new CLIENTE_Service();
            CLIENTE item = null;
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                item = this.getCampos(new CLIENTE());
                item = service.Inserir(item);
            }
            else
            {
                CLIENTE filtro = new CLIENTE();
                filtro.CLIENTE_ID = Convert.ToInt32(Request.QueryString["id"]);
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

		if (!String.IsNullOrEmpty(Request.QueryString["id"]))
		{
			CLIENTE_Service service = new CLIENTE_Service();
			CLIENTE filtro = new CLIENTE();
			filtro.CLIENTE_ID = Convert.ToInt32(Request.QueryString["id"]);
			bool error = false;
			string msgError = "Erro: Ainda existem ";
			List<string> errList = new List<string>();

			List<PROJETO> projetos = new PROJETO_Service().Listar(new PROJETO() { CLIENTE_ID = filtro.CLIENTE_ID });
			if (projetos.Count > 0)
			{
				error = true;
				errList.Add("Projetos");
			}

			if (error)
			{
				msgError += String.Join(" ,", errList.ToArray());
				msgError += " ligados a esse Cliente.";

				int place = msgError.LastIndexOf(",");
				if (place != -1)
				{
					msgError = msgError.Remove(place, 1).Insert(place, "e ");
				}

				msgFeedbackExcluir.Text = msgError;
				phFeedbackExcluir.Visible = true;
				msgFeedbackExcluir.CssClass += " error";
				return;
			}
			else
			{
				service.Excluir(filtro);
				Response.Redirect("Listar.aspx?s=1");
			}

		}
	}

	#endregion

	#region Métodos
	private bool ValidaForm()
    {
        ltlObs.Text = "";
        if (string.IsNullOrEmpty(txtNome.Text)) ltlObs.Text += "Informe o Nome <br />";
		if (string.IsNullOrEmpty(ddlAgencia.SelectedValue)) ltlObs.Text += "Informe a Agência <br />";

		return string.IsNullOrEmpty(ltlObs.Text) ? true : false;
    }

    private CLIENTE getCampos(CLIENTE item)
    {
		item.NOME = txtNome.Text;
		item.AGENCIA_ID = !String.IsNullOrEmpty(ddlAgencia.SelectedValue) ? (Int32?)Convert.ToInt32(ddlAgencia.SelectedValue) : null;

		return item;
    }

    private void setCampos(CLIENTE item)
    {
        txtNome.Text = item.NOME;
		ddlAgencia.SelectedValue = Convert.ToString(item.AGENCIA_ID);
	}

	private void LoadForm()
    {
		CLIENTE filtro = new CLIENTE();
        filtro.CLIENTE_ID = Convert.ToInt32(Request.QueryString["id"]);
        this.setCampos(new CLIENTE_Service().Carregar(filtro));
    }

	private void CarregarCombos()
	{
		ddlAgencia.DataSource = new AGENCIA_Service().Listar(AGENCIA_Ordem.NOME, OrdemTipo.Ascendente);
		ddlAgencia.DataTextField = "NOME";
		ddlAgencia.DataValueField = "AGENCIA_ID";
		ddlAgencia.DataBind();
		ddlAgencia.Items.Insert(0, new ListItem { Value = "", Text = "" });
	}

	#endregion
}