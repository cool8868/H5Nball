<%@ Page Language="C#" AutoEventWireup="true" Codebehind="userInfo.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.User.UserInfoPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>用户信息</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table cellspacing="0" cellpadding="8" width="100%" border="0">
			<tr>
				<td>
					用户资料：<asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table class="bgDark" cellspacing="1" cellpadding="4" width="100%" border="0">
						<tr>
							<th class="bgColor2" width="25%">
								用户编号</th>
							<td class="bgWhite">
								<asp:Label ID="lblUserNo" runat="server"></asp:Label></td>
							<td class="bgWhite" width="40%">
								您的用户编号</td>
						</tr>
						<tr>
							<th class="bgColor2">
								用户名</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtUserName" runat="server" MaxLength="50" CssClass="inputWidth"
									ReadOnly="True"></asp:TextBox><asp:RequiredFieldValidator ID="rfvUserName" runat="server"
										ControlToValidate="txtUserName" ErrorMessage="用户名必填">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite">
								您的登录用户名</td>
						</tr>
						<tr>
							<th class="bgColor2">
								组</th>
							<td class="bgWhite">
								<asp:ListBox ID="lbxGroups" runat="server" CssClass="inputWidth" SelectionMode="Multiple"
									Rows="6"></asp:ListBox></td>
							<td class="bgWhite">
								您所在的组</td>
						</tr>
						<tr>
							<th class="bgColor2">
								角色</th>
							<td class="bgWhite">
								<asp:ListBox ID="lbxRoles" runat="server" CssClass="inputWidth" SelectionMode="Multiple"
									Rows="6"></asp:ListBox></td>
							<td class="bgWhite">
								您隶属的角色</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsUser" runat="server" ShowSummary="False" ShowMessageBox="True">
					</asp:ValidationSummary>
				</td>
			</tr>
			<tr>
				<td align="center">
					<asp:Button ID="btnSubmit" runat="server" CssClass="buttonWidth" Text="提 交" OnClick="btnSubmit_Click"></asp:Button></td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
