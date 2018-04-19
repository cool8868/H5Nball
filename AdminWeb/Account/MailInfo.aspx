
<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="MailInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.MailInfo" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="MailInfo" OrderBy="order by Idx desc"
             TableName="[Mail_Info] with(nolock)"  TableDescN="邮件记录" PageSize="20" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                    Operator="Equal" IsDisable="True"/>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="MailType" FieldType="String"
                    Operator="Equal"/>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="HasAttach" FieldType="String"
                    Operator="Equal"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="MailType" FieldDescN="邮件类型" EnumName="EnumMailType" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ContentString" FieldDescN="内容" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="HasAttach" FieldDescN="附件" EnumName="EnumBit" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="IsRead" FieldDescN="已读" EnumName="EnumBit" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Status" FieldDescN="状态" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ExpiredTime" FieldDescN="过期时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
        <table class="bgDark" cellspacing="1" cellpadding="1" border="0" style="width: 100%;">
        <tr style="height: 25px">
            <td class="bgColor2" style="width: 118px">序号：</td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtRecordId" Width="120px"></asp:TextBox><asp:Button runat="server" ID="btnReceive" Text="收取" OnClick="btnReceive_OnClick"/></td>
        </tr>
    </table>
</asp:Content>


