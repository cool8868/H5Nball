

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
    
    public partial class AnnouncementProvider
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
		/// 将IDataReader的当前记录读取到AnnouncementEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public AnnouncementEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new AnnouncementEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Platform = (System.String) reader["Platform"];
            obj.IsTop = (System.Boolean) reader["IsTop"];
            obj.IsRnable = (System.Boolean) reader["IsRnable"];
            obj.Title = (System.String) reader["Title"];
            obj.ContentString = (System.String) reader["ContentString"];
            obj.StartTime = (System.DateTime) reader["StartTime"];
            obj.EndTime = (System.DateTime) reader["EndTime"];
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
        public List<AnnouncementEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<AnnouncementEntity>();
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
        public AnnouncementProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public AnnouncementProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>AnnouncementEntity</returns>
        /// <remarks>2016/8/12 16:39:53</remarks>
        public AnnouncementEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Announcement_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            AnnouncementEntity obj=null;
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
        /// <returns>AnnouncementEntity列表</returns>
        /// <remarks>2016/8/12 16:39:53</remarks>
        public List<AnnouncementEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Announcement_GetAll");
            

            
            List<AnnouncementEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  SelectAnnouncement
		
		/// <summary>
        /// SelectAnnouncement
        /// </summary>
        /// <returns>AnnouncementEntity列表</returns>
        /// <remarks>2016/8/12 16:39:53</remarks>
        public List<AnnouncementEntity> SelectAnnouncement( System.String platform)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Announcement_SelectAnnouncement");
            
			database.AddInParameter(commandWrapper, "@Platform", DbType.AnsiString, platform);

            
            List<AnnouncementEntity> list = null;
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
        /// <remarks>2016/8/12 16:39:53</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_Announcement_Delete");
            
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
		
		#region  CloseAnnouncement
		
		/// <summary>
        /// CloseAnnouncement
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/8/12 16:39:53</remarks>
        public bool CloseAnnouncement ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Announcement_CloseAnnouncement");
            
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
		
		#region  Ranable
		
		/// <summary>
        /// Ranable
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="isTop">isTop</param>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/8/12 16:39:53</remarks>
        public bool Ranable ( System.Int32 idx, System.Boolean isTop, System.DateTime startTime, System.DateTime endTime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Announcement_Ranable");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@IsTop", DbType.Boolean, isTop);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);

            
            
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
		
		#region  Release
		
		/// <summary>
        /// Release
        /// </summary>
		/// <param name="platform">platform</param>
		/// <param name="isTop">isTop</param>
		/// <param name="title">title</param>
		/// <param name="contentString">contentString</param>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/8/12 16:39:53</remarks>
        public bool Release ( System.String platform, System.Boolean isTop, System.String title, System.String contentString, System.DateTime startTime, System.DateTime endTime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Announcement_Release");
            
			database.AddInParameter(commandWrapper, "@Platform", DbType.AnsiString, platform);
			database.AddInParameter(commandWrapper, "@IsTop", DbType.Boolean, isTop);
			database.AddInParameter(commandWrapper, "@Title", DbType.AnsiString, title);
			database.AddInParameter(commandWrapper, "@ContentString", DbType.AnsiString, contentString);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);

            
            
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
        /// <remarks>2016/8/12 16:39:53</remarks>
        public bool Insert(AnnouncementEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/8/12 16:39:53</remarks>
        public bool Insert(AnnouncementEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Announcement_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Platform", DbType.AnsiString, entity.Platform);
			database.AddInParameter(commandWrapper, "@IsTop", DbType.Boolean, entity.IsTop);
			database.AddInParameter(commandWrapper, "@IsRnable", DbType.Boolean, entity.IsRnable);
			database.AddInParameter(commandWrapper, "@Title", DbType.AnsiString, entity.Title);
			database.AddInParameter(commandWrapper, "@ContentString", DbType.AnsiString, entity.ContentString);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/8/12 16:39:53</remarks>
        public bool Update(AnnouncementEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/8/12 16:39:53</remarks>
        public bool Update(AnnouncementEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Announcement_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Platform", DbType.AnsiString, entity.Platform);
			database.AddInParameter(commandWrapper, "@IsTop", DbType.Boolean, entity.IsTop);
			database.AddInParameter(commandWrapper, "@IsRnable", DbType.Boolean, entity.IsRnable);
			database.AddInParameter(commandWrapper, "@Title", DbType.AnsiString, entity.Title);
			database.AddInParameter(commandWrapper, "@ContentString", DbType.AnsiString, entity.ContentString);
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, entity.StartTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, entity.EndTime);
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

            entity.Idx=(System.Int32)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
