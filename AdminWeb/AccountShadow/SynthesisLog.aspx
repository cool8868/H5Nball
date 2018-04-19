<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="SynthesisLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.SynthesisLog" %>

<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Shadow" FileName="SynthesisLog" OrderBy="order by Idx desc"
        TableName="[V_Pandora_Synthesis] with(nolock)" TableDescN="合成记录" PageSize="20" IsInitData="true" HideTitle="True">
        <WhereList>
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                Operator="Equal" IsDisable="True" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="TransactionId" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ResultType" FieldType="String"
                Operator="Equal" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ResultItemCode" FieldType="String"
                Operator="Equal" />
        </WhereList>
        <FieldList>
            <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="TransactionId" FieldDescN="事务id" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="SynthesisType" FieldDescN="合成类型" FieldType="String" FieldLen="8" EnumName="EnumSynthesisType"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode1" FieldDescN="物品编码1" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode2" FieldDescN="物品编码2" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode3" FieldDescN="物品编码2" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode4" FieldDescN="物品编码2" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemCode5" FieldDescN="物品编码2" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="SuitdrawingId" FieldDescN="图纸id" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="SuitdrawingItemCode" FieldDescN="图纸编码" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="IsProtect" FieldDescN="是否保护" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="CostCoin" FieldDescN="消耗金币" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="CostPoint" FieldDescN="消耗点券" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ResultType" FieldDescN="结果类型" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ResultItemId" FieldDescN="获得物品id" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ResultItemCode" FieldDescN="获得物品编码" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId1" FieldDescN="物品id1" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId2" FieldDescN="物品id2" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId3" FieldDescN="物品id2" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId4" FieldDescN="物品id2" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ItemId5" FieldDescN="物品id2" FieldType="String" FieldLen="8"></GMC:FieldParam>

            <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="36"></GMC:FieldParam>
            <GMC:FieldParam FieldName="AppId" FieldDescN="所属应用" FieldType="String" FieldLen="8" EnumName="app"></GMC:FieldParam>
            <GMC:FieldParam FieldName="TerminalIP" FieldDescN="服务器ip" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
        </FieldList>
    </GMC:SelectControl>
</asp:Content>



