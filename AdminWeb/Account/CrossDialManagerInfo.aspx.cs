using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.WebServerFacade;

namespace Games.NBall.AdminWeb.Account
{
    public partial class CrossDialManagerInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("金币信息", BindData, ClearData, SelectControl1, SelectControl1);
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