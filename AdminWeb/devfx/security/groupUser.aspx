<%@ Page Language="C#" AutoEventWireup="true" Codebehind="groupUser.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.GroupUserPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>�����û�֮��Ĺ�ϵ</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table cellspacing="0" cellpadding="8" width="100%" border="0">
			<tr>
				<td>
					<b>�����û�֮��Ĺ�ϵ��<asp:Label ID="lblMessage" runat="server" ForeColor="Red" EnableViewState="False"></asp:Label></b></td>
			</tr>
			<tr>
				<td>
					<table class="bgDark" cellspacing="1" cellpadding="4" width="100%" border="0">
						<tr class="bgColor2">
							<th>
								��</th>
							<th>
								�����µ��û�</th>
							<th>
								����</th>
							<th>
								�����û�</th>
						</tr>
						<tr class="bgWhite">
							<td valign="top" align="center">
								<asp:DropDownList ID="ddlGroup" runat="server" CssClass="inputWidth" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
								</asp:DropDownList></td>
							<td align="center">
								<asp:ListBox ID="lbxGroupUser" runat="server" CssClass="inputWidth" Rows="10" SelectionMode="Multiple">
								</asp:ListBox></td>
							<td valign="top" align="center">
								<asp:LinkButton ID="btnRemoveUserFromGroup" runat="server" OnClick="btnRemoveUserFromGroup_Click">--></asp:LinkButton><br>
								<br>
								<asp:LinkButton ID="btnAddUserToGroup" runat="server" OnClick="btnAddUserToGroup_Click"><--</asp:LinkButton></td>
							<td align="center">
								<asp:ListBox ID="lbxUser" runat="server" CssClass="inputWidth" Rows="10" SelectionMode="Multiple">
								</asp:ListBox></td>
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					����˵����<br>
					<br>
					��&lt;--�� - ����û�������<br>
					��--&gt;�� - ���û��������Ƴ�</td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
