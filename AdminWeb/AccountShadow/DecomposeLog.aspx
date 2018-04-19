<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="DecomposeLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.DecomposeLog" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Shadow" FileName="DecomposeLog" OrderBy="order by Idx desc"
             TableName="[V_Pandora_Decompose] with(nolock)"  TableDescN="分解记录" PageSize="20" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                    Operator="Equal" IsDisable="True"/>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="AppId" FieldType="String"
                    Operator="Equal"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="TransactionId" FieldDescN="事务id" FieldType="String" FieldLen="36" Links="ItemLog.aspx?TransactionId="></GMC:FieldParam>
                <GMC:FieldParam FieldName="ItemIds" FieldDescN="物品id串" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ItemCodes" FieldDescN="物品编码串" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="CritRate" FieldDescN="暴击率" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="IsCrit" FieldDescN="是否暴击" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Reiki" FieldDescN="获得灵气" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="36"></GMC:FieldParam>
                <GMC:FieldParam FieldName="AppId" FieldDescN="所属应用" FieldType="String" FieldLen="8" EnumName="app" ></GMC:FieldParam>
                <GMC:FieldParam FieldName="TerminalIP" FieldDescN="服务器ip" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
</asp:Content>

