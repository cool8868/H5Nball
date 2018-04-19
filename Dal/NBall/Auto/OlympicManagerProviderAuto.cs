

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
    
    public partial class OlympicManagerProvider
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
		/// 将IDataReader的当前记录读取到OlympicManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public OlympicManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new OlympicManagerEntity();
			
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Football = (System.Int32) reader["Football"];
            obj.Basketball = (System.Int32) reader["Basketball"];
            obj.Volleyball = (System.Int32) reader["Volleyball"];
            obj.Swimming = (System.Int32) reader["Swimming"];
            obj.Gymnastics = (System.Int32) reader["Gymnastics"];
            obj.Shooting = (System.Int32) reader["Shooting"];
            obj.TrackAndField = (System.Int32) reader["TrackAndField"];
            obj.WeightLifting = (System.Int32) reader["WeightLifting"];
            obj.TableTennis = (System.Int32) reader["TableTennis"];
            obj.Badminton = (System.Int32) reader["Badminton"];
            obj.Rowing = (System.Int32) reader["Rowing"];
            obj.Judo = (System.Int32) reader["Judo"];
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
        public List<OlympicManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<OlympicManagerEntity>();
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
        public OlympicManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public OlympicManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="managerId">managerId</param>
        /// <returns>OlympicManagerEntity</returns>
        /// <remarks>2016/7/29 15:57:25</remarks>
        public OlympicManagerEntity GetById( System.Guid managerId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_OlympicManager_GetById");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            
            OlympicManagerEntity obj=null;
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
        /// <returns>OlympicManagerEntity列表</returns>
        /// <remarks>2016/7/29 15:57:25</remarks>
        public List<OlympicManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_OlympicManager_GetAll");
            

            
            List<OlympicManagerEntity> list = null;
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
        /// <remarks>2016/7/29 15:57:25</remarks>
        public bool Delete ( System.Guid managerId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_OlympicManager_Delete");
            
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
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/7/29 15:57:25</remarks>
        public bool Insert(OlympicManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_OlympicManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Football", DbType.Int32, entity.Football);
			database.AddInParameter(commandWrapper, "@Basketball", DbType.Int32, entity.Basketball);
			database.AddInParameter(commandWrapper, "@Volleyball", DbType.Int32, entity.Volleyball);
			database.AddInParameter(commandWrapper, "@Swimming", DbType.Int32, entity.Swimming);
			database.AddInParameter(commandWrapper, "@Gymnastics", DbType.Int32, entity.Gymnastics);
			database.AddInParameter(commandWrapper, "@Shooting", DbType.Int32, entity.Shooting);
			database.AddInParameter(commandWrapper, "@TrackAndField", DbType.Int32, entity.TrackAndField);
			database.AddInParameter(commandWrapper, "@WeightLifting", DbType.Int32, entity.WeightLifting);
			database.AddInParameter(commandWrapper, "@TableTennis", DbType.Int32, entity.TableTennis);
			database.AddInParameter(commandWrapper, "@Badminton", DbType.Int32, entity.Badminton);
			database.AddInParameter(commandWrapper, "@Rowing", DbType.Int32, entity.Rowing);
			database.AddInParameter(commandWrapper, "@Judo", DbType.Int32, entity.Judo);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
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
        /// <remarks>2016/7/29 15:57:25</remarks>
        public bool Update(OlympicManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_OlympicManager_Update");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Football", DbType.Int32, entity.Football);
			database.AddInParameter(commandWrapper, "@Basketball", DbType.Int32, entity.Basketball);
			database.AddInParameter(commandWrapper, "@Volleyball", DbType.Int32, entity.Volleyball);
			database.AddInParameter(commandWrapper, "@Swimming", DbType.Int32, entity.Swimming);
			database.AddInParameter(commandWrapper, "@Gymnastics", DbType.Int32, entity.Gymnastics);
			database.AddInParameter(commandWrapper, "@Shooting", DbType.Int32, entity.Shooting);
			database.AddInParameter(commandWrapper, "@TrackAndField", DbType.Int32, entity.TrackAndField);
			database.AddInParameter(commandWrapper, "@WeightLifting", DbType.Int32, entity.WeightLifting);
			database.AddInParameter(commandWrapper, "@TableTennis", DbType.Int32, entity.TableTennis);
			database.AddInParameter(commandWrapper, "@Badminton", DbType.Int32, entity.Badminton);
			database.AddInParameter(commandWrapper, "@Rowing", DbType.Int32, entity.Rowing);
			database.AddInParameter(commandWrapper, "@Judo", DbType.Int32, entity.Judo);
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

            entity.ManagerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
