<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="SendItem.aspx.cs" Inherits="Games.NBall.AdminWeb.Tools.SendItem" %>

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
                    <td>发送物品
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       物品编码：<asp:TextBox runat="server" ID="txtItemCode" Width="60px"></asp:TextBox>
                       数量：<asp:TextBox runat="server" ID="txtItemCount" Width="60px" Text="1"></asp:TextBox>
                       强化等级：<asp:TextBox runat="server" ID="txtItemStrength" Width="60px" Text="1"></asp:TextBox>
                        <asp:CheckBox runat="server" ID="chkBinding" Text="绑定" Checked="True"/>
                        <asp:CheckBox runat="server" ID="chkDeal" Text="勾选可交易" Checked="True"/>
                       <asp:Button runat="server" ID="btnSendItem" Text="发送" OnClick="btnSendItem_Click"></asp:Button>
                    </td>
                </tr>
                <tr class="bgLightGray">
                    <td>发送金币
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       数量：<asp:TextBox runat="server" ID="txtCoinCount" Width="60px" Text="1000"></asp:TextBox>
                       <asp:Button runat="server" ID="btnSendCoin" Text="发送" OnClick="btnSendCoin_Click"></asp:Button>
                    </td>
                </tr>
    <tr class="bgLightGray">
                    <td>发送点券
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       数量：<asp:TextBox runat="server" ID="txtPointCount" Width="60px" Text="10"></asp:TextBox>
                       <asp:Button runat="server" ID="btnSendPoint" Text="发送" OnClick="btnSendPoint_Click"></asp:Button>
                    </td>
                </tr>
                <tr class="bgLightGray">
                    <td>gm充值(会自动提升vip)
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       点券数量：<asp:TextBox runat="server" ID="txtGmPoint" Width="60px" Text="10"></asp:TextBox>
                       <asp:Button runat="server" ID="btnGmCharge" Text="发送" OnClick="btnGmCharge_Click"></asp:Button>
                        <asp:Button runat="server" ID="btnGetGmPoint" Text="查看已充值点数" OnClick="btnGetGmPoint_Click"/>
                        已充值点数：<asp:Label runat="server" ID="lblGmChargePoint"></asp:Label>
                    </td>
                </tr>
    <tr class="bgLightGray">
                    <td>发送金条
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       金条数量：<asp:TextBox runat="server" ID="txt_GoldBar" Width="60px" Text="10"></asp:TextBox>
                       <asp:Button runat="server" ID="btn_GoldBar" Text="发送" OnClick="btnGoldBar_Click"></asp:Button>
                    </td>
                </tr>
    
     <tr class="bgLightGray">
                    <td>充值道具
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       道具ID：<asp:TextBox runat="server" ID="txt_mallId" Width="60px" Text=""></asp:TextBox>
                       <asp:Button runat="server" ID="btn_SendMallItem" Text="购买" OnClick="btn_SendMallItem_Click"></asp:Button>
                    </td>
                </tr>
            </table>

</asp:Content>

