using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Item;

namespace Games.NBall.ServiceContract.IService
{
    [ServiceContract(Namespace = ServiceConfig.NameSpace)]
    public interface IMallService
    {
        /// <summary>
        /// 购买虚拟道具前提示用户
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="extraType"></param>
        /// <returns></returns>
        [OperationContract]
        MallCheckExtraResponse CheckExtraItem(Guid managerId, int extraType);

        /// <summary>
        /// 购买道具--扩展，主要用于虚拟道具
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="extraType">1,扩展背包;2,重置精英巡回赛;3,加速训练;4,增加训练位;5,增加替补席;6,增加体力;</param>
        /// <returns></returns>
        [OperationContract]
        MallBuyItemResponse BuyExtraItem(Guid managerId, int extraType);

        /// <summary>
        /// 获取显示列表
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        MallShowlistResponse GetShowList(Guid managerId);

        /// <summary>
        /// 购买道具
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [OperationContract]
        MallBuyItemResponse BuyItem(Guid managerId, int mallCode, int count);

        /// <summary>
        ///  使用道具
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="usedCount"></param>
        /// <returns></returns>
        [OperationContract]
        MallUseItemResponse UseItem(Guid managerId, Guid itemId, int usedCount);

        /// <summary>
        /// 获取经理Buff列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        RootResponse<DTOManagerBuffView> GetManagerBuffs(Guid managerId);


        /// <summary>
        /// 购买钻石下单
        /// </summary>
        [OperationContract]
        MallBuyPointResponse BuyPoint(Guid managerId, int mallCode);
        /// <summary>
        /// 购买钻石发货
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="orderId"></param>
        /// <param name="billingId"></param>
        /// <param name="cash"></param>
        /// <param name="mallCode"></param>
        /// <returns></returns>
        [OperationContract]
        MallBuyPointResponse BuyPointShipments(string managerId, string orderId, string billingId, decimal cash, int mallCode);

        /// <summary>
        /// 获取用户购买点卷信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        [OperationContract]
        MallGetBuyPointResponse GetBuyPointInfo(Guid managerId);

        /// <summary>
        /// 腾讯购买参数
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <returns></returns>
        [OperationContract]
        MallTxBuyPointResponse TxBuyPointPara(Guid managerId, int mallCode);

        [OperationContract]
        MallTxBuyPointResultResponse TxBuyPointShipments(Guid managerId, string orderId, decimal cash, int mallCode);
    }
}
