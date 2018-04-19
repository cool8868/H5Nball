<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="EquipmentWashLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.EquipmentWashLog" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Shadow" FileName="EquipmentWashLog" OrderBy="order by Idx desc"
             TableName="[V_Pandora_EquipmentWash] with(nolock)"  TableDescN="洗炼记录" PageSize="20" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                    Operator="Equal" IsDisable="True"/>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemId" FieldType="String"
                    Operator="Equal"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="TransactionId" FieldDescN="事务id" FieldType="String" FieldLen="36" Links="ItemLog.aspx?TransactionId="></GMC:FieldParam>
                <GMC:FieldParam FieldName="ItemId" FieldDescN="物品id" FieldType="String" FieldLen="36" Links="ItemLog.aspx?ItemId="></GMC:FieldParam>
                <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品编码" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="LockPropertyId" FieldDescN="锁定属性id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="BuyStone" FieldDescN="是否购买洗炼石" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="BuyFusogen" FieldDescN="是否购买融合剂" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="CostPoint" FieldDescN="消耗点券" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="36"></GMC:FieldParam>
                <GMC:FieldParam FieldName="AppId" FieldDescN="所属应用" FieldType="String" FieldLen="8" EnumName="app" ></GMC:FieldParam>
                <GMC:FieldParam FieldName="TerminalIP" FieldDescN="服务器ip" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
</asp:Content>


