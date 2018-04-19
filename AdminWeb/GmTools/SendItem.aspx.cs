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
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Config.Custom;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.WebServerFacade;

namespace Games.NBall.AdminWeb.Tools
{
    public partial class SendItem : System.Web.UI.Page
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
                    var isDeal = chkDeal.Checked;
                    var code = AdminMgr.AddItems(_account.ZoneId, _account.ManagerId, itemCode, count, strength, isBinding,isDeal);
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

        protected void btnGoldBar_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {
                    var count = ConvertHelper.ConvertToInt(txt_GoldBar.Text);
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
                    GoldbarRecordEntity record = new GoldbarRecordEntity(0, _account.ManagerId, true, count,
                        (int)EnumTransactionType.AdminAddItem, DateTime.Now);
                    var goldBarManager = ScoutingGoldbarMgr.GetById(_account.ManagerId, _account.ZoneId);
                    if (goldBarManager == null)
                    {
                        goldBarManager = new ScoutingGoldbarEntity(_account.ManagerId, count, 0, 0, 0, DateTime.Now,
                            DateTime.Now);
                        if (!ScoutingGoldbarMgr.Insert(goldBarManager, null, _account.ZoneId))
                        {
                            ShowMessage("发送失败");
                            return;
                        }
                    }
                    else
                    {
                        goldBarManager.GoldBarNumber = goldBarManager.GoldBarNumber + count;
                        if (!ScoutingGoldbarMgr.Update(goldBarManager, null, _account.ZoneId))
                        {
                            ShowMessage("发送失败");
                            return;
                        }
                    }
                    GoldbarRecordMgr.Insert(record, null, _account.ZoneId);

                    ShowMessage("发送成功");
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

#region 购买商城礼包

        protected void btn_SendMallItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckManager())
                {
                    var mallitemId = ConvertHelper.ConvertToInt(txt_mallId.Text);
                    if (mallitemId <= 0)
                    {
                        ShowMessage("请输入正确的道具ID");
                        return;
                    }
                    var code = TxBuyPointShipments(_account.ManagerId, ShareUtil.GenerateComb().ToString(), mallitemId,
                        _account.ZoneId);
                    if (code == "")
                        ShowMessage("发送成功");
                    else
                        ShowMessage(code);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                ShowMessage(ex.Message);
            }
        }

        public string TxBuyPointShipments(Guid managerId, string orderId, int mallCode,string zoneId)
        {
            try
            {
                var manager = NbManagerMgr.GetById(managerId,zoneId);
                if (manager == null)
                    return "找不到经理信息";
                var mallConfig = DicMallitemMgr.GetById(mallCode);
                //var mallConfig = CacheFactory.MallCache.GetMallEntityWithoutPoint(mallCode);
                if (mallConfig == null ||
                    (mallConfig.MallType != (int)EnumMallType.QP && mallConfig.MallType != (int)EnumMallType.Mystery))
                {
                    return "请输入正确的商城ID";
                }
                
                ManagerMonthcardEntity monthcardEntity = null;
                DateTime date = DateTime.Now;
                int bonus = 0;
                var recordList = ManagerChargenumberMgr.GetManagerIdList(managerId, zoneId);
                ManagerChargenumberEntity buyRecord = null;
                bool isFirst = false;
                if (recordList.Count > 0)
                {
                    buyRecord = recordList.Find(r => r.MallCode == mallCode);
                }
                else
                    isFirst = true;
                if (buyRecord == null)
                {
                    buyRecord = new ManagerChargenumberEntity(0, managerId, mallCode, 1, date, date);
                }
                else
                {
                    buyRecord.BuyNumber++;
                }
                MailBuilder mail = null;
                List<ConfigMallgiftbagEntity> prizeList = null;
                EnumMailType mailType = EnumMailType.ChargeSuccess;
                int point = mallConfig.CurrencyCount;
                if (mallConfig.EffectType == (int)EnumMallEffectType.MonthCard) //月卡
                {
                    MonthcardShipments(managerId, ref monthcardEntity, zoneId);
                }
                else if (mallConfig.MallType == (int)EnumMallType.Mystery)
                {
                    if (mallConfig.EffectType == (int)EnumMallEffectType.GiftBag)
                    {
                        //if (!IsHaveBuyWeekGiftBag(buyRecord, DateTime.Now))
                        //{
                        //    mailType = EnumMailType.ChargeSuccess;
                        //    point = mallConfig.EffectValue * 10;
                        //}
                        //else
                        //{
                            prizeList = GetGiftBag(mallCode);
                            if (prizeList.Count <= 0)
                                return "获取配置失败";
                            mailType = EnumMailType.GiftBagSuccess;
                        //}
                    }
                    else if (mallConfig.EffectType == (int)EnumMallEffectType.WeeklyGiftBag)
                    {
                        //if (!IsHaveBuyWeekGiftBag(buyRecord))
                        //{
                        //    mailType = EnumMailType.ChargeSuccess;
                        //    point = mallConfig.EffectValue * 10;
                        //}
                        //else
                        //{
                            prizeList = GetGiftBag(mallCode);
                            if (prizeList.Count <= 0)
                                return "获取配置失败";
                            mailType = EnumMailType.GiftBagSuccess;
                        //}
                    }
                    //else if (buyRecord.Idx > 0)
                    //{
                    //    mailType = EnumMailType.ChargeSuccess;
                    //    point = mallConfig.EffectValue * 10;
                    //}
                    else
                    {
                        prizeList = GetGiftBag(mallCode);
                        if (prizeList.Count <= 0)
                            return "获取配置失败";
                        mailType = EnumMailType.GiftBagSuccess;
                    }
                }
                prizeList = GetGiftBag(mallCode);
                

                var payEntity = new PayChargehistoryEntity();
                payEntity.Idx = orderId;
                payEntity.Account = _account.Account;
                payEntity.BillingId = orderId;
                payEntity.Bonus = bonus;
                payEntity.Cash = 0;
                payEntity.IsFirst = isFirst;
                payEntity.MallCode = mallCode;
                payEntity.Point = point;
                payEntity.RowTime = DateTime.Now;
                payEntity.SourceType = (int)EnumChargeSourceType.GmCharge;
                payEntity.States = 2;
                payEntity.UpdateTime = DateTime.Now;
                //邮件发货
                mail = new MailBuilder(managerId, mallConfig.Name, point + bonus, prizeList, mailType, 0,
                    0);
                buyRecord.UpdateTiem = DateTime.Now;
                payEntity.Bonus = bonus;
                var code = SaveBuyPointShipmentsTx(manager, point, bonus, payEntity,
                    monthcardEntity, mail,
                    buyRecord, mallConfig,zoneId);
                return code;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("充值发货", ex);
                return ex.Message;
            }
        }


        public List<ConfigMallgiftbagEntity> GetGiftBag(int mallId)
        {
            var list = ConfigMallgiftbagMgr.GetAll();
            if (list.Count == 0)
                return new List<ConfigMallgiftbagEntity>();
            return list.FindAll(r => r.MallCode == mallId);
        }

        /// <summary>
        /// 购买月卡发货
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="monthcardEntity"></param>
        /// <returns></returns>
        private MessageCode MonthcardShipments(Guid managerId, ref ManagerMonthcardEntity monthcardEntity,string zoneId)
        {
            DateTime date = DateTime.Now;
            monthcardEntity = ManagerMonthcardMgr.GetById(managerId,zoneId);
            if (monthcardEntity == null)
            {
                monthcardEntity = new ManagerMonthcardEntity(managerId, 1, date, date.AddDays(30),
                    date.AddDays(-1),
                    date, date);
            }
            else
            {
                monthcardEntity.BuyNumber++;
                if (monthcardEntity.DueToTime.Date < date.Date) //已过期
                {
                    monthcardEntity.BuyTime = date;
                    monthcardEntity.DueToTime = date.AddDays(30);
                    monthcardEntity.PrizeDate = date.AddDays(-1);
                    monthcardEntity.UpdateTime = date;
                    monthcardEntity.RowTime = date;
                }
                else //未过期
                {
                    monthcardEntity.DueToTime = monthcardEntity.DueToTime.AddDays(30);
                    monthcardEntity.UpdateTime = date;
                }
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 是否可以购买每周礼包
        /// </summary>
        /// <param name="managerInfo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private bool IsHaveBuyWeekGiftBag(ManagerChargenumberEntity managerInfo, DateTime date)
        {
            DateTime startBuyTime = ShareUtil.GetThisWeekDayDate(DayOfWeek.Friday);
            DateTime endBuyTime = ShareUtil.GetThisWeekDayDate(DayOfWeek.Sunday);
            if (managerInfo != null && managerInfo.Idx > 0)
            {
                if (managerInfo.UpdateTiem > startBuyTime) //购买过了
                    return false;
            }
            if (date < startBuyTime) //时间未到
                return false;
            return true;
        }
        /// <summary>
        /// 是否可以购买每周礼包
        /// </summary>
        /// <param name="managerInfo"></param>
        /// <returns></returns>
        private bool IsHaveBuyWeekGiftBag(ManagerChargenumberEntity managerInfo)
        {
            DateTime buyTime = ShareUtil.GetThisWeekDayDate(DayOfWeek.Monday);
            if (managerInfo != null && managerInfo.Idx > 0)
            {
                if (managerInfo.UpdateTiem >= buyTime) //购买过了
                    return false;
            }
            return true;
        }

        private string SaveBuyPointShipmentsTx(NbManagerEntity manager, int point, int bonus,
            PayChargehistoryEntity payEntity, ManagerMonthcardEntity monthcardEntity, MailBuilder mail,
            ManagerChargenumberEntity buyRecord, DicMallitemEntity mallConfig, string zoneId)
        {

            if (point > 0 || bonus > 0)
            {
                var messCode1 = PayCore.Instance.AddPoint(payEntity.Account, point, bonus, EnumChargeSourceType.GmCharge,
                    payEntity.BillingId, null, zoneId);
                if (messCode1 != MessageCode.Success)
                {
                    return "增加钻石失败"+point+","+bonus+","+(int)messCode1;
                }
                PayChargehistoryMgr.Insert(payEntity, null, zoneId);
            }
            if (monthcardEntity != null)
            {
                if (monthcardEntity.BuyNumber == 1)
                {
                    if (!ManagerMonthcardMgr.Insert(monthcardEntity, null, zoneId))
                        return "月卡发送失败";
                }
                else
                {
                    if (!ManagerMonthcardMgr.Update(monthcardEntity, null, zoneId))
                        return "月卡发送失败";
                }
            }
            if (!mail.Save(zoneId))
                return "邮件发送失败";

            if (buyRecord.Idx == 0)
            {
                ManagerChargenumberMgr.Insert(buyRecord, null, zoneId);
            }
            else
            {
                ManagerChargenumberMgr.Update(buyRecord, null, zoneId);
            }
            try
            {
                if (manager != null)
                {
                    var curScore = mallConfig.EffectValue*10;
                    var vipManager = VipManagerMgr.GetById(manager.Idx, zoneId);
                    if (vipManager != null)
                    {
                        curScore += vipManager.VipExp;
                        vipManager.VipExp = curScore;
                        VipManagerMgr.Update(vipManager,null,zoneId);
                    }
                    else
                    {
                        vipManager = new VipManagerEntity(manager.Idx, curScore, DateTime.Now, DateTime.Now,
                            DateTime.Now);
                        VipManagerMgr.Insert(vipManager, null, zoneId);
                    }

                    var newlevel = CacheFactory.VipdicCache.GetVipLevel(curScore);
                    if (newlevel > manager.VipLevel)
                    {
                        manager.VipLevel = newlevel;
                        ManagerUtil.SaveManagerData(manager, null, null, zoneId);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeUpVip", ex);
                return ex.Message;
            }
            return "";
        }


        #endregion

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