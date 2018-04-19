

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
    
    public partial class MallExtrarecordProvider
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
		/// 将IDataReader的当前记录读取到MallExtrarecordEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public MallExtrarecordEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new MallExtrarecordEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ExtraType = (System.Int32) reader["ExtraType"];
            obj.UsedCount = (System.Int32) reader["UsedCount"];
            obj.RecordDate = (System.DateTime) reader["RecordDate"];
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
        public List<MallExtrarecordEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<MallExtrarecordEntity>();
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
        public MallExtrarecordProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public MallExtrarecordProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>MallExtrarecordEntity</returns>
        /// <remarks>2015/10/19 17:25:56</remarks>
        public MallExtrarecordEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_MallExtrarecord_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            MallExtrarecordEntity obj=null;
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
		
		#region  GetExtra
		
		/// <summary>
        /// GetExtra
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="extraType">extraType</param>
        /// <returns>MallExtrarecordEntity</returns>
        /// <remarks>2015/10/19 17:25:56</remarks>
        public MallExtrarecordEntity GetExtra( System.Guid managerId, System.Int32 extraType)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Mall_GetExtra");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ExtraType", DbType.Int32, extraType);

            
            MallExtrarecordEntity obj=null;
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
        /// <returns>MallExtrarecordEntity列表</returns>
        /// <remarks>2015/10/19 17:25:56</remarks>
        public List<MallExtrarecordEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_MallExtrarecord_GetAll");
            

            
            List<MallExtrarecordEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  ExtraAddPkCount
		
		/// <summary>
        /// ExtraAddPkCount
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="curPKCount">curPKCount</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:25:56</remarks>
        public bool ExtraAddPkCount ( System.Guid managerId,ref  System.Int32 curPKCount,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Mall_ExtraAddPkCount");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@CurPKCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curPKCount);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            curPKCount=(System.Int32)database.GetParameterValue(commandWrapper, "@CurPKCount");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ExtraAddStamina
		
		/// <summary>
        /// ExtraAddStamina
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="resumeTime">resumeTime</param>
		/// <param name="curStamina">curStamina</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:25:56</remarks>
        public bool ExtraAddStamina ( System.Guid managerId, System.DateTime resumeTime, System.Int32 curStamina,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Mall_ExtraAddStamina");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ResumeTime", DbType.DateTime, resumeTime);
			database.AddInParameter(commandWrapper, "@CurStamina", DbType.Int32, curStamina);

            
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
		
		#region  ExtraAddTrainseat
		
		/// <summary>
        /// ExtraAddTrainseat
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="curTrainSeat">curTrainSeat</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:25:56</remarks>
        public bool ExtraAddTrainseat ( System.Guid managerId,ref  System.Int32 curTrainSeat,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Mall_ExtraAddTrainseat");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@CurTrainSeat", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curTrainSeat);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            curTrainSeat=(System.Int32)database.GetParameterValue(commandWrapper, "@CurTrainSeat");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ExtraExpandPackage
		
		/// <summary>
        /// ExtraExpandPackage
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="addSize">addSize</param>
		/// <param name="resultSize">resultSize</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:25:56</remarks>
        public bool ExtraExpandPackage ( System.Guid managerId, System.Int32 addSize,ref  System.Int32 resultSize,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Mall_ExtraExpandPackage");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@AddSize", DbType.Int32, addSize);
			database.AddParameter(commandWrapper, "@ResultSize", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,resultSize);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            resultSize=(System.Int32)database.GetParameterValue(commandWrapper, "@ResultSize");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ExtraResetElite
		
		/// <summary>
        /// ExtraResetElite
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="recordDate">recordDate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:25:56</remarks>
        public bool ExtraResetElite ( System.Guid managerId, System.DateTime recordDate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Mall_ExtraResetElite");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);

            
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
		
		#region  ExtraSave
		
		/// <summary>
        /// ExtraSave
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="managerId">managerId</param>
		/// <param name="extraType">extraType</param>
		/// <param name="usedCount">usedCount</param>
		/// <param name="recordDate">recordDate</param>
		/// <param name="rowVersion">rowVersion</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:25:56</remarks>
        public bool ExtraSave ( System.Int32 idx, System.Guid managerId, System.Int32 extraType, System.Int32 usedCount, System.DateTime recordDate, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Mall_ExtraSave");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@ExtraType", DbType.Int32, extraType);
			database.AddInParameter(commandWrapper, "@UsedCount", DbType.Int32, usedCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, recordDate);
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
		
		#region  ExtraAddSubstitute
		
		/// <summary>
        /// ExtraAddSubstitute
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="curSubstitute">curSubstitute</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:25:57</remarks>
        public bool ExtraAddSubstitute ( System.Guid managerId,ref  System.Int32 curSubstitute,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Mall_ExtraAddSubstitute");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddParameter(commandWrapper, "@CurSubstitute", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curSubstitute);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            curSubstitute=(System.Int32)database.GetParameterValue(commandWrapper, "@CurSubstitute");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  UpdateUsedCount
		
		/// <summary>
        /// UpdateUsedCount
        /// </summary>
		/// <param name="extraType">extraType</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/10/19 17:25:57</remarks>
        public bool UpdateUsedCount ( System.Int32 extraType,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_MallExtrareCord_UpdateUsedCount");
            
			database.AddInParameter(commandWrapper, "@ExtraType", DbType.Int32, extraType);

            
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
        /// <remarks>2015/10/19 17:25:57</remarks>
        public bool Insert(MallExtrarecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_MallExtrarecord_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ExtraType", DbType.Int32, entity.ExtraType);
			database.AddInParameter(commandWrapper, "@UsedCount", DbType.Int32, entity.UsedCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
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
        /// <remarks>2015/10/19 17:25:57</remarks>
        public bool Update(MallExtrarecordEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_MallExtrarecord_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ExtraType", DbType.Int32, entity.ExtraType);
			database.AddInParameter(commandWrapper, "@UsedCount", DbType.Int32, entity.UsedCount);
			database.AddInParameter(commandWrapper, "@RecordDate", DbType.DateTime, entity.RecordDate);
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

            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

