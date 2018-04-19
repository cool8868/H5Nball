<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="TransactionLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.TransactionLog" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Shadow" FileName="Transactionlog" OrderBy="order by Idx desc"
             TableName="[Transaction] with(nolock)"  TableDescN="事务记录" PageSize="20" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                    Operator="Equal" IsDisable="True"/>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="TransactionType" FieldType="String"
                    Operator="Equal"/>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="AppId" FieldType="String"
                    Operator="Equal"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="事务id" FieldType="String" FieldLen="36"  Links="ItemLog.aspx?TransactionId="></GMC:FieldParam>
                <GMC:FieldParam FieldName="TransactionType" FieldDescN="事务类型" FieldType="String" FieldLen="8" EnumName="EnumTransactionType"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="36"></GMC:FieldParam>
                <GMC:FieldParam FieldName="AppId" FieldDescN="所属应用" FieldType="String" FieldLen="8" EnumName="app" ></GMC:FieldParam>
                <GMC:FieldParam FieldName="TerminalIP" FieldDescN="服务器ip" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
</asp:Content>
