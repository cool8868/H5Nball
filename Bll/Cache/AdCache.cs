using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Ad;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Ad;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;

namespace Games.NBall.Bll.Cache
{
    public class AdCache
    {
        /// <summary>
        /// 奖励字典
        /// </summary>
        public Dictionary<int, List<ConfigPenaltykickprizeEntity>> _prizeDic;

        /// <summary>
        /// 兑换列表
        /// </summary>
        public List<ConfigPenaltykickprizeEntity> _exChangeList;

        #region Singleton
        static readonly object s_lockObj = new object();
        static volatile AdCache s_instnce = null;
        public static AdCache Instance
        {
            get
            {
                if (null == s_instnce)
                {
                    lock (s_lockObj)
                    {
                        if (null == s_instnce)
                        {
                            s_instnce = new AdCache();
                        }
                    }
                }
                return s_instnce;
            }
        }
        #endregion

        #region .ctor
       
        public AdCache()
        {
            try
            {
                _prizeDic = new Dictionary<int, List<ConfigPenaltykickprizeEntity>>();
                _exChangeList = new List<ConfigPenaltykickprizeEntity>();
                var allPrize = ConfigPenaltykickprizeMgr.GetAll();
                foreach (var item in allPrize)
                {
                    if (item.PrizeType == 4)
                    {
                        _exChangeList.Add(item);
                    }
                    else
                    {
                       var key = GetKey(item.PrizeType, item.PrizeSub);
                        if (!_prizeDic.ContainsKey(key))
                            _prizeDic.Add(key, new List<ConfigPenaltykickprizeEntity>());
                        _prizeDic[key].Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("AdCache:Init", ex);
            }
        }

        #endregion

        /// <summary>
        /// 获取奖励
        /// </summary>
        /// <param name="prizeType"></param>
        /// <param name="prizeSub"></param>
        /// <returns></returns>
        public List<ConfigPenaltykickprizeEntity> GetPrize(int prizeType, int prizeSub)
        {
            var key = GetKey(prizeType, prizeSub);
            if (_prizeDic.ContainsKey(key))
                return _prizeDic[key];
            return new List<ConfigPenaltykickprizeEntity>();
        }

        /// <summary>
        /// 获取兑换的物品
        /// </summary>
        /// <returns></returns>
        public string GetExChangeString()
        {
            string resultString = "";
            int index = 0;
            foreach (var item in _exChangeList)
            {
                var itemcode = CacheFactory.LotteryCache.LotteryByLib(item.ItemCode);
                int i = 100;
                do
                {
                    if (itemcode == 390001 || itemcode == 390002 || itemcode == 0)
                    {
                        itemcode = CacheFactory.LotteryCache.LotteryByLib(item.ItemCode);
                        i--;
                    }
                    else
                        break;
                } while (i > 0);
                index ++;
                if (index == 3) //第三挡有梅西和c罗碎片 概率1% 特殊处理
                {
                    if (RandomHelper.GetInt32(1, 100) == 1)
                    {
                        if (RandomHelper.GetInt32(1, 2) == 1)
                            itemcode = 390001;
                        else
                            itemcode = 390002;
                    }
                }
                //物品code，价格和兑换状态
                resultString += itemcode + "," + item.PrizeSub+","+0+"|";
            }
            if (resultString.Length > 0)
                resultString = resultString.Substring(0, resultString.Length - 1);
            return resultString;
        }

        public int GetKey(int number1, int number2)
        {
            return number2*10000 + number1;
        }
    }
}
