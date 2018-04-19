<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="TeammemberGrowInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.TeammemberGrowInfo" %>

<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="bgDark" cellspacing="1" cellpadding="1" border="0" style="width: 100%;">
        <tr style="height: 25px">
            <td class="bgColor2" style="width: 118px">概述：</td>
            <td colspan="5" class="bgWhite">
                <asp:Label ID="lblHint" runat="server"></asp:Label></td>
        </tr>
    </table>
    <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
        <HeaderStyle CssClass="bgColor2"></HeaderStyle>
        <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF" ></ItemStyle>
        <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE" ></AlternatingItemStyle>
        <Columns>
            <asp:BoundColumn HeaderText="球员id" DataField="Idx" />
            <asp:BoundColumn HeaderText="经理id" DataField="ManagerId" />
            <asp:BoundColumn HeaderText="成长等级" DataField="GrowLevel" />
            <asp:BoundColumn HeaderText="成长值" DataField="GrowNum" />
            <asp:BoundColumn HeaderText="今日成长次数" DataField="DayGrowCount" />
            <asp:BoundColumn HeaderText="今日快速成长次数" DataField="DayFastGrowCount" />
            <asp:BoundColumn HeaderText="记录日期" DataField="RecordDate" />

        </Columns>
    </asp:DataGrid>

</asp:Content>

