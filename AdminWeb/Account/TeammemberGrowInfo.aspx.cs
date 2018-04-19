using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;

namespace Games.NBall.AdminWeb.Account
{
    public partial class TeammemberGrowInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("球员成长", BindData, ClearData);
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
            var list = TeammemberGrowMgr.GetByManager(accountData.ManagerId, accountData.ZoneId);
            if (list == null)
            {
                Master.ShowMessage("没有数据.");
                return;
            }
            datagrid1.DataSource = list;
            datagrid1.DataBind();
        }

        void ClearData()
        {
            lblHint.Text = "";
            datagrid1.DataSource = null;
            datagrid1.DataBind();
        }
    }
}