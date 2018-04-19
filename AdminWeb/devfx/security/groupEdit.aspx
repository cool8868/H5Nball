<%@ Page Language="C#" AutoEventWireup="true" Codebehind="groupEdit.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Security.GroupEditPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>用户组修改</title>
	<link rel="stylesheet" type="text/css" href="../resource/style/style.css" media="screen" />	
</head>
<body>
	<form id="mainForm" runat="server">
		<table cellspacing="0" cellpadding="8" width="100%" border="0">
			<tr>
				<td>
					修改/新建组：<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>
					<table class="bgDark" cellspacing="1" cellpadding="4" width="100%" border="0">
						<tr>
							<th class="bgColor2">
								上级组编码</th>
							<td class="bgWhite">
								<asp:HyperLink ID="hlkParentNo" runat="server"></asp:HyperLink></td>
							<td class="bgWhite">
								树型结构中本组上级组的编号</td>
						</tr>
						<tr>
							<th class="bgColor2" width="25%">
								组编号</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtGroupNo" runat="server" CssClass="inputWidth" MaxLength="50"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvGroupNo" runat="server" ErrorMessage="组编号必填" ControlToValidate="txtGroupNo">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite" width="40%">
								请保证系统唯一，请不要使用中文</td>
						</tr>
						<tr>
							<th class="bgColor2">
								组名称</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtTitle" runat="server" CssClass="inputWidth" MaxLength="25"></asp:TextBox><asp:RequiredFieldValidator
									ID="rfvTitle" runat="server" ErrorMessage="组名称必填" ControlToValidate="txtTitle">*</asp:RequiredFieldValidator></td>
							<td class="bgWhite">
								请输入有意义的名称</td>
						</tr>
						<tr>
							<th class="bgColor2">
								组描述</th>
							<td class="bgWhite">
								<asp:TextBox ID="txtDescription" runat="server" CssClass="inputWidth" MaxLength="120"></asp:TextBox></td>
							<td class="bgWhite">
								组的详细描述</td>
						</tr>
						<tr>
							<th class="bgColor2">
								层级</th>
							<td class="bgWhite">
								<asp:Label ID="lblLayerIndex" runat="server"></asp:Label></td>
							<td class="bgWhite">
								树型结构中本组所处的层级（约定根的层级为0）</td>
						</tr>
						<tr>
							<th class="bgColor2">
								是否有效</th>
							<td class="bgWhite">
								<asp:DropDownList ID="ddlIsAvailable" runat="server">
									<asp:ListItem Value="True">是</asp:ListItem>
									<asp:ListItem Value="False">否</asp:ListItem>
								</asp:DropDownList></td>
							<td class="bgWhite">
								本组是否有效</td>
						</tr>
						<tr>
							<th class="bgColor2">
								是否内置</th>
							<td class="bgWhite">
								<asp:DropDownList ID="ddlIsInnerGroup" runat="server" Enabled="False">
									<asp:ListItem Value="True">是</asp:ListItem>
									<asp:ListItem Value="False" Selected="True">否</asp:ListItem>
								</asp:DropDownList></td>
							<td class="bgWhite">
								系统内置组，意味着不可被任何人删除</td>
						</tr>
						<tr>
							<th class="bgColor2">
								选 项</th>
							<td class="bgWhite">
								<asp:CheckBox ID="cbxAddMeToGroup" runat="server" Text="把我添加到此组中" Checked="True"></asp:CheckBox></td>
							<td class="bgWhite">
								针对新建组有效</td>
						</tr>
						<tr>
							<th class="bgColor2">
								角色</th>
							<td colspan="2" class="bgWhite">
								<table width="100%" border="0" cellspacing="0" cellpadding="4">
									<tr>
										<td width="10%">
											此组隶属的角色</td>
										<td width="1%">
											&nbsp;</td>
										<td>
											有效的角色</td>
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
					<asp:Button ID="btnSubmit" runat="server" CssClass="buttonWidth" Text="提 交" OnClick="btnSubmit_Click"></asp:Button>&nbsp;<input
						class="buttonWidth" onclick="<%=ReturnUrl%>" type="button" value="返 回" name="Button"></td>
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
