<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RRPayAccountLog.aspx.cs" Inherits="Games.NBall.AdminWeb.GmTools.RRPayAccountLog" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Support" FileName="RRPayAccountLog" OrderBy=" order by  TotalCash desc"
             TableName="RR_PayAccount with(nolock)" TableDescN="(人人)充值查询" PageSize="20" IsInitData="true">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="Account" FieldType="String"
                    Operator="Equal" />
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="AccountName" FieldType="String"
                    Operator="Equal"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="50"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ZoneId" FieldDescN="所属区" FieldType="String" FieldLen="50"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Account" FieldDescN="账号" FieldType="String" FieldLen="50"></GMC:FieldParam>
                <GMC:FieldParam FieldName="AccountName" FieldDescN="账号名称" FieldType="String" FieldLen="50"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="角色id" FieldType="String" FieldLen="50"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerName" FieldDescN="角色名称" FieldType="String" FieldLen="50"></GMC:FieldParam>
                <GMC:FieldParam FieldName="CurrentPoint" FieldDescN="点券" FieldType="String" FieldLen="50"></GMC:FieldParam>
                <GMC:FieldParam FieldName="BonusPoint" FieldDescN="赠送点券" FieldType="String" FieldLen="50"></GMC:FieldParam>
                <GMC:FieldParam FieldName="TotalCash" FieldDescN="充值金额" FieldType="String" FieldLen="50"></GMC:FieldParam>
                

            </FieldList>
        </GMC:SelectControl>
    </form>
</body>
</html>
