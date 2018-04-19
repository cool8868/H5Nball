using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Common;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;

namespace Games.NBall.Core.League
{
    public class RevelationHistoryFrame
    {
        private RevelationMyhistoryEntity _historyEntity;
        private RevelationMyHistoryGoals _myGoalsEntity;

        private bool isUpdate = false;
        /// <summary>
        /// 进球列表
        /// </summary>
        private Dictionary<int, Dictionary<int, MyHistoryGoalsEntity>> GoalsDic { get; set; }
        public RevelationHistoryFrame(Guid managerId)
        {
            var history = RevelationMyhistoryMgr.GetById(managerId);
            if (history == null)
            {
                history = new RevelationMyhistoryEntity(managerId,new byte[0], DateTime.Now, DateTime.Now);
                RevelationMyhistoryMgr.Insert(history);
            }
            _historyEntity = history;
            AnalyseFightMap();
        }

        /// <summary>
        /// 获取进球记录
        /// </summary>
        /// <param name="markId"></param>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public MyHistoryGoalsEntity GetGoals(int markId, int schedule)
        {
            if (GoalsDic.ContainsKey(markId))
            {
                if (GoalsDic[markId].ContainsKey(schedule))
                    return GoalsDic[markId][schedule];
            }
            return null;
        }

        /// <summary>
        /// 更新进球
        /// </summary>
        /// <param name="markId"></param>
        /// <param name="schedule"></param>
        /// <param name="goals"></param>
        /// <param name="toConcede"></param>
        public void SetGoals(int markId, int schedule, int goals, int toConcede)
        {
            if (GoalsDic.ContainsKey(markId))
            {
                if (GoalsDic[markId].ContainsKey(schedule))
                {
                    var entity = GoalsDic[markId][schedule];
                    var history = entity.Goals - entity.ToConcede;
                    var newhistory = goals - toConcede;
                    if (newhistory > history)
                    {
                        entity.Goals = goals;
                        entity.ToConcede = toConcede;
                        isUpdate = true;
                    }
                }
                else
                {
                    var entity = new MyHistoryGoalsEntity();
                    entity.Goals = goals;
                    entity.ToConcede = toConcede;
                    GoalsDic[markId].Add(schedule, entity);
                    isUpdate = true;
                }
            }
            else
            {
                var entity = new MyHistoryGoalsEntity();
                entity.Goals = goals;
                entity.ToConcede = toConcede;
                var entityDic =new Dictionary<int, MyHistoryGoalsEntity>();
                entityDic.Add(schedule, entity);
                GoalsDic.Add(markId, entityDic);
                isUpdate = true;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save(DbTransaction trans = null)
        {
            if (!isUpdate)
                return true;
            var historyString = GenerateFightMapString();
            _historyEntity.GoalsString = historyString;
            _historyEntity.UpdateTime = DateTime.Now;
            if (!RevelationMyhistoryMgr.Update(_historyEntity, trans))
                return false;
            return true;
        }

        #region 解析对阵字符串
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>成功返回true</returns>
        private void AnalyseFightMap()
        {
            _myGoalsEntity = SerializationHelper.FromByte<RevelationMyHistoryGoals>(_historyEntity.GoalsString);
            if (_myGoalsEntity == null)
            {
                _myGoalsEntity = new RevelationMyHistoryGoals();
                _myGoalsEntity.History = new Dictionary<int, Dictionary<int, MyHistoryGoalsEntity>>();
            }
            else
            {
                if (_myGoalsEntity.History == null)
                    _myGoalsEntity.History = new Dictionary<int, Dictionary<int, MyHistoryGoalsEntity>>();
            }
            GoalsDic = _myGoalsEntity.History;
        }
        #endregion
        /// <summary>
        /// 获取对阵字符串
        /// </summary>
        private byte[] GenerateFightMapString()
        {
            _myGoalsEntity.History = GoalsDic;
            return SerializationHelper.ToByte(_myGoalsEntity);
        }
    }

}
