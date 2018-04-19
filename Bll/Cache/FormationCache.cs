using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class FormationCache: BaseSingleton
    {
        #region encapsulation

        Dictionary<int, DicFormationEntity> _formationDic;

        Dictionary<int, Dictionary<int,DicFormationdetailEntity>> _formationDetailDic;

        private Dictionary<int, ConfigFormationlevelEntity> _formationLevelDic;
        private Dictionary<string, List<DicFormationpointEntity>> _formationPointDic;

        public FormationCache(int p)
            :base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            LogHelper.Insert("Formation cache init start", LogType.Info);
            var list = DicFormationMgr.GetAll();
            _formationPointDic = new Dictionary<string, List<DicFormationpointEntity>>();
            var list2 = DicFormationdetailMgr.GetAll();
            _formationDic = list.ToDictionary(d=>d.Idx,d=>d);
            _formationDetailDic = new Dictionary<int, Dictionary<int, DicFormationdetailEntity>>(list.Count);
            foreach (var entity in list2)
            {
                if(!_formationDetailDic.ContainsKey(entity.FormationId))
                    _formationDetailDic.Add(entity.FormationId, new Dictionary<int, DicFormationdetailEntity>());
                _formationDetailDic[entity.FormationId].Add(_formationDetailDic[entity.FormationId].Count,entity);
            }

            var list3 = ConfigFormationlevelMgr.GetAll();
            _formationLevelDic = list3.ToDictionary(d => d.Level, d => d);
            var list4 = DicFormationpointMgr.GetAll();
            foreach (var item in list4)
            {
                if (!_formationPointDic.ContainsKey(item.PlayerPoint))
                    _formationPointDic.Add(item.PlayerPoint, new List<DicFormationpointEntity>());
                _formationPointDic[item.PlayerPoint].Add(item);
            }
            LogHelper.Insert("Formation cache init end", LogType.Info);
        }
        #endregion

        #region Facade
        public static FormationCache Instance
        {
            get { return SingletonFactory<FormationCache>.SInstance; }
        }

        /// <summary>
        /// 获取阵型
        /// </summary>
        /// <param name="formationId"></param>
        /// <returns></returns>
        public DicFormationEntity GetFormation(int formationId)
        {
            if (_formationDic.ContainsKey(formationId))
                return _formationDic[formationId];
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取阵型详细坐标信息
        /// </summary>
        /// <param name="formationId"></param>
        /// <returns></returns>
        public Dictionary<int,DicFormationdetailEntity> GetFormationDetail(int formationId)
        {
            if (_formationDetailDic.ContainsKey(formationId))
                return _formationDetailDic[formationId];
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取阵型列表
        /// </summary>
        /// <returns></returns>
        public List<DicFormationEntity> GetFormationList()
        {
            var list =new List<DicFormationEntity>(_formationDic.Count);
            foreach (var entity in _formationDic.Values)
            {
                list.Add(entity.Clone());
            }
            return list;
        }

        /// <summary>
        /// 获取球员适应BUFF
        /// </summary>
        /// <param name="playerPoint"></param>
        /// <param name="ballParkPoint"></param>
        /// <returns></returns>
        public DicFormationpointEntity GetFormationPoint(string playerPoint, string ballParkPoint)
        {
            if (_formationPointDic.ContainsKey(playerPoint))
                return _formationPointDic[playerPoint].Find(r => r.BallParkPoint == ballParkPoint);
            return null;
        }

        /// <summary>
        /// 获取升级所需阅历
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetSophisticate(int level)
        {
            if (_formationLevelDic.ContainsKey(level))
                return _formationLevelDic[level].Sophisticate;
            return 0;
        }
        #endregion
    }
}
