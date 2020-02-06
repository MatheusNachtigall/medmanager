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

public partial class Manager_Modulos_Projetos_Cadastros_Editar : System.Web.UI.Page
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
			txtDataContratacao.Enabled = false;
			txtDataProspeccao.Text = DateTime.Now.ToString("dd/MM/yyyy");
			txtGarantia.Text = "90";
			txtValidade.Text = "60";
			txtPrazoPagamento.Text = "30";

			if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
				phExcluir.Visible = true;
				this.LoadForm();
				if(rdbStatus.SelectedValue == "2") txtDataContratacao.Enabled = true;
			}
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (this.ValidaForm())
        {
            PROJETO_Service service = new PROJETO_Service();
            PROJETO item = null;
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                item = this.getCampos(new PROJETO());
				item.DATA_CADASTRO = DateTime.Now;
				USUARIO session = (USUARIO)Session["usuario"];
				item.USUARIO_ID = session.USUARIO_ID;
                item = service.Inserir(item);
            }
            else
            {
                PROJETO filtro = new PROJETO();
                filtro.PROJETO_ID = Convert.ToInt32(Request.QueryString["id"]);
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

		if (!String.IsNullOrEmpty(Request.QueryString["id"]))
		{
			PROJETO_Service service = new PROJETO_Service();
			PROJETO filtro = new PROJETO();
			filtro.PROJETO_ID = Convert.ToInt32(Request.QueryString["id"]);
			bool error = false;
			string msgError = "Erro: Ainda existem ";
			List<string> errList = new List<string>();

			List<FATURAMENTO> faturamentos = new FATURAMENTO_Service().Listar(new FATURAMENTO() { PROJETO_ID = filtro.PROJETO_ID });
			if(faturamentos.Count > 0)
			{
				error = true;
				errList.Add("Faturamentos");
			}

			if(error)
			{
				msgError += String.Join(" ,", errList.ToArray());
				msgError += " ligados a esse Projeto.";

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

	protected void rdbStatusChanged(object sender, EventArgs e)
	{
		if (rdbStatus.SelectedValue == "2")
		{
			txtDataContratacao.Enabled = true;
			txtDataContratacao.Text = DateTime.Now.ToString("dd/MM/yyyy");
		}
		else
		{
			txtDataContratacao.Enabled = false;
			txtDataContratacao.Text = null;
		}
		ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}

	protected void ddlAgenciaChanged(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(ddlAgencia.SelectedValue))
			ddlCliente.DataSource = new CLIENTE_Service().Listar(CLIENTE_Ordem.NOME, OrdemTipo.Ascendente);
		else
			ddlCliente.DataSource = new CLIENTE_Service().Listar(new CLIENTE() { AGENCIA_ID = Convert.ToInt32(ddlAgencia.SelectedValue) }, CLIENTE_Ordem.NOME, OrdemTipo.Ascendente);

		ddlCliente.DataTextField = "NOME";
		ddlCliente.DataValueField = "CLIENTE_ID";
		ddlCliente.DataBind();
		ddlCliente.Items.Insert(0, new ListItem { Value = "", Text = "" });
		ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}

	protected void ddlClienteChanged(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(ddlCliente.SelectedValue))
		{
			CLIENTE cliente = new CLIENTE_Service().Carregar(new CLIENTE() { CLIENTE_ID = Convert.ToInt32(ddlCliente.SelectedValue) });
			ddlAgencia.SelectedValue = cliente.AGENCIA_ID.ToString();
		}
		ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}
	#endregion

	#region Métodos
	private bool ValidaForm()
    {
        ltlObs.Text = "";
        if (string.IsNullOrEmpty(txtNome.Text)) ltlObs.Text += "Informe o Nome <br />";
        if (string.IsNullOrEmpty(ddlAgencia.SelectedValue)) ltlObs.Text += "Informe a Agência <br />";
        if (string.IsNullOrEmpty(ddlCliente.SelectedValue)) ltlObs.Text += "Informe o Cliente <br />";
        if (string.IsNullOrEmpty(txtDescricao.Text)) ltlObs.Text += "Insira a Descrição <br />";
        if (string.IsNullOrEmpty(txtEscopo.Text)) ltlObs.Text += "Insira o Escopo <br />";
        if (string.IsNullOrEmpty(txtValor.Text)) ltlObs.Text += "Informe o Valor <br />";
        if (string.IsNullOrEmpty(txtSolicitante.Text)) ltlObs.Text += "Informe o Solicitante <br />";
        if (string.IsNullOrEmpty(txtDataProspeccao.Text)) ltlObs.Text += "Data de Prospecção inválida <br />";

		if (rdbStatus.SelectedValue == "2")
		{
			if (string.IsNullOrEmpty(txtDataContratacao.Text)) ltlObs.Text += "Data de Contratação inválida <br />";
		}

        if (string.IsNullOrEmpty(txtGarantia.Text)) ltlObs.Text += "Informe o tempo de Garantia <br />";
        if (string.IsNullOrEmpty(txtValidade.Text)) ltlObs.Text += "Informe o tempo de Validade <br />";
        if (string.IsNullOrEmpty(txtPrazo.Text)) ltlObs.Text += "Informe o tempo de Prazo <br />";
        if (string.IsNullOrEmpty(txtHoras.Text)) ltlObs.Text += "Informe as Horas <br />";
        if (string.IsNullOrEmpty(txtPrazoPagamento.Text)) ltlObs.Text += "Informe o tempo de Prazo de Pagamento <br />";

		return string.IsNullOrEmpty(ltlObs.Text) ? true : false;
    }

    private PROJETO getCampos(PROJETO item)
    {
		item.NOME = txtNome.Text;
		item.AGENCIA_ID = !String.IsNullOrEmpty(ddlAgencia.SelectedValue) ? (Int32?)Convert.ToInt32(ddlAgencia.SelectedValue) : null;
		item.CLIENTE_ID = !String.IsNullOrEmpty(ddlCliente.SelectedValue) ? (Int32?)Convert.ToInt32(ddlCliente.SelectedValue) : null;
		item.DESCRICAO = txtDescricao.Text;
		item.ESCOPO = txtEscopo.Text;
		item.VALOR = Convert.ToDecimal(txtValor.Text);
		item.SOLICITANTE = txtSolicitante.Text;
		item.STATUS = Convert.ToInt32(rdbStatus.SelectedValue);
		item.DATA_PROSPECCAO = DateTime.ParseExact(txtDataProspeccao.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
		item.DATA_CONTRATACAO = (item.STATUS == 2) ? (DateTime?)DateTime.ParseExact(txtDataContratacao.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) : null;
		item.LOCAL_TRABALHO = Convert.ToInt32(rdbLocal.SelectedValue);
		item.GARANTIA_DIAS = Convert.ToInt32(txtGarantia.Text);
		item.VALIDADE_DIAS = Convert.ToInt32(txtValidade.Text);
		item.PRAZO_DIAS = Convert.ToInt32(txtPrazo.Text);
		item.HORAS = Convert.ToInt32(txtHoras.Text);
		item.PRAZO_PAGAMENTO_DIAS = Convert.ToInt32(txtPrazoPagamento.Text);

		return item;
    }

    private void setCampos(PROJETO item)
    {
        txtNome.Text = item.NOME;
		ddlAgencia.SelectedValue = Convert.ToString(item.AGENCIA_ID);
		ddlCliente.SelectedValue = Convert.ToString(item.CLIENTE_ID);
		txtDescricao.Text = item.DESCRICAO;
		txtEscopo.Text = item.ESCOPO;
		txtValor.Text = Convert.ToString(item.VALOR);
		txtSolicitante.Text = item.SOLICITANTE;
		rdbStatus.SelectedValue = Convert.ToString(item.STATUS);
		txtDataProspeccao.Text = item.DATA_PROSPECCAO.Value.ToString("dd/MM/yyyy");

		if(item.STATUS == 2)
		{
			txtDataContratacao.Enabled = true;
			txtDataContratacao.Text = item.DATA_CONTRATACAO.Value.ToString("dd/MM/yyyy");
		}
		rdbLocal.SelectedValue = Convert.ToString(item.LOCAL_TRABALHO);
		txtGarantia.Text = Convert.ToString(item.GARANTIA_DIAS);
		txtValidade.Text = Convert.ToString(item.VALIDADE_DIAS);
		txtPrazo.Text = Convert.ToString(item.PRAZO_DIAS);
		txtHoras.Text = Convert.ToString(item.HORAS);
		txtPrazoPagamento.Text = Convert.ToString(item.PRAZO_PAGAMENTO_DIAS);
	}

	private void LoadForm()
    {
		PROJETO filtro = new PROJETO();
        filtro.PROJETO_ID = Convert.ToInt32(Request.QueryString["id"]);
        this.setCampos(new PROJETO_Service().Carregar(filtro));
    }

	private void CarregarCombos()
	{
		ddlAgencia.DataSource = new AGENCIA_Service().Listar(AGENCIA_Ordem.NOME, OrdemTipo.Ascendente);
		ddlAgencia.DataTextField = "NOME";
		ddlAgencia.DataValueField = "AGENCIA_ID";
		ddlAgencia.DataBind();
		ddlAgencia.Items.Insert(0, new ListItem { Value = "", Text = "" });

		ddlCliente.DataSource = new CLIENTE_Service().Listar(CLIENTE_Ordem.NOME, OrdemTipo.Ascendente);
		ddlCliente.DataTextField = "NOME";
		ddlCliente.DataValueField = "CLIENTE_ID";
		ddlCliente.DataBind();
		ddlCliente.Items.Insert(0, new ListItem { Value = "", Text = "" });
	}

	#endregion
}