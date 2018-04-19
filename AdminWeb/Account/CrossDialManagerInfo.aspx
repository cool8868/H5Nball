
<%@ Page Language="C#" MasterPageFile="~/Account/AccountPage.Master"  AutoEventWireup="true" CodeBehind="CrossDialManagerInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.CrossDialManagerInfo" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Support" ShowZone="True" FileName="CrossDialManagerInfo" OrderBy="order by CurrectDate desc"
        TableName="[CrossDial_ManagerHistory] with(nolock)" TableDescN="璀璨巨星玩家盈亏信息" PageSize="20" IsInitData="true">
        <WhereList>
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                    Operator="Equal" IsDisable="True"/>
            <GMC:WhereParam Connector="And" FieldName="CurrectDate" FieldType="String" Operator="Equal" />
        </WhereList>
        <FieldList>
            <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ManagerName" FieldDescN="经理名称" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="SiteId" FieldDescN="区服" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="DomainId" FieldDescN="跨服编号" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="CurrectDate" FieldDescN="时间" FieldType="DateTime" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Mark" FieldDescN="盈亏(为负时亏损)" FieldType="String" FieldLen="8"></GMC:FieldParam>
        </FieldList>
    </GMC:SelectControl>
</asp:Content>
