

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class DicManagerwillparttipsProvider
    {
        public List<DicManagerwillparttipsData> GetWillItemcodes()
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_DicManagerWillPartTips_GetAllForCache");
            List<DicManagerwillparttipsData> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = new List<DicManagerwillparttipsData>();
                while (reader.Read())
                {
                    var obj = new DicManagerwillparttipsData();

                    obj.ItemCode = (System.Int32)reader["ItemCode"];
                    list.Add(obj);
                }
            }

            return list;
        }
	}
}

