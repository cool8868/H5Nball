using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.AdminWeb.Develop;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.WebServerFacade;

namespace Games.NBall.AdminWeb
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TTest1();
            lblTest.Text = "1";
            try
            {
                
            }
            catch (Exception ex)
            {
                lblTest.Text = ex.Message;
            }
        }

        private void TTest1()
        {
            lblTest.Text = "1";
            try
            {
                var managerId = new Guid("2492CA9A-D856-4FF8-B807-A62600E5BEED");
               // var code = AdminMgr.AddItems("a8s2", managerId, 395003, 20, 0, false);
              
                //var list = AllZoneinfoMgr.GetAllForFactory();

                ////  var r = AdminMgr.AddItems(AllZoneinfoMgr.GetById(1004).ZoneName, new Guid("731DF1FF-B4ED-4932-989B-A60E01314832"), 120006, 1, 1, false);
                //var r = WebServerHandler.AddCoin("" + list[2].ZoneName, new Guid("418A8F6F-7868-4A8D-A008-A62600E5BF1E"), 1000);
                //lblTest.Text = r.ToString() + "123456789";
                //var l = list;
            }
            catch (Exception ex)
            {
                lblTest.Text = ex.Message;
            }
        }
    }
}