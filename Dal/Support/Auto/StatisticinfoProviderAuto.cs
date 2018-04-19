

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
    
    public partial class StatisticInfoProvider
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
		/// 将IDataReader的当前记录读取到StatisticInfoEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public StatisticInfoEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new StatisticInfoEntity();
			
            obj.ZoneId = (System.Int32) reader["ZoneId"];
            obj.TotalUser = (System.Int32) reader["TotalUser"];
            obj.TotalManager = (System.Int32) reader["TotalManager"];
            obj.TotalPay = (System.Int64) reader["TotalPay"];
            obj.PointRemain = (System.Int64) reader["PointRemain"];
            obj.Pcu = (System.Int32) reader["Pcu"];
            obj.Acu = (System.Int32) reader["Acu"];
            obj.OnlineMinutes = (System.Int64) reader["OnlineMinutes"];
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
        public List<StatisticInfoEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<StatisticInfoEntity>();
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
        public StatisticInfoProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public StatisticInfoProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="zoneId">zoneId</param>
        /// <returns>StatisticInfoEntity</returns>
        /// <remarks>2016/6/7 12:10:56</remarks>
        public StatisticInfoEntity GetById( System.Int32 zoneId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_StatisticInfo_GetById");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);

            
            StatisticInfoEntity obj=null;
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
        /// <returns>StatisticInfoEntity列表</returns>
        /// <remarks>2016/6/7 12:10:56</remarks>
        public List<StatisticInfoEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_StatisticInfo_GetAll");
            

            
            List<StatisticInfoEntity> list = null;
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
		/// <param name="zoneId">zoneId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 12:10:56</remarks>
        public bool Delete ( System.Int32 zoneId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_StatisticInfo_Delete");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);

            
            
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
		
		#region  Create
		
		/// <summary>
        /// Create
        /// </summary>
		/// <param name="zoneId">zoneId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 12:10:56</remarks>
        public bool Create ( System.Int32 zoneId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticInfo_Create");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);

            
            
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
		/// <param name="zoneId">zoneId</param>
		/// <param name="totalUser">totalUser</param>
		/// <param name="totalManager">totalManager</param>
		/// <param name="totalPay">totalPay</param>
		/// <param name="pointRemain">pointRemain</param>
		/// <param name="onlineMinutes">onlineMinutes</param>
		/// <param name="updateTime">updateTime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/7 12:10:56</remarks>
        public bool Update ( System.Int32 zoneId, System.Int32 totalUser, System.Int32 totalManager, System.Int64 totalPay, System.Int64 pointRemain, System.Int64 onlineMinutes, System.DateTime updateTime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticInfo_Update");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@TotalUser", DbType.Int32, totalUser);
			database.AddInParameter(commandWrapper, "@TotalManager", DbType.Int32, totalManager);
			database.AddInParameter(commandWrapper, "@TotalPay", DbType.Int64, totalPay);
			database.AddInParameter(commandWrapper, "@PointRemain", DbType.Int64, pointRemain);
			database.AddInParameter(commandWrapper, "@OnlineMinutes", DbType.Int64, onlineMinutes);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, updateTime);

            
            
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
        /// <remarks>2016/6/7 12:10:56</remarks>
        public bool Insert(StatisticInfoEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/7 12:10:56</remarks>
        public bool Insert(StatisticInfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_StatisticInfo_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@TotalUser", DbType.Int32, entity.TotalUser);
			database.AddInParameter(commandWrapper, "@TotalManager", DbType.Int32, entity.TotalManager);
			database.AddInParameter(commandWrapper, "@TotalPay", DbType.Int64, entity.TotalPay);
			database.AddInParameter(commandWrapper, "@PointRemain", DbType.Int64, entity.PointRemain);
			database.AddInParameter(commandWrapper, "@Pcu", DbType.Int32, entity.Pcu);
			database.AddInParameter(commandWrapper, "@Acu", DbType.Int32, entity.Acu);
			database.AddInParameter(commandWrapper, "@OnlineMinutes", DbType.Int64, entity.OnlineMinutes);
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
            
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/6/7 12:10:56</remarks>
        public bool Update(StatisticInfoEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/6/7 12:10:56</remarks>
        public bool Update(StatisticInfoEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_StatisticInfo_Update");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@TotalUser", DbType.Int32, entity.TotalUser);
			database.AddInParameter(commandWrapper, "@TotalManager", DbType.Int32, entity.TotalManager);
			database.AddInParameter(commandWrapper, "@TotalPay", DbType.Int64, entity.TotalPay);
			database.AddInParameter(commandWrapper, "@PointRemain", DbType.Int64, entity.PointRemain);
			database.AddInParameter(commandWrapper, "@Pcu", DbType.Int32, entity.Pcu);
			database.AddInParameter(commandWrapper, "@Acu", DbType.Int32, entity.Acu);
			database.AddInParameter(commandWrapper, "@OnlineMinutes", DbType.Int64, entity.OnlineMinutes);
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

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
