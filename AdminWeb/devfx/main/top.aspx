<%@ Page Language="C#" AutoEventWireup="true" Codebehind="top.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Main.TopPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>系统信息</title>
</head>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" class="bgColor">
	<table width="100%" border="0" cellpadding="4" cellspacing="0" style='background: url(<%= this.ClientScript.GetWebResourceUrl(typeof(HTB.DevFx.Security.Web.Pages.Main.TopPage), "HTB.DevFx.Security.Web.Pages.devfx.resource.image.top_bg.gif") %>)'>
		<tr>
			<td>
				<font style="font-size: 10pt; color: #a50606"><b>
					<asp:Label ID="lblAppTitle" runat="server"></asp:Label></b></font>
				<asp:Label ID="lblVersion" Visible="False" runat="server" EnableViewState="False" ForeColor="#023E3E"
					Font-Name="verdana" Font-Size="9px"></asp:Label></td>
			<td align="right" width="200" style='height: 33px; background: url(<%= this.ClientScript.GetWebResourceUrl(typeof(HTB.DevFx.Security.Web.Pages.Main.TopPage), "HTB.DevFx.Security.Web.Pages.devfx.resource.image.top_bg.gif") %>) no-repeat right 0px'>
				
			</td>
		</tr>
	</table>
</body>
</html>
