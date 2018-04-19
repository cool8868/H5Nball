<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="TeammemberTrainInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.TeammemberTrainInfo" %>

<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
        <HeaderStyle CssClass="bgColor2"></HeaderStyle>
        <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
        <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
        <Columns>
            <asp:BoundColumn HeaderText="球员id" DataField="Idx" />
            <asp:BoundColumn HeaderText="经理id" DataField="ManagerId" />
            <asp:BoundColumn HeaderText="Pid" DataField="PlayerId" />
            <asp:BoundColumn HeaderText="等级" DataField="Level" />
            <asp:BoundColumn HeaderText="经验" DataField="EXP" />
            <asp:BoundColumn HeaderText="训练体力" DataField="TrainStamina" />
            <asp:BoundColumn HeaderText="训练状态" DataField="TrainState" />
            <asp:BoundColumn HeaderText="开始时间" DataField="StartTime" />
            <asp:BoundColumn HeaderText="结算时间" DataField="SettleTime" />
            <asp:BoundColumn HeaderText="状态" DataField="Status" />


        </Columns>
    </asp:DataGrid>

</asp:Content>
