using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Common;

namespace Games.NBall.AdminWeb.Statistics
{
    public partial class StatisticsPay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //StatisticThread.Instance.GetKpi(DateTime.Now,DateTime.Now);
                AdminMgr.BindZoneControl(HttpContext.Current, ddlZone, this.User.Identity.Name);
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        void BindData()
        {
            try
            {
                var zone = AdminMgr.GetSelectZoneIdInt(HttpContext.Current, ddlZone);
                var startTime = DateTime.Today.AddDays(-15);
                var endTime = DateTime.Today;
                var list = StatisticKpiMgr.GetbyDate(zone, startTime, endTime);
                datagrid1.DataSource = list;
                datagrid1.DataBind();

                var list1 = PayUserMgr.GetPayList(zone.ToString());
                payRank.DataSource = list1;
                payRank.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }

        }
    }
}