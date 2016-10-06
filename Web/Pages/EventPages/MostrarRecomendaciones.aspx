<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="MostrarRecomendaciones.aspx.cs" 
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages.MostrarRecomendaciones" meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
 <form runat="server">
    <p><asp:Label ID="lblNoEvents" meta:resourcekey="lblNoEvents" runat="server"></asp:Label></p>
    <asp:GridView ID="gvEvents" runat="server" CssClass="recomendacion"
        AutoGenerateColumns="False" Width="80%" HorizontalAlign="Center" onrowcommand="gvEvents_RowCommand">
        <Columns>
            <asp:BoundField DataField="idEvento" HeaderText="<%$ Resources:, idEvento %>" />
            <asp:ButtonField DataTextField="nombre" CommandName="Select" HeaderText="<%$ Resources:, nombreEvento %>" />
            <asp:BoundField DataField="reseña" HeaderText="<%$ Resources:, reseña %>" />
            <asp:BoundField DataField="recomendacion" HeaderText="<%$ Resources:, recomendacion %>" />
            <asp:BoundField DataField="fechaRecomendacion" HeaderText="<%$ Resources:, fechaRecomendacion %>" />
            <asp:BoundField DataField="fecha" HeaderText="<%$ Resources:, fechaEvento %>" />
        </Columns>
    </asp:GridView>
    </form>
    <br />
</asp:Content>
