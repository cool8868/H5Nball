using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;

namespace Games.NBall.AdminWeb.Account
{
    public partial class MatchStatInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("比赛统计", BindData, ClearData);
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
            var matchList = NbMatchstatMgr.GetByManager(accountData.ManagerId,accountData.ZoneId);
            if (matchList == null)
            {
                Master.ShowMessage("没有比赛数据.");
                return;
            }
            var localList = new List<AdminMatchStatEntity>(matchList.Count);
            foreach (var entity in matchList)
            {
                var aEntity = new AdminMatchStatEntity(entity);
                aEntity.MatchTypeV = AdminMgr.GetEnumName("EnumMatchType", entity.MatchType);
                localList.Add(aEntity);
            }

            lblHint.Text = string.Format("总场次:{0}", matchList.Sum(d => d.TotalCount));
            datagrid1.DataSource = localList;
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