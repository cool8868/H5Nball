<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiveReward.aspx.cs" Inherits="Games.NBall.AdminWeb.GmTools.GiveReward" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>真实比赛发奖</title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
        <tr class="bgLightGray">
            <td colspan="2">真实比赛发奖
            </td>
        </tr>
        <tr>
        <td class="bgWhite" style="color: Red;" colspan="2">
                        <div class="errorMessage" style="padding: 0px;">
                        </div>
        </td></tr>
        <tr>
        <td class="bgWhite">所属区：</td>
            <td class="bgWhite"><asp:DropDownList ID="SZone" runat="server" Height="16px" Width="110px"></asp:DropDownList>
                <asp:Button runat="server" ID="btnSend" Text="发放奖励" Width="60px" Height="25px" OnClick="btnSend_Click"></asp:Button>
            </td>
            </tr>
        <tr >
            <td class="bgColor2" style="width: 118px">
               
            </td>
            <td class="bgWhite">
                
                            <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                
            </td>
            </tr>
        <tr height="60px">
            <td class="bgWhite" colspan="2">
                <div style="margin-left: 300px"></div>
            </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
