
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrossDialSystemBankerInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.CrossDialSystemBankerInfo" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Support" ShowZone="True" FileName="CrossDialSystemBankerInfo" OrderBy="order by DomainId"
                TableName="[CrossDial_SystemBanker] with(nolock)" TableDescN="璀璨巨星系统庄家信息" PageSize="20" IsInitData="true">
                <WhereList>
                    <GMC:WhereParam Connector="And" FieldName="DomainId" FieldType="String" Operator="Equal" />
                </WhereList>
                <FieldList>
                    <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="UserName" FieldDescN="庄家名称" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="DomainId" FieldDescN="跨服编号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="SendPoint" FieldDescN="补充的绑定券" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="BindPoint" FieldDescN="当前绑定券" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Mark" FieldDescN="盈亏(为负时亏损)" FieldType="String" FieldLen="8"></GMC:FieldParam>
                </FieldList>
            </GMC:SelectControl>

        </div>
    </form>
</body>
</html>
