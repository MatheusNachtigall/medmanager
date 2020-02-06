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

public partial class Manager_Modulos_Deducoes_Cadastros_Editar : System.Web.UI.Page
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
			else
			{
				if (Session["DEDUCAO_Filtro"] != null)
				{
					ddlPlantaoID.SelectedValue = ((DEDUCAO)Session["DEDUCAO_Filtro"]).PLANTAO_ID.ToString();
					ddlTipo.SelectedValue = ((DEDUCAO)Session["DEDUCAO_Filtro"]).DEDUCAO_TIPO_ID.ToString();
				}
			}
		}
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (this.ValidaForm())
        {
			DEDUCAO_Service service = new DEDUCAO_Service();
            DEDUCAO item = null;
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                item = this.getCampos(new DEDUCAO());
                item = service.Inserir(item);
            }
            else
            {
				DEDUCAO filtro = new DEDUCAO();
                filtro.DEDUCAO_ID = Convert.ToInt32(Request.QueryString["id"]);
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
		//	DEDUCAO_Service service = new DEDUCAO_Service();
		//	DEDUCAO filtro = new DEDUCAO();
		//	filtro.DEDUCAO_ID = Convert.ToInt32(Request.QueryString["id"]);
		//	bool error = false;
		//	string msgError = "Erro: Ainda existem ";
		//	List<string> errList = new List<string>();

		//	List<FATURAMENTO> faturamentos = new FATURAMENTO_Service().Listar(new FATURAMENTO() { DEDUCAO_ID = filtro.DEDUCAO_ID });
		//	if(faturamentos.Count > 0)
		//	{
		//		error = true;
		//		errList.Add("Faturamentos");
		//	}

		//	if(error)
		//	{
		//		msgError += String.Join(" ,", errList.ToArray());
		//		msgError += " ligados a esse DEDUCAO.";

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
		if (string.IsNullOrEmpty(ddlTipo.SelectedValue)) ltlObs.Text += "Informe a Tipo da Dedução <br />";
		if (string.IsNullOrEmpty(ddlPlantaoID.SelectedValue)) ltlObs.Text += "Informe o Plantão <br />";
        if (string.IsNullOrEmpty(txtValor.Text)) ltlObs.Text += "Informe o Valor <br />";
        if (string.IsNullOrEmpty(txtDataDeducao.Text)) ltlObs.Text += "Data da Dedução inválida <br />";
		return string.IsNullOrEmpty(ltlObs.Text) ? true : false;
    }

    private DEDUCAO getCampos(DEDUCAO item)
    {
		item.DEDUCAO_TIPO_ID = !String.IsNullOrEmpty(ddlTipo.SelectedValue) ? (Int32?)Convert.ToInt32(ddlTipo.SelectedValue) : null;
		item.PLANTAO_ID = !String.IsNullOrEmpty(ddlPlantaoID.SelectedValue) ? (Int32?)Convert.ToInt32(ddlPlantaoID.SelectedValue) : null;
		item.VALOR = Convert.ToDecimal(txtValor.Text);
		item.DATA_DEDUCAO = DateTime.ParseExact(txtDataDeducao.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
		return item;
    }

    private void setCampos(DEDUCAO item)
    {
		ddlPlantaoID.SelectedValue = Convert.ToString(item.PLANTAO_ID);
		txtValor.Text = Convert.ToString(item.VALOR);
		txtDataDeducao.Text = item.DATA_DEDUCAO.Value.ToString("dd/MM/yyyy");
		ddlTipo.SelectedValue = Convert.ToString(item.DEDUCAO_TIPO_ID);
	}

	private void LoadForm()
    {
		DEDUCAO filtro = new DEDUCAO();
        filtro.DEDUCAO_ID = Convert.ToInt32(Request.QueryString["id"]);
        this.setCampos(new DEDUCAO_Service().Carregar(filtro));
    }

	private void CarregarCombos()
	{
		ddlPlantaoID.DataSource = new PLANTAO_Service().Listar(PLANTAO_Ordem.PLANTAO_ID, OrdemTipo.Ascendente);
		ddlPlantaoID.DataTextField = "PLANTAO_ID";
		ddlPlantaoID.DataValueField = "PLANTAO_ID";
		ddlPlantaoID.DataBind();
		ddlPlantaoID.Items.Insert(0, new ListItem { Value = "", Text = "" });

		ddlTipo.DataSource = new DEDUCAO_TIPO_Service().Listar(DEDUCAO_TIPO_Ordem.NOME, OrdemTipo.Ascendente);
		ddlTipo.DataTextField = "NOME";
		ddlTipo.DataValueField = "DEDUCAO_TIPO_ID";
		ddlTipo.DataBind();
		ddlTipo.Items.Insert(0, new ListItem { Value = "", Text = "" });
	}
	#endregion
}