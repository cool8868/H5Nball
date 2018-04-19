

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
    
    public partial class DicPlayerProvider
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
		/// 将IDataReader的当前记录读取到DicPlayerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicPlayerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicPlayerEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Name = (System.String) reader["Name"];
            obj.Area = (System.Int32) reader["Area"];
            obj.AllPosition = (System.String) reader["AllPosition"];
            obj.Position = (System.Int32) reader["Position"];
            obj.PositionDesc = (System.String) reader["PositionDesc"];
            obj.CardLevel = (System.Int32) reader["CardLevel"];
            obj.KpiLevel = (System.String) reader["KpiLevel"];
            obj.LeagueLevel = (System.Int32) reader["LeagueLevel"];
            obj.NameEn = (System.String) reader["NameEn"];
            obj.Specific = (System.Double) reader["Specific"];
            obj.Kpi = (System.Double) reader["Kpi"];
            obj.Capacity = (System.Int32) reader["Capacity"];
            obj.Speed = (System.Double) reader["Speed"];
            obj.Shoot = (System.Double) reader["Shoot"];
            obj.FreeKick = (System.Double) reader["FreeKick"];
            obj.Balance = (System.Double) reader["Balance"];
            obj.Physique = (System.Double) reader["Physique"];
            obj.Power = (System.Double) reader["Power"];
            obj.Aggression = (System.Double) reader["Aggression"];
            obj.Disturb = (System.Double) reader["Disturb"];
            obj.Interception = (System.Double) reader["Interception"];
            obj.Dribble = (System.Double) reader["Dribble"];
            obj.Pass = (System.Double) reader["Pass"];
            obj.Mentality = (System.Double) reader["Mentality"];
            obj.Response = (System.Double) reader["Response"];
            obj.Positioning = (System.Double) reader["Positioning"];
            obj.HandControl = (System.Double) reader["HandControl"];
            obj.Acceleration = (System.Double) reader["Acceleration"];
            obj.Bounce = (System.Double) reader["Bounce"];
            obj.Club = (System.String) reader["Club"];
            obj.Birthday = (System.String) reader["Birthday"];
            obj.Stature = (System.Double) reader["Stature"];
            obj.Weight = (System.Double) reader["Weight"];
            obj.Nationality = (System.String) reader["Nationality"];
            obj.Description = (System.String) reader["Description"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicPlayerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicPlayerEntity>();
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
        public DicPlayerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicPlayerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicPlayerEntity</returns>
        /// <remarks>2016/5/18 20:12:02</remarks>
        public DicPlayerEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicPlayer_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DicPlayerEntity obj=null;
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
        /// <returns>DicPlayerEntity列表</returns>
        /// <remarks>2016/5/18 20:12:02</remarks>
        public List<DicPlayerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicPlayer_GetAll");
            

            
            List<DicPlayerEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAllForCache
		
		/// <summary>
        /// GetAllForCache
        /// </summary>
        /// <returns>DicPlayerEntity列表</returns>
        /// <remarks>2016/5/18 20:12:02</remarks>
        public List<DicPlayerEntity> GetAllForCache()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicPlayer_GetAllForCache");
            

            
            List<DicPlayerEntity> list = null;
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
        /// <remarks>2016/5/18 20:12:02</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicPlayer_Delete");
            
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
        /// <remarks>2016/5/18 20:12:02</remarks>
        public bool Insert(DicPlayerEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/18 20:12:02</remarks>
        public bool Insert(DicPlayerEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicPlayer_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Area", DbType.Int32, entity.Area);
			database.AddInParameter(commandWrapper, "@AllPosition", DbType.StringFixedLength, entity.AllPosition);
			database.AddInParameter(commandWrapper, "@Position", DbType.Int32, entity.Position);
			database.AddInParameter(commandWrapper, "@PositionDesc", DbType.AnsiString, entity.PositionDesc);
			database.AddInParameter(commandWrapper, "@CardLevel", DbType.Int32, entity.CardLevel);
			database.AddInParameter(commandWrapper, "@KpiLevel", DbType.AnsiString, entity.KpiLevel);
			database.AddInParameter(commandWrapper, "@LeagueLevel", DbType.Int32, entity.LeagueLevel);
			database.AddInParameter(commandWrapper, "@NameEn", DbType.String, entity.NameEn);
			database.AddInParameter(commandWrapper, "@Specific", DbType.Double, entity.Specific);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Double, entity.Kpi);
			database.AddInParameter(commandWrapper, "@Capacity", DbType.Int32, entity.Capacity);
			database.AddInParameter(commandWrapper, "@Speed", DbType.Double, entity.Speed);
			database.AddInParameter(commandWrapper, "@Shoot", DbType.Double, entity.Shoot);
			database.AddInParameter(commandWrapper, "@FreeKick", DbType.Double, entity.FreeKick);
			database.AddInParameter(commandWrapper, "@Balance", DbType.Double, entity.Balance);
			database.AddInParameter(commandWrapper, "@Physique", DbType.Double, entity.Physique);
			database.AddInParameter(commandWrapper, "@Power", DbType.Double, entity.Power);
			database.AddInParameter(commandWrapper, "@Aggression", DbType.Double, entity.Aggression);
			database.AddInParameter(commandWrapper, "@Disturb", DbType.Double, entity.Disturb);
			database.AddInParameter(commandWrapper, "@Interception", DbType.Double, entity.Interception);
			database.AddInParameter(commandWrapper, "@Dribble", DbType.Double, entity.Dribble);
			database.AddInParameter(commandWrapper, "@Pass", DbType.Double, entity.Pass);
			database.AddInParameter(commandWrapper, "@Mentality", DbType.Double, entity.Mentality);
			database.AddInParameter(commandWrapper, "@Response", DbType.Double, entity.Response);
			database.AddInParameter(commandWrapper, "@Positioning", DbType.Double, entity.Positioning);
			database.AddInParameter(commandWrapper, "@HandControl", DbType.Double, entity.HandControl);
			database.AddInParameter(commandWrapper, "@Acceleration", DbType.Double, entity.Acceleration);
			database.AddInParameter(commandWrapper, "@Bounce", DbType.Double, entity.Bounce);
			database.AddInParameter(commandWrapper, "@Club", DbType.String, entity.Club);
			database.AddInParameter(commandWrapper, "@Birthday", DbType.AnsiString, entity.Birthday);
			database.AddInParameter(commandWrapper, "@Stature", DbType.Double, entity.Stature);
			database.AddInParameter(commandWrapper, "@Weight", DbType.Double, entity.Weight);
			database.AddInParameter(commandWrapper, "@Nationality", DbType.String, entity.Nationality);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);

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
        /// <remarks>2016/5/18 20:12:02</remarks>
        public bool Update(DicPlayerEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/18 20:12:02</remarks>
        public bool Update(DicPlayerEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicPlayer_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Area", DbType.Int32, entity.Area);
			database.AddInParameter(commandWrapper, "@AllPosition", DbType.StringFixedLength, entity.AllPosition);
			database.AddInParameter(commandWrapper, "@Position", DbType.Int32, entity.Position);
			database.AddInParameter(commandWrapper, "@PositionDesc", DbType.AnsiString, entity.PositionDesc);
			database.AddInParameter(commandWrapper, "@CardLevel", DbType.Int32, entity.CardLevel);
			database.AddInParameter(commandWrapper, "@KpiLevel", DbType.AnsiString, entity.KpiLevel);
			database.AddInParameter(commandWrapper, "@LeagueLevel", DbType.Int32, entity.LeagueLevel);
			database.AddInParameter(commandWrapper, "@NameEn", DbType.String, entity.NameEn);
			database.AddInParameter(commandWrapper, "@Specific", DbType.Double, entity.Specific);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Double, entity.Kpi);
			database.AddInParameter(commandWrapper, "@Capacity", DbType.Int32, entity.Capacity);
			database.AddInParameter(commandWrapper, "@Speed", DbType.Double, entity.Speed);
			database.AddInParameter(commandWrapper, "@Shoot", DbType.Double, entity.Shoot);
			database.AddInParameter(commandWrapper, "@FreeKick", DbType.Double, entity.FreeKick);
			database.AddInParameter(commandWrapper, "@Balance", DbType.Double, entity.Balance);
			database.AddInParameter(commandWrapper, "@Physique", DbType.Double, entity.Physique);
			database.AddInParameter(commandWrapper, "@Power", DbType.Double, entity.Power);
			database.AddInParameter(commandWrapper, "@Aggression", DbType.Double, entity.Aggression);
			database.AddInParameter(commandWrapper, "@Disturb", DbType.Double, entity.Disturb);
			database.AddInParameter(commandWrapper, "@Interception", DbType.Double, entity.Interception);
			database.AddInParameter(commandWrapper, "@Dribble", DbType.Double, entity.Dribble);
			database.AddInParameter(commandWrapper, "@Pass", DbType.Double, entity.Pass);
			database.AddInParameter(commandWrapper, "@Mentality", DbType.Double, entity.Mentality);
			database.AddInParameter(commandWrapper, "@Response", DbType.Double, entity.Response);
			database.AddInParameter(commandWrapper, "@Positioning", DbType.Double, entity.Positioning);
			database.AddInParameter(commandWrapper, "@HandControl", DbType.Double, entity.HandControl);
			database.AddInParameter(commandWrapper, "@Acceleration", DbType.Double, entity.Acceleration);
			database.AddInParameter(commandWrapper, "@Bounce", DbType.Double, entity.Bounce);
			database.AddInParameter(commandWrapper, "@Club", DbType.String, entity.Club);
			database.AddInParameter(commandWrapper, "@Birthday", DbType.AnsiString, entity.Birthday);
			database.AddInParameter(commandWrapper, "@Stature", DbType.Double, entity.Stature);
			database.AddInParameter(commandWrapper, "@Weight", DbType.Double, entity.Weight);
			database.AddInParameter(commandWrapper, "@Nationality", DbType.String, entity.Nationality);
			database.AddInParameter(commandWrapper, "@Description", DbType.String, entity.Description);

            
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

