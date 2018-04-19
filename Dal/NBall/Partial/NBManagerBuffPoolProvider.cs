

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;

namespace Games.NBall.Dal
{

    public partial class NbManagerbuffpoolProvider
    {
        //#region  GetVersionByMid
        ///// <summary>
        ///// GetVersionByMid
        ///// </summary>
        ///// <param name="managerId">managerId</param>
        ///// <param name="managerHash">managerHash</param>
        ///// <param name="rowVersion">rowVersion</param>
        ///// <returns>int 影响的数据行数</returns>
        ///// <remarks>2014/5/20 18:43:17</remarks>
        //public bool GetVersionByMid(System.Guid managerId, System.Int32 managerHash, ref  System.Byte[] rowVersion, DbTransaction trans = null)
        //{
        //    var database = new SqlDatabase(this.ConnectionString);

        //    DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_GetVersionByMid");

        //    database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
        //    database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
        //    database.AddParameter(commandWrapper, "@RowVersion", DbType.Binary, ParameterDirection.InputOutput, "", DataRowVersion.Current, rowVersion);

        //    commandWrapper.Parameters[2].Size = 8;
        //    int rValue = 0;
        //    if (trans != null)
        //    {
        //        rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
        //    }
        //    else
        //    {
        //        rValue = (int)database.ExecuteNonQuery(commandWrapper);
        //    }
        //    rowVersion = (System.Byte[])database.GetParameterValue(commandWrapper, "@RowVersion");

        //    return Convert.ToBoolean(rValue);
        //}

        //#endregion		  

        //#region  GetByMid

        ///// <summary>
        ///// GetByMid
        ///// </summary>
        ///// <returns>NbManagerbuffpoolEntity列表</returns>
        ///// <remarks>2014/5/20 18:43:17</remarks>
        //public List<NbManagerbuffpoolEntity> GetByMid(System.Guid managerId, System.Int32 managerHash)
        //{
        //    var database = new SqlDatabase(this.ConnectionString);

        //    DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_GetByMid");

        //    database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
        //    database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);


        //    List<NbManagerbuffpoolEntity> list = null;
        //    using (IDataReader reader = database.ExecuteReader(commandWrapper))
        //    {
        //        list = LoadRows(reader);
        //    }

        //    return list;
        //}

        //#endregion

        //#region  Include

        ///// <summary>
        ///// Include
        ///// </summary>
        ///// <param name="managerId">managerId</param>
        ///// <param name="managerHash">managerHash</param>
        ///// <param name="skillCode">skillCode</param>
        ///// <param name="skillLevel">skillLevel</param>
        ///// <param name="buffSrcType">buffSrcType</param>
        ///// <param name="buffSrcId">buffSrcId</param>
        ///// <param name="buffUnitType">buffUnitType</param>
        ///// <param name="liveFlag">liveFlag</param>
        ///// <param name="buffNo">buffNo</param>
        ///// <param name="dstDir">dstDir</param>
        ///// <param name="dstMode">dstMode</param>
        ///// <param name="dstKey">dstKey</param>
        ///// <param name="buffMap">buffMap</param>
        ///// <param name="buffVal">buffVal</param>
        ///// <param name="buffPer">buffPer</param>
        ///// <param name="expiryMinutes">expiryMinutes</param>
        ///// <param name="limitTimes">limitTimes</param>
        ///// <param name="repeatBuffFlag">repeatBuffFlag</param>
        ///// <param name="repeatTimeFlag">repeatTimeFlag</param>
        ///// <param name="repeatTimesFlag">repeatTimesFlag</param>
        ///// <returns>int 影响的数据行数</returns>
        ///// <remarks>2014/5/20 18:43:17</remarks>
        //public bool Include(System.Guid managerId, System.Int32 managerHash, System.String skillCode, System.Int32 skillLevel, System.Int32 buffSrcType, System.String buffSrcId, System.Int32 buffUnitType, System.Int32 liveFlag, System.Int32 buffNo, System.Int32 dstDir, System.Int32 dstMode, System.String dstKey, System.String buffMap, System.Decimal buffVal, System.Decimal buffPer, System.Int32 expiryMinutes, System.Int32 limitTimes, System.Boolean repeatBuffFlag, System.Boolean repeatTimeFlag, System.Boolean repeatTimesFlag, DbTransaction trans = null)
        //{
        //    var database = new SqlDatabase(this.ConnectionString);

        //    DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_Include");

        //    database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
        //    database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
        //    database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);
        //    database.AddInParameter(commandWrapper, "@SkillLevel", DbType.Int32, skillLevel);
        //    database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, buffSrcType);
        //    database.AddInParameter(commandWrapper, "@BuffSrcId", DbType.AnsiString, buffSrcId);
        //    database.AddInParameter(commandWrapper, "@BuffUnitType", DbType.Int32, buffUnitType);
        //    database.AddInParameter(commandWrapper, "@LiveFlag", DbType.Int32, liveFlag);
        //    database.AddInParameter(commandWrapper, "@BuffNo", DbType.Int32, buffNo);
        //    database.AddInParameter(commandWrapper, "@DstDir", DbType.Int32, dstDir);
        //    database.AddInParameter(commandWrapper, "@DstMode", DbType.Int32, dstMode);
        //    database.AddInParameter(commandWrapper, "@DstKey", DbType.AnsiString, dstKey);
        //    database.AddInParameter(commandWrapper, "@BuffMap", DbType.AnsiString, buffMap);
        //    database.AddInParameter(commandWrapper, "@BuffVal", DbType.Currency, buffVal);
        //    database.AddInParameter(commandWrapper, "@BuffPer", DbType.Currency, buffPer);
        //    database.AddInParameter(commandWrapper, "@ExpiryMinutes", DbType.Int32, expiryMinutes);
        //    database.AddInParameter(commandWrapper, "@LimitTimes", DbType.Int32, limitTimes);
        //    database.AddInParameter(commandWrapper, "@RepeatBuffFlag", DbType.Boolean, repeatBuffFlag);
        //    database.AddInParameter(commandWrapper, "@RepeatTimeFlag", DbType.Boolean, repeatTimeFlag);
        //    database.AddInParameter(commandWrapper, "@RepeatTimesFlag", DbType.Boolean, repeatTimesFlag);


        //    int rValue = 0;
        //    if (trans != null)
        //    {
        //        rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
        //    }
        //    else
        //    {
        //        rValue = (int)database.ExecuteNonQuery(commandWrapper);
        //    }

        //    return Convert.ToBoolean(rValue);
        //}

        //#endregion

        //#region  Exclude

        ///// <summary>
        ///// Exclude
        ///// </summary>
        ///// <param name="managerId">managerId</param>
        ///// <param name="managerHash">managerHash</param>
        ///// <param name="buffSrcType">buffSrcType</param>
        ///// <param name="buffSrcId">buffSrcId</param>
        ///// <param name="skillCode">skillCode</param>
        ///// <returns>int 影响的数据行数</returns>
        ///// <remarks>2014/5/20 18:43:17</remarks>
        //public bool Exclude(System.Guid managerId, System.Int32 managerHash, System.Int32 buffSrcType, System.String buffSrcId, System.String skillCode, DbTransaction trans = null)
        //{
        //    var database = new SqlDatabase(this.ConnectionString);

        //    DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_Exclude");

        //    database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
        //    database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
        //    database.AddInParameter(commandWrapper, "@BuffSrcType", DbType.Int32, buffSrcType);
        //    database.AddInParameter(commandWrapper, "@BuffSrcId", DbType.AnsiString, buffSrcId);
        //    database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);


        //    int rValue = 0;
        //    if (trans != null)
        //    {
        //        rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
        //    }
        //    else
        //    {
        //        rValue = (int)database.ExecuteNonQuery(commandWrapper);
        //    }

        //    return Convert.ToBoolean(rValue);
        //}

        //#endregion

        //#region  ExcludeMulti

        ///// <summary>
        ///// ExcludeMulti
        ///// </summary>
        ///// <param name="managerId">managerId</param>
        ///// <param name="managerHash">managerHash</param>
        ///// <param name="skillCode">skillCode</param>
        ///// <param name="buffNo">buffNo</param>
        ///// <param name="skillCode2">skillCode2</param>
        ///// <param name="buffNo2">buffNo2</param>
        ///// <param name="skillCode3">skillCode3</param>
        ///// <param name="buffNo3">buffNo3</param>
        ///// <param name="skillCode4">skillCode4</param>
        ///// <param name="buffNo4">buffNo4</param>
        ///// <param name="skillCode5">skillCode5</param>
        ///// <param name="buffNo5">buffNo5</param>
        ///// <returns>int 影响的数据行数</returns>
        ///// <remarks>2014/5/20 18:43:17</remarks>
        //public bool ExcludeMulti(System.Guid managerId, System.Int32 managerHash, System.String skillCode, System.Int32 buffNo, System.String skillCode2, System.Int32 buffNo2, System.String skillCode3, System.Int32 buffNo3, System.String skillCode4, System.Int32 buffNo4, System.String skillCode5, System.Int32 buffNo5, DbTransaction trans = null)
        //{
        //    var database = new SqlDatabase(this.ConnectionString);

        //    DbCommand commandWrapper = database.GetStoredProcCommand("C_BuffPool_ExcludeMulti");

        //    database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
        //    database.AddInParameter(commandWrapper, "@ManagerHash", DbType.Int32, managerHash);
        //    database.AddInParameter(commandWrapper, "@SkillCode", DbType.AnsiString, skillCode);
        //    database.AddInParameter(commandWrapper, "@BuffNo", DbType.Int32, buffNo);
        //    database.AddInParameter(commandWrapper, "@SkillCode2", DbType.AnsiString, skillCode2);
        //    database.AddInParameter(commandWrapper, "@BuffNo2", DbType.Int32, buffNo2);
        //    database.AddInParameter(commandWrapper, "@SkillCode3", DbType.AnsiString, skillCode3);
        //    database.AddInParameter(commandWrapper, "@BuffNo3", DbType.Int32, buffNo3);
        //    database.AddInParameter(commandWrapper, "@SkillCode4", DbType.AnsiString, skillCode4);
        //    database.AddInParameter(commandWrapper, "@BuffNo4", DbType.Int32, buffNo4);
        //    database.AddInParameter(commandWrapper, "@SkillCode5", DbType.AnsiString, skillCode5);
        //    database.AddInParameter(commandWrapper, "@BuffNo5", DbType.Int32, buffNo5);


        //    int rValue = 0;
        //    if (trans != null)
        //    {
        //        rValue = (int)database.ExecuteNonQuery(commandWrapper, trans);
        //    }
        //    else
        //    {
        //        rValue = (int)database.ExecuteNonQuery(commandWrapper);
        //    }

        //    return Convert.ToBoolean(rValue);
        //}

        //#endregion		  
	}
}

