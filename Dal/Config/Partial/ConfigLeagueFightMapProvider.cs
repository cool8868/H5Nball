

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class ConfigLeaguefightmapProvider
    {
        public Dictionary<int,List<int>> GetAllForCache()
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_ConfigLeagueFightMap_GetAllForCache");

            Dictionary<int,List<int>> dic = new Dictionary<int, List<int>>();

            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                while (reader.Read())
                {
                    var teamCount = (System.Int32)reader["TeamCount"];
                    var templateId = (System.Int32)reader["TemplateId"];
                    if (!dic.ContainsKey(teamCount))
                        dic.Add(teamCount, new List<int>());
                    dic[teamCount].Add(templateId);
                }
            }

            return dic;
        }
	}
}

