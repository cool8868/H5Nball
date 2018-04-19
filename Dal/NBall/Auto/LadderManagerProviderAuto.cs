

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
    
    public partial class LadderManagerProvider
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
		/// 将IDataReader的当前记录读取到LadderManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LadderManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LadderManagerEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Score = (System.Int32) reader["Score"];
            obj.NewlyScore = (System.Int32) reader["NewlyScore"];
            obj.NewlyHonor = (System.Int32) reader["NewlyHonor"];
            obj.Honor = (System.Int32) reader["Honor"];
            obj.MaxScore = (System.Int32) reader["MaxScore"];
            obj.MatchTime = (System.Int32) reader["MatchTime"];
            obj.LastExchageTime = (System.DateTime) reader["LastExchageTime"];
            obj.ExchangeIds = (System.String) reader["ExchangeIds"];
            obj.ExchangedIds = (System.String) reader["ExchangedIds"];
            obj.RefreshDate = (System.DateTime) reader["RefreshDate"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
            obj.RefreshTimes = (System.Int32) reader["RefreshTimes"];
            obj.EquipmentProperties = (System.String) reader["EquipmentProperties"];
            obj.EquipmentItems = (System.String) reader["EquipmentItems"];
            obj.LadderCoin = (System.Int32) reader["LadderCoin"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<LadderManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LadderManagerEntity>();
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
        public LadderManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LadderManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="initLadderScore">initLadderScore</param>
        /// <returns>LadderManagerEntity</returns>
        /// <remarks>2016-09-07 19:07:43</remarks>
        public LadderManagerEntity GetById( System.Guid managerId, System.Int32 initLadderScore)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Ladder_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@InitLadderScore", DbType.Int32, initLadderScore);

            
            LadderManagerEntity obj=null;
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
        /// <returns>LadderManagerEntity列表</returns>
        /// <remarks>2016-09-07 19:07:43</remarks>
        public List<LadderManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LadderManager_GetAll");
            

            
            List<LadderManagerEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetBot
		
		/// <summary>
        /// GetBot
        /// </summary>
        /// <returns>LadderManagerEntity列表</returns>
        /// <remarks>2016-09-07 19:07:43</remarks>
        public List<LadderManagerEntity> GetBot( System.Int32 botNumber, System.Int32 minScore, System.Int32 maxScore)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Ladder_GetBot");
            
			database.AddInParameter(commandWrapper, "@BotNumber", DbType.Int32, botNumber);
			database.AddInParameter(commandWrapper, "@MinScore", DbType.Int32, minScore);
			database.AddInParameter(commandWrapper, "@MaxScore", DbType.Int32, maxScore);

            
            List<LadderManagerEntity> list = null;
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
        /// <remarks>2016-09-07 19:07:44</remarks>
        public bool Delete ( System.Guid managerId, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LadderManager_Delete");
            
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
		
		#region  AddHonor
		
		/// <summary>
        /// AddHonor
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="honor">honor</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-09-07 19:07:44</remarks>
        public bool AddHonor ( System.Guid managerId, System.Int32 honor,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Ladder_AddHonor");
            
			database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@honor", DbType.Int32, honor);

            
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
		
		#region  AddDailyHonor
		
		/// <summary>
        /// AddDailyHonor
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="newlyHonor">newlyHonor</param>
		/// <param name="newlyLadderCoin">newlyLadderCoin</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016-09-07 19:07:44</remarks>
        public bool AddDailyHonor ( System.Guid managerId, System.Int32 newlyHonor, System.Int32 newlyLadderCoin,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Ladder_AddDailyHonor");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@NewlyHonor", DbType.Int32, newlyHonor);
			database.AddInParameter(commandWrapper, "@NewlyLadderCoin", DbType.Int32, newlyLadderCoin);

            
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
        /// <remarks>2016-09-07 19:07:44</remarks>
        public bool Insert(LadderManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LadderManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@NewlyScore", DbType.Int32, entity.NewlyScore);
			database.AddInParameter(commandWrapper, "@NewlyHonor", DbType.Int32, entity.NewlyHonor);
			database.AddInParameter(commandWrapper, "@Honor", DbType.Int32, entity.Honor);
			database.AddInParameter(commandWrapper, "@MaxScore", DbType.Int32, entity.MaxScore);
			database.AddInParameter(commandWrapper, "@MatchTime", DbType.Int32, entity.MatchTime);
			database.AddInParameter(commandWrapper, "@LastExchageTime", DbType.DateTime, entity.LastExchageTime);
			database.AddInParameter(commandWrapper, "@ExchangeIds", DbType.AnsiString, entity.ExchangeIds);
			database.AddInParameter(commandWrapper, "@ExchangedIds", DbType.AnsiString, entity.ExchangedIds);
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.DateTime, entity.RefreshDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RefreshTimes", DbType.Int32, entity.RefreshTimes);
			database.AddInParameter(commandWrapper, "@EquipmentProperties", DbType.AnsiString, entity.EquipmentProperties);
			database.AddInParameter(commandWrapper, "@EquipmentItems", DbType.AnsiString, entity.EquipmentItems);
			database.AddInParameter(commandWrapper, "@LadderCoin", DbType.Int32, entity.LadderCoin);
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
        /// <remarks>2016-09-07 19:07:44</remarks>
        public bool Update(LadderManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LadderManager_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@NewlyScore", DbType.Int32, entity.NewlyScore);
			database.AddInParameter(commandWrapper, "@NewlyHonor", DbType.Int32, entity.NewlyHonor);
			database.AddInParameter(commandWrapper, "@Honor", DbType.Int32, entity.Honor);
			database.AddInParameter(commandWrapper, "@MaxScore", DbType.Int32, entity.MaxScore);
			database.AddInParameter(commandWrapper, "@MatchTime", DbType.Int32, entity.MatchTime);
			database.AddInParameter(commandWrapper, "@LastExchageTime", DbType.DateTime, entity.LastExchageTime);
			database.AddInParameter(commandWrapper, "@ExchangeIds", DbType.AnsiString, entity.ExchangeIds);
			database.AddInParameter(commandWrapper, "@ExchangedIds", DbType.AnsiString, entity.ExchangedIds);
			database.AddInParameter(commandWrapper, "@RefreshDate", DbType.DateTime, entity.RefreshDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);
			database.AddInParameter(commandWrapper, "@RefreshTimes", DbType.Int32, entity.RefreshTimes);
			database.AddInParameter(commandWrapper, "@EquipmentProperties", DbType.AnsiString, entity.EquipmentProperties);
			database.AddInParameter(commandWrapper, "@EquipmentItems", DbType.AnsiString, entity.EquipmentItems);
			database.AddInParameter(commandWrapper, "@LadderCoin", DbType.Int32, entity.LadderCoin);

            
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
