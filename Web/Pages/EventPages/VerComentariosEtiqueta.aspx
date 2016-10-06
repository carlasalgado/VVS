<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="VerComentariosEtiqueta.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages.VerComentariosEtiqueta" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
    <form id="TagCommentsGridViewForm" method="POST" runat="server">
        <p>
            <asp:Label ID="nombre" runat="server" meta:resourcekey="nombreTag"></asp:Label>
            <asp:Label ID="lblTag" runat="server"></asp:Label>
        </p>
        <p><asp:Label ID="lblNoComments" meta:resourcekey="lblNoComments" runat="server"></asp:Label></p>
        <asp:GridView ID="gvComments" runat="server" CssClass="comentario"
            AutoGenerateColumns="False" Width="80%" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="idComentario" HeaderText="<%$ Resources:, idComentario %>" />
                <asp:BoundField DataField="login" HeaderText="<%$ Resources:, usuarioComentario %>" />
                <asp:BoundField DataField="fecha" HeaderText="<%$ Resources:, fechaComentario %>" />
                <asp:BoundField DataField="texto" HeaderText="<%$ Resources:, textoComentario %>" />
            </Columns>
        </asp:GridView>

    </form>
    <br />
    <br />
    <br />
</asp:Content>
