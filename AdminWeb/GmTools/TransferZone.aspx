<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferZone.aspx.cs" Inherits="Games.NBall.AdminWeb.GmTools.TransferZone" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>转服</title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style3 {
            background-color: #FFFFFF;
            width: 80px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="bgWhite" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
        <tr class="bgLightGray">
            <td colspan="2">转区
            </td>
        </tr>
        <tr>
        <td class="bgWhite" style="color: Red;" colspan="2">
                        <div class="errorMessage" style="padding: 0px;">
                            <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
        </td></tr>
        <tr>
        <td class="auto-style3">原所属区：</td>
            <td class="bgWhite" style="width:138px"><asp:DropDownList ID="SZoneOld" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SZone_SelectedIndexChanged"></asp:DropDownList>
            </td>
            </tr>
        <tr>
            <td class="auto-style3">原区角色名：</td>
        <td class="bgWhite" style="width:238px"><asp:TextBox ID="txtOldRoleName" runat="server" AutoPostBack="true"></asp:TextBox></td>
             </tr>
        
        <tr>
            <td class="auto-style3">新所属区：</td>
            <td class="bgWhite" style="width:138px"><asp:DropDownList ID="SZoneNew" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SZone_SelectedIndexChanged"></asp:DropDownList>
            </td>
            </tr>
        <tr>
           <td class="auto-style3">新区角色名：</td>
        <td class="bgWhite" style="width:238px"><asp:TextBox ID="txtNewRoleName" runat="server" AutoPostBack="true"></asp:TextBox></td>
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
