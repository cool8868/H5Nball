<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test1.aspx.cs" Inherits="Games.NBall.AdminWeb.Develop.test1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
            <tr class="bgLightGray">
                <td colspan="4">缓存管理
                </td>
            </tr>
         <tr>
                            <td class="bgWhite" colspan="4" style="color: Red;">
                                <div class="errorMessage" style="padding: 0px;">
                                    <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                                </div>
                            </td>
                        </tr>
            <tr>
            <td class="bgColor2" style="width: 118px">选择区：</td>
            <td class="bgWhite" style="width: 260px"><asp:DropDownList runat="server" ID="ddlZone"/></td>
                <td class="bgColor2" style="width: 118px">缓存类型：</td>
            <td class="bgWhite"><asp:DropDownList runat="server" ID="ddlCacheType"/></td>
                </tr>
        <tr class="bgColor2">
            <td class="bgColor2" style="width: 118px">操作</td>
            <td colspan="3">
                <asp:Button runat="server" ID="btnSearch" Text="重启" OnClick="btnReset_Click"/>
            </td>
        </tr>
            </table>
    </form>
</body>
</html>

