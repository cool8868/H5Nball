

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class NbManagerbuffmemberProvider
    {
        #region  GetByMid

        /// <summary>
        /// GetByMid
        /// </summary>
        /// <returns>NbManagerbuffmemberEntity列表</returns>
        /// <remarks>2014/5/26 15:04:46</remarks>
        public List<NbManagerbuffmemberEntity> GetByMid(System.Guid managerId, System.Int32 managerHash)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffMember_GetByMid");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);


            List<NbManagerbuffmemberEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }

            return list;
        }

        #endregion

        #region  SyncSend

        /// <summary>
        /// SyncSend
        /// </summary>
        /// <param name="managerId">managerId</param>
        /// <param name="syncRowVersion">syncRowVersion</param>
        /// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/5/26 15:04:46</remarks>
        public bool SyncSend(System.Guid managerId, System.Byte[] syncRowVersion, ref  System.Int32 errorCode, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffMember_SyncSend");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@SyncRowVersion", DbType.Binary, syncRowVersion);
            database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, errorCode);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            errorCode = (System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");

            return Convert.ToBoolean(rValue);
        }

        #endregion

        #region  SyncStart

        /// <summary>
        /// SyncStart
        /// </summary>
        /// <param name="managerId">managerId</param>
        /// <param name="syncRowVersion">syncRowVersion</param>
        /// <param name="newRowVersion">newRowVersion</param>
        /// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/5/26 15:04:46</remarks>
        public bool SyncStart(System.Guid managerId, System.Byte[] syncRowVersion, ref  System.Byte[] newRowVersion, ref  System.Int32 errorCode, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffMember_SyncStart");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@SyncRowVersion", DbType.Binary, syncRowVersion);
            database.AddParameter(commandWrapper, "@NewRowVersion", DbType.Binary, ParameterDirection.InputOutput, "", DataRowVersion.Current, newRowVersion);
            database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, errorCode);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            newRowVersion = (System.Byte[])database.GetParameterValue(commandWrapper, "@NewRowVersion");
            errorCode = (System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");

            return Convert.ToBoolean(rValue);
        }

        #endregion

        #region  SyncIdle

        /// <summary>
        /// SyncIdle
        /// </summary>
        /// <param name="managerId">managerId</param>
        /// <param name="managerHash">managerHash</param>
        /// <param name="tid">tid</param>
        /// <param name="tid2">tid2</param>
        /// <param name="tid3">tid3</param>
        /// <param name="tid4">tid4</param>
        /// <param name="tid5">tid5</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/5/26 15:04:46</remarks>
        public bool SyncIdle(System.Guid managerId, System.Int32 managerHash, System.Guid tid, System.Guid tid2, System.Guid tid3, System.Guid tid4, System.Guid tid5, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffMember_SyncIdle");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
            database.AddInParameter(commandWrapper, "@Tid", DbType.Guid, tid);
            database.AddInParameter(commandWrapper, "@Tid2", DbType.Guid, tid2);
            database.AddInParameter(commandWrapper, "@Tid3", DbType.Guid, tid3);
            database.AddInParameter(commandWrapper, "@Tid4", DbType.Guid, tid4);
            database.AddInParameter(commandWrapper, "@Tid5", DbType.Guid, tid5);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }

            return Convert.ToBoolean(rValue);
        }

        #endregion

        #region  SyncInto

        /// <summary>
        /// SyncInto
        /// </summary>
        /// <param name="managerId">managerId</param>
        /// <param name="managerHash">managerHash</param>
        /// <param name="tid">tid</param>
        /// <param name="pid">pid</param>
        /// <param name="pPos">pPos</param>
        /// <param name="kpi">kpi</param>
        /// <param name="level">level</param>
        /// <param name="strength">strength</param>
        /// <param name="showOrder">showOrder</param>
        /// <param name="isMain">isMain</param>
        /// <param name="speed">speed</param>
        /// <param name="shoot">shoot</param>
        /// <param name="freeKick">freeKick</param>
        /// <param name="balance">balance</param>
        /// <param name="physique">physique</param>
        /// <param name="bounce">bounce</param>
        /// <param name="aggression">aggression</param>
        /// <param name="disturb">disturb</param>
        /// <param name="interception">interception</param>
        /// <param name="dribble">dribble</param>
        /// <param name="pass">pass</param>
        /// <param name="mentality">mentality</param>
        /// <param name="response">response</param>
        /// <param name="positioning">positioning</param>
        /// <param name="handControl">handControl</param>
        /// <param name="acceleration">acceleration</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/5/26 15:04:46</remarks>
        public bool SyncInto(System.Guid managerId, System.Int32 managerHash, System.Guid tid, System.Int32 pid, System.Int32 pPos, System.Int32 kpi, System.Int32 level, System.Int32 strength, System.Int32 showOrder, System.Boolean isMain, System.Double speed, System.Double shoot, System.Double freeKick, System.Double balance, System.Double physique, System.Double bounce, System.Double aggression, System.Double disturb, System.Double interception, System.Double dribble, System.Double pass, System.Double mentality, System.Double response, System.Double positioning, System.Double handControl, System.Double acceleration, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffMember_SyncInto");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
            database.AddInParameter(commandWrapper, "@Tid", DbType.Guid, tid);
            database.AddInParameter(commandWrapper, "@Pid", DbType.Int32, pid);
            database.AddInParameter(commandWrapper, "@PPos", DbType.Int32, pPos);
            database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, kpi);
            database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);
            database.AddInParameter(commandWrapper, "@Strength", DbType.Int32, strength);
            database.AddInParameter(commandWrapper, "@ShowOrder", DbType.Int32, showOrder);
            database.AddInParameter(commandWrapper, "@IsMain", DbType.Boolean, isMain);
            database.AddInParameter(commandWrapper, "@Speed", DbType.Double, speed);
            database.AddInParameter(commandWrapper, "@Shoot", DbType.Double, shoot);
            database.AddInParameter(commandWrapper, "@FreeKick", DbType.Double, freeKick);
            database.AddInParameter(commandWrapper, "@Balance", DbType.Double, balance);
            database.AddInParameter(commandWrapper, "@Physique", DbType.Double, physique);
            database.AddInParameter(commandWrapper, "@Bounce", DbType.Double, bounce);
            database.AddInParameter(commandWrapper, "@Aggression", DbType.Double, aggression);
            database.AddInParameter(commandWrapper, "@Disturb", DbType.Double, disturb);
            database.AddInParameter(commandWrapper, "@Interception", DbType.Double, interception);
            database.AddInParameter(commandWrapper, "@Dribble", DbType.Double, dribble);
            database.AddInParameter(commandWrapper, "@Pass", DbType.Double, pass);
            database.AddInParameter(commandWrapper, "@Mentality", DbType.Double, mentality);
            database.AddInParameter(commandWrapper, "@Response", DbType.Double, response);
            database.AddInParameter(commandWrapper, "@Positioning", DbType.Double, positioning);
            database.AddInParameter(commandWrapper, "@HandControl", DbType.Double, handControl);
            database.AddInParameter(commandWrapper, "@Acceleration", DbType.Double, acceleration);


            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }

            return Convert.ToBoolean(rValue);
        }

        #endregion		  
		
        #region  SyncEnd

        /// <summary>
        /// SyncEnd
        /// </summary>
        /// <param name="managerId">managerId</param>
        /// <param name="playerSkills">playerSkills</param>
        /// <param name="managerSkills">managerSkills</param>
        /// <param name="syncRowVersion">syncRowVersion</param>
        /// <param name="newRowVersion">newRowVersion</param>
        /// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/5/15 15:19:25</remarks>
        public bool SyncEnd(System.Guid managerId, System.String playerSkills, System.String managerSkills, System.Byte[] syncRowVersion, ref  System.Byte[] newRowVersion, ref  System.Int32 errorCode, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffMember_SyncEnd");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@PlayerSkills", DbType.AnsiString, playerSkills);
            database.AddInParameter(commandWrapper, "@ManagerSkills", DbType.AnsiString, managerSkills);
            database.AddInParameter(commandWrapper, "@SyncRowVersion", DbType.Binary, syncRowVersion);
            database.AddParameter(commandWrapper, "@NewRowVersion", DbType.Binary, ParameterDirection.InputOutput, "", DataRowVersion.Current, newRowVersion);
            database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, errorCode);
            commandWrapper.Parameters[4].Size = 8;

            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            newRowVersion = (System.Byte[])database.GetParameterValue(commandWrapper, "@NewRowVersion");
            errorCode = (System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");

            return Convert.ToBoolean(rValue);
        }

        #endregion

        #region  SyncBatch

        /// <summary>
        /// SyncBatch
        /// </summary>
        /// <param name="managerId">managerId</param>
        /// <param name="kpi">kpi</param>
        /// <param name="playerSkills">playerSkills</param>
        /// <param name="managerSkills">managerSkills</param>
        /// <param name="syncRowVersion">syncRowVersion</param>
        /// <param name="newRowVersion">newRowVersion</param>
        /// <param name="errorCode">errorCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2014/5/26 15:04:46</remarks>
        public bool SyncBatch(System.Guid managerId, System.Int32 kpi, System.String playerSkills, System.String managerSkills, System.Byte[] syncRowVersion, ref  System.Byte[] newRowVersion, ref  System.Int32 errorCode, DbTransaction trans = null)
        {
            var database = new SqlDatabase(this.ConnectionString);

            DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffMember_SyncBatch");

            database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
            database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, kpi);
            database.AddInParameter(commandWrapper, "@PlayerSkills", DbType.AnsiString, playerSkills);
            database.AddInParameter(commandWrapper, "@ManagerSkills", DbType.AnsiString, managerSkills);
            database.AddInParameter(commandWrapper, "@SyncRowVersion", DbType.Binary, syncRowVersion);
            database.AddParameter(commandWrapper, "@NewRowVersion", DbType.Binary, ParameterDirection.InputOutput, "", DataRowVersion.Current, newRowVersion);
            database.AddParameter(commandWrapper, "@ErrorCode", DbType.Int32, ParameterDirection.InputOutput, "", DataRowVersion.Current, errorCode);
            commandWrapper.Parameters[5].Size = 8;

            int rValue = 0;
            if (trans != null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            newRowVersion = (System.Byte[])database.GetParameterValue(commandWrapper, "@NewRowVersion");
            errorCode = (System.Int32)database.GetParameterValue(commandWrapper, "@ErrorCode");

            return Convert.ToBoolean(rValue);
        }

        #endregion		  
	}
}

