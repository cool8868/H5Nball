<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="MosaicLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountShadow.MosaicLog" %>

<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Shadow" FileName="MosaicLog" OrderBy="order by Idx desc"
        TableName="[V_Pandora_Mosaic] with(nolock)" TableDescN="镶嵌记录" PageSize="20" IsInitData="true" HideTitle="True">
        <WhereList>
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                Operator="Equal" IsDisable="True" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemId" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemCode" FieldType="String"
                Operator="Equal" />
        </WhereList>
        <FieldList>
            <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
           <GMC:FieldParam FieldName="TransactionId" FieldDescN="事务id" FieldType="String" FieldLen="36" Links="ItemLog.aspx?TransactionId="></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId" FieldDescN="物品id" FieldType="String" FieldLen="36"  Links="ItemLog.aspx?ItemId="></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品编码" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="SlotId" FieldDescN="插槽id" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="BallsoulId" FieldDescN="球魂id" FieldType="String" FieldLen="36" Links="ItemLog.aspx?ItemId="></GMC:FieldParam>
            <GMC:FieldParam FieldName="BallsoulItemCode" FieldDescN="球魂编码" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="36"></GMC:FieldParam>
            <GMC:FieldParam FieldName="AppId" FieldDescN="所属应用" FieldType="String" FieldLen="8" EnumName="app"></GMC:FieldParam>
            <GMC:FieldParam FieldName="TerminalIP" FieldDescN="服务器ip" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
        </FieldList>
    </GMC:SelectControl>
</asp:Content>



