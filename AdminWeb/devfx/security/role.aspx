<%@ Page Language="C#" AutoEventWireup="true" Codebehind="role.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.RolePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>角色管理</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table cellspacing="0" cellpadding="8" width="100%" border="0">
			<tbody>
				<tr>
					<td>
						以下是您可操作的角色列表：<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th>
											角色编号</th>
										<th>
											角色名称</th>
										<th>
											角色描述</th>
										<th>
											是否有效</th>
										<th>
											是否内置</th>
										<th>
											操作</th>
									</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr class="bgWhite">
									<td>
										<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Title") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Description") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsAvailable") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsInnerRole") %>
									</td>
									<td align="center">
										<a href='roleEdit.aspx?ReturnUrl=role.aspx&RoleNo=<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>
											修改</a>/<asp:LinkButton runat="server" ID="btnDelete" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>删除</asp:LinkButton>/<asp:LinkButton
												ID="LinkButton1" runat="server" CommandName="REMOVE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>移除</asp:LinkButton>/<a
													href='rolePermission.aspx?roleNo=<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>权限</a></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="bgLightGray">
									<td>
										<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Title") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Description") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsAvailable") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsInnerRole") %>
									</td>
									<td align="center">
										<a href='roleEdit.aspx?ReturnUrl=role.aspx&RoleNo=<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>
											修改</a>/<asp:LinkButton runat="server" ID="btnDelete" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>删除</asp:LinkButton>/<asp:LinkButton
												ID="LinkButton2" runat="server" CommandName="REMOVE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>移除</asp:LinkButton>/<a
													href='rolePermission.aspx?roleNo=<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>权限</a></td>
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
						<input class="buttonWidth" onclick="window.location.href='roleEdit.aspx?ReturnUrl=role.aspx'" type="button"
							value="新 建" name="Button"></td>
				</tr>
				<tr>
					<td>
						操作说明：<br>
						<br>
						新建 - 建立新的角色
						<br>
						修改 - 修改此角色的名称、描述等信息<br>
						删除 - 从系统中删除此角色<br>
						移除 - 把当前用户移出此角色<br>
						权限 - 查看此角色下拥有的权限</td>
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
