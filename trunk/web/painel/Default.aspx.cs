using CRM_Blue.Service;
using CRM_Blue.Entity;
using System;
using Utilities;
using CRM_Blue;

public partial class Manager_Default : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		txtEmail.Attributes["placeholder"] = "E-mail";
		txtSenha.Attributes["placeholder"] = "Senha";
		if (Request.QueryString["l"] == "1")
		{
			Session["usuario"] = null;
		}
		else
		{
			USUARIO session = null;
			try
			{
				session = (USUARIO)Session["usuario"];
			}
			catch (Exception)
			{
			}
			if (session != null)
			{
				Response.Redirect("Interna/Default.aspx", true);
			}
		}
		this.Master.FindControl("body").ID = "login";
	}

	protected void btSubmit_Click(object sender, EventArgs e)
	{

		if ((!String.IsNullOrEmpty(txtEmail.Text)) && (!String.IsNullOrEmpty(txtSenha.Text)))
		{
			var usuario = new USUARIO_Service().Carregar(new USUARIO() { EMAIL = txtEmail.Text });

			if (usuario != null)
			{
				if (usuario.SENHA == txtSenha.Text)
				{
					new USUARIO_Service().Atualizar(usuario);
					Session["usuario"] = usuario;
					Response.Redirect("Interna/Default.aspx", true);
					return;
				}
				else
				{
					phErro.Visible = true;
					txtErro.InnerText = "Senha incorreta";
				}
			}
			else
			{
				//Usuário nao existe
				//idLogin.Attributes.Add("class", idLogin.Attributes["class"] + " login-error");
				phErro.Visible = true;
				txtErro.InnerText = "Usuário não existe";
			}

		}
		else
		{
			//idLogin.Attributes.Add("class", idLogin.Attributes["class"] + " login-error");
			phErro.Visible = true;
			txtErro.InnerText = "Preencha os campos Usuário e Senha";
		}

		txtEmail.Text = "";
		txtSenha.Text = "";
		//phLogin.Visible = true;
	}

}