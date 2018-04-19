<%@ Page Language="C#" AutoEventWireup="true" Codebehind="role.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.RolePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>��ɫ����</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table cellspacing="0" cellpadding="8" width="100%" border="0">
			<tbody>
				<tr>
					<td>
						���������ɲ����Ľ�ɫ�б�<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th>
											��ɫ���</th>
										<th>
											��ɫ����</th>
										<th>
											��ɫ����</th>
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
											�޸�</a>/<asp:LinkButton runat="server" ID="btnDelete" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>ɾ��</asp:LinkButton>/<asp:LinkButton
												ID="LinkButton1" runat="server" CommandName="REMOVE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>�Ƴ�</asp:LinkButton>/<a
													href='rolePermission.aspx?roleNo=<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>Ȩ��</a></td>
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
											�޸�</a>/<asp:LinkButton runat="server" ID="btnDelete" CommandName="DELETE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>ɾ��</asp:LinkButton>/<asp:LinkButton
												ID="LinkButton2" runat="server" CommandName="REMOVE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>�Ƴ�</asp:LinkButton>/<a
													href='rolePermission.aspx?roleNo=<%# DataBinder.Eval(Container.DataItem, "RoleNo") %>'>Ȩ��</a></td>
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
							value="�� ��" name="Button"></td>
				</tr>
				<tr>
					<td>
						����˵����<br>
						<br>
						�½� - �����µĽ�ɫ
						<br>
						�޸� - �޸Ĵ˽�ɫ�����ơ���������Ϣ<br>
						ɾ�� - ��ϵͳ��ɾ���˽�ɫ<br>
						�Ƴ� - �ѵ�ǰ�û��Ƴ��˽�ɫ<br>
						Ȩ�� - �鿴�˽�ɫ��ӵ�е�Ȩ��</td>
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
