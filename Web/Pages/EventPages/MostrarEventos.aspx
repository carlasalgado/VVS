<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="MostrarEventos.aspx.cs" 
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages.MostrarEventos" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
 <form runat="server">
    <p><asp:Label ID="lblNoEvents" meta:resourcekey="lblNoEvents" runat="server"></asp:Label></p>
    <asp:GridView ID="gvEvents" runat="server" CssClass="evento"
        AutoGenerateColumns="False" Width="80%" HorizontalAlign="Center" onrowcommand="gvEvents_RowCommand">
        <Columns>
            <asp:BoundField DataField="idEvento" HeaderText="<%$ Resources:, idEvento %>" />
            <asp:BoundField DataField="nombre" HeaderText="<%$ Resources:, nombreEvento %>" />
            <asp:BoundField DataField="reseña" HeaderText="<%$ Resources:, reseña %>" />
            <asp:BoundField DataField="fecha" HeaderText="<%$ Resources:, fechaEvento %>" />
            <asp:ButtonField Text="<%$ Resources:, recomendar %>"  CommandName="Insert" ItemStyle-Width="150" />
            <asp:ButtonField Text="<%$ Resources:, verComentarios %>"  CommandName="Select" ItemStyle-Width="150" />
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
</asp:Content>
