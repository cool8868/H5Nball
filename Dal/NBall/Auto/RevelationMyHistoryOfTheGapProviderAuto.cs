

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
    
    public partial class RevelationMyhistoryofthegapProvider
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
		/// 将IDataReader的当前记录读取到RevelationMyhistoryofthegapEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public RevelationMyhistoryofthegapEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new RevelationMyhistoryofthegapEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Mark = (System.Int32) reader["Mark"];
            obj.Schedule = (System.Int32) reader["Schedule"];
            obj.Goals = (System.Int32) reader["Goals"];
            obj.ToConcede = (System.Int32) reader["ToConcede"];
            obj.HistoryOfTheGap = (System.Int32) reader["HistoryOfTheGap"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<RevelationMyhistoryofthegapEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<RevelationMyhistoryofthegapEntity>();
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
        public RevelationMyhistoryofthegapProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public RevelationMyhistoryofthegapProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>RevelationMyhistoryofthegapEntity</returns>
        /// <remarks>2014/11/15 0:07:19</remarks>
        public RevelationMyhistoryofthegapEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationMyhistoryofthegap_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            RevelationMyhistoryofthegapEntity obj=null;
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
		
		#region  GetMyMarkHistoryOfTheGap
		
		/// <summary>
        /// GetMyMarkHistoryOfTheGap
        /// </summary>
		/// <param name="managerid">managerid</param>
		/// <param name="mark">mark</param>
		/// <param name="littleLevels">littleLevels</param>
        /// <returns>RevelationMyhistoryofthegapEntity</returns>
        /// <remarks>2014/11/15 0:07:19</remarks>
        public RevelationMyhistoryofthegapEntity GetMyMarkHistoryOfTheGap( System.Guid managerid, System.Int32 mark, System.Int32 littleLevels)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Revelation_GetMyMarkHistoryOfTheGap");
            
			database.AddInParameter(commandWrapper, "@managerid", DbType.Guid, managerid);
			database.AddInParameter(commandWrapper, "@mark", DbType.Int32, mark);
			database.AddInParameter(commandWrapper, "@littleLevels", DbType.Int32, littleLevels);

            
            RevelationMyhistoryofthegapEntity obj=null;
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
        /// <returns>RevelationMyhistoryofthegapEntity列表</returns>
        /// <remarks>2014/11/15 0:07:19</remarks>
        public List<RevelationMyhistoryofthegapEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationMyhistoryofthegap_GetAll");
            

            
            List<RevelationMyhistoryofthegapEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetMyHistoryOfTheGap
		
		/// <summary>
        /// GetMyHistoryOfTheGap
        /// </summary>
        /// <returns>RevelationMyhistoryofthegapEntity列表</returns>
        /// <remarks>2014/11/15 0:07:19</remarks>
        public List<RevelationMyhistoryofthegapEntity> GetMyHistoryOfTheGap( System.Guid managerId, System.Int32 mark)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Revelation_GetMyHistoryOfTheGap");
            
			database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@mark", DbType.Int32, mark);

            
            List<RevelationMyhistoryofthegapEntity> list = null;
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
        /// <remarks>2014/11/15 0:07:19</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationMyhistoryofthegap_Delete");
            
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
		
		#region  C_RevelationEverDay
		
		/// <summary>
        /// C_RevelationEverDay
        /// </summary>
		/// <param name="managerid">managerid</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/11/15 0:07:19</remarks>
        public bool C_RevelationEverDay ( System.Guid managerid,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_RevelationEverDay");
            
			database.AddInParameter(commandWrapper, "@managerid", DbType.Guid, managerid);

            
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
        /// <remarks>2014/11/15 0:07:19</remarks>
        public bool Insert(RevelationMyhistoryofthegapEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationMyhistoryofthegap_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, entity.Goals);
			database.AddInParameter(commandWrapper, "@ToConcede", DbType.Int32, entity.ToConcede);
			database.AddInParameter(commandWrapper, "@HistoryOfTheGap", DbType.Int32, entity.HistoryOfTheGap);
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
        /// <remarks>2014/11/15 0:07:19</remarks>
        public bool Update(RevelationMyhistoryofthegapEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationMyhistoryofthegap_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, entity.Goals);
			database.AddInParameter(commandWrapper, "@ToConcede", DbType.Int32, entity.ToConcede);
			database.AddInParameter(commandWrapper, "@HistoryOfTheGap", DbType.Int32, entity.HistoryOfTheGap);

            
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

