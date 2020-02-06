<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/painel/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Manager_Default" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ContentPlaceHolderID="content" runat="Server">
    <div class="border">
        <!-- -->
    </div>
	<div class="login-logo"></div>
    <div class="login-middle">
        <asp:PlaceHolder runat="server" ID="phErro" Visible="false">
            <p class="loginErrorMsg" id="txtErro" runat="server"></p>
        </asp:PlaceHolder>
		<asp:PlaceHolder runat="server" ID="phLogin" Visible="true">
			<div class="login-fields">
				<div>
					<asp:TextBox runat="server" ID="txtEmail" MaxLength="256"></asp:TextBox>
				</div>
				<div>
					<asp:TextBox runat="server" ID="txtSenha" MaxLength="256" TextMode="Password"></asp:TextBox>
				</div>
				<div>
					<asp:Button runat="server" ID="btSubmit" Text="OK" OnClick="btSubmit_Click" />
				</div>
			</div>
		</asp:PlaceHolder>
    </div>
</asp:Content>

