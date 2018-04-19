<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsKpi.aspx.cs" Inherits="Games.NBall.AdminWeb.Statistics.StatisticsKpi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
            <tr class="bgLightGray">
                <td colspan="2">每日数据统计
                </td>
            </tr>
            <tr>
            <td class="bgColor2" style="width: 118px">选择区：</td>
            <td class="bgWhite"><asp:DropDownList runat="server" ID="ddlZone"/><asp:Button runat="server" ID="btnSearch" Text="查询" OnClick="btnSearch_Click"/></td>
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
                            <asp:BoundColumn HeaderText="Dau" DataField="Dau" />
                            <asp:BoundColumn HeaderText="唯一ip" DataField="DUniqueIp" />
                            <asp:BoundColumn HeaderText="新用户" DataField="DNewUser" />
                            <asp:BoundColumn HeaderText="新经理" DataField="DNewManager" />
                            
                            <asp:BoundColumn HeaderText="次日留存" DataField="RetentionPercent2" />
                            <asp:BoundColumn HeaderText="3日留存" DataField="RetentionPercent3" />
                            <asp:BoundColumn HeaderText="4日留存" DataField="RetentionPercent4" />
                            <asp:BoundColumn HeaderText="5日留存" DataField="RetentionPercent5" />
                            <asp:BoundColumn HeaderText="6日留存" DataField="RetentionPercent6" />
                            <asp:BoundColumn HeaderText="7日留存" DataField="RetentionPercent7" />
                            <asp:BoundColumn HeaderText="15日留存" DataField="Retention15" />
                            <asp:BoundColumn HeaderText="30日留存" DataField="Retention30" />
                            <asp:BoundColumn HeaderText="7日流失" DataField="DLostUser7" />
                            <asp:BoundColumn HeaderText="15日流失" DataField="DLostUser15" />
                            <asp:BoundColumn HeaderText="30日流失" DataField="DLostUser30" />
                            <asp:BoundColumn HeaderText="Acu" DataField="Acu" />
                            <asp:BoundColumn HeaderText="Pcu" DataField="Pcu" />
                            <asp:BoundColumn HeaderText="Lcu" DataField="Lcu" />
                            <asp:BoundColumn HeaderText="在线时长" DataField="TotalOnline" />
                            <asp:BoundColumn HeaderText="平均在线时长" DataField="AvgOnline" />
                            <asp:BoundColumn HeaderText="Wau" DataField="Wau" />
                            <asp:BoundColumn HeaderText="周流失" DataField="WLost" />
                            <asp:BoundColumn HeaderText="周忠诚" DataField="WHonor" />
                            <asp:BoundColumn HeaderText="周忠诚流失" DataField="WHonorLost" />
                            <asp:BoundColumn HeaderText="Mau" DataField="Mau" />
                            <asp:BoundColumn HeaderText="充值用户数" DataField="PayUserCount" />
                            <asp:BoundColumn HeaderText="充值次数" DataField="PayCount" />
                            <asp:BoundColumn HeaderText="充值金额" DataField="PayTotal" />
                            <asp:BoundColumn HeaderText="首充人数" DataField="PayFirst" />
                            <asp:BoundColumn HeaderText="ARPU" DataField="ARPU" />
                            <asp:BoundColumn HeaderText="ARRPU" DataField="ARRPU" />
                            <asp:BoundColumn HeaderText="付费率" DataField="PayRate" />
                            <asp:BoundColumn HeaderText="付费用户流失" DataField="PayUserLost" />
                            <asp:BoundColumn HeaderText="付费流失率" DataField="PayLost" />
                            <asp:BoundColumn HeaderText="LTV" DataField="LTV" />
                            <asp:BoundColumn HeaderText="剩余点券" DataField="PointRemain" />
                            <asp:BoundColumn HeaderText="消耗点券" DataField="PointConsume" />
                            <asp:BoundColumn HeaderText="流通点券" DataField="PointCirculate" />
                            <asp:BoundColumn HeaderText="更新时间" DataField="UpdateTime" />

                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            </table>
        
        <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">

                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">邀请数量：</td>
                    <td colspan="5" class="bgWhite">
                        <asp:Label ID="lblInviteNumber" runat="server"></asp:Label></td>
                </tr>
            </table>
    </form>
</body>
</html>
