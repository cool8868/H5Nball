<%@ Control Language="C#" AutoEventWireup="true" Codebehind="Pager.ascx.cs" Inherits="HTB.DevFx.Security.Web.Pages.PagerControl" %>
<span id="spanPager" runat="server" visible="false">
	<asp:LinkButton ID="btnFirst" runat="server" CommandName="Page" CommandArgument="First"
		OnClick="btnFirst_Click">最前页</asp:LinkButton>
	<asp:LinkButton ID="btnPrev" runat="server" CommandName="Page" CommandArgument="Prev"
		OnClick="btnPrev_Click">上一页</asp:LinkButton>
	<asp:LinkButton ID="btnNext" runat="server" CommandName="Page" CommandArgument="Next"
		OnClick="btnNext_Click">下一页</asp:LinkButton>
	<asp:LinkButton ID="btnLast" runat="server" CommandName="Page" CommandArgument="Last"
		OnClick="btnLast_Click">最后页</asp:LinkButton>
	<asp:Label ID="lblPage" runat="server"></asp:Label>
	转到第
	<asp:DropDownList ID="ddlPages" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPages_SelectedIndexChanged">
	</asp:DropDownList>
	页</span>