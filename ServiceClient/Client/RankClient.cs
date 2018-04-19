using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity.Response.Rank;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class RankClient
    {
        private static IRankService proxy = ServiceProxy<IRankService>.Create("NetTcp_IRankService");

        /// <summary>
        /// 获取排行榜
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="rankType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public RankResponse GetRanking(Guid managerId, int rankType, int pageIndex, int pageSize)
        {
            return proxy.GetRanking(managerId, rankType, pageIndex, pageSize);
        }
    }
}
