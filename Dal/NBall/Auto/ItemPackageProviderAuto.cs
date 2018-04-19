

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Dal
{
    
    public partial class ItemPackageProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Main;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到ItemPackageEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ItemPackageEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ItemPackageEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.PackageSize = (System.Int32) reader["PackageSize"];
            obj.ItemVersion = (System.Byte) reader["ItemVersion"];
            obj.ItemString = (System.Byte[]) reader["ItemString"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ItemPackageEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ItemPackageEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow(reader));
            }
            return clt;
        }
        #endregion
        
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ItemPackageProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ItemPackageProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>ItemPackageEntity</returns>
        /// <remarks>2016/1/19 10:29:03</remarks>
        public ItemPackageEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ItemPackage_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            ItemPackageEntity obj=null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                if(reader.Read())
                {
                    
            
                    obj = LoadSingleRow(reader);
                }
            }
            return obj;
        }
		
		#endregion		  
		
		#region  GetAll
		
		/// <summary>
        /// GetAll
        /// </summary>
        /// <returns>ItemPackageEntity列表</returns>
        /// <remarks>2016/1/19 10:29:03</remarks>
        public List<ItemPackageEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ItemPackage_GetAll");
            

            
            List<ItemPackageEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/19 10:29:04</remarks>
        public bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ItemPackage_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Update
		
		/// <summary>
        /// Update
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="itemString">itemString</param>
		/// <param name="rowVersion">rowVersion</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/19 10:29:04</remarks>
        public bool Update ( System.Guid managerId, System.Byte[] itemString, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ItemPackage_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, itemString);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  AddPlayer
		
		/// <summary>
        /// AddPlayer
        /// </summary>
		/// <param name="teammemberId">teammemberId</param>
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/19 10:29:04</remarks>
        public bool AddPlayer ( System.Guid teammemberId, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_AddPlayer");
            
			database.AddInParameter(commandWrapper, "@TeammemberId", DbType.Guid, teammemberId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  DeletePlayer
		
		/// <summary>
        /// DeletePlayer
        /// </summary>
		/// <param name="playerId">playerId</param>
		/// <param name="managerId">managerId</param>
		/// <param name="teammemberId">teammemberId</param>
		/// <param name="strengthenLevel">strengthenLevel</param>
		/// <param name="usedPlayerCard">usedPlayerCard</param>
		/// <param name="usedEquipment">usedEquipment</param>
		/// <param name="level">level</param>
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/19 10:29:04</remarks>
        public bool DeletePlayer ( System.Int32 playerId, System.Guid managerId, System.Guid teammemberId, System.Int32 strengthenLevel, System.Byte[] usedPlayerCard, System.Byte[] usedEquipment, System.Int32 level, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_DeletePlayer");
            
			database.AddInParameter(commandWrapper, "@playerId", DbType.Int32, playerId);
			database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@TeammemberId", DbType.Guid, teammemberId);
			database.AddInParameter(commandWrapper, "@StrengthenLevel", DbType.Int32, strengthenLevel);
			database.AddInParameter(commandWrapper, "@UsedPlayerCard", DbType.Binary, usedPlayerCard);
			database.AddInParameter(commandWrapper, "@UsedEquipment", DbType.Binary, usedEquipment);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Delete5Player
		
		/// <summary>
        /// Delete5Player
        /// </summary>
		/// <param name="teammemberId1">teammemberId1</param>
		/// <param name="teammemberId2">teammemberId2</param>
		/// <param name="teammemberId3">teammemberId3</param>
		/// <param name="teammemberId4">teammemberId4</param>
		/// <param name="teammemberId5">teammemberId5</param>
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/19 10:29:04</remarks>
        public bool Delete5Player ( System.Guid teammemberId1, System.Guid teammemberId2, System.Guid teammemberId3, System.Guid teammemberId4, System.Guid teammemberId5, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_Delete5Player");
            
			database.AddInParameter(commandWrapper, "@TeammemberId1", DbType.Guid, teammemberId1);
			database.AddInParameter(commandWrapper, "@TeammemberId2", DbType.Guid, teammemberId2);
			database.AddInParameter(commandWrapper, "@TeammemberId3", DbType.Guid, teammemberId3);
			database.AddInParameter(commandWrapper, "@TeammemberId4", DbType.Guid, teammemberId4);
			database.AddInParameter(commandWrapper, "@TeammemberId5", DbType.Guid, teammemberId5);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/19 10:29:04</remarks>
        public bool Insert(ItemPackageEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ItemPackage_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@PackageSize", DbType.Int32, entity.PackageSize);
			database.AddInParameter(commandWrapper, "@ItemVersion", DbType.Byte, entity.ItemVersion);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, entity.ItemString);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@ManagerId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.ManagerId);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/19 10:29:04</remarks>
        public bool Update(ItemPackageEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ItemPackage_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@PackageSize", DbType.Int32, entity.PackageSize);
			database.AddInParameter(commandWrapper, "@ItemVersion", DbType.Byte, entity.ItemVersion);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, entity.ItemString);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

