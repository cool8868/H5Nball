<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="HTB.DevFx.Security.Web.Pages.IndexPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>统一开发框架</title>
    <script language="javascript" type="text/javascript">
		if(window.top != window.self) 
		{
			window.top.location.replace("./index.aspx");
		}
    </script>
       <link rel="stylesheet" type="text/css" href="resource/style/style.css" media="screen" />
</head>
	<frameset rows="33,*" cols="*" frameborder="xxx" border="1" framespacing="0" onbeforeunload="return window_onbeforeunload()">
		<frame id="frameTop" runat="server" src="main/top.aspx" name="devfxTopFrame" scrolling="no" noresize class="bottomLine" />
		<frameset rows="*" cols="160,*" framespacing="0" frameborder="xxx" border="1">
			<frame id="frameLeft" runat="server" src="main/left.aspx" name="devfxLeftFrame" class="rightLine" />
			<frame id="frameMain" runat="server" src="main/main.aspx" name="devfxMainFrame" />
		</frameset>
	</frameset>
