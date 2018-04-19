<%@ Page Title="" Language="C#" MasterPageFile="~/Account/AccountPage.Master" AutoEventWireup="true" CodeBehind="AccountInfo.aspx.cs" Inherits="Games.NBall.AdminWeb.Account.AccountInfo" %>

<%@ MasterType VirtualPath="~/Account/AccountPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
                <tr class="bgLightGray">
                    <td colspan="6">基本信息
                    </td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">经理名：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblName" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">头像：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblLogo" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">账号状态：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">等级：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblLevel" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">经验：</td>
                    <td class="bgWhite" colspan="3">
                        <asp:Label ID="lblExp" runat="server"></asp:Label></td>

                </tr>
                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">充值金额：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblCash" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">剩余点券：</td>
                    <td class="bgLightGreen">
                        <asp:Label ID="lblPoint" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">Vip等级：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblVipLevel" runat="server"></asp:Label></td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">剩余金币：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblCoin" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">剩余阅历：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblSophisticate" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">剩余灵气：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblReiki" runat="server"></asp:Label></td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">替补席数：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblSubstitute" runat="server"></asp:Label></td>

                    <td class="bgColor2" style="width: 118px">剩余体力：</td>
                    <td class="bgWhite" colspan="3">
                        <asp:Label ID="lblStamina" runat="server"></asp:Label></td>
                </tr>
                <tr class="bgLightGray">
                    <td colspan="6">登录信息</td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">在线状态：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblOnlineStatus" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">总在线时长：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblOnlineTotal" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">今日在线时长：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblOnlineCur" runat="server"></asp:Label></td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">注册时间：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblRegisterTime" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">最近登录：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblLastLoginTime" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">连续登录天数：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblLoginday" runat="server"></asp:Label></td>
                </tr>
                <tr class="bgLightGray">
                    <td colspan="6">阵型信息</td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">当前阵型：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblSolution" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">球员串：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblSolutionPlayer" runat="server"></asp:Label></td>
                    <td class="bgColor2" style="width: 118px">技能串：</td>
                    <td class="bgWhite">
                        <asp:Label ID="lblSolutionSkill" runat="server"></asp:Label></td>
                </tr>
                <tr class="bgLightGray">
                    <td colspan="6">球员信息</td>
                </tr>
                <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">球员数量：</td>
                    <td colspan="5" class="bgWhite">
                        <asp:Label ID="lblTeammemberCount" runat="server"></asp:Label></td>
                </tr>
             <tr style="height: 25px">
                    <td class="bgColor2" style="width: 118px">邀请的经理：</td>
                    <td colspan="5" class="bgWhite">
                        <asp:Label ID="lblFriendInvite" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="6" class="bgWhite">
                        <asp:DataGrid runat="server" ID="datagrid1" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False" OnItemDataBound="DataGrid1_ItemDataBound">
                            <HeaderStyle CssClass="bgColor2"></HeaderStyle>
                            <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                            <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                            <Columns>
                                <asp:BoundColumn DataField="PlayerId" HeaderText="pid" />
                                <asp:BoundColumn DataField="Name" HeaderText="球员名" />
                                <asp:BoundColumn DataField="Level" HeaderText="等级" />
                                <asp:BoundColumn DataField="Strength" HeaderText="强化等级" />
                                <asp:BoundColumn DataField="PropertyPoint" HeaderText="未分配属性点" />
                                <asp:BoundColumn DataField="IsMain" HeaderText="主力" />
                                <asp:TemplateColumn>
                                    <HeaderTemplate>副卡</HeaderTemplate>
                                    <ItemTemplate><asp:Label ID="lblPlayerCard" runat="server" Text="副卡"></asp:Label></ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>装备</HeaderTemplate>
                                    <ItemTemplate><asp:Label ID="lblEquipment" runat="server" Text="装备"></asp:Label></ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:HyperLinkColumn DataTextField="Idx" HeaderText="Tid" NavigateUrl="TeammemberInfo.aspx"/>

                            </Columns>
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>

</asp:Content>
