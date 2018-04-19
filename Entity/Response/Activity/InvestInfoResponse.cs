using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Response.Activity
{
    public class InvestInfoResponse : BaseResponse<InvestInfoEntity>
    {
        
    }

    public class InvestInfoEntity
    {
        /// <summary>
        /// 绑定点券数量
        /// </summary>
        public int BindPoint { get; set; }
        /// <summary>
        /// 存入非绑定点券数量
        /// </summary>
        public int Deposit { get; set; }
        /// <summary>
        /// 每档可返还多少绑定点券
        /// </summary>
        public List<int> Restitution { get; set; }
        /// <summary>
        /// 每档领取状态
        /// </summary>
        public string[] ReceiveStatus { get; set; }
        /// <summary>
        /// 存入月卡时是否是立即返还
        /// </summary>
        public bool Once { get; set; }
        /// <summary>
        /// 总计可领多少次
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 实际领取了多少次
        /// </summary>
        public int ReceiveCount { get; set; }

        /// <summary>
        /// 表示从充值那天至当天可以领取的次数
        /// </summary>
        public int DayCount { get; set; }
    }


    
}
