<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Statistics.StatisticsInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
            <tr class="bgLightGray">
                <td colspan="2">概览
                </td>
            </tr>
            <tr class="bgWhite">
                <td colspan="2">
                    <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                        <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                        <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                        <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                        <Columns>
                            <asp:BoundColumn DataField="ZoneId" HeaderText="区" />
                            <asp:BoundColumn DataField="TotalUser" HeaderText="用户数" />
                            <asp:BoundColumn DataField="TotalManager" HeaderText="经理数" />
                            <asp:BoundColumn DataField="TotalPay" HeaderText="充值金额" />
                            <asp:BoundColumn DataField="PointRemain" HeaderText="剩余点券" />
                            <asp:BoundColumn DataField="Pcu" HeaderText="最高在线用户" />
                            <asp:BoundColumn DataField="Acu" HeaderText="平均在线用户" />
                            <asp:BoundColumn DataField="OnlineMinutes" HeaderText="总在线时长" />
                            <asp:BoundColumn DataField="AvgMinutes" HeaderText="平均在线时长" />
                            <asp:BoundColumn DataField="UpdateTime" HeaderText="统计时间" />
                            <asp:ButtonColumn Text="统计"/>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr class="bgLightGray">
                <td colspan="2">每日详情
                </td>
            </tr>

         <tr>
            <td class="bgColor2" style="width: 118px">选择区：</td>
            <td class="bgWhite"><asp:DropDownList runat="server" ID="ddlZone"/><asp:Button runat="server" ID="btnSearch" Text="查询" OnClick="btnSearch_Click"/></td>
                </tr>
            <tr style="height: 25px">
                <td class="bgColor2" style="width: 118px">选择日期：</td>

                <td class="bgWhite">
                    <input style="width:120px" class="Wdate" type="text" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" runat="server" id="txtStartTime">
                    <input style="width:120px" class="Wdate" type="text" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" runat="server" id="txtEndTime">
                </td>
            </tr>
            <tr class="bgWhite">
                <td colspan="2">
                    <asp:DataGrid runat="server" ID="datagrid2" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                        <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                        <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                        <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                        <Columns>
                            <asp:BoundColumn DataField="RecordDateStr" HeaderText="日期" />
                            <asp:BoundColumn DataField="Dau" HeaderText="日活跃用户数" />
                            <asp:BoundColumn DataField="DNewUser" HeaderText="新用户" />
                            <asp:BoundColumn DataField="DNewManager" HeaderText="新经理" />
                            <asp:BoundColumn DataField="PayTotal" HeaderText="收入" />
                            <asp:BoundColumn DataField="Pcu" HeaderText="最高在线用户" />
                            <asp:BoundColumn DataField="Acu" HeaderText="平均在线用户" />
                          
                              <asp:BoundColumn DataField="PointRemain" HeaderText="剩余点券" />
                            <asp:BoundColumn DataField="PointConsume" HeaderText="消耗点券" />
                            <asp:BoundColumn DataField="GetCoin" HeaderText="获得金币" />
                            <asp:BoundColumn DataField="CoinConsume" HeaderText="消耗金币" />
                            <asp:BoundColumn DataField="EnergyConsume" HeaderText="消耗体力" />
                            <asp:BoundColumn DataField="PointCirculate" HeaderText="流通点券" />
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
