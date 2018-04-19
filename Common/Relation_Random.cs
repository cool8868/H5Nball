using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NBall.Common
{
    /// <summary>
    /// 单纯按时间判断概率
    /// </summary>
    public class RandomSingleInTime
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 该抽奖概率起始范围
        /// </summary>
        public int Begin
        {
            get;
            set;
        }

        /// <summary>
        /// 该抽奖概率结束范围
        /// </summary>
        public int End
        {
            get;
            set;
        }

        /// <summary>
        /// 随机一个值
        /// </summary>
        public int Next
        {
            get
            {
                return RandomHelper.GetInt32(Begin, End);
            }
        }
    }

    /// <summary>
    /// 抽奖概率，带时间
    /// </summary>
    public class RandomRelationInTime
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public RandomRelation Relation { get; set; }
    }

    public class RandomRelation
    {
        public RandomRelation(int totalSeed, List<RandomRate> rates)
        {
            this.TotalSeed = totalSeed;
            _rates = rates;
        }

        private List<RandomRate> _rates;
        /// <summary>
        /// 总种子数，用做概率分母
        /// </summary>
        private int TotalSeed
        {
            get;
            set;
        }

        /// <summary>
        /// 根据概率随机获得关联id
        /// </summary>
        public int RandomLinkId
        {
            get
            {
                int linkId = 0;
                if ((this._rates != null) && (this._rates.Count > 0))
                {
                    int num = RandomHelper.GetInt32WithoutMax(1, this.TotalSeed);
                    RandomRate item = this._rates.Find(n => (num >= n.Begin) && (num <= n.End));
                    linkId = item.LinkId;
                }
                return linkId;
            }
        }

        /// <summary>
        /// 根据概率随机活动物品串
        /// </summary>
        public string RandomLinkString
        {
            get
            {
                string linkId = "";
                if (this._rates != null && this._rates.Count > 0)
                {
                    int num = RandomHelper.GetInt32WithoutMax(1, this.TotalSeed);
                    RandomRate item = this._rates.Find(n => num >= n.Begin && num <= n.End);
                    linkId = item.LinkString;
                }
                return linkId;
            }
        }

        public RandomRelation Clone()
        {
            List<RandomRate> rateList = new List<RandomRate>();
            foreach (var randomRate in _rates)
            {
                rateList.Add(randomRate.Clone());
            }
            return new RandomRelation(this.TotalSeed, rateList);
        }
    }

    public class RandomRate
    {
        public RandomRate(int linkId, int begin, int end)
        {
            this.LinkId = linkId;
            this.Begin = begin;
            this.End = end;
        }

        public RandomRate(string linkString, int begin, int end)
        {
            this.LinkString = linkString;
            this.Begin = begin;
            this.End = end;
        }

        /// <summary>
        /// 该抽奖概率起始范围
        /// </summary>
        public int Begin
        {
            get;
            set;
        }


        /// <summary>
        /// 该抽奖概率结束范围
        /// </summary>
        public int End
        {
            get;
            set;
        }

        public int LinkId { get; set; }

        public string LinkString { get; set; }

        public RandomRate Clone()
        {
            return new RandomRate(this.LinkId, this.Begin, this.End);
        }

        public RandomRate CloneString()
        {
            return new RandomRate(this.LinkString, this.Begin, this.End);
        }
    }
}
