using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Item;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Core.Constellation
{
    /// <summary>
    /// 星座背包
    /// </summary>
    public class ConstellationPackbager
    {
         #region .ctor
        private ConstellationPackageEntity _packageEntity;
        private string _zoneId="";

        public ConstellationPackbager(Guid managerId,string zoneId = "")
        {
            var package = ConstellationPackageMgr.GetById(managerId, zoneId);
            if (package == null)
            {
                try
                {
                    package = new ConstellationPackageEntity();
                    package.ManagerId = managerId;
                    package.PackageSize = 480;
                    package.RowTime = DateTime.Now;
                    package.ItemString = new byte[0];
                    ConstellationPackageMgr.Insert(package);
                    package = ConstellationPackageMgr.GetById(managerId);
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("星座背包加数据", ex);
                    package = null;
                }
            }
            _packageEntity = package;
            AnalysePackage();
        }

        #endregion

        #region Fields

        /// <summary>
        /// 所属经理id
        /// </summary>
        public Guid ManagerId { get { return _packageEntity.ManagerId; } }
        /// <summary>
        /// 最近添加的物品id
        /// </summary>
        public Guid LastAddItemId { get; set; }

        /// <summary>
        /// 背包
        /// </summary>
        public ConstellationPackageEntity Package { get { return _packageEntity; }}

        public ConstellationInfoEntity LastAddItem { get; set; }

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
            get
            {
                return _itemCount;
            }
        }

        /// <summary>
        /// 包裹内空格子数.
        /// </summary>
        /// <value>The blank count.</value>
        public int BlankCount
        {
            get
            {
                return _blanks.Count;
            }
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

        #region Add
        
        /// <summary>
        /// 添加一个物品
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="isBinding"></param>
        /// <returns></returns>
        public MessageCode AddItem(int itemCode, bool isBinding)
        {
            int itemCount = 1;
            return AddItems(itemCode, itemCount, isBinding);
        }

        /// <summary>
        /// ,如果返回失败，请勿继续使用当前封装，以免产生脏数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <returns></returns>
        public MessageCode AddItems(int itemCode, int itemCount, bool isBinding)
        {
            try
            {
                var dicItem = CacheFactory.ItemsdicCache.GetItem(itemCode);
                if (dicItem == null)
                    return MessageCode.ItemNotExists;
                switch (dicItem.ItemType)
                {
                    case (int)EnumItemType.MallItem:
                        return AddMallItem(itemCode, itemCount, isBinding);
                    default:
                        return MessageCode.NbParameterError;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("CostellationPackageFrame AddItems",ex);
                return MessageCode.Exception;
            }
            
        }

        /// <summary>
        /// 添加商城道具,如果返回失败，请勿继续使用当前封装，以免产生脏数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="isBinding"></param>
        /// <returns></returns>
        public MessageCode AddMallItem(int itemCode, int itemCount, bool isBinding = false)
        {
            if (itemCount == 0)
                itemCount = 1;
            const int itemType = (int) EnumItemType.MallItem;
            int lapover = CacheFactory.ItemsdicCache.GetLapover(itemType);
            foreach (var item in Items)
            {
                if (item.ItemCode == itemCode && item.IsBinding == isBinding && item.ItemCount < lapover)
                {
                    int totalCount = itemCount + item.ItemCount;
                    if (totalCount > lapover)
                    {
                        item.ItemCount = lapover;
                        itemCount = totalCount - lapover;
                        _synchronized = false;
                    }
                    else
                    {
                        item.ItemCount = totalCount;
                        _synchronized = false;
                        return MessageCode.Success;
                    }
                }
            }
            int gridCount = itemCount/lapover;
            if (itemCount%lapover > 0)
                gridCount=gridCount+1;

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
                    var item = BuildItem((int) EnumItemType.MallItem, itemCode, curCount, isBinding);
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
        /// <returns></returns>
        public ConstellationInfoEntity BuildItem(int itemType, int itemCode, int itemCount, bool isBinding)
        {
            Guid itemId = ShareUtil.GenerateComb();
            LastAddItemId = itemId;
            
            LastAddItem= BuildItem(itemId,itemType, itemCode, itemCount, isBinding);
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
        /// <returns></returns>
        public ConstellationInfoEntity BuildItem(Guid itemId,int itemType, int itemCode, int itemCount, bool isBinding)
        {
            var item = new ConstellationInfoEntity(itemId, itemCode, itemType);
            item.IsBinding = isBinding;

            item.GridIndex = GetBlankGrid();
            _blanks.Remove(item.GridIndex);
            _itemCount++;

            if (itemCount == 0)
                itemCount = 1;
            item.ItemCount = itemCount;
            return item;
        }

        public MessageCode SaveItem(ConstellationInfoEntity item)
        {
            if (!CheckLapover(item.ItemType, item.ItemCount))
                return MessageCode.ItemCountInvalid;
            Items.Add(item);
            _synchronized = false;
            return MessageCode.Success;
        }

        public MessageCode SaveUsedItem(ConstellationInfoEntity item)
        {
          
            if (!CheckLapover(item.ItemType, item.ItemCount))
                return MessageCode.ItemCountInvalid;
            Items.Add(item);
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
        public MessageCode Update(ConstellationInfoEntity item,int operationCount=1)
        {
           
            if (!CheckLapover(item.ItemType, item.ItemCount))
                return MessageCode.ItemLapOver;
            try
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Equals(item))
                    {
                        Items[i] = item;
                        _synchronized = false;
                        return MessageCode.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("ConstellationPackageFrame Update", ex);
                return MessageCode.Exception;
            }
            return MessageCode.ItemNotExists;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 按数量删除物品
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public MessageCode Delete(Guid itemId, int count = 1,bool isUserDelete=false)
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
        public MessageCode Delete(ConstellationInfoEntity item, int count = 1, bool isUserDelete = false)
        {
            if (item != null)
            {
                if (item.Status == (int) EnumItemStatus.Locked)
                {
                    return MessageCode.ItemIsLocked;
                }
                try
                {
                    if (count>0 && item.ItemCount > count)
                    {
                        item.ItemCount -= count;
                        return Update(item,count*-1);
                    }
                    else
                    {
                        _itemsDic.Remove(item.ItemId);
                        _blanks.Add(item.GridIndex, item.GridIndex);
                        Items.Remove(item);
                        _itemCount--;
                        _synchronized = false;
                        return MessageCode.Success;
                    }
                }
                catch (Exception ex)
                {
                    SystemlogMgr.Error("ConstellationPackageFrame Delete",ex);
                    return MessageCode.Exception;
                }
            }
            return MessageCode.ItemNotExists;
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
        public MessageCode Split(ConstellationInfoEntity item, int count)
        {
            if (item != null)
            {
                if (IsFull)
                    return MessageCode.ItemPackageFull;
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
                        var code = Update(item, count * -1);
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
                    SystemlogMgr.Error("ConstellationPackbager Split", ex);
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
        public List<ConstellationInfoEntity> GetItemsByType(int type)
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
        public ConstellationInfoEntity GetItem(Guid itemId)
        {
            if (_itemsDic != null && _itemsDic.ContainsKey(itemId))
            {
                return _itemsDic[itemId];
            }
            return null;
        }

        /// <summary>
        /// 检查包内物品
        /// </summary>
        /// <param name="itemCode">物品编号</param>
        /// <returns>符合返回true</returns>
        public List<ConstellationInfoEntity> GetListByItemCode(int itemCode)
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
        public ConstellationInfoEntity GetByItemCode(int itemCode)
        {
            if (Items == null)
                return null;
            if (itemCode == 0)
                return null;
            return Items.Find(d => d.ItemCode == itemCode);
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
            if (Items != null && Items.Count > 0)
            {
                List<ConstellationInfoEntity> removeList = new List<ConstellationInfoEntity>();
                List<ConstellationInfoEntity> updateList = new List<ConstellationInfoEntity>();
                for (int i = 0; i < Items.Count; i++)
                {
                    if(removeList.Contains(Items[i]))
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

                Items.Sort(new CompareConstellation());

                int k = 1;
                foreach (var item in Items)
                {
                    item.GridIndex = k;
                    k++;
                }
                _synchronized = false;
            }
            return MessageCode.Success;
        }
        #endregion

        #region Save
        /// <summary>
        /// 保存，如果没有变动，直接return true
        /// </summary>
        /// <returns></returns>
        public bool Save(string zoneId = "")
        {
            return Save(null,zoneId);
        }

        public bool Save(DbTransaction transaction,string zoneId = "")
        {
            if (_synchronized)
                return true;
            _packageEntity.ItemString = GenerateItemString();

            return ConstellationPackageMgr.Update(_packageEntity, transaction, zoneId);
        }
        #endregion

        #region encapsulation

        private int _itemCount;

        private bool _synchronized = true; //表示包裹信息和字符串是否一致了

        private List<ConstellationInfoEntity> Items { get { return _packageEntity.Items; } }

        private Dictionary<Guid, ConstellationInfoEntity> _itemsDic;
        /// <summary>
        /// 空格子
        /// </summary>
        private Dictionary<int, int> _blanks;

        #region AnalysePackage
        /// <summary>
        /// 解析
        /// </summary>
        /// <returns>成功返回true</returns>
        private void AnalysePackage()
        {
            try
            {
                var packageItemsEntity = SerializationHelper.FromByte<ConstellationPackageItemsEntity>(_packageEntity.ItemString);

                if (packageItemsEntity == null || packageItemsEntity.Items==null)
                    _packageEntity.Items = new List<ConstellationInfoEntity>();
                else
                {
                    _packageEntity.Items = packageItemsEntity.Items;
                }
                BuildPackageGrid();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
        }

        /// <summary>
        /// 计算空格数
        /// </summary>
        private void BuildPackageGrid()
        {
            _blanks = new Dictionary<int, int>();
            _itemsDic = new Dictionary<Guid, ConstellationInfoEntity>(Items.Count);
            for (int i = 0; i < _packageEntity.PackageSize; i++)
            {
                _blanks.Add(i + 1, i + 1);
            }
            foreach (ConstellationInfoEntity item in Items)
            {
                _itemsDic.Add(item.ItemId, item);
                _blanks.Remove(item.GridIndex);//筛选空格
            }
        }
        #endregion

        #region GenerateItemString

        /// <summary>
        /// 获取物品字符串
        /// </summary>
        private byte[] GenerateItemString()
        {
            return SerializationHelper.ToByte(Items);
        }
        #endregion

        #region Stacking
        void Stacking(ConstellationInfoEntity item, List<ConstellationInfoEntity> removeList)
        {
            int lapoverCount = CacheFactory.ItemsdicCache.GetLapover(item.ItemType);
            if (lapoverCount > 1)//可堆叠
            {
                if (item.ItemCount < lapoverCount)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {

                        var curItem = Items[i];
                        if (removeList.Contains(curItem))
                            continue;
                        if (curItem.ItemCount < lapoverCount && curItem.ItemId != item.ItemId && curItem.ItemCode == item.ItemCode && curItem.IsBinding == item.IsBinding)
                        {
                            int addCount = lapoverCount - item.ItemCount;
                            if (curItem.ItemCount > addCount)
                            {
                                item.ItemCount += addCount;
                                curItem.ItemCount = curItem.ItemCount - addCount;
                            }
                            else
                            {
                                item.ItemCount += curItem.ItemCount;
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
        int GetBlankGrid()
        {
            var enumerator = _blanks.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current.Key;
        }
        #endregion

        bool CheckLapover(int itemType,int itemCount)
        {
            if (itemCount == 1)
                return true;
            int lapover = CacheFactory.ItemsdicCache.GetLapover(itemType);
            if (itemCount > lapover)
                return false;
            return true;
        }

        bool CheckUsedItem(int itemCode, EnumItemType itemType, Guid itemId)
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
}
