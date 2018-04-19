using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.UAFacade;
using Games.NBall.NB_Web.BaseCommon;
using Games.NBall.WebClient.Weibo;

namespace Games.NBall.NB_Web
{
    public partial class Ul : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
                Games.NBall.UAFacade.UAFactory.Instance.Adapter.doLogin();
              
            //Response.AddHeader("P3P", "CP=CAO PSA OUR");
            //var host = Request.Url.Host;
            //var zoneName = UAFactory.Instance.ZoneName;
            //var zoneCache = CacheFactory.FunctionAppCache.GetZone("A8");
            //if (zoneCache == null)
            //{
            //    Response.Redirect("Error.aspx?Message=no zone config,zonename:" + zoneName);
            //    Response.End();
            //    return;
            //}
            //string api = zoneCache.ApiHost;
            //if (zoneCache.ApiHost.IndexOf(':') >= 0)
            //    api = zoneCache.ApiHost.Substring(0, zoneCache.ApiHost.IndexOf(':'));
            //if (host != api)
            //{
            //    var url = Request.Url.ToString();
            //    url = url.Replace(host, zoneCache.ApiHost);
            //    Response.Redirect(url);
            //    Response.End();
            //    return;
            //}
            
        }
    }
}