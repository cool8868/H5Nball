

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
    
    public partial class ConfigReveationcheckawaryProvider
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
		/// 将IDataReader的当前记录读取到ConfigReveationcheckawaryEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigReveationcheckawaryEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigReveationcheckawaryEntity();
			
            obj.Mark = (System.Int32) reader["Mark"];
            obj.LittleLevels = (System.Int32) reader["LittleLevels"];
            obj.NpcId = (System.Guid) reader["NpcId"];
            obj.CheckpointPlayers = (System.String) reader["CheckpointPlayers"];
            obj.Describe = (System.String) reader["Describe"];
            obj.TheStory = (System.String) reader["TheStory"];
            obj.AwaryTheCourageTo = (System.String) reader["AwaryTheCourageTo"];
            obj.Exp = (System.Int32) reader["Exp"];
            obj.Gold = (System.Int32) reader["Gold"];
            obj.Team = (System.String) reader["Team"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigReveationcheckawaryEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigReveationcheckawaryEntity>();
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
        public ConfigReveationcheckawaryProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigReveationcheckawaryProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="mark">mark</param>
		/// <param name="littleLevels">littleLevels</param>
        /// <returns>ConfigReveationcheckawaryEntity</returns>
        /// <remarks>2014/11/6 13:32:42</remarks>
        public ConfigReveationcheckawaryEntity GetById( System.Int32 mark, System.Int32 littleLevels)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigReveationcheckawary_GetById");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, mark);
			database.AddInParameter(commandWrapper, "@LittleLevels", DbType.Int32, littleLevels);

            
            ConfigReveationcheckawaryEntity obj=null;
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
        /// <returns>ConfigReveationcheckawaryEntity列表</returns>
        /// <remarks>2014/11/6 13:32:42</remarks>
        public List<ConfigReveationcheckawaryEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigReveationcheckawary_GetAll");
            

            
            List<ConfigReveationcheckawaryEntity> list = null;
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
		/// <param name="mark">mark</param>
		/// <param name="littleLevels">littleLevels</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/11/6 13:32:42</remarks>
        public bool Delete ( System.Int32 mark, System.Int32 littleLevels,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigReveationcheckawary_Delete");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, mark);
			database.AddInParameter(commandWrapper, "@LittleLevels", DbType.Int32, littleLevels);

            
            
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
        /// <remarks>2014/11/6 13:32:42</remarks>
        public bool Insert(ConfigReveationcheckawaryEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/11/6 13:32:42</remarks>
        public bool Insert(ConfigReveationcheckawaryEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigReveationcheckawary_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@LittleLevels", DbType.Int32, entity.LittleLevels);
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, entity.NpcId);
			database.AddInParameter(commandWrapper, "@CheckpointPlayers", DbType.String, entity.CheckpointPlayers);
			database.AddInParameter(commandWrapper, "@Describe", DbType.String, entity.Describe);
			database.AddInParameter(commandWrapper, "@TheStory", DbType.String, entity.TheStory);
			database.AddInParameter(commandWrapper, "@AwaryTheCourageTo", DbType.AnsiString, entity.AwaryTheCourageTo);
			database.AddInParameter(commandWrapper, "@Exp", DbType.Int32, entity.Exp);
			database.AddInParameter(commandWrapper, "@Gold", DbType.Int32, entity.Gold);
			database.AddInParameter(commandWrapper, "@Team", DbType.AnsiString, entity.Team);

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
        /// <remarks>2014/11/6 13:32:42</remarks>
        public bool Update(ConfigReveationcheckawaryEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/11/6 13:32:42</remarks>
        public bool Update(ConfigReveationcheckawaryEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigReveationcheckawary_Update");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@LittleLevels", DbType.Int32, entity.LittleLevels);
			database.AddInParameter(commandWrapper, "@NpcId", DbType.Guid, entity.NpcId);
			database.AddInParameter(commandWrapper, "@CheckpointPlayers", DbType.String, entity.CheckpointPlayers);
			database.AddInParameter(commandWrapper, "@Describe", DbType.String, entity.Describe);
			database.AddInParameter(commandWrapper, "@TheStory", DbType.String, entity.TheStory);
			database.AddInParameter(commandWrapper, "@AwaryTheCourageTo", DbType.AnsiString, entity.AwaryTheCourageTo);
			database.AddInParameter(commandWrapper, "@Exp", DbType.Int32, entity.Exp);
			database.AddInParameter(commandWrapper, "@Gold", DbType.Int32, entity.Gold);
			database.AddInParameter(commandWrapper, "@Team", DbType.AnsiString, entity.Team);

            
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

