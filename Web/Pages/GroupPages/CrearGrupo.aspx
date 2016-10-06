<%@ Page Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" 
    CodeBehind="CrearGrupo.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.GroupPages.CrearGrupo"
    meta:resourcekey="Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_BodyContent"
    runat="server">
    <div id="form">
        <form id="GroupCreationForm" method="POST" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclGroupName" runat="server" meta:resourcekey="lclGroupName" /></span><span
                        class="entry">
                    <asp:TextBox ID="txtGroupName" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvGroupName" runat="server"
                            ControlToValidate="txtGroupName" Display="Dynamic" Text="<%$ Resources:Common, mandatoryField %>"/>
                    <asp:Label ID="lblGroupNameError" runat="server" ForeColor="Red" Style="position: relative"
                            Visible="False" meta:resourcekey="lblGroupNameError">                        
                    </asp:Label>

                </span>
            </div>

            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclGroupDescripton" runat="server" meta:resourcekey="lclGroupDescription" /></span><span
                        class="entry">
                    <asp:TextBox ID="txtGroupDescription" runat="server" Width="349px" Columns="255" Height="127px" TextMode="MultiLine"></asp:TextBox>
                </span>
            </div>

            <div class="button">
                <asp:Button ID="btnCreateGroup" runat="server" OnClick="BtnCreateGroupClick" meta:resourcekey="btnCreateGroup" />
            </div>
        </form>
    </div>
</asp:Content>