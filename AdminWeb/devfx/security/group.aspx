<%@ Page Language="C#" AutoEventWireup="true" Codebehind="group.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.GroupPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>�û������</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8" id="Table1">
			<tbody>
				<tr>
					<td>
						���������ɲ�������б�<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th>
											����</th>
										<th>
											������</th>
										<th>
											������</th>
										<th>
											���</th>
										<th>
											�ϼ�����</th>
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
									<td nowrap>
										<%# new String('��', (int)DataBinder.Eval(Container.DataItem, "LayerIndex")) %>
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
											�޸�</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>ɾ��</asp:LinkButton>/<a
												href='groupEdit.aspx?ParentNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>����</a>/<a
													href='groupUser.aspx?GroupNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>�û�</a></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class="bgLightGray">
									<td nowrap>
										<%# new String('��', (int)DataBinder.Eval(Container.DataItem, "LayerIndex")) %>
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
											�޸�</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>ɾ��</asp:LinkButton>/<a
												href='groupEdit.aspx?ParentNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>����</a>/<a
													href='groupUser.aspx?GroupNo=<%# DataBinder.Eval(Container.DataItem, "GroupNo") %>'>�û�</a></td>
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
						<input name="Button" type="button" class="buttonWidth" value="�� ��" onclick="window.location.href='groupEdit.aspx?ReturnUrl=group.aspx'"></td>
				</tr>
				<tr>
					<td>
						����˵����<br>
						<br>
						�½� - ��������<br>
						�޸� - �޸Ĵ����һЩ��Ϣ���������ơ�������<br>
						ɾ�� - ��ϵͳ��ɾ������<br>
						���� - �ڴ��齨���¼��飨���ͽṹ��<br>
						�û� - �г����ڴ���������û�</td>
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
