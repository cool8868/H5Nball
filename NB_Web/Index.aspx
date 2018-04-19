<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Games.NBall.NB_Web.Index" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
     <meta charset="utf-8">
    <title>热血足球III</title>
    <meta name="viewport" content="width=device-width,initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no"/>
    <meta name="apple-mobile-web-app-capable" content="yes"/>
    <meta name="full-screen" content="true"/>
    <meta name="screen-orientation" content="portrait"/>
    <meta name="x5-fullscreen" content="true"/>
    <meta name="360-fullscreen" content="true"/>
    <style>
        html, body {
            -ms-touch-action: none;
            background: #000000;
            padding: 0;
            border: 0;
            margin: 0;
            height: 100%;
        }
    </style>

    <!--这个标签为通过egret提供的第三方库的方式生成的 javascript 文件。删除 modules_files 标签后，库文件加载列表将不会变化，请谨慎操作！-->
    <!--modules_files_start-->
	<script egret="lib" src="libs/modules/egret/egret.min.js?v=0516a"></script>
	<script egret="lib" src="libs/modules/egret/egret.web.min.js?v=0516a"></script>
	<script egret="lib" src="libs/modules/game/game.min.js?v=0516a"></script>
	<script egret="lib" src="libs/modules/game/game.web.min.js?v=0516a"></script>
	<script egret="lib" src="libs/modules/tween/tween.min.js?v=0516a"></script>
	<script egret="lib" src="libs/modules/res/res.min.js?v=0516a"></script>
	<script egret="lib" src="libs/modules/jszip/jszip.min.js?v=0531"></script>
	<!--modules_files_end-->

    <!--这个标签为不通过egret提供的第三方库的方式使用的 javascript 文件，请将这些文件放在libs下，但不要放在modules下面。-->
    <!--other_libs_files_start-->
    <!--other_libs_files_end-->

    <!--这个标签会被替换为项目中所有的 javascript 文件。删除 game_files 标签后，项目文件加载列表将不会变化，请谨慎操作！-->
    <!--game_files_start-->
	<script src="main.min.js?v=0602C"></script>
	<!--game_files_end-->
	<script language="javascript">
	    function getQueryString(name) { 
	        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i"); 
	        var r = window.location.search.substr(1).match(reg); 
	        if (r != null) return unescape(r[2]); return null; 
	    } 
	</script>
	<!--第三方类库http://58.67.198.154:8080/csdkh5/h5/sdk/8030/h5_a8/js.htm-->
	<script type="text/javascript">
	    var webpath = getQueryString("pfurl")
	    document.write('<scr'+'ipt src="'+'<%=pfUrl%>'+ '"></scr'+'ipt>');
	</script>
	
</head>
<body>
<iframe id="gameFrame" src="" id="index" allowTransparency="true" width="100%" height="100%" scrolling="no" frameborder="0"></iframe>
<script>
    function getAccount() {
        var account = '<%=accountName%>';
        return account;
    }

    function getCookie() {
        var cookie = '<%=cookie%>';
        return cookie;
    }

    function getHost() {
        var Host = '<%=GetHost%>';
        return Host;
    }

    function getPlant() {
        var plant = '<%=GetPlant%>';
        return plant;
    }

    function getV() {
        var v1 = '<%=v%>';
        return v1;
    }

    function getZoneId() {
        var zoneId = '<%=zoneId%>';
        return zoneId;
    }
	
    function getServerInfo() {
        return '<%=skipUrl%>';
    }


    //var plant = "qq";
    plant = getPlant();
     var gameFrame = document.getElementById("gameFrame");
    //images.dingpamao.net/hb3
    //http://images.dingpamao.net/sjxg/football/index.html
     gameFrame.src = '<%=skipUrl%>';
 
</script>
<script language="javascript">
    var obj = {};
    obj.isDisplayShare = "show";
    obj.shareType = 2;
    if(window.platformApi){
        window.platformApi.init(obj);
			
        window.platformApi.onShareSuccess = function(type){  //分享成功回调
            ActivityRequest.sg(type);
        }
    }
		
	
    function pay(mark){
        window.platformApi.pay("",mark);
    }
		
    //
    function share(type){
        window.platformApi.share();
    }
	</script>

    <div style="margin: auto;width: 100%;height: 100%;" class="egret-player"
         data-entry-class="FootBall"
         data-orientation="auto"
         data-scale-mode="showAll"
         data-frame-rate="36"
         data-content-width="640"
         data-content-height="960"
         data-show-paint-rect="false"
         data-multi-fingered="2"
         data-show-fps="false" data-show-log="false"
         data-log-filter="" data-show-fps-style="x:0,y:0,size:30,textColor:0x00c200,bgAlpha:0.9">
    </div>
    <script>
        egret.runEgret({renderMode:"webgl"});
    </script>
</body>
</html>
