using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Games.NBall.AdminWeb.AdminEntity;
using Games.NBall.Bll;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.WebServerFacade;

namespace Games.NBall.AdminWeb.Develop
{
    public partial class DevUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("GM工具", BindData, ClearData);
        }

        private void SaveAdminLog( EnumAdminOperationType operationType, string memo)
        {
            try
            {
                AdminMgr.SaveAdminLog(this.User.Identity.Name, this.Request.UserHostAddress, operationType, _account.ZoneId, _account.Account, _account.Name, _account.ManagerId, memo);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
            
        }

        protected void btnUpLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {
                    var level = ConvertHelper.ConvertToInt(txtLevel.Text);
                    if (level < 1)
                    {
                        ShowMessage("等级不能小于1");
                        return;
                    }
                    if (level > 80)
                    {
                        ShowMessage("等级不能超过80");
                        return;
                    }
                    var exp = ConvertHelper.ConvertToInt(txtExp.Text);
                    var manager = NbManagerMgr.GetById(_account.ManagerId,_account.ZoneId);
                    manager.Level = level;
                    manager.EXP = exp;
                    if (NbManagerMgr.Update(manager,null, _account.ZoneId))
                    {
                        SaveAdminLog(EnumAdminOperationType.UpLevel, string.Format("Level:{0}", level));
                        ShowMessage("修改等级成功");
                    }
                    else
                    {
                        ShowMessage("修改等级失败");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        protected void btnUpVipLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {
                    var level = ConvertHelper.ConvertToInt(txtVipLevel.Text);
                    if (level < 0)
                    {
                        ShowMessage("等级不能小于0");
                        return;
                    }
                    if (level > 10)
                    {
                        ShowMessage("等级不能超过10");
                        return;
                    }
                    var manager = NbManagerMgr.GetById(_account.ManagerId, _account.ZoneId);
                    manager.VipLevel = level;
                    if (NbManagerMgr.Update(manager, null, _account.ZoneId))
                    {
                        SaveAdminLog(EnumAdminOperationType.UpLevel, string.Format("VipLevel:{0}", level));
                        ShowMessage("修改Vip等级成功");
                    }
                    else
                    {
                        ShowMessage("修改Vip等级失败");
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        protected void btnPlayerLevel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {
                    Guid playerId = Guid.Empty;
                    try
                    {

                        playerId = new Guid(txtPlayerId.Text);
                    }
                    catch
                    {
                        ShowMessage("球员ID格式错误");
                        return;
                    }
                    if (playerId == Guid.Empty)
                    {
                        ShowMessage("球员ID不能为空");
                        return;
                    }

                    var playerlevel = ConvertHelper.ConvertToInt(txtPlayerlevel.Text);

                    if (playerlevel <= 0)
                    {
                        ShowMessage("球员等级不能小于0");
                        return;
                    }
                    if (playerlevel > 80)
                    {
                        ShowMessage("球员等级不能超过80");
                        return;
                    }
                    var package = ItemCore.Instance.GetPackage(_account.ManagerId,
                        Entity.Enums.Shadow.EnumTransactionType.AdminAddItem, _account.ZoneId);
                    if (package == null)
                    {
                        ShowMessage("获取背包失败");
                        return;
                    }
                    var player = package.GetItem(playerId);
                    if (player == null || player.ItemType != (int) EnumItemType.PlayerCard)
                    {
                        ShowMessage("获取球员失败");
                        return;
                    }
                    var p = player.ItemProperty as PlayerCardProperty;
                    if (p == null)
                    {
                        ShowMessage("获取球员失败");
                        return;
                    }
                    p.Level = playerlevel;
                    package.Update(player);
                    if (!package.SaveTask(null))
                    {
                        ShowMessage("背包更新失败");
                        return;
                    }
                    ShowMessage("修改成功");
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.ToString());
                return;
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