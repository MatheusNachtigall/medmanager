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

public partial class Manager_Modulos_INSS_Cadastros_Editar : System.Web.UI.Page
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
			INSS_Service service = new INSS_Service();
            INSS item = null;
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                item = this.getCampos(new INSS());
				item.DATA_CADASTRO = DateTime.Now;
                item = service.Inserir(item);
            }
            else
            {
				INSS filtro = new INSS();
                filtro.INSS_ID = Convert.ToInt32(Request.QueryString["id"]);
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
		//	INSS_Service service = new INSS_Service();
		//	INSS filtro = new INSS();
		//	filtro.INSS_ID = Convert.ToInt32(Request.QueryString["id"]);
		//	bool error = false;
		//	string msgError = "Erro: Ainda existem ";
		//	List<string> errList = new List<string>();

		//	List<FATURAMENTO> faturamentos = new FATURAMENTO_Service().Listar(new FATURAMENTO() { INSS_ID = filtro.INSS_ID });
		//	if(faturamentos.Count > 0)
		//	{
		//		error = true;
		//		errList.Add("Faturamentos");
		//	}

		//	if(error)
		//	{
		//		msgError += String.Join(" ,", errList.ToArray());
		//		msgError += " ligados a esse INSS.";

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
        if (string.IsNullOrEmpty(ddlPlantaoID.SelectedValue)) ltlObs.Text += "Informe o ID do Plantão <br />";
        if (string.IsNullOrEmpty(txtValor.Text)) ltlObs.Text += "Informe o Valor <br />";
        if (string.IsNullOrEmpty(txtDataCadastro.Text)) ltlObs.Text += "Data do Plantão inválida <br />";
		return string.IsNullOrEmpty(ltlObs.Text) ? true : false;
    }

    private INSS getCampos(INSS item)
    {
		item.PLANTAO_ID = !String.IsNullOrEmpty(ddlPlantaoID.SelectedValue) ? (Int32?)Convert.ToInt32(ddlPlantaoID.SelectedValue) : null;
		item.VALOR = Convert.ToDecimal(txtValor.Text);
		item.DATA_CADASTRO = DateTime.ParseExact(txtDataCadastro.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
		return item;
    }

    private void setCampos(INSS item)
    {
		ddlPlantaoID.SelectedValue = Convert.ToString(item.PLANTAO_ID);
		txtValor.Text = Convert.ToString(item.VALOR);
		txtDataCadastro.Text = item.DATA_CADASTRO.Value.ToString("dd/MM/yyyy");
	}

	private void LoadForm()
    {
		INSS filtro = new INSS();
        filtro.INSS_ID = Convert.ToInt32(Request.QueryString["id"]);

		INSS INSS = new INSS_Service().Carregar(filtro);
		if (INSS != null)
		{
			this.setCampos(INSS);
		}
    }

	private void CarregarCombos()
	{
		ddlPlantaoID.DataSource = new PLANTAO_Service().Listar(PLANTAO_Ordem.PLANTAO_ID, OrdemTipo.Ascendente);
		ddlPlantaoID.DataTextField = "PLANTAO_ID";
		ddlPlantaoID.DataValueField = "PLANTAO_ID";
		ddlPlantaoID.DataBind();
		ddlPlantaoID.Items.Insert(0, new System.Web.UI.WebControls.ListItem { Value = "", Text = "" });
	}

	#endregion
}