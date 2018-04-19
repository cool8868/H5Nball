<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="CoinChargeLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.CoinChargeLog" %>

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
            <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Shadow" FileName="CoinChargeLog" OrderBy="order by Idx desc"
             TableName="Coin_ChargeHistory with(nolock)" TableDescN="金币获取日志" PageSize="20" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldNameN="ManagerIda" FieldType="String"
                    Operator="Equal" IsDisable="True" />
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="SourceType" FieldNameN="SourceTypea" FieldType="String"
                    Operator="Equal"/>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="Coin" FieldType="Int"
                    Operator="Big" FieldDefaultValue="0"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="36"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Coin" FieldDescN="金币" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="SourceType" FieldDescN="来源" EnumName="EnumCoinChargeSourceType" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="SourceId" FieldDescN="关联记录" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
</asp:Content>


