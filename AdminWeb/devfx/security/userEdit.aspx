<%@ Page Language="C#" AutoEventWireup="true" Codebehind="userEdit.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.UserEditPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>用户修改</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tr>
				<td>
					修改/新建用户：<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
						<tr>
							<th width="25%" class="bgColor2">
								用户编号</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtUserNo" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvUserNo" runat="server" ErrorMessage="用户编号必填" ControlToValidate="txtUserNo">*</asp:RequiredFieldValidator></td>
							<td width="40%" class="bgWhite">
								保证系统唯一，请不要使用中文</td>
						</tr>
						<tr>
							<th class="bgColor2">
								用户名</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtUserName" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvUserName" runat="server" ErrorMessage="用户名必填" ControlToValidate="txtUserName">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite">
								保证系统唯一（或从应用系统中导入）</td>
						</tr>
						<tr>
							<th class="bgColor2">
								用户密码</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtPassword" runat="server" CssClass="inputWidth" TextMode="Password"
									MaxLength="50"></asp:TextBox></td>
							<td class="bgWhite">
								（修改密码请填入新密码）</td>
						</tr>
						<tr>
							<th class="bgColor2">
								是否有效</th>
							<td class="bgWhite">
								<asp:DropDownList ID="ddlIsAvailable" runat="server">
									<asp:ListItem Value="True">是</asp:ListItem>
									<asp:ListItem Value="False">否</asp:ListItem>
								</asp:DropDownList></td>
							<td class="bgWhite">
								&nbsp;</td>
						</tr>
						<tr>
							<th class="bgColor2">
								是否内置</th>
							<td class="bgWhite">
								<asp:DropDownList ID="ddlIsInnerUser" runat="server" Enabled="False">
									<asp:ListItem Value="True">是</asp:ListItem>
									<asp:ListItem Value="False" Selected="True">否</asp:ListItem>
								</asp:DropDownList></td>
							<td class="bgWhite">
								系统内置用户，意味着不可被任何人删除</td>
						</tr>
						<tr>
							<th class="bgColor2">
								组</th>
							<td colspan="2" class="bgWhite">
								<table width="100%" border="0" cellspacing="0" cellpadding="4">
									<tr>
										<td width="10%">
											此用户隶属的组</td>
										<td width="1%">
											&nbsp;</td>
										<td>
											有效的组</td>
									</tr>
									<tr>
										<td>
											<asp:ListBox ID="lbxGroups" runat="server" CssClass="inputWidth" Rows="6" SelectionMode="Multiple">
											</asp:ListBox></td>
										<td align="center" nowrap>
											<asp:LinkButton ID="btnRemoveGroups" runat="server" CausesValidation="False" OnClick="btnRemoveGroups_Click">--></asp:LinkButton><br>
											<br>
											<asp:LinkButton ID="btnAddGroups" runat="server" CausesValidation="False" OnClick="btnAddGroups_Click"><--</asp:LinkButton></td>
										<td>
											<asp:ListBox ID="lbxAvailGroups" runat="server" CssClass="inputWidth" Rows="6" SelectionMode="Multiple">
											</asp:ListBox></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<th class="bgColor2">
								角色</th>
							<td colspan="2" class="bgWhite">
								<table width="100%" border="0" cellspacing="0" cellpadding="4">
									<tr>
										<td width="10%">
											此用户隶属的角色</td>
										<td width="1%">
											&nbsp;</td>
										<td>
											有效的角色</td>
									</tr>
									<tr>
										<td>
											<asp:ListBox ID="lbxRoles" runat="server" CssClass="inputWidth" Rows="6" SelectionMode="Multiple">
											</asp:ListBox></td>
										<td align="center" nowrap>
											<asp:LinkButton ID="btnRemoveRoles" runat="server" CausesValidation="False" OnClick="btnRemoveRoles_Click">--></asp:LinkButton><br>
											<br>
											<asp:LinkButton ID="btnAddRoles" runat="server" CausesValidation="False" OnClick="btnAddRoles_Click"><--</asp:LinkButton></td>
										<td>
											<asp:ListBox ID="lbxAailRoles" runat="server" CssClass="inputWidth" Rows="6" SelectionMode="Multiple">
											</asp:ListBox></td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsUser" runat="server" ShowMessageBox="True" ShowSummary="False">
					</asp:ValidationSummary>
				</td>
			</tr>
			<tr>
				<td align="center">
					<asp:Button ID="btnSubmit" runat="server" CssClass="buttonWidth" Text="提 交" OnClick="btnSubmit_Click"></asp:Button>&nbsp;<input
						class="buttonWidth" onclick="<%=ReturnUrl%>" type="button" value="返 回" name="Button"></td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
