<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="TalentInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.TalentInfo" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="TalentInfo" OrderBy="order by ManagerId asc"
             TableName="[ManagerSkill_Lib] with(nolock)"  TableDescN="天赋信息" PageSize="20" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                    Operator="Equal" IsDisable="True"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="SyncTalentPoint" FieldDescN="技能点" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="MaxTalentPoint" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="MaxWillNumber" FieldDescN="意志数" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="TodoTalents" FieldDescN="主动天赋" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="NodoTalents" FieldDescN="被动天赋" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="TodoWills" FieldDescN="主动意志" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="NodoWills" FieldDescN="被动意志" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="9"></GMC:FieldParam>

            </FieldList>
        </GMC:SelectControl>
</asp:Content>


