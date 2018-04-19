using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Dailycup;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Dailycup;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
     [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class DailycupService:IDailycupService
    {
        /// <summary>
        /// 获取杯赛数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="dailycupId"></param>
        /// <returns></returns>
        public DailycupFullDataResponse GetDailycupData(Guid managerId, int dailycupId)
        {
            return ResponseHelper.TryCatch(() => DailycupCore.Instance.GetDailycupData(managerId, dailycupId));
        }

        /// <summary>
        /// 报名杯赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse Attend(Guid managerId, bool hasTask)
        {
            //if (!ManagerUtil.CheckFunction(managerId, EnumOpenFunction.Dailycup))
            //    return ResponseHelper.InvalidFunction<MessageCodeResponse>();
            return ResponseHelper.TryCatch(() => DailycupCore.Instance.Attend(managerId,hasTask));
        }

         /// <summary>
         /// 杯赛竞猜任务，仅需打开
         /// </summary>
         /// <param name="managerId"></param>
         /// <param name="hasTask"></param>
         /// <returns></returns>
         public MessageCodeResponse AttendGambleTask(Guid managerId, bool hasTask)
         {
             return ResponseHelper.TryCatch(() => DailycupCore.Instance.AttendGambleTask(managerId, hasTask));
         }

         /// <summary>
        /// 竞猜杯赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="gamblePoint"></param>
        /// <param name="gambleResult"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        public DailycupAttendGambleResponse AttendGamble(Guid managerId, int gamblePoint, int gambleResult,
                                                         Guid matchId, bool hasTask)
        {
            return ResponseHelper.TryCatch(() => DailycupCore.Instance.AttendGamble(managerId, gamblePoint, gambleResult, matchId, hasTask));
        }

        /// <summary>
        /// 获取我的杯赛历程
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MyDailycupMatchResponse GetMyDailycupMatch(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => DailycupCore.Instance.GetMyDailycupMatch(managerId));
        }

         /// <summary>
         /// 创建杯赛
         /// </summary>
         /// <returns></returns>
         public MessageCode JobCreate()
         {
             try
             {
                 return DailycupAssociation.Instance.CreateDailycup();
             }
             catch (Exception ex)
             {
                 SystemlogMgr.Error("JobCreate", ex);
                 return MessageCode.Exception;
             }
         }

         /// <summary>
         /// 发送奖励
         /// </summary>
         /// <param name="dailycupId"></param>
         /// <returns></returns>
         public void JobSendPrize(int dailycupId)
         {
             try
             {
                 DailycupAssociation.Instance.SendPrize(dailycupId);
             }
             catch (Exception ex)
             {
                 SystemlogMgr.Error("JobSendPrize", ex);
             }
         }
         /// <summary>
         /// 竞猜开奖
         /// </summary>
         /// <param name="dailycupId"></param>
         /// <returns></returns>
         public void JobOpenGamble(int dailycupId)
         {
             try
             {
                 DailycupAssociation.Instance.OpenGamble(dailycupId);
             }
             catch (Exception ex)
             {
                 SystemlogMgr.Error("JobOpenGamble", ex);
             }
         }
         /// <summary>
         /// 运行杯赛
         /// </summary>
         /// <param name="dailycupId"></param>
         /// <returns></returns>
         public void JobRunDailycup(int dailycupId)
         {
             try
             {
                 DailycupAssociation.Instance.RunDailycup(dailycupId);
             }
             catch (Exception ex)
             {
                 SystemlogMgr.Error("JobRunDailycup", ex);
             }
         }

         public string ResetCache(int cacheType)
         {
             return CacheManager.Instance.ResetCache(cacheType);
         }
    }
}
