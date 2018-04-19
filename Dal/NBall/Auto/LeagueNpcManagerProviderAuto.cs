

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
    
    public partial class LeagueNpcmanagerProvider
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
		/// 将IDataReader的当前记录读取到LeagueNpcmanagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public LeagueNpcmanagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new LeagueNpcmanagerEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.LaegueRecordId = (System.Guid) reader["LaegueRecordId"];
            obj.NpcId = (System.Guid) reader["NpcId"];
            obj.NpcName = (System.String) reader["NpcName"];
            obj.Score = (System.Int32) reader["Score"];
            obj.MatchNumber = (System.Int32) reader["MatchNumber"];
            obj.WinNumber = (System.Int32) reader["WinNumber"];
            obj.FlatNumber = (System.Int32) reader["FlatNumber"];
            obj.LoseNumber = (System.Int32) reader["LoseNumber"];
            obj.GoalsNumber = (System.Int32) reader["GoalsNumber"];
            obj.FumbleNumber = (System.Int32) reader["FumbleNumber"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<LeagueNpcmanagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<LeagueNpcmanagerEntity>();
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
        public LeagueNpcmanagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public LeagueNpcmanagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>LeagueNpcmanagerEntity</returns>
        /// <remarks>2016/1/4 13:48:50</remarks>
        public LeagueNpcmanagerEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueNpcmanager_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            LeagueNpcmanagerEntity obj=null;
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
		
		#region  GetNpcManager
		
		/// <summary>
        /// GetNpcManager
        /// </summary>
		/// <param name="npcId">npcId</param>
		/// <param name="leagueRecordId">leagueRecordId</param>
        /// <returns>LeagueNpcmanagerEntity</returns>
        /// <remarks>2016/1/4 13:48:50</remarks>
        public LeagueNpcmanagerEntity GetNpcManager( System.Guid npcId, System.Guid leagueRecordId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_League_GetNpcManager");
            
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, npcId);
			database.AddInParameter(commandWrapper, "@LeagueRecordId", DbType.Guid, leagueRecordId);

            
            LeagueNpcmanagerEntity obj=null;
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
        /// <returns>LeagueNpcmanagerEntity列表</returns>
        /// <remarks>2016/1/4 13:48:50</remarks>
        public List<LeagueNpcmanagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueNpcmanager_GetAll");
            

            
            List<LeagueNpcmanagerEntity> list = null;
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
		/// <param name="idx">idx</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/1/4 13:48:50</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_LeagueNpcmanager_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
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
        /// <remarks>2016/1/4 13:48:50</remarks>
        public bool Insert(LeagueNpcmanagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueNpcmanager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@LaegueRecordId", DbType.Guid, entity.LaegueRecordId);
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, entity.NpcId);
			database.AddInParameter(commandWrapper, "@NpcName", DbType.AnsiString, entity.NpcName);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@MatchNumber", DbType.Int32, entity.MatchNumber);
			database.AddInParameter(commandWrapper, "@WinNumber", DbType.Int32, entity.WinNumber);
			database.AddInParameter(commandWrapper, "@FlatNumber", DbType.Int32, entity.FlatNumber);
			database.AddInParameter(commandWrapper, "@LoseNumber", DbType.Int32, entity.LoseNumber);
			database.AddInParameter(commandWrapper, "@GoalsNumber", DbType.Int32, entity.GoalsNumber);
			database.AddInParameter(commandWrapper, "@FumbleNumber", DbType.Int32, entity.FumbleNumber);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
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
        /// <remarks>2016/1/4 13:48:50</remarks>
        public bool Update(LeagueNpcmanagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_LeagueNpcmanager_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@LaegueRecordId", DbType.Guid, entity.LaegueRecordId);
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, entity.NpcId);
			database.AddInParameter(commandWrapper, "@NpcName", DbType.AnsiString, entity.NpcName);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@MatchNumber", DbType.Int32, entity.MatchNumber);
			database.AddInParameter(commandWrapper, "@WinNumber", DbType.Int32, entity.WinNumber);
			database.AddInParameter(commandWrapper, "@FlatNumber", DbType.Int32, entity.FlatNumber);
			database.AddInParameter(commandWrapper, "@LoseNumber", DbType.Int32, entity.LoseNumber);
			database.AddInParameter(commandWrapper, "@GoalsNumber", DbType.Int32, entity.GoalsNumber);
			database.AddInParameter(commandWrapper, "@FumbleNumber", DbType.Int32, entity.FumbleNumber);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

