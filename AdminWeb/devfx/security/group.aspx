<%@ Page Language="C#" AutoEventWireup="true" Codebehind="group.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.GroupPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>用户组管理</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8" id="Table1">
			<tbody>
				<tr>
					<td>
						以下是您可操作组的列表：<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th>
											组编号</th>
										<th>
											组名称</th>
										<th>
											组描述</th>
										<th>
											层次</th>
										<th>
											上级组编号</th>
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
									<td nowrap>
										<%# new String('　', (int)DataBinder.Eval(Container.DataItem, "LayerIndex")) %>
										<%# DataBinder.Eval(Container.DataItem, "GroupNo")%>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Title") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Description") %>
									</td>
									<td align="right">
										<%# DataBinder.Eval(Container.DataItem, "LayerIndex") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "ParentNo") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsAvailable") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsInnerGroup") %>
									</td>
									<td align="center">
										<a href='groupEdit.aspx?ReturnUrl=group.aspx&GroupNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>
											修改</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>删除</asp:LinkButton>/<a
												href='groupEdit.aspx?ParentNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>创建</a>/<a
													href='groupUser.aspx?GroupNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>用户</a></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="bgLightGray">
									<td nowrap>
										<%# new String('　', (int)DataBinder.Eval(Container.DataItem, "LayerIndex")) %>
										<%# DataBinder.Eval(Container.DataItem, "GroupNo")%>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Title") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "Description") %>
									</td>
									<td align="right">
										<%# DataBinder.Eval(Container.DataItem, "LayerIndex") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "ParentNo") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsAvailable") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsInnerGroup") %>
									</td>
									<td align="center">
										<a href='groupEdit.aspx?ReturnUrl=group.aspx&GroupNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>
											修改</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>删除</asp:LinkButton>/<a
												href='groupEdit.aspx?ParentNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>创建</a>/<a
													href='groupUser.aspx?GroupNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>用户</a></td>
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
						<input name="Button" type="button" class="buttonWidth" value="新 建" onclick="window.location.href='groupEdit.aspx?ReturnUrl=group.aspx'"></td>
				</tr>
				<tr>
					<td>
						操作说明：<br>
						<br>
						新建 - 建立新组<br>
						修改 - 修改此组的一些信息，比如名称、描述等<br>
						删除 - 从系统中删除此组<br>
						创建 - 在此组建立下级组（树型结构）<br>
						用户 - 列出属于此组的所有用户</td>
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
