using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Model.TranIn;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Match;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class NpcdicCache : BaseSingleton
    {
        private Dictionary<Guid, Match_FightManagerinfo> _fightManagerinfoDic;
        private Dictionary<Guid, ManagerInput> _transferManagerDic;
        private Dictionary<Guid, DTOBuffMemberView> _dicBuffView;
        private Dictionary<Guid, DicNpcEntity> _dicNpc;

        private List<PlayerInput> _guidePlayers;
        #region encapsulation
        public NpcdicCache(int p)
            : base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            try
            {
                LogHelper.Insert("npc dic cache init start", LogType.Info);
                List<DicNpcEntity> list = null;
                if (ShareUtil.IsCross)
                {
                    list = DicNpcMgr.GetAllForCross();
                }
                else
                {
                    list = DicNpcMgr.GetAll();
                }
                _dicNpc = new Dictionary<Guid, DicNpcEntity>();
                _transferManagerDic = new Dictionary<Guid, ManagerInput>(list.Count);
                _fightManagerinfoDic = new Dictionary<Guid, Match_FightManagerinfo>(list.Count);
                _dicBuffView = new Dictionary<Guid, DTOBuffMemberView>(list.Count);
                foreach (var entity in list)
                {
                    var buffView = NpcDataHelper.GetMemberView(entity);
                    _dicNpc.Add(entity.Idx, entity);
                    _dicBuffView.Add(entity.Idx, buffView);
                    _transferManagerDic.Add(entity.Idx, MatchTransferUtil.BuildTransferNpc(entity, buffView));
                    _fightManagerinfoDic.Add(entity.Idx, MatchDataHelper.GetFightinfo(entity, buffView, true));
                }

                _guidePlayers = new List<PlayerInput>();
                var guideConfig = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.TourGuidePlayers);
                var ssss = guideConfig.Split('|');
                foreach (var ss in ssss)
                {
                    var s = ss.Split(',');
                    _guidePlayers.Add(MatchTransferUtil.BuildPlayerInputForGuide(Convert.ToInt32(s[0]), Convert.ToInt32(s[1]), s[2]));
                }
                LogHelper.Insert("npc dic cache init end", LogType.Info);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }

        }
        #endregion

        #region Facade
        public static NpcdicCache Instance
        {
            get { return SingletonFactory<NpcdicCache>.SInstance; }
        }
        public DicNpcEntity GetNpc(Guid npcId)
        {
            DicNpcEntity obj;
            _dicNpc.TryGetValue(npcId, out obj);
            return obj;
        }

        /// <summary>
        /// 获取npc球队信息
        /// </summary>
        /// <param name="npcId"></param>
        /// <returns></returns>
        public DTOBuffMemberView GetBuffView(Guid npcId)
        {
            DTOBuffMemberView obj;
            _dicBuffView.TryGetValue(npcId, out obj);
            return obj;
        }
        /// <summary>
        /// 获取npc比赛数据
        /// </summary>
        /// <param name="npcId"></param>
        /// <returns></returns>
        public ManagerInput GetTransferData(Guid npcId)
        {
            if (_transferManagerDic.ContainsKey(npcId))
                return _transferManagerDic[npcId];
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取npc对阵信息
        /// </summary>
        /// <param name="npcId"></param>
        /// <returns></returns>
        public Match_FightManagerinfo GetFightManagerinfo(Guid npcId)
        {
            if (_fightManagerinfoDic.ContainsKey(npcId))
                return _fightManagerinfoDic[npcId];
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 判断是否是npc
        /// </summary>
        /// <param name="npcId"></param>
        /// <returns></returns>
        public bool IsNpc(Guid npcId)
        {
            return _fightManagerinfoDic.ContainsKey(npcId);
        }

        public List<PlayerInput> GetGuidePlayers()
        {
            return _guidePlayers;
        }
        #endregion
    }
}
