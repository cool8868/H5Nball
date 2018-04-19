using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Common;

namespace Games.NBall.AdminWeb.Develop
{
    public partial class TxData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        //    try
        //    {
        //        var list = Bll.TxRelogindataMgr.GetByDate(DateTime.Today);
        //        WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        //        foreach (var entity in list)
        //        {
        //            WriteLine(string.Format("{0:yyyyMMdd}:{1}--{2}", entity.RecordDate, entity.InstallCount, entity.RecordDate == DateTime.Today ? "" : entity.ReLoginCount.ToString()));                   
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("cause error....");
        //        LogHelper.Insert(ex);
        //    }
        //    Response.End();
        //}

        //void WriteLine(string msg)
        //{
        //    Response.Write("<div>"+msg+"</div>");
        }
    }
}