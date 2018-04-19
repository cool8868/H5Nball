using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.WpfEmulator.Entity;

namespace Games.NBall.WpfEmulator.Core
{
    /// <summary>
    /// 负责从数据库获取数据
    /// </summary>
    public class CacheDataHelper
    {
        #region .ctor
        private static CacheDataHelper _instance = null;
        private static object _lockObj = new object();

        private CacheDataHelper()
        {
            InitCache();
        }
        #endregion

        #region Facade
        public static CacheDataHelper Instance{
            get
        {
            if (_instance == null)
            {
                lock (_lockObj)
                {
                    if(_instance==null)
                        _instance = new CacheDataHelper();
                }
            }
            return _instance;
        }}

        /// <summary>
        /// itemcode->entity
        /// </summary>
        public Dictionary<int, PlayerCardDescriptionEntity> ItemTipPlayerDic { get; private set; }
        /// <summary>
        /// itemcode->entity
        /// </summary>
        public Dictionary<int, EquipmentDescriptionEntity> ItemTipEquipmentDic { get; private set; }
        /// <summary>
        /// equipment id->entity
        /// </summary>
        public Dictionary<int, EquipmentDescriptionEntity> ItemTipEquipmentLinkDic { get; private set; }
        /// <summary>
        /// itemcode->entity
        /// </summary>
        public Dictionary<int, MallItemDescriptionEntity> ItemTipMallitemDic { get; private set; }
        /// <summary>
        /// player id->entity
        /// </summary>
        public Dictionary<int, PlayerCardDescriptionEntity> ItemTipPlayerIdDic { get; private set; }
        /// <summary>
        /// itemcode->entity
        /// </summary>
        public Dictionary<int, DescriptionEntity> AllItemDic { get; private set; }
        /// <summary>
        /// itemType->linkid->entity
        /// </summary>
        public Dictionary<int, Dictionary<int, DescriptionEntity>> AllLinkDic { get; private set; } 

        public string GetItemTypeView(int itemType)
        {
            if (CacheHelper.Instance.MessageItemTypeDic.ContainsKey(itemType))
                return CacheHelper.Instance.MessageItemTypeDic[itemType];
            else
            {
                return "无";
            }
        }

        public string GetItemStatusView(int itemStatus)
        {
            if (_itemStatusDic.ContainsKey(itemStatus))
                return _itemStatusDic[itemStatus];
            else
            {
                return "无";
            }
        }

        public void RefreshItemtips(ItemTipsEntity itemtips)
        {
            ItemTipPlayerDic = itemtips.PlayerCard.ToDictionary(d => d.ItemCode, d => d);
            ItemTipPlayerIdDic = itemtips.PlayerCard.ToDictionary(d => d.PlayerId, d => d);

            ItemTipEquipmentDic = itemtips.Equipment.ToDictionary(d => d.ItemCode, d => d);
            ItemTipEquipmentLinkDic = itemtips.Equipment.ToDictionary(d => d.Idx, d => d);
            ItemTipMallitemDic = itemtips.MallItem.ToDictionary(d => d.ItemCode, d => d);
            AllItemDic = new Dictionary<int, DescriptionEntity>();
            AllLinkDic = new Dictionary<int, Dictionary<int, DescriptionEntity>>();
            AllLinkDic.Add((int) EnumItemType.PlayerCard, new Dictionary<int, DescriptionEntity>());
            AllLinkDic.Add((int) EnumItemType.Equipment, new Dictionary<int, DescriptionEntity>());
            AllLinkDic.Add((int) EnumItemType.MallItem, new Dictionary<int, DescriptionEntity>());
            foreach (var entity in itemtips.PlayerCard)
            {
                AllItemDic.Add(entity.ItemCode, entity);
                AllLinkDic[(int) EnumItemType.PlayerCard].Add(entity.PlayerId, entity);
            }
            foreach (var entity in itemtips.Equipment)
            {
                AllItemDic.Add(entity.ItemCode, entity);
                AllLinkDic[(int) EnumItemType.Equipment].Add(entity.Idx, entity);
            }
            foreach (var entity in itemtips.MallItem)
            {
                AllItemDic.Add(entity.ItemCode, entity);
                AllLinkDic[(int) EnumItemType.MallItem].Add(entity.Idx, entity);


            }
        }

        #endregion

        #region encapsulation

        private Dictionary<int, string> _itemStatusDic;

        private void InitCache()
        {
            //#region init itemdescription
            //var itemtips = EmulatorHelper.LoadConfig<ItemTipsEntity>(EmulatorHelper.ItemtipFileName);
            //if (itemtips == null)
            //{
            //    itemtips = DataExport.ExportItemTips();
            //    EmulatorHelper.SaveConfig<ItemTipsEntity>(itemtips, EmulatorHelper.ItemtipFileName);
            //}
            //RefreshItemtips(itemtips);
            //#endregion

            //_itemStatusDic = new Dictionary<int, string>(2);
            //_itemStatusDic.Add(0, "");
            //_itemStatusDic.Add(1, "锁定");

        }


        #endregion
    }
}
