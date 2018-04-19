using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Arena;
using Games.NBall.Core.Item;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;

namespace Games.NBall.Core.Transfer
{
    /// <summary>
    /// 转会市场
    /// </summary>
    public class TransferCore
    { 
        /// <summary>
        /// 跨服字典
        /// </summary>
        private Dictionary<EnumArenaDomainType, TransferThread> _threadDic;

        public TransferCore(int p)
        {
            _threadDic = new Dictionary<EnumArenaDomainType, TransferThread>();
            var domainDic = CacheFactory.ArenaCache.GetDomainDic();
            foreach (var item in domainDic)
            {
                if (item.Value.Count == 0)
                    continue;
                _threadDic.Add(item.Key, new TransferThread((int)item.Key));
            }

        }

        #region Instance


        public static TransferCore Instance
       {
           get { return SingletonFactory<TransferCore>.SInstance; }
       }

        #endregion

        
        /// <summary>
        /// 开始拍卖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <param name="itemId"></param>
        /// <param name="price"></param>
        /// <param name="transferDuration"></param>
        public AuctionItemResponse AuctionItem(Guid managerId, string zoneName, Guid itemId, int price,
            int transferDuration)
        {
            try
            {
               var transferThread = GetThread(zoneName);
               if (transferThread == null)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NbParameterError);
               return transferThread.AuctionItem(managerId, zoneName, itemId, price,
                    transferDuration);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("开始拍卖", ex);
                return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 获取我的拍卖列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public AuctionItemResponse GetMyAuctionList(Guid managerId,string zoneName)
        {
            try
            {
                var transferThread = GetThread(zoneName);
                if (transferThread == null)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NbParameterError);
                return transferThread.GetMyAuctionList(managerId, zoneName);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取我的拍卖列表", ex);
                return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 获取拍卖列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="rankRule"></param>
        /// <param name="itemName"></param>
        /// <param name="zoneName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public AuctionItemResponse GetTransferList(Guid managerId,int rankRule, string itemName, string zoneName, int pageSize, int pageIndex)
        {
            try
            {
                var transferThread = GetThread(zoneName);
                if (transferThread == null)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NbParameterError);
                return transferThread.GetTransferList(managerId, zoneName, rankRule, itemName, pageSize, pageIndex);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取拍卖列表", ex);
                return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 获取拍卖物品详情
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="transferId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public TransferMainResponse GetTransferInfo(Guid managerId,Guid transferId, string zoneName)
        {
            try
            {
                var transferThread = GetThread(zoneName);
                if (transferThread == null)
                    return ResponseHelper.Create<TransferMainResponse>(MessageCode.NbParameterError);
                return transferThread.GetTransferInfo(managerId,zoneName,transferId);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取拍卖物品详情", ex);
                return ResponseHelper.Create<TransferMainResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 竞拍
        /// </summary>
        /// <param name="transferId"></param>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public AuctionResponse Auction(Guid transferId, Guid managerId, string zoneName)
        {
            try
            {
                var transferThread = GetThread(zoneName);
                if (transferThread == null)
                    return ResponseHelper.Create<AuctionResponse>(MessageCode.NbParameterError);
                return transferThread.Auction(transferId, managerId, zoneName);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞拍", ex);
                return ResponseHelper.Create<AuctionResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 下架
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="transferId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public MessageCodeResponse SoldOut(Guid managerId, Guid transferId, string zoneName)
        {
            try
            {
                var transferThread = GetThread(zoneName);
                if (transferThread == null)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
                return transferThread.SoldOut(managerId, transferId, zoneName);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞拍", ex);
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
            }
        }

        public MessageCode RefreshStatus()
        {
            foreach (var entity in _threadDic.Values)
            {
                entity.RefreshStatus();
            }
            return MessageCode.Success;
        }

        public TransferThread GetThread(string zoneName)
        {
            var domainId = CacheFactory.ArenaCache.GetDomainId(zoneName);
            if (domainId == null || !_threadDic.ContainsKey((EnumArenaDomainType)domainId))
                return null;
            return _threadDic[(EnumArenaDomainType) domainId];
        }

    }
}
