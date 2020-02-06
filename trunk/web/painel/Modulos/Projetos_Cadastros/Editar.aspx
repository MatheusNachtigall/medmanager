<%@ Page ClientIDMode="AutoID" Title="" Language="C#" MasterPageFile="~/painel/Interna/Interna.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Manager_Modulos_Projetos_Cadastros_Editar" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

	<div class="overlay" style="display:none;"></div>
    
    <asp:PlaceHolder runat="server" ID="phOverlay" Visible="false">
        <div class="overlay"></div>
    </asp:PlaceHolder>

	<asp:UpdatePanel runat="server" ID="upConteudo">
        <ContentTemplate>

			<div id="title">
				<h1>Projetos - Cadastros</h1>
			</div>

			<asp:PlaceHolder runat="server" ID="phFeedback" Visible="false">
				<div id="divFeedBackInsert">
					<asp:Literal runat="server" ID="ltlObs"></asp:Literal>
				</div>
			</asp:PlaceHolder>

			<div class="p20 pt0">
				<asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome">Nome:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtNome" CssClass="frmTxt" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblAgencia" runat="server" AssociatedControlID="ddlAgencia">Agência</asp:Label>
				<div class="select">
					<asp:DropDownList runat="server" AutoPostBack="true" ID="ddlAgencia" Width="700px" ClientIDMode="Static" OnSelectedIndexChanged="ddlAgenciaChanged"></asp:DropDownList>
				</div>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblCliente" runat="server" AssociatedControlID="ddlCliente">Cliente</asp:Label>
				<div class="select">
					<asp:DropDownList runat="server" AutoPostBack="true" ID="ddlCliente" Width="700px" ClientIDMode="Static" OnSelectedIndexChanged="ddlClienteChanged"></asp:DropDownList>
				</div>
			</div>
			
			<div class="p20 pt0">
				<asp:Label ID="lblDescricao" runat="server" AssociatedControlID="txtDescricao">Descrição:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtDescricao" CssClass="frmTxt" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>
			
			<div class="p20 pt0">
				<asp:Label ID="lblEscopo" runat="server" AssociatedControlID="txtEscopo">Escopo:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtEscopo" CssClass="frmTxt" Width="678px" TextMode="MultiLine" Height="100"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblValor" runat="server" AssociatedControlID="txtValor">Valor (R$):</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtValor" CssClass="frmTxt mask-money" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblSolicitante" runat="server" AssociatedControlID="txtSolicitante">Solicitante:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtSolicitante" CssClass="frmTxt" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblStatus" runat="server" AssociatedControlID="rdbStatus">Status:</asp:Label>
				<asp:RadioButtonList CssClass="radio-list" ID="rdbStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbStatusChanged" >
					<asp:ListItem Text="Prospectado" Value="1" Selected="True" />
					<asp:ListItem Text="Contratado" Value="2" />
				</asp:RadioButtonList>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblDataProspeccao" runat="server" AssociatedControlID="txtDataProspeccao">Data Prospecção:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtDataProspeccao" CssClass="frmTxt mask-date" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblDataContratacao" runat="server" AssociatedControlID="txtDataContratacao">Data Contratação:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtDataContratacao" CssClass="frmTxt mask-date" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblLocal" runat="server" AssociatedControlID="rdbLocal">Local de Trabalho:</asp:Label>
				<asp:RadioButtonList CssClass="radio-list" ID="rdbLocal" runat="server" RepeatDirection="Horizontal">
					<asp:ListItem Text="Blueshift" Value="1" Selected="True" />
					<asp:ListItem Text="Blueshift + Sede Cliente" Value="2" />
				</asp:RadioButtonList>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblGarantia" runat="server" AssociatedControlID="txtGarantia">Garantia(Dias):</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtGarantia" CssClass="frmTxt mask-numero" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblValidade" runat="server" AssociatedControlID="txtValidade">Validade(Dias):</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtValidade" CssClass="frmTxt mask-numero" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblPrazo" runat="server" AssociatedControlID="txtPrazo">Prazo(Dias):</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtPrazo" CssClass="frmTxt mask-numero" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblHoras" runat="server" AssociatedControlID="txtHoras">Horas:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtHoras" CssClass="frmTxt mask-numero" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblPrazoPagamento" runat="server" AssociatedControlID="txtPrazoPagamento">Prazo de Pagamento(Dias):</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtPrazoPagamento" CssClass="frmTxt mask-numero" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

	
			<div class="options editar">
				<asp:PlaceHolder runat="server" ID="phOptions" Visible="true">
					<asp:Button runat="server" ID="btnOK" CssClass="submit" Text="Salvar" OnClick="btnOK_Click" />
					<asp:Button runat="server" ID="btnCancelar" CssClass="cancelar" Text="Cancelar" OnClick="btnCancelar_Click" />
					<asp:PlaceHolder runat="server" ID="phExcluir" Visible="false">
						<input id="btnExcluir" type="button" class="excluir" name="Excluir" value="Excluir" />
					</asp:PlaceHolder>

					<div id="popup-confirma" title="Confirmar" class="modal" style="display:none;">
						<label class="text-modal">
							Tem certeza que deseja excluir?
						</label>

						<div>  
							<asp:Button runat="server" ClientIDMode="Static" ID="aspbtnConfirma" CssClass="submit" Text="Confirmar" OnClick="btnExcluir_Click"/>
							<input id="btnPopupClose" type="button" class="cancelar" name="Close" value="Cancelar" />
						</div>
					</div>
				</asp:PlaceHolder>

				<asp:PlaceHolder runat="server" ID="phFeedbackExcluir" Visible="false">
					<div class="modal">
						<label class="text-modal">
							<asp:Label ID="msgFeedbackExcluir" runat="server"> </asp:Label>
						</label>

						<div>
							<asp:Button runat="server" CssClass="cancelar" Text="Ok" OnClick="btnVoltar_Click" />
						</div>
					</div> 
				</asp:PlaceHolder>
			</div>

		</ContentTemplate>
	</asp:UpdatePanel>

	<script>
		$('#btnExcluir').click(function () {
            $('#popup-confirma').show();
            $('.overlay').show();
        });
        $('#btnPopupClose').click(function () {
            $('#popup-confirma').hide();
            $('.overlay').hide();
		});
    </script>

</asp:Content>
