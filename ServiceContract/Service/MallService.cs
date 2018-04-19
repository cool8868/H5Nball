using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Core;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceContract.IService;

namespace Games.NBall.ServiceContract.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public class MallService : IMallService
    {
        /// <summary>
        /// 购买虚拟道具前提示用户
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="extraType"></param>
        /// <returns></returns>
        public MallCheckExtraResponse CheckExtraItem(Guid managerId, int extraType)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.CheckExtraItem(managerId, extraType));
        }

        /// <summary>
        /// 购买道具--扩展，主要用于虚拟道具
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="extraType">1,扩展背包;2,重置精英巡回赛;3,加速训练;4,增加训练位;5,增加替补席;6,增加体力;</param>
        /// <returns></returns>
        public MallBuyItemResponse BuyExtraItem(Guid managerId, int extraType)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.BuyExtraItem(managerId, extraType));
        }

        /// <summary>
        /// 获取显示列表
        /// </summary>
        /// <returns></returns>
        public MallShowlistResponse GetShowList(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.GetShowList(managerId));
        }

        /// <summary>
        /// 购买道具
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public MallBuyItemResponse BuyItem(Guid managerId, int mallCode, int count)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.BuyItem(managerId, mallCode, count));
        }

        /// <summary>
        /// 使用道具
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <param name="usedCount"></param>
        /// <returns></returns>
        public MallUseItemResponse UseItem(Guid managerId, Guid itemId, int usedCount)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.UseItem(managerId, itemId, usedCount));
        }

        /// <summary>
        /// 经理Buff列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public RootResponse<DTOManagerBuffView> GetManagerBuffs(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => Bll.Frame.BuffPoolCore.Instance().GetManagerShowBuffs(managerId));
        }
        /// <summary>
        /// 购买钻石下单
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="mallCode"></param>
        /// <returns></returns>
        public MallBuyPointResponse BuyPoint(Guid managerId, int mallCode)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.BuyPoint(managerId, mallCode));
        }
        /// <summary>
        /// 购买钻石发货
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="sourceType"></param>
        /// <param name="cash"></param>
        /// <param name="point"></param>
        /// <param name="bonus"></param>
        /// <param name="orderId"></param>
        /// <param name="eqid"></param>
        /// <returns></returns>
        public MallBuyPointResponse BuyPointShipments(string managerId, string orderId, string billingId, decimal cash, int mallCode)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.BuyPointShipments(managerId, orderId, billingId, cash, mallCode));
        }

        /// <summary>
        /// 获取用户购买钻石详情
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MallGetBuyPointResponse GetBuyPointInfo(Guid managerId)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.GetBuyPointInfo(managerId));
        }

        public MallTxBuyPointResponse TxBuyPointPara(Guid managerId, int mallCode)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.TxBuyPointPara(managerId, mallCode));
        }


        public MallTxBuyPointResultResponse TxBuyPointShipments(Guid managerId, string orderId, decimal cash,
            int mallCode)
        {
            return ResponseHelper.TryCatch(() => MallCore.Instance.TxBuyPointShipments(managerId, orderId, cash, mallCode));
        }

    }
}
