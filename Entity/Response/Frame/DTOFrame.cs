using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.Item;
using ProtoBuf;

namespace Games.NBall.Entity.Response
{
    public interface IFlatSplit
    {
        string[] GetFlatSplit();
        bool FromFlatSplit(string[] units);
        bool ToFlatSplit(string[] units);
    }

    [DataContract]
    [Serializable]
    public class DTOPageItem
    {
        public DTOPageItem()
        { }
        public DTOPageItem(int pageNo)
        {
            this.PageNo = pageNo;
        }
        /// <summary>
        /// 总行数,=0时不处理
        /// </summary>
        [DataMember]
        public int RowCount;
        /// <summary>
        /// 总页数,=0时不处理
        /// </summary>
        [DataMember]
        public int PageCount;
        /// <summary>
        /// 当前页号,=0时不处理
        /// </summary>
        [DataMember]
        public int PageNo;

        public int AsPageCount(int pageSize)
        {
            return AsPageCount(this.RowCount, pageSize);
        }
        public static int AsPageCount(int rowCount, int pageSize)
        {
            if (pageSize <= 0)
                return 0;
            return (rowCount + pageSize - 1) / pageSize;
        }
    }
    [DataContract]
    [Serializable]
    public class DTOSiteItem
    {
        [DataMember]
        public string SiteId
        {
            get;
            set;
        }
        [DataMember]
        public string SiteName
        {
            get;
            set;
        }
    }

    [DataContract]
    [Serializable]
    public class DTOWorthItem
    {
        /// <summary>
        /// 经理属性类型：1-点券，2-金币,91-经理等级,92-VIP等级
        /// </summary>
        [DataMember]
        public int WorthType
        {
            get;
            set;
        }
        /// <summary>
        /// 经理属性值
        /// </summary>
        [DataMember]
        public long Value
        {
            get;
            set;
        }

        public int PlusValue;
        public int CostValue;
        public long AsValue
        {
            get { return Value + PlusValue - CostValue; }
        }
        public bool LackFlag
        {
            get { return Value < CostValue; }
        }
        public void Plus()
        {
            Value += PlusValue;
        }
        public void Cost()
        {
            Value -= CostValue;
        }
    }

    public class WorthSource
    {
        public NbManagerEntity Manager
        {
            get;
            set;
        }
        public PayUserEntity Account
        {
            get;
            set;
        }
    }

    public class GenItemProto
    {
        public int GenType
        {
            get;
            set;
        }
        public int ItemCode
        {
            get;
            set;
        }
        public string ItemMap
        {
            get;
            set;
        }
        public int ItemType
        {
            get;
            set;
        }
        public int ItemRank
        {
            get;
            set;
        }
        public int Strength
        {
            get;
            set;
        }
        public int BindFlag
        {
            get;
            set;
        }
        public string Args
        {
            get;
            set;
        }
        public int Qty
        {
            get;
            set;
        }

        public List<int> ItemList
        {
            get;
            set;
        }

        public bool WorthFlag
        {
            get
            {
                return GenType == (int)EnumItemGenType.Coin || GenType == (int)EnumItemGenType.Point;
            }
        }

    }

    public class GenItemProdu
    {
        public GenItemProdu()
        {
            this.PackItems = new List<LotteryEntity>();
        }
        public int Coin
        {
            get;
            set;
        }
        public int Point
        {
            get;
            set;
        }
        public int Prestige
        {
            get;
            set;
        }
        public List<LotteryEntity> PackItems
        {
            get;
            private set;
        }
        public string ItemMap
        {
            get
            {
                var sb = new StringBuilder();
                if (Point > 0)
                    sb.AppendFormat("P:{0},", Point);
                if (Coin > 0)
                    sb.AppendFormat("C:{0},", Coin);
                if (Prestige > 0)
                    sb.AppendFormat("R:{0},", Prestige);
                if (null != PackItems)
                {
                    foreach (var item in PackItems)
                    {
                        sb.AppendFormat("I:{0}_{1},", item.PrizeItemCode, item.Count);
                    }
                }
                var str = sb.ToString();
                sb.Clear();
                return str;
            }
        }
        public void Clear()
        {
            this.Point = 0;
            this.Coin = 0;
            this.Prestige = 0;
            if (null != this.PackItems)
                this.PackItems.Clear();
        }
    }
}