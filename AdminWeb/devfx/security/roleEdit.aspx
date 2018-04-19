<%@ Page Language="C#" AutoEventWireup="true" Codebehind="roleEdit.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.RoleEditPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>角色修改</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tr>
				<td>
					修改/新建角色：<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
						<tr>
							<th width="25%" class="bgColor2">
								角色编号</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtRoleNo" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvRoleNo" runat="server" ErrorMessage="角色编号必填" ControlToValidate="txtRoleNo">*</asp:RequiredFieldValidator></td>
							<td width="40%" class="bgWhite">
								保证系统唯一，请不要使用中文</td>
						</tr>
						<tr>
							<th class="bgColor2">
								角色名称</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtTitle" runat="server" CssClass="inputWidth" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvTitle" runat="server" ErrorMessage="角色名称必填" ControlToValidate="txtTitle">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite">
								请填写有意义的名称</td>
						</tr>
						<tr>
							<th class="bgColor2">
								角色描述</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtDescription" runat="server" CssClass="inputWidth" MaxLength="120"></asp:TextBox></td>
							<td class="bgWhite">
								更详细的描述</td>
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
								<asp:DropDownList ID="ddlIsInnerRole" runat="server" Enabled="False">
									<asp:ListItem Value="True">是</asp:ListItem>
									<asp:ListItem Value="False" Selected="True">否</asp:ListItem>
								</asp:DropDownList></td>
							<td class="bgWhite">
								系统内置角色，意味着不可被任何人删除</td>
						</tr>
						<tr>
							<th class="bgColor2">
								选 项</th>
							<td class="bgWhite">
								<asp:CheckBox ID="cbxAddMeToRole" runat="server" Text="把我添加到此角色中" Checked="True"></asp:CheckBox></td>
							<td class="bgWhite">
								针对新建角色有效</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsRole" runat="server" ShowMessageBox="True" ShowSummary="False">
					</asp:ValidationSummary>
				</td>
			</tr>
			<tr>
				<td align="center">
					<asp:Button ID="btnSubmit" runat="server" Text="提 交" CssClass="buttonWidth" OnClick="btnSubmit_Click">
					</asp:Button>&nbsp;<input name="Button" type="button" class="buttonWidth" value="返 回"
						onclick="<%=ReturnUrl%>"></td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
