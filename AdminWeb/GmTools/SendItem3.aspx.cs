﻿using System;
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
using Games.NBall.Core.Item;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.WebServerFacade;

namespace Games.NBall.AdminWeb.Tools
{
    public partial class SendItem3 : System.Web.UI.Page
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
                    var strength = ConvertHelper.ConvertToInt(txtItemStrength.Text);
                    var isBinding = chkBinding.Checked;

                    var code = AdminMgr.AddItems(_account.ZoneId, _account.ManagerId, itemCode, count, strength, isBinding,false);
                    if (code == 0)
                    {
                        SaveAdminLog(EnumAdminOperationType.SendItem,string.Format("ItemCode:{0},Count:{1},Strength:{2}",itemCode,count,strength));
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

        protected void btnSendCoin_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {
                    var count = ConvertHelper.ConvertToInt(txtCoinCount.Text);
                    if (count < 0)
                    {
                        ShowMessage("数量不能小于0");
                        return;
                    }
                    if (count > 100000)
                    {
                        ShowMessage("一次发送不能超过100000");
                        return;
                    }
                    var code = WebServerHandler.AddCoin(_account.ZoneId,_account.ManagerId, count);
                    if (code == 0)
                    {
                        SaveAdminLog(EnumAdminOperationType.SendCoin, string.Format("Coin:{0}",  count));
                    }
                    ShowMessage("添加金币：" + code);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
            
        }

        protected void btnSendPoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {
                    var count = ConvertHelper.ConvertToInt(txtPointCount.Text);
                    if (count < 0)
                    {
                        ShowMessage("数量不能小于0");
                        return;
                    }
                    if (count > 5000)
                    {
                        ShowMessage("一次发送不能超过5000");
                        return;
                    }
                    var code = WebServerHandler.Charge(_account.ZoneId, _account.Account, EnumChargeSourceType.AdminSend, 0, 0, count, Guid.NewGuid().ToString());
                    if (code == 0)
                    {
                        SaveAdminLog(EnumAdminOperationType.SendPoint, string.Format("Point:{0}", count));
                    }
                    ShowMessage("添加点券：" + code);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        protected void btnGmCharge_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {
                    var count = ConvertHelper.ConvertToInt(txtGmPoint.Text);
                    if (count < 0)
                    {
                        ShowMessage("数量不能小于0");
                        return;
                    }
                    if (count > 5000)
                    {
                        ShowMessage("一次发送不能超过5000");
                        return;
                    }
                    string billingId = Guid.NewGuid().ToString();
                    var code = WebServerHandler.Charge(_account.ZoneId, _account.Account, EnumChargeSourceType.GmCharge, 0, 0, count, billingId);
                    if (code == 0)
                    {
                        SaveAdminLog(EnumAdminOperationType.GmCharge, string.Format("gm charge,Count:{0},BillingId:{1}", count, billingId));
                    }
                    ShowMessage("Gm充值：" + code);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        protected void btnGetGmPoint_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {
                    int totalPoint = 0;
                    PayUserMgr.GetGmChargePoint(_account.Account, ref totalPoint, null, _account.ZoneId);
                    lblGmChargePoint.Text = totalPoint.ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
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