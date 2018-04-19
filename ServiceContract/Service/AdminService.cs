using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class AdminService : IAdminService
    {
        public MessageCode AddCoin(Guid managerId, int coin)
        {
            return AdminCore.Instance.AddCoin(managerId, coin);
        }
        public MessageCode AddCoin2(Guid managerId, int coin, int sourceType)
        {
            return AdminCore.Instance.AddCoin(managerId, coin, sourceType);
        }
        public MessageCode AddSophisticate(Guid managerId, int coin)
        {
            return AdminCore.Instance.AddSophisticate(managerId, coin);
        }

        public MessageCode AddReiki(Guid managerId, int coin)
        {
            return AdminCore.Instance.AddReiki(managerId, coin);
        }

        //public MessageCode Charge(string account, int sourceType, int cash, int point, int bonus, string orderId)
        //{
        //    return PayCore.Instance.Charge(account, sourceType, cash, point, bonus, orderId);
        //} 
        public MessageCode Charge(string account, int sourceType, int cash, int point, int bonus, string orderId,string eqid)
        {
            return PayCore.Instance.Charge(account, sourceType, cash, point, bonus, orderId);
        }
     
        public MessageCode AddCSDK(String sign, int orderId, string gameOrderId, int price,
                   string channelAlias, string playerId, string serverId, int goodsId, string payResult, string state, DateTime sourceTime)
        {
            return PayCore.Instance.AddCSDK(sign, orderId, gameOrderId, price,
                     channelAlias, playerId, serverId, goodsId, payResult, state, sourceTime);
        }



        public MessageCode CheckActive(string account)
        {
            return AdminCore.Instance.CheckActive(account);
        }

        public MessageCode AddManagerData(Guid managerId, int prizeExp, int prizeCoin, int prizeSophisticate)
        {
            return AdminCore.Instance.AddManagerData(managerId, prizeExp, prizeCoin, prizeSophisticate);
        }
    }
}
