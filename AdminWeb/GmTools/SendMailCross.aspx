<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMailCross.aspx.cs" Inherits="Games.NBall.AdminWeb.GmTools.SendMailCross" %>
<%@ Register TagPrefix="uc1" TagName="AccountSelectControl" Src="~/UserControls/AccountSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" Namespace="Games.NBall.AdminWeb.UserControls" Assembly="Games.NBall.AdminWeb" %>

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
            <td colspan="2">发送邮件-跨服
            </td>
        </tr>
        <tr>
        <td class="bgWhite" style="color: Red;" colspan="2">
                        <div class="errorMessage" style="padding: 0px;">
                            <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
        </td></tr>
        
        <tr >
            <td class="bgColor2" style="width: 118px">
                收件人：
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtUserList" Width="978px" ></asp:TextBox>
                <div>多个收件人(所属区,登录账号)用竖线分隔</div>
            </td>
            </tr>
        <tr >
            <td class="bgColor2" style="width: 118px">
                标题：
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtMailTitle"></asp:TextBox>
            </td>
            </tr>
        <tr>
            <td class="bgColor2" style="width: 118px">
                内容：
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txtMailContent" Height="100px" TextMode="MultiLine" Width="594px"></asp:TextBox>
            </td>
            </tr>
        <tr >
            <td class="bgColor2" style="width: 118px">
                附件：
            </td>
            <td class="bgWhite">
                    附件类型：<asp:DropDownList runat="server" ID="ddlType" Width="80px"/>
                    数量：<asp:TextBox runat="server" ID="txtCount" Width="60px"></asp:TextBox>
                    物品编码：<asp:TextBox runat="server" ID="txtItemCode" Width="60px"></asp:TextBox>
                    强化级别：<asp:TextBox runat="server" ID="txtStrength" Width="60px"></asp:TextBox>
                    <asp:CheckBox runat="server" ID="chkBinding" Text="绑定" Checked="True"/>
                    <asp:Button runat="server" ID="btnAttachment" Text="添加附件" OnClick="btnAttachment_Click"/>
            </td>
            </tr>
        <tr >
            <td class="bgColor2" style="width: 118px">
                附件列表：
            </td>
            <td class="bgWhite">
                <asp:DataGrid runat="server" ID="datagrid1" Width="400px" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                            <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                            <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                            <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                            <Columns>
                               <asp:BoundColumn HeaderText="类型" DataField="TypeStr" />
                                <asp:BoundColumn HeaderText="名称" DataField="Name" />
                                <asp:BoundColumn HeaderText="数量" DataField="Count" />
                                <asp:BoundColumn HeaderText="强化级别" DataField="Strength" />
                                <asp:BoundColumn HeaderText="是否绑定" DataField="IsBinding" />
                            </Columns>
               </asp:DataGrid>
            </td>
            </tr>
        <tr height="60px">
            <td class="bgWhite" colspan="2">
                <div style="margin-left: 300px"><asp:Button runat="server" ID="btnSend" Text="发送" Width="60px" Height="25px" OnClick="btnSend_Click"></asp:Button></div>
            </td>
            </tr>
        </table>
        <table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
             <tr class="bgLightGray">
            <td colspan="2">跨服发点卷
            </td>
        </tr>
        <tr>
        <td class="bgWhite" style="color: Red;" colspan="2">
                        <div class="errorMessage" style="padding: 0px;">
                            <asp:Literal ID="Literal1" runat="server" EnableViewState="False"></asp:Literal>
                        </div>
        </td></tr>
        
            <tr>
                 <td class="bgColor2" style="width: 118px">
                标题：
            </td>
                <td class="bgWhite">
                    <asp:TextBox runat="server" ID="txt_MailName"></asp:TextBox>
                </td>
            </tr>
            <tr>
                 <td class="bgColor2" style="width: 118px">
                内容：
            </td>
                <td class="bgWhite">
                   <asp:TextBox runat="server" ID="txt_mailInfo" Height="100px" TextMode="MultiLine" Width="594px"></asp:TextBox>
                </td>
            </tr>
            <tr>
             <td class="bgColor2" style="width: 118px">
               发送串：SiteId,Account,PointNumber|SiteId,Account,PointNumber
            </td>
            <td class="bgWhite">
                <asp:TextBox runat="server" ID="txt_SendString" Height="100px" TextMode="MultiLine" Width="594px"></asp:TextBox>
            </td>
            </tr>
            <tr height="60px">
            <td class="bgWhite" colspan="2">
                <div style="margin-left: 300px"><asp:Button runat="server" ID="btn_SendMail" Text="发送" OnClick="btnSendMail_Click"/></div>
            </td>
            </tr>
        </table>
    </form>
</body>
</html>