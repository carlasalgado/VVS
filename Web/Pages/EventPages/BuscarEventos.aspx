<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="BuscarEventos.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages.BuscarEventos" meta:resourcekey="Page" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">

    <form id="SearchEventsForm" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclkeyword" runat="server" meta:resourcekey="lclkeyword" /></span><span
                                class="entry">
                        <asp:TextBox ID="txtkeyword" runat="server" Width="100" Columns="16" ></asp:TextBox>
                </span>
            </div>           
            <div class="button">
                <asp:Button ID="btnBuscar" runat="server" OnClick="BtnBuscarClick" meta:resourcekey="btnBuscar" />
            </div>
    </form>


</asp:Content>