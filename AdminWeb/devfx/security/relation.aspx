<%@ Page Language="C#" AutoEventWireup="true" Codebehind="relation.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.RelationPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>授权</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tr>
				<td>
					修改/新建角色权限之间的关系：<asp:Label ID="lblMessage" runat="server" EnableViewState="False"
						ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
						<tr class="bgWhite">
							<th width="25%" class="bgColor2">
								角色</th>
							<td>
								<asp:DropDownList ID="ddlRoleNo" runat="server" CssClass="inputWidth">
								</asp:DropDownList><asp:RequiredFieldValidator ID="rfvRoleNo" runat="server" ErrorMessage="角色必填"
									ControlToValidate="ddlRoleNo">*</asp:RequiredFieldValidator></td>
							<td width="40%">
								&nbsp;</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								权限</th>
							<td>
								<asp:DropDownList ID="ddlPermissionNo" runat="server" CssClass="inputWidth" AutoPostBack="True" OnSelectedIndexChanged="ddlPermissionNo_SelectedIndexChanged">
								</asp:DropDownList><asp:RequiredFieldValidator ID="rfvPermissionNo" runat="server"
									ErrorMessage="权限必填" ControlToValidate="ddlPermissionNo">*</asp:RequiredFieldValidator></td>
							<td>
								&nbsp;</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								权限范围</th>
							<td>
								<asp:DropDownList ID="ddlResNo" runat="server" CssClass="inputWidth">
								</asp:DropDownList></td>
							<td>
								&nbsp;</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								优先级</th>
							<td>
								<asp:TextBox ID="txtPriority" runat="server" CssClass="inputWidth">0</asp:TextBox><asp:RequiredFieldValidator
									ID="rfvPriority" runat="server" ErrorMessage="优先级必填" ControlToValidate="txtPriority">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
										ID="revPriority" runat="server" ErrorMessage="优先级必须为数字" ControlToValidate="txtPriority"
										ValidationExpression="-?(\d)*">*</asp:RegularExpressionValidator></td>
							<td>
								数值越高，优先级越高</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								权限属性</th>
							<td>
								<asp:CheckBox ID="cbxExecute" runat="server" Text="Execute"></asp:CheckBox><br>
								<asp:CheckBox ID="cbxList" runat="server" Text="List"></asp:CheckBox><br>
								<asp:CheckBox ID="cbxGrant" runat="server" Text="Grant"></asp:CheckBox></td>
							<td>
								权限属性说明：<br>
								<br>
								Grant&nbsp;&nbsp; - 可以把此权限授权给别的角色<br>
								List&nbsp;&nbsp;&nbsp; - 此权限可否被列出<br>
								Execute&nbsp;- 此权限是否能被执行（如果要拒绝权限，设置为不可执行）</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								备 注</th>
							<td>
								<asp:TextBox ID="txtRemark" runat="server" CssClass="inputWidth"></asp:TextBox></td>
							<td>
								&nbsp;</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsRelation" runat="server" ShowSummary="False" ShowMessageBox="True">
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
			<tr>
				<td>
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
