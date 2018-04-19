using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Exceptions;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.Share;

namespace Games.NBall.Bll.Cache
{
    public class EquipmentCache : BaseSingleton
    {
        #region encapsulation

        /// <summary>
        /// Idx->entity
        /// </summary>
        private Dictionary<int, DicEquipmentEntity> _equipmentDic;// = new Dictionary<int, DicEquipmentEntity>();

        /// <summary>
        /// Quarity->list
        /// </summary>
        Dictionary<int, List<DicEquipmentEntity>> _equipmentQuarityDic;//=new Dictionary<int, List<DicEquipmentEntity>>();
        
        /// <summary>
        /// Quarity->entity
        /// </summary>
        Dictionary<int, ConfigEquipmentplusEntity> _equipmentPlusDic;// = new Dictionary<int, ConfigEquipmentplusEntity>(); 

        private int _propertyTypeMin;
        private int _propertyTypeMax; 

        public EquipmentCache(int p)
            :base(p)
        {
            InitCache();
        }

        void InitCache()
        {
            LogHelper.Insert("Equipment cache init start", LogType.Info);

            var list = DicEquipmentMgr.GetAllForCache();
            var list2 = ConfigEquipmentplusMgr.GetAll();
            //var list3 = ConfigEquipmentsynthesizeMgr.GetAll();
            //var list4 = DicEquipmentsuitMgr.GetAllForCache();
            _equipmentDic = list.ToDictionary(d => d.Idx, d => d);
            _equipmentQuarityDic = new Dictionary<int, List<DicEquipmentEntity>>(list2.Count);
            foreach (var entity in list)
            {
                if(!_equipmentQuarityDic.ContainsKey(entity.Quality))
                    _equipmentQuarityDic.Add(entity.Quality,new List<DicEquipmentEntity>());
                _equipmentQuarityDic[entity.Quality].Add(entity);
            }

            _equipmentPlusDic = list2.ToDictionary(d => d.Quality, d => d);
            //_equipmentSynthesizeDic = list3.ToDictionary(d => d.Quarity, d => d);
            //_equipmentsuitDic = list4.ToDictionary(d => d.Idx, d => d);

            var propertyType = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.EquipmentPropertyTypeRange);
            var ss = propertyType.Split(',');
            _propertyTypeMin=ConvertHelper.ConvertToInt(ss[0]);
            _propertyTypeMax = ConvertHelper.ConvertToInt(ss[1]);

            LogHelper.Insert("Equipment cache init end", LogType.Info);
        }

        int RandomSlotColor()
        {
            return RandomHelper.GetInt32(1, 4);
        }
        #endregion

        #region Facade

        public static EquipmentCache Instance
        {
            get { return SingletonFactory<EquipmentCache>.SInstance; }
        }

        public DicEquipmentEntity GetEquipment(int equipmentId)
        {
            if (_equipmentDic.ContainsKey(equipmentId))
            {
                return _equipmentDic[equipmentId];
            }
            return null;
        }

        public DicEquipmentEntity RandomEquipment(int quality)
        {
            if (_equipmentQuarityDic.ContainsKey(quality))
            {
                var list = _equipmentQuarityDic[quality];
                int index = RandomHelper.GetInt32WithoutMax(0,list.Count);
                return list[index];
            }
            else
            {
                return null;
            }
        }

        public ConfigEquipmentplusEntity GetEquipmentPlus(int quality)
        {
            if (_equipmentPlusDic.ContainsKey(quality))
                return _equipmentPlusDic[quality];
            else
            {
                return null;
            }
        }

        public int RandomPlus1(ConfigEquipmentplusEntity plusEntity)
        {
            return RandomHelper.GetInt32(plusEntity.PlusValueMin, plusEntity.PlusValueMax);
        }

        public int RandomPlus2(ConfigEquipmentplusEntity plusEntity)
        {
            return RandomHelper.GetInt32(plusEntity.PlusRateMin, plusEntity.PlusRateMax);
        }

        public int RandomPropertyType(int propertyType)
        {
            int p = RandomHelper.GetInt32(_propertyTypeMin, _propertyTypeMax);
            int loopCount = 1;
            while (p==propertyType && loopCount<5)
            {
                p = RandomHelper.GetInt32(_propertyTypeMin, _propertyTypeMax);
                loopCount++;
            }
            return p;
        }

        public EquipmentProperty RandomEquipmentProperty(int equipmentId,int level=0,int slotColorCount=0)
        {
            ConfigEquipmentplusEntity plusEntity = null;
            var itemProperty = RandomProperty(equipmentId,out plusEntity,level);
            if (plusEntity.SlotMax > 0)
            {
                if (slotColorCount > plusEntity.SlotMax)
                    slotColorCount = plusEntity.SlotMax;

                int slotCount = RandomHelper.GetInt32(plusEntity.SlotMin, plusEntity.SlotMax);
                if (slotCount < slotColorCount)
                    slotCount = slotColorCount;
                if (slotCount > 0)
                {
                    itemProperty.EquipmentSlots = new List<EquipmentSlotEntity>(slotCount);
                    for (int i = 0; i < slotCount; i++)
                    {
                        if (slotColorCount > 0)
                        {
                            itemProperty.EquipmentSlots.Add(new EquipmentSlotEntity() { SlotId = i + 1, Color = 1 });
                            slotColorCount = slotColorCount - 1;
                        }
                        else
                        {
                            itemProperty.EquipmentSlots.Add(new EquipmentSlotEntity() { SlotId = i + 1, Color = RandomSlotColor() });
                        }
                    }
                }
            }
            
            return itemProperty;
        }

        public EquipmentProperty RandomEquipmentPropertyForNpc(int equipmentId)
        {
            ConfigEquipmentplusEntity plusEntity = null;
            return RandomProperty(equipmentId,out plusEntity);
        }

        EquipmentProperty RandomProperty(int equipmentId,out ConfigEquipmentplusEntity plusEntity,int level=0)
        {
            var equipEntity = GetEquipment(equipmentId);
            if(equipEntity==null)
                throw new CacheException("can't find equip,id:"+equipmentId);
            plusEntity = GetEquipmentPlus(equipEntity.Quality);
            if(plusEntity==null)
                throw new CacheException("can't find equip plus,quality:" + equipEntity.Quality);
            var itemProperty = new EquipmentProperty();
            itemProperty.PropertyPluses = new List<PropertyPlusEntity>(2);
            var plus1 = new PropertyPlusEntity(EnumPlusType.Abs, equipEntity.PropertyType1,RandomPlus1(plusEntity));
            itemProperty.PropertyPluses.Add(plus1);
            var plus2 = new PropertyPlusEntity(EnumPlusType.Percent, equipEntity.PropertyType2, RandomPlus2(plusEntity));
            itemProperty.PropertyPluses.Add(plus2);

            if (level > 0)
            {
                var upgradeEntity = PandoraCache.Instance.GetEquipmentUpgradeEntityByTarget(equipEntity.Quality, level);
                if (upgradeEntity != null)
                {
                    itemProperty.PrecisionCastingPropertis =
                        new List<PrecisionCastingPropertyEntity>(upgradeEntity.PropertyNum);

                    for (int i = 0; i < upgradeEntity.PropertyNum; i++)
                    {
                        var plus = AddPrecisionCastingProperty(itemProperty, equipEntity.Quality);
                        if (plus != null)
                        {
                            itemProperty.PrecisionCastingPropertis.Add(plus);
                        }
                    }
                }
            }
            return itemProperty;
        }

        /// <summary>
        /// 随机生成一个附加属性
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        public PrecisionCastingPropertyEntity RandomPrecisionCastingProperty(int quality, bool isActivity = false)
        {
            //var propertyQuality = PandoraCache.Instance.RandomPropertyQuality(quality, isActivity);
            //int propertyId = PandoraCache.Instance.RandomPropertyId(quality);
            //var plusPropertyEntity = PandoraCache.Instance.GetEquipmentprecisioncastingEntity(quality, propertyQuality, propertyId);
            //if (plusPropertyEntity != null)
            //{
            //    var plus = new PrecisionCastingPropertyEntity(propertyId,
            //        RandomPrecisionCastingPropertyPlus(plusPropertyEntity),
            //        plusPropertyEntity.PropertyQuality);
            //    return plus;
            //}
            //else
            //{
            //    return null;
            //}
            return null;
        }


        /// <summary>
        /// 加精铸属性
        /// </summary>
        /// <param name="equipmentProperty"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public PrecisionCastingPropertyEntity AddPrecisionCastingProperty(EquipmentProperty equipmentProperty, int quality,bool isActivity = false)
        {
            PrecisionCastingPropertyEntity plus = null;
            //限制
            for (int i = 0; i < 10; i++)
            {
                plus = RandomPrecisionCastingProperty(quality, isActivity);
                if (plus.PropertyQuality == 4)
                {
                    if (!CheckProperty(equipmentProperty, plus))
                    {
                        return plus;
                    }
                }
                else
                {
                    if (!HaveSameProperty(equipmentProperty, plus))
                    {
                        return plus;
                    }
                }
                
            }
            return plus;
        }

        bool CheckProperty(EquipmentProperty equipmentProperty, PrecisionCastingPropertyEntity plus)
        {
            if (IsHadOrangeProperty(equipmentProperty))
            {
                return true;
            }

            if (HaveSameProperty(equipmentProperty, plus))
            {
                return true;
            }
            return false;
        }

        //检查是否已经有橙色的属性存在
        bool IsHadOrangeProperty(EquipmentProperty equipmentProperty)
        {
            return equipmentProperty.PrecisionCastingPropertis.Exists(d => d.PropertyQuality == 4);
        }

        //检查是否有相同的属性存在
        bool HaveSameProperty(EquipmentProperty equipmentProperty, PrecisionCastingPropertyEntity entity)
        {
            return equipmentProperty.PrecisionCastingPropertis.Exists(d => d.PropertyId == entity.PropertyId);
        }

        double RandomPrecisionCastingPropertyPlus(ConfigEquipmentprecisioncastingEntity entity)
        {
            return RandomHelper.GetInt32(entity.RateMin, entity.RateMax) / 100.00;
        }
        #endregion

    }
}
