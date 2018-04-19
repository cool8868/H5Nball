using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Common;
using Games.NBall.Entity;

namespace Games.NBall.AdminWeb.Statistics
{
    public partial class StatisticsZone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminMgr.BindZoneControl(HttpContext.Current, ddlZone, this.User.Identity.Name, true);
                txtStartTime.Value = DateTime.Today.AddDays(-5).ToShortDateString();
                txtEndTime.Value = DateTime.Today.ToShortDateString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var zone = AdminMgr.GetSelectZoneIdInt(HttpContext.Current, ddlZone);
                var starTime = Convert.ToDateTime(txtStartTime.Value);
                var endTime = Convert.ToDateTime(txtEndTime.Value);
                var list = StatisticKpiMgr.GetbyDate(zone, starTime, endTime);
                if (list != null && list.Count > 0)
                {
                    var total = new StatisticKpiEntity();
                    total.RecordDateStr = "合计";
                    foreach (var entity in list)
                    {
                        total.Dau += entity.Dau;
                        total.DNewUser += entity.DNewUser;
                        total.DNewManager += entity.DNewManager;
                        total.PayTotal += entity.PayTotal;
                        total.Pcu += entity.Pcu;
                        total.Acu += entity.Acu;
                        total.PointRemain += entity.PointRemain;
                        total.PointConsume += entity.PointConsume;
                        total.PointCirculate += entity.PointCirculate;
                        total.RecordDate = DateTime.MinValue;
                    }
                    list.Add(total);
                }
                datagridzone.DataSource = list;
                datagridzone.DataBind();
                ltlMessage.Text = "";
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        private static int _index = 0;
        void ShowMessage(string msg)
        {
            _index++;
            ltlMessage.Text = "(序列:" + _index + ")" + msg;
        }

        protected void datagridzone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}