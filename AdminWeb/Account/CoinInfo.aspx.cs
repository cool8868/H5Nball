using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Common;
using Games.NBall.Core.Manager;

namespace Games.NBall.AdminWeb.Account
{
    public partial class CoinInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("金币信息", BindData, ClearData,SelectControl1,SelectControl2);
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
            var manager = NbManagerMgr.GetById(accountData.ManagerId, accountData.ZoneId);
            if (manager == null)
            {
                Master.ShowMessage("获取经理信息失败.");
                return;
            }
            lblPoint.Text = manager.Coin.ToString();

            int chargeCoin = 0;
            int consumeCoin = 0;
            ShadowMgr.CoinStat(accountData.ManagerId, ref chargeCoin, ref consumeCoin, accountData.ZoneId);
            lblCharge.Text = chargeCoin.ToString();
            lblConsume.Text = consumeCoin.ToString();
            lblPoint2.Text = string.Format("{0}", chargeCoin - consumeCoin);
        }

        void ClearData()
        {
            lblPoint.Text = "";
            lblCharge.Text = "";
            lblConsume.Text = "";
            lblPoint2.Text = "";
            //datagrid1.DataSource = null;
            //datagrid1.DataBind();
        }
    }
}