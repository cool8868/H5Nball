<%@ Page Language="C#" AutoEventWireup="true" Codebehind="permissionRole.aspx.cs"
	Inherits="HTB.DevFx.Security.Web.Pages.Security.PermissionRolePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>权限和角色的关系</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tbody>
				<tr>
					<td>
						以下是拥有
						<asp:HyperLink ID="hlkPermission" runat="server"></asp:HyperLink>&nbsp;权限的角色列表（按优先级倒序排列）：<asp:Label
							ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th>
											角色名称[编号]</th>
										<th>
											权限范围</th>
										<th>
											优先级</th>
										<th>
											权限属性</th>
										<th>
											操作</th>
									</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr class="bgWhite">
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Role") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "ResNo") %>
									</td>
									<td align="right">
										<%# DataBinder.Eval(Container.DataItem, "Priority") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "StatusFlags") %>
									</td>
									<td align="center">
										<a href='relation.aspx?RelationID=<%# DataBinder.Eval(Container.DataItem, "RelationID") %>'>
											修改</a>/<asp:LinkButton ID="btnRemove" runat="server" CommandName="REMOVE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RelationID") %>'>移除</asp:LinkButton></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="bgLightGray">
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Role") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "ResNo") %>
									</td>
									<td align="right">
										<%# DataBinder.Eval(Container.DataItem, "Priority") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "StatusFlags") %>
									</td>
									<td align="center">
										<a href='relation.aspx?RelationID=<%# DataBinder.Eval(Container.DataItem, "RelationID") %>'>
											修改</a>/<asp:LinkButton ID="btnRemove" runat="server" CommandName="REMOVE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RelationID") %>'>移除</asp:LinkButton></td>
								</tr>
							</AlternatingItemTemplate>
							<FooterTemplate>
								</table>
							</FooterTemplate>
						</asp:Repeater>
					</td>
				</tr>
				<tr>
					<td align="right">
						<input name="Button" type="button" class="buttonWidth" value="添 加" onclick="window.location.href='relation.aspx?ReturnUrl=' + window.location.href + '&PermissionNo=<%=permissionNo%>'"></td>
				</tr>
				<tr>
					<td>
						操作说明：<br>
						<br>
						添加 - 添加此权限到角色中<br>
						修改 - 修改此关系的一些信息，比如权限范围、优先级等<br>
						移除 - 去除拥有此权限的角色</td>
				</tr>
				<tr>
					<td>
						&nbsp;</td>
				</tr>
			</tbody>
		</table>
	</form>
</body>
</html>
