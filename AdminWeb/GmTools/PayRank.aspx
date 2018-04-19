<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayRank.aspx.cs" Inherits="Games.NBall.AdminWeb.GmTools.PayRank" %>

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
    <div>
        <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
            <tr>
        <td class="bgWhite" colspan="2" style="color: Red;">
                        <div class="errorMessage" style="padding: 0px;">
                            <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
        </td></tr>
            <tr class="bgLightGray">
                <td colspan="2" >
                    充值排行
                </td>
            </tr>
            <tr>
                <td class="bgColor2" style="width: 118px">选择区：</td>
                <td class="bgWhite"><asp:DropDownList ID="ddlZone" runat="server"></asp:DropDownList><asp:Button runat="server" ID="btnSearch" Text="查询" OnClick="btnSearch_Click"></asp:Button></td>
            </tr>
            <tr class="bgWhite">
                <td colspan="2" >
                <asp:DataGrid ID="rank" runat="server" AutoGenerateColumns="False">
                    <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                    <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                    <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                    <Columns>
                        <asp:BoundColumn DataField="Name" HeaderText="经理名" />
                        <asp:BoundColumn DataField="TotalCash" HeaderText="充值金额" />
                    </Columns>
                </asp:DataGrid>
                    </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
