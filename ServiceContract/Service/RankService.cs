using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Rank;
using Games.NBall.Entity.Response.Rank;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class RankService:IRankService
    {
        /// <summary>
        /// 获取排行榜
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public RankResponse GetRanking(Guid managerId,int rankType, int pageIndex, int pageSize)
        {
            return ResponseHelper.TryCatch(()=>RankThread.Instance.GetRanking(managerId, rankType, pageIndex, pageSize));
        }
    }
}
