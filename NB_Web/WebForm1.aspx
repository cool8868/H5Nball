<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Games.NBall.NB_Web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="js/jquery-2.1.1.min.js"></script>
    <script type="text/javascript">
        $(function () {
            GetCard();
           

            CreateTB();

           

        })

        function CreateTB() {
            var str = "";
            for (var i = 0; i < 4; i++) {
                str += "<tr >";
                for (var j = 0; j < 4; j++) {
                    str += "<td  pos='" + (i * 4 + j) + "' style=\"width:100px;height:100px;\">翻牌</td>";
                }
                str += "</tr>";

            }
            $('#tb').html(str);
            //翻牌
            $('#tb').find("td").click(function () {
                var aa = $(this);
                $.ajax({
                    type: "GET",
                    async: false,
                    url: "http://localhost:9000/nda.do?action=co&&pos=" + $(this).attr('pos'),
                    success: function (data) {
                        eval("var list=" + data);
                        aa.html(list.Data.CardCode)
                        if (list.PR != null) {
                            alert(list.PR);
                        }

                    }
                })

                GetCard();
            })
        }

        
        //重置卡牌
        function resetcard() {
            $.ajax({
                type: "GET",
                url: "http://localhost:9000/nda.do?action=ra",
                success: function (data) {
                    GetCard();
                }
            })
        }

        //获取当前状态
        function GetCard() {
            $.ajax({
                type: "GET",
                url: "http://localhost:9000/nda.do?action=gs",
                success: function (data) {
                    eval("var list=" + data);
                    CreateTB();
                    for (var i = 0; i < list.Data.length; i++) {
                       
                        $('#tb').find("td").each(function () {
                            if ($(this).attr("pos") == list.Data[i].Position && list.Data[i].Status == 1) {
                                $(this).html(list.Data[i].CardCode);
                            } 
                        })
                    }
                }
            })
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="1" id="tb">
    </table>
    Bonus:<table border="1">
        <tr style="width:100px"><td  style="width:100px">?</td><td  style="width:100px">?</td><td><input type="button" onclick="" value="重置bonus" /><input type="button" onclick="" value="确定" /></td></tr>
    </table>

     ID:<input type="text"  value="111111111111"/><input type="button" onclick="resetcard()" value="重置卡牌" />
    </div>
    </form>
</body>
</html>

