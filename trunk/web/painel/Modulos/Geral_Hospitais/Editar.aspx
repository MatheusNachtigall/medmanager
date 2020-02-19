<%@ Page ClientIDMode="AutoID" Title="" Language="C#" MasterPageFile="~/painel/Interna/Interna.master" AutoEventWireup="true" CodeFile="Editar.aspx.cs" Inherits="Manager_Modulos_Geral_Hospitais_Editar" ValidateRequest="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headInterna" runat="Server">
	<%--<script src="<%=ResolveUrl("~/painel/JS/farbtastic.js")%>"></script>--%>
	<script src="<%=ResolveUrl("~/painel/JS/farbtastic2.0.js")%>"></script>
    <link rel="stylesheet" href="<%=ResolveUrl("~/painel/CSS/farbtastic.css")%>">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

	<div class="overlay" style="display:none;"></div>
    
    <asp:PlaceHolder runat="server" ID="phOverlay" Visible="false">
        <div class="overlay"></div>
    </asp:PlaceHolder>

	<asp:UpdatePanel runat="server" ID="upConteudo">
        <ContentTemplate>

			<div id="title">
				<h1>Geral - Hospitais</h1>
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
				<asp:Label ID="lblCidade" runat="server" AssociatedControlID="txtCidade">Cidade:</asp:Label>
                <span class="input-txt">
					<asp:TextBox runat="server" ID="txtCidade" CssClass="frmTxt" Width="678px" MaxLength="256"></asp:TextBox>
				</span>
            </div>

            <div class="p20 pt0">
                <%--<asp:Label ID="lblCor" runat="server" AssociatedControlID="color">Cor:</asp:Label>--%>
               <%-- <div class="input-txt">
                    <input type="color" name="favcolor" value="#ff0000">
                </div>--%>
            </div>
            
<%--            <div class="p20 pt0">
                <input type="text" id="color" name="color" value="#123456" />
                <div id="colorpicker"></div>
            </div>--%>
            <div class="p20 pt0">
                <div class="colorpicker">
                    <div id="colorpicker"></div>
                    <asp:Label ID="lblCor" runat="server" AssociatedControlID="color">Cor:</asp:Label>
				    <asp:TextBox runat="server" ID="color" CssClass="frmTxt" Width="222px" MaxLength="256" ClientIDMode="Static"></asp:TextBox>
				
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
        $(document).ready(function() {
            $('#colorpicker').farbtastic({ callback: '#color', width: 150 });
        });

            
    
        //$(function () {
        //  $('#colorpicker1').farbtastic('#color1');
        //  $('#colorpicker2').farbtastic({ callback: '#color2', width: 150 });
        //});
    
    </script>

</asp:Content>
