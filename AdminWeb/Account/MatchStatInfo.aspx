<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="MatchStatInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.MatchStatInfo" %>

<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="bgDark" cellspacing="1" cellpadding="1" border="0" style="width: 100%;">
        <tr style="height: 25px">
            <td class="bgColor2" style="width: 118px">概述：</td>
            <td class="bgWhite">
                <asp:Label ID="lblHint" runat="server"></asp:Label></td>
        </tr>
    </table>
    <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
        <HeaderStyle CssClass="bgColor2"></HeaderStyle>
        <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF" ></ItemStyle>
        <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE" ></AlternatingItemStyle>
        <Columns>
            <asp:BoundColumn HeaderText="序号" DataField="Idx" />
            <asp:BoundColumn HeaderText="经理id" DataField="ManagerId" />
            <asp:BoundColumn HeaderText="比赛类型" DataField="MatchTypeV" />
            <asp:BoundColumn HeaderText="总场次" DataField="TotalCount" />
            <asp:BoundColumn HeaderText="胜场" DataField="Win" />
            <asp:BoundColumn HeaderText="败场" DataField="Lose" />
            <asp:BoundColumn HeaderText="平局" DataField="Draw" />
            <asp:BoundColumn HeaderText="胜率" DataField="WinRateV" />
            <asp:BoundColumn HeaderText="更新时间" DataField="UpdateTime" />
        </Columns>
    </asp:DataGrid>

</asp:Content>

