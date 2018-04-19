<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemBallsoulDic.aspx.cs" Inherits="Games.NBall.AdminWeb.ConfigData.ItemBallsoulDic" %>
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
    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Config" FileName="ItemBallsoulDic" OrderBy="order by Idx asc"
             TableName="V_Dic_Ballsoul with(nolock)" TableDescN="球魂字典表" PageSize="20" IsInitData="true">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldName="Color" FieldType="Int" Operator="Equal"/>
                <GMC:WhereParam Connector="And" FieldName="Idx" FieldType="Int" Operator="Equal"/>
                <GMC:WhereParam Connector="And" FieldName="ItemCode" FieldType="Int" Operator="Equal"/>
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品编号" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ImageId" FieldDescN="图片id" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Idx" FieldDescN="球魂编号" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Name" FieldDescN="名称" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Color" FieldDescN="颜色" FieldType="Int" FieldLen="8" EnumName="EnumBallSoulColor"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Level" FieldDescN="等级" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Type" FieldDescN="类型" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Description" FieldDescN="描述" FieldType="String" FieldLen="8"></GMC:FieldParam>

            </FieldList>
        </GMC:SelectControl>
    </div>
    </form>
</body>
</html>
