<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Account/AccountPage.Master"  CodeBehind="DevUp.aspx.cs" Inherits="Games.NBall.AdminWeb.Develop.DevUp" %>

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
                    <td>修改等级
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       等级：<asp:TextBox runat="server" ID="txtLevel" Width="60px" Text="15"></asp:TextBox>
                        经验：<asp:TextBox runat="server" ID="txtExp" Width="60px" Text="15"></asp:TextBox>
                       <asp:Button runat="server" ID="btnUpLevel" Text="修改" OnClick="btnUpLevel_Click"></asp:Button>
                    </td>
                </tr>
                <tr class="bgLightGray">
                    <td>修改Vip等级
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgWhite">
                       Vip等级：<asp:TextBox runat="server" ID="txtVipLevel" Width="60px" Text="15"></asp:TextBox>
                       <asp:Button runat="server" ID="btnUpVipLevel" Text="修改" OnClick="btnUpVipLevel_Click"></asp:Button>
                    </td>
                </tr>
    <tr class="bgLightGray">
                    <td>修改球员卡等级
                    </td>
                </tr>
    <tr style="height: 25px">
                    <td class="bgWhite">
                      球员卡ID：<asp:TextBox runat="server" ID="txtPlayerId" Width="60px" Text="15"></asp:TextBox>
                      训练等级：<asp:TextBox runat="server" ID="txtPlayerlevel" Width="60px" Text="15"></asp:TextBox>
                       <asp:Button runat="server" ID="btnPlayerLevel" Text="修改" OnClick="btnPlayerLevel_Click"></asp:Button>
                    </td>
                </tr>
            </table>

</asp:Content>
