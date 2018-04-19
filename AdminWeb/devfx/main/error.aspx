<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Main.ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>出错啦</title>
</head>
<body>
	<asp:Label id="lblMessage" runat="server" EnableViewState="False">有错误发生！</asp:Label>
</body>
</html>
