<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemSuitdrawingDic.aspx.cs" Inherits="Games.NBall.AdminWeb.ConfigData.ItemSuitdrawingDic" %>

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
            <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Config" FileName="ItemSuitdrawingDic" OrderBy="order by Idx asc"
                TableName="V_Dic_Suitdrawing with(nolock)" TableDescN="图纸字典表" PageSize="20" IsInitData="true">
                <WhereList>
                    <GMC:WhereParam Connector="And" FieldName="Idx" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="ItemCode" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="SuitType" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="SuitId" FieldType="Int" Operator="Equal" />
                    
                </WhereList>
                <FieldList>
                    <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品编号" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ImageId" FieldDescN="图片id" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Idx" FieldDescN="图纸id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Name" FieldDescN="名称" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="SuitType" FieldDescN="套装类型" FieldType="String" FieldLen="8" EnumName="EnumSuitType"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="SuitId" FieldDescN="套装id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="FormulaItemString" FieldDescN="合成物品串" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ResultQuality1" FieldDescN="结果品质1" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ResultQuality2" FieldDescN="结果品质2" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ResultQuality3" FieldDescN="结果品质3" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ResultQuality4" FieldDescN="结果品质4" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ResultQuality5" FieldDescN="结果品质5" FieldType="String" FieldLen="8"></GMC:FieldParam>

                </FieldList>
            </GMC:SelectControl>
        </div>
    </form>
</body>
</html>
