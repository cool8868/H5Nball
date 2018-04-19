<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountSelectControl.ascx.cs" Inherits="Games.NBall.AdminWeb.UserControls.AccountSelectControl" %>
<%@ Register Src="~/UserControls/ZoneSelectControl.ascx" TagPrefix="uc1" TagName="ZoneSelectControl" %>
<div style="width: 90%;height: 40px; vertical-align: central">
    <table style="margin: 5px 0 0 0; height: 30px;">
        <tr>
            <td width="10px">&nbsp;</td>
            <td width="70px">所属区：</td>
            <td width="80px"><uc1:ZoneSelectControl ID="ZoneControl1" runat="server"/></td>
            <td>账号：</td>
            <td><asp:TextBox runat="server" ID="txtAccount" Width="100"></asp:TextBox></td>
            <td>经理名：</td>
            <td><asp:TextBox runat="server" ID="txtManagerName" Width="100"></asp:TextBox></td>
            <td>经理id：</td>
            <td><asp:TextBox runat="server" ID="txtManagerId" Width="240"></asp:TextBox></td>
            <td><asp:Button ID="btnAccountSearch" runat="server" Text="查询" OnClick="btnAccountSearch_Click"/>
                <asp:Button ID="btnAccountClear" runat="server" Text="清除" OnClientClick="doclear()" OnClick="btnAccountClear_Click"/>
            </td>
        </tr>
        <tr>
        <td class="bgWhite" style="color: Red;" colspan="10">
                        <div class="errorMessage" style="padding: 0px;">
                            <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
        </td></tr>
    </table>
    
</div>

