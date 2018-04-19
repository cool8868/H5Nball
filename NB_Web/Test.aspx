<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Games.NBall.NB_Web.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>test</title>
</head>
<body>
    <form id="form1" runat="server">
        <div><%=ServerIp %></div>
    <asp:TextBox ID="txtText" runat="server"></asp:TextBox>
    <asp:Button runat="server" ID="btnFitler1" Text="Filter1" OnClick="btnFitler1_Click"/>
        <asp:Button runat="server" ID="FastFilter" Text="FastFilter" OnClick="FastFilter_Click" style="height: 21px"/>
        <br/>
        结果：<br/>
        <asp:Label ID="lblText" runat="server"></asp:Label>
        
    <div>
        时间戳:<br/>
        平台参数：<asp:TextBox ID="txtTimetick" runat="server"></asp:TextBox>
        服务器时间：<asp:TextBox ID="txtTimeServer" runat="server"></asp:TextBox>
    <asp:Button runat="server" ID="btnTimetick" Text="解析" OnClick="btnTimetick_Click"/>
        <br/>
        结果：<br/>
        平台参数：<asp:Label ID="lblTimetick" runat="server"></asp:Label>
        服务器时间：<asp:Label ID="lblTimeServer" runat="server"></asp:Label>
    </div>
    </form>
    

</body>
</html>