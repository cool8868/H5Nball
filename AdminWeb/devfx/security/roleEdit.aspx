<%@ Page Language="C#" AutoEventWireup="true" Codebehind="roleEdit.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.RoleEditPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>��ɫ�޸�</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tr>
				<td>
					�޸�/�½���ɫ��<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
						<tr>
							<th width="25%" class="bgColor2">
								��ɫ���</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtRoleNo" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvRoleNo" runat="server" ErrorMessage="��ɫ��ű���" ControlToValidate="txtRoleNo">*</asp:RequiredFieldValidator></td>
							<td width="40%" class="bgWhite">
								��֤ϵͳΨһ���벻Ҫʹ������</td>
						</tr>
						<tr>
							<th class="bgColor2">
								��ɫ����</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtTitle" runat="server" CssClass="inputWidth" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvTitle" runat="server" ErrorMessage="��ɫ���Ʊ���" ControlToValidate="txtTitle">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite">
								����д�����������</td>
						</tr>
						<tr>
							<th class="bgColor2">
								��ɫ����</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtDescription" runat="server" CssClass="inputWidth" MaxLength="120"></asp:TextBox></td>
							<td class="bgWhite">
								����ϸ������</td>
						</tr>
						<tr>
							<th class="bgColor2">
								�Ƿ���Ч</th>
							<td class="bgWhite">
								<asp:DropDownList ID="ddlIsAvailable" runat="server">
									<asp:ListItem Value="True">��</asp:ListItem>
									<asp:ListItem Value="False">��</asp:ListItem>
								</asp:DropDownList></td>
							<td class="bgWhite">
								&nbsp;</td>
						</tr>
						<tr>
							<th class="bgColor2">
								�Ƿ�����</th>
							<td class="bgWhite">
								<asp:DropDownList ID="ddlIsInnerRole" runat="server" Enabled="False">
									<asp:ListItem Value="True">��</asp:ListItem>
									<asp:ListItem Value="False" Selected="True">��</asp:ListItem>
								</asp:DropDownList></td>
							<td class="bgWhite">
								ϵͳ���ý�ɫ����ζ�Ų��ɱ��κ���ɾ��</td>
						</tr>
						<tr>
							<th class="bgColor2">
								ѡ ��</th>
							<td class="bgWhite">
								<asp:CheckBox ID="cbxAddMeToRole" runat="server" Text="������ӵ��˽�ɫ��" Checked="True"></asp:CheckBox></td>
							<td class="bgWhite">
								����½���ɫ��Ч</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsRole" runat="server" ShowMessageBox="True" ShowSummary="False">
					</asp:ValidationSummary>
				</td>
			</tr>
			<tr>
				<td align="center">
					<asp:Button ID="btnSubmit" runat="server" Text="�� ��" CssClass="buttonWidth" OnClick="btnSubmit_Click">
					</asp:Button>&nbsp;<input name="Button" type="button" class="buttonWidth" value="�� ��"
						onclick="<%=ReturnUrl%>"></td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
