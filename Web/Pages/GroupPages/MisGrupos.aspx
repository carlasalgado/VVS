<%@ Page Language="C#"   MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="MisGrupos.aspx.cs" 
    Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.GroupPages.MisGrupos" meta:resourcekey="Page"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <br />
        <form id="MyGroupsGridViewForm" method="POST" runat="server">
            <p><asp:Label ID="lblNoGroups" meta:resourcekey="lblNoGroups" runat="server"></asp:Label></p>
            <asp:GridView ID="gvGroups" runat="server" CssClass="grupo"
                AutoGenerateColumns="False" Width="80%" HorizontalAlign="Center" OnSelectedIndexChanged="gvGroups_SelectedIndexChanged">
                <Columns>

                    <asp:BoundField DataField="idGrupo" HeaderText="<%$ Resources:, idGrupo %>" />
                    <asp:BoundField DataField="nombre" HeaderText="<%$ Resources:, nombreGrupo %>" />
                    <asp:BoundField DataField="descripcion" HeaderText="<%$ Resources:, descripcionGrupo %>" />
                    <asp:BoundField DataField="nMiembros" HeaderText= "<%$ Resources:, nMiembros %>" />
                    <asp:BoundField DataField="nRecomendaciones" HeaderText="<%$ Resources:, nRecomendaciones %>" />
                    <asp:ButtonField Text="<%$ Resources:, baja %>"  CommandName="Select" ItemStyle-Width="150"   />
                </Columns>
            </asp:GridView>
        
        </form>

    <br />
    <br />
</asp:Content>