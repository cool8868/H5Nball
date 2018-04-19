using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceContract.IService;
using Games.NBall.ServiceEngine;

namespace Games.NBall.ServiceContract.Client
{
    public class AdminClient
    {
        private static IAdminService proxy = ServiceProxy<IAdminService>.Create("NetTcp_IAdminService");


        public MessageCode AddCoin(Guid managerId, int coin)
        {
            return proxy.AddCoin(managerId, coin);
        }
        public MessageCode AddCoin2(Guid managerId, int coin, int sourceType)
        {
            return proxy.AddCoin2(managerId, coin, sourceType);
        }
        public MessageCode AddSophisticate(Guid managerId, int coin)
        {
            return proxy.AddSophisticate(managerId, coin);
        }

        public MessageCode AddReiki(Guid managerId, int coin)
        {
            return proxy.AddReiki(managerId, coin);
        }

        //public MessageCode Charge(string account, int sourceType, int cash, int point, int bonus, string orderId)
        //{
        //    return proxy.Charge(account, sourceType, cash, point, bonus, orderId);
        //}

        public MessageCode Charge(string account, int sourceType, int cash, int point, int bonus, string orderId,string eqid="")
        {
            return proxy.Charge(account, sourceType, cash, point, bonus, orderId,eqid);
        }

       

        public MessageCode AddCSDK(String sign, int orderId, string gameOrderId, int price,
                  string channelAlias, string playerId, string serverId, int goodsId, string payResult, string state, DateTime sourceTime)
        {
            return proxy.AddCSDK(sign, orderId, gameOrderId, price,
                     channelAlias, playerId, serverId, goodsId, payResult, state, sourceTime);
        }


        public MessageCode CheckActive(string account)
        {
            return proxy.CheckActive(account);
        }

        public MessageCode AddManagerData(Guid managerId, int prizeExp, int prizeCoin, int prizeSophisticate)
        {
            return proxy.AddManagerData(managerId, prizeExp, prizeCoin, prizeSophisticate);
        }
    }
}
