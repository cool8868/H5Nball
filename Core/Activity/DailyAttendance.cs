using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response.Activity;

namespace Games.Dpm.Core.Activity
{
    public class DailyAttendance
    {
        public DailyAttendance(int p)
        {
            IsInstance = false;
        }

        public static DailyAttendance Instance
        {
            get { return SingletonFactory<DailyAttendance>.SInstance; }
        }
        /// <summary>
        /// 今天是否签到 
        /// </summary>
        private bool IsAttendance { get; set; }
        private bool IsInstance { get; set; }
        /// <summary>
        /// 今天是否签到 
        /// </summary>
        public bool GetIsAttendance(Guid managerId)
        {
            try
            {
                //if (IsInstance)
                //    return IsAttendance;
                var manager = ManagerCore.Instance.GetManager(managerId);
                int days;
                var dailyAttendManager = GetManager(managerId, manager.Name, out days);
                var isAttendance = dailyAttendManager.IsAttend;
                //IsInstance = true;
                return isAttendance;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取是否签到", ex);
                return true;
            }
        }

        /// <summary>
        /// 获取签到信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public DailyAttendanceInfoResponse GetDailyAttendanceInfo(Guid managerId)
        {
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if(manager==null)
                    return ResponseHelper.InvalidParameter<DailyAttendanceInfoResponse>();

                int days;
                var dailyAttendManager = GetManager(managerId, manager.Name, out days);
                var response = ResponseHelper.Create<DailyAttendanceInfoResponse>(MessageCode.Success);
                response.Data = new DailyAttendanceInfo
                {
                    AttendTimes = dailyAttendManager.AttendTimes,
                    MaxAttendTimes = days,
                    IsAttend = dailyAttendManager.IsAttend
                };
                //IsAttendance = dailyAttendManager.IsAttend;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetDailyAttendanceInfo", ex);
                return ResponseHelper.Create<DailyAttendanceInfoResponse>(MessageCode.Exception);
            } 
        }

        /// <summary>
        /// 当前签到奖励
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public DailyAttendancePrizeResponse AttendancePrize(Guid managerId)
        {
            try
            {
                var manager = ManagerCore.Instance.GetManager(managerId);
                if (manager == null)
                    return ResponseHelper.InvalidParameter<DailyAttendancePrizeResponse>();
                int days;
                var dailyAttendManager = GetManager(managerId, manager.Name, out days);
                if (dailyAttendManager.IsAttend)
                    return ResponseHelper.Create<DailyAttendancePrizeResponse>(MessageCode.PrizeHaveSend);
                int curTimes = ++dailyAttendManager.AttendTimes;
                var prizeEntity = CacheFactory.DayAttendCache.GetPrizeEntity(curTimes);
                if (prizeEntity == null)
                    return ResponseHelper.Create<DailyAttendancePrizeResponse>(MessageCode.ActivityNoConfigPrize);

                //奖励发送
                var code = SendPrize(managerId, prizeEntity);
                if (code != MessageCode.Success)
                    return ResponseHelper.Create<DailyAttendancePrizeResponse>(code);

                dailyAttendManager.IsAttend = true;
                //IsAttendance = true;
                dailyAttendManager.UpdateTime = DateTime.Now;
                if (!DailyattendanceManagerMgr.Update(dailyAttendManager))
                    return ResponseHelper.Create<DailyAttendancePrizeResponse>(MessageCode.FailUpdate);
                var response = ResponseHelper.Create<DailyAttendancePrizeResponse>(MessageCode.Success);
                response.Data = new DailyAttendancePrize();
                response.Data.Prize = prizeEntity;
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("DailyAttendancePrize", ex);
                return ResponseHelper.Create<DailyAttendancePrizeResponse>(MessageCode.Exception);
            }
        }

        private MessageCode SendPrize(Guid managerId, ConfigDaysattendprizeEntity prizeEntity)
        {
            bool doublePrize = false;
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return MessageCode.NbParameterError;
            //是否有双倍奖励
            if (prizeEntity.HasDouble && manager.VipLevel >= prizeEntity.DoubleVipLevel)
                doublePrize = true;

            var code = MessageCode.Success;
            switch (prizeEntity.PrizeType)
            {
                case 1://点券
                    var point = prizeEntity.Count;
                    if (doublePrize)
                        point = point*2;
                    code = PayCore.Instance.AddBonus(managerId, point, EnumChargeSourceType.LeaguePrize, ShareUtil.GenerateComb().ToString(), null);
                    break;
                case 2://奖励物品
                    var count = prizeEntity.Count;
                    if (doublePrize)
                        count = count * 2;

                    //获取背包
                    var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.LeaguePrize);
                    if (package == null)
                        return MessageCode.NbNoPackage;

                    code = package.AddItems(prizeEntity.PrizeItemCode, count, 1, prizeEntity.IsBinding, false);
                    if (package.Save())
                        package.Shadow.Save();
                    break;
            }
            return code;
        }


       public DailyattendanceManagerEntity GetManager(Guid managerId, string name, out int days)
        {
            var dtNow = DateTime.Now.Date;
            var curMonth = dtNow.Month;
            days = DateTime.DaysInMonth(dtNow.Year, curMonth);
            var manager = DailyattendanceManagerMgr.GetManager(managerId, name, curMonth);
            if (manager.Month != curMonth)
            {
                //IsInstance = false;
                manager = DailyattendanceManagerMgr.UpdateMonth(managerId, curMonth);
            }
            if (dtNow != manager.UpdateTime.Date)
            {
                //IsInstance = false;
                manager = DailyattendanceManagerMgr.UpdateStatus(managerId);
            }
            
            return manager;
        }
    }
}
