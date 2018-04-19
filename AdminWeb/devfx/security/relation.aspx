<%@ Page Language="C#" AutoEventWireup="true" Codebehind="relation.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.RelationPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>��Ȩ</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tr>
				<td>
					�޸�/�½���ɫȨ��֮��Ĺ�ϵ��<asp:Label ID="lblMessage" runat="server" EnableViewState="False"
						ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
						<tr class="bgWhite">
							<th width="25%" class="bgColor2">
								��ɫ</th>
							<td>
								<asp:DropDownList ID="ddlRoleNo" runat="server" CssClass="inputWidth">
								</asp:DropDownList><asp:RequiredFieldValidator ID="rfvRoleNo" runat="server" ErrorMessage="��ɫ����"
									ControlToValidate="ddlRoleNo">*</asp:RequiredFieldValidator></td>
							<td width="40%">
								&nbsp;</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								Ȩ��</th>
							<td>
								<asp:DropDownList ID="ddlPermissionNo" runat="server" CssClass="inputWidth" AutoPostBack="True" OnSelectedIndexChanged="ddlPermissionNo_SelectedIndexChanged">
								</asp:DropDownList><asp:RequiredFieldValidator ID="rfvPermissionNo" runat="server"
									ErrorMessage="Ȩ�ޱ���" ControlToValidate="ddlPermissionNo">*</asp:RequiredFieldValidator></td>
							<td>
								&nbsp;</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								Ȩ�޷�Χ</th>
							<td>
								<asp:DropDownList ID="ddlResNo" runat="server" CssClass="inputWidth">
								</asp:DropDownList></td>
							<td>
								&nbsp;</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								���ȼ�</th>
							<td>
								<asp:TextBox ID="txtPriority" runat="server" CssClass="inputWidth">0</asp:TextBox><asp:RequiredFieldValidator
									ID="rfvPriority" runat="server" ErrorMessage="���ȼ�����" ControlToValidate="txtPriority">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
										ID="revPriority" runat="server" ErrorMessage="���ȼ�����Ϊ����" ControlToValidate="txtPriority"
										ValidationExpression="-?(\d)*">*</asp:RegularExpressionValidator></td>
							<td>
								��ֵԽ�ߣ����ȼ�Խ��</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								Ȩ������</th>
							<td>
								<asp:CheckBox ID="cbxExecute" runat="server" Text="Execute"></asp:CheckBox><br>
								<asp:CheckBox ID="cbxList" runat="server" Text="List"></asp:CheckBox><br>
								<asp:CheckBox ID="cbxGrant" runat="server" Text="Grant"></asp:CheckBox></td>
							<td>
								Ȩ������˵����<br>
								<br>
								Grant&nbsp;&nbsp; - ���԰Ѵ�Ȩ����Ȩ����Ľ�ɫ<br>
								List&nbsp;&nbsp;&nbsp; - ��Ȩ�޿ɷ��г�<br>
								Execute&nbsp;- ��Ȩ���Ƿ��ܱ�ִ�У����Ҫ�ܾ�Ȩ�ޣ�����Ϊ����ִ�У�</td>
						</tr>
						<tr class="bgWhite">
							<th class="bgColor2">
								�� ע</th>
							<td>
								<asp:TextBox ID="txtRemark" runat="server" CssClass="inputWidth"></asp:TextBox></td>
							<td>
								&nbsp;</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsRelation" runat="server" ShowSummary="False" ShowMessageBox="True">
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
