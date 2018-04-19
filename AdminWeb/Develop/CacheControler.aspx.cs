using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Common;
using Games.NBall.WebServerFacade;
using Games.NBall.WebServerFacade.NwWebService;

namespace Games.NBall.AdminWeb.Develop
{
    public partial class CacheControler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminMgr.BindZoneControl(HttpContext.Current, ddlZone, this.User.Identity.Name);
                AdminMgr.BindDdlControl(ddlCacheType, "EnumCacheType");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            try
            {
                var zone = AdminMgr.GetSelectZoneId(HttpContext.Current,ddlZone);
                var cacheType = AdminMgr.GetSelectInt(ddlCacheType);
                var s = WebServiceFactory.GetWebService(zone).ResetCache(cacheType);
                ShowMessage(s);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage("local exception:" + ex.Message);
            }
        }

        void ShowMessage(string msg)
        {
            ltlMessage.Text += string.Format("<div>{0}</div>", msg);
        }
    }
}