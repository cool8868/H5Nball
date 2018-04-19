using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response.Coach;

namespace Games.NBall.Core.Coach
{
    public class CoachFrame
    {
        public CoachManagerEntity Entity
        {
            get { return _entity; }
        }

        private CoachManagerEntity _entity;
        private Dictionary<int, CoachFrameEntity> CoachDic;
        private bool isInsert = false;
        public CoachFrame(Guid managerId)
        {
            _entity = CoachManagerMgr.GetById(managerId);
            if (_entity == null)
            {
                _entity = new CoachManagerEntity(managerId);
                isInsert = true;
            }
            AnalyseTurntable();
        }

        /// <summary>
        /// 获取教练列表
        /// </summary>
        /// <returns></returns>
        public List<CoachFrameEntity> GetCoachList()
        {
            return CoachDic.Values.ToList();
        }

        /// <summary>
        /// 随机获取一个未满星的教练碎片
        /// </summary>
        /// <returns></returns>
        public int GetCoachItemCode()
        {
            //默认是高级教练证书
            var itemCode = 310179;
            var coachList = new List<int>();
            //获取所有教练配置
            var allCoachList = CacheFactory.CoachCache.GetAllCoach();
            foreach (var item in allCoachList)
            {
                if (CoachDic.ContainsKey(item.Idx))//已经激活了的
                {
                    if (CoachDic[item.Idx].IsMaxStar)
                        continue;
                }
                coachList.Add(item.Idx);
            }
            if (coachList.Count == 0)
                return itemCode;
            var random = RandomHelper.GetInt32(0, coachList.Count - 1);
            var coachId = coachList[random];
            var coachInfo = CacheFactory.CoachCache.GetCoachInfo(coachId);
            if (coachInfo != null)
                itemCode = coachInfo.DebrisCode;
            return itemCode;
        }

        /// <summary>
        /// 获取一个教练
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public CoachFrameEntity GetCoachInfo(int coachId)
        {
            if (CoachDic.ContainsKey(coachId))
                return CoachDic[coachId];
            return null;
        }

        /// <summary>
        /// 获取是否有这个教练
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public bool IsHaveCoach(int coachId)
        {
            return CoachDic.ContainsKey(coachId);
        }

        /// <summary>
        /// 替换教练
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public bool ReplaceCoach(int coachId)
        {
            var info = GetCoachInfo(coachId);
            if (info == null)
                return false;
            _entity.EnableCoachId = coachId;
            _entity.EnableCoachLevel = info.CoachLevel;
            _entity.EnableCoachSkillLevel = info.SkillLevel;
            _entity.EnableCoachStar = info.CoachStar;
            return true;
        }

        /// <summary>
        /// 激活教练
        /// </summary>
        /// <param name="coachId"></param>
        public bool TheActivation(int coachId)
        {
            if (IsHaveCoach(coachId))
                return false;
            var config = CacheFactory.CoachCache.GetCoachInfo(coachId);
            if (config == null)
                return false;
            var upgradeConfig = CacheFactory.CoachCache.GetCoachUpgradeInfo(1);
            var starConfig = CacheFactory.CoachCache.GetCoachStarInfo(coachId, 0);
            if (upgradeConfig == null || starConfig == null)
                return false;
            var coachInfo = new CoachFrameEntity(coachId, config.Offensive, config.Organizational, config.Defense, config.BodyAttr, config.Goalkeeping, upgradeConfig.UpgradeExp, starConfig.ConsumeDebris);
            CoachDic.Add(coachId, coachInfo);
            if (_entity.EnableCoachId == 0)
            {
                _entity.EnableCoachId = coachId;
                _entity.EnableCoachLevel = 1;
                _entity.EnableCoachStar = 0;
                _entity.EnableCoachSkillLevel = 1;
                _entity.Offensive += config.Offensive;
                _entity.Organizational += config.Organizational;
                _entity.Defense += config.Defense;
                _entity.BodyAttr += config.BodyAttr;
                _entity.Goalkeeping += config.Goalkeeping;
            }
            return true;
        }

        /// <summary>
        /// 教练升级
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns>需要扣除的经验</returns>
        public int CoachUpgarde(int coachId)
        {
            int costExp = 0;
            var info = GetCoachInfo(coachId);
            int needExp = info.NeedCoachExp - info.CoachExp;
            //能升级
            if (_entity.HaveExp >= needExp)
            {
                info.CoachLevel ++;
                if (info.CoachLevel >= 80)
                    info.IsMaxLevel = true;
                costExp = info.NeedCoachExp - info.CoachExp;
                info.CoachExp = 0; 
                var needConfig = CacheFactory.CoachCache.GetCoachUpgradeInfo(info.CoachLevel);
                info.NeedCoachExp = needConfig.UpgradeExp;
                CalculateKpi(info);
                _entity.HaveExp = _entity.HaveExp - costExp;
            }
            else
            {
                info.CoachExp = info.CoachExp + _entity.HaveExp;
                costExp = _entity.HaveExp;
                _entity.HaveExp = 0;
            }
            return costExp;
        }

        /// <summary>
        /// 教练技能升级
        /// </summary>
        /// <param name="coachId"></param>
        /// <param name="maxSkillLevel"></param>
        /// <returns></returns>
        public MessageCode CoachSkillUpgrade(int coachId,int maxSkillLevel)
        {
            var info = GetCoachInfo(coachId);
            if (info.SkillLevel >= maxSkillLevel)
                return MessageCode.CoachStarNot;
            info.SkillLevel++;
            if (info.SkillLevel >= 80)
                info.IsMaxSkillLevel = true;
            return MessageCode.Success;
        }

        /// <summary>
        /// 教练升星
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        public MessageCode CoachStarUpgrade(int coachId)
        {
            var info = GetCoachInfo(coachId);
            info.CoachStar ++;
            if (info.CoachStar >= 5)
                info.IsMaxStar = true;
            CalculateKpi(info);
            return MessageCode.Success;
        }

        /// <summary>
        /// 计算所有教练的属性
        /// </summary>
        private void CalculateKpi()
        {
            //_entity.Offensive = 0;
            //_entity.Organizational = 0;
            //_entity.Defense = 0;
            //_entity.BodyAttr = 0;
            //_entity.Goalkeeping = 0;
            //foreach (var item in CoachDic.Values)
            //{
            //    _entity.Offensive += item.Offensive;
            //    _entity.Organizational += item.Organizational;
            //    _entity.Defense += item.Defense;
            //    _entity.BodyAttr += item.BodyAttr;
            //    _entity.Goalkeeping += item.Goalkeeping;
            //}
        }

        /// <summary>
        /// 计算一个教练的属性
        /// </summary>
        /// <param name="coachInfo"></param>
        /// <returns></returns>
        private bool CalculateKpi(CoachFrameEntity coachInfo)
        {
            var config = CacheFactory.CoachCache.GetCoachInfo(coachInfo.CoachId);
            if (config == null)
                return false;
            int coachLevel = coachInfo.CoachLevel - 1;
            coachInfo.Offensive = Kpi(config.Offensive, coachLevel, coachInfo.CoachStar);
            coachInfo.Organizational = Kpi(config.Organizational, coachLevel, coachInfo.CoachStar);
            coachInfo.Defense = Kpi(config.Defense, coachLevel, coachInfo.CoachStar);
            coachInfo.BodyAttr = Kpi(config.BodyAttr, coachLevel, coachInfo.CoachStar);
            coachInfo.Goalkeeping = Kpi(config.Goalkeeping, coachLevel, coachInfo.CoachStar);

            CalculateKpi();
            return true;
        }

        /// <summary>
        /// 计算某一个属性
        /// </summary>
        /// <param name="originalKpi">原始属性</param>
        /// <param name="coachLevel">教练等级</param>
        /// <param name="coachStar">教练星级</param>
        /// <returns></returns>
        private decimal Kpi(decimal originalKpi, int coachLevel, int coachStar)
        {
            return originalKpi + originalKpi * coachLevel / 100 + originalKpi * coachStar / 5;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save(DbTransaction trans = null)
        {
            var coachString = GenerateFightMapString();
            _entity.CoachString = coachString;
            _entity.UpdateTime = DateTime.Now;
            if (isInsert)
            {
                if (!CoachManagerMgr.Insert(_entity, trans))
                    return false;
            }
            else if (!CoachManagerMgr.Update(_entity, trans))
                return false;

            return true;
        }

        #region 解析字符串
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>成功返回true</returns>
        private void AnalyseTurntable()
        {
            CoachDic = SerializationHelper.FromByte<Dictionary<int,CoachFrameEntity>>(_entity.CoachString);
            if (CoachDic == null) CoachDic = new Dictionary<int, CoachFrameEntity>();
        }
        #endregion
        /// <summary>
        /// 获取字符串
        /// </summary>
        private byte[] GenerateFightMapString()
        {
            return SerializationHelper.ToByte(CoachDic);
        }
    }
}
