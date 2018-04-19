

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
    
    public partial class NbMatchstatProvider
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
		/// 将IDataReader的当前记录读取到NbMatchstatEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbMatchstatEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbMatchstatEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.MatchType = (System.Int32) reader["MatchType"];
            obj.Win = (System.Int32) reader["Win"];
            obj.Lose = (System.Int32) reader["Lose"];
            obj.Draw = (System.Int32) reader["Draw"];
            obj.Goals = (System.Int32) reader["Goals"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<NbMatchstatEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbMatchstatEntity>();
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
        public NbMatchstatProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbMatchstatProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>NbMatchstatEntity</returns>
        /// <remarks>2016/3/3 15:25:02</remarks>
        public NbMatchstatEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbMatchstat_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            NbMatchstatEntity obj=null;
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
		
		#region  GetByManagerAndType
		
		/// <summary>
        /// GetByManagerAndType
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="matchType">matchType</param>
        /// <returns>NbMatchstatEntity</returns>
        /// <remarks>2016/3/3 15:25:02</remarks>
        public NbMatchstatEntity GetByManagerAndType( System.Guid managerId, System.Int32 matchType)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MatchStat_GetByManagerAndType");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@MatchType", DbType.Int32, matchType);

            
            NbMatchstatEntity obj=null;
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
        /// <returns>NbMatchstatEntity列表</returns>
        /// <remarks>2016/3/3 15:25:02</remarks>
        public List<NbMatchstatEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbMatchstat_GetAll");
            

            
            List<NbMatchstatEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByManager
		
		/// <summary>
        /// GetByManager
        /// </summary>
        /// <returns>NbMatchstatEntity列表</returns>
        /// <remarks>2016/3/3 15:25:02</remarks>
        public List<NbMatchstatEntity> GetByManager( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MatchStat_GetByManager");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            List<NbMatchstatEntity> list = null;
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
        /// <remarks>2016/3/3 15:25:02</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbMatchstat_Delete");
            
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
		
		#region  Save
		
		/// <summary>
        /// Save
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="matchType">matchType</param>
		/// <param name="win">win</param>
		/// <param name="lose">lose</param>
		/// <param name="draw">draw</param>
		/// <param name="goals">goals</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/3/3 15:25:02</remarks>
        public bool Save ( System.Guid managerId, System.Int32 matchType, System.Int32 win, System.Int32 lose, System.Int32 draw, System.Int32 goals,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MatchStat_Save");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@MatchType", DbType.Int32, matchType);
			database.AddInParameter(commandWrapper, "@Win", DbType.Int32, win);
			database.AddInParameter(commandWrapper, "@Lose", DbType.Int32, lose);
			database.AddInParameter(commandWrapper, "@Draw", DbType.Int32, draw);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, goals);

            
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
        /// <remarks>2016/3/3 15:25:02</remarks>
        public bool Insert(NbMatchstatEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbMatchstat_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MatchType", DbType.Int32, entity.MatchType);
			database.AddInParameter(commandWrapper, "@Win", DbType.Int32, entity.Win);
			database.AddInParameter(commandWrapper, "@Lose", DbType.Int32, entity.Lose);
			database.AddInParameter(commandWrapper, "@Draw", DbType.Int32, entity.Draw);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, entity.Goals);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
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
        /// <remarks>2016/3/3 15:25:02</remarks>
        public bool Update(NbMatchstatEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbMatchstat_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@MatchType", DbType.Int32, entity.MatchType);
			database.AddInParameter(commandWrapper, "@Win", DbType.Int32, entity.Win);
			database.AddInParameter(commandWrapper, "@Lose", DbType.Int32, entity.Lose);
			database.AddInParameter(commandWrapper, "@Draw", DbType.Int32, entity.Draw);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, entity.Goals);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);

            
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

