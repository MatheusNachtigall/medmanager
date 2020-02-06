<%@ Page ClientIDMode="AutoID" Title="" Language="C#" MasterPageFile="~/painel/Interna/Interna.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Manager_Modulos_Projetos_Faturamentos_Editar" ValidateRequest="false" %>

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
				<asp:Label ID="lblProjeto" runat="server" AssociatedControlID="ddlProjeto">Projeto</asp:Label>
				<div class="select">
					<asp:DropDownList runat="server" ID="ddlProjeto" Width="700px" ClientIDMode="Static"></asp:DropDownList>
				</div>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblValor" runat="server" AssociatedControlID="txtValor">Valor (R$):</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtValor" CssClass="frmTxt mask-money" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblStatus" runat="server" AssociatedControlID="rdbStatus">Status:</asp:Label>
				<asp:RadioButtonList CssClass="radio-list" ID="rdbStatus" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbStatusChanged" >
					<asp:ListItem Text="Faturado" Value="1" Selected="True" />
					<asp:ListItem Text="Recebido" Value="2" />
				</asp:RadioButtonList>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblDataFaturamento" runat="server" AssociatedControlID="txtDataFaturamento">Data Faturamento:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtDataFaturamento" CssClass="frmTxt mask-date" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblDataRecebimento" runat="server" AssociatedControlID="txtDataRecebimento">Data Recebimento:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtDataRecebimento" CssClass="frmTxt mask-date" Width="678px" MaxLength="256"></asp:TextBox>
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

				<%--<asp:PlaceHolder runat="server" ID="phErroExcluir" Visible="false">
					<div class="modal">
						<label class="text-modal">
							<asp:Label ID="msgErroExcluir" runat="server"> </asp:Label>
						</label>

						<div>
							<asp:Button runat="server" CssClass="cancelar" Text="Ok" OnClick="btnVoltar_Click" />
						</div>
					</div> 
				</asp:PlaceHolder>--%>
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
