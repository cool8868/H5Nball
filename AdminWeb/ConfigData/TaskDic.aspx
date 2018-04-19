<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaskDic.aspx.cs" Inherits="Games.NBall.AdminWeb.ConfigData.TaskDic" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Config" FileName="TaskDic" OrderBy="order by Idx asc"
             TableName="Config_Task with(nolock)" TableDescN="任务配置" PageSize="20" IsInitData="true">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="TaskType" FieldType="String"
                    Operator="Equal" />
                <GMC:WhereParam Connector="And" FieldName="Name" FieldType="String" Operator="Like"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="任务id" Title="任务id"
                    FieldLen="8" FieldType="Int" IsEdit="False" IsImg="False" IsSort="false" 
                    Links="" LinkTarget="" ShowLinks="False">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Name" FieldDescN="任务名称" Title="任务名称" FieldType="String" FieldLen="50">
                </GMC:FieldParam>
                <GMC:FieldParam FieldContentLen="0" FieldDescN="任务类型" FieldLen="0" FieldName="TaskType"
                    FieldType="String" EnumName="EnumTaskType" Title="任务类型">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerLevel" FieldDescN="所需等级" Title="所需等级" FieldType="Int" FieldLen="8">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="ParentId" FieldDescN="前置任务id" Title="前置任务id" FieldType="String" FieldLen="8">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Times" FieldDescN="需完成数量" Title="需完成数量" FieldType="Int" FieldLen="50">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="PrizeExp" FieldDescN="经验" Title="任务名称" FieldType="Int" FieldLen="50">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="PrizeCoin" FieldDescN="金币" Title="任务名称" FieldType="Int" FieldLen="50">
                </GMC:FieldParam>
                <GMC:FieldParam FieldName="Description" FieldDescN="任务描述" Title="任务描述" FieldType="String" FieldLen="100">
                </GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
    </form>
</body>
</html>
