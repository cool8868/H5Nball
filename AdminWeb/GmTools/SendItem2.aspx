<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="SendItem2.aspx.cs" Inherits="Games.NBall.AdminWeb.Tools.SendItem2" %>

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
                    <td>发送装备
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       物品编码：<asp:TextBox runat="server" ID="txtItemCode" Width="60px"></asp:TextBox>
                       数量：<asp:TextBox runat="server" ID="txtItemCount" Width="60px" Text="1" Enabled="False"></asp:TextBox>
                       强化等级：<asp:TextBox runat="server" ID="txtItemStrength" Width="60px" Text="1"></asp:TextBox>
                        <asp:CheckBox runat="server" ID="chkBinding" Text="绑定" Checked="True"/>
                        彩色插槽数量：<asp:TextBox runat="server" ID="txtSlotColorCount" Width="60px" Text="0"></asp:TextBox>
                       <asp:Button runat="server" ID="btnSendItem" Text="发送" OnClick="btnSendItem_Click"></asp:Button>
                    </td>
                </tr>
            </table>

</asp:Content>

