

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
    
    public partial class RevelationCheckpointProvider
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
		/// 将IDataReader的当前记录读取到RevelationCheckpointEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public RevelationCheckpointEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new RevelationCheckpointEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.ToDayGeneralNums = (System.Int32) reader["ToDayGeneralNums"];
            obj.CustomsPass = (System.Int32) reader["CustomsPass"];
            obj.Schedule = (System.Int32) reader["Schedule"];
            obj.IsGeneral = (System.Boolean) reader["IsGeneral"];
            obj.GeneralTime = (System.DateTime) reader["GeneralTime"];
            obj.IsGeneralAwary = (System.Boolean) reader["IsGeneralAwary"];
            obj.GeneralAwaryTime = (System.DateTime) reader["GeneralAwaryTime"];
            obj.AwaryItem = (System.String) reader["AwaryItem"];
            obj.States = (System.Int32) reader["States"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<RevelationCheckpointEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<RevelationCheckpointEntity>();
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
        public RevelationCheckpointProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public RevelationCheckpointProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>RevelationCheckpointEntity</returns>
        /// <remarks>2015/2/12 14:30:06</remarks>
        public RevelationCheckpointEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationCheckpoint_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            RevelationCheckpointEntity obj=null;
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
		
		#region  C_RevelationGetCheckoint
		
		/// <summary>
        /// C_RevelationGetCheckoint
        /// </summary>
		/// <param name="managerid">managerid</param>
		/// <param name="mark">mark</param>
        /// <returns>RevelationCheckpointEntity</returns>
        /// <remarks>2015/2/12 14:30:06</remarks>
        public RevelationCheckpointEntity C_RevelationGetCheckoint( System.Guid managerid, System.Int32 mark)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_RevelationGetCheckoint");
            
			database.AddInParameter(commandWrapper, "@managerid", DbType.Guid, managerid);
			database.AddInParameter(commandWrapper, "@mark", DbType.Int32, mark);

            
            RevelationCheckpointEntity obj=null;
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
        /// <returns>RevelationCheckpointEntity列表</returns>
        /// <remarks>2015/2/12 14:30:06</remarks>
        public List<RevelationCheckpointEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationCheckpoint_GetAll");
            

            
            List<RevelationCheckpointEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  C_RevelationCountGenerl
		
		/// <summary>
        /// C_RevelationCountGenerl
        /// </summary>
        /// <returns>RevelationCheckpointEntity列表</returns>
        /// <remarks>2015/2/12 14:30:06</remarks>
        public List<RevelationCheckpointEntity> C_RevelationCountGenerl( System.Guid managerid)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_RevelationCountGenerl");
            
			database.AddInParameter(commandWrapper, "@managerid", DbType.Guid, managerid);

            
            List<RevelationCheckpointEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  C_RevelationIsOpenVeteran
		
		/// <summary>
        /// C_RevelationIsOpenVeteran
        /// </summary>

        /// <returns>Int32</returns>
        /// <remarks>2015/2/12 14:30:06</remarks>
        public Int32 C_RevelationIsOpenVeteran ()
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_RevelationIsOpenVeteran");
            

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/2/12 14:30:06</remarks>
        public bool Delete ( System.Int32 idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_RevelationCheckpoint_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);

            
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
		
		#region  C_RevelationTheGame
		
		/// <summary>
        /// C_RevelationTheGame
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="mark">mark</param>
		/// <param name="littleLevels">littleLevels</param>
		/// <param name="goals">goals</param>
		/// <param name="toConcede">toConcede</param>
		/// <param name="isGeneral">isGeneral</param>
		/// <param name="courage">courage</param>
		/// <param name="isVictory">isVictory</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2015/2/12 14:30:06</remarks>
        public bool C_RevelationTheGame ( System.Guid managerId, System.Int32 mark, System.Int32 littleLevels, System.Int32 goals, System.Int32 toConcede, System.Boolean isGeneral, System.Int32 courage, System.Boolean isVictory,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_RevelationTheGame");
            
			database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@mark", DbType.Int32, mark);
			database.AddInParameter(commandWrapper, "@littleLevels", DbType.Int32, littleLevels);
			database.AddInParameter(commandWrapper, "@Goals", DbType.Int32, goals);
			database.AddInParameter(commandWrapper, "@ToConcede", DbType.Int32, toConcede);
			database.AddInParameter(commandWrapper, "@IsGeneral", DbType.Boolean, isGeneral);
			database.AddInParameter(commandWrapper, "@Courage", DbType.Int32, courage);
			database.AddInParameter(commandWrapper, "@IsVictory", DbType.Boolean, isVictory);

            
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
        /// <remarks>2015/2/12 14:30:06</remarks>
        public bool Insert(RevelationCheckpointEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationCheckpoint_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ToDayGeneralNums", DbType.Int32, entity.ToDayGeneralNums);
			database.AddInParameter(commandWrapper, "@CustomsPass", DbType.Int32, entity.CustomsPass);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@IsGeneral", DbType.Boolean, entity.IsGeneral);
			database.AddInParameter(commandWrapper, "@GeneralTime", DbType.DateTime, entity.GeneralTime);
			database.AddInParameter(commandWrapper, "@IsGeneralAwary", DbType.Boolean, entity.IsGeneralAwary);
			database.AddInParameter(commandWrapper, "@GeneralAwaryTime", DbType.DateTime, entity.GeneralAwaryTime);
			database.AddInParameter(commandWrapper, "@AwaryItem", DbType.AnsiString, entity.AwaryItem);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
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
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2015/2/12 14:30:06</remarks>
        public bool Update(RevelationCheckpointEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_RevelationCheckpoint_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@ToDayGeneralNums", DbType.Int32, entity.ToDayGeneralNums);
			database.AddInParameter(commandWrapper, "@CustomsPass", DbType.Int32, entity.CustomsPass);
			database.AddInParameter(commandWrapper, "@Schedule", DbType.Int32, entity.Schedule);
			database.AddInParameter(commandWrapper, "@IsGeneral", DbType.Boolean, entity.IsGeneral);
			database.AddInParameter(commandWrapper, "@GeneralTime", DbType.DateTime, entity.GeneralTime);
			database.AddInParameter(commandWrapper, "@IsGeneralAwary", DbType.Boolean, entity.IsGeneralAwary);
			database.AddInParameter(commandWrapper, "@GeneralAwaryTime", DbType.DateTime, entity.GeneralAwaryTime);
			database.AddInParameter(commandWrapper, "@AwaryItem", DbType.AnsiString, entity.AwaryItem);
			database.AddInParameter(commandWrapper, "@States", DbType.Int32, entity.States);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);

            
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

