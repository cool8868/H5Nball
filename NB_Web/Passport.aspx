<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Passport.aspx.cs" Inherits="Games.NBall.NB_Web.Passport" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>线下登录器</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%">
    <table style="width: 100%; height: 100%; vertical-align: middle; text-align: center;
        margin-top: 300px">
        <tr>
            <td>
                <table>
                    <tr>
                        <td td align="left" width="80px">
                            平台：
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlPlatform"/>
                        </td>
                        <td align="left" width="80px">
                            用户名：
                        </td>
                        <td align="left" width="160px">
                            <asp:TextBox ID="txtName" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnLogin" Text="登 录" runat="server" Width="80" OnClick="BtnLoginClick" />
                            <asp:Button ID="btnTest" Visible="False" Text="测试服务端" runat="server" Width="120" OnClick="BtnTestClick" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="left">
                            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Red"
                                Text="请输入用户名!"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
