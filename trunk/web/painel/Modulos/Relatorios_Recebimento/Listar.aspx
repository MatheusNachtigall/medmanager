<%@ Page ClientIDMode="AutoID" Title="" Language="C#" MasterPageFile="~/painel/Interna/Interna.master" AutoEventWireup="true" CodeFile="Listar.aspx.cs" Inherits="Manager_Modulos_Relatorios_Recebimento_Listar" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headInterna" runat="Server">
	<script src="<%=ResolveUrl("~/painel/JS/vendor/chart.js")%>"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="contentInterna" runat="Server">

    <asp:UpdateProgress ID="upgConteudo" runat="server" AssociatedUpdatePanelID="upConteudo">
        <ProgressTemplate>
            <div id="divBlockFormID" class="divBlockForm">
                <div class="overlay">
                    <!-- -->
                </div>
                <div class="txt">Aguarde</div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server" ID="upConteudo">
        <ContentTemplate>            

			<div id="title">
                <h1>Relatórios - Recebimento</h1>
            </div>

            <div id="filtrar">
                <div class="p20 pt0 pb0 clearfix">
					<div class="filtro-item">
                        <asp:Label ID="lblDataIni" runat="server" AssociatedControlID="ddlDataIni">Data Inicial</asp:Label>
                        <div class="select">
                            <asp:DropDownList runat="server" ID="ddlDataIni" Width="240px"></asp:DropDownList>
                        </div>
                    </div>
					<div class="filtro-item">
                        <asp:Label ID="lblDataFim" runat="server" AssociatedControlID="ddlDataFim">Data Final</asp:Label>
                        <div class="select">
                            <asp:DropDownList runat="server" ID="ddlDataFim" Width="240px"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="options clearfix">
                    <asp:Button runat="server" ID="btnBuscar" CssClass="submit" Text="Filtrar" OnClick="btnBuscar_Click" /><asp:Button runat="server" ID="btnLimpar" CssClass="limpar" Text="Limpar" OnClick="btnLimpar_Click" />
                </div>
            </div>

            <asp:Panel ID="pnlNenhumRegistro" Visible="false" runat="server">
                <div class="txt-nenhum-reg">Nenhum registro encontrado.</div>
            </asp:Panel>

            <asp:Panel ID="pnlRegistrosEncontrados" runat="server">

				<asp:HiddenField ID="hdDados" runat="server" ClientIDMode="Static" />
				<asp:HiddenField ID="hdNome" runat="server" ClientIDMode="Static" />

				<canvas id="relatorioChart" ></canvas>

                <asp:Repeater runat="server" ID="rptTable" OnItemDataBound="rptTable_ItemDataBound">
                <HeaderTemplate>
                    <table width="1100" class="tb-registros">
                    <colgroup>
                        <col />
                        <col />
                    </colgroup>
                    <thead>
                        <tr>
                        <th>Mês</th>
                        <th>Valor (R$)</th>
                        </tr>
                    </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class=""><asp:Literal runat="server" ID="ltlMes"></asp:Literal><asp:HiddenField runat="server" ID="hdnID" /></div></td>
                        <td class="mask-money"><asp:Literal runat="server" ID="ltlValor"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
                </asp:Repeater>
                <div class="txt-n-reg"><span><asp:Literal runat="server" ID="ltlQuantidadeRegistrosEncontrados"></asp:Literal></span> registro(s) encontrados(s)</div>

            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





