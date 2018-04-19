<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="PointInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.PointInfo" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>

<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="bgDark" cellspacing="1" cellpadding="1" border="0" style="width: 100%;">
        <tr style="height: 25px">
            <td class="bgColor2" style="width: 118px">充值金额：</td>
            <td class="bgWhite">
                <asp:Label ID="lblCash" runat="server"></asp:Label></td>
            <td class="bgColor2" style="width: 118px">剩余点券：</td>
            <td class="bgWhite" colspan="3">
                <asp:Label ID="lblPoint" runat="server"></asp:Label></td>
        </tr>
        <tr style="height: 25px">
            <td class="bgColor2" style="width: 118px">充值统计：</td>
            <td class="bgWhite">
                <asp:Label ID="lblCharge" runat="server"></asp:Label></td>
            <td class="bgColor2" style="width: 118px">消费统计：</td>
            <td class="bgWhite">
                <asp:Label ID="lblConsume" runat="server"></asp:Label></td>
            <td class="bgColor2" style="width: 118px">统计剩余：</td>
            <td class="bgWhite">
                <asp:Label ID="lblPoint2" runat="server"></asp:Label></td>
        </tr>
    </table>
    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="PointInfo" OrderBy="order by Idx desc"
             TableName="Pay_ChargeHistory with(nolock)" TableDescN="充值记录" PageSize="10" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="Account" FieldNameN="Accounta" FieldType="String"
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
    <GMC:SelectControl ID="SelectControl2" runat="server" DbCategory="Main" FileName="PointInfo" OrderBy="order by Idx desc"
             TableName="Pay_ConsumeHistory with(nolock)" TableDescN="消费记录" PageSize="10" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="Account" FieldType="String"
                    Operator="Equal" IsDisable="True" />
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="SourceType" FieldType="String"
                    Operator="Equal"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Account" FieldDescN="账号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Point" FieldDescN="点券" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Bonus" FieldDescN="赠送点券" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="SourceType" FieldDescN="来源" FieldType="String" FieldLen="8" EnumName="EnumConsumeSourceType"></GMC:FieldParam>
                <GMC:FieldParam FieldName="SourceId" FieldDescN="关联记录" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>

            </FieldList>
        </GMC:SelectControl>
</asp:Content>
