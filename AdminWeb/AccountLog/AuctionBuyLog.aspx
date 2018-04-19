<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="AuctionBuyLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.AuctionBuyLog" %>

<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
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
    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="AuctionBuyLog" OrderBy="order by Idx desc"
        TableName="V_AuctionHistory with(nolock)" TableDescN="竞拍日志" PageSize="10" IsInitData="true" HideTitle="True">
        <WhereList>
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                Operator="Equal" IsDisable="True" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="AuctionId" FieldType="String"
                Operator="Equal" />
        </WhereList>
        <FieldList>
            <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="AuctionId" FieldDescN="拍卖记录" FieldType="String" FieldLen="8" Links="AuctionLog.aspx?Idx="></GMC:FieldParam>
            <GMC:FieldParam FieldName="ManagerId" FieldDescN="竞拍者" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Price" FieldDescN="价格" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Status" FieldDescN="状态" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Rowtime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
        </FieldList>
    </GMC:SelectControl>
</asp:Content>

