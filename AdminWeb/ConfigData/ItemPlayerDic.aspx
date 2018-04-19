<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemPlayerDic.aspx.cs" Inherits="Games.NBall.AdminWeb.ConfigData.ItemPlayerDic" %>

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
            <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Config" FileName="ItemPlayerDic" OrderBy="order by Idx asc"
                TableName="V_Dic_Player with(nolock)" TableDescN="球员字典表" PageSize="20" IsInitData="true">
                <WhereList>
                    <GMC:WhereParam Connector="And" FieldName="CardLevel" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="Idx" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="Position" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="LeagueLevel" FieldType="Int" Operator="Equal" />
                    <GMC:WhereParam Connector="And" FieldName="ItemCode" FieldType="Int" Operator="Equal" />
                </WhereList>
                <FieldList>
                    <GMC:FieldParam FieldName="ItemCode" FieldDescN="物品编号" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="ImageId" FieldDescN="图片id" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Idx" FieldDescN="球员id" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Name" FieldDescN="名称" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Position" FieldDescN="位置" FieldType="Int" FieldLen="8" EnumName="EnumPosition"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="CardLevel" FieldDescN="颜色" FieldType="Int" FieldLen="8" EnumName="EnumPlayerCardLevel"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="LeagueLevel" FieldDescN="联赛" FieldType="Int" FieldLen="8" EnumName="EnumPlayerLeagueLevel"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="NameEn" FieldDescN="英文名" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Specific" FieldDescN="具体值" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Kpi" FieldDescN="能力值" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Speed" FieldDescN="速度" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Shoot" FieldDescN="射门" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="FreeKick" FieldDescN="任意球" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Balance" FieldDescN="控制" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Physique" FieldDescN="体质" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Bounce" FieldDescN="弹跳" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Aggression" FieldDescN="侵略性" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Disturb" FieldDescN="干扰" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Interception" FieldDescN="抢断" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Dribble" FieldDescN="控球" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Pass" FieldDescN="传球" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Mentality" FieldDescN="意识" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Response" FieldDescN="反应" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Positioning" FieldDescN="位置感" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="HandControl" FieldDescN="手控球" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Acceleration" FieldDescN="加速度" FieldType="Int" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Club" FieldDescN="俱乐部" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Birthday" FieldDescN="生日" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Stature" FieldDescN="身高" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Weight" FieldDescN="体重" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Nationality" FieldDescN="国籍" FieldType="String" FieldLen="8"></GMC:FieldParam>
                    <GMC:FieldParam FieldName="Description" FieldDescN="描述" FieldType="String" FieldLen="8"></GMC:FieldParam>


                </FieldList>
            </GMC:SelectControl>
        </div>
    </form>
</body>
</html>
