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
    public partial class StatisticsDayInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //StatisticThread.Instance.GetKpi(DateTime.Now,DateTime.Now);
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
                    for (int i = 0; i < list.Count; i++)
                    {

                        list[i].RetentionPercent2 = list[i].Retention2.ToString() + "  " + GetPercent(list[i].Retention2, i, list);
                        list[i].RetentionPercent3 = list[i].Retention3.ToString() + "  " + GetPercent(list[i].Retention3, i, list);
                        list[i].RetentionPercent4 = list[i].Retention4.ToString() + "  " + GetPercent(list[i].Retention4, i, list);
                        list[i].RetentionPercent5 = list[i].Retention5.ToString() + "  " + GetPercent(list[i].Retention5, i, list);
                        list[i].RetentionPercent6 = list[i].Retention6.ToString() + "  " + GetPercent(list[i].Retention6, i, list);
                        list[i].RetentionPercent7 = list[i].Retention7.ToString() + "  " + GetPercent(list[i].Retention7, i, list);
                        list[i].RetentionPercent15 = list[i].Retention15.ToString() + "  " + GetPercent(list[i].Retention7, i, list);
                        list[i].RetentionPercent30 = list[i].Retention30.ToString() + "  " + GetPercent(list[i].Retention7, i, list);


                   
                    }
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
                datagrid2.DataSource = list;
                datagrid2.DataBind();
                ltlMessage.Text = "";
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        string GetPercent(int retentionValue, int compareIndex, List<StatisticKpiEntity> list)
        {
            if (compareIndex < 0 || list[compareIndex].DNewUser == 0)
            {
                return "";
            }
            else
            {
                return string.Format("{0:P}", retentionValue * 1.00 / list[compareIndex].DNewUser);
            }
        }

        private static int _index = 0;
        void ShowMessage(string msg)
        {
            _index++;
            ltlMessage.Text = "(序列:" + _index + ")" + msg;
        }

        protected void datagrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            var command = e.CommandName;
            switch (command)
            {
                case "detail":
                    var date = e.Item.Cells[0].Text;
                    if (date != "合计")
                    {
                        BindDetail(Convert.ToDateTime(date));
                    }

                    break;

            }
        }

        void BindDetail(DateTime recordDate)
        {
            try
            {
                var list = StatisticKpiMgr.GetbyDate(1, recordDate, recordDate);
                datagrid3.DataSource = list;
                datagrid3.DataBind();
                ltlMessage.Text = "";
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

    }
}