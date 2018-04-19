<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenGamble.aspx.cs" Inherits="Games.NBall.AdminWeb.GmTools.OpenGamble" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>开奖</title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
        <tr class="bgLightGray">
            <td colspan="2">公布结果
            </td>
        </tr>
        <tr>
        <td class="bgWhite" style="color: Red;" colspan="2">
                        <div class="errorMessage" style="padding: 0px;">
                            <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
        </td></tr>
        <tr>
        <td class="bgWhite">所属区：</td>
            <td class="bgWhite"><asp:DropDownList ID="SZone" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SZone_SelectedIndexChanged"></asp:DropDownList>
                <asp:CheckBox ID="chkAll" runat="server" Text="所有区" Checked="True" AutoPostBack="True" />
            </td>
            </tr>
        <tr >
            <td class="bgColor2" style="width: 118px">
                竞猜主题：
            </td>
            <td class="bgWhite">
                <div><asp:DropDownList ID="ddltNeedOpenGambleTitle" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddltNeedOpenGambleTitle_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </td>
            </tr>
        <tr>
            <td class="bgColor2" style="width: 118px">
                选项</td>
            <td class="bgWhite">
               
                <asp:RadioButtonList ID="rblOptions" runat="server" AutoPostBack="True">
                </asp:RadioButtonList>
            </td>
            </tr>
       
        <tr height="60px">
            <td class="bgWhite" colspan="2">
                <div style="margin-left: 300px"><asp:Button runat="server" ID="btnSend" Text="确定" Width="60px" Height="25px" OnClick="btnSend_Click"></asp:Button></div>
            </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
