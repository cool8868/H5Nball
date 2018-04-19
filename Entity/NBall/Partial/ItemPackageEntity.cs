using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class ItemPackageEntity
	{
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///      2009-12-29 13:54     Created
        /// </history>
        [DataMember]
        public List<ItemInfoEntity> Items { get; set; }
	}
	
	
    public partial class ItemPackageResponse
    {

    }
    [DataContract]
    [Serializable]
    [ProtoContract]
    public class ItemPackageItemsEntity
    {
        [ProtoMember(1)]
        public List<ItemInfoEntity> Items { get; set; }
    }

    /// <summary>
    /// 对Table dbo.ItemPackageEntity 的输出映射.
    /// </summary>	
    [DataContract]
    [Serializable]
    [ProtoContract]
    [ProtoInclude(1, typeof(ItemPackageData))]
    public class ItemPackageDataResponse : BaseResponse<ItemPackageData>
    {

    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class ItemPackageData
    {
        /// <summary>
        /// 物品列表.
        /// </summary>
        /// <value>The items.</value>
        /// <remarks>
        /// </remarks>
        /// <history>
        ///      2009-12-29 13:54     Created
        /// </history>
        [DataMember]
        [ProtoMember(1)]
        public List<ItemInfoEntity> Items { get; set; }

        ///<summary>
        ///背包大小
        ///</summary>
        [DataMember]
        [ProtoMember(2)]
        public System.Int32 PackageSize { get; set; }
    }

    /// <summary>
    /// 物品比较
    /// 先判断itemtype，小的排前面，再判断itemcode,最后判断strengthen,接着判断数量，大的放前面
    /// </summary>
    public class CompareItem : IComparer<ItemInfoEntity>
    {
        #region CompareItem
        /// <summary>
        /// 比较两个物品
        /// 先判断itemtype，小的排前面，再判断itemcode,最后判断strengthen,接着判断数量，大的放前面
        /// </summary>
        /// <param name="itemx"></param>
        /// <param name="itemy"></param>
        /// <returns></returns>
        public int Compare(ItemInfoEntity itemx, ItemInfoEntity itemy)
        {
            if (itemx == null)
            {
                if (itemy == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (itemy == null)
                {
                    return 1;
                }
                else
                {
                    //先判断itemtype，小的排前面，再判断itemcode,最后判断strengthen
                    var result = doComapre(itemx.ItemType, itemy.ItemType);
                    if (result != 0)
                    {
                        return result;
                    }
                    int subTypex = itemx.SubType;
                    int subTypey = itemy.SubType;
                    if (itemx.ItemType == 1)
                    {
                        if (subTypex == 7 || subTypex == 8)
                            subTypex = 1;
                    }
                    if (itemy.ItemType == 1)
                    {
                        if (subTypey == 7 || subTypey == 8)
                            subTypey = 1;
                    }
                    result = doComapre(subTypex, subTypey);
                    if (result != 0)
                    {
                        return result;
                    }

                    result = doComapre(itemx.ItemCode, itemy.ItemCode);
                    if (result != 0)
                    {
                        return result;
                    }
                    result = doComapre(itemy.GetStrength(), itemx.GetStrength());
                    if (result != 0)
                    {
                        return result;
                    }
                    result = doComapre(itemy.ItemCount, itemx.ItemCount);
                    return result;
                }
            }
        }

        static int doComapre(int valueleft, int valueright)
        {
            if (valueleft < valueright)
            {
                return -1;
            }
            else if (valueleft > valueright)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}

