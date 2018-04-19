using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class PandoraCache: BaseSingleton
    {
        #region encapsulation

        /// <summary>
        /// quality->entity
        /// </summary>
        private Dictionary<int, ConfigEquipmentsynthesizeEntity> _configEquipmentSynthesisDic;

        /// <summary>
        /// itemCode->quality->formula
        /// </summary>
        private Dictionary<int, Dictionary<int, Dictionary<int, int>>> _suitdrawingFormulaDic;

        /// <summary>
        /// cardlevel->reiki
        /// </summary>
        private Dictionary<int, ConfigDecomposeEntity> _configDecomposeDic;
        /// <summary>
        /// cardlevel->entity
        /// </summary>
        private Dictionary<int, ConfigSynthesisEntity> _configSynthesisDic;

        /// <summary>
        /// cardlevel->entity
        /// </summary>
        private Dictionary<int, ConfigSynthesiscardEntity> _configSynthesisCardDic;
        /// <summary>
        /// key->entity
        /// </summary>
        private Dictionary<int, ConfigEquipmentupgradeEntity> _configEquipmentUpgradeDic;

        /// <summary>
        /// key->entity
        /// </summary>
        private Dictionary<int, ConfigStrengthEntity> _configStrengthDic;

        /// <summary>
        /// key entity
        /// </summary>
        private Dictionary<int, ConfigEquipmentprecisioncastingEntity> _configEquipmentPrecisionCastingDic;

        private Dictionary<int, int> _syntheticItemDic;
        private Dictionary<int, int> _equipmentSellCoinDic;

        private string _rateString1;
        private string _rateString2;

        public PandoraCache(int p)
            :base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            var list2 = ConfigDecomposeMgr.GetAll();
            _configDecomposeDic = list2.ToDictionary(d => d.CardLevel, d => d);

            var list4 = ConfigEquipmentsynthesizeMgr.GetAll();
            _configEquipmentSynthesisDic = list4.ToDictionary(d => d.Quality, d => d);

            var list = ConfigStrengthMgr.GetAll();
            _configStrengthDic = new Dictionary<int, ConfigStrengthEntity>(list.Count);
            foreach (var entity in list)
            {
                var key = BuildStrengthKey(entity.CardLevel, entity.Source, entity.Target);
                if (!_configStrengthDic.ContainsKey(key))
                    _configStrengthDic.Add(key, entity);
            }

            var list3 = ConfigSynthesisMgr.GetAll();
            _configSynthesisDic = list3.ToDictionary(d => d.CardLevel, d => d);

            var listSynthesisCard = ConfigSynthesiscardMgr.GetAll();
            _configSynthesisCardDic = listSynthesisCard.ToDictionary(d => d.CardLevel, d => d);

            var list6 = ConfigEquipmentupgradeMgr.GetAll();
            _configEquipmentUpgradeDic = new Dictionary<int, ConfigEquipmentupgradeEntity>(list6.Count);
            
            foreach (var entity in list6)
            {
                var key = BuildUpgradeKey(entity.EquipQuality, entity.SourceLevel);
                if (!_configEquipmentUpgradeDic.ContainsKey(key))
                {
                    _configEquipmentUpgradeDic.Add(key, entity);
                }      
                
            }

            var list12 = DicSyntheticitemMgr.GetAll();
            _syntheticItemDic = list12.ToDictionary(d => d.ItemCode, d => d.TarItemCode);

            var list8 = ConfigEquipmentprecisioncastingMgr.GetAll();
            _configEquipmentPrecisionCastingDic = new Dictionary<int, ConfigEquipmentprecisioncastingEntity>(list8.Count);

            foreach (var entity in list8)
            {
                var key = BuilePrecisionCastingKey(entity.EquipmentQuality, entity.PropertyQuality, entity.PropertyType);
                if (!_configEquipmentPrecisionCastingDic.ContainsKey(key))
                {
                    _configEquipmentPrecisionCastingDic.Add(key, entity);
                }
            }

            _rateString1 = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.QualityRateString1);
            _rateString2 = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.QualityRateString2);

            var equipmentSellCoin = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.EquipmentSellCoin);
            var eqs = equipmentSellCoin.Split('|');
            _equipmentSellCoinDic = new Dictionary<int, int>();
            foreach (var item in eqs)
            {
                var sq = item.Split(',');
                _equipmentSellCoinDic.Add(ConvertHelper.ConvertToInt(sq[0]), ConvertHelper.ConvertToInt(sq[1]));

            }
        }

        

        int BuildStrengthKey(int cardLevel, int source, int target)
        {
            return cardLevel * 10000 + source * 100 + target;
        }

        int BuildUpgradeKey(int quality, int source)
        {
            return quality*100 + source;
        }

        int BuilePrecisionCastingKey(int equipQuality, int propertyQuality, int propertyType)
        {
            return equipQuality*10000 + propertyQuality*1000 + propertyType;
        }
        #endregion

        #region Facade

        public static PandoraCache Instance
        {
            get { return SingletonFactory<PandoraCache>.SInstance; }
        }

        public bool CheckFormula(int suitItemCode,int quality, List<ItemInfoEntity> itemList)
        {
            if (_suitdrawingFormulaDic.ContainsKey(suitItemCode))
            {
                if (_suitdrawingFormulaDic[suitItemCode].ContainsKey(quality))
                {
                    var dic = _suitdrawingFormulaDic[suitItemCode][quality];
                    if (dic != null)
                    {
                        Dictionary<int, int> curDic = new Dictionary<int, int>(5);
                        bool check = true;
                        foreach (var entity in itemList)
                        {
                            if (!dic.ContainsKey(entity.ItemCode))
                            {
                                check= false;
                                break;
                            }
                            if (curDic.ContainsKey(entity.ItemCode))
                            {
                                check = false;
                                break;
                            }
                            curDic.Add(entity.ItemCode,1);
                        }
                        curDic.Clear();
                        curDic = null;
                        return check;
                    }
                }
            }
            return false;
        }

        public ConfigEquipmentsynthesizeEntity GetEquipmentSynthesisConfig(int quality,bool isProtect=false)
        {
            if (_configEquipmentSynthesisDic.ContainsKey(quality))
            {
                var config =_configEquipmentSynthesisDic[quality].Clone();
                if (isProtect)
                    config.Coin = 0;
                return config;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据放入的装备获取需要的消耗及成功率
        /// </summary>
        /// <param name="quailtyList"></param>
        /// <param name="maxQuality"></param>
        /// <param name="coin"></param>
        /// <param name="totalRate"></param>
        public void GetEquipmentSynthesisParam(List<int> qualityList, int maxQuality, out int coin, out int totalRate)
        {
            coin = 0;
            totalRate = 0;
            if(maxQuality>4)
                return;
            foreach (var quality in qualityList)
            {
                if (_configEquipmentSynthesisDic.ContainsKey(quality))
                {
                    coin += _configEquipmentSynthesisDic[quality].Coin;

                    switch (maxQuality)
                    {
                        case 1:
                            totalRate += _configEquipmentSynthesisDic[quality].RateQuality1;
                            break;
                        case 2:
                            totalRate += _configEquipmentSynthesisDic[quality].RateQuality2;
                            break;
                        case 3:
                            totalRate += _configEquipmentSynthesisDic[quality].RateQuality3;
                            break;
                        case 4:
                            totalRate += _configEquipmentSynthesisDic[quality].RateQuality4;
                            break;
                    }

                    
                }
            }
        }

        public ConfigEquipmentupgradeEntity GetEquipmentUpgradeEntity(int quality, int sourceLevel)
        {
            var key = BuildUpgradeKey(quality, sourceLevel);
            if (_configEquipmentUpgradeDic.ContainsKey(key))
                return _configEquipmentUpgradeDic[key];
            else
            {
                return null;
            }
        }

        public ConfigEquipmentupgradeEntity GetEquipmentUpgradeEntityByTarget(int quality, int targetLevel)
        {
            return GetEquipmentUpgradeEntity(quality, targetLevel - 1);
        }

        /// <summary>
        /// 获取分解配置
        /// </summary>
        /// <param name="cardLevel"></param>
        /// <returns></returns>
        public ConfigDecomposeEntity GetDecomposeConfig(int cardLevel)
        {
            if (_configDecomposeDic.ContainsKey(cardLevel))
                return _configDecomposeDic[cardLevel];
            return null;
        }

        /// <summary>
        /// 根据装备、属性品质、属性的类型 获得该属性的加成百分比
        /// </summary>
        /// <param name="equipQuality"></param>
        /// <param name="propertyQuality"></param>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public ConfigEquipmentprecisioncastingEntity GetEquipmentprecisioncastingEntity(int equipQuality,
            int propertyQuality, int propertyId)
        {
            var key = BuilePrecisionCastingKey(equipQuality, propertyQuality, propertyId);
            if (_configEquipmentPrecisionCastingDic.ContainsKey(key))
                return _configEquipmentPrecisionCastingDic[key];
            else
            {
                return null;
            }
        }

        public bool HasSyntheticItem(int itemCode)
        {
            return _syntheticItemDic.ContainsKey(itemCode);
        }

        public int GetSyntheticItem(int itemCode)
        {
            if (_syntheticItemDic.ContainsKey(itemCode))
                return _syntheticItemDic[itemCode];
            else
                return 0;
        }

        public ConfigSynthesisEntity GetSynthesisConfig(int cardLevel, bool isProtect = false)
        {
            if (_configSynthesisDic.ContainsKey(cardLevel))
            {
                var config = _configSynthesisDic[cardLevel].Clone();
                if (isProtect)
                    config.Coin = 0;
                return config;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据放入的球员卡获取需要的消耗
        /// </summary>
        /// <param name="playerList"></param>
        /// <param name="coin"></param>
        /// <param name="point"></param>
        public void GetSynthesisParam(List<DicPlayerEntity> playerList, out int coin, out int point)
        {
            coin = 0;
            point = 0;
            foreach (var playerEntity in playerList)
            {
                if (_configSynthesisCardDic.ContainsKey(playerEntity.CardLevel))
                {
                    coin += _configSynthesisCardDic[playerEntity.CardLevel].Coin;
                    point += _configSynthesisCardDic[playerEntity.CardLevel].ProtectPoint;
                }
            }
        }

        public ConfigSynthesiscardEntity GetSynthesisCardConfig(int cardLevel, bool isProtect = false)
        {
            if (_configSynthesisCardDic.ContainsKey(cardLevel))
            {
                var config = _configSynthesisCardDic[cardLevel].Clone();
                if (isProtect)
                    config.Coin = 0;
                return config;
            }
            else
            {
                return null;
            }
        }

        public ConfigStrengthEntity GetStrengthConfig(int cardLevel, int source, int target)
        {
            if (cardLevel == 8 || cardLevel == 9)
                cardLevel = 1;
            var key = BuildStrengthKey(cardLevel, source, target);
            if (_configStrengthDic.ContainsKey(key))
                return _configStrengthDic[key].Clone();
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取装备出售金币
        /// </summary>
        /// <param name="equipQuality"></param>
        /// <returns></returns>
        public int GetEquipmentSellCoin(int equipQuality)
        {
            if (_equipmentSellCoinDic.ContainsKey(equipQuality))
                return _equipmentSellCoinDic[equipQuality];
            return 0;
        }


        #endregion
    }
}
