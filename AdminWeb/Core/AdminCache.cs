using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Games.NBall.Bll;
using Games.NBall.Bll.Cache;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.AdminWeb
{
    public class AdminCache
    {
        #region encapsulation
        /// <summary>
        /// itemcode>>entity
        /// </summary>
        Dictionary<int, DicItemEntity> _itemsDic;
        /// <summary>
        /// itemtype>>linkid>>entity
        /// </summary>
        private Dictionary<int, Dictionary<int, DicItemEntity>> _itemTypeLinkDic;

        public AdminCache(int p)
        {
            InitCache();
        }

        void InitCache()
        {
            var itemList = DicItemMgr.GetAll();
            _itemTypeLinkDic = new Dictionary<int, Dictionary<int, DicItemEntity>>();
            _itemsDic = itemList.ToDictionary(d => d.ItemCode, d => d);
            foreach (var dicItem in itemList)
            {
                if (!_itemTypeLinkDic.ContainsKey(dicItem.ItemType))
                    _itemTypeLinkDic.Add(dicItem.ItemType, new Dictionary<int, DicItemEntity>());
                _itemTypeLinkDic[dicItem.ItemType].Add(dicItem.LinkId, dicItem);
            }
        }
        #endregion

        #region Facade
        public static AdminCache Instance
        {
            get { return SingletonFactory<AdminCache>.SInstance; }
        }

        public string GetPlayerName(int pid)
        {
            var itemDic = GetItemByType(pid, EnumItemType.PlayerCard);
            if (itemDic != null)
                return itemDic.ItemName;
            else
            {
                return pid.ToString();
            }
        }

        public DicItemEntity GetItemByType(int linkId, EnumItemType itemType)
        {
            return GetItemByType(linkId, (int)itemType);
        }

        public DicItemEntity GetItemByType(int linkId, int itemType)
        {
            if (_itemTypeLinkDic.ContainsKey(itemType))
            {
                if (_itemTypeLinkDic[itemType].ContainsKey(linkId))
                    return _itemTypeLinkDic[itemType][linkId];
            }
            return null;
        }

        public string GetItemName(int itemCode)
        {
            var item = GetItem(itemCode);
            if (item != null)
                return item.ItemName;
            else
            {
                return "";
            }
        }

        public DicItemEntity GetItem(int itemCode)
        {
            if (_itemsDic.ContainsKey(itemCode))
            {
                return _itemsDic[itemCode];
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}