using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Item;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.ServiceEngine;
using Games.NBall.WebServerFacade;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Manager
{
    public class PayCore : BaseDomain
    {
        #region .ctor
        static readonly NBThreadPool _threadPool = new NBThreadPool(10);
        private readonly int _payChargeCashToVipScore;
        private readonly int _payContinuePoint;
        private readonly string _gambleAccount;
        public PayCore(int p)
        {
            _payChargeCashToVipScore =
                CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PayChargeCashToVipScore);
            _gambleAccount = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.GambleAccount);
            _payContinuePoint = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.PayContinuePoint);
        }
        #endregion

        #region Facade

        public int PayContinuePoint
        {
            get { return _payContinuePoint; }
        }

        public int PayChargeCashToVipScore
        {
            get { return _payChargeCashToVipScore; }
        }

        public static PayCore Instance
        {
            get { return SingletonFactory<PayCore>.SInstance; }
        }

        public int GetPoint(string account)
        {
            var entity = PayUserMgr.GetById(account);
            if (entity == null)
                return 0;
            else
            {
                return entity.TotalPoint;
            }
        }

        public int GetBindPoint(string account)
        {
            var entity = PayUserMgr.GetById(account);
            if (entity == null)
                return 0;
            else
            {
                return entity.BindPoint;
            }
        }

        public int GetBindPoint(Guid managerId)
        {
            var entity = PayUserMgr.GetPointByManagerId(managerId);
            if (entity == null)
                return 0;
            else
            {
                return entity.BindPoint;
            }
        }

        public int GetPoint(Guid managerId)
        {
            var entity = PayUserMgr.GetPointByManagerId(managerId);
            if (entity == null)
                return 0;
            else
            {
                return entity.TotalPoint;
            }
        }

        public PayUserEntity GetPayUser(string account, string siteId = "")
        {
            var entity = PayUserMgr.GetById(account, siteId);
            if (entity == null)
            {
                entity = CreateEntity();
            }
            return entity;
        }

        public PayUserEntity GetPayUser(Guid managerId, string siteId = "")
        {
            var entity = PayUserMgr.GetPointByManagerId(managerId, siteId);
            if (entity == null)
            {
                entity = CreateEntity();
            }
            return entity;
        }

        public MessageCode TxCharge(string account, int mallCode, int sourceType, decimal cash, int point, int bonus, string orderId, string zoneId)
        {
            try
            {
                if (point <= 0 && bonus <= 0)
                    return MessageCode.NbParameterError;
                if (point > 0 && cash <= 0)
                    return MessageCode.NbParameterError;
                if (string.IsNullOrEmpty(orderId))
                {
                    return MessageCode.NbParameterError;
                }
                int result = 0;

                PayUserMgr.ChargeTx(account, sourceType, orderId, point, point, cash, bonus,mallCode, ref result, null, zoneId);
                if (result != (int)MessageCode.PaySuccess)
                {
                    return (MessageCode)result;
                }
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("TxCharge", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode ChargeTest(string account, int sourceType, int cash, int point, int bonus, string orderId, DateTime curTime)
        {
            try
            {
                if (point <= 0 && bonus <= 0)
                    return MessageCode.NbParameterError;
                if (point > 0 && cash <= 0)
                    return MessageCode.NbParameterError;
                if (string.IsNullOrEmpty(orderId))
                {
                    return MessageCode.NbParameterError;
                }
                int result = 0;
                PayUserMgr.ChargeTest(account, (int)EnumChargeSourceType.System, orderId, point, cash, bonus, curTime, ref result);
                if (result != (int)MessageCode.PaySuccess)
                {
                    return (MessageCode)result;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeTest", ex);
                return MessageCode.Exception;
            }
            try
            {
                PayContinuingMgr.UpdateContinueday(account, point, PayContinuePoint, curTime.Date.AddDays(-1), curTime.Date, curTime);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeUpContinue", ex);
            }
            //if (cash > 0)
            //{

            //    try
            //    {
            //        _threadPool.Add(() => ChargeAfter(account, point, PayContinuePoint, _payChargeCashToVipScore, curTime));
            //    }
            //    catch (Exception ex)
            //    {
            //        SystemlogMgr.Error("ChargeUpVip", ex);
            //    }
            //}
            return MessageCode.Success;
        }

        public MessageCode TxCharge(string account, int sourceType, decimal cash, int point, int bonus, string orderId, string zoneId)
        {
            try
            {
                if (point <= 0 && bonus <= 0)
                    return MessageCode.NbParameterError;
                if (point > 0 && cash <= 0)
                    return MessageCode.NbParameterError;
                if (string.IsNullOrEmpty(orderId))
                {
                    return MessageCode.NbParameterError;
                }
                int result = 0;

                PayUserMgr.Charge(account, sourceType, orderId, point, point, cash, bonus, ref result, null, zoneId);
                if (result != (int)MessageCode.PaySuccess)
                {
                    return (MessageCode)result;
                }
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("TxCharge", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode Charge(string account, int sourceType, decimal cash, int point, int bonus, string orderId,string eqid)
                                     //用户名，      充值类型，        现金  ，       点券， 赠送点券，      订单号，   装备id
        {
            try
            {
                if (point <= 0 && bonus <= 0)
                    return MessageCode.NbParameterError;
                if (point > 0 && cash <= 0)
                    return MessageCode.NbParameterError;
                if (string.IsNullOrEmpty(orderId))
                {
                    return MessageCode.NbParameterError;
                }
                int result = 0;
                int chargePoint = point;
                if (sourceType == (int)EnumChargeSourceType.GmCharge)
                    chargePoint = bonus;
                ItemPackageFrame package = null;
                  //登记装备
                if (!eqid.IsNullOrWhiteSpace())
                {
                    var manager = ManagerCore.Instance.GetManager(account);
                    package = ItemCore.Instance.GetPackage(manager.Idx, EnumTransactionType.Charge);
                    if (package == null)
                    {
                        return MessageCode.NbNoPackage;
                    }

                    package.AddItem(int.Parse(eqid),false,false);
                   
                }
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    //var messageCode = Tran_SaveLottery(transactionManager.TransactionObject, matchId, managerId, package, mail, lotteryRepeatCode);
                    var messageCode = SaveCharge(package,account,sourceType,orderId,point,chargePoint,cash,bonus,transactionManager.TransactionObject,eqid);

                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                        if (point > 0)
                        {
                            doChargeAfter(account, point);
                        }
                        if (sourceType == (int)EnumChargeSourceType.GmCharge)
                        {
                            GmVip(account, bonus, PayContinuePoint, DateTime.Now);
                        }
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Charge", ex);
                return MessageCode.Exception;
            }
            if (point > 0)
            {
                doChargeAfter(account, point);
            }
            if (sourceType == (int)EnumChargeSourceType.GmCharge)
            {
                GmVip(account, bonus, PayContinuePoint, DateTime.Now);
            }
            return MessageCode.Success;
        }

        private int SaveCharge(ItemPackageFrame package,string account,int sourceType,string orderId,int point,int chargePoint,decimal cash,int bonus,DbTransaction trans,string eqid)
        {
            int result = -1;
            PayChargehistoryMgr.ChargeCSDK(account, sourceType, orderId, point, chargePoint, cash, bonus, ref result, int.Parse(eqid),trans);
            if (result != 0)
                return result;
            if (package != null)
            {
                if (!package.Save(trans))
                    return (int) MessageCode.NbUpdateFail;
                package.Shadow.Save();
            }
            return 0;
        }

      

        public MessageCode AddCSDK(String sign, int orderId, string gameOrderId, int price,
                 string channelAlias, string playerId, string serverId, int goodsId, string payResult, string state, DateTime sourceTime)
        {
            var b=AllCsdkMgr.Insert(new AllCsdkEntity(0, sign, orderId, gameOrderId, price,
                channelAlias, playerId, serverId, goodsId, int.Parse(payResult), state, sourceTime));
            if(b)
            return MessageCode.Success;
            return MessageCode.NbUpdateFailPackage;
        }


        public MessageCode Charge(string account, int sourceType, decimal cash, int point, int bonus, string orderId)
        {
            try
            {
                if (point <= 0 && bonus <= 0)
                    return MessageCode.NbParameterError;
                if (point > 0 && cash <= 0)
                    return MessageCode.NbParameterError;
                if (string.IsNullOrEmpty(orderId))
                {
                    return MessageCode.NbParameterError;
                }
                int result = 0;
                int chargePoint = point;
                if (sourceType == (int)EnumChargeSourceType.GmCharge)
                    chargePoint = bonus;
                PayUserMgr.Charge(account, sourceType, orderId, point, chargePoint, cash, bonus, ref result);
                if (result != (int)MessageCode.PaySuccess)
                {
                    return (MessageCode)result;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Charge", ex);
                return MessageCode.Exception;
            }
            if (point > 0)
            {
                doChargeAfter(account, point);
            }
            if (sourceType == (int)EnumChargeSourceType.GmCharge)
            {
                GmVip(account, bonus, PayContinuePoint, DateTime.Now);
            }
            return MessageCode.Success;
        }

        void GmVip(string account, int point, int needPoint, DateTime curTime)
        {
            try
            {
                PayContinuingMgr.UpdateContinueday(account, point, needPoint, curTime.Date.AddDays(-1), curTime.Date, curTime);

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeUpContinue", ex);
            }
            var manager = ManagerCore.Instance.GetManager(account);
            try
            {

                if (manager != null)
                {
                    int totalPoint = 0;
                    PayUserMgr.GetGmChargePoint(account, ref totalPoint);
                    var payUser = PayUserMgr.GetById(account);
                    var curScore = totalPoint;
                    var vipManager = VipManagerMgr.GetById(manager.Idx);
                    if (vipManager != null && vipManager.VipExp > 0)
                        curScore += vipManager.VipExp;

                    var newlevel = CacheFactory.VipdicCache.GetVipLevel(curScore);
                    if (newlevel > manager.VipLevel)
                    {
                        manager.VipLevel = newlevel;
                        ManagerUtil.SaveManagerData(manager);
                        ManagerCore.Instance.DeleteCache(manager.Idx);
                    }
                    //ChatHelper.SendUpdateManagerInfoPop(manager.Idx, payUser.TotalPoint, true, manager.VipLevel);
                    if (point > 0)
                    {
                        ActivityExThread.Instance.Charge(manager.Idx, account, point);
                        ActivityExThread.Instance.Charge(manager.Idx, point);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeUpVip", ex);
            }
            try
            {
                if (manager != null)
                {
                    int returnPoint = point;

                    //BuffPlusHelper.ChargeReturn50(ref returnPoint);
                    if (returnPoint > 0)
                    {
                        //var mail = new MailBuilder(manager.Idx, EnumMailType.ChargeReturn50, returnPoint);
                        //mail.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeReturnDouble", ex);
            }
        }

        public void doChargeAfter(string account, int point)
        {
            try
            {
                _threadPool.Add(
                    () => ChargeAfter(account, point, PayContinuePoint, DateTime.Now));
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("doChargeAfter", ex);
            }
        }

        public static void ChargeAfter(string account, int point, int needPoint, DateTime curTime)
        {
            try
            {
                PayContinuingMgr.UpdateContinueday(account, point, needPoint, curTime.Date.AddDays(-1), curTime.Date, curTime);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeUpContinue", ex);
            }
            var manager = ManagerCore.Instance.GetManager(account);
            try
            {
                if (manager != null)
                {
                    var payUser = PayUserMgr.GetById(account);
                    if (payUser == null)
                    {
                        payUser = new PayUserEntity(account,0,0,0,DateTime.Now,new byte[0],point,0);
                    }
                    var curScore = payUser.ChargePoint;
                    var vipManager = VipManagerMgr.GetById(manager.Idx);
                    if (vipManager != null)
                    {
                        curScore += vipManager.VipExp;
                      //  VipManagerMgr.Update(vipManager);
                    }

                    var newlevel = CacheFactory.VipdicCache.GetVipLevel(curScore);
                    if (newlevel > manager.VipLevel)
                    {
                        manager.VipLevel = newlevel;
                        ManagerUtil.SaveManagerData(manager);
                        ManagerCore.Instance.DeleteCache(manager.Idx);
                    }
                   // ChatHelper.SendUpdateManagerInfoPop(manager.Idx, payUser.TotalPoint, true, manager.VipLevel);
                    if (point > 0)
                    {
                        ActivityExThread.Instance.Charge(manager.Idx, account, point);
                        ActivityExThread.Instance.Charge(manager.Idx, point);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeUpVip", ex);
            }
            
        }

        public static void ChargeAfter(string account, int point, int needPoint, DateTime curTime, decimal cash,
            int vipExp)
        {
            try
            {
                PayContinuingMgr.UpdateContinueday(account, point, needPoint, curTime.Date.AddDays(-1), curTime.Date,
                    curTime);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeUpContinue", ex);
            }
            var payUser = PayUserMgr.GetById(account);
            bool isInsert = false;
            if (payUser == null)
            {
                isInsert = true;
                payUser = new PayUserEntity(account, 0, 0, cash, DateTime.Now, new byte[0], point, 0);
            }
            else
            {
                payUser.TotalCash += cash;
                payUser.ChargePoint = point;
            }
            try
            {
                if (isInsert)
                {
                    if (!PayUserMgr.Insert(payUser))
                        SystemlogMgr.Error("charetUpPayUser", "统计钱失败", "Account：" + account + "_Cash" + cash);
                }
                else
                {
                    if (!PayUserMgr.Update(payUser))
                        SystemlogMgr.Error("charetUpPayUser", "统计钱失败", "Account：" + account + "_Cash" + cash);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("charetUpPayUser" + cash, ex);
            }
            var manager = ManagerCore.Instance.GetManager(account);
            try
            {
                if (manager != null)
                {
                    var curScore = vipExp;
                    var vipManager = VipManagerMgr.GetById(manager.Idx);
                    if (vipManager != null)
                    {
                        curScore += vipManager.VipExp;
                        vipManager.VipExp = curScore;
                        VipManagerMgr.Update(vipManager);
                    }
                    else
                    {
                        vipManager = new VipManagerEntity(manager.Idx, curScore, DateTime.Now, DateTime.Now,
                            DateTime.Now);
                        VipManagerMgr.Insert(vipManager);
                    }

                    var newlevel = CacheFactory.VipdicCache.GetVipLevel(curScore);
                    if (newlevel > manager.VipLevel)
                    {
                        manager.VipLevel = newlevel;
                        ManagerUtil.SaveManagerData(manager);
                        ManagerCore.Instance.DeleteCache(manager.Idx);
                    }
                    // ChatHelper.SendUpdateManagerInfoPop(manager.Idx, payUser.TotalPoint, true, manager.VipLevel);

                    ActivityExThread.Instance.Charge(manager.Idx, account, vipExp);
                    ActivityExThread.Instance.Charge(manager.Idx, vipExp);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ChargeUpVip", ex);
            }

        }

        /// <summary>
        /// 给用户添加点券到bonus里
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="bonus"></param>
        /// <param name="billingId"></param>
        /// <returns></returns>
        public MessageCode AddBonus(Guid managerId, int bonus, EnumChargeSourceType sourceType, string billingId, DbTransaction transaction = null)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return MessageCode.NbParameterError;
            return AddBonus(manager.Account, bonus, sourceType, billingId, transaction);
        }

        /// <summary>
        /// 给用户添加点券到bonus里
        /// </summary>
        /// /// <param name="account"></param>
        /// <param name="bonus"></param>
        /// <param name="billingId"></param>
        /// <returns></returns>
        public MessageCode AddBonus(string account, int bonus, EnumChargeSourceType sourceType, string billingId, DbTransaction transaction = null, string zoneId = "")
        {
            if (bonus <= 0)
                return MessageCode.NbParameterError;
            int result = 0;
            PayUserMgr.ChargeForBonus(account, (int)sourceType, billingId, bonus, ref result, transaction, zoneId);
            if (result == (int)MessageCode.PaySuccess)
            {
                return MessageCode.Success;
            }
            else
            {
                return (MessageCode)result;
            }
        }
        //Add for matchReward
        public MessageCode AddBonusV2(out int totalPoint, string account, int bonus, EnumChargeSourceType sourceType, string billingId, DbTransaction transaction = null, string zoneId = "")
        {
            totalPoint = -1;
            if (bonus <= 0)
                return MessageCode.NbParameterError;
            int result = 0;
            PayUserMgr.ChargeForBonusV2(account, (int)sourceType, billingId, bonus, ref totalPoint, ref result, transaction, zoneId);
            if (result == (int)MessageCode.PaySuccess)
            {
                return MessageCode.Success;
            }
            else
            {
                return (MessageCode)result;
            }
        }

        /// <summary>
        /// 给用户加点卷   购买钻石专用
        /// </summary>
        /// <param name="account"></param>
        /// <param name="point"></param>
        /// <param name="bonus"></param>
        /// <param name="sourceType"></param>
        /// <param name="billingId"></param>
        /// <param name="tans"></param>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public MessageCode AddPoint(string account, int point,int bonus, EnumChargeSourceType sourceType, string billingId,
            DbTransaction tans = null, string zoneId = "")
        {
            if (point <= 0)
                return MessageCode.NbParameterError;
            int result = 0;
            PayUserMgr.ChargeForPoint(account, (int)sourceType, billingId, point, bonus, ref result, tans, zoneId);
            if (result == (int)MessageCode.PaySuccess)
            {
                return MessageCode.Success;
            }
            else
            {
                return (MessageCode)result;
            }
        }

        public MessageCode AddBindPoint(string account, int bindPoint, EnumChargeSourceType sourceType, string billingId,
            DbTransaction transaction = null, string zoneId = "")
        {
            if (bindPoint <= 0)
                return MessageCode.NbParameterError;

            int result = 0;
            PayUserMgr.ChargeForBindPoint(account, (int)sourceType, billingId, bindPoint, ref result, transaction,zoneId);
            if (result == (int)MessageCode.PaySuccess)
            {
                return MessageCode.Success;
            }
            else
            {
                return (MessageCode)result;
            }
        }


        /// <summary>
        /// 联赛竞猜扣点券
        /// </summary>
        /// <param name="managerId">经理id</param>
        /// <param name="gamblePoint">竞猜点券</param>
        /// <param name="gambleId">竞猜记录id</param>
        /// <param name="trans">当前事务</param>
        /// <returns>返回MessageCode.Success表示成功</returns>
        public MessageCode LeagueGambleConsume(Guid managerId, int gamblePoint, Guid gambleId, DbTransaction trans = null)
        {
            //如果是联赛NPC的押注，需要根据account来扣除点券
            string account = "";
            //if (managerId == LeagueConst.GambleNpcId)
            //{
            //    account = _gambleAccount;
            //}
            //else
            //{
            //    var manager = ManagerCore.Instance.GetManager(managerId);
            //    if (manager == null)
            //        return MessageCode.NbParameterError;
            //    account = manager.Account;
            //}
            return ConsumePointForGamble(account, managerId, EnumConsumeSourceType.LeagueGamble, gambleId.ToString(), gamblePoint, trans);
        }

        /// <summary>
        /// 扣除点卷
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="gambelPoint">扣除点卷数</param>
        /// <param name="gambleId">订单ID</param>
        /// <param name="type">扣除点卷类型</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public MessageCode GambleConsume(Guid managerId, int gambelPoint, Guid gambleId, EnumConsumeSourceType type, DbTransaction trans = null)
        {
            string account = "";
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return MessageCode.NbParameterError;
            account = manager.Account;
            return ConsumePointForGamble(account, managerId, type, gambleId.ToString(), gambelPoint, trans);
        }

        /// <summary>
        /// 扣除绑定点券
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="bindPoint"></param>
        /// <param name="consumeId"></param>
        /// <param name="type"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public MessageCode ConsumeBindPoint(Guid managerId, int bindPoint, Guid consumeId, EnumConsumeSourceType type,
            DbTransaction trans = null)
        {
            string account = "";
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return MessageCode.NbParameterError;
            account = manager.Account;
            return ConsumeBindPoint(account, managerId, (int)type, consumeId.ToString(), bindPoint, trans);
        }

        /// <summary>
        /// 翻翻乐活动扣点卷
        /// </summary>
        /// <param name="managerId">经理ID</param>
        /// <param name="gambelPoint">扣除点卷数</param>
        /// <param name="gambleId">订单ID</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public MessageCode NDAGambleConsume(Guid managerId, int gambelPoint, Guid gambleId, DbTransaction trans = null)
        {
            string account = "";
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return MessageCode.NbParameterError;
            account = manager.Account;
            return ConsumePointForGamble(account, managerId, EnumConsumeSourceType.ArenaGamble, gambleId.ToString(), gambelPoint, trans);
        }

        /// <summary>
        /// 拍卖行扣点券
        /// </summary>
        /// <param name="managerId">经理id</param>
        /// <param name="needPoint">需要扣除的点券数</param>
        /// <param name="billing">订单号</param>
        /// <param name="trans">当前事务</param>
        /// <returns>返回MessageCode.Success表示成功</returns>
        public MessageCode Auction(Guid managerId, int needPoint, string billing, DbTransaction trans = null)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return MessageCode.NbParameterError;

            return ConsumePointForGamble(manager.Account, managerId, EnumConsumeSourceType.Auction, billing, needPoint, trans);
        }
        /// <summary>
        /// 竞猜真实比赛扣点券
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="needPoint"></param>
        /// <param name="billing"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public MessageCode GambleTrueMatch(Guid managerId, int needPoint, string billing, DbTransaction trans = null)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return MessageCode.NbParameterError;

            return ConsumePointForGamble(manager.Account, managerId, EnumConsumeSourceType.GambleTrueMatch, billing, needPoint, trans);
        }

        public MessageCode ConsumePointForGamble(string account, Guid managerId, int sourceType,
                                                 string orderId, int consumePoint, DbTransaction trans = null)
        {
            return ConsumePointForGamble(account, managerId, sourceType, orderId, consumePoint, trans, "");
        }

        public MessageCode ConsumePointForGamble(string account, Guid managerId, int sourceType,
                                                 string orderId, int consumePoint, DbTransaction trans = null, string siteId = "")
        {
            if (consumePoint <= 0)
                return MessageCode.NbParameterError;
            int result = (int)MessageCode.PayNoEnoughPoint;
            PayUserMgr.ConsumePointForGamble(account, managerId, sourceType, orderId, consumePoint, DateTime.Now, ref result, trans, siteId);
            if (result == (int)MessageCode.PaySuccess)
            {
                return MessageCode.Success;
            }
            else
            {
                return (MessageCode)result;
            }
        }

        public MessageCode ConsumeBindPoint(string account, Guid managerId, int sourceType, string orderId,
            int consumeBindPoint, DbTransaction trans = null, string siteId = "")
        {
            if (consumeBindPoint <= 0)
                return MessageCode.NbParameterError;
            int result = (int)MessageCode.PayNoEnoughPoint;
            PayUserMgr.ConsumeBindPoint(account, managerId, sourceType, orderId, consumeBindPoint, DateTime.Now,
                ref result, trans, siteId);
            if (result == (int)MessageCode.PaySuccess)
            {
                return MessageCode.Success;
            }
            else
            {
                return (MessageCode)result;
            }

        }

        public MessageCode ConsumePointForGamble(string account, Guid managerId, int sourceType,
                                                 string orderId, int consumePoint, string siteId)
        {
            return ConsumePointForGamble(account, managerId, sourceType, orderId, consumePoint, null, siteId);
        }

        /// <summary>
        /// 消费点券，除商城外
        /// </summary>
        /// <param name="account"></param>
        /// <param name="managerId"></param>
        /// <param name="sourceType">源类型</param>
        /// <param name="orderId">订单id，需唯一</param>
        /// <param name="consumePoint">消费点券数量</param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public MessageCode ConsumePointForGamble(string account, Guid managerId, EnumConsumeSourceType sourceType, string orderId, int consumePoint, DbTransaction trans = null)
        {
            return ConsumePointForGamble(account, managerId, (int)sourceType, orderId, consumePoint, trans);
        }
        #endregion

        #region encapsulation
        PayUserEntity CreateEntity()
        {
            PayUserEntity entity = new PayUserEntity();
            entity.RowTime = DateTime.Now;
            return entity;
        }

        #endregion
    }
}
