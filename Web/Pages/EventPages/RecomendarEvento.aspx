<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="RecomendarEvento.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages.RecomendarEvento" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
    <form id="EventRecommendGridViewForm" method="POST" runat="server">
        <p>
            <asp:Label ID="lblNoGroups" meta:resourcekey="lblNoGroups" runat="server"></asp:Label></p>
        <asp:GridView ID="gvGroups" runat="server" CssClass="grupo"
            AutoGenerateColumns="False" Width="80%" HorizontalAlign="Center">
            <Columns>

                <asp:BoundField DataField="idGrupo" HeaderText="<%$ Resources:, idGrupo %>" />
                <asp:BoundField DataField="nombre" HeaderText="<%$ Resources:, nombreGrupo %>" />
                <asp:BoundField DataField="descripcion" HeaderText="<%$ Resources:, descripcionGrupo %>" />
                <asp:BoundField DataField="nMiembros" HeaderText="<%$ Resources:, nMiembros %>" />
                <asp:BoundField DataField="nRecomendaciones" HeaderText="<%$ Resources:, nRecomendaciones %>" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSeleccion" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclTxtRecomendacion" runat="server" meta:resourcekey="lclTxtRecomendacion" /></span><span
                    class="entry">
                    <asp:TextBox ID="txtTxtRecomendacion" runat="server" Width="349px" Columns="255" Height="127px" TextMode="MultiLine"></asp:TextBox>
                </span>
        </div>
        <div class="button">
            <asp:Button ID="btnRecommendGroup" runat="server" OnClick="BtnRecommendClick" meta:resourcekey="btnRecommendGroup" />
        </div>
    </form>

    <br />
    <br />
</asp:Content>
