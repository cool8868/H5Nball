<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="GambleLeagueInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.GambleLeagueInfo" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="GambleLeagueInfo" OrderBy="order by Idx desc"
             TableName="[League_Bet] with(nolock)"  TableDescN="联赛竞猜记录" PageSize="20" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                    Operator="Equal" IsDisable="True"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="BetId" FieldDescN="押注ID" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="BetMoney" FieldDescN="押注数量" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="BetType" FieldDescN="押注类型" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="MoneyType" FieldDescN="货币类型" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="BetContent" FieldDescN="押注内容" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="BetResult" FieldDescN="开注状态" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ResultRate" FieldDescN="最终赔率" FieldType="String" FieldLen="9"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ResultMoney" FieldDescN="最终得到的钱" FieldType="String" FieldLen="10"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Status" FieldDescN="状态" FieldType="String" FieldLen="11"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="12"></GMC:FieldParam>

            </FieldList>
        </GMC:SelectControl>
</asp:Content>

