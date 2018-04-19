<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemEquipmentDic.aspx.cs" Inherits="Games.NBall.AdminWeb.ConfigData.ItemEquipmentDic" %>

<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Config" FileName="ItemEquipmentDic" OrderBy="order by Idx asc"
                TableName="V_Dic_Equipment with(nolock)" TableDescN="装备字典表" PageSize="20" IsInitData="true">
                <WhereList>
                    <GMC:WhereParam Connector="And" FieldName="Quality" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="Idx" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="SuitType" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="SuitId" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="ItemCode" FieldType="Int" Operator="Equal" />
                </WhereList>
                <FieldList>
                    <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品编号" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ImageId" FieldDescN="图片id" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Idx" FieldDescN="装备编码" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Name" FieldDescN="名称" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="SuitType" FieldDescN="套装类型" FieldType="Int" FieldLen="8" EnumName="EnumSuitType"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="SuitId" FieldDescN="套装id" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Quality" FieldDescN="品质" FieldType="Int" FieldLen="8" EnumName="EnumEquipmentQuality"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="PropertyType1" FieldDescN="属性1" FieldType="Int" FieldLen="8" EnumName="EnumProperty"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="PropertyType2" FieldDescN="属性2" FieldType="Int" FieldLen="8" EnumName="EnumProperty"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Description" FieldDescN="描述" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    
                </FieldList>
            </GMC:SelectControl>
        </div>
    </form>
</body>
</html>
