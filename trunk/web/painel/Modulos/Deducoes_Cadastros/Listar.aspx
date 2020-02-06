<%@ Page ClientIDMode="AutoID" Title="" Language="C#" MasterPageFile="~/painel/Interna/Interna.master" AutoEventWireup="true" CodeFile="Listar.aspx.cs" Inherits="Manager_Modulos_Deducoes_Cadastros_Listar" %>

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
                <a href="Editar.aspx" class="adicionar"><span class="ico"></span>Adicionar</a>
                <h1>Deduções - Cadastros</h1>
            </div>

            <div id="filtrar">
                <div class="p20 pt0 pb0 clearfix">
                     <div class="filtro-item">
                        <asp:Label ID="lblTipo" runat="server" AssociatedControlID="ddlTipo">Tipo</asp:Label>
                        <div class="select">
                            <asp:DropDownList runat="server" ID="ddlTipo" Width="240px"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="filtro-item">
                        <asp:Label ID="lblPlantaoID" runat="server" AssociatedControlID="ddlPlantaoID">Plantao ID</asp:Label>
                        <div class="select">
                            <asp:DropDownList runat="server" ID="ddlPlantaoID" Width="240px"></asp:DropDownList>
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
                <asp:Repeater runat="server" ID="rptTable" OnItemDataBound="rptTable_ItemDataBound">
                <HeaderTemplate>
                    <table width="1100" class="tb-registros">
                    <colgroup>
                        <col width="80" />
                        <col />
                        <col width="180" />
                        <col width="80" />
                        <col width="180" />
                    </colgroup>
                    <thead>
                        <tr>
                        <th>ID</th>
                        <th>Tipo</th>
                        <th>Valor (R$)</th>
                        <th>Plantão_ID</th>
                        <th>Data da Dedução</th>
                        </tr>
                    </thead>
                    <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="td-detalhe"><asp:Literal runat="server" ID="ltlID"></asp:Literal><asp:HiddenField runat="server" ID="hdnID" /></div></td>
                        <td class="td-detalhe"><asp:Literal runat="server" ID="ltlTipo"></asp:Literal></td>
                        <td class="td-detalhe"><asp:Literal runat="server" ID="ltlValor"></asp:Literal></td>
                        <td class="td-detalhe"><asp:Literal runat="server" ID="ltlPlantaoID"></asp:Literal></td>
                        <td class="td-detalhe"><asp:Literal runat="server" ID="ltlDataDeducao"></asp:Literal></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
                </asp:Repeater>
                <div class="txt-n-reg"><span><asp:Literal runat="server" ID="ltlQuantidadeRegistrosEncontrados"></asp:Literal></span> registro(s) encontrados(s)</div>

                <div class="paginacao">
                    <asp:Panel runat="server" ID="pnlPaginacao" CssClass="pt15 clr">
                        <asp:LinkButton runat="server" ID="lkbPrimeira" OnClick="lnkPagina_Click">Primeira</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lkbAnterior" OnClick="lnkPagina_Click">Anterior</asp:LinkButton>
                        <asp:Repeater runat="server" ID="rptPagina" OnItemDataBound="rptPagina_ItemDataBound">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lkbPagina" OnClick="lnkPagina_Click"></asp:LinkButton>
                        </ItemTemplate>
                        </asp:Repeater>
                        <asp:LinkButton runat="server" ID="lkbProxima" OnClick="lnkPagina_Click">Próxima</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lkbUltima" OnClick="lnkPagina_Click">Última</asp:LinkButton>
                    </asp:Panel>
                </div>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
