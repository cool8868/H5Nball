using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Common;

namespace Games.NBall.AdminWeb.Statistics
{
    public partial class StatisticsInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStartTime.Value = DateTime.Today.AddDays(-5).ToShortDateString();
                txtEndTime.Value = DateTime.Today.ToShortDateString();
                //StatisticThread.Instance.GetKpi(DateTime.Now,DateTime.Now);
                AdminMgr.BindZoneControl(HttpContext.Current, ddlZone, this.User.Identity.Name, true);
                BindInfo();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        void BindInfo()
        {
            try
            {
                var list = StatisticInfoMgr.GetAll();
              
                datagrid1.DataSource = list;
                datagrid1.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }

        }

        void BindData()
        {
            try
            {
                var zone = AdminMgr.GetSelectZoneIdInt(HttpContext.Current, ddlZone);
                var starTime = Convert.ToDateTime(txtStartTime.Value);
                var endTime = Convert.ToDateTime(txtEndTime.Value);
                var list = StatisticKpiMgr.GetbyDate(zone, starTime, endTime);
                  for (int i = 0; i < list.Count; i++)
                  {
                      var c = list[i].CoinConsume;
                      list[i].CoinConsume = c*-1;
                  }
                datagrid2.DataSource = list;
                datagrid2.DataBind();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
        }
    }
}