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
using System.Linq;

public partial class Manager_Modulos_Projetos_Faturamentos_Editar : System.Web.UI.Page
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
			txtDataRecebimento.Enabled = false;
			txtDataFaturamento.Text = DateTime.Now.ToString("dd/MM/yyyy");

			if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
				phExcluir.Visible = true;
				this.LoadForm();
				if(rdbStatus.SelectedValue == "2") txtDataRecebimento.Enabled = true;
			}
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (this.ValidaForm())
        {
            FATURAMENTO_Service service = new FATURAMENTO_Service();
            FATURAMENTO item = null;
            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                item = this.getCampos(new FATURAMENTO());
				item.DATA_CADASTRO = DateTime.Now;
				USUARIO session = (USUARIO)Session["usuario"];
				item.USUARIO_ID = session.USUARIO_ID;
                item = service.Inserir(item);
            }
            else
            {
                FATURAMENTO filtro = new FATURAMENTO();
                filtro.FATURAMENTO_ID = Convert.ToInt32(Request.QueryString["id"]);
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

	protected void btnExcluir_Click(object sender, EventArgs e)
    {

		if (!String.IsNullOrEmpty(Request.QueryString["id"]))
		{
			FATURAMENTO_Service service = new FATURAMENTO_Service();
			FATURAMENTO filtro = new FATURAMENTO();
			filtro.FATURAMENTO_ID = Convert.ToInt32(Request.QueryString["id"]);
			service.Excluir(filtro);

			Response.Redirect("Listar.aspx?s=1");
		}

	}

	protected void rdbStatusChanged(object sender, EventArgs e)
	{
		if (rdbStatus.SelectedValue == "2")
		{
			txtDataRecebimento.Enabled = true;
			txtDataRecebimento.Text = DateTime.Now.ToString("dd/MM/yyyy");
		}
		else
		{
			txtDataRecebimento.Enabled = false;
			txtDataRecebimento.Text = null;
		}
		ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
	}

	#endregion

	#region Métodos
	private bool ValidaForm()
    {
        ltlObs.Text = "";
        if (string.IsNullOrEmpty(ddlProjeto.SelectedValue)) ltlObs.Text += "Informe o Projeto <br />";
        if (string.IsNullOrEmpty(txtValor.Text)) ltlObs.Text += "Informe o Valor <br />";
        if (string.IsNullOrEmpty(txtDataFaturamento.Text)) ltlObs.Text += "Data de Faturamento inválida <br />";

		if (rdbStatus.SelectedValue == "2")
		{
			if (string.IsNullOrEmpty(txtDataRecebimento.Text)) ltlObs.Text += "Data de Recebimento inválida <br />";
		}

		return string.IsNullOrEmpty(ltlObs.Text) ? true : false;
    }

    private FATURAMENTO getCampos(FATURAMENTO item)
    {
		item.PROJETO_ID = !String.IsNullOrEmpty(ddlProjeto.SelectedValue) ? (Int32?)Convert.ToInt32(ddlProjeto.SelectedValue) : null;
		item.VALOR = Convert.ToDecimal(txtValor.Text);
		item.STATUS = Convert.ToInt32(rdbStatus.SelectedValue);
		item.DATA_FATURAMENTO = DateTime.ParseExact(txtDataFaturamento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
		item.DATA_RECEBIMENTO = (item.STATUS == 2) ? (DateTime?)DateTime.ParseExact(txtDataRecebimento.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) : null;

		return item;
    }

    private void setCampos(FATURAMENTO item)
    {
		ddlProjeto.SelectedValue = Convert.ToString(item.PROJETO_ID);
		txtValor.Text = Convert.ToString(item.VALOR);
		rdbStatus.SelectedValue = Convert.ToString(item.STATUS);
		txtDataFaturamento.Text = item.DATA_FATURAMENTO.Value.ToString("dd/MM/yyyy");

		if(item.STATUS == 2)
		{
			txtDataRecebimento.Enabled = true;
			txtDataRecebimento.Text = item.DATA_RECEBIMENTO.Value.ToString("dd/MM/yyyy");
		}
	}

	private void LoadForm()
    {
		FATURAMENTO filtro = new FATURAMENTO();
        filtro.FATURAMENTO_ID = Convert.ToInt32(Request.QueryString["id"]);
        this.setCampos(new FATURAMENTO_Service().Carregar(filtro));
    }

	private void CarregarCombos()
	{
		List<PROJETO> projetos = new PROJETO_Service().Listar(PROJETO_Ordem.NOME, OrdemTipo.Ascendente);

		List<ListaProjetos> lstProjetos = projetos.Select(projeto => new ListaProjetos(projeto)).ToList();

		lstProjetos.Sort(new orderListaProjetos());

		ddlProjeto.DataSource = lstProjetos;
		ddlProjeto.DataTextField = "AGENCIA_CLIENTE_PROJETO";
		ddlProjeto.DataValueField = "PROJETO_ID";
		ddlProjeto.DataBind();
		ddlProjeto.Items.Insert(0, new ListItem { Value = "", Text = "" });
	}

	public class ListaProjetos
	{
		public int PROJETO_ID { get; set; }
		public string AGENCIA_CLIENTE_PROJETO { get; set; }
		public ListaProjetos (PROJETO p)
		{
			this.PROJETO_ID = (Int32)p.PROJETO_ID;
			this.AGENCIA_CLIENTE_PROJETO = String.Concat(p.AGENCIA.NOME," - ",p.CLIENTE.NOME," - ",p.NOME);
		}
	}

	class orderListaProjetos : IComparer<ListaProjetos>
	{
		public int Compare(ListaProjetos x, ListaProjetos y)
		{
			if (x.AGENCIA_CLIENTE_PROJETO == null || y.AGENCIA_CLIENTE_PROJETO == null)
			{
				return 0;
			}
			return x.AGENCIA_CLIENTE_PROJETO.CompareTo(y.AGENCIA_CLIENTE_PROJETO);
		}
	}

	#endregion
}