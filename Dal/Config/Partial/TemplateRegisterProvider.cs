

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Games.NBall.Entity.Config.Custom;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class TemplateRegisterProvider
    {
        /// <summary>
        /// GetAllForFactory
        /// </summary>
        /// <returns>ConfigDatabaseEntity列表</returns>
        /// <remarks>2013/9/19 7:25:34</remarks>
        public List<TemplatePlayerName> GetPlayerNameList()
        {
            var database = new SqlDatabase(Properties.Settings.Default.NB_ConfigConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Template_GetPlayerNameList");

            List<TemplatePlayerName> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<TemplatePlayerName>();
                while (reader.Read())
                {
                    list.Add(new TemplatePlayerName()
                    {
                        PlayerId = (System.Int32)reader["PlayerId"],
                        Name = (System.String)reader["Name"],
                        CardLevel = (System.Int32)reader["CardLevel"]
                    });
                }
            }

            return list;
        }
	}
}

