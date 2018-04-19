using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Dal.Xsd;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Shadow;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Games.NBall.Dal.Shadow
{
    public class ShadowProvider
    {
        private const EnumDbType DBTYPE = EnumDbType.Shadow;
        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";

        public ShadowProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ShadowProvider(string zoneId)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneId, DBTYPE);
        }

        #region Item
        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/1/2 17:25:02</remarks>
        public bool SaveItem(ShadowItemEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Item_Insert");


            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@OperationType", DbType.Int32, entity.OperationType);
            database.AddInParameter(commandWrapper, "@ItemId", DbType.Guid, entity.ItemId);
            database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
            database.AddInParameter(commandWrapper, "@ItemCount", DbType.Int32, entity.ItemCount);
            database.AddInParameter(commandWrapper, "@ItemType", DbType.Int32, entity.ItemType);
            database.AddInParameter(commandWrapper, "@IsBinding", DbType.Boolean, entity.IsBinding);
            database.AddInParameter(commandWrapper, "@ItemProperty", DbType.Binary, entity.ItemProperty);
            database.AddInParameter(commandWrapper, "@GridIndex", DbType.Int32, entity.GridIndex);
            database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
            database.AddInParameter(commandWrapper, "@OperationCount", DbType.Int32, entity.OperationCount);
            database.AddParameter(commandWrapper, "@Idx", DbType.Int64, ParameterDirection.InputOutput, "", DataRowVersion.Current, entity.Idx);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx = (System.Int64)database.GetParameterValue(commandWrapper, "@Idx");

            return Convert.ToBoolean(results);
        }
        #endregion

        #region SaveItemBulk
        public void SaveItemBulk(List<ShadowItemEntity> list, DbTransaction trans = null)
        {
            ItemShadowDataSet.ItemDataTable itemData = new ItemShadowDataSet.ItemDataTable();
            foreach (var entity in list)
            {
                var row = itemData.NewRow();
                row["TransactionId"] = entity.TransactionId;
                row["OperationType"] = entity.OperationType;
                row["ItemId"] = entity.ItemId;
                row["ItemCode"] = entity.ItemCode;
                row["ItemCount"] = entity.ItemCount;
                row["ItemType"] = entity.ItemType;
                row["IsBinding"] = entity.IsBinding;
                row["ItemProperty"] = entity.ItemProperty;
                row["GridIndex"] = entity.GridIndex;
                row["Status"] = entity.Status;
                itemData.Rows.Add(row);
            }
            SqlBatchHelper.BulkInsert(this.ConnectionString, itemData);
        }
        #endregion


        #region Teammember
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/1/2 17:28:29</remarks>
        public bool SaveTeammember(ShadowTeammemberEntity entity)
        {
            return SaveTeammember(entity, null);
        }

        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/1/2 17:28:29</remarks>
        public bool SaveTeammember(ShadowTeammemberEntity entity, DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Teammember_Insert");


            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@OperationType", DbType.Int32, entity.OperationType);
            database.AddInParameter(commandWrapper, "@TeammemberId", DbType.Guid, entity.TeammemberId);
            database.AddInParameter(commandWrapper, "@PlayerId", DbType.Int32, entity.PlayerId);
            database.AddParameter(commandWrapper, "@Idx", DbType.Int64, ParameterDirection.InputOutput, "", DataRowVersion.Current, entity.Idx);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx = (System.Int64)database.GetParameterValue(commandWrapper, "@Idx");

            return Convert.ToBoolean(results);
        }
        #endregion

        #region Transaction
        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/1/2 17:29:22</remarks>
        public bool SaveTransaction(ShadowTransactionEntity entity)
        {
            return SaveTransaction(entity, null);
        }

        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/1/2 17:29:22</remarks>
        public bool SaveTransaction(ShadowTransactionEntity entity, DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Transaction_Insert");


            database.AddInParameter(commandWrapper, "@TransactionType", DbType.Int32, entity.TransactionType);
            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
            database.AddInParameter(commandWrapper, "@AppId", DbType.AnsiString, entity.AppId);
            database.AddInParameter(commandWrapper, "@TerminalIP", DbType.AnsiString, entity.TerminalIP);
            database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
            database.AddParameter(commandWrapper, "@Idx", DbType.Guid, ParameterDirection.InputOutput, "", DataRowVersion.Current, entity.Idx);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx = (System.Guid)database.GetParameterValue(commandWrapper, "@Idx");

            return Convert.ToBoolean(results);
        }
        #endregion

        #region Item_Package
        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/3/22 21:54:18</remarks>
        public bool SaveItemPackage(ShadowItemPackageEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ItemPackage_Insert");

            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@PackageSize", DbType.Int32, entity.PackageSize);
            database.AddInParameter(commandWrapper, "@ItemVersion", DbType.Byte, entity.ItemVersion);
            database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, entity.ItemString);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            return Convert.ToBoolean(results);
        }



        #endregion

        #region SavePandoraDecompose
        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/3/23 22:11:08</remarks>
        public bool SavePandoraDecompose(ShadowPandoraDecomposeEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraDecompose_Insert");

            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@ItemIds", DbType.AnsiString, entity.ItemIds);
            database.AddInParameter(commandWrapper, "@ItemCodes", DbType.AnsiString, entity.ItemCodes);
            database.AddInParameter(commandWrapper, "@CritRate", DbType.Int32, entity.CritRate);
            database.AddInParameter(commandWrapper, "@IsCrit", DbType.Boolean, entity.IsCrit);
            database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, entity.Coin);
            database.AddInParameter(commandWrapper, "@EquipmentList", DbType.AnsiString, entity.EquipmentList);
            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }


            return Convert.ToBoolean(results);
        }
        #endregion

        #region SavePandoraEquipmentwash
        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/3/22 22:05:15</remarks>
        public bool SavePandoraEquipmentwash(ShadowPandoraEquipmentwashEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraEquipmentwash_Insert");

            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@ItemId", DbType.Guid, entity.ItemId);
            database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
            database.AddInParameter(commandWrapper, "@LockPropertyId", DbType.Int32, entity.LockPropertyId);
            database.AddInParameter(commandWrapper, "@BuyStone", DbType.Boolean, entity.BuyStone);
            database.AddInParameter(commandWrapper, "@BuyFusogen", DbType.Boolean, entity.BuyFusogen);
            database.AddInParameter(commandWrapper, "@CostPoint", DbType.Int32, entity.CostPoint);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }


            return Convert.ToBoolean(results);
        }
        #endregion

        #region SavePandoraEquipmentUpgrade
        public bool SavePandoraEquipmentUpgrade(ShadowPandoraEquipmentUpgradeEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraEquipmentUpgrade_Insert");

            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
            database.AddInParameter(commandWrapper, "@ItemId", DbType.Guid, entity.ItemId);
            database.AddInParameter(commandWrapper, "@CurLevel", DbType.Int32, entity.CurLevel);
            database.AddInParameter(commandWrapper, "@ResultLevle", DbType.Int32, entity.ResultLevel);
            database.AddInParameter(commandWrapper, "@Properties", DbType.String, entity.Properties);
            database.AddInParameter(commandWrapper, "@CostCoin", DbType.Int32, entity.CostCoin);

            int results = 0;
            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }


            return Convert.ToBoolean(results);
        }
        #endregion

        #region SavePandoraEquipmentSell
        public bool SavePandoraEquipmentSell(ShadowPandoraEquipmentSellEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraEquipmentSell_Insert");

            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
            database.AddInParameter(commandWrapper, "@ItemId", DbType.Guid, entity.ItemId);
            database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
            database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, entity.Coin);

            int results = 0;
            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }


            return Convert.ToBoolean(results);
        }
        #endregion

        #region SavePandoraEquipmentPrecisionCasting

        public bool SavePandoraEquipmentPrecisionCasting(ShadowPandoraEquipmentPrecisionCastingEntity entity,
            DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraEquipmentPrecisionCasting_Insert");

            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
            database.AddInParameter(commandWrapper, "@ItemId", DbType.Guid, entity.ItemId);
            database.AddInParameter(commandWrapper, "@LockCondition", DbType.String, entity.LockCondition);
            database.AddInParameter(commandWrapper, "@ExProperties", DbType.String, entity.ExProperties);
            database.AddInParameter(commandWrapper, "@CurProperties", DbType.String, entity.CurProperties);
            database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, entity.Coin);
            database.AddInParameter(commandWrapper, "@Point", DbType.Int32, entity.Point);

            int results = 0;
            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            return Convert.ToBoolean(results);
        }
        #endregion

        #region SavePandoraArousal

        public bool SavePandoraArousal(ShadowPandoraArousalEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraArousal_Insert");

            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
            database.AddInParameter(commandWrapper, "@SourceCard", DbType.Guid, entity.SourceCard);
            database.AddInParameter(commandWrapper, "@SourceCardLv", DbType.Int32, entity.SourceCardLv);
            database.AddInParameter(commandWrapper, "@CurArousalLv", DbType.Int32, entity.CurArousalLv);
            database.AddInParameter(commandWrapper, "@TarArousalLv", DbType.Int32, entity.TarArousalLv);
            database.AddInParameter(commandWrapper, "@UseCard", DbType.Guid, entity.UseCard);
            database.AddInParameter(commandWrapper, "@UseCardStrenth", DbType.Int32, entity.UseCardStrenth);
            database.AddInParameter(commandWrapper, "@IsBinding", DbType.Boolean, entity.IsBinding);

            int results = 0;
            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            return Convert.ToBoolean(results);
        }

        #endregion
        #region SavePandoraMedal
        public bool SavePandoraMedal(ShadowPandoraMedalEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraMedal_Insert");
            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
            database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
            database.AddInParameter(commandWrapper, "@ItemIds", DbType.String, entity.ItemIds);
            database.AddInParameter(commandWrapper, "@ItemCodes", DbType.String, entity.ItemCodes);
            database.AddInParameter(commandWrapper, "@MedalCount", DbType.Int32, entity.MedalCount);
            int results = 0;
            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            return Convert.ToBoolean(results);
        }
        #endregion


        #region SavePandoraMosaic
        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/3/22 22:05:29</remarks>
        public bool SavePandoraMosaic(ShadowPandoraMosaicEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraMosaic_Insert");

            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@ItemId", DbType.Guid, entity.ItemId);
            database.AddInParameter(commandWrapper, "@ItemCode", DbType.Int32, entity.ItemCode);
            database.AddInParameter(commandWrapper, "@SlotId", DbType.Int32, entity.SlotId);
            database.AddInParameter(commandWrapper, "@BallsoulId", DbType.Guid, entity.BallsoulId);
            database.AddInParameter(commandWrapper, "@BallsoulItemCode", DbType.Int32, entity.BallsoulItemCode);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }


            return Convert.ToBoolean(results);
        }
        #endregion

        #region SavePandoraStrength
        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/3/23 22:11:34</remarks>
        public bool SavePandoraStrength(ShadowPandoraStrengthEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraStrength_Insert");


            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@ItemId1", DbType.Guid, entity.ItemId1);
            database.AddInParameter(commandWrapper, "@ItemCode1", DbType.Int32, entity.ItemCode1);
            database.AddInParameter(commandWrapper, "@Strength1", DbType.Int32, entity.Strength1);
            database.AddInParameter(commandWrapper, "@ItemId2", DbType.Guid, entity.ItemId2);
            database.AddInParameter(commandWrapper, "@ItemCode2", DbType.Int32, entity.ItemCode2);
            database.AddInParameter(commandWrapper, "@Strength2", DbType.Int32, entity.Strength2);
            database.AddInParameter(commandWrapper, "@IsProtect", DbType.Boolean, entity.IsProtect);
            database.AddInParameter(commandWrapper, "@CostCoin", DbType.Int32, entity.CostCoin);
            database.AddInParameter(commandWrapper, "@CostPoint", DbType.Int32, entity.CostPoint);
            database.AddInParameter(commandWrapper, "@ResultType", DbType.Int32, entity.ResultType);
            database.AddInParameter(commandWrapper, "@ResultItemId", DbType.Guid, entity.ResultItemId);
            database.AddInParameter(commandWrapper, "@ResultItemCode", DbType.Int32, entity.ResultItemCode);
            database.AddInParameter(commandWrapper, "@ResultStrength", DbType.Int32, entity.ResultStrength);
            database.AddInParameter(commandWrapper, "@LuckyItemId", DbType.Guid, entity.LuckyItemId);
            database.AddInParameter(commandWrapper, "@LuckyItemCode", DbType.Int32, entity.LuckyItemCode);
            database.AddInParameter(commandWrapper, "@Rate", DbType.Decimal, entity.Rate);
            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }


            return Convert.ToBoolean(results);
        }
        #endregion

        #region SavePandoraSynthesis

        /// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/3/22 22:05:56</remarks>
        public bool SavePandoraSynthesis(ShadowPandoraSynthesisEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_PandoraSynthesis_Insert");

            database.AddInParameter(commandWrapper, "@TransactionId", DbType.Guid, entity.TransactionId);
            database.AddInParameter(commandWrapper, "@SynthesisType", DbType.Int32, entity.SynthesisType);
            database.AddInParameter(commandWrapper, "@ItemId1", DbType.Guid, entity.ItemId1);
            database.AddInParameter(commandWrapper, "@ItemCode1", DbType.Int32, entity.ItemCode1);
            database.AddInParameter(commandWrapper, "@ItemId2", DbType.Guid, entity.ItemId2);
            database.AddInParameter(commandWrapper, "@ItemCode2", DbType.Int32, entity.ItemCode2);
            database.AddInParameter(commandWrapper, "@ItemId3", DbType.Guid, entity.ItemId3);
            database.AddInParameter(commandWrapper, "@ItemCode3", DbType.Int32, entity.ItemCode3);
            database.AddInParameter(commandWrapper, "@ItemId4", DbType.Guid, entity.ItemId4);
            database.AddInParameter(commandWrapper, "@ItemCode4", DbType.Int32, entity.ItemCode4);
            database.AddInParameter(commandWrapper, "@ItemId5", DbType.Guid, entity.ItemId5);
            database.AddInParameter(commandWrapper, "@ItemCode5", DbType.Int32, entity.ItemCode5);
            database.AddInParameter(commandWrapper, "@SuitdrawingId", DbType.Guid, entity.SuitdrawingId);
            database.AddInParameter(commandWrapper, "@SuitdrawingItemCode", DbType.Int32, entity.SuitdrawingItemCode);
            database.AddInParameter(commandWrapper, "@IsProtect", DbType.Boolean, entity.IsProtect);
            database.AddInParameter(commandWrapper, "@CostCoin", DbType.Int32, entity.CostCoin);
            database.AddInParameter(commandWrapper, "@CostPoint", DbType.Int32, entity.CostPoint);
            database.AddInParameter(commandWrapper, "@ResultType", DbType.Int32, entity.ResultType);
            database.AddInParameter(commandWrapper, "@ResultItemId", DbType.Guid, entity.ResultItemId);
            database.AddInParameter(commandWrapper, "@ResultItemCode", DbType.Int32, entity.ResultItemCode);
            database.AddInParameter(commandWrapper, "@LuckyItemId", DbType.Guid, entity.LuckyItemId);
            database.AddInParameter(commandWrapper, "@LuckyItemCode", DbType.Int32, entity.LuckyItemCode);
            database.AddInParameter(commandWrapper, "@GoldFormulaItemId", DbType.Guid, entity.GoldFormulaItemId);
            database.AddInParameter(commandWrapper, "@GoldFormulaItemCode", DbType.Int32, entity.GoldFormulaItemCode);
            database.AddInParameter(commandWrapper, "@Rate", DbType.Decimal, entity.Rate);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }


            return Convert.ToBoolean(results);
        }
        #endregion

        #region SaveCoinConsume
        /// <summary>
        /// 
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coin"></param>
        /// <param name="sourceType"></param>
        /// <param name="orderId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool SaveCoinConsume(Guid managerId, int coin, int sourceType, string orderId, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CoinConsumeHistory_Insert");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, coin);
            database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
            database.AddInParameter(commandWrapper, "@SourceId", DbType.String, orderId);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }


            return Convert.ToBoolean(results);
        }
        #endregion

        #region SaveCoinCharge
        /// <summary>
        /// 
        /// </summary>
        /// <param name="managerId"></param>
        /// <param name="coin"></param>
        /// <param name="sourceType"></param>
        /// <param name="orderId"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public bool SaveCoinCharge(Guid managerId, int coin, int exp, bool isLevelup, int level, int sourceType, string orderId, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_CoinChargeHistory_Insert");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, coin);
            database.AddInParameter(commandWrapper, "@Exp", DbType.Int32, exp);
            database.AddInParameter(commandWrapper, "@IsLevelup", DbType.Boolean, isLevelup);
            database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);
            database.AddInParameter(commandWrapper, "@SourceType", DbType.Int32, sourceType);
            database.AddInParameter(commandWrapper, "@SourceId", DbType.String, orderId);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }


            return Convert.ToBoolean(results);
        }
        #endregion

        #region SaveCoinCharge

        public bool SaveOnlineHistory(OnlineInfoEntity entity, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_OnlineHistory_Insert");


            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
            database.AddInParameter(commandWrapper, "@LoginTime", DbType.DateTime, entity.LoginTime);
            database.AddInParameter(commandWrapper, "@GuildInTime", DbType.DateTime, entity.GuildInTime);
            database.AddInParameter(commandWrapper, "@ActiveTime", DbType.DateTime, entity.ActiveTime);
            database.AddInParameter(commandWrapper, "@CntOnlineMinutes", DbType.Int32, entity.CntOnlineMinutes);
            database.AddInParameter(commandWrapper, "@CurOnlineMinutes", DbType.Int32, entity.CurOnlineMinutes);
            database.AddInParameter(commandWrapper, "@LoginIp", DbType.AnsiString, entity.LoginIp);
            database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
            database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, DateTime.Now);
            long idx = 0;
            database.AddParameter(commandWrapper, "@Idx", DbType.Int64, ParameterDirection.InputOutput, "", DataRowVersion.Current, idx);

            int results = 0;

            if (trans != null)
            {
                results = database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            idx = (System.Int64)database.GetParameterValue(commandWrapper, "@Idx");

            return Convert.ToBoolean(results);
        }
        #endregion

        #region CoinStat
        public void CoinStat(Guid managerId, ref int chargeCoin, ref int consumeCoin)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Coin_Stat");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddParameter(commandWrapper, "@ChargeCoin", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, chargeCoin);
            database.AddParameter(commandWrapper, "@ConsumeCoin", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, consumeCoin);

            database.ExecuteNonQuery(commandWrapper);


            chargeCoin = (System.Int32)database.GetParameterValue(commandWrapper, "@ChargeCoin");
            consumeCoin = (System.Int32)database.GetParameterValue(commandWrapper, "@ConsumeCoin");
        }
        #endregion
        #region CoinAllStat
        public void CoinAllStat(DateTime  startTime,DateTime endTime, ref int chargeCoin, ref int consumeCoin)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Coin_AllStat");

            database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
            database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, startTime);
            database.AddParameter(commandWrapper, "@ChargeCoin", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, chargeCoin);
            database.AddParameter(commandWrapper, "@ConsumeCoin", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, consumeCoin);

            database.ExecuteNonQuery(commandWrapper);


            chargeCoin = (System.Int32)database.GetParameterValue(commandWrapper, "@ChargeCoin");
            consumeCoin = (System.Int32)database.GetParameterValue(commandWrapper, "@ConsumeCoin");
        }
        #endregion
    }
}
