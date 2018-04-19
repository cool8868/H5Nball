using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class VipDicCache : BaseSingleton
    {
        #region encapsulation
        /// <summary>
        /// vipLevel>>EffectType>>EffectValue
        /// </summary>
        Dictionary<int, Dictionary<int, int>> _vipEffectDic;

        private List<int> _vipScoreList;

        public VipDicCache(int p)
            : base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            LogHelper.Insert("vip cache init start", LogType.Info);

            var list = ConfigViplevelMgr.GetAllForCache();
            _vipEffectDic = new Dictionary<int, Dictionary<int, int>>(11);
            _vipScoreList = new List<int>(11);
            for (int i = 0; i < 11; i++)
            {
                _vipEffectDic.Add(i, new Dictionary<int, int>());
            }
            foreach (var entity in list)
            {
                _vipEffectDic[0].Add(entity.EffectId, entity.Vip0);
                _vipEffectDic[1].Add(entity.EffectId, entity.Vip1);
                _vipEffectDic[2].Add(entity.EffectId, entity.Vip2);
                _vipEffectDic[3].Add(entity.EffectId, entity.Vip3);
                _vipEffectDic[4].Add(entity.EffectId, entity.Vip4);
                _vipEffectDic[5].Add(entity.EffectId, entity.Vip5);
                _vipEffectDic[6].Add(entity.EffectId, entity.Vip6);
                _vipEffectDic[7].Add(entity.EffectId, entity.Vip7);
                _vipEffectDic[8].Add(entity.EffectId, entity.Vip8);
                _vipEffectDic[9].Add(entity.EffectId, entity.Vip9);
                _vipEffectDic[10].Add(entity.EffectId, entity.Vip10);
            }
            int leveup = (int)EnumVipEffect.LevelupPoint;
            _vipScoreList.Add(0);
            _vipScoreList.Add(_vipEffectDic[0][leveup]);
            _vipScoreList.Add(_vipEffectDic[1][leveup]);
            _vipScoreList.Add(_vipEffectDic[2][leveup]);
            _vipScoreList.Add(_vipEffectDic[3][leveup]);
            _vipScoreList.Add(_vipEffectDic[4][leveup]);
            _vipScoreList.Add(_vipEffectDic[5][leveup]);
            _vipScoreList.Add(_vipEffectDic[6][leveup]);
            _vipScoreList.Add(_vipEffectDic[7][leveup]);
            _vipScoreList.Add(_vipEffectDic[8][leveup]);
            _vipScoreList.Add(_vipEffectDic[9][leveup]);
            LogHelper.Insert("vip cache init end", LogType.Info);
        }
        #endregion

        #region Facade

        public static VipDicCache Instance
        {
            get { return SingletonFactory<VipDicCache>.SInstance; }

        }

        public int GetEffectValue(int vipLevel, EnumVipEffect enumVipEffect)
        {
            return GetEffectValue(vipLevel, (int)enumVipEffect);
        }

        public int GetEffectValue(int vipLevel, int enumVipEffect)
        {
            if (_vipEffectDic.ContainsKey(vipLevel))
            {
                if (_vipEffectDic[vipLevel].ContainsKey(enumVipEffect))
                    return _vipEffectDic[vipLevel][enumVipEffect];
            }
            return 0;
        }

        public int GetVipLevel(decimal vipScore)
        {
            return _vipScoreList.FindLastIndex(d => d <= vipScore);
        }

        public int GetVipLevelupPoint(int vipLevel)
        {
            if (_vipEffectDic.ContainsKey(vipLevel))
            {
                return _vipEffectDic[vipLevel][(int)EnumVipEffect.LevelupPoint];
            }
            return 0;
        }
        #endregion
    }
}
