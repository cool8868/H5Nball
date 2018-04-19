﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Games.NBall.AdminWeb.Account
{
    public partial class TalentInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("天赋信息", BindData, ClearData, SelectControl1);
            if (!IsPostBack)
            {
                BindData();
            }
        }

        void BindData()
        {
            var accountData = Master.GetAccount();
            if (accountData == null)
            {
                Master.ShowMessage("请先选择经理.");
                return;
            }

            ClearData();
            
        }

        void ClearData()
        {
        }
    }
}