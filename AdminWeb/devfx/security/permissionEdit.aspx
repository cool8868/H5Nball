<%@ Page Language="C#" AutoEventWireup="true" Codebehind="permissionEdit.aspx.cs"
	Inherits="HTB.DevFx.Security.Web.Pages.Security.PermissionEditPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>权限修改</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tr>
				<td>
					修改/新建权限：<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
						<tr>
							<th class="bgColor2">
								上级权限编号</th>
							<td class="bgWhite">
								<asp:HyperLink ID="hlkParentNo" runat="server"></asp:HyperLink></td>
							<td class="bgWhite">
								树型结构中本权限上级权限的编号</td>
						</tr>
						<tr>
							<th width="25%" class="bgColor2">
								权限编号</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtPermissionNo" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvPermissioNo" runat="server" ErrorMessage="权限编号必填" ControlToValidate="txtPermissionNo">*</asp:RequiredFieldValidator></td>
							<td width="40%" class="bgWhite">
								请保证系统唯一，请不要使用中文</td>
						</tr>
						<tr>
							<th class="bgColor2">
								权限名称</th>
							<td class="bgWhite">
								<font color="#ff0000">
									<asp:TextBox ID="txtTitle" runat="server" CssClass="inputWidth" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator
										ID="rfvTitle" runat="server" ErrorMessage="权限名称必填" ControlToValidate="txtTitle">*</asp:RequiredFieldValidator></font></td>
							<td class="bgWhite">
								请输入有意义的名称</td>
						</tr>
						<tr>
							<th class="bgColor2">
								权限描述</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtDescription" runat="server" CssClass="inputWidth" MaxLength="120"></asp:TextBox></td>
							<td class="bgWhite">
								权限的详细描述</td>
						</tr>
						<tr>
							<th class="bgColor2">
								权限范围类型</th>
							<td class="bgWhite">
								<asp:DropDownList ID="ddlResType" runat="server" CssClass="inputWidth">
								</asp:DropDownList></td>
							<td class="bgWhite">
								数据权限对应范围的类型，比如发帖权限对应的范围类型是版面等</td>
						</tr>
						<tr>
							<th class="bgColor2">
								资源1</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtRes1" runat="server" CssClass="inputWidth" MaxLength="255"></asp:TextBox></td>
							<td class="bgWhite">
								权限资源1（比如菜单对应的URL）</td>
						</tr>
						<tr>
							<th class="bgColor2">
								资源2</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtRes2" runat="server" CssClass="inputWidth" MaxLength="255"></asp:TextBox></td>
							<td class="bgWhite">
								权限资源2（比如菜单对应的图片URL）</td>
						</tr>
						<tr>
							<th class="bgColor2">
								显示顺序</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtDispIndex" runat="server" CssClass="inputWidth">0</asp:TextBox><asp:RequiredFieldValidator
									ID="rfvDispIndex" runat="server" ErrorMessage="显示顺序必填" ControlToValidate="txtDispIndex">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
										ID="revDispIndex" runat="server" ErrorMessage="显示顺序必须为数字" ControlToValidate="txtDispIndex"
										ValidationExpression="-?(\d)*">*</asp:RegularExpressionValidator></td>
							<td class="bgWhite">
								针对菜单可调节显示顺序</td>
						</tr>
						<tr>
							<th class="bgColor2">
								层级</th>
							<td class="bgWhite">
								<asp:Label ID="lblLayerIndex" runat="server"></asp:Label></td>
							<td class="bgWhite">
								树型结构中本权限所处的层级（约定根的层级为0）</td>
						</tr>
						<tr>
							<th class="bgColor2">
								是否可见</th>
							<td class="bgWhite">
								<font color="#ff0000">
									<asp:DropDownList ID="ddlIsView" runat="server" CssClass="inputWidth">
										<asp:ListItem Value="True">是</asp:ListItem>
										<asp:ListItem Value="False" Selected="True">否</asp:ListItem>
									</asp:DropDownList></font></td>
							<td class="bgWhite">
								是否为可显示的权限，一般菜单此属性为“是”</td>
						</tr>
						<tr>
							<th class="bgColor2">
								是否有效</th>
							<td class="bgWhite">
								<font color="#ff0000">
									<asp:DropDownList ID="ddlIsAvailable" runat="server" CssClass="inputWidth">
										<asp:ListItem Value="True">是</asp:ListItem>
										<asp:ListItem Value="False">否</asp:ListItem>
									</asp:DropDownList></font></td>
							<td class="bgWhite">
								本权限是否有效</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsPermission" runat="server" ShowMessageBox="True" ShowSummary="False">
					</asp:ValidationSummary>
				</td>
			</tr>
			<tr>
				<td align="center">
					<asp:Button ID="btnSubmit" runat="server" CssClass="buttonWidth" Text="提 交" OnClick="btnSubmit_Click">
					</asp:Button>&nbsp;<input class="buttonWidth" type="button" value="返 回" onclick="<%=ReturnUrl%>"></td>
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
