<%@ Page Language="C#" AutoEventWireup="true" Codebehind="permissionEdit.aspx.cs"
	Inherits="HTB.DevFx.Security.Web.Pages.Security.PermissionEditPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Ȩ���޸�</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table width="100%" border="0" cellspacing="0" cellpadding="8">
			<tr>
				<td>
					�޸�/�½�Ȩ�ޣ�<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table width="100%" border="0" cellpadding="4" cellspacing="1" class="bgDark">
						<tr>
							<th class="bgColor2">
								�ϼ�Ȩ�ޱ��</th>
							<td class="bgWhite">
								<asp:HyperLink ID="hlkParentNo" runat="server"></asp:HyperLink></td>
							<td class="bgWhite">
								���ͽṹ�б�Ȩ���ϼ�Ȩ�޵ı��</td>
						</tr>
						<tr>
							<th width="25%" class="bgColor2">
								Ȩ�ޱ��</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtPermissionNo" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvPermissioNo" runat="server" ErrorMessage="Ȩ�ޱ�ű���" ControlToValidate="txtPermissionNo">*</asp:RequiredFieldValidator></td>
							<td width="40%" class="bgWhite">
								�뱣֤ϵͳΨһ���벻Ҫʹ������</td>
						</tr>
						<tr>
							<th class="bgColor2">
								Ȩ������</th>
							<td class="bgWhite">
								<font color="#ff0000">
									<asp:TextBox ID="txtTitle" runat="server" CssClass="inputWidth" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator
										ID="rfvTitle" runat="server" ErrorMessage="Ȩ�����Ʊ���" ControlToValidate="txtTitle">*</asp:RequiredFieldValidator></font></td>
							<td class="bgWhite">
								�����������������</td>
						</tr>
						<tr>
							<th class="bgColor2">
								Ȩ������</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtDescription" runat="server" CssClass="inputWidth" MaxLength="120"></asp:TextBox></td>
							<td class="bgWhite">
								Ȩ�޵���ϸ����</td>
						</tr>
						<tr>
							<th class="bgColor2">
								Ȩ�޷�Χ����</th>
							<td class="bgWhite">
								<asp:DropDownList ID="ddlResType" runat="server" CssClass="inputWidth">
								</asp:DropDownList></td>
							<td class="bgWhite">
								����Ȩ�޶�Ӧ��Χ�����ͣ����緢��Ȩ�޶�Ӧ�ķ�Χ�����ǰ����</td>
						</tr>
						<tr>
							<th class="bgColor2">
								��Դ1</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtRes1" runat="server" CssClass="inputWidth" MaxLength="255"></asp:TextBox></td>
							<td class="bgWhite">
								Ȩ����Դ1������˵���Ӧ��URL��</td>
						</tr>
						<tr>
							<th class="bgColor2">
								��Դ2</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtRes2" runat="server" CssClass="inputWidth" MaxLength="255"></asp:TextBox></td>
							<td class="bgWhite">
								Ȩ����Դ2������˵���Ӧ��ͼƬURL��</td>
						</tr>
						<tr>
							<th class="bgColor2">
								��ʾ˳��</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtDispIndex" runat="server" CssClass="inputWidth">0</asp:TextBox><asp:RequiredFieldValidator
									ID="rfvDispIndex" runat="server" ErrorMessage="��ʾ˳�����" ControlToValidate="txtDispIndex">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
										ID="revDispIndex" runat="server" ErrorMessage="��ʾ˳�����Ϊ����" ControlToValidate="txtDispIndex"
										ValidationExpression="-?(\d)*">*</asp:RegularExpressionValidator></td>
							<td class="bgWhite">
								��Բ˵��ɵ�����ʾ˳��</td>
						</tr>
						<tr>
							<th class="bgColor2">
								�㼶</th>
							<td class="bgWhite">
								<asp:Label ID="lblLayerIndex" runat="server"></asp:Label></td>
							<td class="bgWhite">
								���ͽṹ�б�Ȩ�������Ĳ㼶��Լ�����Ĳ㼶Ϊ0��</td>
						</tr>
						<tr>
							<th class="bgColor2">
								�Ƿ�ɼ�</th>
							<td class="bgWhite">
								<font color="#ff0000">
									<asp:DropDownList ID="ddlIsView" runat="server" CssClass="inputWidth">
										<asp:ListItem Value="True">��</asp:ListItem>
										<asp:ListItem Value="False" Selected="True">��</asp:ListItem>
									</asp:DropDownList></font></td>
							<td class="bgWhite">
								�Ƿ�Ϊ����ʾ��Ȩ�ޣ�һ��˵�������Ϊ���ǡ�</td>
						</tr>
						<tr>
							<th class="bgColor2">
								�Ƿ���Ч</th>
							<td class="bgWhite">
								<font color="#ff0000">
									<asp:DropDownList ID="ddlIsAvailable" runat="server" CssClass="inputWidth">
										<asp:ListItem Value="True">��</asp:ListItem>
										<asp:ListItem Value="False">��</asp:ListItem>
									</asp:DropDownList></font></td>
							<td class="bgWhite">
								��Ȩ���Ƿ���Ч</td>
						</tr>
					</table>
					<asp:ValidationSummary ID="vsPermission" runat="server" ShowMessageBox="True" ShowSummary="False">
					</asp:ValidationSummary>
				</td>
			</tr>
			<tr>
				<td align="center">
					<asp:Button ID="btnSubmit" runat="server" CssClass="buttonWidth" Text="�� ��" OnClick="btnSubmit_Click">
					</asp:Button>&nbsp;<input class="buttonWidth" type="button" value="�� ��" onclick="<%=ReturnUrl%>"></td>
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
