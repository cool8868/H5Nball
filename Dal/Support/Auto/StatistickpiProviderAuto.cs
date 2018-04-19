

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
    
    public partial class StatisticKpiProvider
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
		/// 将IDataReader的当前记录读取到StatisticKpiEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public StatisticKpiEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new StatisticKpiEntity();
			
            obj.Idx = (System.Int64) reader["Idx"];
            obj.ZoneId = (System.Int32) reader["ZoneId"];
            obj.RecordMonth = (System.String) reader["RecordMonth"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
            obj.TotalUser = (System.Int32) reader["TotalUser"];
            obj.TotalManager = (System.Int32) reader["TotalManager"];
            obj.Dau = (System.Int32) reader["Dau"];
            obj.DUniqueIp = (System.Int32) reader["DUniqueIp"];
            obj.DNewUser = (System.Int32) reader["DNewUser"];
            obj.DNewManager = (System.Int32) reader["DNewManager"];
            obj.DLostUser7 = (System.Int32) reader["DLostUser7"];
            obj.DLostUser15 = (System.Int32) reader["DLostUser15"];
            obj.DLostUser30 = (System.Int32) reader["DLostUser30"];
            obj.Retention2 = (System.Int32) reader["Retention2"];
            obj.Retention3 = (System.Int32) reader["Retention3"];
            obj.Retention4 = (System.Int32) reader["Retention4"];
            obj.Retention5 = (System.Int32) reader["Retention5"];
            obj.Retention6 = (System.Int32) reader["Retention6"];
            obj.Retention7 = (System.Int32) reader["Retention7"];
            obj.Retention15 = (System.Int32) reader["Retention15"];
            obj.Retention30 = (System.Int32) reader["Retention30"];
            obj.Acu = (System.Int32) reader["Acu"];
            obj.Pcu = (System.Int32) reader["Pcu"];
            obj.Lcu = (System.Int32) reader["Lcu"];
            obj.TotalOnline = (System.Int64) reader["TotalOnline"];
            obj.Wau = (System.Int32) reader["Wau"];
            obj.WLost = (System.Int32) reader["WLost"];
            obj.WHonor = (System.Int32) reader["WHonor"];
            obj.WHonorLost = (System.Int32) reader["WHonorLost"];
            obj.Mau = (System.Int32) reader["Mau"];
            obj.PayUserCount = (System.Int32) reader["PayUserCount"];
            obj.PayCount = (System.Int32) reader["PayCount"];
            obj.PayTotal = (System.Int32) reader["PayTotal"];
            obj.PaySum = (System.Int64) reader["PaySum"];
            obj.PayFirst = (System.Int32) reader["PayFirst"];
            obj.PointRemain = (System.Int64) reader["PointRemain"];
            obj.PointConsume = (System.Int64) reader["PointConsume"];
            obj.PointCirculate = (System.Int64) reader["PointCirculate"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.GetPoint = (System.Int32) reader["GetPoint"];
            obj.GetCoin = (System.Int64) reader["GetCoin"];
            obj.CoinConsume = (System.Int64) reader["CoinConsume"];
            obj.EnergyConsume = (System.Int32) reader["EnergyConsume"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<StatisticKpiEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<StatisticKpiEntity>();
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
        public StatisticKpiProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public StatisticKpiProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>StatisticKpiEntity</returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public StatisticKpiEntity GetById( System.Int64 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_StatisticKpi_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, idx);

            
            StatisticKpiEntity obj=null;
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
        /// <returns>StatisticKpiEntity列表</returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public List<StatisticKpiEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_StatisticKpi_GetAll");
            

            
            List<StatisticKpiEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetbyDate
		
		/// <summary>
        /// GetbyDate
        /// </summary>
        /// <returns>StatisticKpiEntity列表</returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public List<StatisticKpiEntity> GetbyDate( System.Int32 zoneId, System.DateTime startTime, System.DateTime endTime)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticKpi_GetbyDate");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);

            
            List<StatisticKpiEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetZonebyDate
		
		/// <summary>
        /// GetZonebyDate
        /// </summary>
        /// <returns>StatisticKpiEntity列表</returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public List<StatisticKpiEntity> GetZonebyDate( System.Int32 zoneId, System.DateTime startTime, System.DateTime endTime)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticKpiZone_GetZonebyDate");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);

            
            List<StatisticKpiEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetbyPlatform
		
		/// <summary>
        /// GetbyPlatform
        /// </summary>
        /// <returns>StatisticKpiEntity列表</returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public List<StatisticKpiEntity> GetbyPlatform( System.Int32 zoneId, System.String platCode, System.DateTime startTime, System.DateTime endTime)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticKpi_GetbyPlatform");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@PlatCode", DbType.AnsiString, platCode);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);

            
            List<StatisticKpiEntity> list = null;
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
        /// <remarks>2016/7/5 16:39:38</remarks>
        public bool Delete ( System.Int64 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_StatisticKpi_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, idx);

            
            
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
		/// <param name="curDate">curDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public bool Create ( System.Int32 zoneId, System.DateTime curDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticKpi_Create");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@CurDate", DbType.DateTime, curDate);

            
            
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
		/// <param name="recordMonth">recordMonth</param>
		/// <param name="recordDate">recordDate</param>
		/// <param name="totalUser">totalUser</param>
		/// <param name="totalManager">totalManager</param>
		/// <param name="dau">dau</param>
		/// <param name="dUniqueIp">dUniqueIp</param>
		/// <param name="dNewUser">dNewUser</param>
		/// <param name="dNewManager">dNewManager</param>
		/// <param name="dLostUser7">dLostUser7</param>
		/// <param name="dLostUser15">dLostUser15</param>
		/// <param name="dLostUser30">dLostUser30</param>
		/// <param name="retention2">retention2</param>
		/// <param name="retention3">retention3</param>
		/// <param name="retention4">retention4</param>
		/// <param name="retention5">retention5</param>
		/// <param name="retention6">retention6</param>
		/// <param name="retention7">retention7</param>
		/// <param name="retention15">retention15</param>
		/// <param name="retention30">retention30</param>
		/// <param name="wau">wau</param>
		/// <param name="wLost">wLost</param>
		/// <param name="wHonor">wHonor</param>
		/// <param name="wHonorLost">wHonorLost</param>
		/// <param name="mau">mau</param>
		/// <param name="payUserCount">payUserCount</param>
		/// <param name="payCount">payCount</param>
		/// <param name="payTotal">payTotal</param>
		/// <param name="paySum">paySum</param>
		/// <param name="payFirst">payFirst</param>
		/// <param name="pointRemain">pointRemain</param>
		/// <param name="pointConsume">pointConsume</param>
		/// <param name="pointCirculate">pointCirculate</param>
		/// <param name="updateTime">updateTime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public bool Update ( System.Int32 zoneId, System.String recordMonth, System.DateTime recordDate, System.Int32 totalUser, System.Int32 totalManager, System.Int32 dau, System.Int32 dUniqueIp, System.Int32 dNewUser, System.Int32 dNewManager, System.Int32 dLostUser7, System.Int32 dLostUser15, System.Int32 dLostUser30, System.Int32 retention2, System.Int32 retention3, System.Int32 retention4, System.Int32 retention5, System.Int32 retention6, System.Int32 retention7, System.Int32 retention15, System.Int32 retention30, System.Int32 wau, System.Int32 wLost, System.Int32 wHonor, System.Int32 wHonorLost, System.Int32 mau, System.Int32 payUserCount, System.Int32 payCount, System.Int32 payTotal, System.Int64 paySum, System.Int32 payFirst, System.Int64 pointRemain, System.Int64 pointConsume, System.Int64 pointCirculate, System.DateTime updateTime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticKpi_Update");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@RecordMonth", DbType.AnsiStringFixedLength, recordMonth);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);
			database.AddInParameter(commandWrapper, "@TotalUser", DbType.Int32, totalUser);
			database.AddInParameter(commandWrapper, "@TotalManager", DbType.Int32, totalManager);
			database.AddInParameter(commandWrapper, "@Dau", DbType.Int32, dau);
			database.AddInParameter(commandWrapper, "@DUniqueIp", DbType.Int32, dUniqueIp);
			database.AddInParameter(commandWrapper, "@DNewUser", DbType.Int32, dNewUser);
			database.AddInParameter(commandWrapper, "@DNewManager", DbType.Int32, dNewManager);
			database.AddInParameter(commandWrapper, "@DLostUser7", DbType.Int32, dLostUser7);
			database.AddInParameter(commandWrapper, "@DLostUser15", DbType.Int32, dLostUser15);
			database.AddInParameter(commandWrapper, "@DLostUser30", DbType.Int32, dLostUser30);
			database.AddInParameter(commandWrapper, "@Retention2", DbType.Int32, retention2);
			database.AddInParameter(commandWrapper, "@Retention3", DbType.Int32, retention3);
			database.AddInParameter(commandWrapper, "@Retention4", DbType.Int32, retention4);
			database.AddInParameter(commandWrapper, "@Retention5", DbType.Int32, retention5);
			database.AddInParameter(commandWrapper, "@Retention6", DbType.Int32, retention6);
			database.AddInParameter(commandWrapper, "@Retention7", DbType.Int32, retention7);
			database.AddInParameter(commandWrapper, "@Retention15", DbType.Int32, retention15);
			database.AddInParameter(commandWrapper, "@Retention30", DbType.Int32, retention30);
			database.AddInParameter(commandWrapper, "@Wau", DbType.Int32, wau);
			database.AddInParameter(commandWrapper, "@WLost", DbType.Int32, wLost);
			database.AddInParameter(commandWrapper, "@WHonor", DbType.Int32, wHonor);
			database.AddInParameter(commandWrapper, "@WHonorLost", DbType.Int32, wHonorLost);
			database.AddInParameter(commandWrapper, "@Mau", DbType.Int32, mau);
			database.AddInParameter(commandWrapper, "@PayUserCount", DbType.Int32, payUserCount);
			database.AddInParameter(commandWrapper, "@PayCount", DbType.Int32, payCount);
			database.AddInParameter(commandWrapper, "@PayTotal", DbType.Int32, payTotal);
			database.AddInParameter(commandWrapper, "@PaySum", DbType.Int64, paySum);
			database.AddInParameter(commandWrapper, "@PayFirst", DbType.Int32, payFirst);
			database.AddInParameter(commandWrapper, "@PointRemain", DbType.Int64, pointRemain);
			database.AddInParameter(commandWrapper, "@PointConsume", DbType.Int64, pointConsume);
			database.AddInParameter(commandWrapper, "@PointCirculate", DbType.Int64, pointCirculate);
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
		
		#region  UpdateImmediate
		
		/// <summary>
        /// UpdateImmediate
        /// </summary>
		/// <param name="zoneId">zoneId</param>
		/// <param name="recordMonth">recordMonth</param>
		/// <param name="recordDate">recordDate</param>
		/// <param name="totalUser">totalUser</param>
		/// <param name="totalManager">totalManager</param>
		/// <param name="dau">dau</param>
		/// <param name="dUniqueIp">dUniqueIp</param>
		/// <param name="dNewUser">dNewUser</param>
		/// <param name="dNewManager">dNewManager</param>
		/// <param name="payUserCount">payUserCount</param>
		/// <param name="payCount">payCount</param>
		/// <param name="payTotal">payTotal</param>
		/// <param name="paySum">paySum</param>
		/// <param name="payFirst">payFirst</param>
		/// <param name="mau">mau</param>
		/// <param name="updateTime">updateTime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public bool UpdateImmediate ( System.Int32 zoneId, System.String recordMonth, System.DateTime recordDate, System.Int32 totalUser, System.Int32 totalManager, System.Int32 dau, System.Int32 dUniqueIp, System.Int32 dNewUser, System.Int32 dNewManager, System.Int32 payUserCount, System.Int32 payCount, System.Int32 payTotal, System.Int64 paySum, System.Int32 payFirst, System.Int32 mau, System.DateTime updateTime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticKpi_UpdateImmediate");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@RecordMonth", DbType.AnsiStringFixedLength, recordMonth);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);
			database.AddInParameter(commandWrapper, "@TotalUser", DbType.Int32, totalUser);
			database.AddInParameter(commandWrapper, "@TotalManager", DbType.Int32, totalManager);
			database.AddInParameter(commandWrapper, "@Dau", DbType.Int32, dau);
			database.AddInParameter(commandWrapper, "@DUniqueIp", DbType.Int32, dUniqueIp);
			database.AddInParameter(commandWrapper, "@DNewUser", DbType.Int32, dNewUser);
			database.AddInParameter(commandWrapper, "@DNewManager", DbType.Int32, dNewManager);
			database.AddInParameter(commandWrapper, "@PayUserCount", DbType.Int32, payUserCount);
			database.AddInParameter(commandWrapper, "@PayCount", DbType.Int32, payCount);
			database.AddInParameter(commandWrapper, "@PayTotal", DbType.Int32, payTotal);
			database.AddInParameter(commandWrapper, "@PaySum", DbType.Int64, paySum);
			database.AddInParameter(commandWrapper, "@PayFirst", DbType.Int32, payFirst);
			database.AddInParameter(commandWrapper, "@Mau", DbType.Int32, mau);
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
		
		#region  UpdateSame
		
		/// <summary>
        /// UpdateSame
        /// </summary>
		/// <param name="zoneId">zoneId</param>
		/// <param name="recordDate">recordDate</param>
		/// <param name="getPoint">getPoint</param>
		/// <param name="energyConsume">energyConsume</param>
		/// <param name="coinConsume">coinConsume</param>
		/// <param name="getCion">getCion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public bool UpdateSame ( System.Int32 zoneId, System.DateTime recordDate, System.Int32 getPoint, System.Int32 energyConsume, System.Int64 coinConsume, System.Int64 getCion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_StatisticKpi_UpdateSame");
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, zoneId);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);
			database.AddInParameter(commandWrapper, "@GetPoint", DbType.Int32, getPoint);
			database.AddInParameter(commandWrapper, "@EnergyConsume", DbType.Int32, energyConsume);
			database.AddInParameter(commandWrapper, "@CoinConsume", DbType.Int64, coinConsume);
			database.AddInParameter(commandWrapper, "@GetCion", DbType.Int64, getCion);

            
            
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
        /// <remarks>2016/7/5 16:39:38</remarks>
        public bool Insert(StatisticKpiEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public bool Insert(StatisticKpiEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_StatisticKpi_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@RecordMonth", DbType.AnsiStringFixedLength, entity.RecordMonth);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@TotalUser", DbType.Int32, entity.TotalUser);
			database.AddInParameter(commandWrapper, "@TotalManager", DbType.Int32, entity.TotalManager);
			database.AddInParameter(commandWrapper, "@Dau", DbType.Int32, entity.Dau);
			database.AddInParameter(commandWrapper, "@DUniqueIp", DbType.Int32, entity.DUniqueIp);
			database.AddInParameter(commandWrapper, "@DNewUser", DbType.Int32, entity.DNewUser);
			database.AddInParameter(commandWrapper, "@DNewManager", DbType.Int32, entity.DNewManager);
			database.AddInParameter(commandWrapper, "@DLostUser7", DbType.Int32, entity.DLostUser7);
			database.AddInParameter(commandWrapper, "@DLostUser15", DbType.Int32, entity.DLostUser15);
			database.AddInParameter(commandWrapper, "@DLostUser30", DbType.Int32, entity.DLostUser30);
			database.AddInParameter(commandWrapper, "@Retention2", DbType.Int32, entity.Retention2);
			database.AddInParameter(commandWrapper, "@Retention3", DbType.Int32, entity.Retention3);
			database.AddInParameter(commandWrapper, "@Retention4", DbType.Int32, entity.Retention4);
			database.AddInParameter(commandWrapper, "@Retention5", DbType.Int32, entity.Retention5);
			database.AddInParameter(commandWrapper, "@Retention6", DbType.Int32, entity.Retention6);
			database.AddInParameter(commandWrapper, "@Retention7", DbType.Int32, entity.Retention7);
			database.AddInParameter(commandWrapper, "@Retention15", DbType.Int32, entity.Retention15);
			database.AddInParameter(commandWrapper, "@Retention30", DbType.Int32, entity.Retention30);
			database.AddInParameter(commandWrapper, "@Acu", DbType.Int32, entity.Acu);
			database.AddInParameter(commandWrapper, "@Pcu", DbType.Int32, entity.Pcu);
			database.AddInParameter(commandWrapper, "@Lcu", DbType.Int32, entity.Lcu);
			database.AddInParameter(commandWrapper, "@TotalOnline", DbType.Int64, entity.TotalOnline);
			database.AddInParameter(commandWrapper, "@Wau", DbType.Int32, entity.Wau);
			database.AddInParameter(commandWrapper, "@WLost", DbType.Int32, entity.WLost);
			database.AddInParameter(commandWrapper, "@WHonor", DbType.Int32, entity.WHonor);
			database.AddInParameter(commandWrapper, "@WHonorLost", DbType.Int32, entity.WHonorLost);
			database.AddInParameter(commandWrapper, "@Mau", DbType.Int32, entity.Mau);
			database.AddInParameter(commandWrapper, "@PayUserCount", DbType.Int32, entity.PayUserCount);
			database.AddInParameter(commandWrapper, "@PayCount", DbType.Int32, entity.PayCount);
			database.AddInParameter(commandWrapper, "@PayTotal", DbType.Int32, entity.PayTotal);
			database.AddInParameter(commandWrapper, "@PaySum", DbType.Int64, entity.PaySum);
			database.AddInParameter(commandWrapper, "@PayFirst", DbType.Int32, entity.PayFirst);
			database.AddInParameter(commandWrapper, "@PointRemain", DbType.Int64, entity.PointRemain);
			database.AddInParameter(commandWrapper, "@PointConsume", DbType.Int64, entity.PointConsume);
			database.AddInParameter(commandWrapper, "@PointCirculate", DbType.Int64, entity.PointCirculate);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@GetPoint", DbType.Int32, entity.GetPoint);
			database.AddInParameter(commandWrapper, "@GetCoin", DbType.Int64, entity.GetCoin);
			database.AddInParameter(commandWrapper, "@CoinConsume", DbType.Int64, entity.CoinConsume);
			database.AddInParameter(commandWrapper, "@EnergyConsume", DbType.Int32, entity.EnergyConsume);
			database.AddParameter(commandWrapper, "@Idx", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Int64)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public bool Update(StatisticKpiEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/7/5 16:39:38</remarks>
        public bool Update(StatisticKpiEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_StatisticKpi_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int64, entity.Idx);
			database.AddInParameter(commandWrapper, "@ZoneId", DbType.Int32, entity.ZoneId);
			database.AddInParameter(commandWrapper, "@RecordMonth", DbType.AnsiStringFixedLength, entity.RecordMonth);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
			database.AddInParameter(commandWrapper, "@TotalUser", DbType.Int32, entity.TotalUser);
			database.AddInParameter(commandWrapper, "@TotalManager", DbType.Int32, entity.TotalManager);
			database.AddInParameter(commandWrapper, "@Dau", DbType.Int32, entity.Dau);
			database.AddInParameter(commandWrapper, "@DUniqueIp", DbType.Int32, entity.DUniqueIp);
			database.AddInParameter(commandWrapper, "@DNewUser", DbType.Int32, entity.DNewUser);
			database.AddInParameter(commandWrapper, "@DNewManager", DbType.Int32, entity.DNewManager);
			database.AddInParameter(commandWrapper, "@DLostUser7", DbType.Int32, entity.DLostUser7);
			database.AddInParameter(commandWrapper, "@DLostUser15", DbType.Int32, entity.DLostUser15);
			database.AddInParameter(commandWrapper, "@DLostUser30", DbType.Int32, entity.DLostUser30);
			database.AddInParameter(commandWrapper, "@Retention2", DbType.Int32, entity.Retention2);
			database.AddInParameter(commandWrapper, "@Retention3", DbType.Int32, entity.Retention3);
			database.AddInParameter(commandWrapper, "@Retention4", DbType.Int32, entity.Retention4);
			database.AddInParameter(commandWrapper, "@Retention5", DbType.Int32, entity.Retention5);
			database.AddInParameter(commandWrapper, "@Retention6", DbType.Int32, entity.Retention6);
			database.AddInParameter(commandWrapper, "@Retention7", DbType.Int32, entity.Retention7);
			database.AddInParameter(commandWrapper, "@Retention15", DbType.Int32, entity.Retention15);
			database.AddInParameter(commandWrapper, "@Retention30", DbType.Int32, entity.Retention30);
			database.AddInParameter(commandWrapper, "@Acu", DbType.Int32, entity.Acu);
			database.AddInParameter(commandWrapper, "@Pcu", DbType.Int32, entity.Pcu);
			database.AddInParameter(commandWrapper, "@Lcu", DbType.Int32, entity.Lcu);
			database.AddInParameter(commandWrapper, "@TotalOnline", DbType.Int64, entity.TotalOnline);
			database.AddInParameter(commandWrapper, "@Wau", DbType.Int32, entity.Wau);
			database.AddInParameter(commandWrapper, "@WLost", DbType.Int32, entity.WLost);
			database.AddInParameter(commandWrapper, "@WHonor", DbType.Int32, entity.WHonor);
			database.AddInParameter(commandWrapper, "@WHonorLost", DbType.Int32, entity.WHonorLost);
			database.AddInParameter(commandWrapper, "@Mau", DbType.Int32, entity.Mau);
			database.AddInParameter(commandWrapper, "@PayUserCount", DbType.Int32, entity.PayUserCount);
			database.AddInParameter(commandWrapper, "@PayCount", DbType.Int32, entity.PayCount);
			database.AddInParameter(commandWrapper, "@PayTotal", DbType.Int32, entity.PayTotal);
			database.AddInParameter(commandWrapper, "@PaySum", DbType.Int64, entity.PaySum);
			database.AddInParameter(commandWrapper, "@PayFirst", DbType.Int32, entity.PayFirst);
			database.AddInParameter(commandWrapper, "@PointRemain", DbType.Int64, entity.PointRemain);
			database.AddInParameter(commandWrapper, "@PointConsume", DbType.Int64, entity.PointConsume);
			database.AddInParameter(commandWrapper, "@PointCirculate", DbType.Int64, entity.PointCirculate);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@GetPoint", DbType.Int32, entity.GetPoint);
			database.AddInParameter(commandWrapper, "@GetCoin", DbType.Int64, entity.GetCoin);
			database.AddInParameter(commandWrapper, "@CoinConsume", DbType.Int64, entity.CoinConsume);
			database.AddInParameter(commandWrapper, "@EnergyConsume", DbType.Int32, entity.EnergyConsume);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Int64)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
