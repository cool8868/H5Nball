using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Common;

namespace Games.NBall.AdminWeb.Develop
{
    public partial class TestLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtLoginTimeTick.Text = AdminMgr.DateTime2UnixTimeStamp(DateTime.Now).ToString("f0");
                txtLoginTime.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
                txtChargeTime.Text = DateTime.Now.ToString("yyyyMMddHHmmss");

                var factorys = Bll.AllUafactoryMgr.GetAll();
                ddlFactory.DataSource = factorys;
                ddlFactory.DataTextField = "Code";
                ddlFactory.DataValueField = "Code";
                ddlFactory.DataBind();
                ddlFactory.SelectedIndex = 0;
                ddlFactory_SelectedIndexChanged(null,null);
                
            }
        }

        #region Login
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var uid = txtLoginUsername.Text;
            var time = txtLoginTime.Text;
            var timeTick = txtLoginTimeTick.Text;
            var cryptKey = txtLoginKey.Text;
            var platform = txtLoginPlatform.Text;
            //var 
            string paras = "";
            var factory = ddlFactory.SelectedValue;
            factory = factory.ToLower();
            switch (factory)
            {
                case "u17":
                    paras = BuildLoginU17(uid,  cryptKey,timeTick);
                    break;
                
                case "7k":
                    paras = BuildLogin7k7k(uid, timeTick, cryptKey);
                    break;
            }
            if (factory.Contains("gov"))
            {
                paras = BuildLoginGov(platform, uid, time, cryptKey);
            }
            string url = GetUrl(txtLoginAddress.Text) + "ul.aspx?" + paras;
            Response.Redirect(url);
            Response.End();
        }

        string BuildLoginGov(string platform,string uid,string time,string cryptKey)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("platform=").Append(platform).Append("&username=").Append(uid).Append("&time=").Append(time);
            string sig = AdminMgr.GetSHA1(sb.ToString() + "&key=" + cryptKey);
            string paras = sb.ToString() + "&sign=" + sig;
            return paras;
        }

        string BuildLoginU17(string uid,string cryptKey,string timeTick)
        {
            var server_id = "1";
            var openid = "U17";
            StringBuilder sb = new StringBuilder();
            sb.Append("uid=" + uid);
            sb.Append("&time=" + timeTick);
            sb.Append("&server_id="+server_id).Append("&openid="+openid);

            string signParam = uid + timeTick + server_id + openid + cryptKey;
            string sign = AdminMgr.GetMD5(signParam);
            sb.Append("&sign=").Append(sign);
            return sb.ToString();
        }

        string BuildLogin7k7k(string uid,string timetick,string cryptKey)
        {
            //s1.rxqq3.u17.com?uid=4211368&time=1400139100506&server_id=1&openid=U17&sign=58940646ba264e9b48d55c693f923aca
            var server_id = "1";
            StringBuilder sb = new StringBuilder();
            sb.Append("userid=" + uid).Append("&username="+uid);
            sb.Append("&time=" + timetick);
            sb.Append("&server_id=" + server_id).Append("&isAdult=" + 1).Append("&source=" + 1);

            string signParam = uid + uid + timetick + server_id + 1 + cryptKey;
            string sign = AdminMgr.GetMD5(signParam);
            sb.Append("&flag=").Append(sign);
            return sb.ToString();
        }
        #endregion

        #region CheckActive
        protected void btnCheckActive_Click(object sender, EventArgs e)
        {
            //s1.rxqq3.u17.com?uid=4211368&time=1400139100506&server_id=1&openid=U17&sign=58940646ba264e9b48d55c693f923aca
            var uid = txtUsername2.Text;
            var server_id = txtServer2.Text;
            var agentid = "U17";

            StringBuilder sb = new StringBuilder();
            sb.Append("uid=" + uid);
            sb.Append("&server_id=" + server_id).Append("&agentid=" + agentid);

            string url = GetUrl(txtAddress2.Text) + "ua.aspx?" + sb.ToString();
            Response.Redirect(url);
            Response.End();
        }
        #endregion

        #region Charge
        protected void btnCharge_Click(object sender, EventArgs e)
        {
            var uid = txtChargeUsername.Text;
            var cash = txtChargeCash.Text;
            var order_id ="adminTest"+ txtChargeOrderId.Text;
            var platform = txtChargePlatform.Text;
            var time = txtChargeTime.Text;
            var cryptKey = txtChargeKey.Text;
            var serverid = txtCharegeServerId.Text;

            string paras = "";
            var factory = ddlFactory.SelectedValue;
            factory = factory.ToLower();
            switch (factory)
            {
                case "u17":
                    paras = BuildChargeU17(serverid, uid,cash,order_id, time, cryptKey);
                    break;
                case "7k":
                    paras = BuildCharge7K7K(serverid, uid, cash, order_id, time, cryptKey);
                    break;
            }
            if (factory.Contains("gov"))
            {
                paras = BuildChargeGov(platform, uid, cash, order_id, time, cryptKey);
            }
            string url = GetUrl(txtChargeAddress.Text) + "uc.aspx?" + paras;
            Response.Redirect(url);
            Response.End();
        }
        
        string BuildChargeGov(string platform, string uid, string cash, string orderId, string time, string crptyKey)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("platform=").Append(platform).Append("&username=").Append(uid).Append("&money=").Append(cash)
            .Append("&orderid=").Append(orderId).Append("&time=").Append(time);
            string sig = AdminMgr.GetSHA1(sb.ToString() + "&key=" + crptyKey);
            string paras = sb.ToString() + "&sign=" + sig;
            return paras;
        }

        string BuildChargeU17(string server_id, string uid, string cash, string orderId, string time, string crptyKey)
        {
            //s1.rxqq3.u17.com?uid=4211368&time=1400139100506&server_id=1&openid=U17&sign=58940646ba264e9b48d55c693f923aca
            var openid = "U17";

            StringBuilder sb = new StringBuilder();
            sb.Append("uid=" + uid);
            sb.Append("&order_amount=" + cash);
            sb.Append("&order_id=" + orderId);
            sb.Append("&server_id=" + server_id).Append("&agentid=" + openid);

            string signParam = uid + cash +orderId+ server_id + crptyKey;
            string sign = AdminMgr.GetMD5(signParam);
            sb.Append("&sign=").Append(sign);
            return sb.ToString();
        }

        protected string BuildCharge7K7K(string server_id, string uid, string cash, string orderId, string time, string crptyKey)
        {
            //s1.rxqq3.u17.com?uid=4211368&time=1400139100506&server_id=1&openid=U17&sign=58940646ba264e9b48d55c693f923aca
            var point = ConvertHelper.ConvertToInt(cash)*10;

            StringBuilder sb = new StringBuilder();
            sb.Append("PayToUserId=" + uid);
            sb.Append("&PayToUser=" + uid);
            sb.Append("&PayMoney=" + cash);
            sb.Append("&PayNum=" + orderId);
            sb.Append("&server_id=" + server_id);
            sb.Append("&PayGold="+point);
            sb.Append("&PayTime=" + time);

            string signParam = orderId + "|" + uid + "|" + uid + "|" + cash + "|" + point + "|" + time + crptyKey;
            string sign = AdminMgr.GetMD5(signParam);
            sb.Append("&flag=").Append(sign);
            return sb.ToString();
        }
        #endregion

        private void doRequest(string url)
        {
            string strSb = "";
            try
            {
                byte[] data = System.Text.Encoding.GetEncoding("GB2312").GetBytes(url);
                // 准备请求... 
                HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);

                //设置超时
                req.Timeout = 30000;
                req.Method = "Post";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = data.Length;
                Stream stream = req.GetRequestStream();
                // 发送数据 
                stream.Write(data, 0, data.Length);
                stream.Close();

                HttpWebResponse rep = (HttpWebResponse) req.GetResponse();
                Stream receiveStream = rep.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("GB2312");
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, encode);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder sb2 = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    sb2.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }

                rep.Close();
                readStream.Close();
                strSb = sb2.ToString();
            }
            catch (Exception ex)
            {
                //LogManager.Error("Login", ex);
                Response.Write(ex.Message);
                Response.Write(ex.StackTrace);
                return;
            }
            if (strSb == "0")
            {
                var gourl = GetUrl(txtLoginAddress.Text) + "index.aspx";
                Response.Redirect("<script>window.top.location='" + gourl + "'</script>");
                Response.End();
            }
            else
            {
                Response.Write(strSb);
                Response.End();
            }
        }

        string GetUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (!url.EndsWith("/"))
                {
                    url = url + "/";
                }
            }
            return url;
        }

        protected void ddlFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var factory = ddlFactory.SelectedValue;
            var list = AllUaplatformMgr.GetByFactory(factory);
            ddlPlatform.DataSource = list;
            ddlPlatform.DataTextField = "PlatformCode";
            ddlPlatform.DataValueField = "PlatformCode";
            ddlPlatform.DataBind();
            if (list != null && list.Count > 0)
            {
                ddlPlatform.SelectedIndex = 0;
                ddlPlatform_SelectedIndexChanged(null, null);
            }
        }

        protected void ddlPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            var platform = ddlPlatform.SelectedValue;
            var list = AllZoneinfoMgr.GetByPlatform(platform);
            ddlZoneList.DataSource = list;
            ddlZoneList.DataTextField = "ZoneName";
            ddlZoneList.DataValueField = "Idx";
            ddlZoneList.DataBind();
            if (list != null && list.Count > 0)
            {
                ddlZoneList.SelectedIndex = 0;
                ddlZoneList_SelectedIndexChanged(null, null);
            }
        }

        protected void ddlZoneList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var zoneId = ddlZoneList.SelectedValue;
            var zoneCache = AllZoneinfoMgr.GetById(Convert.ToInt32(zoneId));
            if(zoneCache==null)return;
            var platform = AllUaplatformMgr.GetByCode(zoneCache.PlatformCode);
            if(platform==null)return;
            txtLoginAddress.Text = zoneCache.ApiUrl;
            txtLoginKey.Text = platform.LoginKey;
            txtLoginPlatform.Text = platform.PlatformCode;
            txtLoginUsername.Text = "dbbb";

            txtChargeAddress.Text = zoneCache.ApiUrl;
            txtChargeCash.Text = "1";
            txtChargeKey.Text = platform.ChargeKey;
            txtChargeOrderId.Text = string.Format("AdminTest@{0:yyyyMMddHHmmssfff}", DateTime.Now);
            txtChargePlatform.Text = platform.PlatformCode;
            txtChargeUsername.Text = "dbbb";
            txtCharegeServerId.Text = zoneCache.PlatformZoneName;
        }
    }
}
