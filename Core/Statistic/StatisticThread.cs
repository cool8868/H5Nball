using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Support;

namespace Games.NBall.Core.Statistic
{
    public class StatisticThread
    {
        #region .ctor
        public StatisticThread(int p)
        {

        }
        #endregion

        #region Facade
        public static StatisticThread Instance
        {
            get { return SingletonFactory<StatisticThread>.SInstance; }
        }

        public MessageCode JobCreateRecord()
        {
            try
            {
                StatisticDetailMgr.Create(ShareUtil.ZoneId, DateTime.Today);
                StatisticKpiMgr.Create(ShareUtil.ZoneId, DateTime.Today);
                StatisticOnlineMgr.Create(ShareUtil.ZoneId, DateTime.Today);
                StatisticInfoMgr.Create(ShareUtil.ZoneId);
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("JobCreateRecord", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode JobGetOnlineData()
        {
            try
            {
                int userCount = 0;
                long totalTime = 0;
                DateTime curTime = DateTime.Now;
                NbUserMgr.GetOnline(ref userCount, ref totalTime);
                var hour = curTime.Hour;
                StatisticOnlineMgr.Update(ShareUtil.ZoneId, hour.ToString(), curTime.Date, curTime, userCount, totalTime);
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("JobGetOnlineData", ex);
                return MessageCode.Exception;
            }
            return MessageCode.Exception;
        }

        public MessageCode JobGetDetail()
        {
            DateTime curTime = DateTime.Now;
            DateTime recordTime = DateTime.Now.AddHours(-1);
            return GetDetail(curTime, recordTime);
        }

        public MessageCode GetDetail(DateTime curTime, DateTime recordTime)
        {
            try
            {
                int loginUser = 0;
                int chargeUser = 0;
                int chargeCash = 0;
                int consumePoint = 0;
                int newUser = 0;
                int newMangaer = 0;

                var hour = recordTime.Hour;
                var starTime = recordTime.Date.AddHours(hour);
                var endTime = starTime.AddHours(1).AddMilliseconds(-3);
                NbUserMgr.GetDetail(starTime, endTime, ref loginUser, ref chargeUser, ref chargeCash, ref consumePoint,ref newUser,ref newMangaer);
                var hourStr = hour.ToString();
                SaveDetail(EnumAnalyseType.Login, loginUser,curTime, recordTime, hourStr);
                SaveDetail(EnumAnalyseType.ChargeUser, chargeUser, curTime, recordTime, hourStr);
                SaveDetail(EnumAnalyseType.ChargeCash, chargeCash, curTime, recordTime, hourStr);
                SaveDetail(EnumAnalyseType.ConsumePoint, consumePoint, curTime, recordTime, hourStr);
                SaveDetail(EnumAnalyseType.NewUser, newUser, curTime, recordTime, hourStr);
                SaveDetail(EnumAnalyseType.NewManager, newMangaer, curTime, recordTime, hourStr);
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("JobGetDetail", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode JobGetYesterdayKpi()
        {
            DateTime curTime = DateTime.Now;
            DateTime recordTime = curTime.Date.AddMilliseconds(-3);
            return GetKpi(curTime, recordTime,ShareUtil.ZoneId);
        }

        public MessageCode JobGetTodayKpi()
        {
            DateTime curTime = DateTime.Now;
            return GetKpi(curTime, curTime, ShareUtil.ZoneId);
        }

        public MessageCode GetKpi(DateTime curTime,DateTime recordTime,int zoneId,string siteName="")
        {
            try
            {
                string recordMonth = recordTime.ToString("yyyyMM");
                int totalUser=0;
                int totalManager = 0;
                int dau=0;
                int dUniqueIp=0;
                int dNewUser=0;
                int dNewManager=0;
                int dLostUser7=0;
                int dLostUser15=0;
                int dLostUser30=0;
                int retention2=0;
                int retention3=0;
                int retention4=0;
                int retention5=0;
                int retention6=0;
                int retention7=0;
                int retention15=0;
                int retention30=0;
                int wau=0;
                int wLost=0;
                int wHonor=0;
                int wHonorLost=0;
                int mau=0;
                int payUserCount=0;
                int payCount = 0;
                int payTotal=0;
                long paySum = 0;
                int payFirst=0;
                int payWLost=0;
                int lTV=0;
                long pointRemain=0;
                long pointConsume = 0;
                long pointCirculate = 0;
                int getPoint = 0;
                long getCoin = 0;
                long coinConsume = 0;
                int energyConsume = 0;
                for (int i=0;i>-31;i--)
                {
                    try
                    {
                        var time = recordTime.AddDays(i);
                        NbUserMgr.GetKpi(time, ref totalUser, ref totalManager, ref dau, ref dUniqueIp, ref dNewUser,
                                   ref dNewManager, ref dLostUser7, ref dLostUser15, ref dLostUser30, ref retention2,
                                   ref retention3, ref retention4, ref retention5, ref retention6, ref retention7,
                                   ref retention15, ref retention30, ref wau, ref wLost, ref wHonor, ref wHonorLost, ref mau,
                                   ref payUserCount, ref payCount, ref payTotal, ref paySum, ref payFirst, ref payWLost, ref lTV, ref pointRemain, ref pointConsume, ref pointCirculate, null, siteName);
                        StatisticKpiMgr.Update(zoneId, recordMonth, time.Date, totalUser, totalManager, dau, dUniqueIp, dNewUser, dNewManager,
                                               dLostUser7, dLostUser15, dLostUser30, retention2, retention3, retention4, retention5,
                                               retention6, retention7, retention15, retention30, wau, wLost, wHonor, wHonorLost, mau,
                                               payUserCount, payCount, payTotal, paySum, payFirst, pointRemain, pointConsume, pointCirculate, curTime);
                    }
                    catch (Exception ex)
                    {
                        
                      
                    }
                   

                }
              

                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("JobGetKpi", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode GetKpiImmediate(DateTime curTime,  int zoneId, string siteName = "")
        {
            try
            {
                string recordMonth = curTime.ToString("yyyyMM");
                int registerUser = 0;
                int registerManager = 0;
                int dau = 0;
                int dUniqueIp = 0;
                int dNewUser = 0;
                int dNewManager = 0;
                int curOnline = 0;
                int payUserCount = 0;
                int payCount = 0;
                int payTotal = 0;
                long paySum = 0;
                int payFirst = 0;
                NbUserMgr.GetKpiImmediate(curTime, ref  registerUser, ref  registerManager, ref  dau, ref  dUniqueIp, ref  dNewUser, ref  dNewManager, ref  payUserCount, ref  payCount, ref  payTotal, ref  paySum, ref  payFirst, ref  curOnline, null, siteName);
                StatisticKpiMgr.UpdateImmediate(zoneId, recordMonth, curTime.Date, registerUser, registerManager, dau, dUniqueIp, dNewUser, dNewManager, payUserCount, payCount, payTotal, paySum, payFirst, curOnline, curTime);

                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetKpiImmediate", ex);
                return MessageCode.Exception;
            }
        }

        public MessageCode JobGetTotal()
        {
            int totalUser = 0;
            int totalManager = 0;
            long totalPay = 0;
            long pointRemain = 0;
            long onlineMinutes = 0;

            NbUserMgr.GetTotal(ref totalUser, ref totalManager, ref totalPay, ref pointRemain, ref onlineMinutes);
            StatisticInfoMgr.Update(ShareUtil.ZoneId, totalUser, totalManager, totalPay, pointRemain, onlineMinutes,
                                    DateTime.Now);
            return MessageCode.Success;
        }
        #endregion

        #region encapsulation

        void SaveDetail(EnumAnalyseType analyseType, int value,DateTime curTime,DateTime recordTime, string hour)
        {
            StatisticDetailMgr.Update(ShareUtil.ZoneId, (int)analyseType, hour, recordTime.Date, curTime, value);
        }

        #endregion
    }
}
