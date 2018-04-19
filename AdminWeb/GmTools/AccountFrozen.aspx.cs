using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.AdminWeb.AdminEntity;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core;
using Games.NBall.Core.Online;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.WebServerFacade;

namespace Games.NBall.AdminWeb.Tools
{
    public partial class AccountFrozen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("GM工具", BindData, ClearData);
        }

        protected void btnKickSession_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {

                    var code = WebServerHandler.KickSession(_account.ZoneId, _account.ManagerId);
                    if (code)
                    {
                        SaveAdminLog(EnumAdminOperationType.KickSession, string.Format("Managerid:{0},Name:{1}", _account.ManagerId,_account.Name));
                        ShowMessage("踢线成功");
                    }
                    else
                    {
                        ShowMessage("踢线失败");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        protected void btnLockUserUnexpect_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {

                    var code = WebServerHandler.LockUserUnexpect(_account.ZoneId, _account.ManagerId, this.User.Identity.Name, txtLockMemo.Text);
                    if (code)
                    {
                        SaveAdminLog(EnumAdminOperationType.LockAccount, string.Format("Managerid:{0},Name:{1}", _account.ManagerId, _account.Name));
                        ShowMessage("封停成功");
                    }
                    else
                    {
                        ShowMessage("封停失败");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        protected void btnUnlockUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {

                    var code = WebServerHandler.BreakLock(_account.ZoneId, User.Identity.Name, _account.ManagerId, this.User.Identity.Name, txtLockMemo.Text, _account.ZoneId);
                    if (code)
                    {
                        SaveAdminLog(EnumAdminOperationType.BreakLock, string.Format("Managerid:{0},Name:{1}", _account.ManagerId, _account.Name));
                        ShowMessage("解封成功");
                    }
                    else
                    {
                        ShowMessage("解封失败");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        private void SaveAdminLog(EnumAdminOperationType operationType, string memo)
        {
            try
            {
                AdminMgr.SaveAdminLog(this.User.Identity.Name, this.Request.UserHostAddress, operationType,_account.ZoneId, _account.Account, _account.Name, _account.ManagerId, memo);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }

        }

        private AccountData _account = null;
        bool CheckManager()
        {
            var accountInfo = Master.GetAccount();
            if (accountInfo == null)
            {
                ltlMessage.Text = "请先选择经理!";
                return false;
            }
            _account = accountInfo;
            return true;
        }

        private static int _index = 0;
        void ShowMessage(string msg)
        {
            _index++;
            ltlMessage.Text = "(序列:" + _index + ")" + msg;
        }

        void BindData()
        {

        }

        void ClearData()
        {

        }
    }
}