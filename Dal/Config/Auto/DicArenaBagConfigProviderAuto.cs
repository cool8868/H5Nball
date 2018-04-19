

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
    
    public partial class DicArenabagconfigProvider
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
		/// 将IDataReader的当前记录读取到DicArenabagconfigEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicArenabagconfigEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicArenabagconfigEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.Name = (System.String) reader["Name"];
            obj.Area = (System.String) reader["Area"];
            obj.AllPosition = (System.String) reader["AllPosition"];
            obj.Position = (System.String) reader["Position"];
            obj.PositionDesc = (System.String) reader["PositionDesc"];
            obj.CardLevel = (System.String) reader["CardLevel"];
            obj.KpiLevel = (System.String) reader["KpiLevel"];
            obj.LeagueLevel = (System.String) reader["LeagueLevel"];
            obj.NameEn = (System.String) reader["NameEn"];
            obj.Specific = (System.String) reader["Specific"];
            obj.Kpi = (System.String) reader["Kpi"];
            obj.Capacity = (System.String) reader["Capacity"];
            obj.Speed = (System.String) reader["Speed"];
            obj.Shoot = (System.String) reader["Shoot"];
            obj.FreeKick = (System.String) reader["FreeKick"];
            obj.Balance = (System.String) reader["Balance"];
            obj.Physique = (System.String) reader["Physique"];
            obj.Power = (System.String) reader["Power"];
            obj.Aggression = (System.String) reader["Aggression"];
            obj.Disturb = (System.String) reader["Disturb"];
            obj.Interception = (System.String) reader["Interception"];
            obj.Dribble = (System.String) reader["Dribble"];
            obj.Pass = (System.String) reader["Pass"];
            obj.Mentality = (System.String) reader["Mentality"];
            obj.Response = (System.String) reader["Response"];
            obj.Positioning = (System.String) reader["Positioning"];
            obj.HandControl = (System.String) reader["HandControl"];
            obj.Acceleration = (System.String) reader["Acceleration"];
            obj.Bounce = (System.String) reader["Bounce"];
            obj.Club = (System.String) reader["Club"];
            obj.Birthday = (System.String) reader["Birthday"];
            obj.Stature = (System.String) reader["Stature"];
            obj.Weight = (System.String) reader["Weight"];
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
        public List<DicArenabagconfigEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicArenabagconfigEntity>();
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
        public DicArenabagconfigProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicArenabagconfigProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicArenabagconfigEntity</returns>
        /// <remarks>2016/8/16 13:18:38</remarks>
        public DicArenabagconfigEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicArenabagconfig_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DicArenabagconfigEntity obj=null;
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
        /// <returns>DicArenabagconfigEntity列表</returns>
        /// <remarks>2016/8/16 13:18:38</remarks>
        public List<DicArenabagconfigEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicArenabagconfig_GetAll");
            

            
            List<DicArenabagconfigEntity> list = null;
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
        /// <remarks>2016/8/16 13:18:38</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicArenabagconfig_Delete");
            
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
        /// <remarks>2016/8/16 13:18:38</remarks>
        public bool Insert(DicArenabagconfigEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/8/16 13:18:38</remarks>
        public bool Insert(DicArenabagconfigEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicArenabagconfig_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Area", DbType.AnsiString, entity.Area);
			database.AddInParameter(commandWrapper, "@AllPosition", DbType.AnsiString, entity.AllPosition);
			database.AddInParameter(commandWrapper, "@Position", DbType.AnsiString, entity.Position);
			database.AddInParameter(commandWrapper, "@PositionDesc", DbType.AnsiString, entity.PositionDesc);
			database.AddInParameter(commandWrapper, "@CardLevel", DbType.AnsiString, entity.CardLevel);
			database.AddInParameter(commandWrapper, "@KpiLevel", DbType.AnsiString, entity.KpiLevel);
			database.AddInParameter(commandWrapper, "@LeagueLevel", DbType.AnsiString, entity.LeagueLevel);
			database.AddInParameter(commandWrapper, "@NameEn", DbType.AnsiString, entity.NameEn);
			database.AddInParameter(commandWrapper, "@Specific", DbType.AnsiString, entity.Specific);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.AnsiString, entity.Kpi);
			database.AddInParameter(commandWrapper, "@Capacity", DbType.AnsiString, entity.Capacity);
			database.AddInParameter(commandWrapper, "@Speed", DbType.AnsiString, entity.Speed);
			database.AddInParameter(commandWrapper, "@Shoot", DbType.AnsiString, entity.Shoot);
			database.AddInParameter(commandWrapper, "@FreeKick", DbType.AnsiString, entity.FreeKick);
			database.AddInParameter(commandWrapper, "@Balance", DbType.AnsiString, entity.Balance);
			database.AddInParameter(commandWrapper, "@Physique", DbType.AnsiString, entity.Physique);
			database.AddInParameter(commandWrapper, "@Power", DbType.AnsiString, entity.Power);
			database.AddInParameter(commandWrapper, "@Aggression", DbType.AnsiString, entity.Aggression);
			database.AddInParameter(commandWrapper, "@Disturb", DbType.AnsiString, entity.Disturb);
			database.AddInParameter(commandWrapper, "@Interception", DbType.AnsiString, entity.Interception);
			database.AddInParameter(commandWrapper, "@Dribble", DbType.AnsiString, entity.Dribble);
			database.AddInParameter(commandWrapper, "@Pass", DbType.AnsiString, entity.Pass);
			database.AddInParameter(commandWrapper, "@Mentality", DbType.AnsiString, entity.Mentality);
			database.AddInParameter(commandWrapper, "@Response", DbType.AnsiString, entity.Response);
			database.AddInParameter(commandWrapper, "@Positioning", DbType.AnsiString, entity.Positioning);
			database.AddInParameter(commandWrapper, "@HandControl", DbType.AnsiString, entity.HandControl);
			database.AddInParameter(commandWrapper, "@Acceleration", DbType.AnsiString, entity.Acceleration);
			database.AddInParameter(commandWrapper, "@Bounce", DbType.AnsiString, entity.Bounce);
			database.AddInParameter(commandWrapper, "@Club", DbType.String, entity.Club);
			database.AddInParameter(commandWrapper, "@Birthday", DbType.AnsiString, entity.Birthday);
			database.AddInParameter(commandWrapper, "@Stature", DbType.AnsiString, entity.Stature);
			database.AddInParameter(commandWrapper, "@Weight", DbType.AnsiString, entity.Weight);
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
        /// <remarks>2016/8/16 13:18:38</remarks>
        public bool Update(DicArenabagconfigEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/8/16 13:18:38</remarks>
        public bool Update(DicArenabagconfigEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicArenabagconfig_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Area", DbType.AnsiString, entity.Area);
			database.AddInParameter(commandWrapper, "@AllPosition", DbType.AnsiString, entity.AllPosition);
			database.AddInParameter(commandWrapper, "@Position", DbType.AnsiString, entity.Position);
			database.AddInParameter(commandWrapper, "@PositionDesc", DbType.AnsiString, entity.PositionDesc);
			database.AddInParameter(commandWrapper, "@CardLevel", DbType.AnsiString, entity.CardLevel);
			database.AddInParameter(commandWrapper, "@KpiLevel", DbType.AnsiString, entity.KpiLevel);
			database.AddInParameter(commandWrapper, "@LeagueLevel", DbType.AnsiString, entity.LeagueLevel);
			database.AddInParameter(commandWrapper, "@NameEn", DbType.AnsiString, entity.NameEn);
			database.AddInParameter(commandWrapper, "@Specific", DbType.AnsiString, entity.Specific);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.AnsiString, entity.Kpi);
			database.AddInParameter(commandWrapper, "@Capacity", DbType.AnsiString, entity.Capacity);
			database.AddInParameter(commandWrapper, "@Speed", DbType.AnsiString, entity.Speed);
			database.AddInParameter(commandWrapper, "@Shoot", DbType.AnsiString, entity.Shoot);
			database.AddInParameter(commandWrapper, "@FreeKick", DbType.AnsiString, entity.FreeKick);
			database.AddInParameter(commandWrapper, "@Balance", DbType.AnsiString, entity.Balance);
			database.AddInParameter(commandWrapper, "@Physique", DbType.AnsiString, entity.Physique);
			database.AddInParameter(commandWrapper, "@Power", DbType.AnsiString, entity.Power);
			database.AddInParameter(commandWrapper, "@Aggression", DbType.AnsiString, entity.Aggression);
			database.AddInParameter(commandWrapper, "@Disturb", DbType.AnsiString, entity.Disturb);
			database.AddInParameter(commandWrapper, "@Interception", DbType.AnsiString, entity.Interception);
			database.AddInParameter(commandWrapper, "@Dribble", DbType.AnsiString, entity.Dribble);
			database.AddInParameter(commandWrapper, "@Pass", DbType.AnsiString, entity.Pass);
			database.AddInParameter(commandWrapper, "@Mentality", DbType.AnsiString, entity.Mentality);
			database.AddInParameter(commandWrapper, "@Response", DbType.AnsiString, entity.Response);
			database.AddInParameter(commandWrapper, "@Positioning", DbType.AnsiString, entity.Positioning);
			database.AddInParameter(commandWrapper, "@HandControl", DbType.AnsiString, entity.HandControl);
			database.AddInParameter(commandWrapper, "@Acceleration", DbType.AnsiString, entity.Acceleration);
			database.AddInParameter(commandWrapper, "@Bounce", DbType.AnsiString, entity.Bounce);
			database.AddInParameter(commandWrapper, "@Club", DbType.String, entity.Club);
			database.AddInParameter(commandWrapper, "@Birthday", DbType.AnsiString, entity.Birthday);
			database.AddInParameter(commandWrapper, "@Stature", DbType.AnsiString, entity.Stature);
			database.AddInParameter(commandWrapper, "@Weight", DbType.AnsiString, entity.Weight);
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
