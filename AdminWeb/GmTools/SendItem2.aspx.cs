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
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.WebServerFacade;

namespace Games.NBall.AdminWeb.Tools
{
    public partial class SendItem2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Master.SetMaster("GM工具", BindData, ClearData);
        }

        protected void btnSendItem_Click(object sender, EventArgs e)
        {
            try
            {    if (CheckManager())
                {
                    var itemCode =ConvertHelper.ConvertToInt(txtItemCode.Text);
                    if (itemCode <= 0)
                    {
                        ShowMessage("物品编码不对");
                        return;
                    }
                    var count = ConvertHelper.ConvertToInt(txtItemCount.Text);
                    if (count >10)
                    {
                        ShowMessage("一次发送不能超过10个");
                        return;
                    }
                    var slotcount = ConvertHelper.ConvertToInt(txtSlotColorCount.Text);
                    if (count >3)
                    {
                        ShowMessage("彩色插槽不能超过3个");
                        return;
                    }
                    var strength = ConvertHelper.ConvertToInt(txtItemStrength.Text);
                    var isBinding = chkBinding.Checked;
                    var code = AdminMgr.AddItems(_account.ZoneId, _account.ManagerId, itemCode, count, strength, isBinding,false, slotcount);
                    if (code == 0)
                    {
                        SaveAdminLog(EnumAdminOperationType.SendItemAdvance,string.Format("ItemCode:{0},Count:{1},Strength:{2},SlotColorCount:{3},IsBinding:{4}",itemCode,count,strength,slotcount,isBinding));
                    }
                    ShowMessage("添加物品返回："+code);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        private void SaveAdminLog( EnumAdminOperationType operationType, string memo)
        {
            try
            {
                AdminMgr.SaveAdminLog(this.User.Identity.Name, this.Request.UserHostAddress, operationType,_account.ZoneId, _account.Account,_account.Name,_account.ManagerId,memo);
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