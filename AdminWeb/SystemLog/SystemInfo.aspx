<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Log.SystemInfo" %>
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
    <GMC:SelectControl ID="SelectControl1" runat="server" ShowZone="True" DbCategory="SystemLog" FileName="SystemInfo"
             TableName="Log_Info with(nolock)" TableDescN="系统信息日志查询" PageSize="20" IsInitData="true">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldName="AppId" FieldType="String" Operator="Equal"/>
                <GMC:WhereParam Connector="And" FieldName="FunctionId" FieldType="String" Operator="Equal" />
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="idx" Title="idx" FieldLen="8" FieldType="Int" ShowLinks="False">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="TerminalIP" FieldDescN="服务器ip" Title="服务器ip" FieldType="String" FieldLen="8">
                </GMC:FieldParam>
                <GMC:FieldParam FieldContentLen="0" FieldDescN="所属应用" FieldLen="0" FieldName="AppId"
                    FieldType="String" EnumName="app" Title="所属应用">
                </GMC:FieldParam>
                <GMC:FieldParam FieldContentLen="0" FieldDescN="函数标记" FieldLen="0" FieldName="FunctionId"
                    FieldType="String" EnumName="logfunction" Title="函数标记">
                </GMC:FieldParam>
                <GMC:FieldParam FieldDescN="消息" FieldLen="0" FieldName="Message" FieldType="String" 
                    Title="消息">
                </GMC:FieldParam>
                <GMC:FieldParam FieldDescN="RowTime" FieldLen="0" FieldName="RowTime" FieldType="DateTime" 
                    Title="RowTime">
                </GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
    </div>
    </form>
</body>
</html>
