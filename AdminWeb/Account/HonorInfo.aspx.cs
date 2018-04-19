using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.Bll;
using Games.NBall.Common;

namespace Games.NBall.AdminWeb.Account
{
    public partial class HonorInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("荣誉信息", BindData, ClearData);
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
            var matchList = NbManagerhonorMgr.GetByManager(accountData.ManagerId, accountData.ZoneId);
            if (matchList == null)
            {
                Master.ShowMessage("没有荣誉数据.");
                return;
            }
            var localList = new List<AdminManagerhonorEntity>(matchList.Count);
            foreach (var entity in matchList)
            {
                var aEntity = new AdminManagerhonorEntity(entity);
                aEntity.MatchTypeV = AdminMgr.GetEnumName("EnumMatchType", entity.MatchType);
                localList.Add(aEntity);
            }
            lblHint.Text = "总荣誉数：" + localList.Count;
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