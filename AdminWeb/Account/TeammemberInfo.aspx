<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="TeammemberInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.TeammemberInfo" %>

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
    <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False" OnItemDataBound="DataGrid1_ItemDataBound">
        <HeaderStyle CssClass="bgColor2"></HeaderStyle>
        <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
        <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
        <Columns>
            <asp:BoundColumn DataField="PlayerId" HeaderText="pid" />
            <asp:BoundColumn DataField="Name" HeaderText="球员名" />
            <asp:BoundColumn DataField="Level" HeaderText="等级" />
            <asp:BoundColumn DataField="Strength" HeaderText="强化等级" />
            <asp:BoundColumn DataField="PropertyPoint" HeaderText="未分配属性点" />
            <asp:BoundColumn DataField="IsMain" HeaderText="主力" />
            <asp:TemplateColumn>
                <HeaderTemplate>副卡</HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblPlayerCard" runat="server" Text="副卡"></asp:Label></ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn>
                <HeaderTemplate>装备</HeaderTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblEquipment" runat="server" Text="装备"></asp:Label></ItemTemplate>
            </asp:TemplateColumn>
            <asp:HyperLinkColumn DataTextField="Idx" HeaderText="Tid" NavigateUrl="TeammemberInfo.aspx" />

        </Columns>
    </asp:DataGrid>

</asp:Content>
