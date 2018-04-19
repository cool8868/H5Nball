using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;

namespace Games.NBall.AdminWeb.Account
{
    public partial class TaskRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("任务信息", BindData, ClearData,SelectControl1,SelectControl2);
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
            //var pending = TaskPendingMgr.GetById(accountData.ManagerId, accountData.ZoneId);
            //if (pending != null)
            //{
            //    lblPending.Text = pending.TaskString;
            //}
            //else
            //{
            //    lblPending.Text = "";
            //}
        }

        void ClearData()
        {
            lblPending.Text = "";
        }
    }
}