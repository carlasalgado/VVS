<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="ModificarComentario.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages.ModificarComentario" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
    <form id="ModifyCommentForm" method="POST" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lcltexto" runat="server" meta:resourcekey="textoComentario" /></span><span
                    class="entry">
                    <asp:TextBox ID="txtTexto" runat="server" Width="349px" Columns="255" Height="127px" TextMode="MultiLine"></asp:TextBox>
                </span>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Etiquetas" meta:resourcekey="lblTags" runat="server"></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="lblNoTags" meta:resourcekey="lblNoTags" runat="server"></asp:Label>
        <asp:GridView ID="gvEtiquetas" runat="server" CssClass="etiqueta"
            AutoGenerateColumns="False" Width="80%" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="idEtiqueta" HeaderText="<%$ Resources:, idEtiqueta %>" />
                <asp:BoundField DataField="nombre" HeaderText="<%$ Resources:, nombreEtiqueta %>" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSeleccion" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="button">
            <asp:Button ID="btnModificarComentario" runat="server" OnClick="BtnModificarComentarioClick" meta:resourcekey="btnModificarComentario" />
        </div>
                <div class="button">
            <asp:Button ID="btnAnadirEtiqueta" runat="server" OnClick="BtnAnadirEtiquetaClick" meta:resourcekey="btnAnadirEtiqueta" />
        </div>
    </form>

</asp:Content>
