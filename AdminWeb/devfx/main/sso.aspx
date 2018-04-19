<%@ Page Language="C#" AutoEventWireup="true" Codebehind="sso.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.Main.SsoPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>登录系统</title>
</head>
<body>
	<form id="mainForm" runat="server">
		<div id="divLocal" runat="server">
			<asp:Label ID="lblReceiveMsg" runat="server" EnableViewState="False" ForeColor="Red"
				Font-Bold="True"></asp:Label>
			<ul>
				<li>
					<asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="Blue"></asp:Label>
				<li>
					<asp:LinkButton ID="btnLogin" runat="server" OnClick="btnLogin_Click">如果登录失败，请点击这里再次认证</asp:LinkButton>
				<li>
					<asp:HyperLink ID="hlkLocal" runat="server" EnableViewState="False">如果要使用本地帐户登录，请点击这里进行本地系统认证</asp:HyperLink>
				<li>
					<asp:HyperLink ID="hlkRemote" runat="server" EnableViewState="False" Target="_top">点击这里转向统一认证平台</asp:HyperLink></li>
			</ul>
		</div>
		<div id="divRemote" runat="server" visible="false">
			<ul>
				<li><asp:HyperLink ID="hlkLoginByRmote" runat="server" EnableViewState="False" ForeColor="Red">系统正在转向远程认证......</asp:HyperLink><span id="spanCount"></span>
				<li><asp:HyperLink ID="hlkLoginByLocal" runat="server" EnableViewState="False">如果要使用本地帐户登录，请点击这里进行本地系统认证</asp:HyperLink>
			</ul>
			<script language="javascript">
				var i = 3;
				function doCount() {
					if( i <= 0) {
						window.top.location.href='<%=this.RemoteUrl%>';
					} else {
						document.getElementById("spanCount").innerText = i--;
						window.setTimeout(doCount, 1000);
					}
				}
				doCount();
			</script>

		</div>
	</form>
</body>
</html>
