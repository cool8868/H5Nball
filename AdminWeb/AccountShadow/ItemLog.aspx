<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="ItemLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.ItemLog" %>


<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Shadow" FileName="ItemLog" OrderBy="order by Idx desc"
        TableName="[V_Item] with(nolock)" TableDescN="物品操作记录" PageSize="20" IsInitData="true" HideTitle="True">
        <WhereList>
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                Operator="Equal" IsDisable="True" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="TransactionType" FieldType="String"
                Operator="Equal" />
             <GMC:WhereParam Connector="And" FieldLen="0" FieldName="TransactionId" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemId" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemCode" FieldType="String"
                Operator="Equal" />
            
        </WhereList>
        <FieldList>
            <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="TransactionId" FieldDescN="事务id" FieldType="String" FieldLen="36" Links="ItemLog.aspx?TransactionId="></GMC:FieldParam>
            <GMC:FieldParam FieldName="TransactionType" FieldDescN="事务类型" FieldType="String" FieldLen="8" EnumName="EnumTransactionType"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId" FieldDescN="物品id" FieldType="String" FieldLen="36" Links="ItemLog.aspx?ItemId="></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品编码" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品名称" FieldType="String" FieldLen="8" EnumName="ItemName"></GMC:FieldParam>
            <GMC:FieldParam FieldName="OperationType" FieldDescN="操作类型" FieldType="String" FieldLen="8" EnumName="EnumOperationType"></GMC:FieldParam>
            <GMC:FieldParam FieldName="OperationCount" FieldDescN="操作数量" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCount" FieldDescN="物品数量" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemType" FieldDescN="物品类型" FieldType="String" FieldLen="8" EnumName="EnumItemType"></GMC:FieldParam>
            <GMC:FieldParam FieldName="IsBinding" FieldDescN="是否绑定" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="GridIndex" FieldDescN="所属格数" FieldType="String" FieldLen="9"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Status" FieldDescN="状态" FieldType="String" FieldLen="10"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="36"></GMC:FieldParam>
            <GMC:FieldParam FieldName="AppId" FieldDescN="所属应用" FieldType="String" FieldLen="8" EnumName="app"></GMC:FieldParam>
            <GMC:FieldParam FieldName="TerminalIP" FieldDescN="服务器ip" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8" IsSort="True"></GMC:FieldParam>
        </FieldList>
    </GMC:SelectControl>
</asp:Content>
