

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class CrossladderManagerProvider
    {
        #region  GetDailyHonor

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>CrossladderManagerEntity列表</returns>
        /// <remarks>2016-09-07 18:59:44</remarks>
        public List<CrossladderDailyHonor> GetDailyHonor()
        {
			var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_CrossLadder_GetDailyHonor");



            List<CrossladderDailyHonor> list = new List<CrossladderDailyHonor>();
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                while (reader.Read())
                {
                    var obj = new CrossladderDailyHonor();
                    obj.ManagerId = (System.Guid)reader["ManagerId"];
                    obj.SiteId = (System.String)reader["SiteId"];
                    obj.NewlyLadderCoin = (System.Int32)reader["NewlyLadderCoin"];
                    obj.NewlyHonor = (System.Int32)reader["NewlyHonor"];
                    list.Add(obj);
                }
            }
                
            return list;
        }
		
		#endregion		  
        
	}
}
