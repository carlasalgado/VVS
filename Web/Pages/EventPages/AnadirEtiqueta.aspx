<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true"
    CodeBehind="AnadirEtiqueta.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.EventPages.AnadirEtiqueta"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="TagCreationForm" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclTag" runat="server" meta:resourcekey="lclTag" /></span><span
                        class="entry">
                    <asp:TextBox ID="txtTag" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTag" runat="server"
                            ControlToValidate="txtTag" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                    <asp:Label ID="lblTagError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblTagError">                        
                    </asp:Label>

                </span>
            </div>

            <div class="button">
                <asp:Button ID="btnCreateTag" runat="server" OnClick="BtnCreateTagClick" meta:resourcekey="btnCreateTag" />
            </div>
        </form>
    </div>
</asp:Content>