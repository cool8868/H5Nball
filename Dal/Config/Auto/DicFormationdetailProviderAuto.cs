

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
    
    public partial class DicFormationdetailProvider
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
		/// 将IDataReader的当前记录读取到DicFormationdetailEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public DicFormationdetailEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new DicFormationdetailEntity();
			
            obj.Idx = (System.Int32) reader["Idx"];
            obj.FormationId = (System.Int32) reader["FormationId"];
            obj.Position = (System.Int32) reader["Position"];
            obj.Coordinate = (System.String) reader["Coordinate"];
            obj.SpecificPoint = (System.Int32) reader["SpecificPoint"];
            obj.SpecificPointDesc = (System.String) reader["SpecificPointDesc"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<DicFormationdetailEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<DicFormationdetailEntity>();
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
        public DicFormationdetailProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public DicFormationdetailProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>DicFormationdetailEntity</returns>
        /// <remarks>2016/5/18 18:34:06</remarks>
        public DicFormationdetailEntity GetById( System.Int32 idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicFormationdetail_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, idx);

            
            DicFormationdetailEntity obj=null;
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
        /// <returns>DicFormationdetailEntity列表</returns>
        /// <remarks>2016/5/18 18:34:06</remarks>
        public List<DicFormationdetailEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicFormationdetail_GetAll");
            

            
            List<DicFormationdetailEntity> list = null;
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
        /// <remarks>2016/5/18 18:34:06</remarks>
        public bool Delete ( System.Int32 idx,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_DicFormationdetail_Delete");
            
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
        /// <remarks>2016/5/18 18:34:06</remarks>
        public bool Insert(DicFormationdetailEntity entity)
        {
            return Insert(entity,null);
        }
        
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/18 18:34:06</remarks>
        public bool Insert(DicFormationdetailEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicFormationdetail_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@FormationId", DbType.Int32, entity.FormationId);
			database.AddInParameter(commandWrapper, "@Position", DbType.Int32, entity.Position);
			database.AddInParameter(commandWrapper, "@Coordinate", DbType.AnsiString, entity.Coordinate);
			database.AddInParameter(commandWrapper, "@SpecificPoint", DbType.Int32, entity.SpecificPoint);
			database.AddInParameter(commandWrapper, "@SpecificPointDesc", DbType.AnsiString, entity.SpecificPointDesc);

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
        /// <remarks>2016/5/18 18:34:06</remarks>
        public bool Update(DicFormationdetailEntity entity)
        {
            return Update(entity,null);
        }
            
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/5/18 18:34:06</remarks>
        public bool Update(DicFormationdetailEntity entity,DbTransaction trans)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_DicFormationdetail_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Int32, entity.Idx);
			database.AddInParameter(commandWrapper, "@FormationId", DbType.Int32, entity.FormationId);
			database.AddInParameter(commandWrapper, "@Position", DbType.Int32, entity.Position);
			database.AddInParameter(commandWrapper, "@Coordinate", DbType.AnsiString, entity.Coordinate);
			database.AddInParameter(commandWrapper, "@SpecificPoint", DbType.Int32, entity.SpecificPoint);
			database.AddInParameter(commandWrapper, "@SpecificPointDesc", DbType.AnsiString, entity.SpecificPointDesc);

            
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

