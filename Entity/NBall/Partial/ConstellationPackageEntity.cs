using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ProtoBuf;

namespace Games.NBall.Entity
{    

	public partial class ConstellationPackageEntity
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
        public List<ConstellationInfoEntity> Items { get; set; }
	}
	
	
    public partial class ConstellationPackageResponse
    {

    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class ConstellationPackageItemsEntity
    {
        [ProtoMember(1)]
        public List<ConstellationInfoEntity> Items { get; set; }
    }

    /// <summary>
    /// 对Table dbo.ItemPackageEntity 的输出映射.
    /// </summary>	
    [DataContract]
    [Serializable]
    [ProtoContract]
    [ProtoInclude(1, typeof(ConstellationPackageData))]
    public class ConstellationPackageDataResponse : BaseResponse<ConstellationPackageData>
    {

    }

    [DataContract]
    [Serializable]
    [ProtoContract]
    public class ConstellationPackageData
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
        public List<ConstellationInfoEntity> Items { get; set; }

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
    public class CompareConstellation : IComparer<ConstellationInfoEntity>
    {
        #region CompareItem
        /// <summary>
        /// 比较两个物品
        /// 先判断itemcode，小的排前面,接着判断数量，大的放前面
        /// </summary>
        /// <param name="itemx"></param>
        /// <param name="itemy"></param>
        /// <returns></returns>
        public int Compare(ConstellationInfoEntity itemx, ConstellationInfoEntity itemy)
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
                    var result = doComapre(itemx.ItemCode, itemy.ItemCode);
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

