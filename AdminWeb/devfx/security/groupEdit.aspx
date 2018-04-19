<%@ Page Language="C#" AutoEventWireup="true" Codebehind="groupEdit.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.GroupEditPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>�û����޸�</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table cellspacing="0" cellpadding="8" width="100%" border="0">
			<tr>
				<td>
					�޸�/�½��飺<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table class="bgDark" cellspacing="1" cellpadding="4" width="100%" border="0">
						<tr>
							<th class="bgColor2">
								�ϼ������</th>
							<td class="bgWhite">
								<asp:HyperLink ID="hlkParentNo" runat="server"></asp:HyperLink></td>
							<td class="bgWhite">
								���ͽṹ�б����ϼ���ı��</td>
						</tr>
						<tr>
							<th class="bgColor2" width="25%">
								����</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtGroupNo" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvGroupNo" runat="server" ErrorMessage="���ű���" ControlToValidate="txtGroupNo">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite" width="40%">
								�뱣֤ϵͳΨһ���벻Ҫʹ������</td>
						</tr>
						<tr>
							<th class="bgColor2">
								������</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtTitle" runat="server" CssClass="inputWidth" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvTitle" runat="server" ErrorMessage="�����Ʊ���" ControlToValidate="txtTitle">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite">
								�����������������</td>
						</tr>
						<tr>
							<th class="bgColor2">
								������</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtDescription" runat="server" CssClass="inputWidth" MaxLength="120"></asp:TextBox></td>
							<td class="bgWhite">
								�����ϸ����</td>
						</tr>
						<tr>
							<th class="bgColor2">
								�㼶</th>
							<td class="bgWhite">
								<asp:Label ID="lblLayerIndex" runat="server"></asp:Label></td>
							<td class="bgWhite">
								���ͽṹ�б��������Ĳ㼶��Լ�����Ĳ㼶Ϊ0��</td>
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
								�����Ƿ���Ч</td>
						</tr>
						<tr>
							<th class="bgColor2">
								�Ƿ�����</th>
							<td class="bgWhite">
								<asp:DropDownList ID="ddlIsInnerGroup" runat="server" Enabled="False">
									<asp:ListItem Value="True">��</asp:ListItem>
									<asp:ListItem Value="False" Selected="True">��</asp:ListItem>
								</asp:DropDownList></td>
							<td class="bgWhite">
								ϵͳ�����飬��ζ�Ų��ɱ��κ���ɾ��</td>
						</tr>
						<tr>
							<th class="bgColor2">
								ѡ ��</th>
							<td class="bgWhite">
								<asp:CheckBox ID="cbxAddMeToGroup" runat="server" Text="������ӵ�������" Checked="True"></asp:CheckBox></td>
							<td class="bgWhite">
								����½�����Ч</td>
						</tr>
						<tr>
							<th class="bgColor2">
								��ɫ</th>
							<td colspan="2" class="bgWhite">
								<table width="100%" border="0" cellspacing="0" cellpadding="4">
									<tr>
										<td width="10%">
											���������Ľ�ɫ</td>
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
					<asp:ValidationSummary ID="vsGroup" runat="server" ShowMessageBox="True" ShowSummary="False">
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
			<tr>
				<td>
					&nbsp;</td>
			</tr>
		</table>
	</form>
</body>
</html>
