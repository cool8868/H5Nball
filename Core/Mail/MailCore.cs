using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Core.Manager;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Mail
{
    public class MailCore
    {
        #region .ctor

        public MailCore(int p)
        {
            _mailAttachmentExpireDay = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.MailAttachmentExpireDay);
            _mailExpireDay = CacheFactory.AppsettingCache.GetAppSettingToInt(EnumAppsetting.MailExpireDay);
            _mailTransactionTypeDic = new Dictionary<int, EnumTransactionType>(Enum.GetNames(typeof(EnumMailType)).Length);
            _mailTransactionTypeDic.Add(1, EnumTransactionType.MailLadderPrize);
            _mailTransactionTypeDic.Add(2, EnumTransactionType.MailLeaguePrize);
            _mailTransactionTypeDic.Add(3, EnumTransactionType.MailAuctionSaleSuccess);
            _mailTransactionTypeDic.Add(4, EnumTransactionType.MailAuctionBuySuccess);
            _mailTransactionTypeDic.Add(5, EnumTransactionType.MailTourFight);
            _mailTransactionTypeDic.Add(6, EnumTransactionType.MailArenaPrize);
            _mailTransactionTypeDic.Add(7, EnumTransactionType.MailWorldChalengePrize);
            _mailTransactionTypeDic.Add(8, EnumTransactionType.MailAuctionSaleFail);
            _mailTransactionTypeDic.Add(9, EnumTransactionType.MailScoutingLottery);
            _mailTransactionTypeDic.Add(11, EnumTransactionType.MailAuctionCancel);
            _mailTransactionTypeDic.Add(12, EnumTransactionType.MailDailycupPrize);
            _mailTransactionTypeDic.Add(13, EnumTransactionType.MailDailycupPrize);
            _mailTransactionTypeDic.Add(16, EnumTransactionType.MailWorldChalengeStagePrize);
            _mailTransactionTypeDic.Add(17, EnumTransactionType.MailTourHookPrize);
            _mailTransactionTypeDic.Add(18, EnumTransactionType.MailTourElitePrize);
            _mailTransactionTypeDic.Add(19, EnumTransactionType.MailTourPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.Arena, EnumTransactionType.ArenaRankingAward);
            _mailTransactionTypeDic.Add((int)EnumMailType.AdTopScorerKeep, EnumTransactionType.MailTopScoreKeep);
            _mailTransactionTypeDic.Add((int)EnumMailType.ActivityExPrize, EnumTransactionType.MailActivityExPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.Strength9PrizeMaster, EnumTransactionType.Strength9PrizeMaster);
            _mailTransactionTypeDic.Add((int)EnumMailType.EquipmentSynthesisGift, EnumTransactionType.EquipmentSynthesisGift);
            _mailTransactionTypeDic.Add((int)EnumMailType.SudokuAwary, EnumTransactionType.SudokuRankingAward);
            _mailTransactionTypeDic.Add((int)EnumMailType.SudokuAwaryNotItem, EnumTransactionType.SudokuRankingAward);
            _mailTransactionTypeDic.Add((int)EnumMailType.RevelationPassAwary, EnumTransactionType.RevelationClearanceAward);
            _mailTransactionTypeDic.Add((int)EnumMailType.RevelationAwary, EnumTransactionType.RevelationClearanceAward);
            _mailTransactionTypeDic.Add((int)EnumMailType.CrowdKill, EnumTransactionType.MailCrowdKillPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.CrowdRank, EnumTransactionType.MailCrowdRankPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.GiantsAwary, EnumTransactionType.GiantsAwary);
            _mailTransactionTypeDic.Add((int)EnumMailType.GWarRoundPrize, EnumTransactionType.GWarRoundPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.GWarGuildRankPrize, EnumTransactionType.GWarGuildRankPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.GWarManagerRankPrize, EnumTransactionType.GWarManagerRankPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.CrossCrowdKill, EnumTransactionType.MailCrossCrowdKillPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.CrossCrowdRank, EnumTransactionType.MailCrossCrowdRankPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.GWarGuildPresentPrize, EnumTransactionType.GWarGuildPresentPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.CrossLadderPrize, EnumTransactionType.MailCrossLadderPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.CrossLadderDailyPrize, EnumTransactionType.MailCrossLadderDailyPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.Active, EnumTransactionType.Active);
            _mailTransactionTypeDic.Add((int)EnumMailType.Constellation, EnumTransactionType.Constellation);
            _mailTransactionTypeDic.Add((int)EnumMailType.AdSpores, EnumTransactionType.AdSports);
            _mailTransactionTypeDic.Add((int)EnumMailType.PeakBossPrize, EnumTransactionType.PeakBossPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.PeakKillPrize, EnumTransactionType.PeakKillPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.PeakRankPrize, EnumTransactionType.PeakRankPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.PeakWoldPrize, EnumTransactionType.PeakWoldPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.PeakChallengeNpc, EnumTransactionType.PeakChallengeNpc);
            _mailTransactionTypeDic.Add((int)EnumMailType.HirePlayer, EnumTransactionType.MailHirePlayer);
            _mailTransactionTypeDic.Add((int)EnumMailType.HirePlayerEquip, EnumTransactionType.MailHirePlayerEquip);
            _mailTransactionTypeDic.Add((int)EnumMailType.ChampionWinPrize, EnumTransactionType.ChampionWinPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.ChampionRankPrize, EnumTransactionType.ChampionRankPrize);
            _mailTransactionTypeDic.Add((int)EnumMailType.MonthCard, EnumTransactionType.MonthCard);
            _mailTransactionTypeDic.Add((int)EnumMailType.MonthCard1, EnumTransactionType.MonthCard);
            _mailTransactionTypeDic.Add((int)EnumMailType.ChargeSuccess, EnumTransactionType.Charge);
            _mailTransactionTypeDic.Add((int)EnumMailType.GiftBagSuccess, EnumTransactionType.Charge);
            _mailTransactionTypeDic.Add((int)EnumMailType.Share, EnumTransactionType.Share);
            _mailTransactionTypeDic.Add((int)EnumMailType.PackagingRebate, EnumTransactionType.PackagingRebate);
            _mailTransactionTypeDic.Add((int)EnumMailType.Europe, EnumTransactionType.EuropeConfig);
            _mailTransactionTypeDic.Add((int)EnumMailType.CrowdMaxKiller, EnumTransactionType.MailCrossCrowdMaxKillPrize);

            _mailTransactionTypeDic.Add((int)EnumMailType.TransferSuccess, EnumTransactionType.Transfer);
            _mailTransactionTypeDic.Add((int)EnumMailType.TransferBuySuccess, EnumTransactionType.Transfer);
            _mailTransactionTypeDic.Add((int)EnumMailType.TransferRunOff, EnumTransactionType.Transfer);
            _mailTransactionTypeDic.Add((int)EnumMailType.TransferSoldOut, EnumTransactionType.Transfer);
            _mailTransactionTypeDic.Add(100, EnumTransactionType.MailAdminAddItem);
        }
        #endregion

        private readonly int _pageSize=12;
        private readonly int _mailAttachmentExpireDay;
        private readonly int _mailExpireDay;
        private readonly Dictionary<int, EnumTransactionType> _mailTransactionTypeDic;
 
        #region Facade
        public static MailCore Instance
        {
            get { return SingletonFactory<MailCore>.SInstance; }
        }

        public MessageCode ClearExpired()
        {
            try
            {
                MailInfoMgr.ClearExpired(DateTime.Now);
                return MessageCode.Success;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Mail-ClearExpired", ex);
                return MessageCode.Exception;
            }
            
        }

        public MailDataResponse GetMailData(Guid managerId)
        {
            int totalCount = 0;
            var response = ResponseHelper.CreateSuccess<MailDataResponse>();
            response.Data = new MailDataEntity();
            var list = MailInfoMgr.GetByManager(managerId, ref totalCount);
            if (list != null && list.Count > 0)
            {
                foreach (var entity in list)
                {
                    entity.MailTick = ShareUtil.GetTimeTick(entity.RowTime);
                    entity.MailExpiredTick = ShareUtil.GetTimeTick(entity.ExpiredTime);
                    entity.MailAttachment = SerializationHelper.FromByte<MailAttachmentEntity>(entity.Attachment);
                }
            }
            response.Data.Mails = list;
            response.Data.TotalCount = totalCount;
            return response;
        }

        public bool HasUnReadMail(Guid managerId)
        {
            int totalCount = 0;
            var list = MailInfoMgr.GetByManager(managerId, ref totalCount);
            if (list == null)
                return false;
            return list.Any(mailInfoEntity => !mailInfoEntity.IsRead);
        }


        public MailDataResponse DeleteMail(Guid managerId, string recordIds)
        {
            int returnCode = -2;
            MailInfoMgr.Delete(managerId,recordIds, ref returnCode);
            if (returnCode == ShareUtil.SuccessCode)
                return GetMailData(managerId);
            else
            {
                return ResponseHelper.Create<MailDataResponse>(returnCode);
            }
        }

        public MessageCodeResponse Read(Guid managerId, int recordId)
        {
            if (MailInfoMgr.Read(managerId, recordId))
                return ResponseHelper.CreateSuccess<MessageCodeResponse>();
            else
            {
                return ResponseHelper.InvalidParameter<MessageCodeResponse>();
            }
        }

        public MailAttachmentReceiveResponse AttachmentReceive(Guid managerId, int recordId)
        {
            if (recordId == -1)
            {
                var mailList = MailInfoMgr.GetForAttachmentBatch(managerId);
                return AttachmentReceive(mailList);
            }
            else
            {
                var mail = MailInfoMgr.GetById(recordId);
                if (mail == null || mail.ManagerId != managerId)
                    return ResponseHelper.InvalidParameter<MailAttachmentReceiveResponse>();
                if (!ShareUtil.CheckBytes(mail.Attachment))
                {
                    return ResponseHelper.Create<MailAttachmentReceiveResponse>(MessageCode.MailNoAttachment);
                }
                if (mail.HasAttach == false)
                {
                    return ResponseHelper.Create<MailAttachmentReceiveResponse>(MessageCode.MailAttachmentReceiveRepeat);
                }
                var  mailList = new List<MailInfoEntity>(1);
                mailList.Add(mail);
                return AttachmentReceive(mailList);
            }
        }


        public MailAttachmentReceiveResponse AttachmentReceive(List<MailInfoEntity> mailList)
        {
            if (mailList == null)
                return ResponseHelper.InvalidParameter<MailAttachmentReceiveResponse>();
            if(mailList.Count<=0)
                return ResponseHelper.Create<MailAttachmentReceiveResponse>(MessageCode.MailNoAttachmentBatch);
            
            ItemPackageFrame package = null;
            NbManagerEntity manager = null;
            int addCoin = 0;
            int addSophisticate = 0;
            int addPoint = 0;
            int addPrestige = 0;
            int bindPoint = 0;
            int addGoldBar = 0;
            int addluckyCoin = 0;//幸运币 转盘用
            int addGameCoin = 0;//游戏币 点球用
            MessageCode code = MessageCode.Success;
            Guid managerId = Guid.Empty;
            foreach (var entity in mailList)
            {
                code = BuilReceiveAttachmentData(entity, ref manager, ref package, ref addCoin, ref addSophisticate, ref addPoint, ref addPrestige,ref bindPoint,ref addGoldBar,ref addluckyCoin,ref addGameCoin);
                managerId = entity.ManagerId;
                if (code != MessageCode.Success)
                {
                    return ResponseHelper.Create<MailAttachmentReceiveResponse>(code);
                }
            }
            if (addCoin > 0)
            {
                manager.Coin += addCoin;
            }
            if (addSophisticate > 0)
            {
                manager.Sophisticate += addSophisticate;
            }
            code = SaveAttachment(mailList, package, manager, addCoin, addSophisticate, addPoint, addPrestige, bindPoint, addGoldBar, managerId,addluckyCoin,addGameCoin);
            if (code != MessageCode.Success)
            {
                return ResponseHelper.Create<MailAttachmentReceiveResponse>(code);
            }
            if (package != null)
            {
                package.Shadow.Save();
            }
            var response = ResponseHelper.CreateSuccess<MailAttachmentReceiveResponse>();
            response.Data = new MailAttachmentReceiveEntity();
            if (addCoin > 0)
                response.Data.Coin = manager.Coin;
            else
            {
                response.Data.Coin = -1;
            }
            if (addPoint > 0)
                response.Data.Point = PayCore.Instance.GetPoint(manager.Account);
            else
            {
                response.Data.Point = -1;
            }
            if (bindPoint > 0)
                response.Data.BPoint = PayCore.Instance.GetPayUser(manager.Account).BindPoint;
            else
                response.Data.BPoint = -1;
            return response;
        }

        MessageCode BuilReceiveAttachmentData(MailInfoEntity mail, ref NbManagerEntity manager, ref ItemPackageFrame package, ref int addCoin, ref int addSophisticate, ref int addPoint, ref int addPrestige, ref int addBindPoint, ref int addGoldBar, ref int addluckyCoin,ref int addGameCoin)
        {
            var attachments = SerializationHelper.FromByte<MailAttachmentEntity>(mail.Attachment);
            
            var managerId = mail.ManagerId;
            MessageCode code = MessageCode.Success;
            foreach (var attachment in attachments.Attachments)
            {
                #region build Attachment
                switch (attachment.AttachmentType)
                {
                    case (int)EnumAttachmentType.Coin:
                        if (manager == null)
                            manager = ManagerCore.Instance.GetManager(managerId);
                        addCoin += attachment.Count;
                        break;
                    case (int)EnumAttachmentType.Sophisticate:
                        if (manager == null)
                            manager = ManagerCore.Instance.GetManager(managerId);
                        addSophisticate += attachment.Count;
                        break;
                    case (int)EnumAttachmentType.Point:
                        if (manager == null)
                            manager = ManagerCore.Instance.GetManager(managerId);
                        addPoint += attachment.Count;
                        break;
                    case (int)EnumAttachmentType.Prestige:
                        if (manager == null)
                            manager = ManagerCore.Instance.GetManager(managerId);
                        addPrestige += attachment.Count;
                        break;
                    case (int)EnumAttachmentType.NewItem:
                        var attachmentItemEntity = attachment as AttachmentItemEntity;
                        if (attachmentItemEntity == null)
                             return MessageCode.NbParameterError;
                            if (package == null)
                                package = ItemCore.Instance.GetPackage(managerId, _mailTransactionTypeDic[mail.MailType]);
                            code = package.AddItems(attachmentItemEntity.ItemCode, attachmentItemEntity.Count,
                                attachmentItemEntity.Strength, attachmentItemEntity.IsBinding,attachmentItemEntity.IsDeal);
                            if (code != MessageCode.Success)
                            {
                                return code;
                            }
                        break;
                    case (int)EnumAttachmentType.UsedPlayerCard:
                        if (package == null)
                            package = ItemCore.Instance.GetPackage(managerId, _mailTransactionTypeDic[mail.MailType]);
                        var usedPlayerCard = attachment as AttachmentUsedItemEntity;
                        if (usedPlayerCard == null)
                            return MessageCode.NbParameterError;
                        code = package.AddUsedItem(usedPlayerCard.ItemProperty as PlayerCardUsedEntity);
                        if (code != MessageCode.Success)
                        {
                            return code;
                        }
                        break;
                    case (int)EnumAttachmentType.UsedEquipment:
                        if (package == null)
                            package = ItemCore.Instance.GetPackage(managerId, _mailTransactionTypeDic[mail.MailType]);
                        var usedEquipment = attachment as AttachmentUsedItemEntity;
                        if (usedEquipment == null)
                            return MessageCode.NbParameterError;
                        code = package.AddUsedItem(usedEquipment.ItemProperty as EquipmentUsedEntity);
                        if (code != MessageCode.Success)
                        {
                            return code;
                        }
                        break;
                    case (int)EnumAttachmentType.UsedMallItem:
                        //if (package == null)
                        //    package = ItemCore.Instance.GetPackage(managerId, _mailTransactionTypeDic[mail.MailType]);
                        //var usedMallItem = attachment as AttachmentUsedItemEntity;
                        //if (usedMallItem == null)
                        //    return MessageCode.NbParameterError;
                        //var itemProperty = usedMallItem.ItemProperty as MallItemUsedEntity;

                        //code = package.AddUsedItem(itemProperty);
                        //if (code != MessageCode.Success)
                        //{
                        //    return code;
                        //}
                        //break;

                    case (int)EnumAttachmentType.Equipment:
                        if (package == null)
                            package = ItemCore.Instance.GetPackage(managerId, _mailTransactionTypeDic[mail.MailType]);
                        var equipment = attachment as AttachmentEquipmentEntity;
                        if (equipment == null)
                            return MessageCode.NbParameterError;
                        code = package.AddEquipment(equipment.ItemCode, equipment.IsBinding, equipment.IsDeal, equipment.EquipmentProperty);
                        if (code != MessageCode.Success)
                        {
                            return code;
                        }
                        break;
                    case (int)EnumAttachmentType.BindPoint:
                        if (manager == null)
                            manager = ManagerCore.Instance.GetManager(managerId);
                        addBindPoint += attachment.Count;
                        break;
                    case (int)EnumAttachmentType.GoldBar:
                        addGoldBar += attachment.Count;
                        break;
                    case (int)EnumAttachmentType.LuckyCoin:
                        addluckyCoin += attachment.Count;
                        break;
                    case (int)EnumAttachmentType.GameCoin:
                        addGameCoin += attachment.Count;
                        break;
                    default:
                        return MessageCode.NbParameterError;
                    
                }
                #endregion
            }
            mail.IsRead = true;
            mail.HasAttach = false;
            mail.ExpiredTime = GetExpiredTime(false, DateTime.Now);
            return MessageCode.Success;
        }

        public DateTime GetExpiredTime(bool hasAttachment, DateTime now)
        {
            if (hasAttachment)
                return now.Date.AddDays(_mailAttachmentExpireDay);
            else
            {
                return now.Date.AddDays(_mailExpireDay);
            }
        }

        public static void SaveMailBulk(List<MailBuilder> list)
        {
            var mailInfoData = BuildMailBulkTable(list);
            MailSqlHelper.SaveMailBulk(mailInfoData);
        }

        public static MailinfoDataSet.Mail_InfoDataTable BuildMailBulkTable(List<MailBuilder> list)
        {
            List<MailInfoEntity> list2 = new List<MailInfoEntity>(list.Count);
            foreach (var builder in list)
            {
                var entity = builder.MailInfo;
                if (entity == null)
                    continue;
                list2.Add(entity);
            }
            return BuildMailBulkTable(list2);
        }

        public static MailinfoDataSet.Mail_InfoDataTable BuildMailBulkTable(List<MailInfoEntity> list)
        {
            if (list == null)
                return null;
            MailinfoDataSet.Mail_InfoDataTable mailInfoData = new MailinfoDataSet.Mail_InfoDataTable();
            foreach (var entity in list)
            {
                var row = mailInfoData.NewRow();
                if (entity == null)
                    continue;
                row["ManagerId"] = entity.ManagerId;
                row["MailType"] = entity.MailType;
                row["ContentString"] = entity.ContentString;
                row["Attachment"] = entity.Attachment;
                row["HasAttach"] = entity.HasAttach;
                row["IsRead"] = entity.IsRead;
                row["Status"] = entity.Status;
                row["RowTime"] = entity.RowTime;
                row["ExpiredTime"] = entity.ExpiredTime;
                mailInfoData.Rows.Add(row);
            }
            return mailInfoData;
        }

        #endregion

        #region encapsulation
        MessageCode SaveAttachment(List<MailInfoEntity> mails, ItemPackageFrame package, NbManagerEntity manager, int coin, int sophicate, int point, int prestige, int bindPoint, int addGoldBar,Guid managerId,int addluckyCoin,int addGameCoin)
        {
            if (mails == null || mails.Count<=0)
                return MessageCode.NbUpdateFail;
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    var messageCode = Tran_SaveAttachment(transactionManager.TransactionObject, mails, package, manager, coin, sophicate, point, prestige, bindPoint, addGoldBar,managerId,addluckyCoin,addGameCoin);

                    if (messageCode == ShareUtil.SuccessCode)
                    {
                        transactionManager.Commit();
                        if (coin > 0)
                            ManagerCore.Instance.UpdateCoinAfter(manager);
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                    return messageCode;
                }
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveTourMatch", ex);
                return MessageCode.Exception;
            }
        }

        MessageCode Tran_SaveAttachment(DbTransaction transaction, List<MailInfoEntity> mails, ItemPackageFrame package, NbManagerEntity manager, int coin, int sophicate, int point, int prestige, int bindPoint, int addGoldBar, Guid managerId, int addluckyCoin, int addGameCoin)
        {
            var orderId = mails[0].Idx.ToString();
            foreach (var mail in mails)
            {
                if (!MailInfoMgr.Update(mail, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (package != null)
            {
                if (!package.Save(transaction))
                {
                    return MessageCode.NbUpdateFail;
                }
            }
           
            if (coin > 0)
            {
                var code = ManagerCore.Instance.AddCoin(manager, coin, EnumCoinChargeSourceType.MailAttachment, orderId, transaction);
                if(code!=MessageCode.Success)
                    return MessageCode.NbUpdateFail;
            }
            if (sophicate > 0)
            {
                int resultSophisticate = manager.Sophisticate;
                if(!NbManagerMgr.AddSophisticate(manager.Idx, sophicate, ref resultSophisticate,transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (point > 0)
            {
                var code = PayCore.Instance.AddBonus(manager.Account, point, EnumChargeSourceType.MailAttachment,
                                                     orderId, transaction);
                if(code!=MessageCode.Success)
                    return MessageCode.NbUpdateFail;
            }
            if (bindPoint > 0)
            {
                var payUser = PayUserMgr.GetById(manager.Account);
                if (payUser == null)
                {
                    payUser = new PayUserEntity();
                    payUser.Account = manager.Account;
                    payUser.BindPoint = bindPoint;
                    payUser.RowTime = DateTime.Now;
                    payUser.IsNew = true;
                }else
                    payUser.BindPoint += bindPoint;
                if (payUser.IsNew)
                {
                    if (!PayUserMgr.Insert(payUser, transaction))
                        return MessageCode.NbUpdateFail;
                }
                else
                {
                    if (!PayUserMgr.Update(payUser, transaction))
                        return MessageCode.NbUpdateFail;
                }
            }
            if (addGoldBar > 0)
            {
                var goldBarManager = ScoutingGoldbarMgr.GetById(managerId);
                if (goldBarManager == null)
                {
                    goldBarManager = new ScoutingGoldbarEntity(managerId, addGoldBar, 0, 0, 0, DateTime.Now,
                        DateTime.Now);
                    if (!ScoutingGoldbarMgr.Insert(goldBarManager, transaction))
                        return MessageCode.NbUpdateFail;
                }
                else
                {
                    goldBarManager.GoldBarNumber = goldBarManager.GoldBarNumber + addGoldBar;
                    if (!ScoutingGoldbarMgr.Update(goldBarManager, transaction))
                        return MessageCode.NbUpdateFail;
                }
                GoldbarRecordEntity record = new GoldbarRecordEntity(0, managerId, true, addGoldBar, (int)EnumCoinChargeSourceType.MailAttachment, DateTime.Now);
                GoldbarRecordMgr.Insert(record);
            }
            if (addluckyCoin > 0)
            {
                if (!TurntableManagerMgr.AddLuckyCoin(managerId, addluckyCoin, transaction))
                    return MessageCode.NbUpdateFail;
            }
            if (addGameCoin > 0)
            {
                if (!PenaltykickManagerMgr.AddGameCurrency(managerId, addGameCoin, transaction))
                    return MessageCode.NbUpdateFail;
            }

            return MessageCode.Success;
        }
        #endregion
    }
}
