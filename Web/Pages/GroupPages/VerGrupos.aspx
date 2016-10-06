<%@ Page Language="C#"  MasterPageFile="~/PracticaMaD.Master"  AutoEventWireup="true" CodeBehind="VerGrupos.aspx.cs"
     Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.GroupPages.VerGrupos" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
        <form id="AllGroupsForm" method="post" runat="server">
        <p>
          <asp:Label ID="lblNoGroups" meta:resourcekey="lblNoGroups" runat="server"></asp:Label></p>
        <asp:GridView ID="gvGroups" runat="server" CssClass="grupo" GridLines="None"
            AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="gvGroups_SelectedIndexChanged" >
            <Columns>
                <asp:BoundField DataField="idGrupo" HeaderText="<%$ Resources:, idGrupo %>" />
                <asp:BoundField DataField="nombre" HeaderText="<%$ Resources:, nombreGrupo %>" />
                <asp:BoundField DataField="descripcion" HeaderText="<%$ Resources:, descripcionGrupo %>" />
                <asp:BoundField DataField="nMiembros" HeaderText= "<%$ Resources:, nMiembros %>" />
                <asp:BoundField DataField="nRecomendaciones" HeaderText="<%$ Resources:, nRecomendaciones %>" />
                <asp:ButtonField Text="<%$ Resources:, unirseGrupo %>"  CommandName="Select" ItemStyle-Width="150" />
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
