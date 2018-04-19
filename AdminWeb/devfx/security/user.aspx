<%@ Page Language="C#" AutoEventWireup="true" Codebehind="user.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.UserPage" %>

<%@ Register TagPrefix="uc1" TagName="Pager" Src="../uctrls/Pager.ascx" %>
<%@ Reference Control="../uctrls/Pager.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>用户管理</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tbody>
				<tr>
					<td>
						以下是您可以操作的用户列表：<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th>
											用户编号</th>
										<th>
											用户名</th>
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
										<%# DataBinder.Eval(Container.DataItem, "UserNo") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "UserName") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsAvailable") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsInnerUser") %>
									</td>
									<td align="center">
										<a href='userEdit.aspx?ReturnUrl=user.aspx&UserNo=<%# DataBinder.Eval(Container.DataItem, "UserNo") %>'>
											修改</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserNo") %>'>删除</asp:LinkButton></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="bgLightGray">
									<td>
										<%# DataBinder.Eval(Container.DataItem, "UserNo") %>
									</td>
									<td>
										<%# DataBinder.Eval(Container.DataItem, "UserName") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsAvailable") %>
									</td>
									<td align="center">
										<%# DataBinder.Eval(Container.DataItem, "IsInnerUser") %>
									</td>
									<td align="center">
										<a href='userEdit.aspx?ReturnUrl=user.aspx&UserNo=<%# DataBinder.Eval(Container.DataItem, "UserNo") %>'>
											修改</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserNo") %>'>删除</asp:LinkButton></td>
								</tr>
							</AlternatingItemTemplate>
							<FooterTemplate>
								</table>
							</FooterTemplate>
						</asp:Repeater>
						<center>
							<uc1:Pager ID="pager" runat="server" PagedControlID="repeater" PageSize="20" OnPageIndexChanged="pager_PageIndexChanged"></uc1:Pager>
						</center>
					</td>
				</tr>
				<tr>
					<td align="right">
						<input name="Button" type="button" class="buttonWidth" value="新 建" onclick="window.location.href='userEdit.aspx?ReturnUrl=user.aspx'"></td>
				</tr>
				<tr>
					<td>
						操作说明：<br>
						<br>
						新建 - 建立新的用户<br>
						修改 - 修改此用户的信息，比如用户名，密码等<br>
						删除 - 从系统中删除此用户</td>
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
