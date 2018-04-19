<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddGambleHost.aspx.cs" Inherits="Games.NBall.AdminWeb.GmTools.AddGambleHost" %>

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
            <td colspan="2">官方坐庄
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
            <td class="bgWhite"><asp:DropDownList ID="SZone" runat="server" Height="16px" Width="110px"></asp:DropDownList>
                <asp:CheckBox ID="chkAll" runat="server" Text="所有区" Checked="True" />
            </td>
            </tr>
        <tr >
            <td class="bgColor2" style="width: 118px">
                竞猜主题：
            </td>
            <td class="bgWhite">
                <div><asp:DropDownList ID="SZTitles" runat="server" Height="16px" Width="480px" AutoPostBack="True" OnSelectedIndexChanged="SZTitles_SelectedIndexChanged"></asp:DropDownList></div>
            </td>
            </tr>
        <tr>
            <td class="bgColor2" style="width: 118px">
                选项</td>
            <td class="bgWhite">
              
                <asp:BulletedList ID="blOptions" runat="server">
                </asp:BulletedList>
              
            </td>
            </tr>
        <tr>
            <td class="bgColor2" style="width: 118px">
                赔率</td>
            <td class="bgWhite">
                <asp:TextBox ID="txtRates" runat="server" Width="471px"></asp:TextBox>
            </td>
            </tr>
        
       <tr>
            <td class="bgColor2" style="width: 118px">
                提示：
            
            </td>
            <td class="bgWhite">
                <div>各赔率之间用&quot;|&quot;分隔，举例：1.88|2.88|5.88   欧洲杯不填</div>
            </td>
            </tr>
        <tr>
            <td class="bgColor2" style="width: 118px">
                奖池金额：
            
            </td>
            <td class="bgWhite">
                
                <asp:TextBox ID="txtMoney" runat="server"></asp:TextBox>
                
            </td>
            </tr>
       
        <tr height="60px">
            <td class="bgWhite" colspan="2">
                <div style="margin-left: 300px"><asp:Button runat="server" ID="btnSend" Text="确定" Width="60px" Height="25px" OnClick="btnSend_Click"></asp:Button>
                    <asp:Button runat="server" ID="btn2Send" Text="确定" Width="60px" Height="25px" OnClick="btnSend_Click"/>
                </div>

            </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
