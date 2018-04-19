<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsOnline.aspx.cs" Inherits="Games.NBall.AdminWeb.Statistics.StatisticsOnline" %>

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
                    
            <tr>
            <td class="bgColor2" style="width: 118px">选择区：</td>
            <td class="bgWhite"><asp:DropDownList runat="server" ID="ddlZone"/><asp:Button runat="server" ID="btnSearch" Text="查询" OnClick="btnSearch_Click"/></td>
                </tr>
        <tr class="bgLightGray">
            <td colspan="2">同时在线
            </td>
            </tr>
        <tr class="bgWhite">
            <td colspan="2">
                <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                            <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                            <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                            <Columns>
                                <asp:BoundColumn DataField="RecordDateStr" HeaderText="日期" />
                                <asp:BoundColumn DataField="Pcu" HeaderText="PCU" />
                                <asp:BoundColumn DataField="Acu" HeaderText="ACU" />
                                <asp:BoundColumn DataField="Lcu" HeaderText="LCU" />
                                <asp:BoundColumn DataField="TotalOnline" HeaderText="在线时长" />
                                <asp:BoundColumn DataField="AvgOnline" HeaderText="平均在线时长" />
                            </Columns>
                        </asp:DataGrid>
            </td>
        </tr>
            <tr class="bgLightGray">
            <td colspan="2">同时在线详细记录
            </td>
        </tr>
            <tr class="bgWhite">
            <td colspan="2">
                <asp:DataGrid runat="server" ID="datagrid2" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                            <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                            <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                            <Columns>
                               <asp:BoundColumn HeaderText="区" DataField="ZoneId" />
                                <asp:BoundColumn HeaderText="记录日期" DataField="RecordDate" />
                                <asp:BoundColumn HeaderText="总在线时长" DataField="TotalMinutes" />
                                <asp:BoundColumn HeaderText="Pcu" DataField="Pcu" />
                                <asp:BoundColumn HeaderText="Acu" DataField="Acu" />
                                <asp:BoundColumn HeaderText="Lcu" DataField="Lcu" />
                                <asp:BoundColumn HeaderText="Lcu时间" DataField="MinTime" />
                                <asp:BoundColumn HeaderText="Pcu时间" DataField="MaxTime" />
                                <asp:BoundColumn HeaderText="Hour0" DataField="VHour0" />
                                <asp:BoundColumn HeaderText="Hour1" DataField="VHour1" />
                                <asp:BoundColumn HeaderText="Hour2" DataField="VHour2" />
                                <asp:BoundColumn HeaderText="Hour3" DataField="VHour3" />
                                <asp:BoundColumn HeaderText="Hour4" DataField="VHour4" />
                                <asp:BoundColumn HeaderText="Hour5" DataField="VHour5" />
                                <asp:BoundColumn HeaderText="Hour6" DataField="VHour6" />
                                <asp:BoundColumn HeaderText="Hour7" DataField="VHour7" />
                                <asp:BoundColumn HeaderText="Hour8" DataField="VHour8" />
                                <asp:BoundColumn HeaderText="Hour9" DataField="VHour9" />
                                <asp:BoundColumn HeaderText="Hour10" DataField="VHour10" />
                                <asp:BoundColumn HeaderText="Hour11" DataField="VHour11" />
                                <asp:BoundColumn HeaderText="Hour12" DataField="VHour12" />
                                <asp:BoundColumn HeaderText="Hour13" DataField="VHour13" />
                                <asp:BoundColumn HeaderText="Hour14" DataField="VHour14" />
                                <asp:BoundColumn HeaderText="Hour15" DataField="VHour15" />
                                <asp:BoundColumn HeaderText="Hour16" DataField="VHour16" />
                                <asp:BoundColumn HeaderText="Hour17" DataField="VHour17" />
                                <asp:BoundColumn HeaderText="Hour18" DataField="VHour18" />
                                <asp:BoundColumn HeaderText="Hour19" DataField="VHour19" />
                                <asp:BoundColumn HeaderText="Hour20" DataField="VHour20" />
                                <asp:BoundColumn HeaderText="Hour21" DataField="VHour21" />
                                <asp:BoundColumn HeaderText="Hour22" DataField="VHour22" />
                                <asp:BoundColumn HeaderText="Hour23" DataField="VHour23" />
                                <asp:BoundColumn HeaderText="更新时间" DataField="UpdateTime" />

                            </Columns>
                        </asp:DataGrid>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
