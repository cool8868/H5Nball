<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Account/AccountPage.Master"  CodeBehind="AccountFrozen.aspx.cs" Inherits="Games.NBall.AdminWeb.Tools.AccountFrozen" %>

<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
    <tr>
        <td class="bgWhite" style="color: Red;">
                        <div class="errorMessage" style="padding: 0px;">
                            <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
        </td></tr>
                <tr class="bgLightGray">
                    <td>踢线
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       <asp:Button runat="server" ID="btnKickSession" Text="踢线" OnClick="btnKickSession_Click"></asp:Button>
                    </td>
                </tr>
                <tr class="bgLightGray">
                    <td>封停
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                        备注：<asp:TextBox runat="server" ID="txtLockMemo" Width="160px"></asp:TextBox>
                       <asp:Button runat="server" ID="btnLockUserUnexpect" Text="封停" OnClick="btnLockUserUnexpect_Click"></asp:Button>
                    </td>
                </tr>
                <tr class="bgLightGray">
                    <td>解封
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                        备注：<asp:TextBox runat="server" ID="txtUnlockMemo" Width="160px"></asp:TextBox>
                       <asp:Button runat="server" ID="btnUnlockUser" Text="解封" OnClick="btnUnlockUser_Click"></asp:Button>
                    </td>
                </tr>
            </table>

</asp:Content>
