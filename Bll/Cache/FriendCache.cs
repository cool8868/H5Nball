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
    /// <summary>
    /// 好友系统缓存类
    /// </summary>
    public class FriendCache
    {
        public FriendCache(int p)
        {
            InitCache();
        }


        #region encapsulation

        /// <summary>
        /// 邀请好友奖励配置
        /// </summary>
        private List<ConfigFriendinviteprizeEntity> _prize;

        void InitCache()
        {
            _prize = ConfigFriendinviteprizeMgr.GetAll();
        }

        #endregion

        #region Facade

        public static FriendCache Instance
        {
            get { return SingletonFactory<FriendCache>.SInstance; }
        }

        /// <summary>
        /// 根据邀请人数获取可以领取的配置
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<ConfigFriendinviteprizeEntity> GetPrize(int count)
        {
            if (_prize.Exists(r => r.SucceedCount <= count))
                return _prize.FindAll(r => r.SucceedCount <= count);
            return null;
        }

        public List<ConfigFriendinviteprizeEntity> GetAllPrize()
        {
            return _prize;
        }

        /// <summary>
        /// 根据邀请人数获取下一挡可领取的配置
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<ConfigFriendinviteprizeEntity> GetPrize1(int count)
        {
            if (_prize.Count(r => r.SucceedCount > count) > 0)
            {
                var list = _prize.FindAll(r => r.SucceedCount > count);

                return _prize.FindAll(r => r.SucceedCount == list.Min(h => h.SucceedCount));
            }
            return null;
        }

        #endregion
    }
}
