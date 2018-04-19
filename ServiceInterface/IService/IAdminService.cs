using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IAdminService
    {

        [OperationContract]
        MessageCode AddCoin(Guid managerId, int coin);
        [OperationContract]
        MessageCode AddCoin2(Guid managerId, int coin, int sourceType);
        [OperationContract]
        MessageCode AddSophisticate(Guid managerId, int coin);
        [OperationContract]
        MessageCode AddReiki(Guid managerId, int coin);
       // [OperationContract]
        //MessageCode Charge(string account, int sourceType, int cash, int point, int bonus, string orderId);
        [OperationContract]
        MessageCode Charge(string account, int sourceType, int cash, int point, int bonus, string orderId,string eqid);

      
        [OperationContract]
        MessageCode AddCSDK(String sign, int orderId, string gameOrderId, int price,
            string channelAlias, string playerId, string serverId, int goodsId, string payResult, string state,
            DateTime sourceTime);
       

        [OperationContract]
        MessageCode CheckActive(string account);

        [OperationContract]
        MessageCode AddManagerData(Guid managerId, int prizeExp, int prizeCoin, int prizeSophisticate);
    }
}
