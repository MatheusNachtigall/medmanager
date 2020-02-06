<%@ Page ClientIDMode="AutoID" Title="" Language="C#" MasterPageFile="~/painel/Interna/Interna.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Manager_Modulos_Plantoes_Cadastros_Editar" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

	<div class="overlay" style="display:none;"></div>
    
    <asp:PlaceHolder runat="server" ID="phOverlay" Visible="false">
        <div class="overlay"></div>
    </asp:PlaceHolder>

	<asp:UpdatePanel runat="server" ID="upConteudo">
        <ContentTemplate>

			<div id="title">
				<h1>Plantões - Cadastros</h1>
			</div>

			<asp:PlaceHolder runat="server" ID="phFeedback" Visible="false">
				<div id="divFeedBackInsert">
					<asp:Literal runat="server" ID="ltlObs"></asp:Literal>
				</div>
			</asp:PlaceHolder>

			<div class="p20 pt0">

            <asp:PlaceHolder runat="server" ID="phAtalhos" Visible="false">
			    <div class="cross-links clearfix">
				    <a class="atalho-plantao" href="/painel/Modulos/Deducoes_Cadastros/Listar.aspx" title="Ver Deducoes" style="width:120px">Ver Deduções</a>
			    </div>
		    </asp:PlaceHolder>


				<asp:Label ID="lblHospital" runat="server" AssociatedControlID="ddlHospital">Hospital</asp:Label>
                <div class="select">
                    <asp:DropDownList runat="server" ID="ddlHospital" Width="324px" ClientIDMode="Static"></asp:DropDownList>
                </div>

                <asp:Label ID="lblValor" runat="server" AssociatedControlID="txtValor">Valor (R$):</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtValor" CssClass="frmTxt mask-money" Width="678px" MaxLength="256"></asp:TextBox>
				</span>

                <div class="clearfix pb10">
                    <asp:Label ID="lblCNPJ" runat="server" CssClass="pt3" AssociatedControlID="chkCNPJ">CNPJ:</asp:Label>
			        <div class="input-check">
				        <asp:CheckBox runat="server" ID="chkCNPJ" CssClass="fl" Checked="false" />
			        </div>
                </div>

                <div class="clearfix pb10">
                    <asp:Label ID="lblINSS" runat="server" CssClass="pt3" AssociatedControlID="chkINSS">INSS:</asp:Label>
			        <div class="input-check">
				        <asp:CheckBox runat="server" ID="chkINSS" CssClass="fl" Checked="false" />
			        </div>
                </div>

			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblDataPlantao" runat="server" AssociatedControlID="txtDataPlantao">Data Plantão:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtDataPlantao" CssClass="frmTxt mask-date" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

			<div class="p20 pt0">
				<asp:Label ID="lblDataPagamento" runat="server" AssociatedControlID="txtDataPagamento">Data Pagamento:</asp:Label>
				<span class="input-txt">
					<asp:TextBox runat="server" ID="txtDataPagamento" CssClass="frmTxt mask-date" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
			</div>

            <div class="p20 pt0">
                <div class="clearfix pb10">
                    <asp:Label ID="lblRecebido" runat="server" CssClass="pt3" AssociatedControlID="chkRecebido">Recebido:</asp:Label>
			        <div class="input-check">
				        <asp:CheckBox runat="server" ID="chkRecebido" CssClass="fl" Checked="false" />
			        </div>
                </div>
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
