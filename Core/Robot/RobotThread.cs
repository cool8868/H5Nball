using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.CrossCrowd;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Response.Crowd;

namespace Games.NBall.Core.Robot
{
    public class RobotThread
    {
        private const int RobotLevel = 15;
        private const int RobotVip = 3;

        private Timer _timer = null;
        private EnumMatchType _curMatchType;
        private DateTime _endTime;

        public RobotThread(EnumMatchType matchType)
        {
            _curMatchType = matchType;
            _endTime = DateTime.Now;
            _crossRobotManagerDic = new ConcurrentDictionary<Guid, CrossrobotManagerEntity>();
            _timer = new Timer { Interval = 10000 };
            _timer.Elapsed += new ElapsedEventHandler(TimerCallBack);
        }

        private ConcurrentDictionary<Guid, CrossrobotManagerEntity> _crossRobotManagerDic { get; set; }
        
        void TimerCallBack(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            try
            {
                switch (_curMatchType)
                {
                    case EnumMatchType.CrossCrowd:
                        RobotCrossCrowdHookJob();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("TimerCallBack", ex);
            }
        }

        MessageCode InitRobotManager()
        {
            List<CrossrobotManagerEntity> crossList = null;
            switch (_curMatchType)
            {
                case EnumMatchType.CrossCrowd:
                    crossList = CrossrobotManagerMgr.GetCrossCrowd();
                    break;
                default:
                    return MessageCode.Success;
                    break;
            }
            if (_crossRobotManagerDic == null)
                _crossRobotManagerDic = new ConcurrentDictionary<Guid, CrossrobotManagerEntity>();
            if (crossList.Count > 0)
            {
                foreach (var entity in crossList)
                {
                    if (!_crossRobotManagerDic.ContainsKey(entity.Idx))
                        _crossRobotManagerDic.TryAdd(entity.Idx, entity);
                }
            }

            return MessageCode.Success;
        }

        public MessageCode RunHook(DateTime endTime)
        {
            try
            {
                var code = InitRobotManager();
                if (code != MessageCode.Success)
                    return code;
                _endTime = endTime;
                _timer.Start();
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("RunHook", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode RobotCrossCrowdHookJob()
        {
            if (DateTime.Now < _endTime)
            {
                try
                {
                    foreach (var entity in _crossRobotManagerDic.Values)
                    {
                        if (entity.CrossCrowd)
                        {
                            RobotCrossCrowdHook(entity);
                        }
                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("RobotCrossCrowdHookJob", ex);
                    return MessageCode.Exception;
                }
                finally
                {
                    _timer.Start();
                }
                return MessageCode.Success;
            }
            else
            {
                _timer.Stop();
                foreach (var entity in _crossRobotManagerDic.Values)
                {
                    doCrossCrowdFinish(entity);
                }

            }
            return MessageCode.CrowdNoData;
        }

        void doCrossCrowdFinish(CrossrobotManagerEntity entity)
        {
            CrossrobotManagerMgr.UpdateStatus(entity.Idx, (int)EnumRobotMatchStatus.Stop);
        }

        void RobotCrossCrowdHook(CrossrobotManagerEntity entity)
        {
            if (CrossCrowdFight(entity.SiteId, entity.Idx) == MessageCode.Success)
            {
                CrossrobotManagerMgr.UpdateStatus(entity.Idx, (int)EnumRobotMatchStatus.Run);
            }
        }

        MessageCode CrossCrowdFight(string siteId, Guid managerId)
        {
            try
            {
                var crossCrowdManagerInfo = CrossCrowdCore.Instance.GetManagerInfo(siteId, managerId);
                if (crossCrowdManagerInfo.Data.Morale <= 0)
                    return MessageCode.CrowdNoMorale;
                if (crossCrowdManagerInfo.Data.CdSeconds > 0 || crossCrowdManagerInfo.Data.ResurrectionCdSeconds > 0)
                    return MessageCode.CrowdHasCd;
                CrossCrowdManager.Instance.Attend(siteId, managerId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CrossCrowdFight", ex);
                return MessageCode.Exception;
            }
            return MessageCode.Success;
        }


        /// <summary>
        /// 启动机器人
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RobotResponse StartRobot(string siteId, Guid managerId)
        {
            var manager = ManagerCore.Instance.GetManager(managerId,false,siteId);
            if (manager == null)
                return ResponseHelper.Create<RobotResponse>(MessageCode.MissManager);
            if (manager.Level < RobotLevel)
                return ResponseHelper.Create<RobotResponse>(MessageCode.LackofManagerLevel);
            if (!CheckManagerVip(manager))
                return ResponseHelper.Create<RobotResponse>(MessageCode.LackofVipLevel);

            var crossRobotManager = CrossrobotManagerMgr.GetById(managerId);
            if (crossRobotManager == null)
            {
                crossRobotManager = RobotCore.BuildCrossRobotManager(siteId, managerId);
                SetHookStatus(crossRobotManager, true);
                if (!CrossrobotManagerMgr.Insert(crossRobotManager))
                    return ResponseHelper.Create<RobotResponse>(MessageCode.FailUpdate);
            }
            else
            {
                if (!GetHookStatus(crossRobotManager))
                {
                    SetHookStatus(crossRobotManager, true);
                    if (!CrossrobotManagerMgr.Update(crossRobotManager))
                        return ResponseHelper.Create<RobotResponse>(MessageCode.FailUpdate);
                }
            }
            if (!_crossRobotManagerDic.ContainsKey(managerId))
                _crossRobotManagerDic.TryAdd(managerId, crossRobotManager);
            var response = ResponseHelper.CreateSuccess<RobotResponse>();
            return RobotCore.BuildRobotResponse(response, crossRobotManager);
        }

        public RobotResponse StopRobot(Guid managerId)
        {
            CrossrobotManagerEntity crossRobotManager = null;
            var code = StopCrossRobotManager(managerId,  out crossRobotManager);
            
            if (code != MessageCode.Success)
                return ResponseHelper.Create<RobotResponse>(code);
            var response = ResponseHelper.CreateSuccess<RobotResponse>();
            return RobotCore.BuildRobotResponse(response, crossRobotManager);
        }

        MessageCode StopCrossRobotManager(Guid managerId, out CrossrobotManagerEntity entity)
        {
            entity = CrossrobotManagerMgr.GetById(managerId);
            if (entity == null || !GetHookStatus(entity))
                return MessageCode.InvalidArgs;
            SetHookStatus(entity, false);
            entity.Status = (int)EnumRobotMatchStatus.Stop;
            LogHelper.Insert("test",LogType.Info);
            if (!CrossrobotManagerMgr.Update(entity))
                return MessageCode.FailUpdate;
            LogHelper.Insert("test2", LogType.Info);
            CrossrobotManagerEntity newEntity = null;
            _crossRobotManagerDic.TryRemove(managerId, out newEntity);
            return MessageCode.Success;
        }

        bool CheckManagerVip(NbManagerEntity manager)
        {
            if (manager.VipLevel < RobotVip)
                return false;
            return true;
        }
        
        bool GetHookStatus(CrossrobotManagerEntity entity)
        {
            switch (_curMatchType)
            {
                case EnumMatchType.CrossCrowd:
                    return entity.CrossCrowd;
                default:
                    return false;
            }
        }

        void SetHookStatus(CrossrobotManagerEntity entity,bool status)
        {
            switch (_curMatchType)
            {
                case EnumMatchType.CrossCrowd:
                    entity.CrossCrowd = status;
                    break;
                default:
                    break;
            }
        }
    }
}
