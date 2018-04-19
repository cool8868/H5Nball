<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPrize.aspx.cs" Inherits="Games.NBall.AdminWeb.Develop.TestArena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../style/style.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script></head>
<body>
    <form id="form1" runat="server">
        <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
            <tr class="bgLightGray">
                <td colspan="4">发奖
                </td>
            </tr>
            <tr>
                <td class="bgWhite" colspan="4" style="color: Red;">
                    <div class="errorMessage" style="padding: 0px;">
                        <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="bgColor2" style="width: 118px">选择区：</td>
                <td class="bgWhite" style="width: 260px" colspan="3">
                    <asp:DropDownList runat="server" ID="ddlZone" /></td>
            </tr>
            <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">竞技场</td>
                <td class="bgWhite" colspan="3">
                    <asp:Button ID="bt_ArenaAwary" runat="server" Text="竞技场后台发奖" OnClick="bt_ArenaAwary_Click" />
                </td>
            </tr>
            <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">天梯赛</td>
                <td class="bgWhite" colspan="3">
                    赛季：<asp:TextBox runat="server" ID="txtLadderSeason" Width="120px"></asp:TextBox><asp:Button ID="btnLadderPrize" runat="server" Text="天梯赛发奖" OnClick="btnLadderPrize_Click" />
                </td>
            </tr>
            <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">合区天梯赛</td>
                <td class="bgWhite" colspan="3">
                    赛季：<asp:TextBox runat="server" ID="TextBox1" Width="120px"></asp:TextBox><asp:Button ID="btnLadderPrizeMergeZone" runat="server" Text="合区天梯赛发奖" OnClick="btnLadderPrizeMergeZone_Click"  />
                </td>
            </tr>
            <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">群雄发奖</td>
                <td class="bgWhite" colspan="3">
                    群雄id：<asp:TextBox runat="server" ID="txtCrowdId" Width="120px"></asp:TextBox><asp:Button ID="btnCrowdPrize" runat="server" Text="群雄发奖" OnClick="btnCrowdPrize_Click" />
                </td>
            </tr>
            <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">群雄开启</td>
                <td class="bgWhite" colspan="3">
                    开始时间：<input style="width:150px" class="Wdate" type="text" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" runat="server" id="txtStartTime"/>
                    结束时间：<input style="width:150px" class="Wdate" type="text" onClick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" runat="server" id="txtEndTime"/>
                    <asp:Button ID="btnCrowdStart" runat="server" Text="群雄开启" OnClick="btnCrowdStart_Click" />
                </td>
            </tr>
            <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">跨服群雄发奖</td>
                <td class="bgWhite" colspan="3">
                    群雄id：<asp:TextBox runat="server" ID="txtCrossCrowdId" Width="120px"></asp:TextBox><asp:Button ID="btnCrossCrowdSendPrize" runat="server" Text="跨服群雄发奖" OnClick="btnCrossCrowdSendPrize_Click" />
                </td>
            </tr>

             <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">点球大战发奖</td>
                <td class="bgWhite" colspan="3">
                    第几赛季：<asp:TextBox runat="server" ID="txt_Season" Width="120px"></asp:TextBox>
                    <asp:Button ID="btn_AdPrize" runat="server" Text="点球大战发奖" OnClick="btn_AdPrize_Click" />
                </td>
            </tr>
            
            <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">跨服天梯赛发奖</td>
                <td class="bgWhite" colspan="3">
                    赛季：<asp:TextBox runat="server" ID="txtCrossLadderSeason" Width="120px"></asp:TextBox>Domain：<asp:TextBox runat="server" ID="txtDomain" Width="120px"></asp:TextBox><asp:Button ID="btnCrossLadderPrize" runat="server" Text="跨服天梯赛发奖" OnClick="btnCrossLadderPrize_Click" />
                </td>
            </tr>
             <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">跨服天梯赛补发天梯币</td>
                <td class="bgWhite" colspan="3">
                    <asp:Button ID="Button1" runat="server" Text="补发天梯币" OnClick="btnCrossLadderCoin_Click" />
                </td>
            </tr>
            
            
             <tr class="bgColor2">
                <td class="bgColor2" style="width: 118px">精彩活动发奖</td>
                <td class="bgWhite" colspan="3">
                    <asp:Button ID="btn_activityex" runat="server" Text="精彩活动发奖" OnClick="btn_activityex_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
