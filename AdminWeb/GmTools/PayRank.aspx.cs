using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.Bll;
using Games.NBall.Common;

namespace Games.NBall.AdminWeb.GmTools
{
    public partial class PayRank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminMgr.BindZoneControl(HttpContext.Current, ddlZone, this.User.Identity.Name, true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindRankDetails();
        }

        private static int _index = 0;
        void ShowMessage(string msg)
        {
            _index++;
            ltlMessage.Text = "(序列:" + _index + ")" + msg;
        }

        void BindRankDetails()
        {
            try
            {
                var zone = AdminMgr.GetSelectZoneId(HttpContext.Current,ddlZone);
                var payList = PayUserMgr.GetPayList(zone);
                rank.DataSource = payList;
                rank.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }

        }
    }
}