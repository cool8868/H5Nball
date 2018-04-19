<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="StrengthLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.StrengthLog" %>

<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Shadow" FileName="StrengthLog" OrderBy="order by Idx desc"
        TableName="[V_Pandora_Strength] with(nolock)" TableDescN="强化记录" PageSize="20" IsInitData="true" HideTitle="True">
        <WhereList>
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                Operator="Equal" IsDisable="True" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemId1" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemCode1" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemId2" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemCode2" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ResultType" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ResultStrength" FieldType="String"
                Operator="Equal" />
        </WhereList>
        <FieldList>
            <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="TransactionId" FieldDescN="事务id" FieldType="String" FieldLen="36" Links="ItemLog.aspx?TransactionId="></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode1" FieldDescN="物品编码1" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Strength1" FieldDescN="强化级别1" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode2" FieldDescN="物品编码2" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Strength2" FieldDescN="强化级别2" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="IsProtect" FieldDescN="是否保护" FieldType="String" FieldLen="9"></GMC:FieldParam>
            <GMC:FieldParam FieldName="CostCoin" FieldDescN="消耗金币" FieldType="String" FieldLen="10"></GMC:FieldParam>
            <GMC:FieldParam FieldName="CostPoint" FieldDescN="消耗点券" FieldType="String" FieldLen="11"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ResultType" FieldDescN="结果类型" FieldType="String" FieldLen="12" EnumName="EnumPandoraResultType"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ResultItemId" FieldDescN="获得物品id" FieldType="String" FieldLen="13"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ResultItemCode" FieldDescN="获得物品编码" FieldType="String" FieldLen="14"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ResultStrength" FieldDescN="获得强化级别" FieldType="String" FieldLen="15"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId1" FieldDescN="物品id1" FieldType="String" FieldLen="36"  Links="ItemLog.aspx?ItemId="></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId2" FieldDescN="物品id2" FieldType="String" FieldLen="36" Links="ItemLog.aspx?ItemId="></GMC:FieldParam>
            <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="36"></GMC:FieldParam>
            <GMC:FieldParam FieldName="AppId" FieldDescN="所属应用" FieldType="String" FieldLen="8" EnumName="app"></GMC:FieldParam>
            <GMC:FieldParam FieldName="TerminalIP" FieldDescN="服务器ip" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
        </FieldList>
    </GMC:SelectControl>
</asp:Content>



