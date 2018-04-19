

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
    
    public partial class A8csdkStartgameProvider
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
		/// 将IDataReader的当前记录读取到A8csdkStartgameEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public A8csdkStartgameEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new A8csdkStartgameEntity();
			
            obj.OpenId = (System.String) reader["OpenId"];
            obj.State = (System.String) reader["State"];
            obj.ServerId = (System.String) reader["ServerId"];
            obj.Pf = (System.String) reader["Pf"];
            obj.SessionId = (System.String) reader["SessionId"];
            obj.JsNeed = (System.String) reader["JsNeed"];
            obj.NickName = (System.String) reader["NickName"];
            obj.Common = (System.String) reader["Common"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<A8csdkStartgameEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<A8csdkStartgameEntity>();
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
        public A8csdkStartgameProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public A8csdkStartgameProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="openId">openId</param>
        /// <returns>A8csdkStartgameEntity</returns>
        /// <remarks>2016/6/22 10:52:45</remarks>
        public A8csdkStartgameEntity GetById( System.String openId)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_A8csdkStartgame_GetById");
            
			database.AddInParameter(commandWrapper, "@OpenId", DbType.AnsiString, openId);

            
            A8csdkStartgameEntity obj=null;
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
        /// <returns>A8csdkStartgameEntity列表</returns>
        /// <remarks>2016/6/22 10:52:45</remarks>
        public List<A8csdkStartgameEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_A8csdkStartgame_GetAll");
            

            
            List<A8csdkStartgameEntity> list = null;
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
		/// <param name="openId">openId</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/22 10:52:45</remarks>
        public bool Delete ( System.String openId,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_A8csdkStartgame_Delete");
            
			database.AddInParameter(commandWrapper, "@OpenId", DbType.AnsiString, openId);

            
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
        /// <remarks>2016/6/22 10:52:45</remarks>
        public bool Insert(A8csdkStartgameEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_A8csdkStartgame_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@OpenId", DbType.AnsiString, entity.OpenId);
			database.AddInParameter(commandWrapper, "@State", DbType.AnsiString, entity.State);
			database.AddInParameter(commandWrapper, "@ServerId", DbType.AnsiString, entity.ServerId);
			database.AddInParameter(commandWrapper, "@Pf", DbType.AnsiString, entity.Pf);
			database.AddInParameter(commandWrapper, "@SessionId", DbType.AnsiString, entity.SessionId);
			database.AddInParameter(commandWrapper, "@JsNeed", DbType.AnsiString, entity.JsNeed);
			database.AddInParameter(commandWrapper, "@NickName", DbType.AnsiString, entity.NickName);
			database.AddInParameter(commandWrapper, "@Common", DbType.AnsiString, entity.Common);

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
        /// <remarks>2016/6/22 10:52:45</remarks>
        public bool Update(A8csdkStartgameEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_A8csdkStartgame_Update");
            
			database.AddInParameter(commandWrapper, "@OpenId", DbType.AnsiString, entity.OpenId);
			database.AddInParameter(commandWrapper, "@State", DbType.AnsiString, entity.State);
			database.AddInParameter(commandWrapper, "@ServerId", DbType.AnsiString, entity.ServerId);
			database.AddInParameter(commandWrapper, "@Pf", DbType.AnsiString, entity.Pf);
			database.AddInParameter(commandWrapper, "@SessionId", DbType.AnsiString, entity.SessionId);
			database.AddInParameter(commandWrapper, "@JsNeed", DbType.AnsiString, entity.JsNeed);
			database.AddInParameter(commandWrapper, "@NickName", DbType.AnsiString, entity.NickName);
			database.AddInParameter(commandWrapper, "@Common", DbType.AnsiString, entity.Common);

            
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
