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
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.WebServerFacade;

namespace Games.NBall.AdminWeb.Account
{
    public partial class MailInfo : System.Web.UI.Page
    {
        private Dictionary<int, EnumTransactionType> _mailTransactionTypeDic;
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("邮件记录", BindData, ClearData, SelectControl1);
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

        protected void btnReceive_OnClick(object sender, EventArgs e)
        {
            try
            {
                var s = txtRecordId.Text.Trim();
                var recordId = ConvertHelper.ConvertToInt(s);
                if (recordId <= 0)
                {
                    Master.ShowMessage("序号必须大于0.");
                    return;
                }
                var mail = MailInfoMgr.GetById(recordId,Master.ZoneId);
                if (!mail.HasAttach)
                {
                    Master.ShowMessage("该邮件没有附件.");
                    return;
                }
                var code = WebServerHandler.AttachmentReceive(Master.ZoneId, mail.ManagerId, mail.Idx);
                if (code == 0)
                {
                    Master.ShowMessage("收取成功");
                }
                else
                {
                    Master.ShowMessage("收取失败："+code);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                Master.ShowMessage("出错了："+ex.Message);
            }
        }
    }
}