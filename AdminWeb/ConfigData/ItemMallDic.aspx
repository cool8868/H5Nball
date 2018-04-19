<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemMallDic.aspx.cs" Inherits="Games.NBall.AdminWeb.ConfigData.ItemMallDic" %>

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
            <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Config" FileName="ItemMallDic" OrderBy="order by MallCode asc"
                TableName="V_Dic_MallItem with(nolock)" TableDescN="商城字典表" PageSize="20" IsInitData="true">
                <WhereList>
                    <GMC:WhereParam Connector="And" FieldName="MallType" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="MallCode" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="Quality" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="CurrencyType" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="ShowUse" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="ShowFlag" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="HotFlag" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="PackageFlag" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="ItemCode" FieldType="Int" Operator="Equal" />
                </WhereList>
                <FieldList>
                    <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品编号" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ImageId" FieldDescN="图片id" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="MallCode" FieldDescN="商品编码" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Name" FieldDescN="名称" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="MallType" FieldDescN="商品类型" FieldType="Int" FieldLen="8" EnumName="EnumMallType"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Quality" FieldDescN="品质" FieldType="Int" FieldLen="8" EnumName="EnumMallItemQuality"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ShowOrder" FieldDescN="显示顺序" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="UseLevel" FieldDescN="需求等级" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ShowUse" FieldDescN="使用标记" FieldType="Int" FieldLen="8" EnumName="EnumBit"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="CurrencyType" FieldDescN="货币类型" FieldType="Int" FieldLen="8" EnumName="EnumCurrencyType"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="CurrencyCount" FieldDescN="货币数量" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="CurrencyDiscount" FieldDescN="折扣信息" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="EffectType" FieldDescN="效果类型" FieldType="Int" FieldLen="8" EnumName="EnumMallEffectType"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="EffectValue" FieldDescN="效果值" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ShowFlag" FieldDescN="是否显示" FieldType="Int" FieldLen="8" EnumName="EnumBit"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="HotFlag" FieldDescN="热卖标记" FieldType="Int" FieldLen="8" EnumName="EnumBit"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="PackageFlag" FieldDescN="是否进背包" FieldType="Int" FieldLen="8" EnumName="EnumBit"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ItemIntro" FieldDescN="商品介绍" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ItemTip" FieldDescN="提示" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="UseNote" FieldDescN="使用方法" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="UseMsg" FieldDescN="使用结果提示" FieldType="String" FieldLen="8"></GMC:FieldParam>
                </FieldList>
            </GMC:SelectControl>
        </div>
    </form>
</body>
</html>
