using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;
using log4net.Appender;

namespace Games.NBall.Core.Item
{
    /// <summary>
    /// 背包逻辑封装
    /// </summary>
    public class ItemPackageFrame
    {
        #region .ctor

        private ItemPackageEntity _packageEntity;

        public List<PlayerAddEntity> _addPlayer;
        public List<PlayerAddEntity> _deletePlayer;
        private string _zoneId = "";

        public ItemPackageFrame(ItemPackageEntity packageEntity)
        {
            _packageEntity = packageEntity;
            _addPlayer = new List<PlayerAddEntity>();
            _deletePlayer = new List<PlayerAddEntity>();
            AnalysePackage();
        }

        public ItemPackageFrame(ItemPackageEntity packageEntity, string zoneId)
        {
            _packageEntity = packageEntity;
            AnalysePackage();
            _zoneId = zoneId;
            _addPlayer = new List<PlayerAddEntity>();
            _deletePlayer = new List<PlayerAddEntity>();
        }

        public ItemPackageFrame(ItemPackageEntity packageEntity, EnumTransactionType transactionType, string zoneId)
            : this(packageEntity)
        {
            Shadow = new TransactionShadow(packageEntity.ManagerId, transactionType, zoneId);
            _zoneId = zoneId;
            _addPlayer = new List<PlayerAddEntity>();
            _deletePlayer = new List<PlayerAddEntity>();
        }

        public ItemPackageFrame(ItemPackageEntity packageEntity, TransactionShadow shadow)
            : this(packageEntity)
        {
            Shadow = shadow;
            _addPlayer = new List<PlayerAddEntity>();
            _deletePlayer = new List<PlayerAddEntity>();
        }

        #endregion

        #region Fields

        public TransactionShadow Shadow { get; private set; }

        /// <summary>
        /// 所属经理id
        /// </summary>
        public Guid ManagerId
        {
            get { return _packageEntity.ManagerId; }
        }

        public int _mod = -1;

        public int Mod
        {
            get
            {
                if (_mod == -1)
                {
                    var manager = ManagerCore.Instance.GetManager(ManagerId);
                    if (manager != null)
                        _mod = manager.Mod;
                }
                return _mod;
            }
        }

        /// <summary>
        /// 最近添加的物品id
        /// </summary>
        public Guid LastAddItemId { get; set; }

        public ItemInfoEntity LastAddItem { get; set; }

        /// <summary>
        /// 背包是否满了
        /// </summary>
        public bool IsFull
        {
            get { return BlankCount < 1; } 
        }

        /// <summary>
        /// 背包容量
        /// </summary>
        public int PackageSize
        {
            get { return _packageEntity.PackageSize; }
        }

        /// <summary>
        /// 包裹内的物品数量
        /// </summary>
        public int ItemCount
        {
            get { return _itemCount; }
            }

        /// <summary>
        /// 包裹内空格子数.
        /// </summary>
        /// <value>The blank count.</value>
        public int BlankCount
        {
            get { return _blanks.Count; }
            }

        /// <summary>
        /// 获取物品字符串
        /// </summary>
        public byte[] NewItemString
        {
            get
            {
                if (_synchronized)
                {
                    return _packageEntity.ItemString;
                }
                else
                {
                    return GenerateItemString();
                }
            }
        }

        public byte[] RowVersion
        {
            get { return _packageEntity.RowVersion; }
        }

        #endregion

        #region AddUsedItem

        /// <summary>
        /// 使用过的装备返回背包
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MessageCode AddUsedItem(EquipmentUsedEntity entity)
        {
            if (entity == null)
                return MessageCode.NbParameterError;
            if (!CheckUsedItem(entity.ItemCode, EnumItemType.Equipment, entity.ItemId))
                return MessageCode.NbParameterError;
            try
            {
                if (entity.Property == null)
                    return MessageCode.ItemPropertyIsNull;
                if (Items.Exists(d => d.ItemId == entity.ItemId))
                    return MessageCode.ItemIdRepeat;
                if (1 > BlankCount)
                {
                    return MessageCode.ItemPackageFull;
                }
                var item = BuildItem(entity.ItemId, (int) EnumItemType.Equipment, entity.ItemCode, 1, entity.IsBinding,entity.IsDeal);
                item.ItemProperty = entity.Property.Clone();
                return SaveUsedItem(item);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ItemPackageFrame Add EquipmentUsed", ex);
                return MessageCode.Exception;
            }
        }

        /// <summary>
        /// 使用过的球员卡返回背包
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MessageCode AddUsedItem(PlayerCardUsedEntity entity)
        {
            if (entity == null)
                return MessageCode.NbParameterError;
            if (!CheckUsedItem(entity.ItemCode, EnumItemType.PlayerCard, entity.ItemId))
                return MessageCode.NbParameterError;
            try
            {
                if (Items.Exists(d => d.ItemId == entity.ItemId))
                    return MessageCode.ItemIdRepeat;
                if (1 > BlankCount)
                {
                    return MessageCode.ItemPackageFull;
                }
                var item = BuildItem(entity.ItemId, (int) EnumItemType.PlayerCard, entity.ItemCode, 1, entity.IsBinding,entity.IsDeal);
                item.ItemProperty = entity.Property.Clone();
                return SaveUsedItem(item);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ItemPackageFrame Add PlayerCardUsed", ex);
                return MessageCode.Exception;
            }
        }

        /// <summary>
        /// 使用过的商城道具返回背包
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public MessageCode AddUsedItem(MallItemUsedEntity entity)
        {

            if (entity == null)
                return MessageCode.NbParameterError;
            if (!CheckUsedItem(entity.ItemCode, EnumItemType.MallItem, entity.ItemId))
                return MessageCode.NbParameterError;
            try
            {
                if (Items.Exists(d => d.ItemId == entity.ItemId))
                {
                    entity.ItemId = Guid.NewGuid(); //return MessageCode.ItemIdRepeat;
                }
                if (1 > BlankCount)
                {
                    return MessageCode.ItemPackageFull;
                }
                var item = BuildItem(entity.ItemId, (int) EnumItemType.MallItem, entity.ItemCode, 1, entity.IsBinding,entity.IsDeal);
                item.ItemProperty = entity.Property.Clone();
                return SaveUsedItem(item);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ItemPackageFrame Add MallItemUsed", ex);
                return MessageCode.Exception;
            }
        }
      
        #endregion

        #region Add

        /// <summary>
        /// 添加一个物品
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public MessageCode AddItem(int itemCode)
        {
            return AddItem(itemCode, false,false);
        }

        /// <summary>
        /// 添加一个物品
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public MessageCode AddItem(int itemCode, int strength)
        {
            return AddItem(itemCode, strength, false,false);
        }

        /// <summary>
        /// 添加一个物品
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="strength"></param>
        /// <param name="isBinding"></param>
        /// <param name="isDeal"></param>
        /// <returns></returns>
        public MessageCode AddItem(int itemCode, int strength, bool isBinding,bool isDeal)
        {
            int itemCount = 1;
            return AddItems(itemCode, itemCount, strength, isBinding, isDeal);
        }

        /// <summary>
        /// 添加一个物品
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="isBinding"></param>
        /// <returns></returns>
        public MessageCode AddItem(int itemCode, bool isBinding,bool isDeal)
        {
            int itemCount = 1;
            return AddItems(itemCode, itemCount, isBinding,isDeal);
        }

        /// <summary>
        /// 添加多个物品,如果返回失败，请勿继续使用当前封装，以免产生脏数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <returns></returns>
        public MessageCode AddItems(int itemCode, int itemCount)
        {
            return AddItems(itemCode, itemCount, false,false);
        }

        /// <summary>
        /// 添加多个物品,如果返回失败，请勿继续使用当前封装，以免产生脏数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <param name="isDeal"></param>
        /// <returns></returns>
        public MessageCode AddItems(int itemCode, int itemCount, bool isBinding,bool isDeal)
        {
            int strength = 1;
            return AddItems(itemCode, itemCount, strength, isBinding,isDeal);
        }

        /// <summary>
        /// ,如果返回失败，请勿继续使用当前封装，以免产生脏数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="strength"></param>
        /// <param name="isBinding"></param>
        /// <returns></returns>
        public MessageCode AddItems(int itemCode, int itemCount, int strength, bool isBinding,bool isDeal, int slotColorCount = 0)
        {
            try
            {
                var dicItem = CacheFactory.ItemsdicCache.GetItem(itemCode);
                if (dicItem == null)
                    return MessageCode.ItemNotExists;
                switch (dicItem.ItemType)
                {
                    case (int) EnumItemType.PlayerCard:
                        return AddPlayerCard(itemCode, itemCount, isBinding, strength, isDeal,1, false,
                            ShareUtil.GenerateComb(), new List<Potential>(), 0, 0);
                    case (int) EnumItemType.Equipment:
                        return AddEquipment(itemCode, itemCount, isBinding, dicItem.LinkId, isDeal, strength, slotColorCount);
                    case (int) EnumItemType.MallItem:
                        return AddMallItem(itemCode, itemCount, isBinding,isDeal);
                    default:
                        return MessageCode.NbParameterError;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ItemPackageFrame AddItems", ex);
                return MessageCode.Exception;
            }
            
        }

        public MessageCode AddGeneralItem(int itemCode, int itemCount, bool isBinding, int itemType,bool isDeal)
        {
            if (itemCount == 0)
                itemCount = 1;
            if (itemCount > BlankCount)
            {
                return MessageCode.ItemPackageFull;
            }
            else
            {
                MessageCode returnCode = MessageCode.Success;
                List<ItemInfoEntity> itemList = new List<ItemInfoEntity>(itemCount);
                for (int i = 0; i < itemCount; i++)
                {
                    var item = BuildItem(itemType, itemCode, 1, isBinding,isDeal);
                    item.ItemProperty = new ItemProperty();
                    returnCode = SaveItem(item);
                    if (returnCode != MessageCode.Success)
                        return returnCode;
                    
                    itemList.Add(item);
                }
                return MessageCode.Success;
            }
        }

        /// <summary>
        /// 添加球员卡,如果返回失败，请勿继续使用当前封装，以免产生脏数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <param name="strength"></param>
        /// <param name="level"></param>
        /// <param name="isMain"></param>
        /// <param name="potential"></param>
        /// <param name="theStar"></param>
        /// <param name="theStarExp"></param>
        /// <returns></returns>
        public MessageCode AddPlayerCard(int itemCode, int itemCount, bool isBinding, int strength,bool isDeal, int level = 1,
            bool isMain = false, Guid teammemberId = new Guid(), List<Potential> potential = null, int theStar = 0,
            int theStarExp = 0)
        {
            if (itemCount == 0)
                itemCount = 1;
            if (strength < 1)
                strength = 1;
            if (strength > 9)
            {
                return MessageCode.ItemLevelOver;
            }
            if (itemCount > BlankCount)
            {
                return MessageCode.ItemPackageFull;
            }
            else
            {
                if (teammemberId == new Guid())
                {
                    teammemberId = ShareUtil.GenerateComb();
                }

                //PlayerCardProperty itemProperty = new PlayerCardProperty(strength, teammemberId, null, false, 0, 0, level, isMain, potential, theStar, theStarExp);
                MessageCode returnCode = MessageCode.Success;
                for (int i = 0; i < itemCount; i++)
                {
                    var item = BuildItem((int)EnumItemType.PlayerCard, itemCode, 1, isBinding,isDeal);
                    //if (i > 0)
                    //{
                       var itemProperty = new PlayerCardProperty(strength,item.ItemId, null, false, 0, 0, level, isMain, potential, theStar, theStarExp);
                    //}
                    item.ItemProperty = itemProperty.Clone();
                    returnCode = SaveItem(item);
                    if (returnCode != MessageCode.Success)
                        return returnCode;
                }
                return MessageCode.Success;
            }
        }

        /// <summary>
        ///初始化背包球员卡
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public MessageCode AddPlayerCard(int itemCode, Guid teammemberId)
        {
            PlayerCardProperty itemProperty = new PlayerCardProperty(1, teammemberId, null, false, 0, 0, 1, true,
                new List<Potential>());
            MessageCode returnCode = MessageCode.Success;
            var item = BuildItem((int) EnumItemType.PlayerCard, itemCode, 1, true,false);
            item.ItemProperty = itemProperty.Clone();
            returnCode = SaveItem(item);
            if (returnCode != MessageCode.Success)
                return returnCode;
            return MessageCode.Success;
        }


        /// <summary>
        /// 换替补时候用
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <param name="strength"></param>
        /// <param name="teammemberId"></param>
        /// <param name="equipment"></param>
        /// <param name="itemId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public MessageCode ReplacePlayerCard(int itemCode, bool isBinding, int strength, Guid teammemberId, EquipmentUsedEntity equipment, Guid itemId,int level = 1,int mainType = 0)
        {
            var deleteItem = GetItem(itemId);
            if (deleteItem == null)
                return MessageCode.ItemNotExists;
            //替换用的球员卡,不被删除，但状态改为主力
            doDeleteInTeammember(deleteItem);
            PlayerCardProperty deleteItemProperty = deleteItem.ItemProperty as PlayerCardProperty;
            if (deleteItemProperty != null) 
            {
                deleteItemProperty.IsMain = true;
                deleteItemProperty.MainType = mainType;
            }
            deleteItem.IsDeal = false;
            Update(deleteItem);

            if (strength < 1)
                strength = 1;
            if (strength > 9)
                return MessageCode.ItemLevelOver;

            var newItem = GetPlayer(teammemberId);
            if (newItem == null)
                newItem = GetItem(teammemberId);
            PlayerCardProperty itemProperty = newItem.ItemProperty as PlayerCardProperty;
            if (itemProperty != null)
            {
                itemProperty.IsMain = false;
                itemProperty.MainType = 0;
            }
            Update(newItem);

            _addPlayer.Add(new PlayerAddEntity(itemCode%100000, teammemberId, ManagerId, itemProperty.Strength, Mod,
                level, null, null));
            return MessageCode.Success;
        }
        

        /// <summary>
        /// 添加装备,如果返回失败，请勿继续使用当前封装，以免产生脏数据
        /// </summary>
        /// <param name="itemCode">物品code</param>
        /// <param name="itemCount">数量</param>
        /// <param name="isBinding">是否绑定</param>
        /// <param name="equipmentId">装备id</param>
        /// <param name="isDeal">是否可出售</param>
        /// <param name="quarity">装备品质</param>
        /// <returns></returns>
        public MessageCode AddEquipment(int itemCode, int itemCount, bool isBinding, int equipmentId,bool isDeal, int level = 0,
            int slotColorCount = 0)
        {
            if (itemCount == 0)
                itemCount = 1;
            if (level > 15)
            {
                return MessageCode.ItemLevelOver;
            }
            if (itemCount > BlankCount)
            {
                return MessageCode.ItemPackageFull;
            }
            else
            {
                MessageCode returnCode = MessageCode.Success;
                for (int i = 0; i < itemCount; i++)
                {
                    var itemProperty = CacheFactory.EquipmentCache.RandomEquipmentProperty(equipmentId, level,
                        slotColorCount);
                    if (itemProperty == null)
                        return MessageCode.ItemEquipmentNotExists;
                    if (level > 1)
                    {
                        itemProperty.Level = level;
                    }
                    var item = BuildItem((int)EnumItemType.Equipment, itemCode, 1, isBinding, isDeal);
                    item.ItemProperty = itemProperty;
                    returnCode = SaveItem(item);
                    if (returnCode != MessageCode.Success)
                        return returnCode;
                }
                return MessageCode.Success;
            }
        }

        public MessageCode AddEquipment(int itemCode, bool isBinding,bool isDeal, EquipmentProperty property)
        {
            var itemCount = 1;
            if (itemCount > BlankCount)
            {
                return MessageCode.ItemPackageFull;
            }
            else
            {
                var item = BuildItem((int) EnumItemType.Equipment, itemCode, 1, isBinding,isDeal);
                item.ItemProperty = property;
                var returnCode = SaveItem(item);
                if (returnCode != MessageCode.Success)
                    return returnCode;
                
                return MessageCode.Success;
            }
        }

        /// <summary>
        /// 添加商城道具,如果返回失败，请勿继续使用当前封装，以免产生脏数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <returns></returns>
        public MessageCode AddMallItem(int itemCode, int itemCount, bool isBinding = false,bool isDeal =false)
        {
            if (itemCount == 0)
                itemCount = 1;
            const int itemType = (int) EnumItemType.MallItem;
            int lapover = CacheFactory.ItemsdicCache.GetLapover(itemType);
            foreach (var item in Items)
            {
                if (item.ItemCode == itemCode && item.IsDeal == isDeal && item.ItemCount < lapover)
                {
                    int totalCount = itemCount + item.ItemCount;
                    if (totalCount > lapover)
                    {
                        var addCount = lapover - item.ItemCount;
                        item.ItemCount = lapover;
                        itemCount = totalCount - lapover;
                        _synchronized = false;

                        AddShadow(item, EnumOperationType.Update, addCount);
                    }
                    else
                    {
                        item.ItemCount = totalCount;
                        _synchronized = false;
                        AddShadow(item, EnumOperationType.Update, itemCount);
                        return MessageCode.Success;
                    }
                }
            }
            int gridCount = itemCount/lapover;
            if (itemCount%lapover > 0)
                gridCount = gridCount + 1;

            if (gridCount > BlankCount)
            {
                return MessageCode.ItemPackageFull;
            }
            else
            {
                MallItemProperty itemProperty = new MallItemProperty();

                MessageCode returnCode = MessageCode.Success;
                for (int i = 0; i < gridCount; i++)
                {
                    int curCount = lapover;
                    if (itemCount < lapover)
                        curCount = itemCount;
                    itemCount -= curCount;
                    var item = BuildItem((int) EnumItemType.MallItem, itemCode, curCount, isBinding,isDeal);
                    item.ItemProperty = itemProperty.Clone();
                    returnCode = SaveItem(item);
                    if (returnCode != MessageCode.Success)
                        return returnCode;
                }
                return MessageCode.Success;
            }
        }

        /// <summary>
        /// 生成物品基本信息
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <param name="isDeal"></param>
        /// <returns></returns>
        public ItemInfoEntity BuildItem(int itemType, int itemCode, int itemCount, bool isBinding,bool isDeal)
        {
            Guid itemId = ShareUtil.GenerateComb();
            LastAddItemId = itemId;
            
            LastAddItem = BuildItem(itemId, itemType, itemCode, itemCount, isBinding,isDeal);
            return LastAddItem;
        }

        /// <summary>
        /// 生成物品基本信息
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemType"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <param name="isDeal"></param>
        /// <returns></returns>
        public ItemInfoEntity BuildItem(Guid itemId, int itemType, int itemCode, int itemCount, bool isBinding,bool isDeal)
        {
            var item = new ItemInfoEntity(itemId, itemCode, itemType);
            item.IsBinding = isBinding;
            item.IsDeal = isDeal;
            item.GridIndex = GetBlankGrid();
            _blanks.Remove(item.GridIndex);
            _itemCount++;

            if (itemCount == 0)
                itemCount = 1;
            item.ItemCount = itemCount;
            return item;
        }

        public MessageCode SaveItem(ItemInfoEntity item)
        {
            if (Shadow == null)
                return MessageCode.ItemNoShadow;
            if (!CheckLapover(item.ItemType, item.ItemCount))
                return MessageCode.ItemCountInvalid;
            Items.Add(item);
            AddShadow(item, EnumOperationType.New, item.ItemCount);
            _synchronized = false;
            return MessageCode.Success;
        }

        //public void UpdatePlayer() 
        //{
        //    foreach (var item in Items)
        //    {
        //        if (item.ItemType == (int)EnumItemType.PlayerCard) 
        //        {
        //            var property = item.ItemProperty as PlayerCardProperty;
        //            if (property != null)
        //            {
        //                if (property.IsMain && property.MainType == 0)
        //                {
        //                    if (TeammemberCore.Instance.GetTeammember(this.ManagerId, item.ItemId) == null)
        //                    {
        //                        property.MainType = 0;
        //                        property.IsMain = false;
        //                    }
        //                }
        //                else
        //                {
        //                    property.IsMain = false;
        //                    property.MainType = 0;
        //                }
        //            }
        //            item.ItemProperty = property;
        //        }
        //    }
        //    _synchronized = false;
        //}

        public MessageCode SaveUsedItem(ItemInfoEntity item)
        {
            if (Shadow == null)
                return MessageCode.ItemNoShadow;
            
            if (!CheckLapover(item.ItemType, item.ItemCount))
                return MessageCode.ItemCountInvalid;
            Items.Add(item);
            AddShadow(item, EnumOperationType.UsedReturn, item.ItemCount);
            _synchronized = false;
            return MessageCode.Success;
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新物品
        /// </summary>
        /// <param name="item">物品</param>
        /// <returns>成功返回true</returns>
        public MessageCode Update(ItemInfoEntity item, int operationCount = 1)
        {
            if (Shadow == null)
                return MessageCode.ItemNoShadow;
            if (!CheckLapover(item.ItemType, item.ItemCount))
                return MessageCode.ItemLapOver;
            try
            {
                var newItem = _itemsDic[item.ItemId];
                if (newItem.Equals(item))
                {
                    _synchronized = false;
                    AddShadow(item, EnumOperationType.Update, operationCount);
                    return MessageCode.Success;
                }
                else
                {
                    return MessageCode.ItemNotExists;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ItemPackageFrame Update", ex);
                return MessageCode.Exception;
            }
            
        }

        #endregion

        #region Delete

        public MessageCode Delete(int itemCode, int deleteCount)
        {
            if (Shadow == null)
                return MessageCode.ItemNoShadow;
            if (deleteCount < 1)
                return MessageCode.NbParameterError;
            try
            {
                var list = _packageEntity.Items.FindAll(d => d.ItemCode == itemCode);
                foreach (var item in list)
                {
                    if (deleteCount > 0)
                    {
                        if (item.ItemCount <= deleteCount)
                        {
                            _itemsDic.Remove(item.ItemId);
                            _blanks.Add(item.GridIndex, item.GridIndex);
                            Items.Remove(item);
                            _itemCount--;
                            _synchronized = false;
                            AddShadow(item, EnumOperationType.Delete, item.ItemCount);
                            deleteCount -= item.ItemCount;
                        }
                        else
                        {
                            item.ItemCount -= deleteCount;
                            _synchronized = false;
                            AddShadow(item, EnumOperationType.Update, deleteCount*-1);
                            deleteCount = 0;
                        }
                    }
                }
                if (deleteCount > 0)
                    return MessageCode.ItemCountInvalid;
                else
                {
                    return MessageCode.Success;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ItemPackageFrame Delete", ex);
                return MessageCode.Exception;
            }
        }

        /// <summary>
        /// 按数量删除物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public MessageCode Delete(Guid itemId, int count = 1, bool isUserDelete = false)
        {
            var item = GetItem(itemId);
            return Delete(item, count, isUserDelete);
        }

        /// <summary>
        /// 按数量删除物品
        /// </summary>
        /// <param name="item"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public MessageCode Delete(ItemInfoEntity item, int count = 1, bool isUserDelete = false)
        {
            if (item != null)
            {
                if (Shadow == null)
                    return MessageCode.ItemNoShadow;
                if (count < 1)
                    return MessageCode.NbParameterError;
                if (isUserDelete)
                {
                    if (CacheFactory.ItemsdicCache.IsNewPlayerPack(item.ItemCode))
                    {
                        return MessageCode.ItemNewplayerPackCantDelete;
                    }
                }
                if (item.Status == (int) EnumItemStatus.Locked)
                {
                    return MessageCode.ItemIsLocked;
                }
                try
                {
                    if (item.ItemCount > count)
                    {
                        item.ItemCount -= count;
                        return Update(item, count*-1);
                    }
                    else if (item.ItemCount <= 0)
                    {
                        return doDelete(item);
                    }
                    else if (item.ItemCount == count)
                    {
                        return doDelete(item);
                    }
                    else
                    {
                        return MessageCode.ItemCountInvalid;
                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("ItemPackageFrame Delete", ex);
                    return MessageCode.Exception;
                }
            }
            return MessageCode.ItemNotExists;
        }

        private MessageCode doDelete(ItemInfoEntity item, bool isDeleteTeammember = false)
        {
            if (item.ItemType == (int) EnumItemType.PlayerCard)
            {
                var property = item.ItemProperty as PlayerCardProperty;
                if (property != null && (property.IsMain || property.IsTrain))
                    return MessageCode.WillCardMain;
            }
            _itemsDic.Remove(item.ItemId);
            _blanks.Add(item.GridIndex, item.GridIndex);
            Items.Remove(item);
            _itemCount--;
            _synchronized = false;
            AddShadow(item, EnumOperationType.Delete, item.ItemCount);
            return MessageCode.Success;
        }

        private void doDeleteInTeammember(ItemInfoEntity item)
        {
            if (item.ItemType == (int) EnumItemType.PlayerCard)
            {
                var proper = item.ItemProperty as PlayerCardProperty;
                var usedPlayerCard = new PlayerCardUsedEntity(item);
                if (proper != null)
                    _deletePlayer.Add(new PlayerAddEntity(item.ItemCode%100000, proper.TeammemberId, ManagerId,
                        proper.Strength, Mod, proper.Level, SerializationHelper.ToByte(usedPlayerCard),
                        SerializationHelper.ToByte(proper.Equipment)));
            }
        }

        #endregion

        #region Split

        /// <summary>
        /// 按数量拆分物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public MessageCode Split(Guid itemId, int count)
        {
            var item = GetItem(itemId);
            return Split(item, count);
        }

        /// <summary>
        /// 按数量拆分物品
        /// </summary>
        /// <param name="item"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public MessageCode Split(ItemInfoEntity item, int count)
        {
            if (item != null)
            {
                if (IsFull)
                    return MessageCode.ItemPackageFull;
                if (Shadow == null)
                    return MessageCode.ItemNoShadow;
                try
                {
                    if (count <= 0)
                        return MessageCode.NbParameterError;
                    if (item.ItemCount <= count)
                    {
                        return MessageCode.ItemSplitCountOver;
                    }
                    else
                    {
                        item.ItemCount = item.ItemCount - count;
                        var code = Update(item, count*-1);
                        if (code != MessageCode.Success)
                            return code;
                        var newItem = item.Clone();
                        newItem.GridIndex = GetBlankGrid();
                        _blanks.Remove(newItem.GridIndex);
                        _itemCount++;
                        newItem.ItemId = ShareUtil.GenerateComb();
                        newItem.ItemCount = count;
                        LastAddItemId = newItem.ItemId;
                        return SaveItem(newItem);
                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("ItemPackageFrame Split", ex);
                    return MessageCode.Exception;
                }
            }
            return MessageCode.ItemNotExists;
        }

        #endregion

        #region GetItem

        /// <summary>
        /// Gets the type of the items by.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public List<ItemInfoEntity> GetItemsByType(int type)
        {
            if (type <= 0)
                return Items;
            if (Items == null)
                return null;
            return Items.FindAll(d => d.ItemType == type);
        }

        /// <summary>
        /// 根据物品id获得物品
        /// </summary>
        /// <param name="itemId">The item id.</param>
        /// <returns>道具</returns>
        public ItemInfoEntity GetItem(Guid itemId)
        {
            if (_itemsDic != null && _itemsDic.ContainsKey(itemId))
            {
                return _itemsDic[itemId];
            }
            return null;
        }

        /// <summary>
        /// 根据阵型ID获取球员卡
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public ItemInfoEntity GetPlayer(Guid teammemberId)
        {
            var item = GetItem(teammemberId);
            if (item == null)
            {
                if (_playDic != null && _playDic.ContainsKey(teammemberId))
                {
                    var player = _playDic[teammemberId];
                    if (player != null)
                    {
                        item = GetItem(player.ItemId);
                        var property = player.ItemProperty as PlayerCardProperty;
                        if (item!=null && property != null && player.ItemId != property.TeammemberId)
                        {
                            player.ItemId = property.TeammemberId;
                            item.ItemId = property.TeammemberId;
                        }
                    }
                }
            }
            return item;
        }

        /// <summary>
        /// 根据阵型ID获取球员卡
        /// </summary>
        /// <param name="teammemberId"></param>
        /// <returns></returns>
        public List<ItemInfoEntity> GetPlayer()
        {
            return _playDic.Values.ToList();
        }

        /// <summary>
        /// 检查包内物品
        /// </summary>
        /// <param name="itemCode">物品编号</param>
        /// <returns>符合返回true</returns>
        public List<ItemInfoEntity> GetListByItemCode(int itemCode)
        {
            if (Items == null)
                return null;
            if (itemCode == 0)
                return null;
            return Items.FindAll(d => d.ItemCode == itemCode);
        }

        /// <summary>
        /// 检查包内物品
        /// </summary>
        /// <param name="itemCode">物品编号</param>
        /// <returns>符合返回true</returns>
        public ItemInfoEntity GetByItemCode(int itemCode)
        {
            if (Items == null)
                return null;
            if (itemCode == 0)
                return null;
            return Items.Find(d => d.ItemCode == itemCode);
        }

        /// <summary>
        /// 获取背包中对应级别球员卡数量
        /// </summary>
        /// <param name="cardLevel"></param>
        /// <returns></returns>
        public int GetPlayCountByCardLevel(int cardLevel)
        {
            int count = 0;
            foreach (var item in Items)
            {
                if (item.ItemType == (int) EnumItemType.PlayerCard)
                {
                    var itemDic = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                    if (itemDic != null && itemDic.SubType == cardLevel)
                        count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 获取背包中对应等级以上球员卡数量
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetLevelCardCount(int level)
        {
            int count = 0;
            foreach (var item in Items)
            {
                if (item.ItemType == (int) EnumItemType.PlayerCard)
                {
                    var cardProp = item.ItemProperty as PlayerCardProperty;
                    if (cardProp != null && cardProp.Level >= level)
                        count++;
                }
            }
            return count;

        }

        /// <summary>
        /// 获取背包中某一级别对应强化等级以上的数据
        /// </summary>
        /// <param name="cardLevel">球员卡等级 0表示不判断级别</param>
        /// <param name="strengthLevel">强化等级</param>
        /// <returns></returns>
        public int GetStrengthCardCount(int cardLevel, int strengthLevel)
        {
            int count = 0;
            foreach (var item in Items)
            {
                if (item.ItemType == (int) EnumItemType.PlayerCard)
                {
                    var cardProp = item.ItemProperty as PlayerCardProperty;
                    if (cardLevel == 0)
                    {
                        if (cardProp != null && cardProp.Strength >= strengthLevel)
                            count++;
                    }
                    else
                    {
                        if (cardProp != null && cardProp.Strength >= strengthLevel)
                        {
                            var itemDic = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                            if (itemDic.SubType == cardLevel)
                                count++;
                        }
                    }
                    
                }
            }
            return count;
        }


        #endregion

        #region ItemValidate

        /// <summary>
        /// 检查包内是否有符合条件的物品
        /// </summary>
        /// <param name="itemCode">物品编号</param>
        /// <param name="validateCount">需要检验的数量</param>
        /// <returns>符合返回true</returns>
        public bool ItemValidate(int itemCode, int validateCount)
        {
            int count = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                if (itemCode == Items[i].ItemCode)
                {
                    count += Items[i].ItemCount;
                }
                if (count >= validateCount)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查包内物品数量
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int GetItemNumber(int itemCode)
        {
            int count = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                if (itemCode == Items[i].ItemCode)
                    count += Items[i].ItemCount;
            }
            return count;
        }

        /// <summary>
        /// 检查包内是否有符合条件的物品
        /// </summary>
        /// <param name="itemId">物品唯一编号</param>
        /// <param name="validateCount">需要检验的数量</param>
        /// <returns>符合返回true</returns>
        public bool ItemValidate(Guid itemId, int validateCount)
        {
            int count = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                if (itemId == Items[i].ItemId)
                {
                    count += Items[i].ItemCount;
                }
                if (count >= validateCount)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Arrange

        /// <summary>
        /// 整理背包
        /// </summary>
        public MessageCode Arrange()
        {
            if (Shadow == null)
                return MessageCode.ItemNoShadow;
            if (Items != null && Items.Count > 0)
            {
                List<ItemInfoEntity> removeList = new List<ItemInfoEntity>();
                List<ItemInfoEntity> updateList = new List<ItemInfoEntity>();
                for (int i = 0; i < Items.Count; i++)
                {
                    if (removeList.Contains(Items[i]))
                        continue;
                    Stacking(Items[i], removeList);
                }
                foreach (var entity in removeList)
                {
                    Items.Remove(entity);
                }

                foreach (var item in Items)
                {
                    var itemDic = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                    if (itemDic == null)
                    {
                        return MessageCode.NbUpdateFail;
                    }
                    item.SubType = itemDic.SubType;
                }

                Items.Sort(new CompareItem());

                int k = 1;
                foreach (var item in Items)
                {
                    item.GridIndex = k;
                    UpdateShadowItemGrid(item.ItemId, item.GridIndex);
                    k++;
                }
                _synchronized = false;
            }
            Shadow.AddShadow(PackageSize, NewItemString, _packageEntity.ItemVersion);
            return MessageCode.Success;
        }

        #endregion

        #region Save

        /// <summary>
        /// 保存没有球员卡的，如果没有变动，直接return true
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return Save(null);
        }

        public bool Save(DbTransaction transaction)
        {
            if (_synchronized)
                return true;
            
            _packageEntity.ItemString = GenerateItemString();
            AchievementTaskCore.Instance.UpdatePlayCardCount(this, _zoneId);
            return ItemPackageMgr.Update(_packageEntity, transaction, _zoneId);
        }

       

        public bool SaveTask(DbTransaction transaction)
        {
            if (_synchronized)
                return true;
            _packageEntity.ItemString = GenerateItemString();

            return ItemPackageMgr.Update(_packageEntity, transaction, _zoneId);
        }

        /// <summary>
        /// 换替补的时候用
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool SavePlayer(DbTransaction transaction)
        {
            if (_synchronized)
                return true;

            //添加卡，在整容中删除
            if (_addPlayer.Count > 0)
            {
                foreach (var item in _addPlayer)
                {
                    if (!ItemPackageMgr.AddPlayer(item.TeammemberId, item.Mod, transaction, _zoneId))
                        return false; //移除训练缓存
                    //PlayerTrain.Instance.RemovetrainDic(item.TeammemberId);
                }
                MatchDataCacheHelper.DeleteTeamembersCache(ManagerId, false);
            }
            //删除卡 添加到阵容中
            if (_deletePlayer.Count > 0)
            {
                foreach (var item in _deletePlayer)
                {
                    if (
                        !ItemPackageMgr.DeletePlayer(item.PlayerId, item.ManagerId, item.TeammemberId,
                            item.StrengthenLevel, item.UsedPlayerCard, item.UsedEquipment, item.Level, item.Mod,
                            transaction, _zoneId))
                        return false;
                }
                MatchDataCacheHelper.DeleteTeamembersCache(ManagerId, false);
            }

            _packageEntity.ItemString = GenerateItemString();
            return ItemPackageMgr.Update(_packageEntity, transaction, _zoneId);
        }

        #endregion

        #region encapsulation

        private int _itemCount;

        private bool _synchronized = true; //表示包裹信息和字符串是否一致了

        private List<ItemInfoEntity> Items
        {
            get { return _packageEntity.Items; }
        }

        private Dictionary<Guid, ItemInfoEntity> _itemsDic;
        private Dictionary<Guid, ItemInfoEntity> _playDic;

        /// <summary>
        /// 空格子
        /// </summary>
        private Dictionary<int, int> _blanks;

        #region AddShadow

        private void AddShadow(ItemInfoEntity item, EnumOperationType operationType, int operationCount)
        {
            if (Shadow != null)
            {
                Shadow.AddShadow(item, operationType, operationCount);
            }
        }

        private void UpdateShadowItemGrid(Guid itemId, int grid)
        {
            if (Shadow != null)
            {
                Shadow.UpdateItemGrid(itemId, grid);
            }
        }

        #endregion

        #region AnalysePackage

        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>成功返回true</returns>
        private void AnalysePackage()
        {
            var packageItemsEntity = SerializationHelper.FromByte<ItemPackageItemsEntity>(_packageEntity.ItemString);

            if (packageItemsEntity == null || packageItemsEntity.Items == null)
                _packageEntity.Items = new List<ItemInfoEntity>();
            else
            {
                _packageEntity.Items = packageItemsEntity.Items;
            }
            BuildPackageGrid();
            
        }

        public static void CaluPackageCardKpi(List<ItemInfoEntity> playerCards, List<NBSolutionTeammember> teammember)
        {
            if (teammember == null || teammember.Count <= 0)
                return;
            var dicTeamKpi = teammember.ToDictionary(d => d.Idx, d => d.Kpi);
            foreach (var entity in playerCards)
            {
                if (entity.ItemType == (int) EnumItemType.PlayerCard)
                {
                    var playerCardProperty = entity.ItemProperty as PlayerCardProperty;
                    if (playerCardProperty != null)
                    {
                        playerCardProperty.TheActualKpi = -1;
                        if (playerCardProperty.IsMain)
                        {
                            if (dicTeamKpi.ContainsKey(playerCardProperty.TeammemberId))
                            {
                                playerCardProperty.TheActualKpi = (int) dicTeamKpi[playerCardProperty.TeammemberId];
                            }
                        }
                    }
                }
            }
        }

        public void CaluKpiForPackageCard()
        {
            //var data = new DTOBuffMemberView();
            //var buffPlayers = new Dictionary<Guid, DTOBuffPlayer>();
            //var playerCards = GetItemsByType((int)EnumItemType.PlayerCard);
            //foreach (var entity in playerCards)
            //{
            //    var playerCardProperty = entity.ItemProperty as PlayerCardProperty;
            //    var playerInfo = CacheFactory.ItemsdicCache.GetPlayerByItemCode(entity.ItemCode);
            //    if (playerCardProperty != null)
            //    {
            //        buffPlayers.Add(entity.ItemId,
            //            BuildPlayer(playerCardProperty.Level, playerCardProperty.Strength, playerInfo.Idx,
            //                playerCardProperty.Equipment, "", playerInfo.Position, 100));
            //        NbManagerbuffmemberEntity member = CreateBuffMember(entity.ItemId, buffPlayers[entity.ItemId]);
            //        member.AsKpi();
            //        playerCardProperty.Kpi = member.Kpi;
            //    }
            //}
        }

        private static NbManagerbuffmemberEntity CreateBuffMember(Guid tid, DTOBuffPlayer player)
        {
            if (null == player)
                return null;
            var member = new NbManagerbuffmemberEntity();
            member.Tid = tid;
            member.Pid = player.AsPid;
            member.PPos = player.Pos;
            member.PPosOn = player.PosOn;
            member.Level = player.Level;
            member.Strength = player.Strength;
            member.IsMain = player.OnFlag;
            member.ShowOrder = player.ShowOrder;
            var props = player.Props;
            member.SpeedConst = props[0].Orig;
            member.ShootConst = props[1].Orig;
            member.FreeKickConst = props[2].Orig;
            member.BalanceConst = props[3].Orig;
            member.PhysiqueConst = props[4].Orig;
            member.BounceConst = props[5].Orig;
            member.AggressionConst = props[6].Orig;
            member.DisturbConst = props[7].Orig;
            member.InterceptionConst = props[8].Orig;
            member.DribbleConst = props[9].Orig;
            member.PassConst = props[10].Orig;
            member.MentalityConst = props[11].Orig;
            member.ResponseConst = props[12].Orig;
            member.PositioningConst = props[13].Orig;
            member.HandControlConst = props[14].Orig;
            member.AccelerationConst = props[15].Orig;
            return member;
        }

        private static DTOBuffPlayer BuildPlayer(int level, int strength, int playerId, EquipmentUsedEntity equipment,
            string skill, int position, int buffScale)
        {
            DicPlayerEntity cfg = MatchDataUtil.GetDicPlayer(Guid.Empty, playerId);
            var rawProps = cfg.GetRawProps();
            var obj = new DTOBuffPlayer();
            obj.Pid = cfg.Idx;
            obj.Pos = position;
            obj.Clr = cfg.CardLevel;
            obj.Props = new DTOBuffProp[rawProps.Length];

            for (int i = 0; i < rawProps.Length; ++i)
            {
                obj.Props[i] = new DTOBuffProp {Orig = rawProps[i]};
                obj.Props[i].Percent = (buffScale - 100)/100.00;
            }
            rawProps = null;
            obj.Level = level;
            obj.Strength = strength;
            obj.SBMList = new List<string>();
            obj.ActionSkill = skill;

            if (equipment != null)
            {
                var equipmentProperty = equipment.Property;
                double equipPlus = 0;
                foreach (var plus in equipmentProperty.PropertyPluses)
                {
                    equipPlus = plus.PlusValue*(1 + equipmentProperty.Level*0.1);
                    if (plus.PlusType == (int) EnumPlusType.Percent)
                        obj.Props[plus.PropertyId - 1].Percent += equipPlus/100.00;
                    else
                    {
                        obj.Props[plus.PropertyId - 1].Point += equipPlus;
                    }
                }
            }

            return obj;
        }


        /// <summary>
        /// 计算空格数
        /// </summary>
        private void BuildPackageGrid()
        {
            _blanks = new Dictionary<int, int>();
            _itemsDic = new Dictionary<Guid, ItemInfoEntity>(Items.Count);
            _playDic = new Dictionary<Guid, ItemInfoEntity>();
            for (int i = 0; i < _packageEntity.PackageSize; i++)
            {
                _blanks.Add(i + 1, i + 1);
            }
            foreach (ItemInfoEntity item in Items)
            {
                try
                {
                    if (!_itemsDic.ContainsKey(item.ItemId))
                        _itemsDic.Add(item.ItemId, item);
                    if (item.ItemType == (int) EnumItemType.PlayerCard)
                    {
                        var playerPro = item.ItemProperty as PlayerCardProperty;
                        if (playerPro != null)
                        {
                            if (playerPro.TeammemberId != item.ItemId)
                            {
                                item.ItemId = playerPro.TeammemberId;
                                item.ItemProperty = playerPro;
                            }
                            if (!_playDic.ContainsKey(playerPro.TeammemberId))
                                _playDic.Add(playerPro.TeammemberId, item);
                        }
                    }
                    _blanks.Remove(item.GridIndex); //筛选空格
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("BuildPackageGrid", "itemid exists,id:" + item.ItemId + ",code:" + item.ItemCode);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 检查球员卡训练状态
        /// </summary>
        public void CheckPlayerTrain()
        {
            foreach (var item in Items)
            {
                if (item.ItemType == (int) EnumItemType.PlayerCard)
                {
                    var playerPro = item.ItemProperty as PlayerCardProperty;
                    if (playerPro != null)
                    {
                        playerPro.IsTrain = PlayerTrain.Instance.GetIsTrain(playerPro.TeammemberId);
                        item.ItemProperty = playerPro;
                    }
                }
            }
        }

        #endregion

        #region GenerateItemString

        /// <summary>
        /// 获取物品字符串
        /// </summary>
        private
            byte[] GenerateItemString()
        {
            return SerializationHelper.ToByte(Items);
        }

        #endregion

        #region Stacking

        private void Stacking(ItemInfoEntity item, List<ItemInfoEntity> removeList)
        {
            int lapoverCount = CacheFactory.ItemsdicCache.GetLapover(item.ItemType);
            if (lapoverCount > 1) //可堆叠
            {
                if (item.ItemCount < lapoverCount)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        
                        var curItem = Items[i];
                        if (removeList.Contains(curItem))
                            continue;
                        if (curItem.ItemCount < lapoverCount && curItem.ItemId != item.ItemId &&
                            curItem.ItemCode == item.ItemCode && curItem.IsDeal == item.IsDeal)
                        {
                            int addCount = lapoverCount - item.ItemCount;
                            if (curItem.ItemCount > addCount)
                            {
                                item.ItemCount += addCount;
                                curItem.ItemCount = curItem.ItemCount - addCount;
                                AddShadow(item, EnumOperationType.Update, addCount);
                                AddShadow(curItem, EnumOperationType.Update, -1*addCount);
                            }
                            else
                            {
                                item.ItemCount += curItem.ItemCount;
                                AddShadow(item, EnumOperationType.Update, curItem.ItemCount);
                                removeList.Add(curItem);
                            }

                            if (item.ItemCount == lapoverCount)
                                return;
                        }
                    }
                }
            }
        }

        #endregion

        #region GetBlankGrid

        /// <summary>
        /// 获取空格子
        /// </summary>
        /// <returns></returns>
        private int GetBlankGrid()
        {
            var enumerator = _blanks.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current.Key;
        }

        #endregion

        private bool CheckLapover(int itemType, int itemCount)
        {
            if (itemCount == 1)
                return true;
            int lapover = CacheFactory.ItemsdicCache.GetLapover(itemType);
            if (itemCount > lapover)
                return false;
            return true;
        }

        private bool CheckUsedItem(int itemCode, EnumItemType itemType, Guid itemId)
        {
            if (itemCode <= 0 || itemId == Guid.Empty)
                return false;
            var dicItem = CacheFactory.ItemsdicCache.GetItem(itemCode);
            if (dicItem == null || dicItem.ItemType != (int) itemType)
                return false;
            return true;
        }

        #endregion
    }

    public class PlayerAddEntity
    {
        public int PlayerId { get; set; }
        public Guid TeammemberId { get; set; }
        public Guid ManagerId { get; set; }
        public int StrengthenLevel { get; set; }
        public int Level { get; set; }
        public int Mod { get; set; }
        public Byte[] UsedPlayerCard { get; set; }
        public Byte[] UsedEquipment { get; set; }

        public PlayerAddEntity(int playerId, Guid teammemberId, Guid managerId, int strengthenLevel, int mod, int level, byte[] usedPlayerCard, byte[] usedEquipment)
        {
            this.PlayerId = playerId;
            this.TeammemberId = teammemberId;
            this.ManagerId = managerId;
            this.StrengthenLevel = strengthenLevel;
            this.Mod = mod;
            this.Level = level;
            this.UsedPlayerCard = usedPlayerCard;
            this.UsedEquipment = usedEquipment;
        }
    }
}
