<%@ Page Language="C#" AutoEventWireup="true" Codebehind="password.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.User.PasswordPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>密码管理</title>
    <link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table cellspacing="0" cellpadding="8" width="100%" border="0">
			<tr>
				<td>
					用户密码：<asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table class="bgDark" cellspacing="1" cellpadding="4" width="100%" border="0">
						<tr>
							<th class="bgColor2" width="25%">
								原密码</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtOldPassword" runat="server" CssClass="inputWidth" MaxLength="50"
									TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="rfvOldPassword"
										runat="server" ErrorMessage="原密码必填" ControlToValidate="txtOldPassword">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite" width="40%">
								您的原来密码</td>
						</tr>
						<tr>
							<th class="bgColor2">
								新密码</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtNewPassword" runat="server" MaxLength="50" CssClass="inputWidth"
									TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="rfvNewPassword"
										runat="server" ControlToValidate="txtNewPassword" ErrorMessage="新密码必填">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite">
								输入的新密码</td>
						</tr>
						<tr>
							<th class="bgColor2">
								重复新密码</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="inputWidth" MaxLength="50"
									TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="rfvConfigPassword"
										runat="server" ControlToValidate="txtConfirmPassword" ErrorMessage="重复新密码必填">*</asp:RequiredFieldValidator><asp:CompareValidator
											ID="cvNewPassword" runat="server" ErrorMessage="两次输入的密码不一致" ControlToValidate="txtConfirmPassword"
											ControlToCompare="txtNewPassword">*</asp:CompareValidator></td>
							<td class="bgWhite">
								重复输入新密码</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsUser" runat="server" ShowSummary="False" ShowMessageBox="True">
					</asp:ValidationSummary>
				</td>
			</tr>
			<tr>
				<td align="center">
					<asp:Button ID="btnSubmit" runat="server" CssClass="buttonWidth" Text="提 交" OnClick="btnSubmit_Click">
					</asp:Button></td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
