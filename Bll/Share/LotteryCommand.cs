//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Games.NBall.Entity;
//using Games.NBall.Entity.Enums;

//namespace Games.NBall.Bll.Share
//{
//    public class LotteryCommand
//    {
//        public static List<DicItemEntity> LotteryPlayers(int managerLevel,int cardCount)
//        {
//            //DC_LotteryCommand command = CacheFactory.LotteryCommandCache.GetLotteryCommand(managerLevel);

//            List<DicItemEntity> list = new List<DicItemEntity>();
//            for (int i = 0; i < cardCount; i++)
//            {
//                var item = GetLotteryItem();//command.RandomCard, area);
//                while (list.Exists(d => d.ItemCode == item.ItemCode)) //排除重复
//                {
//                    item = GetLotteryItem(); //command.RandomCard, area);
//                }
//                //#region 特殊橙卡的概率降低50% add by Allen Zheng
//                ////如果抽中的是特殊橙卡球员降低50%概率
//                //if (i < 2)
//                //{
//                //    if (SpecialOrangePlayers.Contains(item.LinkId))
//                //    {
//                //        int random = RandomGenerater.Random(0, 1);
//                //        //random=0时或变成兰卡，random=1时为橙卡
//                //        if (random == 0)
//                //        {

//                //            DC_Playersdic player = CacheFactory.DicPlayersCache.RandomPlayer((int)PlayerLevel.Blue, area);
//                //            item = CacheFactory.ItemCache.GetByLinkIdAndType(player.Idx, (int)ItemType.PlayerCard);

//                //            while (list.Exists(d => d.ItemCode == item.ItemCode)) //排除重复
//                //            {
//                //                item = CacheFactory.ItemCache.GetByLinkIdAndType(player.Idx, (int)ItemType.PlayerCard);
//                //            }
//                //        }
//                //    }
//                //}
//                //#endregion

//                ////排除特殊小红人，暂时无法获得
//                //bool hasLockCard = CacheFactory.RedcardConfigCache.HasLockCard(item.ItemCode);
//                //if (hasLockCard)
//                //{
//                //    DC_Playersdic player = CacheFactory.DicPlayersCache.RandomPlayer((int)PlayerLevel.Purple, area);
//                //    item = CacheFactory.ItemCache.GetByLinkIdAndType(player.Idx, (int)ItemType.PlayerCard);

//                //    while (list.Exists(d => d.ItemCode == item.ItemCode)) //排除重复
//                //    {
//                //        item = CacheFactory.ItemCache.GetByLinkIdAndType(player.Idx, (int)ItemType.PlayerCard);
//                //    }
//                //}
//                list.Add(item);
//            }
//            return list;
//        }

//        private static DicItemEntity GetLotteryItem()//DC_LotteryCard lotteryCard, int area)
//        {
//            return CacheFactory.ItemsdicCache.RandomItem(EnumItemType.PlayerCard);
//            //DC_Itemdic itemdic = null;
//            //switch (lotteryCard.CardType)
//            //{
//            //    case (int)ItemType.PlayerCard:
//            //        var playersdic = CacheFactory.DicPlayersCache.RandomPlayer(lotteryCard.CardLevel, area);
//            //        itemdic = CacheFactory.ItemCache.GetByLinkIdAndType(playersdic.Idx, (int)ItemType.PlayerCard);
//            //        itemdic.ImageId = playersdic.Idx;//球员卡的头像不是imageid，而是球员id
//            //        break;
//            //    case (int)ItemType.ActivityCard:
//            //        itemdic = CacheFactory.ItemCache.RandomActivityItem(lotteryCard.CardLevel);
//            //        break;
//            //    case (int)ItemType.ActivityMallItem:
//            //        itemdic = CacheFactory.ItemCache.RandomActivityMallItem(lotteryCard.CardLevel);
//            //        break;
//            //    case (int)ItemType.MallItem:
//            //        itemdic = CacheFactory.ItemCache.GetByItemCode(lotteryCard.CardLevel);
//            //        break;
//            //    case (int)ItemType.Formula:
//            //        itemdic = CacheFactory.ItemCache.RandomFormula(lotteryCard.CardLevel);
//            //        break;
//            //}
//            //return itemdic;
//        }
//        /*
//        /// <summary>
//        /// Saves the lottery card.
//        /// </summary>
//        /// <param name="matchId">The match id.</param>
//        /// <param name="managerId">The manager id.</param>
//        /// <param name="isGetPreview">是否获取预览的球员卡.</param>
//        /// <param name="eventId">The event id.</param>
//        /// <param name="isFromPark">是否from神秘小屋</param>
//        /// <param name="isFromEvent">if set to <c>true</c> [is from event].</param>
//        /// <returns></returns>
//        /// <remarks>
//        /// 1、从DB中获取抽奖记录
//        /// 2、判断经理是否匹配
//        /// 3、判断抽奖记录的状态
//        /// |--已摇奖
//        /// |--判断传入的卡id是否匹配
//        /// |--判断经理背包是否已满
//        /// |--是：返回背包已满信息
//        /// |--否：更新抽奖记录，更新经理背包
//        /// 4、返回信息
//        /// </remarks>
//        /// <history>
//        /// [fuxiaogang]     2009-12-23 15:13     Created
//        /// </history>
//        public static DC_LotteryResult SaveLotteryCard(Guid matchId, Guid managerId, bool isGetPreview, long eventId, bool isFromPark, bool isFromEvent)
//        {
//            try
//            {
//                var manager = GetByManagerId(managerId, false, false, false, false, true);

//                if (manager == null)
//                {
//                    return new DC_LotteryResult(MessageCode.NoManager);
//                }

//                DC_Slothistory slot = SlothistoryMgr.GetById(matchId);

//                #region 验证
//                if (slot == null //不存在的抽奖id
//                    || slot.ManagerId != manager.ManagerGuid //经理不匹配
//                    )
//                {
//                    return new DC_LotteryResult(MessageCode.LrParameterError);
//                }
//                if (slot.Status == (int)LotteryStatus.HasSend)//已领奖，不能重复抽奖
//                {
//                    return new DC_LotteryResult(MessageCode.LrRepeatError);
//                }
//                #endregion

//                List<DC_LotteryItem> resultItems = new List<DC_LotteryItem>();
//                string resultIds = "";
//                string resultNames = "";
//                var showItems = new List<DC_LotteryItem>();
//                DC_LotteryItem dcLotteryItem = null;
//                #region 处理获取到的卡片
//                switch (slot.SlotType)
//                {
//                    case (int)SlotType.Normal:
//                        dcLotteryItem = BuildLotteryItem(slot.PlayerIdString, 0);
//                        resultIds = resultIds + "," + dcLotteryItem.ItemCode;
//                        resultNames = resultNames + "," + dcLotteryItem.Name;
//                        resultItems.Add(dcLotteryItem);
//                        break;
//                    case (int)SlotType.PrimarySkill:
//                        if (slot.GiveupNumber == 1 || (isFromEvent == false && isGetPreview == false))//获取第二张
//                        {
//                            dcLotteryItem = BuildLotteryItem(slot.PlayerIdString, 1);
//                            resultIds = resultIds + "," + dcLotteryItem.ItemCode;
//                            resultNames = resultNames + "," + dcLotteryItem.Name;
//                            resultItems.Add(dcLotteryItem);
//                        }
//                        else
//                        {
//                            dcLotteryItem = BuildLotteryItem(slot.PlayerIdString, 0);
//                            resultIds = resultIds + "," + dcLotteryItem.ItemCode;
//                            resultNames = resultNames + "," + dcLotteryItem.Name;
//                            resultItems.Add(dcLotteryItem);
//                        }
//                        break;
//                    case (int)SlotType.SecondarySkill:
//                        dcLotteryItem = BuildLotteryItem(slot.PlayerIdString, 0);
//                        resultIds = resultIds + "," + dcLotteryItem.ItemCode;
//                        resultNames = resultNames + "," + dcLotteryItem.Name;
//                        resultItems.Add(dcLotteryItem);

//                        dcLotteryItem = BuildLotteryItem(slot.PlayerIdString, 1);
//                        resultIds = resultIds + "," + dcLotteryItem.ItemCode;
//                        resultNames = resultNames + "," + dcLotteryItem.Name;
//                        resultItems.Add(dcLotteryItem);
//                        break;
//                    default:
//                        return new DC_LotteryResult(MessageCode.ParameterError);
//                }

//                var playerIdString = slot.PlayerIdString;
//                for (int i = 0; i < 5; i++)
//                {
//                    var lotteryItem = BuildLotteryShowItem(playerIdString, i, resultIds);
//                    if (lotteryItem != null)
//                    {
//                        showItems.Add(lotteryItem);
//                    }
//                }
//                #endregion

//                //判断经理背包是否已满
//                DC_Itempackage package = manager.ItemPackages[0];

//                if (package.BlankCount < resultItems.Count)
//                {
//                    if (!isFromPark && !isFromEvent && slot.Status == (int)LotteryStatus.HasLottery)//不是从神秘屋和事件过来的，则发送事件
//                    {
//                        if (slot.SlotType == (int)SlotType.PrimarySkill && isGetPreview == false) //放弃展示的那个
//                            slot.GiveupNumber = 1;
//                        slot.Status = (int)LotteryStatus.PrepareSend;
//                        SlothistoryMgr.UpdateSlothistory(slot);

//                        EventRules.SavePostPlayerCard(managerId, manager.Name, resultNames, matchId, "");
//                    }
//                    return new DC_LotteryResult(MessageCode.LrPackageFull, slot.SlotType, showItems, resultItems);
//                }

//                bool isSuccess = false;
//                int errorCount = 0;

//                #region Shadow
//                S_TransactionLog sTransactionLog = null;
//                if (isFromPark == true)//神秘小屋
//                    sTransactionLog = new S_TransactionLog(TransactionType.CardFromPark, managerId);
//                else
//                {
//                    sTransactionLog = new S_TransactionLog(TransactionType.CardLottery, managerId);
//                }
//                #endregion

//                foreach (var resultItem in resultItems)
//                {
//                    var card = new DC_Item();
//                    card.Count = 1;
//                    card.ItemCode = resultItem.ItemCode;
//                    card.ItemId = Guid.GenerateComb();
//                    card.Strengthen = 1;
//                    card.Type = (ItemType)resultItem.ItemType;
//                    card.ItemRarityLevel = CacheFactory.ItemCache.GetCardLevel(resultItem.ItemCode);
//                    //绑定周年活动卡牌
//                    ActivityExchangeRules.BindActivityItem(card);

//                    if (!package.Add(card))
//                    {
//                        errorCount++;
//                        break;
//                    }
//                    sTransactionLog.AddShadow(card, OperationType.New);
//                }

//                if (errorCount > 0)
//                {
//                    return new DC_LotteryResult(MessageCode.LrUpPackageFail);
//                }

//                //更新抽奖记录为已领奖
//                slot.Status = (int)LotteryStatus.HasSend;
//                slot.ExchangeTime = DateTime.Now;//奖品兑换时间
//                slot.ExchangeNumber = resultItems.Count;
//                if (slot.SlotType == (int)SlotType.PrimarySkill && isGetPreview == false) //放弃展示的那个
//                    slot.GiveupNumber = 1;

//                isSuccess = CommonTransaction.Instance.SaveSlotData(managerId, package, slot);
//                if (isSuccess)
//                {
//                    //发送事件
//                    foreach (var resultItem in resultItems)
//                    {
//                        EventRules.SaveNewPlayerCard(manager.ManagerGuid, manager.Name, resultItem.Name);
//                    }
//                    if (isFromEvent)
//                    {
//                        EventinfoMgr.Delete(eventId);
//                    }

//                    LogManager.WriteTransaction(sTransactionLog);

//                    #region 春节活动卡牌统计
//                    foreach (var item in resultItems)
//                    {
//                        ActivityExchangeRules.GainActivityItem(managerId, item.ItemCode, item.ItemType, item.Name, item.ImageId, 1);
//                    }
//                    #endregion

//                    return new DC_LotteryResult(MessageCode.Success, slot.SlotType, showItems, resultItems);
//                }
//                else
//                {
//                    return new DC_LotteryResult(MessageCode.LrUpPackageFail);
//                }
//            }
//            catch (Exception ex)
//            {
//                SystemerrorlogMgr.Insert("SaveLotteryCard", ex.Message, ex.StackTrace);
//                return new DC_LotteryResult(MessageCode.Exception);
//            }
//        }
//         */
//    }
//}
