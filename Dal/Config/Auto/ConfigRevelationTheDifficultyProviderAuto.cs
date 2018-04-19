

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
    
    public partial class ConfigRevelationthedifficultyProvider
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
		/// 将IDataReader的当前记录读取到ConfigRevelationthedifficultyEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public ConfigRevelationthedifficultyEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new ConfigRevelationthedifficultyEntity();
			
            obj.Mark = (System.Int32) reader["Mark"];
            obj.SmallClearance = (System.Int32) reader["SmallClearance"];
            obj.Bonuses = (System.Int32) reader["Bonuses"];
            obj.StrengthenLevel = (System.Int32) reader["StrengthenLevel"];
            obj.PlayersLlevel = (System.Int32) reader["PlayersLlevel"];
            obj.FormationLevel = (System.Int32) reader["FormationLevel"];
            obj.SkillNums = (System.Int32) reader["SkillNums"];
            obj.SkillTheQualityOf = (System.String) reader["SkillTheQualityOf"];
            obj.SkillLevel = (System.Int32) reader["SkillLevel"];
            obj.TheTeamWill = (System.Boolean) reader["TheTeamWill"];
            obj.CombinationSkillLevel = (System.Int32) reader["CombinationSkillLevel"];
            obj.EquipmentNums = (System.Int32) reader["EquipmentNums"];
            obj.EquipmentTheQualityOf = (System.String) reader["EquipmentTheQualityOf"];
            obj.EquipmentSet = (System.String) reader["EquipmentSet"];
            obj.AwaryTheCourageTo = (System.String) reader["AwaryTheCourageTo"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ConfigRevelationthedifficultyEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<ConfigRevelationthedifficultyEntity>();
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
        public ConfigRevelationthedifficultyProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public ConfigRevelationthedifficultyProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="mark">mark</param>
		/// <param name="smallClearance">smallClearance</param>
        /// <returns>ConfigRevelationthedifficultyEntity</returns>
        /// <remarks>2014/10/13 13:57:18</remarks>
        public ConfigRevelationthedifficultyEntity GetById( System.Int32 mark, System.Int32 smallClearance)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationthedifficulty_GetById");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, mark);
			database.AddInParameter(commandWrapper, "@SmallClearance", DbType.Int32, smallClearance);

            
            ConfigRevelationthedifficultyEntity obj=null;
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
        /// <returns>ConfigRevelationthedifficultyEntity列表</returns>
        /// <remarks>2014/10/13 13:57:18</remarks>
        public List<ConfigRevelationthedifficultyEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationthedifficulty_GetAll");
            

            
            List<ConfigRevelationthedifficultyEntity> list = null;
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
		/// <param name="smallClearance">smallClearance</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/10/13 13:57:18</remarks>
        public bool Delete ( System.Int32 mark, System.Int32 smallClearance,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_ConfigRevelationthedifficulty_Delete");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, mark);
			database.AddInParameter(commandWrapper, "@SmallClearance", DbType.Int32, smallClearance);

            
            
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
        /// <remarks>2014/10/13 13:57:18</remarks>
        public bool Insert(ConfigRevelationthedifficultyEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/10/13 13:57:18</remarks>
        public bool Insert(ConfigRevelationthedifficultyEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelationthedifficulty_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@SmallClearance", DbType.Int32, entity.SmallClearance);
			database.AddInParameter(commandWrapper, "@Bonuses", DbType.Int32, entity.Bonuses);
			database.AddInParameter(commandWrapper, "@StrengthenLevel", DbType.Int32, entity.StrengthenLevel);
			database.AddInParameter(commandWrapper, "@PlayersLlevel", DbType.Int32, entity.PlayersLlevel);
			database.AddInParameter(commandWrapper, "@FormationLevel", DbType.Int32, entity.FormationLevel);
			database.AddInParameter(commandWrapper, "@SkillNums", DbType.Int32, entity.SkillNums);
			database.AddInParameter(commandWrapper, "@SkillTheQualityOf", DbType.AnsiString, entity.SkillTheQualityOf);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@TheTeamWill", DbType.Boolean, entity.TheTeamWill);
			database.AddInParameter(commandWrapper, "@CombinationSkillLevel", DbType.Int32, entity.CombinationSkillLevel);
			database.AddInParameter(commandWrapper, "@EquipmentNums", DbType.Int32, entity.EquipmentNums);
			database.AddInParameter(commandWrapper, "@EquipmentTheQualityOf", DbType.AnsiString, entity.EquipmentTheQualityOf);
			database.AddInParameter(commandWrapper, "@EquipmentSet", DbType.AnsiString, entity.EquipmentSet);
			database.AddInParameter(commandWrapper, "@AwaryTheCourageTo", DbType.AnsiString, entity.AwaryTheCourageTo);

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
        /// <remarks>2014/10/13 13:57:18</remarks>
        public bool Update(ConfigRevelationthedifficultyEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2014/10/13 13:57:18</remarks>
        public bool Update(ConfigRevelationthedifficultyEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_ConfigRevelationthedifficulty_Update");
            
			database.AddInParameter(commandWrapper, "@Mark", DbType.Int32, entity.Mark);
			database.AddInParameter(commandWrapper, "@SmallClearance", DbType.Int32, entity.SmallClearance);
			database.AddInParameter(commandWrapper, "@Bonuses", DbType.Int32, entity.Bonuses);
			database.AddInParameter(commandWrapper, "@StrengthenLevel", DbType.Int32, entity.StrengthenLevel);
			database.AddInParameter(commandWrapper, "@PlayersLlevel", DbType.Int32, entity.PlayersLlevel);
			database.AddInParameter(commandWrapper, "@FormationLevel", DbType.Int32, entity.FormationLevel);
			database.AddInParameter(commandWrapper, "@SkillNums", DbType.Int32, entity.SkillNums);
			database.AddInParameter(commandWrapper, "@SkillTheQualityOf", DbType.AnsiString, entity.SkillTheQualityOf);
			database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, entity.SkillLevel);
			database.AddInParameter(commandWrapper, "@TheTeamWill", DbType.Boolean, entity.TheTeamWill);
			database.AddInParameter(commandWrapper, "@CombinationSkillLevel", DbType.Int32, entity.CombinationSkillLevel);
			database.AddInParameter(commandWrapper, "@EquipmentNums", DbType.Int32, entity.EquipmentNums);
			database.AddInParameter(commandWrapper, "@EquipmentTheQualityOf", DbType.AnsiString, entity.EquipmentTheQualityOf);
			database.AddInParameter(commandWrapper, "@EquipmentSet", DbType.AnsiString, entity.EquipmentSet);
			database.AddInParameter(commandWrapper, "@AwaryTheCourageTo", DbType.AnsiString, entity.AwaryTheCourageTo);

            
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

