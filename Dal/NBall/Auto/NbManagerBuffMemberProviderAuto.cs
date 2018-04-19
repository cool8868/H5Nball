

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
    
    public partial class NbManagerbuffmemberProvider
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
		/// 将IDataReader的当前记录读取到NbManagerbuffmemberEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbManagerbuffmemberEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbManagerbuffmemberEntity();
			
            obj.Id = (System.Int64) reader["Id"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.Tid = (System.Guid) reader["Tid"];
            obj.Pid = (System.Int32) reader["Pid"];
            obj.PPos = (System.Int32) reader["PPos"];
            obj.PPosOn = (System.Int32) reader["PPosOn"];
            obj.Kpi = (System.Int32) reader["Kpi"];
            obj.Level = (System.Int32) reader["Level"];
            obj.Strength = (System.Int32) reader["Strength"];
            obj.ShowOrder = (System.Int32) reader["ShowOrder"];
            obj.IsMain = (System.Boolean) reader["IsMain"];
            obj.ReadySkills = (System.String) reader["ReadySkills"];
            obj.LiveSkills = (System.String) reader["LiveSkills"];
            obj.SpeedConst = (System.Double) reader["SpeedConst"];
            obj.SpeedCount = (System.Double) reader["SpeedCount"];
            obj.ShootConst = (System.Double) reader["ShootConst"];
            obj.ShootCount = (System.Double) reader["ShootCount"];
            obj.FreeKickConst = (System.Double) reader["FreeKickConst"];
            obj.FreeKickCount = (System.Double) reader["FreeKickCount"];
            obj.BalanceConst = (System.Double) reader["BalanceConst"];
            obj.BalanceCount = (System.Double) reader["BalanceCount"];
            obj.PhysiqueConst = (System.Double) reader["PhysiqueConst"];
            obj.PhysiqueCount = (System.Double) reader["PhysiqueCount"];
            obj.PowerConst = (System.Double) reader["PowerConst"];
            obj.PowerCount = (System.Double) reader["PowerCount"];
            obj.AggressionConst = (System.Double) reader["AggressionConst"];
            obj.AggressionCount = (System.Double) reader["AggressionCount"];
            obj.DisturbConst = (System.Double) reader["DisturbConst"];
            obj.DisturbCount = (System.Double) reader["DisturbCount"];
            obj.InterceptionConst = (System.Double) reader["InterceptionConst"];
            obj.InterceptionCount = (System.Double) reader["InterceptionCount"];
            obj.DribbleConst = (System.Double) reader["DribbleConst"];
            obj.DribbleCount = (System.Double) reader["DribbleCount"];
            obj.PassConst = (System.Double) reader["PassConst"];
            obj.PassCount = (System.Double) reader["PassCount"];
            obj.MentalityConst = (System.Double) reader["MentalityConst"];
            obj.MentalityCount = (System.Double) reader["MentalityCount"];
            obj.ResponseConst = (System.Double) reader["ResponseConst"];
            obj.ResponseCount = (System.Double) reader["ResponseCount"];
            obj.PositioningConst = (System.Double) reader["PositioningConst"];
            obj.PositioningCount = (System.Double) reader["PositioningCount"];
            obj.HandControlConst = (System.Double) reader["HandControlConst"];
            obj.HandControlCount = (System.Double) reader["HandControlCount"];
            obj.AccelerationConst = (System.Double) reader["AccelerationConst"];
            obj.AccelerationCount = (System.Double) reader["AccelerationCount"];
            obj.BounceConst = (System.Double) reader["BounceConst"];
            obj.BounceCount = (System.Double) reader["BounceCount"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
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
        public List<NbManagerbuffmemberEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbManagerbuffmemberEntity>();
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
        public NbManagerbuffmemberProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbManagerbuffmemberProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="id">id</param>
        /// <returns>NbManagerbuffmemberEntity</returns>
        /// <remarks>2016/4/22 15:12:23</remarks>
        public NbManagerbuffmemberEntity GetById( System.Int64 id)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagerbuffmember_GetById");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, id);

            
            NbManagerbuffmemberEntity obj=null;
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
        /// <returns>NbManagerbuffmemberEntity列表</returns>
        /// <remarks>2016/4/22 15:12:23</remarks>
        public List<NbManagerbuffmemberEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManagerbuffmember_GetAll");
            

            
            List<NbManagerbuffmemberEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/4/22 15:12:23</remarks>
        public bool Insert(NbManagerbuffmemberEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagerbuffmember_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, entity.Id);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Tid", DbType.Guid, entity.Tid);
			database.AddInParameter(commandWrapper, "@Pid", DbType.Int32, entity.Pid);
			database.AddInParameter(commandWrapper, "@PPos", DbType.Int32, entity.PPos);
			database.AddInParameter(commandWrapper, "@PPosOn", DbType.Int32, entity.PPosOn);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, entity.Kpi);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@ShowOrder", DbType.Int32, entity.ShowOrder);
			database.AddInParameter(commandWrapper, "@IsMain", DbType.Boolean, entity.IsMain);
			database.AddInParameter(commandWrapper, "@ReadySkills", DbType.AnsiString, entity.ReadySkills);
			database.AddInParameter(commandWrapper, "@LiveSkills", DbType.AnsiString, entity.LiveSkills);
			database.AddInParameter(commandWrapper, "@SpeedConst", DbType.Double, entity.SpeedConst);
			database.AddInParameter(commandWrapper, "@SpeedCount", DbType.Double, entity.SpeedCount);
			database.AddInParameter(commandWrapper, "@ShootConst", DbType.Double, entity.ShootConst);
			database.AddInParameter(commandWrapper, "@ShootCount", DbType.Double, entity.ShootCount);
			database.AddInParameter(commandWrapper, "@FreeKickConst", DbType.Double, entity.FreeKickConst);
			database.AddInParameter(commandWrapper, "@FreeKickCount", DbType.Double, entity.FreeKickCount);
			database.AddInParameter(commandWrapper, "@BalanceConst", DbType.Double, entity.BalanceConst);
			database.AddInParameter(commandWrapper, "@BalanceCount", DbType.Double, entity.BalanceCount);
			database.AddInParameter(commandWrapper, "@PhysiqueConst", DbType.Double, entity.PhysiqueConst);
			database.AddInParameter(commandWrapper, "@PhysiqueCount", DbType.Double, entity.PhysiqueCount);
			database.AddInParameter(commandWrapper, "@PowerConst", DbType.Double, entity.PowerConst);
			database.AddInParameter(commandWrapper, "@PowerCount", DbType.Double, entity.PowerCount);
			database.AddInParameter(commandWrapper, "@AggressionConst", DbType.Double, entity.AggressionConst);
			database.AddInParameter(commandWrapper, "@AggressionCount", DbType.Double, entity.AggressionCount);
			database.AddInParameter(commandWrapper, "@DisturbConst", DbType.Double, entity.DisturbConst);
			database.AddInParameter(commandWrapper, "@DisturbCount", DbType.Double, entity.DisturbCount);
			database.AddInParameter(commandWrapper, "@InterceptionConst", DbType.Double, entity.InterceptionConst);
			database.AddInParameter(commandWrapper, "@InterceptionCount", DbType.Double, entity.InterceptionCount);
			database.AddInParameter(commandWrapper, "@DribbleConst", DbType.Double, entity.DribbleConst);
			database.AddInParameter(commandWrapper, "@DribbleCount", DbType.Double, entity.DribbleCount);
			database.AddInParameter(commandWrapper, "@PassConst", DbType.Double, entity.PassConst);
			database.AddInParameter(commandWrapper, "@PassCount", DbType.Double, entity.PassCount);
			database.AddInParameter(commandWrapper, "@MentalityConst", DbType.Double, entity.MentalityConst);
			database.AddInParameter(commandWrapper, "@MentalityCount", DbType.Double, entity.MentalityCount);
			database.AddInParameter(commandWrapper, "@ResponseConst", DbType.Double, entity.ResponseConst);
			database.AddInParameter(commandWrapper, "@ResponseCount", DbType.Double, entity.ResponseCount);
			database.AddInParameter(commandWrapper, "@PositioningConst", DbType.Double, entity.PositioningConst);
			database.AddInParameter(commandWrapper, "@PositioningCount", DbType.Double, entity.PositioningCount);
			database.AddInParameter(commandWrapper, "@HandControlConst", DbType.Double, entity.HandControlConst);
			database.AddInParameter(commandWrapper, "@HandControlCount", DbType.Double, entity.HandControlCount);
			database.AddInParameter(commandWrapper, "@AccelerationConst", DbType.Double, entity.AccelerationConst);
			database.AddInParameter(commandWrapper, "@AccelerationCount", DbType.Double, entity.AccelerationCount);
			database.AddInParameter(commandWrapper, "@BounceConst", DbType.Double, entity.BounceConst);
			database.AddInParameter(commandWrapper, "@BounceCount", DbType.Double, entity.BounceCount);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
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
        /// <remarks>2016/4/22 15:12:23</remarks>
        public bool Update(NbManagerbuffmemberEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManagerbuffmember_Update");
            
			database.AddInParameter(commandWrapper, "@Id", DbType.Int64, entity.Id);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@Tid", DbType.Guid, entity.Tid);
			database.AddInParameter(commandWrapper, "@Pid", DbType.Int32, entity.Pid);
			database.AddInParameter(commandWrapper, "@PPos", DbType.Int32, entity.PPos);
			database.AddInParameter(commandWrapper, "@PPosOn", DbType.Int32, entity.PPosOn);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, entity.Kpi);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, entity.Strength);
			database.AddInParameter(commandWrapper, "@ShowOrder", DbType.Int32, entity.ShowOrder);
			database.AddInParameter(commandWrapper, "@IsMain", DbType.Boolean, entity.IsMain);
			database.AddInParameter(commandWrapper, "@ReadySkills", DbType.AnsiString, entity.ReadySkills);
			database.AddInParameter(commandWrapper, "@LiveSkills", DbType.AnsiString, entity.LiveSkills);
			database.AddInParameter(commandWrapper, "@SpeedConst", DbType.Double, entity.SpeedConst);
			database.AddInParameter(commandWrapper, "@SpeedCount", DbType.Double, entity.SpeedCount);
			database.AddInParameter(commandWrapper, "@ShootConst", DbType.Double, entity.ShootConst);
			database.AddInParameter(commandWrapper, "@ShootCount", DbType.Double, entity.ShootCount);
			database.AddInParameter(commandWrapper, "@FreeKickConst", DbType.Double, entity.FreeKickConst);
			database.AddInParameter(commandWrapper, "@FreeKickCount", DbType.Double, entity.FreeKickCount);
			database.AddInParameter(commandWrapper, "@BalanceConst", DbType.Double, entity.BalanceConst);
			database.AddInParameter(commandWrapper, "@BalanceCount", DbType.Double, entity.BalanceCount);
			database.AddInParameter(commandWrapper, "@PhysiqueConst", DbType.Double, entity.PhysiqueConst);
			database.AddInParameter(commandWrapper, "@PhysiqueCount", DbType.Double, entity.PhysiqueCount);
			database.AddInParameter(commandWrapper, "@PowerConst", DbType.Double, entity.PowerConst);
			database.AddInParameter(commandWrapper, "@PowerCount", DbType.Double, entity.PowerCount);
			database.AddInParameter(commandWrapper, "@AggressionConst", DbType.Double, entity.AggressionConst);
			database.AddInParameter(commandWrapper, "@AggressionCount", DbType.Double, entity.AggressionCount);
			database.AddInParameter(commandWrapper, "@DisturbConst", DbType.Double, entity.DisturbConst);
			database.AddInParameter(commandWrapper, "@DisturbCount", DbType.Double, entity.DisturbCount);
			database.AddInParameter(commandWrapper, "@InterceptionConst", DbType.Double, entity.InterceptionConst);
			database.AddInParameter(commandWrapper, "@InterceptionCount", DbType.Double, entity.InterceptionCount);
			database.AddInParameter(commandWrapper, "@DribbleConst", DbType.Double, entity.DribbleConst);
			database.AddInParameter(commandWrapper, "@DribbleCount", DbType.Double, entity.DribbleCount);
			database.AddInParameter(commandWrapper, "@PassConst", DbType.Double, entity.PassConst);
			database.AddInParameter(commandWrapper, "@PassCount", DbType.Double, entity.PassCount);
			database.AddInParameter(commandWrapper, "@MentalityConst", DbType.Double, entity.MentalityConst);
			database.AddInParameter(commandWrapper, "@MentalityCount", DbType.Double, entity.MentalityCount);
			database.AddInParameter(commandWrapper, "@ResponseConst", DbType.Double, entity.ResponseConst);
			database.AddInParameter(commandWrapper, "@ResponseCount", DbType.Double, entity.ResponseCount);
			database.AddInParameter(commandWrapper, "@PositioningConst", DbType.Double, entity.PositioningConst);
			database.AddInParameter(commandWrapper, "@PositioningCount", DbType.Double, entity.PositioningCount);
			database.AddInParameter(commandWrapper, "@HandControlConst", DbType.Double, entity.HandControlConst);
			database.AddInParameter(commandWrapper, "@HandControlCount", DbType.Double, entity.HandControlCount);
			database.AddInParameter(commandWrapper, "@AccelerationConst", DbType.Double, entity.AccelerationConst);
			database.AddInParameter(commandWrapper, "@AccelerationCount", DbType.Double, entity.AccelerationCount);
			database.AddInParameter(commandWrapper, "@BounceConst", DbType.Double, entity.BounceConst);
			database.AddInParameter(commandWrapper, "@BounceCount", DbType.Double, entity.BounceCount);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
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

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}

