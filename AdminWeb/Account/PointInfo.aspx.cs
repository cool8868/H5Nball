using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Common;
using HTB.DevFx.Data.Utils;

namespace Games.NBall.AdminWeb.Account
{
    public partial class PointInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("点券信息", BindData, ClearData,SelectControl1,SelectControl2);
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            var accountData = Master.GetAccount();
            if (accountData == null)
            {
                Master.ShowMessage("请先选择经理.");
                return;
            }
            ClearData();
            var payAccount = PayUserMgr.GetById(accountData.Account, accountData.ZoneId);
            if (payAccount == null)
            {
                Master.ShowMessage("没有点券信息.");
                return;
            }
            try
            {
                lblCash.Text = payAccount.TotalCash.ToString();

           

            lblPoint.Text = string.Format("{0}+{1}(赠送)={2}", payAccount.Point, payAccount.Bonus, payAccount.TotalPoint);

            var totalCash = 0;
            var totalchargepoint = 0;
            var totalchargebonus = 0;
            var totalconsumepoint = 0;
            var totalconsumebonus = 0;
            PayUserMgr.Stat(accountData.Account, ref totalCash, ref totalchargepoint, ref totalchargebonus,
                ref totalconsumepoint, ref totalconsumebonus);

            lblCharge.Text = string.Format("充值金额：{0}, 点券：{1}+{2}(赠送)={3}", totalCash, totalchargepoint, totalchargebonus,
                totalchargepoint + totalchargebonus);
            lblConsume.Text = string.Format("{0}+{1}(赠送)={2}", totalconsumepoint, totalconsumebonus,
                totalconsumepoint + totalconsumebonus);
            lblPoint2.Text = string.Format("{0}+{1}(赠送)={2}", totalchargepoint - totalconsumepoint,
                totalchargebonus - totalconsumebonus,
                totalchargepoint + totalchargebonus - totalconsumepoint - totalconsumebonus);
        }

                catch (Exception ex)
            {
                lblCash.Text ="读取错误";
            }
        }

        void ClearData()
        {
            lblPoint.Text = "";
            lblConsume.Text = "";
            lblCharge.Text = "";
            lblCash.Text = "";
        }
    }
}