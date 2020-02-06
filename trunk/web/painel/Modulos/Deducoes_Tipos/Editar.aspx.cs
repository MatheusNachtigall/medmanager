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
using System.Globalization;

public partial class Manager_Modulos_Deducoes_Tipos_Editar : System.Web.UI.Page
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
			DEDUCAO_TIPO_Service service = new DEDUCAO_TIPO_Service();
            DEDUCAO_TIPO item = null;
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                item = this.getCampos(new DEDUCAO_TIPO());
                item = service.Inserir(item);
            }
            else
            {
				DEDUCAO_TIPO filtro = new DEDUCAO_TIPO();
                filtro.DEDUCAO_TIPO_ID = Convert.ToInt32(Request.QueryString["id"]);
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
		Response.Redirect("Editar.aspx?ID="+pid);
    }

	protected void btnExcluir_Click(object sender, EventArgs e)
	{

		//if (!String.IsNullOrEmpty(Request.QueryString["id"]))
		//{
		//	DEDUCAO_TIPO_Service service = new DEDUCAO_TIPO_Service();
		//	DEDUCAO_TIPO filtro = new DEDUCAO_TIPO();
		//	filtro.DEDUCAO_TIPO_ID = Convert.ToInt32(Request.QueryString["id"]);
		//	bool error = false;
		//	string msgError = "Erro: Ainda existem ";
		//	List<string> errList = new List<string>();

		//	List<FATURAMENTO> faturamentos = new FATURAMENTO_Service().Listar(new FATURAMENTO() { DEDUCAO_TIPO_ID = filtro.DEDUCAO_TIPO_ID });
		//	if(faturamentos.Count > 0)
		//	{
		//		error = true;
		//		errList.Add("Faturamentos");
		//	}

		//	if(error)
		//	{
		//		msgError += String.Join(" ,", errList.ToArray());
		//		msgError += " ligados a esse DEDUCAO_TIPO.";

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
        if (string.IsNullOrEmpty(txtNome.Text)) ltlObs.Text += "Informe o Nome da Dedução <br />";
		return string.IsNullOrEmpty(ltlObs.Text) ? true : false;
    }

    private DEDUCAO_TIPO getCampos(DEDUCAO_TIPO item)
    {
		item.NOME = txtNome.Text;
		return item;
    }

    private void setCampos(DEDUCAO_TIPO item)
    {
		txtNome.Text = Convert.ToString(item.NOME);
	}

	private void LoadForm()
    {
		DEDUCAO_TIPO filtro = new DEDUCAO_TIPO();
        filtro.DEDUCAO_TIPO_ID = Convert.ToInt32(Request.QueryString["id"]);
        this.setCampos(new DEDUCAO_TIPO_Service().Carregar(filtro));
    }

	#endregion
}