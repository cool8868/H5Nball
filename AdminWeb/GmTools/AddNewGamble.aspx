<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewGamble.aspx.cs" Inherits="Games.NBall.AdminWeb.GmTools.AddNewGamble" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
        <tr class="bgLightGray">
            <td colspan="2">发起竞猜
            </td>
        </tr>
        <tr>
        <td class="bgWhite" style="color: Red;" colspan="2">
                        <div class="errorMessage" style="padding: 0px;">
                            <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
        </td></tr>
        <tr>
        <td class="bgWhite">所属区：</td>
            <td class="bgWhite"><asp:DropDownList ID="SZone" runat="server"></asp:DropDownList>
                <asp:CheckBox ID="chkAll" runat="server" Text="所有区" Checked="True" />
            </td>
            </tr>
        <tr >
            <td class="bgColor2" style="width: 118px">
                主队：
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtHomeName" Width="400px" ></asp:TextBox>
                <div></div>
            </td>
            </tr>
        <tr>
             <tr >
            <td class="bgColor2" style="width: 118px">
                客队：
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtAwayName" Width="400px" ></asp:TextBox>
                <div></div>
            </td>
            </tr>
        <tr>
            <td class="bgColor2" style="width: 118px">
                开始时间：
            </td>
            <td class="bgWhite">
               <div><asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox></div>
                
               <div>格式参考yyyy-MM-dd hh:mm:ss：2014-06-18 12:58:58</div>
            </td>
            </tr>
        <tr height="60px">
            <td class="bgWhite" colspan="2">
                <div style="margin-left: 300px"><asp:Button runat="server" ID="btnSend" Text="确定" Width="60px" Height="25px" OnClick="btnSend_Click"></asp:Button></div>
            </td>
            </tr>
        </table>
        
          <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
        <tr class="bgLightGray">
            <td colspan="2">结束竞猜
            </td>
        </tr>
           <tr>
            <td class="bgColor2" style="width: 118px">
                比赛ID：
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtMatchId" Width="400px" ></asp:TextBox>
                <div></div>
            </td>
            </tr>
        <tr>
        <tr>
            <td class="bgColor2" style="width: 118px">
                主队进球数：
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtHomeGoals" Width="400px" ></asp:TextBox>
                <div></div>
            </td>
            </tr>
        <tr>
             <tr >
            <td class="bgColor2" style="width: 118px">
                客队进球数：
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtAwayGoals" Width="400px" ></asp:TextBox>
                <div></div>
            </td>
            </tr>
        <tr height="60px">
            <td class="bgWhite" colspan="2">
                <div style="margin-left: 300px"><asp:Button runat="server" ID="Button1" Text="确定" Width="60px" Height="25px" OnClick="btnEnd_Click"></asp:Button></div>
            </td>
        </tr>
        </table>
        
         <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False" OnItemDataBound="DataGrid1_ItemDataBound">
        <HeaderStyle CssClass="bgColor2"></HeaderStyle>
        <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
        <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
        <Columns>
            <asp:BoundColumn DataField="MatchId" HeaderText="比赛ID" />
            <asp:BoundColumn DataField="HomeName" HeaderText="主队名" />
            <asp:BoundColumn DataField="AwayName" HeaderText="客队名" />
            <asp:BoundColumn DataField="HomeGoals" HeaderText="主队进球数" />
            <asp:BoundColumn DataField="AwayGoals" HeaderText="客队进球数" />
            <asp:BoundColumn DataField="ResultTypeString" HeaderText="胜利类型" />
            <asp:BoundColumn DataField="StatusString" HeaderText="状态" />
            <asp:BoundColumn DataField="MatchTime" HeaderText="比赛时间" />
        </Columns>
    </asp:DataGrid>
        <br/><br/>
         <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
        <tr class="bgLightGray">
            <td colspan="2">手动补发奖
            </td>
        </tr>
           <tr>
            <td class="bgColor2" style="width: 118px">
                比赛ID：
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtSendMatchId" Width="400px" ></asp:TextBox>
                <div></div>
            </td>
            </tr>
        <tr>
        <tr height="60px">
            <td class="bgWhite" colspan="2">
                <div style="margin-left: 300px"><asp:Button runat="server" ID="btn_SendPrize" Text="确定" Width="60px" Height="25px" OnClick="btn_SendPrize_Click"></asp:Button></div>
            </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
