using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.AdminWeb.GmTools
{
    public partial class Announcement : System.Web.UI.Page
    {
        private readonly ManagerClient reader = new ManagerClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            //Master.SetMetaTable();
           
        }

        private void BindControl()
        {
        }

        protected void Btn1_GetAnnouncement(object sender, EventArgs e)
        {
            var pf = platform1.Text;
            if (string.IsNullOrEmpty(pf))
            {
                pf = "all";
            }
            try
            {
                  var Response = reader.GetPlatformAnnouncement(pf);
                if (Response != null)
                {
                    var list = Response.Data.AnnouncementList;
                    if (list != null && list.Count > 0)
                    {
                        datagrid.DataSource = list;
                        datagrid.DataBind();
                        ClearData();
                    }
                }
                
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
           
        }

        protected void Btn2_SetAnnouncement(object sender, EventArgs e)
        {
            var pf = platform2.Text;
            var isTops = isTop2.Text;
            bool isTop = isTops =="1";

            var title = title2.Text;
            var contentString = contentString2.Text;
            var startTime = startTime2.Text;
            var endTime = endTime2.Text;
            try
            {
                var time1 = DateTime.ParseExact(startTime, "yyyyMMddHHmmss",null);
                var time2= DateTime.ParseExact(endTime, "yyyyMMddHHmmss",null);
                var result = reader.SetPlatformAnnouncement(pf, isTop, title, contentString, time1, time2);
                ShowMessage(result ? "添加成功" : "添加失败");
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }


        }

        protected void Btn3_RanableAnnouncement(object sender, EventArgs e)
        {
            try
            {
                var idx = ConvertHelper.ConvertToInt(idx3.Text);
                var isTops = isTop3.Text;
                bool isTop = isTops =="1";

                var startTime = startTime3.Text;
                var endTime = endTime3.Text;
                var time1 = DateTime.ParseExact(startTime, "yyyyMMddHHmmss",null);
                var time2= DateTime.ParseExact(endTime, "yyyyMMddHHmmss",null);

               var result= reader.RanablePlatformAnnouncement(idx, isTop, time1, time2);
               ShowMessage(result ? "启用成功" : "启用失败");
            }
            catch (Exception ex)
            {
                
               ShowMessage(ex.Message);
            }
        }

        protected void Btn4_CloseAnnouncement(object sender, EventArgs e)
        {
            try
            {
                var idx=ConvertHelper.ConvertToInt(idx4.Text);
                var result = reader.ClosePlatformAnnouncement(idx);
                ShowMessage(result ? "关闭成功" : "关闭失败");
            }
            catch (Exception ex)
            {

                ShowMessage(ex.Message);
            }
        }

        protected void Btn5_DeleteAnnouncement(object sender, EventArgs e)
        {
            try
            {
                var idx = ConvertHelper.ConvertToInt(idx5.Text);
                var result = reader.DeleteAnnouncement(idx);
                ShowMessage(result ? "删除成功" : "删除失败");
            }
            catch (Exception ex)
            {

                ShowMessage(ex.Message);
            }
        }
        void ShowMessage(string msg)
        {
            ltlMessage.Text = msg;
        }

        private void ClearData()
        {
            idx3.Text = "";
            idx4.Text = "";
            idx5.Text = "";
            platform1.Text = "";
            platform2.Text = "";
            isTop2.Text = "";
            isTop3.Text = ""; 
            title2.Text = "";
            contentString2.Text = "";
            startTime2.Text = "";
            startTime3.Text = "";
            endTime2.Text = "";
            endTime3.Text = "";
          
        }
    }
}