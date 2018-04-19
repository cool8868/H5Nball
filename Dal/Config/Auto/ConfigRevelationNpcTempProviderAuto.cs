

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
    
    public partial class ConfigRevelationnpctempProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Config;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到ConfigRevelationnpctempEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigRevelationnpctempEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigRevelationnpctempEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.MarkId = (System.Int32) reader["MarkId"];
            obj.Schedule = (System.Int32) reader["Schedule"];
            obj.OpponentTeamName = (System.String) reader["OpponentTeamName"];
            obj.FormationID = (System.Int32) reader["FormationID"];
            obj.PlayerLevel = (System.Int32) reader["PlayerLevel"];
            obj.PlayerCardStrength = (System.Int32) reader["PlayerCardStrength"];
            obj.Buff = (System.Int32) reader["Buff"];
            obj.P1 = (System.Int32) reader["P1"];
            obj.P2 = (System.Int32) reader["P2"];
            obj.P3 = (System.Int32) reader["P3"];
            obj.P4 = (System.Int32) reader["P4"];
            obj.P5 = (System.Int32) reader["P5"];
            obj.P6 = (System.Int32) reader["P6"];
            obj.P7 = (System.Int32) reader["P7"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigRevelationnpctempEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigRevelationnpctempEntity>();
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
        public ConfigRevelationnpctempProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigRevelationnpctempProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>ConfigRevelationnpctempEntity</returns>
        /// <remarks>2017/2/16 11:35:33</remarks>
        public ConfigRevelationnpctempEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationnpctemp_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            ConfigRevelationnpctempEntity obj=null;
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
        /// <returns>ConfigRevelationnpctempEntity列表</returns>
        /// <remarks>2017/2/16 11:35:33</remarks>
        public List<ConfigRevelationnpctempEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationnpctemp_GetAll");
            

            
            List<ConfigRevelationnpctempEntity> list = null;
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
        /// <remarks>2017/2/16 11:35:33</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationnpctemp_Delete");
            
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
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/16 11:35:33</remarks>
        public bool Insert(ConfigRevelationnpctempEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/16 11:35:33</remarks>
        public bool Insert(ConfigRevelationnpctempEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelationnpctemp_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@OpponentTeamName", DbType.AnsiString, entity.OpponentTeamName);
			database.AddInParameter(commandWrapper, "@FormationID", DbType.Int32, entity.FormationID);
			database.AddInParameter(commandWrapper, "@PlayerLevel", DbType.Int32, entity.PlayerLevel);
			database.AddInParameter(commandWrapper, "@PlayerCardStrength", DbType.Int32, entity.PlayerCardStrength);
			database.AddInParameter(commandWrapper, "@Buff", DbType.Int32, entity.Buff);
			database.AddInParameter(commandWrapper, "@P1", DbType.Int32, entity.P1);
			database.AddInParameter(commandWrapper, "@P2", DbType.Int32, entity.P2);
			database.AddInParameter(commandWrapper, "@P3", DbType.Int32, entity.P3);
			database.AddInParameter(commandWrapper, "@P4", DbType.Int32, entity.P4);
			database.AddInParameter(commandWrapper, "@P5", DbType.Int32, entity.P5);
			database.AddInParameter(commandWrapper, "@P6", DbType.Int32, entity.P6);
			database.AddInParameter(commandWrapper, "@P7", DbType.Int32, entity.P7);

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
        /// <remarks>2017/2/16 11:35:33</remarks>
        public bool Update(ConfigRevelationnpctempEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2017/2/16 11:35:33</remarks>
        public bool Update(ConfigRevelationnpctempEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelationnpctemp_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@MarkId", DbType.Int32, entity.MarkId);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@OpponentTeamName", DbType.AnsiString, entity.OpponentTeamName);
			database.AddInParameter(commandWrapper, "@FormationID", DbType.Int32, entity.FormationID);
			database.AddInParameter(commandWrapper, "@PlayerLevel", DbType.Int32, entity.PlayerLevel);
			database.AddInParameter(commandWrapper, "@PlayerCardStrength", DbType.Int32, entity.PlayerCardStrength);
			database.AddInParameter(commandWrapper, "@Buff", DbType.Int32, entity.Buff);
			database.AddInParameter(commandWrapper, "@P1", DbType.Int32, entity.P1);
			database.AddInParameter(commandWrapper, "@P2", DbType.Int32, entity.P2);
			database.AddInParameter(commandWrapper, "@P3", DbType.Int32, entity.P3);
			database.AddInParameter(commandWrapper, "@P4", DbType.Int32, entity.P4);
			database.AddInParameter(commandWrapper, "@P5", DbType.Int32, entity.P5);
			database.AddInParameter(commandWrapper, "@P6", DbType.Int32, entity.P6);
			database.AddInParameter(commandWrapper, "@P7", DbType.Int32, entity.P7);

            
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
