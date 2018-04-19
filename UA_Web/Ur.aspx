<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ur.aspx.cs" Inherits="UA_Web.Ur" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>热血11人</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="swfobject.js"></script>
    <style type="text/css">
	    <!--
		    html, body { height:100%; overflow:hidden; }
		    body { margin:0; }
	    -->
    </style>
</head>
    <body>
	<div class="player">
		<div id="flashcontent">	
				<h1>Please update your flash player</h1>
				<p><a href="http://www.adobe.com/go/getflashplayer"><img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" /></a></p>
		</div>
		<script type="text/javascript">
		    // <![CDATA[
		    var flashvars = {
		        cdn: "<%=Cdn%>",
		        version: "<%=Version%>",
		        navSiteId:"<%=NavSiteId%>",
		        navArgs: "<%=NavArgs%>",
		        navApi: "<%=NavApiUrl%>"
		    };
		    var params = {
		        menu: "false",
		        scale: "noScale",
		        allowFullscreen: "true",
		        allowScriptAccess: "always",
		        bgcolor: "#000000",
		        bgcolor: "",
		        wmode: "window"
		    };
		    var attributes = {
		        id: "ChooseServer"
		    };

		    function reloadWindow() {
		        location.reload();
		    }
		    function thisMovie(movieName) {
		        if (navigator.appName.indexOf("Microsoft") != -1) {
		            return window[movieName];
		        } else {
		            return document[movieName];
		        }
		    }
		    function goto(url) {
		        top.location.href = url;
		    }
		    swfobject.embedSWF("<%=Cdn%>ChooseServer.swf?v=" + flashvars.version, "flashcontent", "100%", "100%", "10.0.0", "expressInstall.swf", flashvars, params, attributes);
		    // ]]>
		</script>
	</div>
</body>
</html>