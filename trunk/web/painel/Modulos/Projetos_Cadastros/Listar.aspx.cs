using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM_Blue.Entity;
using CRM_Blue.Service;
using CRM_Blue.Enumerator;
using CRM_Blue;
using System.Text;
using Utilities;
using System.IO;
using TemplateEngine.Docx;
using System.Web;

public partial class Manager_Modulos_Projetos_Cadastros_Listar : System.Web.UI.Page
{
    #region Variáveis
    private int Pagina;
    private int TotalPagina;
    private int PaginaExibir;
    #endregion

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
            this.CarregarLista();
        }
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        this.CarregarLista();
        //ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
    }
    
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtNome.Text = String.Empty;
		ddlUsuario.SelectedIndex = 0;
		ddlAgencia.SelectedIndex = 0;
		ddlCliente.SelectedIndex = 0;
		this.CarregarLista();
        //ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
    }

	protected void btnGerarProposta(object sender, CommandEventArgs e)
	{
		PROJETO projeto = new PROJETO_Service().Carregar(new PROJETO() { PROJETO_ID = Convert.ToInt32(e.CommandArgument) });

		string path = HttpContext.Current.Server.MapPath("~/painel/TemplateProposta/");
		string template = string.Concat(path, "NOME-DESCRICAO-ANO-MES-DIA.docx");
		string outputFilename = string.Concat(projeto.NOME.Replace(" ", "_"), "-", projeto.DESCRICAO.Replace(" ", "_"), "-", DateTime.Now.ToString("yyyy-MM-dd"), ".docx");

		string local = "As atividades serão realizadas nas instalações da BLUESHIFT";
		if (projeto.LOCAL_TRABALHO == 2)
		{
			local += " ou na sede do ";
			local += projeto.CLIENTE.NOME;
		}

		Directory.CreateDirectory(string.Concat(path, "temp"));
		string tempPath = HttpContext.Current.Server.MapPath("~/painel/TemplateProposta/temp/");
		string outputFile = string.Concat(tempPath, outputFilename);

		if (File.Exists(outputFile)) File.Delete(outputFile);
		File.Copy(template, outputFile);

		string escopo = projeto.ESCOPO.Replace("\n", "\r\n");

		var valuesToFill = new TemplateEngine.Docx.Content(
			new FieldContent("NOME", projeto.NOME),
			new FieldContent("DESCRICAO", projeto.DESCRICAO),
			new FieldContent("ESCOPO", escopo),
			new FieldContent("CLIENTE", projeto.CLIENTE.NOME),
			new FieldContent("DIA", DateTime.Now.ToString("dd")),
			new FieldContent("MES", DateTime.Now.ToString("MM")),
			new FieldContent("ANO", DateTime.Now.ToString("yyyy")),
			new FieldContent("SOLICITANTE", projeto.SOLICITANTE),
			new FieldContent("FRASE_LOCAL", local),
			new FieldContent("GARANTIA_DIAS", projeto.GARANTIA_DIAS.ToString()),
			new FieldContent("VALIDADE_DIAS", projeto.VALIDADE_DIAS.ToString()),
			new FieldContent("PRAZO_DIAS", projeto.PRAZO_DIAS.ToString()),
			new FieldContent("HORAS", projeto.HORAS.ToString()),
			new FieldContent("VALOR", ((Decimal)projeto.VALOR).ToString("C")),
			new FieldContent("PRAZO_PAGAMENTO_DIAS", projeto.PRAZO_PAGAMENTO_DIAS.ToString())
		);

		using (var outputDocument = new TemplateProcessor(outputFile).SetRemoveContentControls(true))
		{
			outputDocument.FillContent(valuesToFill);
			outputDocument.SaveChanges();
		}

		using (FileStream fileStream = File.OpenRead(outputFile))
		{
			MemoryStream memStream = new MemoryStream();
			memStream.SetLength(fileStream.Length);
			fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);

			Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
			Response.AddHeader("Content-Disposition", string.Concat("attachment; filename=", outputFilename));
			Response.BinaryWrite(memStream.ToArray());
		}
		Directory.Delete(tempPath, true);
		Response.End();

	}

	protected void btnDuplicar(object sender, CommandEventArgs e)
	{
		PROJETO_Service service = new PROJETO_Service();

		PROJETO projeto = service.Carregar(new PROJETO() { PROJETO_ID = Convert.ToInt32(e.CommandArgument) });

		USUARIO session = (USUARIO)Session["usuario"];

		PROJETO clone = new PROJETO()
		{
			USUARIO_ID = session.USUARIO_ID,
			AGENCIA_ID = projeto.AGENCIA_ID,
			CLIENTE_ID = projeto.CLIENTE_ID,
			NOME = projeto.NOME,
			DESCRICAO = projeto.DESCRICAO,
			VALOR = projeto.VALOR,
			SOLICITANTE = projeto.SOLICITANTE,
			STATUS = projeto.STATUS,
			DATA_PROSPECCAO = projeto.DATA_PROSPECCAO,
			DATA_CONTRATACAO = projeto.DATA_CONTRATACAO,
			LOCAL_TRABALHO = projeto.LOCAL_TRABALHO,
			GARANTIA_DIAS = projeto.GARANTIA_DIAS,
			VALIDADE_DIAS = projeto.VALIDADE_DIAS,
			PRAZO_DIAS = projeto.PRAZO_DIAS,
			HORAS = projeto.HORAS,
			PRAZO_PAGAMENTO_DIAS = projeto.PRAZO_PAGAMENTO_DIAS,
			DATA_CADASTRO = DateTime.Now,
			ESCOPO = projeto.ESCOPO
		};

		clone = service.Inserir(clone);

		Response.Redirect("./Editar.aspx?ID="+clone.PROJETO_ID.ToString());
	}

	protected void lnkPagina_Click(object sender, EventArgs e)
    {
        this.Pagina = Convert.ToInt32(((LinkButton)sender).CommandArgument);
        this.CarregarLista();
        //ScriptManager.RegisterClientScriptBlock(upConteudo, upConteudo.GetType(), "update", "PageLoad();", true);
    }

    protected void rptTable_ItemDataBound(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PROJETO item = (PROJETO)e.Item.DataItem;
            ((HiddenField)e.Item.FindControl("hdnID")).Value = item.PROJETO_ID.ToString();
			((Literal)e.Item.FindControl("ltlNome")).Text = item.NOME;
			((Literal)e.Item.FindControl("ltlUsuario")).Text = item.USUARIO.NOME;
			((Literal)e.Item.FindControl("ltlAgencia")).Text = item.AGENCIA.NOME;
			((Literal)e.Item.FindControl("ltlCliente")).Text = item.CLIENTE.NOME;
			((Literal)e.Item.FindControl("ltlDescricao")).Text = item.DESCRICAO;
			((Literal)e.Item.FindControl("ltlValor")).Text = (item.VALOR == null) ? String.Empty : ((Decimal)item.VALOR).ToString("0.00");
			((Literal)e.Item.FindControl("ltlSolicitante")).Text = item.SOLICITANTE;
			((Literal)e.Item.FindControl("ltlStatus")).Text = (item.STATUS == 1) ? "Prospectado" : "Contratado";
			((Literal)e.Item.FindControl("ltlDataProspeccao")).Text = item.DATA_PROSPECCAO.Value.ToString("dd/MM/yyyy");
			((Literal)e.Item.FindControl("ltlDataContratacao")).Text = (item.DATA_CONTRATACAO != null) ? item.DATA_CONTRATACAO.Value.ToString("dd/MM/yyyy") : "-";
			((LinkButton)e.Item.FindControl("btnGerar")).CommandArgument = item.PROJETO_ID.ToString();
			((LinkButton)e.Item.FindControl("btnDuplicar")).CommandArgument = item.PROJETO_ID.ToString();
		}
    }

    protected void rptPagina_ItemDataBound(Object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            int item = (int)e.Item.DataItem;
            LinkButton lkbPagina = (LinkButton)e.Item.FindControl("lkbPagina");
            lkbPagina.Text = item.ToString();
            lkbPagina.CommandArgument = item.ToString();
            if (item == this.Pagina) lkbPagina.CssClass = "on";
        }
    }
    #endregion

    #region Métodos
    private void CarregarFiltro()
    {
        PROJETO filtro = new PROJETO();
        filtro.NOME = String.Concat("%", txtNome.Text, "%");
		filtro.USUARIO_ID = String.IsNullOrEmpty(ddlUsuario.SelectedValue) ? null : (Int32?)Convert.ToInt32(ddlUsuario.SelectedValue);
		filtro.AGENCIA_ID = String.IsNullOrEmpty(ddlAgencia.SelectedValue) ? null : (Int32?)Convert.ToInt32(ddlAgencia.SelectedValue);
		filtro.CLIENTE_ID = String.IsNullOrEmpty(ddlCliente.SelectedValue) ? null : (Int32?)Convert.ToInt32(ddlCliente.SelectedValue);

		Session["PROJETO_Filtro"] = filtro;
    }

    private void CarregarLista()
    {
        CarregarFiltro();
        PROJETO_Ordem Ordem = (Session["PROJETO_Ordem"] != null) ? (PROJETO_Ordem)Session["PROJETO_Ordem"] : PROJETO_Ordem.NOME;
        OrdemTipo OrdemTipo = (Session["PROJETO_Ordem"] != null) ? (OrdemTipo)Session["PROJETO_Ordem_Tipo"] : OrdemTipo.Ascendente;
        Paginacao<PROJETO> pagLista = new PROJETO_Service().Listar((PROJETO)Session["PROJETO_Filtro"], this.Pagina, 10, Ordem, OrdemTipo);
        rptTable.DataSource = pagLista.Itens;
        rptTable.DataBind();
        pnlNenhumRegistro.Visible = (pagLista.Itens.Count == 0);
        pnlRegistrosEncontrados.Visible = (pagLista.Itens.Count != 0);
        ltlQuantidadeRegistrosEncontrados.Text = pagLista.Total.ToString();
        this.TotalPagina = pagLista.Paginas;
        this.PaginaExibir = 9;
        this.CarregarPaginacao();
    }

	private void CarregarCombos()
	{
		ddlUsuario.DataSource = new USUARIO_Service().Listar(USUARIO_Ordem.NOME, OrdemTipo.Ascendente);
		ddlUsuario.DataTextField = "NOME";
		ddlUsuario.DataValueField = "USUARIO_ID";
		ddlUsuario.DataBind();
		ddlUsuario.Items.Insert(0, new System.Web.UI.WebControls.ListItem { Value = "", Text = "" });

		ddlAgencia.DataSource = new AGENCIA_Service().Listar(AGENCIA_Ordem.NOME, OrdemTipo.Ascendente);
		ddlAgencia.DataTextField = "NOME";
		ddlAgencia.DataValueField = "AGENCIA_ID";
		ddlAgencia.DataBind();
		ddlAgencia.Items.Insert(0, new System.Web.UI.WebControls.ListItem { Value = "", Text = "" });

		ddlCliente.DataSource = new CLIENTE_Service().Listar(CLIENTE_Ordem.NOME, OrdemTipo.Ascendente);
		ddlCliente.DataTextField = "NOME";
		ddlCliente.DataValueField = "CLIENTE_ID";
		ddlCliente.DataBind();
		ddlCliente.Items.Insert(0, new System.Web.UI.WebControls.ListItem { Value = "", Text = "" });
	}

	public void CarregarPaginacao()
    {
        if (this.Pagina < 1)
        {
            this.Pagina = 1;
        }
        if (this.Pagina > this.TotalPagina)
        {
            this.Pagina = this.TotalPagina;
        }

        if ((this.PaginaExibir % 2) == 0)
        {
            this.PaginaExibir--;
        }
        int PaginasLado = (this.PaginaExibir - 1) / 2;
        int PaginaInicial = 0;
        int PaginaFinal = 0;
        if (this.TotalPagina > 1)
        {
            if (this.TotalPagina <= this.PaginaExibir)
            {
                PaginaInicial = 1;
                PaginaFinal = this.TotalPagina;
            }
            else
            {
                if ((this.TotalPagina - this.Pagina) >= PaginasLado)
                {
                    if (this.Pagina <= PaginasLado)
                    {
                        PaginaInicial = 1;
                        PaginaFinal = this.Pagina + PaginasLado + (PaginasLado - this.Pagina) + 1;
                        PaginaFinal = (PaginaFinal > this.TotalPagina) ? this.TotalPagina : PaginaFinal;
                    }
                    else
                    {
                        PaginaInicial = this.Pagina - PaginasLado;
                        PaginaFinal = this.Pagina + PaginasLado;
                    }
                }
                else
                {
                    PaginaInicial = this.Pagina - (PaginasLado + (PaginasLado - (this.TotalPagina - this.Pagina)));
                    PaginaFinal = this.TotalPagina;
                }
            }
        }
        if (this.TotalPagina > 1)
        {
            pnlPaginacao.Visible = true;
            lkbPrimeira.Attributes["style"] = "visibility:" + ((this.Pagina != 1) ? "visible" : "hidden");
            lkbAnterior.Attributes["style"] = "visibility:" + ((this.Pagina != 1) ? "visible" : "hidden");
            lkbProxima.Attributes["style"] = "visibility:" + ((this.Pagina < this.TotalPagina) ? "visible" : "hidden");
            lkbUltima.Attributes["style"] = "visibility:" + ((this.Pagina < this.TotalPagina) ? "visible" : "hidden");
            lkbPrimeira.CommandArgument = "1";
            lkbAnterior.CommandArgument = (this.Pagina - 1).ToString();
            lkbProxima.CommandArgument = (this.Pagina + 1).ToString();
            lkbUltima.CommandArgument = this.TotalPagina.ToString();
            List<int> lstPagina = new List<int>();
            for (int i = PaginaInicial; i <= PaginaFinal; i++)
            {
                lstPagina.Add(i);
            }
            rptPagina.DataSource = lstPagina;
            rptPagina.DataBind();
        }
        else
        {
            pnlPaginacao.Visible = false;
        }
    }
    #endregion
}