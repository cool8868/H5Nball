

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
    
    public partial class FriendinviteProvider
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
		/// 将IDataReader的当前记录读取到FriendinviteEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public FriendinviteEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new FriendinviteEntity();
			
            obj.ByAccount = (System.String) reader["ByAccount"];
            obj.Account = (System.String) reader["Account"];
            obj.Level = (System.Int32) reader["Level"];
            obj.IsPrize = (System.Boolean) reader["IsPrize"];
            obj.MayPrize = (System.Int32) reader["MayPrize"];
            obj.AlreadyPrize = (System.Int32) reader["AlreadyPrize"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<FriendinviteEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<FriendinviteEntity>();
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
        public FriendinviteProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public FriendinviteProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="byAccount">byAccount</param>
        /// <returns>FriendinviteEntity</returns>
        /// <remarks>2016/6/12 13:25:38</remarks>
        public FriendinviteEntity GetById( System.String byAccount)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Friendinvite_GetById");
            
			database.AddInParameter(commandWrapper, "@ByAccount", DbType.AnsiString, byAccount);

            
            FriendinviteEntity obj=null;
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
        /// <returns>FriendinviteEntity列表</returns>
        /// <remarks>2016/6/12 13:25:38</remarks>
        public List<FriendinviteEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Friendinvite_GetAll");
            

            
            List<FriendinviteEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAllNumber
		
		/// <summary>
        /// GetAllNumber
        /// </summary>

        /// <returns>Int32</returns>
        /// <remarks>2016/6/12 13:25:38</remarks>
        public Int32 GetAllNumber ()
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_FriendInvite_GetAllNumber");
            

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="byAccount">byAccount</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/12 13:25:38</remarks>
        public bool Delete ( System.String byAccount,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Friendinvite_Delete");
            
			database.AddInParameter(commandWrapper, "@ByAccount", DbType.AnsiString, byAccount);

            
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
		
		#region  SavePrize
		
		/// <summary>
        /// SavePrize
        /// </summary>
		/// <param name="account">account</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/12 13:25:39</remarks>
        public bool SavePrize ( System.String account,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_FriendInvite_SavePrize");
            
			database.AddInParameter(commandWrapper, "@account", DbType.AnsiString, account);

            
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
        /// <remarks>2016/6/12 13:25:39</remarks>
        public bool Insert(FriendinviteEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Friendinvite_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ByAccount", DbType.AnsiString, entity.ByAccount);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@IsPrize", DbType.Boolean, entity.IsPrize);
			database.AddInParameter(commandWrapper, "@MayPrize", DbType.Int32, entity.MayPrize);
			database.AddInParameter(commandWrapper, "@AlreadyPrize", DbType.Int32, entity.AlreadyPrize);

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
        /// <remarks>2016/6/12 13:25:39</remarks>
        public bool Update(FriendinviteEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Friendinvite_Update");
            
			database.AddInParameter(commandWrapper, "@ByAccount", DbType.AnsiString, entity.ByAccount);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@IsPrize", DbType.Boolean, entity.IsPrize);
			database.AddInParameter(commandWrapper, "@MayPrize", DbType.Int32, entity.MayPrize);
			database.AddInParameter(commandWrapper, "@AlreadyPrize", DbType.Int32, entity.AlreadyPrize);

            
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
