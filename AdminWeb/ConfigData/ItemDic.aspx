<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemDic.aspx.cs" Inherits="Games.NBall.AdminWeb.ConfigData.ItemDic" %>

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
            <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Config" FileName="ItemDic" OrderBy="order by ItemCode asc"
                TableName="Dic_Item with(nolock)" TableDescN="物品字典表" PageSize="20" IsInitData="true">
                <WhereList>
                    <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ItemType" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldLen="0" FieldName="SubType" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ThirdType" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldLen="0" FieldName="FourthType" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="ItemName" FieldType="String" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="ItemCode" FieldType="String" Operator="Equal" />
                </WhereList>
                <FieldList>
                    <GMC:FieldParam FieldName="ItemCode" FieldDescN="Code" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ItemName" FieldDescN="名称" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ItemType" FieldDescN="类型" FieldType="Int" FieldLen="8" EnumName="EnumItemType"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="SubType" FieldDescN="二级类型" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ThirdType" FieldDescN="三级类型" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="FourthType" FieldDescN="四级类型" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ImageId" FieldDescN="图片id" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="LinkId" FieldDescN="链接id" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                </FieldList>
            </GMC:SelectControl>

        </div>
    </form>
</body>
</html>
