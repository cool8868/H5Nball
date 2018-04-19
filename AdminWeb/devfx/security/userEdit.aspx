<%@ Page Language="C#" AutoEventWireup="true" Codebehind="userEdit.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.UserEditPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>�û��޸�</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tr>
				<td>
					�޸�/�½��û���<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
						<tr>
							<th width="25%" class="bgColor2">
								�û����</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtUserNo" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvUserNo" runat="server" ErrorMessage="�û���ű���" ControlToValidate="txtUserNo">*</asp:RequiredFieldValidator></td>
							<td width="40%" class="bgWhite">
								��֤ϵͳΨһ���벻Ҫʹ������</td>
						</tr>
						<tr>
							<th class="bgColor2">
								�û���</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtUserName" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvUserName" runat="server" ErrorMessage="�û�������" ControlToValidate="txtUserName">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite">
								��֤ϵͳΨһ�����Ӧ��ϵͳ�е��룩</td>
						</tr>
						<tr>
							<th class="bgColor2">
								�û�����</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtPassword" runat="server" CssClass="inputWidth" TextMode="Password"
									MaxLength="50"></asp:TextBox></td>
							<td class="bgWhite">
								���޸����������������룩</td>
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
								<asp:DropDownList ID="ddlIsInnerUser" runat="server" Enabled="False">
									<asp:ListItem Value="True">��</asp:ListItem>
									<asp:ListItem Value="False" Selected="True">��</asp:ListItem>
								</asp:DropDownList></td>
							<td class="bgWhite">
								ϵͳ�����û�����ζ�Ų��ɱ��κ���ɾ��</td>
						</tr>
						<tr>
							<th class="bgColor2">
								��</th>
							<td colspan="2" class="bgWhite">
								<table width="100%" border="0" cellspacing="0" cellpadding="4">
									<tr>
										<td width="10%">
											���û���������</td>
										<td width="1%">
											&nbsp;</td>
										<td>
											��Ч����</td>
									</tr>
									<tr>
										<td>
											<asp:ListBox ID="lbxGroups" runat="server" CssClass="inputWidth" Rows="6" SelectionMode="Multiple">
											</asp:ListBox></td>
										<td align="center" nowrap>
											<asp:LinkButton ID="btnRemoveGroups" runat="server" CausesValidation="False" OnClick="btnRemoveGroups_Click">--></asp:LinkButton><br>
											<br>
											<asp:LinkButton ID="btnAddGroups" runat="server" CausesValidation="False" OnClick="btnAddGroups_Click"><--</asp:LinkButton></td>
										<td>
											<asp:ListBox ID="lbxAvailGroups" runat="server" CssClass="inputWidth" Rows="6" SelectionMode="Multiple">
											</asp:ListBox></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<th class="bgColor2">
								��ɫ</th>
							<td colspan="2" class="bgWhite">
								<table width="100%" border="0" cellspacing="0" cellpadding="4">
									<tr>
										<td width="10%">
											���û������Ľ�ɫ</td>
										<td width="1%">
											&nbsp;</td>
										<td>
											��Ч�Ľ�ɫ</td>
									</tr>
									<tr>
										<td>
											<asp:ListBox ID="lbxRoles" runat="server" CssClass="inputWidth" Rows="6" SelectionMode="Multiple">
											</asp:ListBox></td>
										<td align="center" nowrap>
											<asp:LinkButton ID="btnRemoveRoles" runat="server" CausesValidation="False" OnClick="btnRemoveRoles_Click">--></asp:LinkButton><br>
											<br>
											<asp:LinkButton ID="btnAddRoles" runat="server" CausesValidation="False" OnClick="btnAddRoles_Click"><--</asp:LinkButton></td>
										<td>
											<asp:ListBox ID="lbxAailRoles" runat="server" CssClass="inputWidth" Rows="6" SelectionMode="Multiple">
											</asp:ListBox></td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsUser" runat="server" ShowMessageBox="True" ShowSummary="False">
					</asp:ValidationSummary>
				</td>
			</tr>
			<tr>
				<td align="center">
					<asp:Button ID="btnSubmit" runat="server" CssClass="buttonWidth" Text="�� ��" OnClick="btnSubmit_Click"></asp:Button>&nbsp;<input
						class="buttonWidth" onclick="<%=ReturnUrl%>" type="button" value="�� ��" name="Button"></td>
			</tr>
			<tr>
				<td>
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
