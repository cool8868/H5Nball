﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Games.NBall.AdminWeb.AccountLog
{
    public partial class AuctionBuyLog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("竞拍日志", BindData, ClearData, SelectControl1);
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