using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Games.NBall.Entity
{    

	public partial class NbManagerEntity
	{
        /// <summary>
        /// 升级所需经验,-1表示不更新
        /// </summary>
        [DataMember]
        public int LevelupExp { get; set; }

        /// <summary>
        /// 经理综合实力
        /// </summary>
        [DataMember]
        public int Kpi { get; set; }

        /// <summary>
        /// 是否升级
        /// </summary>
        [DataMember]
        public bool IsLevelup { get; set; }
        /// <summary>
        /// 巡回赛所属联赛
        /// </summary>
        [DataMember]
        public int TourLeague { get; set; }


        public string FunctionList { get; set; }

        public List<int> OpenFuncs { get; set; }

        public void AddOpenFunc(int funcId)
        {
            if (OpenFuncs == null)
                OpenFuncs = new List<int>();
            if (!OpenFuncs.Contains(funcId))
                OpenFuncs.Add(funcId);
        }

        public bool HasOpenTask { get; set; }

        public int AddCoin { get; set; }

        public int AddExp { get; set; }

        public int CoinSourceType { get; set; }

        public string CoinOrderId { get; set; }

        /// <summary>
        /// 精力值
        /// </summary>
        public int Vigor { get; set; }

        public bool OpenLevelGift { get; set; }
        /// <summary>
        /// 是否是玩吧达人
        /// </summary>
        public bool IsYellowVip { get; set; }
	}
	
	
    public partial class NbManagerResponse
    {

    }
}

