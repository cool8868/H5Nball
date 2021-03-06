﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsOther.aspx.cs" Inherits="Games.NBall.AdminWeb.Statistics.StatisticsOther" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
        <tr class="bgLightGray">
            <td colspan="2">其他数据统计
            </td>
        </tr>
                   <tr>
            <td class="bgColor2" style="width: 118px">选择区：</td>
            <td class="bgWhite"><asp:DropDownList runat="server" ID="ddlZone"/><asp:Button runat="server" ID="btnSearch" Text="查询" OnClick="btnSearch_Click"/></td>
                </tr>
        <tr class="bgWhite">
            <td colspan="2">
                <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                            <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                            <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                            <Columns>
                                <asp:BoundColumn HeaderText="区" DataField="ZoneId" />
                                <asp:BoundColumn HeaderText="统计类型" DataField="AnalyseTypeStr" />
                                <asp:BoundColumn HeaderText="记录日期" DataField="RecordDateStr" />
                                <asp:BoundColumn HeaderText="总数值" DataField="RealTotalValue" />
                                <asp:BoundColumn HeaderText="最小值" DataField="MinValue" />
                                <asp:BoundColumn HeaderText="最小值时间" DataField="MinTime" />
                                <asp:BoundColumn HeaderText="最大值" DataField="MaxValue" />
                                <asp:BoundColumn HeaderText="最大值时间" DataField="MaxTime" />
                                <asp:BoundColumn HeaderText="Hour0" DataField="Hour0" />
                                <asp:BoundColumn HeaderText="Hour1" DataField="Hour1" />
                                <asp:BoundColumn HeaderText="Hour2" DataField="Hour2" />
                                <asp:BoundColumn HeaderText="Hour3" DataField="Hour3" />
                                <asp:BoundColumn HeaderText="Hour4" DataField="Hour4" />
                                <asp:BoundColumn HeaderText="Hour5" DataField="Hour5" />
                                <asp:BoundColumn HeaderText="Hour6" DataField="Hour6" />
                                <asp:BoundColumn HeaderText="Hour7" DataField="Hour7" />
                                <asp:BoundColumn HeaderText="Hour8" DataField="Hour8" />
                                <asp:BoundColumn HeaderText="Hour9" DataField="Hour9" />
                                <asp:BoundColumn HeaderText="Hour10" DataField="Hour10" />
                                <asp:BoundColumn HeaderText="Hour11" DataField="Hour11" />
                                <asp:BoundColumn HeaderText="Hour12" DataField="Hour12" />
                                <asp:BoundColumn HeaderText="Hour13" DataField="Hour13" />
                                <asp:BoundColumn HeaderText="Hour14" DataField="Hour14" />
                                <asp:BoundColumn HeaderText="Hour15" DataField="Hour15" />
                                <asp:BoundColumn HeaderText="Hour16" DataField="Hour16" />
                                <asp:BoundColumn HeaderText="Hour17" DataField="Hour17" />
                                <asp:BoundColumn HeaderText="Hour18" DataField="Hour18" />
                                <asp:BoundColumn HeaderText="Hour19" DataField="Hour19" />
                                <asp:BoundColumn HeaderText="Hour20" DataField="Hour20" />
                                <asp:BoundColumn HeaderText="Hour21" DataField="Hour21" />
                                <asp:BoundColumn HeaderText="Hour22" DataField="Hour22" />
                                <asp:BoundColumn HeaderText="Hour23" DataField="Hour23" />
                                <asp:BoundColumn HeaderText="更新时间" DataField="UpdateTime" />

                            </Columns>
                        </asp:DataGrid>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
