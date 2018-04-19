

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
    
    public partial class RevelationHistoryofthegapProvider
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
		/// 将IDataReader的当前记录读取到RevelationHistoryofthegapEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public RevelationHistoryofthegapEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new RevelationHistoryofthegapEntity();
			
            obj.CustomsPass = (System.Int32) reader["CustomsPass"];
            obj.Schedule = (System.Int32) reader["Schedule"];
            obj.ManagerName = (System.String) reader["ManagerName"];
            obj.Goals = (System.Int32) reader["Goals"];
            obj.ToConcede = (System.Int32) reader["ToConcede"];
            obj.HistoryOfTheGap = (System.Int32) reader["HistoryOfTheGap"];
            obj.States = (System.Int32) reader["States"];
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
        public List<RevelationHistoryofthegapEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<RevelationHistoryofthegapEntity>();
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
        public RevelationHistoryofthegapProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public RevelationHistoryofthegapProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="customsPass">customsPass</param>
		/// <param name="schedule">schedule</param>
        /// <returns>RevelationHistoryofthegapEntity</returns>
        /// <remarks>2017/2/28 9:56:09</remarks>
        public RevelationHistoryofthegapEntity GetById( System.Int32 customsPass, System.Int32 schedule)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationHistoryofthegap_GetById");
            
			database.AddInParameter(commandWrapper, "@CustomsPass", DbType.Int32, customsPass);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, schedule);

            
            RevelationHistoryofthegapEntity obj=null;
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
        /// <returns>RevelationHistoryofthegapEntity列表</returns>
        /// <remarks>2017/2/28 9:56:09</remarks>
        public List<RevelationHistoryofthegapEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationHistoryofthegap_GetAll");
            

            
            List<RevelationHistoryofthegapEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  C_RevelationHistoryOfTheGapGetId
		
		/// <summary>
        /// C_RevelationHistoryOfTheGapGetId
        /// </summary>
        /// <returns>RevelationHistoryofthegapEntity列表</returns>
        /// <remarks>2017/2/28 9:56:09</remarks>
        public List<RevelationHistoryofthegapEntity> C_RevelationHistoryOfTheGapGetId( System.Int32 mark)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_RevelationHistoryOfTheGapGetId");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, mark);

            
            List<RevelationHistoryofthegapEntity> list = null;
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
		/// <param name="customsPass">customsPass</param>
		/// <param name="schedule">schedule</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2017/2/28 9:56:09</remarks>
        public bool Delete ( System.Int32 customsPass, System.Int32 schedule,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationHistoryofthegap_Delete");
            
			database.AddInParameter(commandWrapper, "@CustomsPass", DbType.Int32, customsPass);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, schedule);

            
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
        /// <remarks>2017/2/28 9:56:09</remarks>
        public bool Insert(RevelationHistoryofthegapEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationHistoryofthegap_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@CustomsPass", DbType.Int32, entity.CustomsPass);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, entity.Goals);
			database.AddInParameter(commandWrapper, "@ToConcede", DbType.Int32, entity.ToConcede);
			database.AddInParameter(commandWrapper, "@HistoryOfTheGap", DbType.Int32, entity.HistoryOfTheGap);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
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
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/28 9:56:09</remarks>
        public bool Update(RevelationHistoryofthegapEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationHistoryofthegap_Update");
            
			database.AddInParameter(commandWrapper, "@CustomsPass", DbType.Int32, entity.CustomsPass);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@ManagerName", DbType.String, entity.ManagerName);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, entity.Goals);
			database.AddInParameter(commandWrapper, "@ToConcede", DbType.Int32, entity.ToConcede);
			database.AddInParameter(commandWrapper, "@HistoryOfTheGap", DbType.Int32, entity.HistoryOfTheGap);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
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
