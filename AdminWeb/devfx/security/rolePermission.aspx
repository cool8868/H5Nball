<%@ Page Language="C#" AutoEventWireup="true" Codebehind="rolePermission.aspx.cs"
	Inherits="HTB.DevFx.Security.Web.Pages.Security.RolePermissionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>��ɫ��Ȩ�޹�ϵ</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tbody>
				<tr>
					<td>
						�����ǽ�ɫ
						<asp:HyperLink ID="hlkRole" runat="server"></asp:HyperLink>
						&nbsp;��Ȩ���б�<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
				</tr>
				<tr>
					<td>
						<asp:Repeater ID="repeater" runat="server" OnItemCommand="repeater_ItemCommand" OnItemCreated="repeater_ItemCreated">
							<HeaderTemplate>
								<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
									<tr class="bgColor2">
										<th>
											Ȩ������[���]</th>
										<th>
											Ȩ�޷�Χ</th>
										<th>
											���ȼ�</th>
										<th>
											Ȩ������</th>
										<th>
											����</th>
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
											�޸�</a>/<asp:LinkButton ID="btnRemove" runat="server" CommandName="REMOVE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RelationID") %>'>�Ƴ�</asp:LinkButton></td>
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
											�޸�</a>/<asp:LinkButton ID="btnRemove" runat="server" CommandName="REMOVE" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RelationID") %>'>�Ƴ�</asp:LinkButton></td>
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
						<input name="Button" type="button" class="buttonWidth" value="�� ��" onclick="window.location.href='relation.aspx?ReturnUrl=' + window.location.href + '&RoleNo=<%=roleNo%>'"></td>
				</tr>
				<tr>
					<td>
						����˵����<br>
						<br>
						�޸� - �޸Ľ�ɫӵ�д�Ȩ�޵�һЩ���ԣ�����Ȩ�޷�Χ�����ȼ���Ȩ�����Ե���Ϣ<br>
						�Ƴ� - �Ѵ�Ȩ�޴ӽ�ɫ���Ƴ�������ɾ��Ȩ�ޱ���<br>
						��� - ���Ȩ�޵��˽�ɫ��</td>
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
