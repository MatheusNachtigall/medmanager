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

public partial class Manager_Modulos_Plantoes_Cadastros_Editar : System.Web.UI.Page
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
			PLANTAO_Service service = new PLANTAO_Service();
            PLANTAO item = null;
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                item = this.getCampos(new PLANTAO());
				item.DATA_CADASTRO = DateTime.Now;
                item = service.Inserir(item);
            }
            else
            {
				PLANTAO filtro = new PLANTAO();
                filtro.PLANTAO_ID = Convert.ToInt32(Request.QueryString["id"]);
                item = this.getCampos(service.Carregar(filtro));
                service.Atualizar(item);
            }
			this.atualizaINSS(item);

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

        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
        {
            PLANTAO_Service service = new PLANTAO_Service();
            PLANTAO filtro = new PLANTAO();
            filtro.PLANTAO_ID = Convert.ToInt32(Request.QueryString["id"]);
            service.Excluir(filtro);
            Response.Redirect("Listar.aspx?s=1");
        }
    }
	#endregion

	#region Métodos
	private bool ValidaForm()
    {
        ltlObs.Text = "";
        if (string.IsNullOrEmpty(ddlHospital.SelectedValue)) ltlObs.Text += "Informe o Hospital <br />";
        if (string.IsNullOrEmpty(txtValor.Text)) ltlObs.Text += "Informe o Valor <br />";
        if (string.IsNullOrEmpty(txtDataPlantao.Text)) ltlObs.Text += "Data do Plantão inválida <br />";
		return string.IsNullOrEmpty(ltlObs.Text) ? true : false;
    }

    private PLANTAO getCampos(PLANTAO item)
    {
		item.HOSPITAL_ID = !String.IsNullOrEmpty(ddlHospital.SelectedValue) ? (Int32?)Convert.ToInt32(ddlHospital.SelectedValue) : null;
		item.VALOR = Convert.ToDecimal(txtValor.Text);
		item.DATA = DateTime.ParseExact(txtDataPlantao.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
		item.DATA_PAGAMENTO = DateTime.ParseExact(txtDataPagamento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
		item.CNPJ = chkCNPJ.Checked ? true : false;
		item.INSS = chkINSS.Checked ? true : false;
		item.RECEBIDO = chkRecebido.Checked ? true : false;
		return item;
    }

    private void setCampos(PLANTAO item)
    {
		ddlHospital.SelectedValue = Convert.ToString(item.HOSPITAL_ID);
		txtValor.Text = Convert.ToString(item.VALOR);
		chkCNPJ.Checked = item.CNPJ == true;
		chkINSS.Checked = item.INSS == true;
		txtDataPlantao.Text = item.DATA.Value.ToString("dd/MM/yyyy");
		txtDataPagamento.Text = item.DATA_PAGAMENTO.Value.ToString("dd/MM/yyyy");
		chkRecebido.Checked = item.RECEBIDO == true;
	}

	private void atualizaINSS(PLANTAO item)
	{
		INSS inss = null;
		INSS_Service INSSService = new INSS_Service();
		inss = INSSService.Carregar(new INSS() { PLANTAO_ID = item.PLANTAO_ID });
		// Se INSS for marcado
		if (item.INSS == true)
		{
			//Primeiro checa ja existe o registro na tabela
			if (inss == null)
			{
				inss = new INSS();
				inss.PLANTAO_ID = item.PLANTAO_ID;
				inss.VALOR = item.VALOR * (decimal)0.11; //11%
				inss.DATA_INSS = item.DATA;
				inss.DATA_CADASTRO = DateTime.Now;
				INSSService.Inserir(inss);
			}
			//Caso ja exista, verifica se precisa de atualizacao
			else
			{
				if (inss.VALOR != (item.VALOR * (decimal)0.11))
				{
					inss.VALOR = item.VALOR * (decimal)0.11; //11%
					inss.DATA_CADASTRO = DateTime.Now;
					INSSService.Atualizar(inss);
				}
			}
		}
		//Caso nao tenha sido marcado, pode nao existir ou estar sendo apagado
		else
		{
			//Se inss != null, existe um registro e deve ser apagado
			if (inss != null)
			{
				INSSService.Excluir(new INSS() {INSS_ID = inss.INSS_ID });
			}
		}
	}

	private void LoadForm()
    {
		PLANTAO filtro = new PLANTAO();
        filtro.PLANTAO_ID = Convert.ToInt32(Request.QueryString["id"]);

		PLANTAO plantao = new PLANTAO_Service().Carregar(filtro);
		if (plantao != null)
		{
			this.setCampos(plantao);
			phAtalhos.Visible = true;
		}
    }

	private void CarregarCombos()
	{
		ddlHospital.DataSource = new HOSPITAL_Service().Listar(HOSPITAL_Ordem.NOME, OrdemTipo.Ascendente);
		ddlHospital.DataTextField = "NOME";
		ddlHospital.DataValueField = "HOSPITAL_ID";
		ddlHospital.DataBind();
		ddlHospital.Items.Insert(0, new ListItem { Value = "", Text = "" });
	}

	#endregion
}