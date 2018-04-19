<%@ Page Language="C#" AutoEventWireup="true" Codebehind="login.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Main.LoginPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title>登录系统</title>
</head>
<body>
	<form id="mainForm" runat="server">
		<table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
			<tr>
				<td height="20">
					&nbsp;</td>
			</tr>
			<tr>
				<td align="center">
					<table cellspacing="0" cellpadding="0" width="90%" border="0">
						<tr>
							<td align="center">
								<asp:Label ID="lblAppTitle" runat="server" ForeColor="Crimson" Font-Bold="True" Font-Italic="False" Font-Size="10pt"></asp:Label>
							</td>
						</tr>
						<tr><td>&nbsp;</td></tr>
						<tr>
							<td align="center">
								<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Blue"></asp:Label>
							</td>
						</tr>
					</table>
					<table cellspacing="0" cellpadding="1" width="1%" border="0">
						<tr>
							<td nowrap>
								用户名：
								<asp:TextBox ID="txtUserName" runat="server" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="必填">*</asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td nowrap>
								密&nbsp;&nbsp;码：
								<asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="必填">*</asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td align="center">
								<asp:Button ID="btnLogin" runat="server" Width="100px" Text="登 录" OnClick="btnLogin_Click"></asp:Button>
								<asp:HyperLink ID="hlkLoginByRemote" runat="server" Visible="False">使用远程登录</asp:HyperLink></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td height="100">
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
