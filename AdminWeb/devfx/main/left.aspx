<%@ Page Language="C#" AutoEventWireup="true" Codebehind="left.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Main.LeftPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>²Ëµ¥µ¼º½</title>
	<base target="devfxMainFrame" />
    <script language="javascript" type="text/javascript">
		if(window.frameElement.name == "devfxMainFrame") {
			window.location.href = "main.aspx";
		}
    </script>
    <script language="javascript" src="../resource/script/xtree.js"></script>
    <link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body class="bgColor1" leftmargin="0">
	<table width="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				&nbsp;</td>
		</tr>
		<tr>
			<td>
				<asp:Label ID="lblUserName" runat="server"></asp:Label></td>
		</tr>
		<tr>
			<td>
				&nbsp;</td>
		</tr>
		<tr>
			<td valign="top">
				<asp:Label ID="lblMenu" runat="server"></asp:Label></td>
		</tr>
		<tr>
			<td height="100%">
				&nbsp;</td>
		</tr>
	</table>
    <script language="javascript">
        //p_1.expandFirst();
    </script>
</body>
</html>
