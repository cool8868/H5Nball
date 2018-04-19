using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.Dpm.Core.Activity;
using Games.NBall.Bll;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Friend;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Match;
using Games.NBall.Core.SkillCard;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Match;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;
using Games.NBall.Entity.Response.Information;
using Games.NBall.Entity.Response.SkillCard;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core
{
    public class TransferThread
    {
      
        /// <summary>
        /// 当前域
        /// </summary>
        private int _domainId;

        /// <summary>
        /// 十分钟内拍卖的字典
        /// </summary>
        public List<TransferMainEntity> _tenTransferList;

        /// <summary>
        /// 拍卖字典
        /// </summary>
        public ConcurrentDictionary<Guid, TransferMainEntity> _transferDic;
        /// <summary>
        /// 时间排序
        /// </summary>
        public List<TransferMainEntity> _transferTimeList;
        /// <summary>
        /// 价格排序
        /// </summary>
        public List<TransferMainEntity> _transferPriceList;
        /// <summary>
        /// 价格排序
        /// </summary>
        public List<TransferMainEntity> _transferPriceDescList;

        public TransferThread(int domainId)
        {
            _domainId = domainId;
            var transferList = TransferMainMgr.GetTransferList(_domainId);
            _transferDic = new ConcurrentDictionary<Guid, TransferMainEntity>();
            _tenTransferList = new List<TransferMainEntity>();
            foreach (var item in transferList)
            {
                item.TransferDurationTick = ShareUtil.GetTimeTick(item.TransferDuration);
                item.TransferDurationTick = ShareUtil.GetTimeTick(item.TransferDuration);
                if (DateTime.Now < item.TransferStartTime.AddMinutes(10))
                {
                    var modId =ShareUtil.GetTableMod(item.TransferId);
                    item.ModId = modId;
                    _tenTransferList.Add(item);
                }
                else
                    _transferDic.TryAdd(item.TransferId, item);
            }
            Sort();
        }


        /// <summary>
        /// 开始拍卖
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <param name="itemId"></param>
        /// <param name="price"></param>
        /// <param name="transferDuration"></param>
        public AuctionItemResponse AuctionItem(Guid managerId, string zoneName, Guid itemId, int price,int transferDuration)
        {
            var response = new AuctionItemResponse();
            response.Data = new AuctionItemEntity();
            try
            {
                DateTime date = DateTime.Now;
                var manager = NbManagerMgr.GetById(managerId, zoneName);
                if (manager == null)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.AdMissManager);
                if (!IsOpen(managerId, zoneName, manager))
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.TransferNotOpen);
                //已经挂牌了多少个
                var number = TransferMainMgr.GetTransferNumber(managerId);
                //最多可以挂牌多少个
                var gambleCountMax = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.TransferNumber);
                if (gambleCountMax <= number)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.TransferNumberMax);
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.Transfer,zoneName);
                if (package == null)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NbNoPackage);

                if (price < 2)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.StartPriceSmall);
                var item = package.GetItem(itemId);
                if (item == null)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.ItemNotExists);
                if (!item.IsDeal)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NotDeal);
                var strength = item.GetStrength();
                if(strength>1)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NotDeal);
                var iteminfo = CacheFactory.ItemsdicCache.GetItem(item.ItemCode);
                if(iteminfo ==null)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.ItemNotExists);
                //默认86400秒  24小时
                TransferMainEntity entity = new TransferMainEntity(ShareUtil.GenerateComb(), _domainId, item.ItemCode,iteminfo.ItemName,
                    new byte[0], manager.Name, managerId, zoneName, price, "",
                    "", 0, Guid.Empty, date, date.AddDays(1), 0, 0, 0, date, date);

                var messageCode = package.Delete(itemId);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<AuctionItemResponse>(messageCode);
                if (!package.Save())
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NbUpdateFail);
                if (!TransferMainMgr.Insert(entity))
                {
                    messageCode = package.AddItem(item.ItemCode);
                    if (messageCode != MessageCode.Success)
                        SystemlogMgr.Error("拍卖返还物品失败",
                            "拍卖返还物品失败，managerId:" + managerId + ",zoneName:" + zoneName + "物品ID：" + item.ItemCode);
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.NbUpdateFail);
                }
                package.Shadow.Save();
                entity.TransferDurationTick = ShareUtil.GetTimeTick(entity.TransferDuration);
                entity.TransferDurationTick = ShareUtil.GetTimeTick(entity.TransferDuration);
                var modId = ShareUtil.GetTableMod(entity.TransferId);
                entity.ModId = modId;
                _tenTransferList.Add(entity);
                Sort();
                response.Data.MyTransferList = GetMyTransfer(managerId);
                response.Data.MaxTransferNumber = gambleCountMax;
                response.Data.HaveTransferNumber = number + 1;
                var goldBarEntity = ScoutingGoldbarMgr.GetById(managerId, zoneName);
                if (goldBarEntity != null)
                    response.Data.MyGoldBar = goldBarEntity.GoldBarNumber;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("开始拍卖", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 获取我的拍卖列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public AuctionItemResponse GetMyAuctionList(Guid managerId,string zoneName)
        {
            var response = new AuctionItemResponse();
            response.Data = new AuctionItemEntity();
            try
            {
                var manager = NbManagerMgr.GetById(managerId, zoneName);
                if (manager == null)
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.AdMissManager);
                if (!IsOpen(managerId, zoneName, manager))
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.TransferNotOpen);
                //已经挂牌了多少个
                var number = TransferMainMgr.GetTransferNumber(managerId);
                //最多可以挂牌多少个
                var gambleCountMax = CacheFactory.VipdicCache.GetEffectValue(manager.VipLevel, EnumVipEffect.TransferNumber);
                response.Data.MyTransferList = GetMyTransfer(managerId);
                response.Data.MaxTransferNumber = gambleCountMax;
                response.Data.HaveTransferNumber = number;
                var goldBarEntity = ScoutingGoldbarMgr.GetById(managerId, zoneName);
                if (goldBarEntity != null)
                    response.Data.MyGoldBar = goldBarEntity.GoldBarNumber;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取我的拍卖列表", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 获取拍卖列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <param name="rankRule"></param>
        /// <param name="itemName"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public AuctionItemResponse GetTransferList(Guid managerId,string zoneName,int rankRule,string itemName,int pageSize,int pageIndex)
        {
            AuctionItemResponse response = new AuctionItemResponse();
            response.Data = new AuctionItemEntity();
            try
            {
                if (pageSize <= 0)
                    return response;
                if (!IsOpen(managerId, zoneName))
                    return ResponseHelper.Create<AuctionItemResponse>(MessageCode.TransferNotOpen);
                var list = new List<TransferMainEntity>();
                var modId = ShareUtil.GetTableMod(managerId);
                bool isOrderBy = false;
                if (_tenTransferList != null && _tenTransferList.Count > 0)
                {
                    var modList = _tenTransferList.FindAll(r => r.ModId == modId);
                    if (modList.Count > 0)
                    {
                        isOrderBy = true;
                        list.AddRange(modList);
                    }
                }
                switch (rankRule)
                {
                    case 2: //价格正序
                        list.AddRange(_transferPriceList);
                        if (isOrderBy)
                            list = list.OrderBy(r => r.Price).ToList();
                        break;
                    case 3: //价格到序
                        list.AddRange(_transferPriceDescList);
                        if (isOrderBy)
                            list = list.OrderByDescending(r => r.Price).ToList();
                        break;
                    default: //默认排序 时间倒序
                        list.AddRange(_transferTimeList);
                        break;
                }
                if (itemName.Trim().Length > 0)
                {
                    if (list.Count > 0)
                        list = list.Where(r => r.ItemName.IndexOf(itemName)>-1).ToList();
                }
                response.Data.TotalPageNumber = list.Count/pageSize;
                if (list.Count%pageSize != 0)
                    response.Data.TotalPageNumber = response.Data.TotalPageNumber + 1;
                var resultList = new List<TransferMainEntity>();
                if (list.Count > 0)
                {
                    int index = pageSize*(pageIndex - 1);
                    if (list.Count > index)
                        resultList = list.Skip(index).Take(pageSize).ToList();
                }
                response.Data.MyTransferList = resultList;
                var goldBarEntity = ScoutingGoldbarMgr.GetById(managerId, zoneName);
                if (goldBarEntity != null)
                    response.Data.MyGoldBar = goldBarEntity.GoldBarNumber;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取拍卖列表", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 获取拍卖物品详情
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <param name="transferId"></param>
        /// <returns></returns>
        public TransferMainResponse GetTransferInfo(Guid managerId, string zoneName, Guid transferId)
        {
            TransferMainResponse response = new TransferMainResponse();
            response.Data = new TransferMainEntity();
            try
            {
                if (!IsOpen(managerId, zoneName))
                    return ResponseHelper.Create<TransferMainResponse>(MessageCode.TransferNotOpen);
                var entity = GetInfo(transferId);
                if (entity == null)
                    return ResponseHelper.Create<TransferMainResponse>(MessageCode.ItemSoldOut);
                response.Data = entity;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取拍卖详情", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 竞拍
        /// </summary>
        /// <param name="transferId"></param>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <returns></returns>
        public AuctionResponse Auction(Guid transferId,Guid managerId, string zoneName)
        {
            AuctionResponse response = new AuctionResponse();
            try
            {
                if (!IsOpen(managerId, zoneName))
                    return ResponseHelper.Create<AuctionResponse>(MessageCode.TransferNotOpen);
                var entity = GetInfo(transferId);
                if (entity == null)
                    return ResponseHelper.Create<AuctionResponse>(MessageCode.ItemSoldOut);
                var goldBarEntity = ScoutingGoldbarMgr.GetById(managerId, zoneName);
                if (goldBarEntity == null || goldBarEntity.GoldBarNumber < entity.Price)
                    return ResponseHelper.Create<AuctionResponse>(MessageCode.ScoutingGoldBarNot);
                if (entity.Status == 1)
                    return ResponseHelper.Create<AuctionResponse>(MessageCode.ItemSoldOut);
                if (entity.Status == 2)
                    return ResponseHelper.Create<AuctionResponse>(MessageCode.ItemHaveSellOut);
                if (entity.SellId == managerId)
                    return ResponseHelper.Create<AuctionResponse>(MessageCode.NotBuyOneself);
                goldBarEntity.GoldBarNumber = goldBarEntity.GoldBarNumber - entity.Price;
                //手续费 5%
                int poundage = entity.Price * 5 / 100;
                if (poundage == 0)
                    poundage = 1;
                else if (poundage > 20)
                    poundage = 20;

                GoldbarRecordEntity auctionRecord = new GoldbarRecordEntity();
                auctionRecord.IsAdd = false;
                auctionRecord.ManagerId = managerId;
                auctionRecord.Number = entity.Price;
                auctionRecord.OperationType = (int)EnumTransactionType.Transfer;
                auctionRecord.RowTime = DateTime.Now;

                entity.DealEndId = managerId;
                entity.DealEndPrice = entity.Price;
                entity.DealEndZoneName = zoneName;
                entity.Status = 2;
                entity.UpdateTime = DateTime.Now;
                entity.Poundage = poundage;
                if (!TransferMainMgr.Update(entity))
                {
                    entity.Status = 0;
                    return ResponseHelper.Create<AuctionResponse>(MessageCode.NbUpdateFail);
                }
                if (!ScoutingGoldbarMgr.Update(goldBarEntity, null, zoneName))
                    return ResponseHelper.Create<AuctionResponse>(MessageCode.NbUpdateFail);

                Remove(transferId);
                //出售人邮件
                var sellMail = new MailBuilder(entity.SellId, entity.ItemName, EnumCurrencyType.GoldBar,
                    entity.Price - poundage);
                //购买人邮件
                var buyMail = new MailBuilder(managerId, entity.ItemCode, entity.ItemName, entity.Price);
                if (!sellMail.Save(entity.SellZoneName))
                {
                    SystemlogMgr.Error("邮件发送失败",
                        "邮件发送失败，ManagerId:" + entity.SellId + ",ZoneName:" + entity.SellZoneName + ",GoldBarNumber:" +
                        (entity.Price - poundage));
                }
                if (!buyMail.Save(zoneName))
                {
                    SystemlogMgr.Error("邮件发送失败",
                        "邮件发送失败，ManagerId:" + managerId + ",ZoneName:" + zoneName + ",GoldBarNumber:" +
                        (entity.Price - poundage));
                }
                GoldbarRecordMgr.Insert(auctionRecord, null, zoneName);
                response.Data = new AuctionEntity();
                response.Data.GoldBar = goldBarEntity.GoldBarNumber;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("竞拍", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
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
            MessageCodeResponse response = new MessageCodeResponse();
            try
            {
                if (!IsOpen(managerId, zoneName))
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.TransferNotOpen);
                var info = GetInfo(transferId);
                if (info == null || info.Status == 1)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.ItemSoldOut);
                if (info.Status == 2)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.ItemHaveSellOut);
                if (info.SellId != managerId)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NotSoldOutAuthority);
                info.Status = 1;
                info.UpdateTime = DateTime.Now;
                if (!TransferMainMgr.Delete(transferId))
                {
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbUpdateFail);
                }
                Remove(transferId);
                var mail = new MailBuilder(managerId, info.ItemCode, info.ItemName, EnumMailType.TransferSoldOut);
                if (!mail.Save(info.SellZoneName))
                {
                    SystemlogMgr.Error("邮件发送失败",
                        "邮件发送失败，ManagerId:" + info.SellId + ",ZoneName:" + info.SellZoneName + ",ItemCode:" +
                        info.ItemCode);
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("下架", ex);
                response.Code = (int)MessageCode.NbParameterError;
            }
            return response;
        }

        public MessageCode RefreshStatus()
        {
            DateTime date = DateTime.Now;

            foreach (var item in _transferDic.Values)
            {
                if (date >= item.TransferDuration)
                {
                    item.Status = 3;//流拍。 从数据库删除
                    if (TransferMainMgr.Delete(item.TransferId))
                    {
                        var mail = new MailBuilder(item.SellId, item.ItemCode, item.ItemName, EnumMailType.TransferRunOff);
                        if (!mail.Save(item.SellZoneName))
                        {
                            SystemlogMgr.Error("邮件发送失败",
                                "邮件发送失败，ManagerId:" + item.SellId + ",ZoneName:" + item.SellZoneName + ",ItemCode:" +
                                item.ItemCode);
                        }
                        Remove(item.TransferId);
                    }
                }
            }
            foreach (var entity in _tenTransferList)
            {
                //过了10分钟的放到另外一个字典
                if (entity.TransferStartTime.AddMinutes(10) < date)
                    _transferDic.TryAdd(entity.TransferId, entity);
            }
            _tenTransferList = _tenTransferList.FindAll(r => r.TransferStartTime.AddMinutes(10) > date);
            Sort();
            return MessageCode.Success;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        public MessageCode Sort()
        {
            var list = _transferDic.Values;
            _transferPriceList = new List<TransferMainEntity>();
            _transferPriceDescList = new List<TransferMainEntity>();
            _transferTimeList = new List<TransferMainEntity>();
            _transferPriceList = list.OrderBy(r => r.Price).ToList();
            _transferPriceDescList = list.OrderByDescending(r => r.Price).ToList();
            _transferTimeList = list.OrderByDescending(r => r.TransferStartTime).ToList();
            return MessageCode.Success;
        }

        public TransferMainEntity GetInfo(Guid transferId)
        {
            TransferMainEntity entity = null;

            if (!_transferDic.ContainsKey(transferId))
                entity = _tenTransferList.Find(r => r.TransferId == transferId);
            else
                entity = _transferDic[transferId];
            return entity;
        }

        public bool Remove(Guid transferId)
        {
            TransferMainEntity outEntity = null;
            if (_transferDic.ContainsKey(transferId))
            {
                _transferDic.TryRemove(transferId, out outEntity);
                Sort();
                return true;
            }
            outEntity = _tenTransferList.Find(r => r.TransferId == transferId);
            if (outEntity == null)
                return false;
            _tenTransferList.Remove(outEntity);
            Sort();
            return true;
        }

        /// <summary>
        /// 获取我的拍卖列表
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public List<TransferMainEntity> GetMyTransfer(Guid managerId)
        {
            var list = new List<TransferMainEntity>();
            if (_tenTransferList != null && _tenTransferList.Count > 0)
                list.AddRange(_tenTransferList.FindAll(r => r.SellId == managerId));
            if (_transferDic != null && _transferDic.Count > 0)
                list.AddRange(_transferTimeList.FindAll(r => r.SellId == managerId));
            return list;
        }

        /// <summary>
        /// 是否开启了拍卖行
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="zoneName"></param>
        /// <param name="manager"></param>
        /// <returns></returns>
        public bool IsOpen(Guid managerId, string zoneName, NbManagerEntity manager = null)
        {
            if (manager == null)
                manager = NbManagerMgr.GetById(managerId, zoneName);
            if (manager == null)
                return false;
            if (manager.Level < 30 && manager.VipLevel < 3)
                return false;
            return true;
        }
    }
}
