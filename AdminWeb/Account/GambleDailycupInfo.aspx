<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="GambleDailycupInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.GambleDailycupInfo" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="GambleDailycupInfo" OrderBy="order by Idx desc"
             TableName="[DailyCup_Gamble] with(nolock)"  TableDescN="杯赛竞猜记录" PageSize="20" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                    Operator="Equal" IsDisable="True"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品编号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Strength" FieldDescN="强化级别" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="MatchId" FieldDescN="比赛id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="HomeName" FieldDescN="主队名" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="AwayName" FieldDescN="客队名" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="DailyCupId" FieldDescN="杯赛id" FieldType="String" FieldLen="9"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RoundLevel" FieldDescN="回合" FieldType="String" FieldLen="10"></GMC:FieldParam>
                <GMC:FieldParam FieldName="GambleResult" FieldDescN="押注类型" FieldType="String" FieldLen="11"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ResultLevel" FieldDescN="结果类型" FieldType="String" FieldLen="12"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Status" FieldDescN="状态" FieldType="String" FieldLen="13"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="14"></GMC:FieldParam>

            </FieldList>
        </GMC:SelectControl>
</asp:Content>

