using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Core.Item;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Activity
{
    public class ShareCore
    {

        public ShareCore(int p)
        {
        }

        public static ShareCore Instance
        {
            get { return SingletonFactory<ShareCore>.SInstance; }
        }

        /// <summary>
        /// 获取分享信息
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="shareType">分享类型   4=关注</param>
        /// <returns></returns>
        public ShareGetResponse GetShareInfo(Guid managerId,int shareType)
        {
            var response = new ShareGetResponse();
            response.Data = new ShareGet();
            try
            {
                var manager = NbManagerMgr.GetById(managerId);
                if (manager == null)
                    return ResponseHelper.Create<ShareGetResponse>(MessageCode.MissManager);
                var config = CacheFactory.ManagerDataCache.GetShare(shareType);
                if (config == null || config.Count == 0)
                    return ResponseHelper.Create<ShareGetResponse>(MessageCode.ActivityNoConfigPrize);
                var record = ShareManagerMgr.GetByManagerId(managerId, shareType);
                if (record != null)
                {
                    DateTime date = DateTime.Now;
                    if (record.UpdateTime.Date != date.Date)
                    {
                        var code = RefreshRecord(record, config[0]);
                        if (code != MessageCode.Success)
                            return ResponseHelper.Create<ShareGetResponse>(code);
                    }
                    if (shareType == 1)//第一种特殊处理
                    {
                        if (record.ShareNumber > 0)
                        {
                            int seconds = 3600 - (date - record.UpdateTime).Seconds;
                            response.Data.NextShareEnd = seconds < 0 ? 0 : seconds;
                        }
                    }
                    response.Data.IsFirstShare = false;
                    response.Data.ShareNumber = record.ShareNumber;
                    response.Data.IsHaveShare = config[0].MaxShareNumber > record.ShareNumber;
                }
                else
                {
                    response.Data.IsHaveShare = true;
                    response.Data.IsFirstShare = true;
                    response.Data.ShareNumber = 0;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("获取分享信息", ex);
                response.Code = (int) MessageCode.NbParameterError;
            }
            return response;
        }

        /// <summary>
        /// 刷新每日分享次数
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        private MessageCode RefreshRecord(ShareManagerEntity record,ConfigShareEntity config)
        {
            if (!config.IsRepetition)
                return MessageCode.Success;
            record.ShareNumber = 0;
            record.UpdateTime = DateTime.Now;
            if (ShareManagerMgr.Update(record))
                return MessageCode.Success;
            return MessageCode.NbUpdateFail;
        }

        /// <summary>
        /// 分享游戏奖励（分类）
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="shareType"></param>
        /// <returns></returns>
        public MessageCodeResponse DoShare(Guid managerId, int shareType)
        {
            try
            {
                DateTime date = DateTime.Now;
                var manager = NbManagerMgr.GetById(managerId);
                if (manager == null)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.MissManager);
                var shareConfig = CacheFactory.ManagerDataCache.GetShare(shareType);
                if (shareConfig.Count <= 0)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
                var entity = ShareManagerMgr.GetByManagerId(managerId, shareType);
                if (entity != null && !shareConfig[0].IsRepetition) //不可重复领取
                {
                    TaskHandler.Instance.Share(managerId);
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.AlreadyShare);
                }
                if (entity != null && entity.ShareNumber >= shareConfig[0].MaxShareNumber)
                    return ResponseHelper.Create<MessageCodeResponse>(MessageCode.MaxShareNumber);
                if (shareType == 1) //第一种特殊处理
                {
                    if (entity != null)
                    {
                        if (entity.ShareNumber > 0 && (date - entity.UpdateTime).Seconds < 3600)
                            return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NowShareNotPrize);
                    }
                }
                int point = 0;
                int coin = 0;
                bool isFirst = false;
                if (entity == null) //首次分享
                {
                    isFirst = true;
                    entity = new ShareManagerEntity(0, managerId, shareType, 1, date, date);
                }
                else
                {
                    entity.ShareNumber ++;
                    entity.UpdateTime = date;
                }
                var itemList = new Dictionary<int, int>();
                var messageCode = SendPrize(shareConfig, isFirst, ref itemList, ref point, ref coin);
                var mail = new MailBuilder(managerId,point,coin,itemList,EnumMailType.Share);
                if (messageCode != MessageCode.Success)
                    return ResponseHelper.Create<MessageCodeResponse>(messageCode);
                messageCode = SavePrize(entity, mail);
                return ResponseHelper.Create<MessageCodeResponse>(messageCode);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("分享游戏", ex);
                return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
            }
        }

        /// <summary>
        /// 分享游戏奖励（发放奖励）
        /// </summary>
        /// <param name="record"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        private MessageCode SavePrize(ShareManagerEntity record, MailBuilder mail)
        {
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                var messageCode = MessageCode.NbUpdateFail;
                do
                {
                    
                    if (record.Idx == 0)
                    {
                        if (!ShareManagerMgr.Insert(record, transactionManager.TransactionObject))
                            break;
                    }
                    else
                    {
                        if (!ShareManagerMgr.Update(record, transactionManager.TransactionObject))
                            break;
                    }
                    if (!mail.Save(transactionManager.TransactionObject))
                        break;
                    messageCode = MessageCode.Success;
                } while (false);
                if (messageCode == MessageCode.Success)
                    transactionManager.Commit();
                else
                    transactionManager.Rollback();
            }
            return MessageCode.Success;
        }

        private MessageCode SendPrize(List<ConfigShareEntity> prizeList, bool isHaveFirst, ref Dictionary<int, int> itemList,
            ref int point, ref int coin)
        {
            List<ConfigShareEntity> prize = null;
            if(isHaveFirst)
                prize = prizeList.FindAll(r => r.PrizeType == 0);
            else
                prize = prizeList.FindAll(r => r.PrizeType == 1);
            foreach (var item in prize)
            {
                var code = AddItem(item, ref itemList, ref point, ref coin);
                if (code != MessageCode.Success)
                    return code;
            }
            return MessageCode.Success;
        }

        private MessageCode AddItem(ConfigShareEntity prize, ref Dictionary<int, int> itemList, ref int point, ref int coin)
        {
            if (prize == null)
                return MessageCode.NbParameterError;
            switch (prize.PrizeItemType)
            {
                case 1://钻石
                    point += prize.Number;
                    break;
                case 2://金币
                    coin += prize.Number;
                    break;
                case 3://指定物品
                    itemList.Add(prize.SubType, prize.Number);
                    break;
                default:
                    break;
            }
            return MessageCode.Success;
        }

        /// <summary>
        /// 分享任务
        /// </summary>
        /// <param name="managerId"></param>
        /// <returns></returns>
        public MessageCodeResponse ShareTask(Guid managerId)
        {
            TaskHandler.Instance.Share(managerId);
            return ResponseHelper.Create<MessageCodeResponse>(MessageCode.Success);
        }
    }
}
