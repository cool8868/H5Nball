

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
    
    public partial class NbManagercommonpackageProvider
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
		/// 将IDataReader的当前记录读取到NbManagercommonpackageEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbManagercommonpackageEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbManagercommonpackageEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.Common1 = (System.String) reader["Common1"];
            obj.Common2 = (System.String) reader["Common2"];
            obj.Common3 = (System.String) reader["Common3"];
            obj.Common4 = (System.String) reader["Common4"];
            obj.Rowtime = (System.DateTime) reader["Rowtime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<NbManagercommonpackageEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbManagercommonpackageEntity>();
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
        public NbManagercommonpackageProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbManagercommonpackageProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>NbManagercommonpackageEntity</returns>
        /// <remarks>2016/8/4 10:06:45</remarks>
        public NbManagercommonpackageEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagercommonpackage_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            NbManagercommonpackageEntity obj=null;
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
		
		#region  Select
		
		/// <summary>
        /// Select
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>NbManagercommonpackageEntity</returns>
        /// <remarks>2016/8/4 10:06:45</remarks>
        public NbManagercommonpackageEntity Select( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NBManagerCommonPackage_Select");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            NbManagercommonpackageEntity obj=null;
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
        /// <returns>NbManagercommonpackageEntity列表</returns>
        /// <remarks>2016/8/4 10:06:45</remarks>
        public List<NbManagercommonpackageEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagercommonpackage_GetAll");
            

            
            List<NbManagercommonpackageEntity> list = null;
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
        /// <remarks>2016/8/4 10:06:45</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagercommonpackage_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
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
        /// <remarks>2016/8/4 10:06:45</remarks>
        public bool Insert(NbManagercommonpackageEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagercommonpackage_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Common1", DbType.AnsiString, entity.Common1);
			database.AddInParameter(commandWrapper, "@Common2", DbType.AnsiString, entity.Common2);
			database.AddInParameter(commandWrapper, "@Common3", DbType.AnsiString, entity.Common3);
			database.AddInParameter(commandWrapper, "@Common4", DbType.AnsiString, entity.Common4);
			database.AddInParameter(commandWrapper, "@Rowtime", DbType.DateTime, entity.Rowtime);
			database.AddParameter(commandWrapper, "@Idx", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
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
        /// <remarks>2016/8/4 10:06:45</remarks>
        public bool Update(NbManagercommonpackageEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagercommonpackage_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@Common1", DbType.AnsiString, entity.Common1);
			database.AddInParameter(commandWrapper, "@Common2", DbType.AnsiString, entity.Common2);
			database.AddInParameter(commandWrapper, "@Common3", DbType.AnsiString, entity.Common3);
			database.AddInParameter(commandWrapper, "@Common4", DbType.AnsiString, entity.Common4);
			database.AddInParameter(commandWrapper, "@Rowtime", DbType.DateTime, entity.Rowtime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
