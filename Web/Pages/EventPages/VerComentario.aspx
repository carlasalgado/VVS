<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="VerComentario.aspx.cs"
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages.VerComentario" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
    <form id="EventCommentsGridViewForm" method="POST" runat="server">
        <p style="text-align: center">
            <asp:Label ID="Evento" runat="server" meta:resourcekey="evento" Font-Size="Larger" Font-Bold="true" Font-Underline="true"></asp:Label>
        </p>
        <p>
            <asp:Label ID="nombre" runat="server" meta:resourcekey="nombreEvento" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblNombreEvento" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="fecha" runat="server" meta:resourcekey="fechaEvento" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblFechaEvento" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="reseña" runat="server" meta:resourcekey="reseñaEvento" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblReseñaEvento" runat="server"></asp:Label>
        </p>

        <br />
        <br />

        <p style="text-align: center">
            <asp:Label ID="Comentario" runat="server" meta:resourcekey="ComentarioTitulo" Font-Size="Larger" Font-Bold="true" Font-Underline="true"></asp:Label>
        </p>
        <p>
            <asp:Label ID="Usuario" runat="server" meta:resourcekey="loginUsuario" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblLogin" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="FechaComentario" runat="server" meta:resourcekey="fechaComentario" Font-Bold="true"></asp:Label>
            <asp:Label ID="lblFechaComentario" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="TextoComentario" runat="server" meta:resourcekey="comentario" Font-Bold="true"></asp:Label>
        </p>
        <p>
            <asp:Label ID="lblComentario" runat="server" Width="349px" Columns="255" Height="127px" TextMode="MultiLine"></asp:Label>
        </p>


        <asp:Label ID="lblNoTags" meta:resourcekey="lblNoTags" runat="server"></asp:Label>
        <asp:GridView ID="gvEtiquetas" runat="server" CssClass="etiqueta"
            AutoGenerateColumns="False" Width="80%" HorizontalAlign="Center">
            <Columns>
                <asp:BoundField DataField="nombre" HeaderText="<%$ Resources:, nombreEtiqueta %>" />
            </Columns>
        </asp:GridView>

    </form>
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
