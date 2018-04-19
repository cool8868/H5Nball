<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsPay.aspx.cs" Inherits="Games.NBall.AdminWeb.Statistics.StatisticsPay" %>

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
            <td  colspan="2">收入总览
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
                                <asp:BoundColumn DataField="RecordDateStr" HeaderText="日期" />
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
                                <asp:BoundColumn DataField="PointRemain" HeaderText="剩余点券" />
                                <asp:BoundColumn DataField="PointConsume" HeaderText="消耗点券" />
                                <asp:BoundColumn DataField="PointCirculate" HeaderText="流通点券" />
                            </Columns>
                        </asp:DataGrid>
            </td>
        </tr>
        <tr class="bgWhite">
            <td colspan="2">
                <asp:DataGrid runat="server" ID="payRank" Width="14%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                            <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                            <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                            <Columns>
                                <asp:BoundColumn DataField="Name" HeaderText="经理名" />
                                <asp:BoundColumn DataField="TotalCache" HeaderText="充值金额" />
                            </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        </table>
    </form>
</body>
</html>
