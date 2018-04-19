using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Games.NBall.AdminWeb.Account
{
    public partial class AuctionInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("拍卖信息", BindData, ClearData, SelectControl1);
            if (!IsPostBack)
            {
                Master.SelectData();
            }
        }

        void BindData()
        {

        }

        void ClearData()
        {
        }
    }
}