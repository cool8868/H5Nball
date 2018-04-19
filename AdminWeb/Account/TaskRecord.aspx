<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="TaskRecord.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.TaskRecord" %>
<%@ Register TagPrefix="GMC" Namespace="Games.MyControl" Assembly="Games.MyControl" %>
<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="bgDark" cellspacing="1" cellpadding="1" border="0" style="width: 100%;">
        <tr style="height: 25px">
            <td class="bgColor2" style="width: 118px">挂起任务：</td>
            <td class="bgWhite">
                <asp:Label ID="lblPending" runat="server"></asp:Label></td>
        </tr>
    </table>
    <GMC:SelectControl ID="SelectControl1" runat="server" DbCategory="Main" FileName="TaskRecord" OrderBy="order by Idx desc"
             TableName="Task_Record with(nolock)" TableDescN="当前任务" PageSize="10" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldNameN="ManagerIda" FieldType="String"
                    Operator="Equal" IsDisable="True" />
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="TaskId" FieldDescN="任务id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="CurTimes" FieldDescN="完成次数" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="StepRecord" FieldDescN="完成步骤" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="DoneParam" FieldDescN="记录参数" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Status" FieldDescN="状态" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="UpdateTime" FieldDescN="更新时间" FieldType="String" FieldLen="9"></GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
    <GMC:SelectControl ID="SelectControl2" runat="server" DbCategory="Shadow" FileName="TaskRecord" OrderBy="order by Idx desc"
             TableName="Task_History with(nolock)" TableDescN="已交任务" PageSize="10" IsInitData="true" HideTitle="True">
            <WhereList>
                <GMC:WhereParam Connector="And" FieldLen="0" FieldName="ManagerId" FieldType="String"
                    Operator="Equal" IsDisable="True" />
            </WhereList>
            <FieldList>
                <GMC:FieldParam FieldName="Idx" FieldDescN="序号" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="ManagerId" FieldDescN="经理id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="TaskId" FieldDescN="任务id" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="CurTimes" FieldDescN="完成次数" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="StepRecord" FieldDescN="完成步骤" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="DoneParam" FieldDescN="记录参数" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="PrizeExp" FieldDescN="奖励经验" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="PrizeCoin" FieldDescN="奖励金币" FieldType="String" FieldLen="8"></GMC:FieldParam>
                <GMC:FieldParam FieldName="PrizeItemCode" FieldDescN="奖励物品" FieldType="String" FieldLen="9"></GMC:FieldParam>
                <GMC:FieldParam FieldName="Status" FieldDescN="状态" FieldType="String" FieldLen="10"></GMC:FieldParam>
                <GMC:FieldParam FieldName="RowTime" FieldDescN="创建时间" FieldType="String" FieldLen="11"></GMC:FieldParam>
                <GMC:FieldParam FieldName="UpdateTime" FieldDescN="更新时间" FieldType="String" FieldLen="12"></GMC:FieldParam>
            </FieldList>
        </GMC:SelectControl>
</asp:Content>