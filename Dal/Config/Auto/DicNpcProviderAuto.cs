

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
    
    public partial class DicNpcProvider
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
		/// 将IDataReader的当前记录读取到DicNpcEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicNpcEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicNpcEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.Type = (System.Int32) reader["Type"];
            obj.Name = (System.String) reader["Name"];
            obj.Logo = (System.Int32) reader["Logo"];
            obj.FormationId = (System.Int32) reader["FormationId"];
            obj.FormationLevel = (System.Int32) reader["FormationLevel"];
            obj.TeammemberLevel = (System.Int32) reader["TeammemberLevel"];
            obj.PlayerCardStrength = (System.Int32) reader["PlayerCardStrength"];
            obj.CoachId = (System.Int32) reader["CoachId"];
            obj.DoTalent = (System.String) reader["DoTalent"];
            obj.DoWill = (System.String) reader["DoWill"];
            obj.ManagerSkill = (System.String) reader["ManagerSkill"];
            obj.CombLevel = (System.Int32) reader["CombLevel"];
            obj.Buff = (System.Int32) reader["Buff"];
            obj.PropertyPoint = (System.Int32) reader["PropertyPoint"];
            obj.TP1 = (System.Int32) reader["TP1"];
            obj.TE1 = (System.Int32) reader["TE1"];
            obj.TS1 = (System.String) reader["TS1"];
            obj.TP2 = (System.Int32) reader["TP2"];
            obj.TE2 = (System.Int32) reader["TE2"];
            obj.TS2 = (System.String) reader["TS2"];
            obj.TP3 = (System.Int32) reader["TP3"];
            obj.TE3 = (System.Int32) reader["TE3"];
            obj.TS3 = (System.String) reader["TS3"];
            obj.TP4 = (System.Int32) reader["TP4"];
            obj.TE4 = (System.Int32) reader["TE4"];
            obj.TS4 = (System.String) reader["TS4"];
            obj.TP5 = (System.Int32) reader["TP5"];
            obj.TE5 = (System.Int32) reader["TE5"];
            obj.TS5 = (System.String) reader["TS5"];
            obj.TP6 = (System.Int32) reader["TP6"];
            obj.TE6 = (System.Int32) reader["TE6"];
            obj.TS6 = (System.String) reader["TS6"];
            obj.TP7 = (System.Int32) reader["TP7"];
            obj.TE7 = (System.Int32) reader["TE7"];
            obj.TS7 = (System.String) reader["TS7"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicNpcEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicNpcEntity>();
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
        public DicNpcProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicNpcProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicNpcEntity</returns>
        /// <remarks>2016/1/18 14:25:07</remarks>
        public DicNpcEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicNpc_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            DicNpcEntity obj=null;
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
        /// <returns>DicNpcEntity列表</returns>
        /// <remarks>2016/1/18 14:25:07</remarks>
        public List<DicNpcEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicNpc_GetAll");
            

            
            List<DicNpcEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetAllForCross
		
		/// <summary>
        /// GetAllForCross
        /// </summary>
        /// <returns>DicNpcEntity列表</returns>
        /// <remarks>2016/1/18 14:25:07</remarks>
        public List<DicNpcEntity> GetAllForCross()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicNpc_GetAllForCross");
            

            
            List<DicNpcEntity> list = null;
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
        /// <remarks>2016/1/18 14:25:07</remarks>
        public bool Delete ( System.Guid idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicNpc_Delete");
            
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
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/18 14:25:07</remarks>
        public bool Insert(DicNpcEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/18 14:25:07</remarks>
        public bool Insert(DicNpcEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicNpc_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Logo", DbType.Int32, entity.Logo);
			database.AddInParameter(commandWrapper, "@FormationId", DbType.Int32, entity.FormationId);
			database.AddInParameter(commandWrapper, "@FormationLevel", DbType.Int32, entity.FormationLevel);
			database.AddInParameter(commandWrapper, "@TeammemberLevel", DbType.Int32, entity.TeammemberLevel);
			database.AddInParameter(commandWrapper, "@PlayerCardStrength", DbType.Int32, entity.PlayerCardStrength);
			database.AddInParameter(commandWrapper, "@CoachId", DbType.Int32, entity.CoachId);
			database.AddInParameter(commandWrapper, "@DoTalent", DbType.AnsiString, entity.DoTalent);
			database.AddInParameter(commandWrapper, "@DoWill", DbType.AnsiString, entity.DoWill);
			database.AddInParameter(commandWrapper, "@ManagerSkill", DbType.AnsiString, entity.ManagerSkill);
			database.AddInParameter(commandWrapper, "@CombLevel", DbType.Int32, entity.CombLevel);
			database.AddInParameter(commandWrapper, "@Buff", DbType.Int32, entity.Buff);
			database.AddInParameter(commandWrapper, "@PropertyPoint", DbType.Int32, entity.PropertyPoint);
			database.AddInParameter(commandWrapper, "@TP1", DbType.Int32, entity.TP1);
			database.AddInParameter(commandWrapper, "@TE1", DbType.Int32, entity.TE1);
			database.AddInParameter(commandWrapper, "@TS1", DbType.AnsiString, entity.TS1);
			database.AddInParameter(commandWrapper, "@TP2", DbType.Int32, entity.TP2);
			database.AddInParameter(commandWrapper, "@TE2", DbType.Int32, entity.TE2);
			database.AddInParameter(commandWrapper, "@TS2", DbType.AnsiString, entity.TS2);
			database.AddInParameter(commandWrapper, "@TP3", DbType.Int32, entity.TP3);
			database.AddInParameter(commandWrapper, "@TE3", DbType.Int32, entity.TE3);
			database.AddInParameter(commandWrapper, "@TS3", DbType.AnsiString, entity.TS3);
			database.AddInParameter(commandWrapper, "@TP4", DbType.Int32, entity.TP4);
			database.AddInParameter(commandWrapper, "@TE4", DbType.Int32, entity.TE4);
			database.AddInParameter(commandWrapper, "@TS4", DbType.AnsiString, entity.TS4);
			database.AddInParameter(commandWrapper, "@TP5", DbType.Int32, entity.TP5);
			database.AddInParameter(commandWrapper, "@TE5", DbType.Int32, entity.TE5);
			database.AddInParameter(commandWrapper, "@TS5", DbType.AnsiString, entity.TS5);
			database.AddInParameter(commandWrapper, "@TP6", DbType.Int32, entity.TP6);
			database.AddInParameter(commandWrapper, "@TE6", DbType.Int32, entity.TE6);
			database.AddInParameter(commandWrapper, "@TS6", DbType.AnsiString, entity.TS6);
			database.AddInParameter(commandWrapper, "@TP7", DbType.Int32, entity.TP7);
			database.AddInParameter(commandWrapper, "@TE7", DbType.Int32, entity.TE7);
			database.AddInParameter(commandWrapper, "@TS7", DbType.AnsiString, entity.TS7);
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
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>2016/1/18 14:25:07</remarks>
        public bool Update(DicNpcEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/1/18 14:25:07</remarks>
        public bool Update(DicNpcEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicNpc_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Logo", DbType.Int32, entity.Logo);
			database.AddInParameter(commandWrapper, "@FormationId", DbType.Int32, entity.FormationId);
			database.AddInParameter(commandWrapper, "@FormationLevel", DbType.Int32, entity.FormationLevel);
			database.AddInParameter(commandWrapper, "@TeammemberLevel", DbType.Int32, entity.TeammemberLevel);
			database.AddInParameter(commandWrapper, "@PlayerCardStrength", DbType.Int32, entity.PlayerCardStrength);
			database.AddInParameter(commandWrapper, "@CoachId", DbType.Int32, entity.CoachId);
			database.AddInParameter(commandWrapper, "@DoTalent", DbType.AnsiString, entity.DoTalent);
			database.AddInParameter(commandWrapper, "@DoWill", DbType.AnsiString, entity.DoWill);
			database.AddInParameter(commandWrapper, "@ManagerSkill", DbType.AnsiString, entity.ManagerSkill);
			database.AddInParameter(commandWrapper, "@CombLevel", DbType.Int32, entity.CombLevel);
			database.AddInParameter(commandWrapper, "@Buff", DbType.Int32, entity.Buff);
			database.AddInParameter(commandWrapper, "@PropertyPoint", DbType.Int32, entity.PropertyPoint);
			database.AddInParameter(commandWrapper, "@TP1", DbType.Int32, entity.TP1);
			database.AddInParameter(commandWrapper, "@TE1", DbType.Int32, entity.TE1);
			database.AddInParameter(commandWrapper, "@TS1", DbType.AnsiString, entity.TS1);
			database.AddInParameter(commandWrapper, "@TP2", DbType.Int32, entity.TP2);
			database.AddInParameter(commandWrapper, "@TE2", DbType.Int32, entity.TE2);
			database.AddInParameter(commandWrapper, "@TS2", DbType.AnsiString, entity.TS2);
			database.AddInParameter(commandWrapper, "@TP3", DbType.Int32, entity.TP3);
			database.AddInParameter(commandWrapper, "@TE3", DbType.Int32, entity.TE3);
			database.AddInParameter(commandWrapper, "@TS3", DbType.AnsiString, entity.TS3);
			database.AddInParameter(commandWrapper, "@TP4", DbType.Int32, entity.TP4);
			database.AddInParameter(commandWrapper, "@TE4", DbType.Int32, entity.TE4);
			database.AddInParameter(commandWrapper, "@TS4", DbType.AnsiString, entity.TS4);
			database.AddInParameter(commandWrapper, "@TP5", DbType.Int32, entity.TP5);
			database.AddInParameter(commandWrapper, "@TE5", DbType.Int32, entity.TE5);
			database.AddInParameter(commandWrapper, "@TS5", DbType.AnsiString, entity.TS5);
			database.AddInParameter(commandWrapper, "@TP6", DbType.Int32, entity.TP6);
			database.AddInParameter(commandWrapper, "@TE6", DbType.Int32, entity.TE6);
			database.AddInParameter(commandWrapper, "@TS6", DbType.AnsiString, entity.TS6);
			database.AddInParameter(commandWrapper, "@TP7", DbType.Int32, entity.TP7);
			database.AddInParameter(commandWrapper, "@TE7", DbType.Int32, entity.TE7);
			database.AddInParameter(commandWrapper, "@TS7", DbType.AnsiString, entity.TS7);

            
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

