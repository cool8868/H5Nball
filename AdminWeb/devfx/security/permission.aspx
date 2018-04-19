<%@ Page Language="C#" AutoEventWireup="true" Codebehind="permission.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.PermissionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>权限管理</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tbody>
				<tr>
					<td>
						以下是您可操作的权限列表：<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th nowrap>
											权限编号</th>
										<th>
											权限名称</th>
										<th>
											权限描述</th>
										<th>
											权限范围类型</th>
										<th>
											层次</th>
										<th>
											上级权限编号</th>
										<th>
											显示顺序</th>
										<th>
											是否有效</th>
										<th nowrap>
											操作</th>
									</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr class='<%# (bool)DataBinder.Eval(Container.DataItem, "IsView") ? "bgLightBlue" : "bgWhite" %>'>
									<td nowrap>
										<%# new String('　', (int)DataBinder.Eval(Container.DataItem, "LayerIndex")) %>
										<%# DataBinder.Eval(Container.DataItem, "PermissionNo")%>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Title") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Description") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "ResType") %>
									</td>
									<td align="right">
										<%# DataBinder.Eval(Container.DataItem, "LayerIndex") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "ParentNo") %>
									</td>
									<td align="right">
										<%# DataBinder.Eval(Container.DataItem, "DispIndex") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsAvailable") %>
									</td>
									<td align="center" nowrap>
										<a href='permissionEdit.aspx?ReturnUrl=permission.aspx&PermissionNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>
											修改</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>删除</asp:LinkButton>/<a
												href='permissionEdit.aspx?ParentNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>创建</a>/<a
													href='permissionRole.aspx?PermissionNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>角色</a></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class='<%# (bool)DataBinder.Eval(Container.DataItem, "IsView") ? "bgLightBlue" : "bgLightGray" %>'>
									<td nowrap>
										<%# new String('　', (int)DataBinder.Eval(Container.DataItem, "LayerIndex")) %>
										<%# DataBinder.Eval(Container.DataItem, "PermissionNo")%>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Title") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Description") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "ResType") %>
									</td>
									<td align="right">
										<%# DataBinder.Eval(Container.DataItem, "LayerIndex") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "ParentNo") %>
									</td>
									<td align="right">
										<%# DataBinder.Eval(Container.DataItem, "DispIndex") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsAvailable") %>
									</td>
									<td align="center" nowrap>
										<a href='permissionEdit.aspx?ReturnUrl=permission.aspx&PermissionNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>
											修改</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>删除</asp:LinkButton>/<a
												href='permissionEdit.aspx?ParentNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>创建</a>/<a
													href='permissionRole.aspx?PermissionNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>角色</a></td>
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
						<input name="Button" type="button" class="buttonWidth" value="新 建" onclick="window.location.href='permissionEdit.aspx?ReturnUrl=permission.aspx'"></td>
				</tr>
				<tr>
					<td>
						操作说明：<br>
						<br>
						新建 - 建立新的权限，此操作一般在系统建立初期执行，也可以直接手动添加<br>
						修改 - 修改此权限的一些信息，比如权限名称、描述等<br>
						删除 - 从系统中删除此权限（比较危险的操作，在实际应用中此操作应设为仅限于最高权限管理员）<br>
						创建 - 在此权限建立下级权限（树型结构）<br>
						角色 - 列出拥有此权限的所有角色</td>
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
