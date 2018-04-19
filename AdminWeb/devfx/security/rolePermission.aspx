<%@ Page Language="C#" AutoEventWireup="true" Codebehind="rolePermission.aspx.cs"
	Inherits="HTB.DevFx.Security.Web.Pages.Security.RolePermissionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>角色和权限关系</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tbody>
				<tr>
					<td>
						以下是角色
						<asp:HyperLink ID="hlkRole" runat="server"></asp:HyperLink>
						&nbsp;的权限列表：<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th>
											权限名称[编号]</th>
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
										<%# DataBinder.Eval(Container.DataItem, "Permission") %>
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
										<%# DataBinder.Eval(Container.DataItem, "Permission") %>
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
						<input name="Button" type="button" class="buttonWidth" value="添 加" onclick="window.location.href='relation.aspx?ReturnUrl=' + window.location.href + '&RoleNo=<%=roleNo%>'"></td>
				</tr>
				<tr>
					<td>
						操作说明：<br>
						<br>
						修改 - 修改角色拥有此权限的一些属性，比如权限范围、优先级、权限属性等信息<br>
						移除 - 把此权限从角色中移除（并不删除权限本身）<br>
						添加 - 添加权限到此角色中</td>
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
