<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestLogin.aspx.cs" Inherits="Games.NBall.AdminWeb.Develop.TestLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="position: absolute; padding: 10px; top: 15px;left: 10px ; width: 600px;height: 30px; border: 1px solid gray">
        Factory:<asp:DropDownList runat="server" ID="ddlFactory" OnSelectedIndexChanged="ddlFactory_SelectedIndexChanged" AutoPostBack="True"/>
        Platform:<asp:DropDownList runat="server" ID="ddlPlatform" OnSelectedIndexChanged="ddlPlatform_SelectedIndexChanged" AutoPostBack="True"/>
        Zone:<asp:DropDownList runat="server" ID="ddlZoneList" OnSelectedIndexChanged="ddlZoneList_SelectedIndexChanged" AutoPostBack="True"/>
    </div>
    <div style="position: absolute; padding: 10px; top: 70px;left: 10px ; width: 280px;height: 400px; border: 1px solid gray">
        TestLogin:<br/>
        <asp:Label ID="Label1" runat="server" Text="platform:"></asp:Label>
        <asp:TextBox ID="txtLoginPlatform" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label2" runat="server" Text="account:"></asp:Label>
        <asp:TextBox ID="txtLoginUsername" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label6" runat="server" Text="time:"></asp:Label>
        <asp:TextBox ID="txtLoginTime" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label9" runat="server" Text="timetick:"></asp:Label>
        <asp:TextBox ID="txtLoginTimeTick" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label4" runat="server" Text="address:"></asp:Label>
        <asp:TextBox ID="txtLoginAddress" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label5" runat="server" Text="loginkey:"></asp:Label>
        <asp:TextBox ID="txtLoginKey" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" />
    </div>
        <div style="position: absolute;padding: 10px;  top: 70px;left: 630px ; width: 280px;height: 400px; border: 1px solid gray">
            CheckActive:<br/>
            <asp:Label runat="server" Text="Server_id:"></asp:Label>
            <asp:TextBox ID="txtServer2" runat="server"></asp:TextBox><br />
            <asp:Label runat="server" Text="account:"></asp:Label>
            <asp:TextBox ID="txtUsername2" runat="server"></asp:TextBox><br />
            <asp:Label runat="server" Text="address:"></asp:Label>
            <asp:TextBox ID="txtAddress2" runat="server"></asp:TextBox><br />
            <asp:Button ID="btnCheckActive" runat="server" Text="激活check" OnClick="btnCheckActive_Click" /><br/>
            <asp:Label runat="server" ID="lblResult2"></asp:Label>
        &nbsp;
        </div>
        <div style="position: absolute;padding: 10px;  top: 70px;left: 320px ; width: 280px;height: 400px; border: 1px solid gray">
            TestCharge:<br/>
            <asp:Label ID="Label7" runat="server" Text="platform:"></asp:Label>
            <asp:TextBox ID="txtChargePlatform" runat="server"></asp:TextBox><br />
            <asp:Label  runat="server" Text="account:"></asp:Label>
            <asp:TextBox ID="txtChargeUsername" runat="server"></asp:TextBox><br />
            <asp:Label ID="Label10"  runat="server" Text="server_id:"></asp:Label>
            <asp:TextBox ID="txtCharegeServerId" runat="server"></asp:TextBox><br />
            <asp:Label ID="Label8" runat="server" Text="time:"></asp:Label>
            <asp:TextBox ID="txtChargeTime" runat="server"></asp:TextBox><br />
            <asp:Label  runat="server" Text="金额:"></asp:Label>
            <asp:TextBox ID="txtChargeCash" runat="server"></asp:TextBox><br />
            <asp:Label  runat="server" Text="订单号:"></asp:Label>
            <asp:TextBox ID="txtChargeOrderId" runat="server"></asp:TextBox><br />
            <asp:Label  runat="server" Text="address:"></asp:Label>
            <asp:TextBox ID="txtChargeAddress" runat="server"></asp:TextBox><br />
            <asp:Label ID="Label3" runat="server" Text="chargeKey:"></asp:Label>
            <asp:TextBox ID="txtChargeKey" runat="server"></asp:TextBox><br />
            <asp:Button ID="btnCharge" runat="server" Text="充值" OnClick="btnCharge_Click" /> 
            <br/>
            <asp:Label runat="server" ID="lblResult3"></asp:Label>
        </div>
    </form>
</body>
</html>
