using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.NB_Web.Command;
using Games.NBall.UAFacade;

namespace Games.NBall.NB_Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var r = ShareUtil.ZoneName;
            var rr = r;
           var rrr = rr;
            //Test1();
        }

        private void Test1()
        {
           
        }

        private void Testt2()
        {
            var qqOpenid = "B04DBBF689C75926C62D687B27C61F98";
            var qqOpenkey = "C985F772E2B10317DADCE0E83A8EED89";
            var qqPf = "wanba_ts";
            var platform = "ios";
            var url = AppsettingCache.Instance.GetAppSetting(EnumAppsetting.TxWb_FindVipUrl); //txh5_a8查询达人接口 
            var str = "";
            str = "qqopenid=" + qqOpenid + "&qqopenkey=" + qqOpenkey + "&qqpf=" + qqPf + "&platform=" + platform;
            var result = UAHelper.HttpPost(url, str);
            //{'ret':'success', 'code':'0', 'message':'', 'data': {"is_vip":"1", "vip_level": "8", "score": "1000", "expiredtime": "1407312182"}}
            if (!string.IsNullOrEmpty(result))
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                try
                {
                    var json = serializer.Deserialize<TxVipJsonResult>(result);


                    if (json != null)
                    {
                        var ret = json.ret;
                        if (string.IsNullOrEmpty(ret) || ret == "fail")
                        {
                            return;
                        }
                        else if (ret == "success")
                        {

                            var json2 = serializer.Deserialize<InnerData>(json.data);

                            bool flag = json2.is_vip.ToLower() == "true";
                            int vipLevel = (int)ConvertHelper.ConvertToDouble(json2.vip_level);
                            var str1 = json2.score + "|" + json2.expiredtime;
                            var f = flag;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("{\"ret\":\"fail\",\"msg\":\"" + ex.Message + "}");
                }
            }

        }
    }
}