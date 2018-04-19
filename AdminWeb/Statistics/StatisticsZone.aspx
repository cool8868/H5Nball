<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StatisticsZone.aspx.cs" Inherits="Games.NBall.AdminWeb.Statistics.StatisticsZone" %>

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
                <asp:DropDownList runat="server" ID="ddlZone" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged" />
                <asp:Button runat="server" ID="btnSearch" Text="查询" OnClick="btnSearch_Click" />
            </td>
        </tr>
        <tr style="height: 25px">
            <td class="bgColor2" style="width: 118px">
                选择日期：
            </td>
            <td class="bgWhite">
                <input style="width:120px" class="Wdate" type="text" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" runat="server" id="txtStartTime"/>
                <input style="width:120px" class="Wdate" type="text" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" runat="server" id="txtEndTime"/>
            </td>
        </tr>
        <tr class="bgWhite">
            <td colspan="2">
                <asp:DataGrid runat="server" ID="datagridzone" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False" OnSelectedIndexChanged="datagridzone_SelectedIndexChanged">
                    <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                    <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                    <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                    <Columns>
                        <asp:BoundColumn DataField="RecordDateStr" HeaderText="日期" />
                        <asp:BoundColumn HeaderText="记录日期" DataField="RecordDate" />
                        <asp:BoundColumn HeaderText="新用户" DataField="DNewUser" />
                        <asp:BoundColumn HeaderText="新经理" DataField="DNewManager" />
                        <asp:BoundColumn HeaderText="平均在线用户" DataField="Acu"  />
                        <asp:BoundColumn HeaderText="最高在线用户" DataField="Pcu" />
                        <asp:BoundColumn HeaderText="充值金额" DataField="PayTotal" />
                        <asp:BoundColumn HeaderText="ARPU" DataField="ARPU" />
                        <asp:BoundColumn HeaderText="首充人数" DataField="PayFirst" /> 
                    </Columns>
                </asp:DataGrid>
            </td>  
        </tr>
    </table>
    </form>
</body>
</html>
