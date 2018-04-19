

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class AllLogfunctionProvider
    {/// <summary>
        /// GetAll
        /// </summary>
        /// <returns>AllLogfunctionEntity列表</returns>
        /// <remarks>2014/3/21 16:14:59</remarks>
        public List<AllLogfunctionEntity> GetAllForFactory()
        {
            var database = new SqlDatabase(Properties.Settings.Default.NB_ConfigConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("P_AllLogfunction_GetAll");



            List<AllLogfunctionEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }

            return list;
        }

        /// <summary>
        /// AddNew
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="rowTime">rowTime</param>
        /// <returns>AllLogfunctionEntity</returns>
        /// <remarks>2014/3/21 16:14:59</remarks>
        public AllLogfunctionEntity AddNewForFactory(System.String name, System.DateTime rowTime)
        {
            var database = new SqlDatabase(Properties.Settings.Default.NB_ConfigConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_AllLogFunction_AddNew");

            database.AddInParameter(commandWrapper, "@Name", DbType.AnsiString, name);
            database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, rowTime);


            AllLogfunctionEntity obj = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                if (reader.Read())
                {


                    obj = LoadSingleRow(reader);
                }
            }
            return obj;
        }
	}
}

