<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="LoginLog.aspx.cs" Inherits="Games.NBall.AdminWeb.AccountLog.LoginLog" %>

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
    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Shadow" FileName="LoginLog" OrderBy="order by Idx desc"
        TableName="Online_History with(nolock)" TableDescN="登录日志" PageSize="20" IsInitData="true" HideTitle="True">
        <WhereList>
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                Operator="Equal" IsDisable="True" />
            <GMC:WhereParam Connector="And" FieldLen="0" FieldName="LoginIp" FieldType="String"
                Operator="Equal" />
        </WhereList>
        <FieldList>
            <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="36"></GMC:FieldParam>
            <GMC:FieldParam FieldName="LoginTime" FieldDescN="登录时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="GuildInTime" FieldDescN="记录时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="ActiveTime" FieldDescN="更新时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
            <GMC:FieldParam FieldName="CntOnlineMinutes" FieldDescN="当日在线" FieldType="String" FieldLen="9"></GMC:FieldParam>
            <GMC:FieldParam FieldName="CurOnlineMinutes" FieldDescN="即时在线" FieldType="String" FieldLen="10"></GMC:FieldParam>
            <GMC:FieldParam FieldName="LoginIp" FieldDescN="登录ip" FieldType="String" FieldLen="11"></GMC:FieldParam>
            <GMC:FieldParam FieldName="Status" FieldDescN="状态" FieldType="String" FieldLen="12"></GMC:FieldParam>
            <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="13"></GMC:FieldParam>

        </FieldList>
    </GMC:SelectControl>
</asp:Content>


