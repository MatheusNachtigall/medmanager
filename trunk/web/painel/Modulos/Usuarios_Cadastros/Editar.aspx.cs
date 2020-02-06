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

public partial class Manager_Modulos_Usuarios_Cadastros_Editar : System.Web.UI.Page
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
            USUARIO_Service service = new USUARIO_Service();
            USUARIO item = null;
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                item = this.getCampos(new USUARIO());
                item = service.Inserir(item);
            }
            else
            {
                USUARIO filtro = new USUARIO();
                filtro.USUARIO_ID = Convert.ToInt32(Request.QueryString["id"]);
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
			USUARIO_Service service = new USUARIO_Service();
			USUARIO filtro = new USUARIO();
			filtro.USUARIO_ID = Convert.ToInt32(Request.QueryString["id"]);
			bool error = false;
			string msgError = "Erro: Ainda existem ";
			List<string> errList = new List<string>();

			List<PROJETO> projetos = new PROJETO_Service().Listar(new PROJETO() { USUARIO_ID = filtro.USUARIO_ID });
			if (projetos.Count > 0)
			{
				error = true;
				errList.Add("Projetos");
			}

			List<FATURAMENTO> clientes = new FATURAMENTO_Service().Listar(new FATURAMENTO() { USUARIO_ID = filtro.USUARIO_ID });
			if (clientes.Count > 0)
			{
				error = true;
				errList.Add("Faturamentos");
			}

			if (error)
			{
				msgError += String.Join(" ,", errList.ToArray());
				msgError += " ligados a esse Usuário.";

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
        if (string.IsNullOrEmpty(txtEmail.Text)) ltlObs.Text += "Informe o E-mail <br />";
        if (string.IsNullOrEmpty(txtNome.Text)) ltlObs.Text += "Informe o Nome <br />";

		if (String.IsNullOrEmpty(Request.QueryString["id"]))
		{
			if (string.IsNullOrEmpty(txtSenha.Text)) ltlObs.Text += "Informe a Senha <br />";
		}
		if ((!string.IsNullOrEmpty(txtSenha.Text)) && (txtSenha.Text.Length < 6)) ltlObs.Text += "Informe uma senha com pelo menos 6 caracteres. <br />";

		return string.IsNullOrEmpty(ltlObs.Text) ? true : false;
    }

    private USUARIO getCampos(USUARIO item)
    {
		item.EMAIL = txtEmail.Text;
		item.NOME = txtNome.Text;
		if (!string.IsNullOrEmpty(txtSenha.Text))
		{
			item.SENHA = txtSenha.Text;
		}
		return item;
    }

    private void setCampos(USUARIO item)
    {
        txtEmail.Text = item.EMAIL;
        txtNome.Text = item.NOME;
	}

	private void LoadForm()
    {
		USUARIO filtro = new USUARIO();
        filtro.USUARIO_ID = Convert.ToInt32(Request.QueryString["id"]);
        this.setCampos(new USUARIO_Service().Carregar(filtro));
    }


    #endregion
}