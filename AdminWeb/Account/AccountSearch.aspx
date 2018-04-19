<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountSearch.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.AccountSearch" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <GMC:SelectControl ID="SelectControl1" runat="server" ShowZone="True" DbCategory="Main" FileName="AccountSearch"
             TableName="NB_Manager with(nolock)" TableDescN="经理查询" PageSize="20" IsInitData="true">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldName="Account" FieldType="String" Operator="Equal"/>
                <GMC:WhereParam Connector="And" FieldName="Name" FieldType="String" Operator="Equal"/>
                <GMC:WhereParam Connector="And" FieldName="Idx" FieldType="String" Operator="Equal"/>
                <GMC:WhereParam Connector="And" FieldName="Status" FieldType="String" Operator="Equal" FieldDefaultValue="0" />
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="经理id" FieldType="String" FieldLen="8" ShowLinks="True">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Account" FieldDescN="账号" FieldType="String" FieldLen="8">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Name" FieldDescN="经理名" FieldType="String" FieldLen="0"  >
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Status" FieldDescN="状态" EnumName="EnumManagerStatus" FieldType="String" FieldLen="0">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Level" FieldDescN="等级" FieldType="String" FieldLen="0"  >
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="VipLevel" FieldDescN="Vip等级" FieldType="String" FieldLen="0"  >
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Coin" FieldDescN="金币" FieldType="String" FieldLen="0"  >
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Sophisticate" FieldDescN="阅历" FieldType="String" FieldLen="0"  >
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Reiki" FieldDescN="灵气" FieldType="String" FieldLen="0"  >
                </GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
    </div>
    </form>
</body>
</html>
