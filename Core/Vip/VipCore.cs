using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Information;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.Task;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Friend;
using Games.NBall.Entity.Response.Match;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Vip
{
    public class VipCore
    {
        #region .ctor

        private int _monthCardPointNunber = 0;

        public VipCore(int p)
        {
            _monthCardPointNunber = CacheFactory.AppsettingCache.GetAppSettingToInt(
                EnumAppsetting.MonthCardPointNunber, 60);
           
        }
        #endregion

        #region Instance
        public static VipCore Instance
        {
            get { return SingletonFactory<VipCore>.SInstance; }
        }
        #endregion

        /// <summary>
        /// 月卡发奖
        /// </summary>
        /// <returns></returns>
        public MessageCode MonthCardSendPrize()
        {
            try
            {
                DateTime date = DateTime.Now;
                var prizeList = ManagerMonthcardMgr.GetPrizeList(date.Date);
                foreach (var entity in prizeList)
                {
                    try
                    {
                        if (entity.PrizeDate.Date >= date.Date)//已经发过奖励
                            continue;
                        int overdueDay = (entity.DueToTime.Date - date.Date).Days;
                        MailBuilder mail = null;
                        if (overdueDay > 0)
                            mail = new MailBuilder(entity.ManagerId, EnumMailType.MonthCard, _monthCardPointNunber,
                                overdueDay);
                        else
                            mail = new MailBuilder(entity.ManagerId, _monthCardPointNunber, EnumMailType.MonthCard1);
                        entity.PrizeDate = date;
                        entity.UpdateTime = date;
                        //if (ActivityExThread.Instance.IsActivity(Entity.Enums.Activity.EnumActivityExEffectType.EuropeCarnival, 0, 0))
                        //{
                        //    var prizeCode = ActivityExThread.Instance.GetRandomDebris();
                        //    if (prizeCode > 0)
                        //        mail.AddAttachment(1, prizeCode, false, 1);
                        //}
                        using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                        {
                            transactionManager.BeginTransaction();
                            var messageCode = MessageCode.NbUpdateFail;
                            do
                            {
                                if (!ManagerMonthcardMgr.Update(entity, transactionManager.TransactionObject))
                                    break;
                                if (!mail.Save(transactionManager.TransactionObject))
                                    break;
                                messageCode = MessageCode.Success;
                            } while (false);
                            if (messageCode == MessageCode.Success)
                                transactionManager.Commit();
                            else
                                transactionManager.Rollback();
                        }
                    }
                    catch (Exception ex)
                    {
                        SystemlogMgr.Error("发送月卡奖励出错1", ex);
                        return MessageCode.NbParameterError;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("发送月卡奖励出错", ex);
            }
            return MessageCode.Success;
        }
        /// <summary>
        /// 每周刷新Vip礼包
        /// </summary>
        public MessageCode RefreshVipPackageJob()
        {
            var d = DateTime.Now;
            if (d.DayOfWeek == DayOfWeek.Monday)
            {
                if (NbManagervippackageMgr.DayUpdate())
                {
                    return MessageCode.Success;
                }
            }
            return MessageCode.Exception;
        }
        public VipDataResponse DailyAttendVip(Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId);
            if(manager==null)
                return ResponseHelper.Create<VipDataResponse>(MessageCode.NbParameterError);

            if(manager.VipLevel<1)
                return ResponseHelper.Create<VipDataResponse>(MessageCode.NotVip);

            var vipManager = VipManagerMgr.GetById(managerId);
            if (vipManager == null)
                return ResponseHelper.Create<VipDataResponse>(MessageCode.NbParameterError);
            var date = DateTime.Now;
            //当日已签到
            if (vipManager.ReceiveDate.Date == date.Date)
                return ResponseHelper.Create<VipDataResponse>(MessageCode.HasReceiveToday);

            vipManager.VipExp += 10;
            vipManager.ReceiveDate = date.Date;
            vipManager.UpdateTime = date;

            if (VipManagerMgr.Update(vipManager))
            {
                return ManagerCore.Instance.GetVipData(managerId);
            }
            else
            {
                return ResponseHelper.Create<VipDataResponse>(MessageCode.FailUpdate);
                
            }

        }



    }
}
