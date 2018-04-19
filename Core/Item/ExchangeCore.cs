using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Mall;
using Games.NBall.Core.Manager;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response.Item;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Item
{
    public class ExchangeCore
    {
        #region .ctor
        public ExchangeCore(int p)
        {

        }
        #endregion

        #region Facade
        public static ExchangeCore Instance
        {
            get { return SingletonFactory<ExchangeCore>.SInstance; }
        }

        public ExchangeResponse Exchange(Guid managerId, string exchangeId,string pf)
        {
            if (string.IsNullOrEmpty(exchangeId))
                return ResponseHelper.Create<ExchangeResponse>(MessageCode.ExchangeCodeNotExists);
            var manager = ManagerCore.Instance.GetManager(managerId);
            if (manager == null)
                return ResponseHelper.Create<ExchangeResponse>(MessageCode.NbParameterError);
            var packCode = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_360GoodsBag);
            var arr = packCode.Split('|');
            if (arr.Count() >= 2)
            {
                if (exchangeId == arr[0])
                {
                    //类型1为360礼包
                    return Send360GoodsBay(manager, arr[1],arr[0],1);
                }
            }
            var packCode2 = CacheFactory.AppsettingCache.GetAppSetting(EnumAppsetting.H5_888kkk);
            var arr2 = packCode2.Split('|');
            if (arr2.Count() >= 2)
            {
                if (exchangeId == arr2[0])
                {
                    //类型2为888kkk礼包
                    return Send360GoodsBay(manager, arr2[1], arr2[0],2);
                }
            }
           
            ExchangeInfoEntity exchangeInfo = null;
            exchangeInfo = ExchangeInfoMgr.GetById(exchangeId);
            if (exchangeInfo == null )
                return ResponseHelper.Create<ExchangeResponse>(MessageCode.ExchangeCodeNotExists);
            if (exchangeInfo.Status == 1)
                return ResponseHelper.Create<ExchangeResponse>(MessageCode.ExchangeIsUsed);
            if (exchangeInfo.PlatformCode !="h5_a8")
            {
                if (pf != exchangeInfo.PlatformCode)
                    return ResponseHelper.Create<ExchangeResponse>(MessageCode.ExchangeCodeNotExists);
            }
            int returnCode = -2;
            var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.ExchangeCodePrize);
            int effectCoin = 0;
            int curValue = 0;
            int effectPoint = 0;
            int bindPoint = 0;
            ExchangeEntity exchangeEntity = new ExchangeEntity();
            exchangeEntity.PrizeList = new List<ExchangePrizeEntity>();

            var messagecode = MallCore.Instance.UseNewPlayerPack(managerId, exchangeInfo.PackId, package, EnumCoinChargeSourceType.Exchange, ref manager, ref effectCoin,
                                               ref curValue, ref effectPoint, ref bindPoint, exchangeEntity);

          
            if (messagecode != MessageCode.Success)
                return ResponseHelper.Create<ExchangeResponse>(messagecode);

            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Support)))
                {
                    transactionManager.BeginTransaction();
                        ExchangeInfoMgr.Save(exchangeInfo.Idx, ShareUtil.PlatformCode, manager.Account, manager.Idx, ShareUtil.ZoneId, exchangeInfo.PackId,
                                         exchangeInfo.RowVersion, (int)MessageCode.ExchangeBatchLimit, ref returnCode, transactionManager.TransactionObject);

                        if (returnCode==(int)MessageCode.Success)
                    {

                        if (effectCoin > 0)
                        {
                            ManagerUtil.SaveManagerData(manager, null);
                            ManagerUtil.SaveManagerAfter(manager);
                        }
                        if (package.Save())
                            package.Shadow.Save();
                        if (effectPoint > 0)
                        {
                            messagecode = PayCore.Instance.AddBonus(manager.Account, effectPoint,
                                EnumChargeSourceType.ExchangePrize,
                                exchangeId);
                        }
                    }
                    if (messagecode==MessageCode.Success)
                    {
                        transactionManager.Commit();
                    
                    }
                    else
                    {
                        transactionManager.Rollback();
                    }
                  
                }
          

            if (returnCode != 0)
            {

                return ResponseHelper.Create<ExchangeResponse>(returnCode);

            }
            exchangeEntity.ExchangeType = exchangeInfo.ExchangeType;



                var response = ResponseHelper.CreateSuccess<ExchangeResponse>();
                response.Data = new ExchangeEntity();
                response.Data.PrizeList=new List<ExchangePrizeEntity>();
                response.Data.PrizeList = exchangeEntity.PrizeList;
                    response.Code = 0;
                
                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveItemException", ex);
                return ResponseHelper.Create<ExchangeResponse>(returnCode);
            }
               
        
        }
        /// <summary>
        /// 360礼包
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="packStr"></param>
        /// <param name="exchangeId"></param>
        /// <returns></returns>
        private ExchangeResponse Send360GoodsBay(NbManagerEntity manager, string packStr, string exchangeId ,int type)
        {
            var response = new ExchangeResponse();
            var entity = NbManagercommonpackageMgr.Select(manager.Idx);//公用字段1   为360礼包  字段2为888kkk礼包
            if (entity != null)
            {
                switch (type)
                {
                    case 1:
                        var i = entity.Common1;
                        var common1 = ConvertHelper.ConvertToInt(i);
                        if (common1 >= 1)
                        {
                            response.Code = (int)MessageCode.NbPrizeRepeat;
                            return response;
                        }
                        else
                        {
                            entity.Common1 = "1";
                        }
                        break;


                    case 2:
                        var i2 = entity.Common2;
                        var common2 = ConvertHelper.ConvertToInt(i2);
                        if (common2 >= 1)
                        {
                            response.Code = (int)MessageCode.NbPrizeRepeat;
                            return response;
                        }
                        else
                        {
                            entity.Common2 = "1";
                            
                        }
                        break;
                }

            }
            var packId = ConvertHelper.ConvertToInt(packStr);
            var package = ItemCore.Instance.GetPackage(manager.Idx, EnumTransactionType.ExchangeCodePrize);
            int effectCoin = 0;
            int curValue = 0;
            int effectPoint = 0;
            int bindPoint = 0;
            ExchangeEntity exchangeEntity = new ExchangeEntity();
            exchangeEntity.PrizeList = new List<ExchangePrizeEntity>();

            response.Code =(int) MallCore.Instance.UseNewPlayerPack(manager.Idx, packId, package, EnumCoinChargeSourceType.Exchange, ref manager, ref effectCoin,
                                               ref curValue, ref effectPoint, ref bindPoint, exchangeEntity);
            if (response.Code !=(int) MessageCode.Success)
                return response;
            try
            {
                using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
                {
                    transactionManager.BeginTransaction();
                    response.Code = (int)MessageCode.Exception;
                  

                    if (entity != null)
                    {
                      
                        if (NbManagercommonpackageMgr.Update(entity, transactionManager.TransactionObject))
                        {
                            response.Code = (int) MessageCode.Success;
                        }
                    }
                    if (response.Code == (int)MessageCode.Success)
                    {

                        if (effectCoin > 0)
                        {
                            ManagerUtil.SaveManagerData(manager, null, transactionManager.TransactionObject);
                            ManagerUtil.SaveManagerAfter(manager);
                        }
                        if (package.Save(transactionManager.TransactionObject))
                        {
                            package.Shadow.Save();
                        }
                        else
                        {
                            response.Code = (int) MessageCode.ItemNoShadow;
                        }
                        if (effectPoint > 0)
                        {
                            response.Code =(int) PayCore.Instance.AddBonus(manager.Account, effectPoint,
                                EnumChargeSourceType.ExchangePrize, exchangeId, transactionManager.TransactionObject);
                        }
                    }
                    if (response.Code ==(int) MessageCode.Success)
                    {
                        transactionManager.Commit();

                    }
                    else
                    {
                        transactionManager.Rollback();
                    }

                }


                if (response.Code != 0)
                {

                    return ResponseHelper.Create<ExchangeResponse>(response.Code);

                }
                exchangeEntity.ExchangeType =1;
                response.Data = new ExchangeEntity();
                response.Data.PrizeList = new List<ExchangePrizeEntity>();
                response.Data.PrizeList = exchangeEntity.PrizeList;
                response.Code = 0;

                return response;
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("SaveItemException", ex);
                return ResponseHelper.Create<ExchangeResponse>(MessageCode.Exception);
            }



            return response;
        }

        #endregion
    }
}
