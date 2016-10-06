<%@ Page Language="C#"   MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="VerComentarios.aspx.cs" 
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages.VerComentarios" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
        <form id="EventCommentsGridViewForm" method="POST" runat="server">
            <p>
                <asp:Label ID="nombre" runat="server"  meta:resourcekey="nombreEvento"></asp:Label>
                <asp:Label ID="lblnombreEvento" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Label ID="fecha" runat="server" meta:resourcekey="fechaEvento"></asp:Label>
                <asp:Label ID="lblfechaEvento" runat="server"></asp:Label>
            </p>
            <p>
                <asp:Label ID="reseña" runat="server" meta:resourcekey="reseñaEvento"></asp:Label>
                <asp:Label ID="lblreseñaEvento" runat="server"></asp:Label>
            </p>

                   
            <div class="button">
                <asp:Button ID="btnComentar" runat="server" OnClick="BtnComentarClick" meta:resourcekey="btnComentar" />
            </div>   

            <p><asp:Label ID="lblNoComments" meta:resourcekey="lblNoComments" runat="server"></asp:Label></p>
            <asp:GridView ID="gvComments" runat="server" CssClass="comentario"
                AutoGenerateColumns="False" Width="80%" HorizontalAlign="Center" onrowcommand="gvComments_RowCommand">
                <Columns>
                    <asp:BoundField DataField="idComentario" HeaderText="<%$ Resources:, idComentario %>" />
                    <asp:BoundField DataField="login" HeaderText="<%$ Resources:, usuarioComentario %>" />
                    <asp:BoundField DataField="fecha" HeaderText="<%$ Resources:, fechaComentario %>" />
                    <asp:BoundField DataField="texto" HeaderText= "<%$ Resources:, textoComentario %>" />
                    <asp:ButtonField Text="<%$ Resources:, verComentario %>"  CommandName="Ver" ItemStyle-Width="150" />
                    <asp:ButtonField Text="<%$ Resources:, modificarComentario %>"  CommandName="Modificar" ItemStyle-Width="150" />
                    <asp:ButtonField Text="<%$ Resources:, eliminarComentario %>"  CommandName="Eliminar" ItemStyle-Width="150" />
                </Columns>
            </asp:GridView>
        
        </form>
       <br />
        <!-- "Previous" and "Next" links. -->
        <div class="previousNextLinks">
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                    Visible="False"></asp:HyperLink>
                </span><span class="nextLink">
                <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
            </span>
        </div>
        <br />
        <br />
    <br />
    <br />
</asp:Content>