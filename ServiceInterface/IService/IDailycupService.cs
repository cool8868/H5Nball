using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using System.ServiceModel;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Dailycup;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IDailycupService
    {
        /// <summary>
        /// 获取杯赛数据
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="dailycupId"></param>
        /// <returns></returns>
        [OperationContract]
        DailycupFullDataResponse GetDailycupData(Guid managerId, int dailycupId);

        /// <summary>
        /// 报名杯赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse Attend(Guid managerId, bool hasTask);

        /// <summary>
        /// 杯赛竞猜任务，仅需打开
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="hasTask"></param>
        /// <returns></returns>
        [OperationContract]
        MessageCodeResponse AttendGambleTask(Guid managerId, bool hasTask);

        /// <summary>
        /// 竞猜杯赛
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="gamblePoint"></param>
        /// <param name="gambleResult"></param>
        /// <param name="matchId"></param>
        /// <returns></returns>
        [OperationContract]
        DailycupAttendGambleResponse AttendGamble(Guid managerId, int gamblePoint, int gambleResult, Guid matchId, bool hasTask);

        /// <summary>
        /// 获取我的杯赛历程
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MyDailycupMatchResponse GetMyDailycupMatch(Guid managerId);
        /// <summary>
        /// 创建杯赛
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MessageCode JobCreate();
        /// <summary>
        /// 发送奖励
        /// </summary>
        /// <param name="dailycupId"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        void JobSendPrize(int dailycupId);
        /// <summary>
        /// 竞猜开奖
        /// </summary>
        /// <param name="dailycupId"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        void JobOpenGamble(int dailycupId);
        /// <summary>
        /// 运行杯赛
        /// </summary>
        /// <param name="dailycupId"></param>
        /// <returns></returns>
        [OperationContract(IsOneWay = true)]
        void JobRunDailycup(int dailycupId);

        [OperationContract]
        string ResetCache(int cacheType);
    }
}
