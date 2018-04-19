using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Player;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class TemplateCache : BaseSingleton
    {
        #region encapsulation
        List<TemplateRegisterEntity> _registerList;
        private Dictionary<int, TemplateRegisterEntity> _registerDic;
        public TemplateCache(int p)
            : base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            _registerList = TemplateRegisterMgr.GetAll();
            foreach (var entity in _registerList)
            {
                var ss = entity.SolutionString.Split(',');
                foreach (var s in ss)
                {
                    var pid = ConvertHelper.ConvertToInt(s);
                    var pcache = CacheFactory.PlayersdicCache.GetPlayer(pid);
                    if (pcache != null && pcache.CardLevel == (int)EnumPlayerCardLevel.Orange)
                        entity.OrangeCount++;
                }

            }
            _registerDic = _registerList.ToDictionary(d => d.Idx, d => d);
        }
        #endregion

        #region Facade
        public static TemplateCache Instance
        {
            get { return SingletonFactory<TemplateCache>.SInstance; }
        }

        public TemplateRegisterEntity GetRandom()
        {
            return _registerList[RandomHelper.GetInt32WithoutMax(0, _registerList.Count)];
        }

        public TemplateRegisterEntity GetEntity(int templateId)
        {
            if (_registerDic.ContainsKey(templateId))
                return _registerDic[templateId];
            else
            {
                return null;
            }
        }
        #endregion
    }
}
