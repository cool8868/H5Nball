<%@ Page Language="C#" AutoEventWireup="true" Codebehind="system.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.SystemPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>系统维护</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tr>
				<td>
					系统维护：
					<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					&nbsp;
					<asp:LinkButton ID="btnShutdown" runat="server" OnClick="btnShutdown_Click">重启本站点</asp:LinkButton></td>
			</tr>
		</table>
	</form>
</body>
</html>
