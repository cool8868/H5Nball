using System;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class DailycupInfoEntity
	{
	}
	
	
    public partial class DailycupInfoResponse
    {

    }

    /// <summary>
    /// 杯赛每轮的时间点
    /// </summary>
    [DataContract]
    public class DailycupRoundTimeEntity
    {
        /// <summary>
        /// 比赛开始时间，秒数；如10点为10*60*60=36000
        /// </summary>
        [DataMember]
        public int MatchStartTime
        { get; set; }

        /// <summary>
        /// 比赛结束时间，秒数；如10点为10*60*60=36000
        /// </summary>
        [DataMember]
        public int MatchEndTime
        { get; set; }

        /// <summary>
        /// 竞猜关闭时间，秒数；如10点为10*60*60=36000
        /// </summary>
        [DataMember]
        public int GambelCloseTime
        { get; set; }

        /// <summary>
        /// 竞猜发奖时间，秒数；如10点为10*60*60=36000
        /// </summary>
        [DataMember]
        public int GambelOpenTime
        { get; set; }
    }
}

