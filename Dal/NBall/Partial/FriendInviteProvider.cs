

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class FriendinviteProvider
    {  
        #region LoadSingleRow
        /// <summary>
        /// 将IDataReader的当前记录读取到FriendinviteEntity 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public FriendinviteEntity LoadSingleRow1(IDataReader reader)
        {
            var obj = new FriendinviteEntity();

            obj.ByAccount = (System.String)reader["ByAccount"];
            obj.Account = (System.String)reader["Account"];
            obj.Level = (System.Int32)reader["Level"];
            obj.IsPrize = (System.Boolean)reader["IsPrize"];
            obj.MayPrize = (System.Int32)reader["MayPrize"];
            obj.AlreadyPrize = (System.Int32)reader["AlreadyPrize"];
            obj.Name = (System.String)reader["Name"];
            obj.NLevel = (System.Int32)reader["NLevel"];
            return obj;
        }
        #endregion

        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<FriendinviteEntity> LoadRows1(IDataReader reader)
        {
            var clt = new List<FriendinviteEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow1(reader));
            }
            return clt;
        }
        #endregion
        

        #region  InviteManagerList

        /// <summary>
        /// InviteManagerList
        /// </summary>
        /// <returns>FriendinviteEntity列表</returns>
        /// <remarks>2015/2/4 20:17:04</remarks>
        public List<FriendinviteEntity> InviteManagerList(System.String account)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_FriendInvite_InviteManagerList");

            database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);


            List<FriendinviteEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows1(reader);
            }

            return list;
        }

        #endregion		  
		
	}
}

