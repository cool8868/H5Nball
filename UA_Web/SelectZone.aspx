<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectZone.aspx.cs" Inherits="UA_Web.ChooseSuit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>热血球球3</title>


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="text" value="1111" />
        <input type="button" value="btn" onclick="getSelectZoneId(1004)"/>
    </div>
    </form>
    <script src="jquery-1.7.1.min.js"></script>
<%--        <script>
            function getSelectZoneId(id) {
                var url = '<%=SelectZoneId()%>';
                var urlList = url.split('|');
                var zoneInfoArrary = [];
                for (var i = 0; i < urlList.length; i++) {
                    var zoneId = urlList[i].split(',')[0];
                    var zoneUrl = urlList[i].split(',')[1];
                    zoneInfoArrary[zoneId] = zoneUrl;
                }
                alert(zoneInfoArrary[id]);
                location.href = zoneInfoArrary[id];
            }

            function getPlant() {
                var plant = '<%=GetPlant%>';
               return plant;
           }
        </script>--%>

</body>
</html>
