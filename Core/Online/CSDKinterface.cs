using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Activity;
using Games.NBall.Core.Mail;
using Games.NBall.Core.Manager;
using Games.NBall.Core.Task;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.NBall.Custom.A8csdk;
using Games.NBall.Entity.Response.Friend;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Online
{
   public  class CSDKinterface
   {
        /// <summary>
        /// a8玩家数据缓存
        /// </summary>
       private Dictionary<string, A8csdkStartgameEntity> _a8CsdkStartgameEntities;


       public CSDKinterface(int p)
       {

           _a8CsdkStartgameEntities=new Dictionary<string, A8csdkStartgameEntity>();
       }

       public static CSDKinterface Instance
       {
           get { return SingletonFactory<CSDKinterface>.SInstance; }
       }




       /// <summary>
       /// 储存渠道玩家数据
       /// </summary>
       /// <param name="openId"></param>
       /// <param name="state"></param>
       /// <param name="serverId"></param>
       /// <param name="pf"></param>
       /// <param name="sessionId"></param>
       /// <param name="jsNeed"></param>
       /// <param name="nickName"></param>
       /// <returns></returns>
       public bool SetStartGameEntity(string openId, string state, string serverId, string pf, string sessionId,
           string jsNeed = "", string nickName = "",string common="")
       {
           var info = A8csdkStartgameMgr.GetById(openId);
           bool isInsert = false;
           if (info == null)
           {
               isInsert = true;
               info = new A8csdkStartgameEntity();
               info.OpenId = openId;
           }
           info.State = state;
           info.ServerId = serverId;
           info.Pf = pf;
           info.SessionId = sessionId;
           info.JsNeed = jsNeed;
           info.NickName = nickName;
           info.Common = common;


           if (_a8CsdkStartgameEntities.ContainsKey(openId))
           {
               _a8CsdkStartgameEntities[openId] = info;
           }
           else
           {
               _a8CsdkStartgameEntities.Add(openId, info);

           }
           UserLoginCore.Instance.SetUserLogin(openId, pf, serverId);
           if (isInsert)
           {
               return A8csdkStartgameMgr.Insert(info);
           }
           else
           {
               return A8csdkStartgameMgr.Update(info);
           }

       }

       /// <summary>
      /// 获得渠道玩家数据
      /// </summary>
      /// <param name="openId"></param>
      /// <returns></returns>
       public A8csdkStartgameEntity GetStartgameEntity(string openId)
       {
           if (_a8CsdkStartgameEntities.ContainsKey(openId))
           {
               return _a8CsdkStartgameEntities[openId];

           }
           return A8csdkStartgameMgr.GetById(openId);
       }
       /// <summary>
       /// 是否是玩吧达人
       /// </summary>
       /// <param name="account"></param>
       /// <returns></returns>
       public bool IsTxVip(string account)
       {
           var entity=TxYellowvipMgr.GetById(account);
           if (entity != null && entity.IsYellowVip!=null)
               return entity.IsYellowVip;
           return false;
       }

       public bool IsTxVip(NbManagerEntity manager)
       {
           return IsTxVip(manager.Account);
       }

       public bool IsTxVip(Guid managerId)
       {
           var manager = NbManagerMgr.GetById(managerId);
           if (manager == null)
               return false;
           return IsTxVip(manager);
       }
       /// <summary>
       /// 批量获取玩家信息
       /// </summary>
       /// <param name="data">json</param>
       /// <returns></returns>
       public List<NbManagerEntity> IsRegistByNameList(string data)
       {
           data = "{\"data\":" + data + "}";
           JavaScriptSerializer serializer = new JavaScriptSerializer();
           var listData = new OtherTweJsonEntity();
           try
           {
               listData = serializer.Deserialize<OtherTweJsonEntity>(data);
           }
           catch (Exception e)
           {
               SystemlogMgr.Info("IsRegistByNameList", e.Message + "|json解析错误");
               return null;
           }
           if (listData == null || listData.Data == null || listData.Data.Count == 0)
           {
               SystemlogMgr.Info("IsRegistByNameList", "参数为空");

               return null;
           }
           SystemlogMgr.Info("IsRegistByNameList", listData.Data.Count + "|计数");

           var list = new List<NbManagerEntity>();
           foreach (var entity in listData.Data)
           {
               var manager = new NbManagerEntity();
               manager =NbManagerMgr.GetByName(entity.Name);
               if (manager == null || manager.Name != entity.Name)
               {
                   SystemlogMgr.Info("IsRegistByNameList", entity.Name + "不存在" + manager.Name);
                   return null;
               }
               list.Add(manager);
           }
           return list;
       }

       /// <summary>
       /// 分享游戏任务
       /// </summary>
       /// <param name="managerId"></param>
       /// <returns></returns>
       public MessageCodeResponse ShareTask(Guid managerId)
       {
           try
           {
               var manager = ManagerCore.Instance.GetManager(managerId);
               if (manager == null)
                   return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
               TaskHandler.Instance.Share(managerId);
               var response = new MessageCodeResponse();
               response.Code = (int)MessageCode.Success;
               return response;
           }
           catch (Exception ex)
           {
               SystemlogMgr.Error("分享游戏", ex);
               return ResponseHelper.Create<MessageCodeResponse>(MessageCode.NbParameterError);
           }
       }

       /// <summary>
      ///  分享礼包
      /// </summary>
      /// <param name="name"></param>
      /// <param name="type"></param>
      /// <returns></returns>
       public int SendItemByShare(string name ,int type)
      {
           
          if (string.IsNullOrEmpty(name) || type == 0)
              return (int)MessageCode.NbParameterError;//参数错误
           var manager=ManagerCore.Instance.GetManagerByName(name);
          if (manager == null||manager.Idx==Guid.Empty)
              return (int)MessageCode.LoginNoUser;//没有该经理
          var shareEntity = NbManagershareMgr.Select(manager.Idx);
          if (shareEntity == null||shareEntity.ManagerId==Guid.Empty)
          {
              return (int)MessageCode.LoginNoUser;
          }

          //拿取礼包奖励 MallCache.GetMallGiftBagPrize 10001为发出邀请礼包  10002为接受邀请礼包
          
          int code = 0;
          List<ConfigMallgiftbagEntity> prizeList = new List<ConfigMallgiftbagEntity>();
         
          switch (type)
          {
              case 1:
                  if (shareEntity.OutPut != 0)
                      return (int)MessageCode.TourPassPrizeHasReceive;
                  shareEntity.OutPut = 1;
                  shareEntity.OutTime = DateTime.Now;
                  prizeList=CacheFactory.MallCache.GetMallGiftBagPrize(10001);
                  code = SendItemByType(shareEntity,prizeList);
                  break;
              case 2:
                  if (shareEntity.InPut != 0)
                      return (int)MessageCode.TourPassPrizeHasReceive;
                  shareEntity.InPut = 1;
                  shareEntity.InTime = DateTime.Now;
                  prizeList = CacheFactory.MallCache.GetMallGiftBagPrize(10002);
                  code = SendItemByType(shareEntity,prizeList);
                  break;
          }
          return code;
      }

       int SendItemByType(NbManagershareEntity shareEntity,List<ConfigMallgiftbagEntity> prizeList)
       {
           if (prizeList.Count <= 0)
               return (int)MessageCode.NbParameterError;
           var mail = new MailBuilder(shareEntity.ManagerId, "分享礼包", 0, prizeList, EnumMailType.Share,0,0);
       //    var mail = new MailBuilder(shareEntity.ManagerId, point, coin, itemList, EnumMailType.Share);
           using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
           {
               transactionManager.BeginTransaction();
               var f = true;
               if (!mail.Save(transactionManager.TransactionObject))
                   f = false;

               if (!NbManagershareMgr.Update(shareEntity, transactionManager.TransactionObject))
                   f = false;
               if (f)
                   transactionManager.Commit();
               else
               {
                   transactionManager.Rollback();
                   return (int)MessageCode.Exception;
               }
           }
           return (int)MessageCode.Success;
       }
   }
}
