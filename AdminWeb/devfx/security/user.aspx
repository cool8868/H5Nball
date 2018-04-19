<%@ Page Language="C#" AutoEventWireup="true" Codebehind="user.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.UserPage" %>

<%@ Register TagPrefix="uc1" TagName="Pager" Src="../uctrls/Pager.ascx" %>
<%@ Reference Control="../uctrls/Pager.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>�û�����</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tbody>
				<tr>
					<td>
						�����������Բ������û��б�<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th>
											�û����</th>
										<th>
											�û���</th>
										<th>
											�Ƿ���Ч</th>
										<th>
											�Ƿ�����</th>
										<th>
											����</th>
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
											�޸�</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserNo") %>'>ɾ��</asp:LinkButton></td>
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
											�޸�</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserNo") %>'>ɾ��</asp:LinkButton></td>
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
						<input name="Button" type="button" class="buttonWidth" value="�� ��" onclick="window.location.href='userEdit.aspx?ReturnUrl=user.aspx'"></td>
				</tr>
				<tr>
					<td>
						����˵����<br>
						<br>
						�½� - �����µ��û�<br>
						�޸� - �޸Ĵ��û�����Ϣ�������û����������<br>
						ɾ�� - ��ϵͳ��ɾ�����û�</td>
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
