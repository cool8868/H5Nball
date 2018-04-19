<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsActivityDy.aspx.cs" Inherits="Games.NBall.AdminWeb.Statistics.StatisticsActivity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
        <tr>
            <td class="bgWhite" colspan="2" style="color: Red;">
                 <div class="errorMessage" style="padding: 0px;">
                    <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                 </div>       
            </td>
        </tr>
        <tr class="bgLightGray">
            <td colspan="2">
                详情
            </td>
        </tr>
        <tr>
            <td class="bgColor2" style="width: 118px">选择区：</td>
            <td class="bgWhite">
                <asp:DropDownList runat="server" ID="ddlZone" />
                <asp:Button runat="server" ID="btnSearch" Text="查询" OnClick="btnSearch_Click"/>
            </td>
        </tr>
        <tr class="bgWhite">
            <td colspan="2">
                <asp:DataGrid runat="server" ID="datagridzone" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                    <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                    <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                    <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                    <Columns>
                        <asp:BoundColumn HeaderText="区服" DataField="ZoneName" />
                        <asp:BoundColumn HeaderText="用户名" DataField="Account" />
                        <asp:BoundColumn HeaderText="经理ID" DataField="ManagerId" />
                        <asp:BoundColumn HeaderText="活动ID" DataField="ExcitingId" />
                        <asp:BoundColumn HeaderText="活动橙卡数量" DataField="CurData" />
                        <asp:BoundColumn HeaderText="状态" DataField="Status" />
                        <asp:BoundColumn HeaderText="强化加7数量" DataField="Strength7" />
                        <asp:BoundColumn HeaderText="强化加9数量" DataField="Strength9" />
                    </Columns>
                </asp:DataGrid>
                
                <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                    <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                    <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                    <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                    <Columns>
                        <asp:BoundColumn HeaderText="区服" DataField="ZoneName" />
                        <asp:BoundColumn HeaderText="用户名" DataField="Account" />
                        <asp:BoundColumn HeaderText="经理ID" DataField="ManagerId" />
                        <asp:BoundColumn HeaderText="天梯名次" DataField="Curdata" />
                    </Columns>
                </asp:DataGrid>
                
                <asp:DataGrid runat="server" ID="datagrid2" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                    <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                    <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                    <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                    <Columns>
                        <asp:BoundColumn HeaderText="区服" DataField="ZoneName" />
                        <asp:BoundColumn HeaderText="用户名" DataField="Account" />
                        <asp:BoundColumn HeaderText="经理ID" DataField="ManagerId" />
                        <asp:BoundColumn HeaderText="活动ID" DataField="ExcitingId" />
                        <asp:BoundColumn HeaderText="综合实力名次" DataField="Curdata" />
                    </Columns>
                </asp:DataGrid>
            </td>  
        </tr>
    </table>
    </form>
</body>
</html>
