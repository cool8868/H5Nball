

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class ArenaManagerinfoProvider
    {

        #region LoadSingleRow
        /// <summary>
        /// 将IDataReader的当前记录读取到ArenaManagerinfoEntity 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public ArenaManagerinfoEntity LoadSingleRow1(IDataReader reader)
        {
            var obj = new ArenaManagerinfoEntity();

            obj.ManagerId = (System.Guid)reader["ManagerId"];
            obj.Integral = (System.Int32)reader["Integral"];
            obj.Rank = (System.Int32)reader["Rank"];
            obj.UpdateTime = (System.DateTime)reader["UpdateTime"];
            obj.Logo = (System.String)reader["Logo"];
            obj.SiteId = (System.String)reader["SiteId"];
            obj.ZoneName = (System.String)reader["ZoneName"];
            obj.ManagerName = (System.String)reader["ManagerName"];
            return obj;
        }
        #endregion

        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ArenaManagerinfoEntity> LoadRows1(IDataReader reader)
        {
            var clt = new List<ArenaManagerinfoEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow1(reader));
            }
            return clt;
        }
        #endregion

        #region  GetRank

        /// <summary>
        /// GetRank
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="domainId"></param>
        /// <returns>ArenaManagerinfoEntity列表</returns>
        /// <remarks>2016/8/23 17:49:26</remarks>
        public List<ArenaManagerinfoEntity> GetRank(int pageIndex, int domainId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Arena_GetRank");

            database.AddInParameter(commandWrapper, "@PageIndex", DbType.Int32, pageIndex);
            database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);


            List<ArenaManagerinfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows1(reader);
            }

            return list;
        }

        #endregion	
	  
        	
        #region  RefreshOpponent

        /// <summary>
        /// RefreshOpponent
        /// </summary>
        /// <returns>ArenaManagerinfoEntity列表</returns>
        /// <remarks>2016/9/1 13:59:49</remarks>
        public List<ArenaManagerinfoEntity> RefreshOpponent(System.Int32 danGrading, System.Int32 domainId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_ArenaManagerInfo_RefreshOpponent");

            database.AddInParameter(commandWrapper, "@DanGrading", DbType.Int32, danGrading);
            database.AddInParameter(commandWrapper, "@DomainId", DbType.Int32, domainId);


            List<ArenaManagerinfoEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows2(reader);
            }

            return list;
        }


        #region LoadSingleRow
        /// <summary>
        /// 将IDataReader的当前记录读取到ArenaManagerinfoEntity 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public ArenaManagerinfoEntity LoadSingleRow2(IDataReader reader)
        {
            var obj = new ArenaManagerinfoEntity();

            obj.ManagerId = (System.Guid)reader["ManagerId"];
            obj.ManagerName = (System.String)reader["ManagerName"];
            obj.SiteId = (System.String)reader["SiteId"];
            obj.ZoneName = (System.String)reader["ZoneName"];
            obj.Logo = (System.String)reader["Logo"];
            obj.Integral = (System.Int32)reader["Integral"];
            obj.DanGrading = (System.Int32)reader["DanGrading"];
            obj.Rank = (System.Int32)reader["Rank"];
            return obj;
        }
        #endregion

        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<ArenaManagerinfoEntity> LoadRows2(IDataReader reader)
        {
            var clt = new List<ArenaManagerinfoEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow2(reader));
            }
            return clt;
        }
        #endregion


        #endregion		  
		
	}
}
