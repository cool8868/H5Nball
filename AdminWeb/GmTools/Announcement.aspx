<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Announcement.aspx.cs" Inherits="Games.NBall.AdminWeb.GmTools.Announcement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head >
<body runat="server">
<table class="bgDark" cellspacing="1" cellpadding="2" border="0" style="width: 100%;">
    <tr><td colspan="6"></td></tr>
    <tr>
        <td>1、公告查询</td>
    </tr>
    <tr>
       <td  style="width: 140px">平台简称（可空）</td> 
        <td  style="width: 140px"><asp:TextBox type="text" ID="platform1" runat="server"/></td>
        <td><asp:button onclick="Btn1_GetAnnouncement" runat="server" Text="查询公告"></asp:button></td>
    </tr>
         <tr><td colspan="6"></td></tr>
           <tr>
        <td>2、新增公告</td>
    </tr>
             <tr>
       <td>平台简称（可空）</td> <td><asp:TextBox type="text" ID="platform2" style="height: 19px" runat="server"/></td>
          <td>是否置顶(0不置顶，1为置顶)</td><td> <asp:TextBox  ID="isTop2"  runat="server"/></td>
         <td>公告消息头</td> <td><asp:TextBox type="text" ID="title2" runat="server"/></td>
    </tr>
    <tr>
        <td>公告内容</td> <td colspan="5"><asp:TextBox  ID="contentString2" style="width: 100%" runat="server"/></td>
    </tr>
     <tr>
        <td>时间格式（年月日时分秒）：20160602130030 </td>
       <td> 公告开始时间</td> <td><asp:TextBox type="text" ID="startTime2" runat="server"/></td>
        <td>公告结束时间</td> <td><asp:TextBox type="text" ID="endTime2" runat="server"/></td>
        <td><asp:button onclick="Btn2_SetAnnouncement" runat="server" Text="提交新增公告"></asp:button></td>
    </tr>
         <tr><td colspan="6"></td></tr>
          <tr><td>3、启用公告</td></tr>

    <tr>
       <td>公告编号</td> <td><asp:TextBox type="text" ID="idx3" runat="server"/></td>
          <td>是否置顶(0不置顶，1为置顶)</td><td> <asp:TextBox  ID="isTop3" runat="server" /></td>
         
    </tr>
     <tr>
                 <td>时间格式（年月日时分秒）：20160602130030 </td>
       <td> 公告开始时间</td> <td><asp:TextBox type="text" ID="startTime3" runat="server"/></td>
         <td>公告结束时间</td> <td><asp:TextBox type="text" ID="endTime3" runat="server"/></td>
          <td><asp:button onclick="Btn3_RanableAnnouncement" runat="server" Text="确定启用"></asp:button></td>
    </tr>
         <tr><td colspan="6"></td></tr>
          <tr><td>4、关闭公告</td></tr>
    <tr>
     <td>公告编号</td> <td><asp:TextBox type="text" ID="idx4" runat="server"/></td>
     <td><asp:button onclick="Btn4_CloseAnnouncement" runat="server" Text="确认关闭"></asp:button></td>
        </tr>
    
         <tr><td colspan="6"></td></tr>
          <tr><td>5、删除公告</td></tr>
    <tr>
     <td>公告编号</td> <td><asp:TextBox type="text" ID="idx5" runat="server"/></td>
     <td><asp:button onclick="Btn5_DeleteAnnouncement" runat="server" Text="确认删除"></asp:button></td>
        </tr>
        <tr><td colspan="6"></td></tr>
    <tr><td colspan="6">  <asp:Literal ID="ltlMessage" runat="server" EnableViewState="False"></asp:Literal></td></tr>
</table>
   
                          
                        
     <asp:DataGrid runat="server" ID="datagrid" Width="100%" CssClass="bgDark" CellPadding="4" CellSpacing="1" AutoGenerateColumns="False">
                            <HeaderStyle ></HeaderStyle>
                            <ItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#FFFFFF"></ItemStyle>
                            <AlternatingItemStyle CssClass="trList" HorizontalAlign="Center" BackColor="#EEEEEE"></AlternatingItemStyle>
                            <Columns>
                                <asp:BoundColumn DataField="Idx" HeaderText="公告编号" />
                                <asp:BoundColumn DataField="Platform" HeaderText="平台" />
                                <asp:BoundColumn DataField="IsTop" HeaderText="是否置顶" />
                                <asp:BoundColumn DataField="Title" HeaderText="公告消息头" />
                                <asp:BoundColumn DataField="ContentString" HeaderText="公告内容" />
                                <asp:BoundColumn DataField="StartTime" HeaderText="开始时间" />
                                 <asp:BoundColumn DataField="Endtime" HeaderText="结束时间" />
                                <asp:BoundColumn DataField="Rowtime" HeaderText="录入时间" />
                            </Columns>
                        </asp:DataGrid>
    
</body>
</html>
