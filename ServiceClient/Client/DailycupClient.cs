using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Dailycup;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class DailycupClient
    {
        private static IDailycupService proxy = ServiceProxy<IDailycupService>.Create("NetTcp_IDailycupService");
        /// <summary>
        /// 获取杯赛数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="dailycupId"></param>
        /// <returns></returns>
        public DailycupFullDataResponse GetDailycupData(Guid managerId, int dailycupId)
        {
            return proxy.GetDailycupData(managerId, dailycupId);
        }

        /// <summary>
        /// 报名杯赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse Attend(Guid managerId, bool hasTask)
        {
            return proxy.Attend(managerId,hasTask);
        }

        /// <summary>
         /// 杯赛竞猜任务，仅需打开
         /// </summary>
         /// <param name="managerId"></param>
         /// <param name="hasTask"></param>
         /// <returns></returns>
        public MessageCodeResponse AttendGambleTask(Guid managerId, bool hasTask)
        {
            return proxy.AttendGambleTask(managerId, hasTask);
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
            return proxy.AttendGamble(managerId, gamblePoint, gambleResult, matchId, hasTask);
        }

        /// <summary>
        /// 获取我的杯赛历程
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MyDailycupMatchResponse GetMyDailycupMatch(Guid managerId)
        {
            return proxy.GetMyDailycupMatch(managerId);
        }

        /// <summary>
        /// 创建杯赛
        /// </summary>
        /// <returns></returns>
        public MessageCode JobCreate()
        {
            return proxy.JobCreate();
        }

        /// <summary>
        /// 发送奖励
        /// </summary>
        /// <param name="dailycupId"></param>
        /// <returns></returns>
        public void JobSendPrize(int dailycupId)
        {
            proxy.JobSendPrize(dailycupId);
        }
        /// <summary>
        /// 竞猜开奖
        /// </summary>
        /// <param name="dailycupId"></param>
        /// <returns></returns>
        public void JobOpenGamble(int dailycupId)
        {
            proxy.JobOpenGamble(dailycupId);
        }
        /// <summary>
        /// 运行杯赛
        /// </summary>
        /// <param name="dailycupId"></param>
        /// <returns></returns>
        public void JobRunDailycup(int dailycupId)
        {
            proxy.JobRunDailycup(dailycupId);
        }

        public string ResetCache(int cacheType)
        {
            return proxy.ResetCache(cacheType);
        }
    }
}
