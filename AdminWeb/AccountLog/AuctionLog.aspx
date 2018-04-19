<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="AuctionLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.AuctionLog" %>

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
    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="AuctionLog" OrderBy="order by Idx desc"
        TableName="V_Auction_Bak with(nolock)" TableDescN="拍卖日志" PageSize="10" IsInitData="true" HideTitle="True">
        <WhereList>
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                Operator="Equal" IsDisable="True" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemType" FieldType="String"
                Operator="Equal"/>
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="Idx" FieldType="String"
                Operator="Equal"/>
        </WhereList>
        <FieldList>
            <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="OwnerName" FieldDescN="经理名" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId" FieldDescN="物品id" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品code" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemName" FieldDescN="物品名" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemType" FieldDescN="物品类型" FieldType="String" FieldLen="8" EnumName="EnumItemType"></GMC:FieldParam>
            <GMC:FieldParam FieldName="SecondType" FieldDescN="二级类型" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ThirdType" FieldDescN="三级类型" FieldType="String" FieldLen="9"></GMC:FieldParam>
            <GMC:FieldParam FieldName="PriceStart" FieldDescN="起拍价格" FieldType="String" FieldLen="10"></GMC:FieldParam>
            <GMC:FieldParam FieldName="PriceEnd" FieldDescN="一口价" FieldType="String" FieldLen="11"></GMC:FieldParam>
            <GMC:FieldParam FieldName="CurrentPrice" FieldDescN="当前价格" FieldType="String" FieldLen="12"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Auctioner" FieldDescN="竞拍者id" FieldType="String" FieldLen="13"></GMC:FieldParam>
            <GMC:FieldParam FieldName="AuctionName" FieldDescN="竞拍者名称" FieldType="String" FieldLen="14"></GMC:FieldParam>
            <GMC:FieldParam FieldName="AuctionTimes" FieldDescN="竞拍次数" FieldType="String" FieldLen="15"></GMC:FieldParam>
            <GMC:FieldParam FieldName="SellHours" FieldDescN="出售小时" FieldType="String" FieldLen="16"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Status" FieldDescN="状态" FieldType="String" FieldLen="17"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Rowtime" FieldDescN="创建时间" FieldType="DateTime" FieldLen="18"></GMC:FieldParam>
        </FieldList>
    </GMC:SelectControl>
</asp:Content>

