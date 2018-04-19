<%@ Page Language="C#" AutoEventWireup="true" Codebehind="permission.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.PermissionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Ȩ�޹���</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tbody>
				<tr>
					<td>
						���������ɲ�����Ȩ���б�<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th nowrap>
											Ȩ�ޱ��</th>
										<th>
											Ȩ������</th>
										<th>
											Ȩ������</th>
										<th>
											Ȩ�޷�Χ����</th>
										<th>
											���</th>
										<th>
											�ϼ�Ȩ�ޱ��</th>
										<th>
											��ʾ˳��</th>
										<th>
											�Ƿ���Ч</th>
										<th nowrap>
											����</th>
									</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr class='<%# (bool)DataBinder.Eval(Container.DataItem, "IsView") ? "bgLightBlue" : "bgWhite" %>'>
									<td nowrap>
										<%# new String('��', (int)DataBinder.Eval(Container.DataItem, "LayerIndex")) %>
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
											�޸�</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>ɾ��</asp:LinkButton>/<a
												href='permissionEdit.aspx?ParentNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>����</a>/<a
													href='permissionRole.aspx?PermissionNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>��ɫ</a></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr class='<%# (bool)DataBinder.Eval(Container.DataItem, "IsView") ? "bgLightBlue" : "bgLightGray" %>'>
									<td nowrap>
										<%# new String('��', (int)DataBinder.Eval(Container.DataItem, "LayerIndex")) %>
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
											�޸�</a>/<asp:LinkButton ID="btnDelete" runat="server" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>ɾ��</asp:LinkButton>/<a
												href='permissionEdit.aspx?ParentNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>����</a>/<a
													href='permissionRole.aspx?PermissionNo=<%# DataBinder.Eval(Container.DataItem, "PermissionNo") %>'>��ɫ</a></td>
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
						<input name="Button" type="button" class="buttonWidth" value="�� ��" onclick="window.location.href='permissionEdit.aspx?ReturnUrl=permission.aspx'"></td>
				</tr>
				<tr>
					<td>
						����˵����<br>
						<br>
						�½� - �����µ�Ȩ�ޣ��˲���һ����ϵͳ��������ִ�У�Ҳ����ֱ���ֶ����<br>
						�޸� - �޸Ĵ�Ȩ�޵�һЩ��Ϣ������Ȩ�����ơ�������<br>
						ɾ�� - ��ϵͳ��ɾ����Ȩ�ޣ��Ƚ�Σ�յĲ�������ʵ��Ӧ���д˲���Ӧ��Ϊ���������Ȩ�޹���Ա��<br>
						���� - �ڴ�Ȩ�޽����¼�Ȩ�ޣ����ͽṹ��<br>
						��ɫ - �г�ӵ�д�Ȩ�޵����н�ɫ</td>
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
