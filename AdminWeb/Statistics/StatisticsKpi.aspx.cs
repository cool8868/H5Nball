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
    public partial class StatisticsKpi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //StatisticThread.Instance.GetKpi(DateTime.Now,DateTime.Now);
                //BindData();
                AdminMgr.BindZoneControl(HttpContext.Current, ddlZone, this.User.Identity.Name);
            }
        }
        public void SetSelectControlZone()
        {

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
                datagrid2.DataSource = list;
                
                datagrid2.DataBind();
                lblInviteNumber.Text = FriendinviteMgr.GetAllNumber(ddlZone.SelectedValue).ToString();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
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

        

        void OnloadZone()
        {

        }
    }
}