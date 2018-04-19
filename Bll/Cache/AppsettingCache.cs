using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class AppsettingCache : BaseSingleton
    {
        #region encapsulation
        /// <summary>
        /// AppSetting
        /// </summary>
        private Dictionary<string, string> _appSettingDic = new Dictionary<string, string>();
        Dictionary<int, int> _legendcardPlusBuffDic = new Dictionary<int, int>();
        Dictionary<int, int> _vipPlusBuffDic = new Dictionary<int, int>();
        Dictionary<int,int> _txAndItemIdDic=new Dictionary<int, int>();
        Dictionary<int, int> _txIosItemIdDic = new Dictionary<int, int>();
        Dictionary<int,int>_txItemKeyDic=new Dictionary<int, int>(); 

        public AppsettingCache(int p)
            : base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            //LogHelper.Insert("appsetting cache init start", LogType.Info);
            List<ConfigAppsettingEntity> listAppSetting = ConfigAppsettingMgr.GetAllForCache();

            _appSettingDic = listAppSetting.ToDictionary(d => d.Key, d => d.Value);
            var shadowitem = ConfigurationManager.AppSettings["NotShadowItem"];
            var shadowcoin = ConfigurationManager.AppSettings["ShadowCoin"];
            if (!string.IsNullOrEmpty(shadowitem) && shadowitem == "1")
                NotShadowItem = true;
            if (!string.IsNullOrEmpty(shadowcoin) && shadowcoin == "1")
                ShadowCoin = true;
            _legendcardPlusBuffDic = new Dictionary<int, int>();
            if (_appSettingDic.ContainsKey("SolutionLegendPlusConfig"))
            {
                var config = _appSettingDic["SolutionLegendPlusConfig"];
                var ss = config.Split('|');
                foreach (var s in ss)
                {
                    var sss = s.Split(',');
                    _legendcardPlusBuffDic.Add(Convert.ToInt32(sss[0]), Convert.ToInt32(sss[1]));
                }
            }
            _vipPlusBuffDic = new Dictionary<int, int>();
            if (_appSettingDic.ContainsKey("VipPlusConfig"))
            {
                var config = _appSettingDic["VipPlusConfig"];
                var ss = config.Split('|');
                foreach (var s in ss)
                {
                    var sss = s.Split(',');
                    _vipPlusBuffDic.Add(Convert.ToInt32(sss[0]), Convert.ToInt32(sss[1]));
                }
            }
            try
            {
                if(_appSettingDic.ContainsKey("TxWb_ItemId"))
                    SetTxItemId(_appSettingDic["TxWb_ItemId"]);

            }
            catch (Exception)
            {
                
                
            }
            //MaxItemStrength = Convert.ToInt32(_appSettingDic["MaxItemStrength"]);
             LogHelper.Insert("appsetting cache init end", LogType.Info);
        }
        #endregion

        #region Facade
        public static AppsettingCache Instance
        {
            get { return SingletonFactory<AppsettingCache>.SInstance; }
        }

        public bool NotShadowItem { get; set; }

        public bool ShadowCoin { get; set; }

        /// <summary>
        /// 获取应用配置
        /// </summary>
        /// <param name="objName">Name of the obj.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [luohaitao]     2009-11-30 10:58     Created
        /// </history>
        public string GetAppSetting(EnumAppsetting objName)
        {
            string name = objName.ToString();
            if (_appSettingDic.ContainsKey(name))
                return _appSettingDic[name];
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取应用配置,并转换为int，转换失败返回0.
        /// </summary>
        /// <param name="objName">Name of the obj.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [fuxiaogang]     2009-12-29 14:38     Created
        /// </history>
        public int GetAppSettingToInt(EnumAppsetting objName)
        {
            return GetAppSettingToInt(objName, 0);
        }

        /// <summary>
        /// 获取应用配置,并转换为int，转换失败返回默认值.
        /// </summary>
        /// <param name="objName">Name of the obj.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [fuxiaogang]     2009-12-29 14:38     Created
        /// </history>
        public int GetAppSettingToInt(EnumAppsetting objName, int defValue)
        {
            int i = 0;
            if (int.TryParse(GetAppSetting(objName), out i))
                return i;
            else
            {
                return defValue;
            }
        }

        /// <summary>
        /// 获取应用配置,并转换为double，转换失败返回0.
        /// </summary>
        /// <param name="objName">Name of the obj.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [fuxiaogang]     2009-12-29 14:38     Created
        /// </history>
        public double GetAppSettingToDouble(EnumAppsetting objName)
        {
            return GetAppSettingToDouble(objName, 0);
        }

        /// <summary>
        /// 获取应用配置,并转换为double，转换失败返回默认值.
        /// </summary>
        /// <param name="objName">Name of the obj.</param>
        /// <param name="defValue">The def value.</param>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// [fuxiaogang]     2009-12-29 14:38     Created
        /// </history>
        public double GetAppSettingToDouble(EnumAppsetting objName, int defValue)
        {
            double i = 0;
            if (double.TryParse(GetAppSetting(objName), out i))
                return i;
            else
            {
                return defValue;
            }
        }
        /// <summary>
        /// 腾讯玩吧itemId字典缓存
        /// </summary>
        /// <param name="items"></param>
        private void SetTxItemId(string items)
        {
            var itemList = items.Split(',');
            foreach (var entity in itemList)
            {
                var e = entity.Split('|');
                int itemCode = ConvertHelper.ConvertToInt(e[0]);
                int itemIdAnd = ConvertHelper.ConvertToInt(e[1]);
                int itemIdIos = ConvertHelper.ConvertToInt(e[2]);
                if (!_txAndItemIdDic.ContainsKey(itemCode))
                    _txAndItemIdDic.Add(itemCode,itemIdAnd);
                else
                {
                    _txAndItemIdDic[itemCode] = itemIdAnd;
                }

                if (!_txIosItemIdDic.ContainsKey(itemCode))
                    _txIosItemIdDic.Add(itemCode, itemIdIos);
                else
                {
                    _txIosItemIdDic[itemCode] = itemIdIos;
                }
                if (_txItemKeyDic.ContainsKey(itemIdAnd))
                {
                    _txItemKeyDic.Add(itemIdAnd,itemCode);
                }
                else
                {
                    _txItemKeyDic[itemIdAnd] = itemCode;
                }
                if (_txItemKeyDic.ContainsKey(itemIdIos))
                {
                    _txItemKeyDic.Add(itemIdIos, itemCode);
                }
                else
                {
                    _txItemKeyDic[itemIdIos] = itemCode;
                }
            }
        }
        /// <summary>
        /// 获取腾讯玩吧安卓itemId
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int GetAndItem(int itemCode)
        {
            if (_txAndItemIdDic.ContainsKey(itemCode))
                return _txAndItemIdDic[itemCode];
            return 0;
        }
        /// <summary>
        /// 获取腾讯玩吧IOSitemId
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int GetIOSItem(int itemCode)
        {
            if (_txIosItemIdDic.ContainsKey(itemCode))
                return _txIosItemIdDic[itemCode];
            return 0;
        }
        /// <summary>
        /// 腾讯玩吧itemId转itemCode
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int FindValueByTX(int value)
        {
            if (_txItemKeyDic.ContainsKey(value))
            {
                return _txItemKeyDic[value];

            }
            return 0;
        }

        public int MaxItemStrength { get; set; }

        public int GetSolutionLegendAndVipPlus(int legendCount, int vipLevel)
        {
            int plus = 0;
            if (_legendcardPlusBuffDic.ContainsKey(legendCount))
            {
                plus = _legendcardPlusBuffDic[legendCount];
            }
            if (_vipPlusBuffDic.ContainsKey(vipLevel))
            {
                plus += _vipPlusBuffDic[vipLevel];
            }
            return plus;
        }

        public int GetVipPlus(int vipLevel)
        {
            if (_vipPlusBuffDic.ContainsKey(vipLevel))
                return _vipPlusBuffDic[vipLevel];
            else
            {
                return 0;
            }
        }
        #endregion

       


    }
}
