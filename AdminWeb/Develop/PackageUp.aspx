<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Account/AccountPage.Master" CodeBehind="PackageUp.aspx.cs" Inherits="Games.NBall.AdminWeb.Develop.PackageUp" %>

<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="bgDark" cellspacing="1" cellpadding="1" border="0" style="width: 100%;">
        <tr style="height: 25px">
            <td class="bgColor2" style="width: 118px">背包格数：</td>
            <td class="bgWhite">
                <asp:Label ID="lblPackageGrid" runat="server"></asp:Label></td>
        </tr>
    </table>
    <asp:DataGrid runat="server" ID="dgPackage" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False" OnItemCommand="ItemsGrid_Command">
        <HeaderStyle CssClass="bgColor2"></HeaderStyle>
        <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF" ></ItemStyle>
        <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE" ></AlternatingItemStyle>
        <Columns>
            <asp:BoundColumn HeaderText="ItemId" DataField="ItemId" />
            <asp:BoundColumn HeaderText="code" DataField="ItemCode" />
            <asp:BoundColumn HeaderText="强化等级" DataField="Strength" />
            <asp:BoundColumn HeaderText="名称" DataField="Name" />
            <asp:BoundColumn HeaderText="数量" DataField="ItemCount" />
            <asp:BoundColumn HeaderText="类型" DataField="ItemTypeV" />
            <asp:BoundColumn HeaderText="类型2" DataField="SubTypeV" />
            <asp:BoundColumn HeaderText="出售" DataField="IsBindingV" />
            <asp:BoundColumn HeaderText="格子" DataField="GridIndex" />
            <asp:BoundColumn HeaderText="状态" DataField="StatusV" />
            <asp:ButtonColumn HeaderText="解锁" Text="解锁" CommandName="unlock"/>
            <asp:ButtonColumn HeaderText="变成可出售" Text="变成可出售" CommandName="unlockBinding"/>
            <asp:ButtonColumn HeaderText="删除" Text="删除" CommandName="delete"/>
        </Columns>
    </asp:DataGrid>

</asp:Content>
