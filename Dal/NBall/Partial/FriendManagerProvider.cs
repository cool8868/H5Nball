

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Games.NBall.Entity.Response.Friend;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class FriendManagerProvider
    {
        public List<MyFriendsEntity> GetMyFriends(System.Guid managerId, System.Int32 pageIndex, System.Int32 pageSize, ref  System.Int32 totalRecord)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Friend_GetMyFriend");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@PageIndex", DbType.Int32, pageIndex);
            database.AddInParameter(commandWrapper, "@PageSize", DbType.Int32, pageSize);
            database.AddParameter(commandWrapper, "@TotalRecord", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, totalRecord);

            List<MyFriendsEntity> list = new List<MyFriendsEntity>();
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                while (reader.Read())
                {
                    var obj = new MyFriendsEntity();
                    obj.Idx = (System.Int32)reader["Idx"];
                    obj.FriendId = (System.Guid)reader["FriendId"];
                    obj.Intimacy = (System.Int32)reader["Intimacy"];
                    obj.Name = (System.String)reader["Name"];
                    obj.Level = (System.Int32)reader["Level"];
                    obj.ByHelpTrainCount = (System.Int32)reader["ByHelpTrainCount"];
                    obj.DayHelpTrainCount = (System.Int32)reader["DayHelpTrainCount"];
                    obj.RecordDate = (System.DateTime)reader["RecordDate"];
                    obj.FRecordDate = (System.DateTime)reader["FRecordDate"];
                    obj.VipLevel = (System.Int32)reader["VipLevel"];
                    list.Add(obj);
                }
            }
            totalRecord = (System.Int32)database.GetParameterValue(commandWrapper, "@TotalRecord");
            return list;
        }

        bool CalIsOnline(object obj)
        {
            string s = obj.ToString();
            return s == "1";
        }

        public List<MyBlacksEntity> GetMyBlacks(System.Guid managerId, System.Int32 pageIndex, System.Int32 pageSize, ref  System.Int32 totalRecord)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Friend_GetMyBlack");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@PageIndex", DbType.Int32, pageIndex);
            database.AddInParameter(commandWrapper, "@PageSize", DbType.Int32, pageSize);
            database.AddParameter(commandWrapper, "@TotalRecord", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, totalRecord);

            List<MyBlacksEntity> list = new List<MyBlacksEntity>();
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                while (reader.Read())
                {
                    var obj = new MyBlacksEntity();
                    obj.Idx = (System.Int32)reader["Idx"];
                    obj.FriendId = (System.Guid)reader["FriendId"];
                    obj.Name = (System.String)reader["Name"];
                    obj.Level = (System.Int32)reader["Level"];
                    list.Add(obj);
                }
            }
            totalRecord = (System.Int32)database.GetParameterValue(commandWrapper, "@TotalRecord");
            return list;
        }

        public List<FriendsAdd> GetFriendAddList(System.Guid managerId)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_Friend_FriendAddList");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);

            List<FriendsAdd> list = new List<FriendsAdd>();
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                while (reader.Read())
                {
                    var obj = new FriendsAdd();
                    obj.ManagerId = (System.Guid)reader["ManagerId"];
                    obj.Name = (System.String)reader["Name"];
                    list.Add(obj);
                }
            }
           
            return list;
        }

        public bool IgnoreAddFriend(System.Guid managerId, System.Guid friendId, ref int returnCode)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Friend_IgnoreAddFriend");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@FriendId", DbType.Guid, friendId);
            database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, returnCode);

            int rValue = (int)database.ExecuteNonQuery(commandWrapper);
            returnCode = (System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");

            return Convert.ToBoolean(rValue);
        }	  
	}
}

