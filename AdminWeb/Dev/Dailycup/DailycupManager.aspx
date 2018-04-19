<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailycupManager.aspx.cs" Inherits="Games.NBall.AdminWeb.Dev.DailycupManager" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="TaskDic" OrderBy="Idx desc"
             TableName="Dailycup_Info with(nolock)" TableDescN="杯赛列表" PageSize="20" IsInitData="true">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="Idx" FieldType="Int"
                    Operator="Equal" />
                <GMC:WhereParam Connector="And" FieldName="Status" FieldType="Int" Operator="Like"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="杯赛id" Title="杯赛id" FieldLen="8" FieldType="Int">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Round" FieldDescN="轮次" Title="轮次" FieldLen="8" FieldType="Int">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="OpenGambleRound" FieldDescN="开奖轮次" Title="开奖轮次" FieldLen="8" FieldType="Int">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="AttendDate" FieldDescN="报名日期" Title="报名日期" FieldLen="8" FieldType="DateTime">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="RunDate" FieldDescN="运行日期" Title="运行日期" FieldLen="8" FieldType="DateTime">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Status" FieldDescN="状态" Title="状态" FieldLen="8" FieldType="Int" EnumName="EnumDailycupStatus">
                </GMC:FieldParam>
                 <GMC:FieldParam FieldName="RowTime" FieldDescN="RowTime" Title="RowTime" FieldLen="8" FieldType="DateTime">
                </GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
    </form>
</body>
</html>
