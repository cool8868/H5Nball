<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="PayChargeLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.PayChargeLog" %>

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
     <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="PayChargeLog" OrderBy="order by RowTime desc"
             TableName="Pay_ChargeHistory with(nolock)" TableDescN="充值日志" PageSize="20" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="Account" FieldType="String"
                    Operator="Equal" IsDisable="True" />
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="SourceType" FieldNameN="SourceTypea" FieldType="String"
                    Operator="Equal"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Account" FieldDescN="账号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                
                <GMC:FieldParam FieldName="Point" FieldDescN="点券" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Bonus" FieldDescN="赠送点券" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Cash" FieldDescN="充值金额" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="SourceType" FieldDescN="来源" FieldType="String" FieldLen="8" EnumName="EnumChargeSourceType"></GMC:FieldParam>
                <GMC:FieldParam FieldName="BillingId" FieldDescN="关联记录" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>

            </FieldList>
        </GMC:SelectControl>
</asp:Content>




