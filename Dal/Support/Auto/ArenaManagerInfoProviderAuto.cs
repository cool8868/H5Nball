

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
    
    public partial class ArenaManagerinfoProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Support;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到ArenaManagerinfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ArenaManagerinfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ArenaManagerinfoEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ManagerName = (System.String) reader["ManagerName"];
            obj.SiteId = (System.String) reader["SiteId"];
            obj.ZoneName = (System.String) reader["ZoneName"];
            obj.Logo = (System.String) reader["Logo"];
            obj.ChampionNumber = (System.Int32) reader["ChampionNumber"];
            obj.Integral = (System.Int32) reader["Integral"];
            obj.DanGrading = (System.Int32) reader["DanGrading"];
            obj.ArenaCoin = (System.Int32) reader["ArenaCoin"];
            obj.ArenaType = (System.Int32) reader["ArenaType"];
            obj.Stamina = (System.Int32) reader["Stamina"];
            obj.MaxStamina = (System.Int32) reader["MaxStamina"];
            obj.BuyStaminaNumber = (System.Int32) reader["BuyStaminaNumber"];
            obj.StaminaRestoreTime = (System.DateTime) reader["StaminaRestoreTime"];
            obj.Rank = (System.Int32) reader["Rank"];
            obj.Status = (System.Int32) reader["Status"];
            obj.Teammember1Status = (System.Boolean) reader["Teammember1Status"];
            obj.Teammember2Status = (System.Boolean) reader["Teammember2Status"];
            obj.Teammember3Status = (System.Boolean) reader["Teammember3Status"];
            obj.Teammember4Status = (System.Boolean) reader["Teammember4Status"];
            obj.Teammember5Status = (System.Boolean) reader["Teammember5Status"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.Opponent = (System.Byte[]) reader["Opponent"];
            obj.DomainId = (System.Int32) reader["DomainId"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ArenaManagerinfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ArenaManagerinfoEntity>();
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
        public ArenaManagerinfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ArenaManagerinfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>ArenaManagerinfoEntity</returns>
        /// <remarks>2016/9/1 13:59:49</remarks>
        public ArenaManagerinfoEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaManagerinfo_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            ArenaManagerinfoEntity obj=null;
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
		
		#region  GetChampionMax
		
		/// <summary>
        /// GetChampionMax
        /// </summary>
		/// <param name="domainId">domainId</param>
        /// <returns>ArenaManagerinfoEntity</returns>
        /// <remarks>2016/9/1 13:59:49</remarks>
        public ArenaManagerinfoEntity GetChampionMax( System.Int32 domainId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaManagerInfo_GetChampionMax");
            
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);

            
            ArenaManagerinfoEntity obj=null;
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
        /// <returns>ArenaManagerinfoEntity列表</returns>
        /// <remarks>2016/9/1 13:59:49</remarks>
        public List<ArenaManagerinfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaManagerinfo_GetAll");
            

            
            List<ArenaManagerinfoEntity> list = null;
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
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/1 13:59:49</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ArenaManagerinfo_Delete");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            
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
		
		#region  ImportRecord
		
		/// <summary>
        /// ImportRecord
        /// </summary>
		/// <param name="seasonId">seasonId</param>
		/// <param name="arenaType">arenaType</param>
		/// <param name="domainId">domainId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/1 13:59:49</remarks>
        public bool ImportRecord ( System.Int32 seasonId, System.Int32 arenaType, System.Int32 domainId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaManagerInfo_ImportRecord");
            
			database.AddInParameter(commandWrapper, "@SeasonId", DbType.Int32, seasonId);
			database.AddInParameter(commandWrapper, "@ArenaType", DbType.Int32, arenaType);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);

            
            
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
		
		#region  AddArenaCoin
		
		/// <summary>
        /// AddArenaCoin
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="arenaCoin">arenaCoin</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/1 13:59:49</remarks>
        public bool AddArenaCoin ( System.Guid managerId, System.Int32 arenaCoin,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaManagerInfo_AddArenaCoin");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ArenaCoin", DbType.Int32, arenaCoin);

            
            
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
		
		#region  SetChampion
		
		/// <summary>
        /// SetChampion
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/1 13:59:50</remarks>
        public bool SetChampion ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaManagerInfo_SetChampion");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            
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
		
		#region  ClearRecord
		
		/// <summary>
        /// ClearRecord
        /// </summary>
		/// <param name="arenaType">arenaType</param>
		/// <param name="domainId">domainId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/1 13:59:50</remarks>
        public bool ClearRecord ( System.Int32 arenaType, System.Int32 domainId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaManagerInfo_ClearRecord");
            
			database.AddInParameter(commandWrapper, "@ArenaType", DbType.Int32, arenaType);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);

            
            
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
		
		#region  SetRank
		
		/// <summary>
        /// SetRank
        /// </summary>
		/// <param name="domainId">domainId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/9/1 13:59:50</remarks>
        public bool SetRank ( System.Int32 domainId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaManagerInfo_SetRank");
            
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);

            
            
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
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/9/1 13:59:50</remarks>
        public bool Insert(ArenaManagerinfoEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/9/1 13:59:50</remarks>
        public bool Insert(ArenaManagerinfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ArenaManagerinfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.AnsiString, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.AnsiString, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, entity.Logo);
			database.AddInParameter(commandWrapper, "@ChampionNumber", DbType.Int32, entity.ChampionNumber);
			database.AddInParameter(commandWrapper, "@Integral", DbType.Int32, entity.Integral);
			database.AddInParameter(commandWrapper, "@DanGrading", DbType.Int32, entity.DanGrading);
			database.AddInParameter(commandWrapper, "@ArenaCoin", DbType.Int32, entity.ArenaCoin);
			database.AddInParameter(commandWrapper, "@ArenaType", DbType.Int32, entity.ArenaType);
			database.AddInParameter(commandWrapper, "@Stamina", DbType.Int32, entity.Stamina);
			database.AddInParameter(commandWrapper, "@MaxStamina", DbType.Int32, entity.MaxStamina);
			database.AddInParameter(commandWrapper, "@BuyStaminaNumber", DbType.Int32, entity.BuyStaminaNumber);
			database.AddInParameter(commandWrapper, "@StaminaRestoreTime", DbType.DateTime, entity.StaminaRestoreTime);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Teammember1Status", DbType.Boolean, entity.Teammember1Status);
			database.AddInParameter(commandWrapper, "@Teammember2Status", DbType.Boolean, entity.Teammember2Status);
			database.AddInParameter(commandWrapper, "@Teammember3Status", DbType.Boolean, entity.Teammember3Status);
			database.AddInParameter(commandWrapper, "@Teammember4Status", DbType.Boolean, entity.Teammember4Status);
			database.AddInParameter(commandWrapper, "@Teammember5Status", DbType.Boolean, entity.Teammember5Status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@Opponent", DbType.Binary, entity.Opponent);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/9/1 13:59:50</remarks>
        public bool Update(ArenaManagerinfoEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/9/1 13:59:50</remarks>
        public bool Update(ArenaManagerinfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ArenaManagerinfo_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.AnsiString, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@SiteId", DbType.AnsiString, entity.SiteId);
			database.AddInParameter(commandWrapper, "@ZoneName", DbType.AnsiString, entity.ZoneName);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, entity.Logo);
			database.AddInParameter(commandWrapper, "@ChampionNumber", DbType.Int32, entity.ChampionNumber);
			database.AddInParameter(commandWrapper, "@Integral", DbType.Int32, entity.Integral);
			database.AddInParameter(commandWrapper, "@DanGrading", DbType.Int32, entity.DanGrading);
			database.AddInParameter(commandWrapper, "@ArenaCoin", DbType.Int32, entity.ArenaCoin);
			database.AddInParameter(commandWrapper, "@ArenaType", DbType.Int32, entity.ArenaType);
			database.AddInParameter(commandWrapper, "@Stamina", DbType.Int32, entity.Stamina);
			database.AddInParameter(commandWrapper, "@MaxStamina", DbType.Int32, entity.MaxStamina);
			database.AddInParameter(commandWrapper, "@BuyStaminaNumber", DbType.Int32, entity.BuyStaminaNumber);
			database.AddInParameter(commandWrapper, "@StaminaRestoreTime", DbType.DateTime, entity.StaminaRestoreTime);
			database.AddInParameter(commandWrapper, "@Rank", DbType.Int32, entity.Rank);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@Teammember1Status", DbType.Boolean, entity.Teammember1Status);
			database.AddInParameter(commandWrapper, "@Teammember2Status", DbType.Boolean, entity.Teammember2Status);
			database.AddInParameter(commandWrapper, "@Teammember3Status", DbType.Boolean, entity.Teammember3Status);
			database.AddInParameter(commandWrapper, "@Teammember4Status", DbType.Boolean, entity.Teammember4Status);
			database.AddInParameter(commandWrapper, "@Teammember5Status", DbType.Boolean, entity.Teammember5Status);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@Opponent", DbType.Binary, entity.Opponent);
			database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, entity.DomainId);

            
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
