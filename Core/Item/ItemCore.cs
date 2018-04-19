using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Shadow;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Task;
using Games.NBall.Core.Teammember;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.Response;
using Games.NBall.ServiceEngine;
using MsEntLibWrapper.Data;

namespace Games.NBall.Core.Item
{
    public class ItemCore
    {
        #region .ctor
        public ItemCore(int p)
        {

        }
        #endregion

        #region Facade
        public static ItemCore Instance
        {
            get { return SingletonFactory<ItemCore>.SInstance; }
        }

        public void Test()
        {
            var package = ItemCore.Instance.GetPackage(new Guid("D1BDE417-57DD-475A-8BCF-A539010AAAF8"),
                EnumTransactionType.TeammemberTrans);
            if (package == null)
                return;
            package.AddItem(110002);
            using (var transactionManager = new TransactionManager(Dal.ConnectionFactory.Instance.GetDefault()))
            {
                transactionManager.BeginTransaction();
                bool b = package.SavePlayer(transactionManager.TransactionObject);
                if (b)
                    transactionManager.Commit();
                else
                    transactionManager.Rollback();
            }
          
        }

        #region 获取背包
        public ItemPackageFrame GetPackageWithoutShadow(Guid managerId, string zoneId = "")
        {
            return new ItemPackageFrame(ItemPackageMgr.GetById(managerId,zoneId),zoneId);
        }

        public ItemPackageFrame GetPackage(Guid managerId, EnumTransactionType transactionType, string zoneId = "")
        {
            var package = new ItemPackageFrame(ItemPackageMgr.GetById(managerId, zoneId), transactionType, zoneId);
            //AchievementTaskCore.Instance.UpdatePlayCardCount(package);
            return package;
        }

        public ItemPackageResponse GetPackageResponse(Guid managerId)
        {
            var package = ItemPackageMgr.GetById(managerId);
            var response = ResponseHelper.CreateSuccess<ItemPackageResponse>();
            response.Data = package;

            //AchievementTaskCore.Instance.UpdatePlayCardCount(new ItemPackageFrame(package));
            
            var packageItemsEntity = SerializationHelper.FromByte<ItemPackageItemsEntity>(package.ItemString);
            if (packageItemsEntity == null || packageItemsEntity.Items == null)
                response.Data.Items = new List<ItemInfoEntity>();
            else
            {
                var teammember = MatchDataHelper.GetSolutionTeammembers(managerId);
                ItemPackageFrame.CaluPackageCardKpi(packageItemsEntity.Items,teammember);
                response.Data.Items = packageItemsEntity.Items;
            }
           
            return response;
        }
        #endregion
        /// <summary>
        /// 后台添加物品
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="managerId"></param>
        /// <param name="itemCode"></param>
        /// <param name="itemCount"></param>
        /// <param name="strength"></param>
        /// <param name="isBinding"></param>
        /// <param name="slotColorCount"></param>
        /// <returns></returns>
        public MessageCode GMAddItem(string zoneId, Guid managerId, int itemCode, int itemCount, int strength,
            bool isBinding, int slotColorCount = 0)
        {
            try
            {
            var package = GetPackage(managerId, EnumTransactionType.AdminAddItem);
            if (package == null)
                return MessageCode.NbParameterError;
            var result = package.AddItems(itemCode, itemCount, strength, isBinding,false, slotColorCount);
            if (result == MessageCode.Success)
            {
                bool isSuccess = package.Save();
                if (isSuccess)
                {
                    package.Shadow.Save();
                }
                return MessageCode.Success;
            }
            else
            {
                return result;
            }
        }

                 catch (Exception ex)
                 {
                     var e = ex;
                return MessageCode.Exception;
            }
                
        }

        #region 删除物品

        /// <summary>
        /// 删除物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ItemPackageDataResponse DeleteItem(Guid managerId, Guid itemId,int count=0)
        {
            var package = GetPackage(managerId,EnumTransactionType.ItemDelete);
            var code = DeleteItem(package, itemId,count);
            if (code==MessageCode.Success)
            {
                return BuildPackageResponse(package);
            }
            return ResponseHelper.Create<ItemPackageDataResponse>(code);
        }

        /// <summary>
        /// 删除物品，并保存
        /// </summary>
        /// <param name="package"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public MessageCode DeleteItem(ItemPackageFrame package, Guid itemId, int count = 0)
        {
            var code = package.Delete(itemId, count,true);
            if(code==MessageCode.Success)
            {
                if (package.Save())
                {
                    package.Shadow.Save();
                    return code;
                }
                else
                {
                    return MessageCode.NbUpdateFail;
                }
            }
            return code;
        }
        #endregion

        #region 整理背包
        /// <summary>
        /// 整理背包.
        /// </summary>
        /// <param name="managerId">The player id.</param>
        /// <returns></returns>
        public ItemPackageDataResponse Arrangepackage(Guid managerId)
        {
            var package = GetPackage(managerId,EnumTransactionType.Arrange);
            if (package == null)
                return ResponseHelper.Exception<ItemPackageDataResponse>();

            var code =package.Arrange();
            if(code!=MessageCode.Success)
                return ResponseHelper.Create<ItemPackageDataResponse>(code);
            
            if (package.Save())
            {
                package.Shadow.Save();
                return BuildPackageResponse(package);
            }
            else
            {
                return ResponseHelper.Create<ItemPackageDataResponse>(MessageCode.NbUpdateFail);
            }
        }

        #endregion

        #region SplitItem
        /// <summary>
        /// 拆分物品
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ItemPackageDataResponse SplitItem(Guid managerId, Guid itemId, int splitCount = 0)
        {
            var package = GetPackage(managerId, EnumTransactionType.SplitItem);
            var code = package.Split(itemId, splitCount);
            if (code == MessageCode.Success)
            {
                if (package.Save())
                {
                    package.Shadow.Save();
                    return BuildPackageResponse(package);
                }
                else
                {
                    return ResponseHelper.Create<ItemPackageDataResponse>(MessageCode.NbUpdateFail);
                }
            }
            else
            {
                return ResponseHelper.Create<ItemPackageDataResponse>(code);
            }           
        }
        #endregion

        #region UpdateItem
        /// <summary>
        /// 更新物品并保存
        /// </summary>
        /// <param name="package"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public MessageCode UpdateItem(ItemPackageFrame package, ItemInfoEntity item)
        {
            var code = package.Update(item);
            if (code==MessageCode.Success)
            {
                if (package.Save())
                {
                    package.Shadow.Save();
                    return code;
                }
                else
                {
                    return MessageCode.NbUpdateFail;
                }
            }
            return code;
        }
        #endregion

        #region BuildPackageData
        /// <summary>
        /// BuildPackageData
        /// </summary>
        /// <param name="package"></param>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public ItemPackageData BuildPackageData(ItemPackageFrame package, int itemType = 0)
        {
            var data = new ItemPackageData();
            data.Items = package.GetItemsByType(itemType);
            var teammember = MatchDataHelper.GetSolutionTeammembers(package.ManagerId);
            ItemPackageFrame.CaluPackageCardKpi(data.Items,teammember);
            data.PackageSize = package.PackageSize;
            return data;
        }

        #endregion

        #endregion

        #region encapsulation
        ItemPackageDataResponse BuildPackageResponse(ItemPackageFrame package, int itemType=0)
        {
            var response = ResponseHelper.CreateSuccess<ItemPackageDataResponse>();
            response.Data = BuildPackageData(package, itemType);
            return response;
        }
        #endregion
    }
}
