using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.CrossCrowd;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Crowd;

namespace Games.NBall.Core.Robot
{
    public class RobotCore
    {
        private Dictionary<int, RobotThread> _robotThreadDic; 

        #region .ctor
        public RobotCore(int p)
        {
            _robotThreadDic=new Dictionary<int, RobotThread>();
            AddThread(EnumMatchType.CrossCrowd);
        }

        #endregion

        #region Facade
        public static RobotCore Instance
        {
            get { return SingletonFactory<RobotCore>.SInstance; }
        }


        public MessageCode RunHook(EnumMatchType matchType, DateTime endTime)
        {
            var thread = GetThread(matchType);
            if (thread == null)
                return MessageCode.NbParameterError;
            else
            {
                return thread.RunHook(endTime);
            }
        }


        public RobotResponse RobotInfo(string siteId, Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId,false,siteId);
            if (manager == null)
                return ResponseHelper.Create<RobotResponse>(MessageCode.MissManager);

            var crossRobotManager = CrossrobotManagerMgr.GetById(managerId);
            if (crossRobotManager == null)
            {
                crossRobotManager = BuildCrossRobotManager(siteId, managerId);
                if (!CrossrobotManagerMgr.Insert(crossRobotManager))
                    return ResponseHelper.Create<RobotResponse>(MessageCode.FailUpdate);
            }

            var response = ResponseHelper.CreateSuccess<RobotResponse>();
            return BuildRobotResponse(response, crossRobotManager);
        }

        /// <summary>
        /// 启动机器人
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RobotResponse StartRobot(string siteId, Guid managerId, int matchType)
        {
            var thread = GetThread(matchType);
            if (thread == null)
                return ResponseHelper.Create<RobotResponse>(MessageCode.NbParameterError);
            else
            {
                return thread.StartRobot(siteId,managerId);
            }
        }

        /// <summary>
        /// 手动停止机器人
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RobotResponse StopRobot(Guid managerId, int matchType)
        {
            var thread = GetThread(matchType);
            if (thread == null)
                return ResponseHelper.Create<RobotResponse>(MessageCode.NbParameterError);
            else
            {
                return thread.StopRobot(managerId);
            }
        }

        public static CrossrobotManagerEntity BuildCrossRobotManager(string siteId, Guid managerId)
        {
            CrossrobotManagerEntity entity = new CrossrobotManagerEntity();
            entity.Idx = managerId;
            entity.SiteId = siteId;
            entity.CrossCrowd = false;
            entity.Status = 0;
            entity.RowTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            return entity;
        }

        public static RobotResponse BuildRobotResponse(RobotResponse response, CrossrobotManagerEntity crossRobotManager)
        {
            response.Data = new RobotEntity();
            if (crossRobotManager == null)
            {
                response.Data.CrossCrowd = false;
            }
            else
            {
                response.Data.CrossCrowd = crossRobotManager.CrossCrowd;
            }
            return response;
        }
        #endregion

        #region encapsulation


        private RobotThread GetThread(EnumMatchType matchType)
        {
            return GetThread((int) matchType);
        }

        RobotThread GetThread(int matchType)
        {
            if (_robotThreadDic.ContainsKey(matchType))
                return _robotThreadDic[matchType];
            return null;
        }

        void AddThread(EnumMatchType matchType)
        {
            _robotThreadDic.Add((int)matchType, new RobotThread(matchType));
        }


        #endregion
    }
}
