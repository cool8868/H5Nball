

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Dal
{
    
    public partial class TeammemberProvider
    {
        #region properties

        private const EnumDbType DBTYPE = EnumDbType.Main;

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string ConnectionString = "";
        #endregion 
        
        #region Helper Functions
        
        #region LoadSingleRow
		/// <summary>
		/// 将IDataReader的当前记录读取到TeammemberEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public TeammemberEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new TeammemberEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.ManagerId = (System.Guid) reader["ManagerId"];
            obj.PlayerId = (System.Int32) reader["PlayerId"];
            obj.Level = (System.Int32) reader["Level"];
            obj.UsedProperty = (System.Int32) reader["UsedProperty"];
            obj.Speed = (System.Double) reader["Speed"];
            obj.Shoot = (System.Double) reader["Shoot"];
            obj.FreeKick = (System.Double) reader["FreeKick"];
            obj.Balance = (System.Double) reader["Balance"];
            obj.Physique = (System.Double) reader["Physique"];
            obj.Bounce = (System.Double) reader["Bounce"];
            obj.Aggression = (System.Double) reader["Aggression"];
            obj.Disturb = (System.Double) reader["Disturb"];
            obj.Interception = (System.Double) reader["Interception"];
            obj.Dribble = (System.Double) reader["Dribble"];
            obj.Pass = (System.Double) reader["Pass"];
            obj.Mentality = (System.Double) reader["Mentality"];
            obj.Response = (System.Double) reader["Response"];
            obj.Positioning = (System.Double) reader["Positioning"];
            obj.HandControl = (System.Double) reader["HandControl"];
            obj.Acceleration = (System.Double) reader["Acceleration"];
            obj.UsedPlayerCard = (System.Byte[]) reader["UsedPlayerCard"];
            obj.UsedEquipment = (System.Byte[]) reader["UsedEquipment"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.IsCopyed = (System.Boolean) reader["IsCopyed"];
            obj.IsInherited = (System.Boolean) reader["IsInherited"];
            obj.UsedBadge = (System.Byte[]) reader["UsedBadge"];
            obj.ArousalLv = (System.Int32) reader["ArousalLv"];
            obj.UsedClubClothes = (System.Byte[]) reader["UsedClubClothes"];
            obj.StrengthenLevel = (System.Int32) reader["StrengthenLevel"];
            obj.Power = (System.Double) reader["Power"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<TeammemberEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<TeammemberEntity>();
            while (reader.Read())
            {
                clt.Add(LoadSingleRow(reader));
            }
            return clt;
        }
        #endregion
        
        #endregion
        
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public TeammemberProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public TeammemberProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="mod">mod</param>
        /// <returns>TeammemberEntity</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public TeammemberEntity GetById( System.Guid idx, System.Int32 mod)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            TeammemberEntity obj=null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                if(reader.Read())
                {
                    
            
                    obj = LoadSingleRow(reader);
                }
            }
            return obj;
        }
		
		#endregion		  
		
		#region  GetByManager
		
		/// <summary>
        /// GetByManager
        /// </summary>
        /// <returns>TeammemberEntity列表</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public List<TeammemberEntity> GetByManager( System.Guid managerId, System.Int32 mod)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_GetByManager");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            List<TeammemberEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByStatus
		
		/// <summary>
        /// GetByStatus
        /// </summary>
        /// <returns>TeammemberEntity列表</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public List<TeammemberEntity> GetByStatus( System.Guid managerId, System.Int32 status, System.Int32 mod)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_GetByStatus");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            List<TeammemberEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  EquipmentExists
		
		/// <summary>
        /// EquipmentExists
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="mod">mod</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool EquipmentExists ( System.Guid managerId, System.Int32 mod,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_EquipmentExists");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  PlayerCardExists
		
		/// <summary>
        /// PlayerCardExists
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="mod">mod</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool PlayerCardExists ( System.Guid managerId, System.Int32 mod,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_PlayerCardExists");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  ResetProperty
		
		/// <summary>
        /// ResetProperty
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool ResetProperty ( System.Guid idx, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_ResetProperty");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  UpdateProperty
		
		/// <summary>
        /// UpdateProperty
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="usedProperty">usedProperty</param>
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
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool UpdateProperty ( System.Guid idx, System.Int32 usedProperty, System.Double speed, System.Double shoot, System.Double freeKick, System.Double balance, System.Double physique, System.Double bounce, System.Double aggression, System.Double disturb, System.Double interception, System.Double dribble, System.Double pass, System.Double mentality, System.Double response, System.Double positioning, System.Double handControl, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_UpdateProperty");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@UsedProperty", DbType.Int32, usedProperty);
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
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  UpdateLevel
		
		/// <summary>
        /// UpdateLevel
        /// </summary>
		/// <param name="teammemberId">teammemberId</param>
		/// <param name="level">level</param>
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool UpdateLevel ( System.Guid teammemberId, System.Int32 level, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_UpdateLevel");
            
			database.AddInParameter(commandWrapper, "@TeammemberId", DbType.Guid, teammemberId);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetForTransCheck
		
		/// <summary>
        /// GetForTransCheck
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="playerId">playerId</param>
		/// <param name="mod">mod</param>
		/// <param name="repeatCode">repeatCode</param>
		/// <param name="teammemberCount">teammemberCount</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool GetForTransCheck ( System.Guid managerId, System.Int32 playerId, System.Int32 mod, System.Int32 repeatCode,ref  System.Int32 teammemberCount,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_GetForTransCheck");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@PlayerId", DbType.Int32, playerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddInParameter(commandWrapper, "@RepeatCode", DbType.Int32, repeatCode);
			database.AddParameter(commandWrapper, "@TeammemberCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,teammemberCount);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            teammemberCount=(System.Int32)database.GetParameterValue(commandWrapper, "@TeammemberCount");
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  TransSave
		
		/// <summary>
        /// TransSave
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="idx">idx</param>
		/// <param name="playerId">playerId</param>
		/// <param name="repeatCode">repeatCode</param>
		/// <param name="itemString">itemString</param>
		/// <param name="mod">mod</param>
		/// <param name="returnCode">returnCode</param>
		/// <param name="errorMessage">errorMessage</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool TransSave ( System.Guid managerId, System.Guid idx, System.Int32 playerId, System.Int32 repeatCode, System.Byte[] itemString, System.Int32 mod,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_TransSave");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@PlayerId", DbType.Int32, playerId);
			database.AddInParameter(commandWrapper, "@RepeatCode", DbType.Int32, repeatCode);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, itemString);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);
			database.AddOutParameter(commandWrapper, "@ErrorMessage", DbType.String,500);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            errorMessage=(System.String)database.GetParameterValue(commandWrapper, "@ErrorMessage");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SaveSolution
		
		/// <summary>
        /// SaveSolution
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="formationId">formationId</param>
		/// <param name="playerString">playerString</param>
		/// <param name="veteranCount">veteranCount</param>
		/// <param name="orangeCount">orangeCount</param>
		/// <param name="combCount">combCount</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool SaveSolution ( System.Guid managerId, System.Int32 formationId, System.String playerString, System.Int32 veteranCount, System.Int32 orangeCount, System.Int32 combCount,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_SaveSolution");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@FormationId", DbType.Int32, formationId);
			database.AddInParameter(commandWrapper, "@PlayerString", DbType.AnsiStringFixedLength, playerString);
			database.AddInParameter(commandWrapper, "@VeteranCount", DbType.Int32, veteranCount);
			database.AddInParameter(commandWrapper, "@OrangeCount", DbType.Int32, orangeCount);
			database.AddInParameter(commandWrapper, "@CombCount", DbType.Int32, combCount);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Fire
		
		/// <summary>
        /// Fire
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool Fire ( System.Guid idx, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_Fire");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SetPlayerCard
		
		/// <summary>
        /// SetPlayerCard
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="managerId">managerId</param>
		/// <param name="mod">mod</param>
		/// <param name="equipmentData">equipmentData</param>
		/// <param name="itemString">itemString</param>
		/// <param name="itemRowVersion">itemRowVersion</param>
		/// <param name="returnCode">returnCode</param>
		/// <param name="errorMessage">errorMessage</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool SetPlayerCard ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] equipmentData, System.Byte[] itemString, System.Byte[] itemRowVersion,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_SetPlayerCard");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddInParameter(commandWrapper, "@EquipmentData", DbType.Binary, equipmentData);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, itemString);
			database.AddInParameter(commandWrapper, "@ItemRowVersion", DbType.Binary, itemRowVersion);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);
			database.AddOutParameter(commandWrapper, "@ErrorMessage", DbType.String,500);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            errorMessage=(System.String)database.GetParameterValue(commandWrapper, "@ErrorMessage");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SetEquipment
		
		/// <summary>
        /// SetEquipment
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="managerId">managerId</param>
		/// <param name="mod">mod</param>
		/// <param name="equipmentData">equipmentData</param>
		/// <param name="itemString">itemString</param>
		/// <param name="itemRowVersion">itemRowVersion</param>
		/// <param name="returnCode">returnCode</param>
		/// <param name="errorMessage">errorMessage</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool SetEquipment ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] equipmentData, System.Byte[] itemString, System.Byte[] itemRowVersion,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_SetEquipment");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddInParameter(commandWrapper, "@EquipmentData", DbType.Binary, equipmentData);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, itemString);
			database.AddInParameter(commandWrapper, "@ItemRowVersion", DbType.Binary, itemRowVersion);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);
			database.AddOutParameter(commandWrapper, "@ErrorMessage", DbType.String,500);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            errorMessage=(System.String)database.GetParameterValue(commandWrapper, "@ErrorMessage");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  FormationLevelup
		
		/// <summary>
        /// FormationLevelup
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="formationData">formationData</param>
		/// <param name="sophisticate">sophisticate</param>
		/// <param name="sophisticateShortageCode">sophisticateShortageCode</param>
		/// <param name="curSophisticate">curSophisticate</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool FormationLevelup ( System.Guid managerId, System.Byte[] formationData, System.Int32 sophisticate, System.Int32 sophisticateShortageCode,ref  System.Int32 curSophisticate,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_FormationLevelup");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@FormationData", DbType.Binary, formationData);
			database.AddInParameter(commandWrapper, "@Sophisticate", DbType.Int32, sophisticate);
			database.AddInParameter(commandWrapper, "@SophisticateShortageCode", DbType.Int32, sophisticateShortageCode);
			database.AddParameter(commandWrapper, "@CurSophisticate", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curSophisticate);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            curSophisticate=(System.Int32)database.GetParameterValue(commandWrapper, "@CurSophisticate");
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  UpdateIsCopyed
		
		/// <summary>
        /// UpdateIsCopyed
        /// </summary>
		/// <param name="teammemberId">teammemberId</param>
		/// <param name="isCopyed">isCopyed</param>
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool UpdateIsCopyed ( System.Guid teammemberId, System.Boolean isCopyed, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_UpdateIsCopyed");
            
			database.AddInParameter(commandWrapper, "@TeammemberId", DbType.Guid, teammemberId);
			database.AddInParameter(commandWrapper, "@IsCopyed", DbType.Boolean, isCopyed);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SaveInherit
		
		/// <summary>
        /// SaveInherit
        /// </summary>
		/// <param name="teammemberId">teammemberId</param>
		/// <param name="level">level</param>
		/// <param name="exp">exp</param>
		/// <param name="targetTeammemberId">targetTeammemberId</param>
		/// <param name="targetLevel">targetLevel</param>
		/// <param name="targetExp">targetExp</param>
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool SaveInherit ( System.Guid teammemberId, System.Int32 level, System.Int32 exp, System.Guid targetTeammemberId, System.Int32 targetLevel, System.Int32 targetExp, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_SaveInherit");
            
			database.AddInParameter(commandWrapper, "@TeammemberId", DbType.Guid, teammemberId);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);
			database.AddInParameter(commandWrapper, "@Exp", DbType.Int32, exp);
			database.AddInParameter(commandWrapper, "@TargetTeammemberId", DbType.Guid, targetTeammemberId);
			database.AddInParameter(commandWrapper, "@TargetLevel", DbType.Int32, targetLevel);
			database.AddInParameter(commandWrapper, "@TargetExp", DbType.Int32, targetExp);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SetBadge
		
		/// <summary>
        /// SetBadge
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="managerId">managerId</param>
		/// <param name="mod">mod</param>
		/// <param name="badgeData">badgeData</param>
		/// <param name="itemString">itemString</param>
		/// <param name="itemRowVersion">itemRowVersion</param>
		/// <param name="returnCode">returnCode</param>
		/// <param name="errorMessage">errorMessage</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool SetBadge ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] badgeData, System.Byte[] itemString, System.Byte[] itemRowVersion,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_SetBadge");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddInParameter(commandWrapper, "@BadgeData", DbType.Binary, badgeData);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, itemString);
			database.AddInParameter(commandWrapper, "@ItemRowVersion", DbType.Binary, itemRowVersion);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);
			database.AddOutParameter(commandWrapper, "@ErrorMessage", DbType.String,500);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            errorMessage=(System.String)database.GetParameterValue(commandWrapper, "@ErrorMessage");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  UpdateArousalLv
		
		/// <summary>
        /// UpdateArousalLv
        /// </summary>
		/// <param name="teammemberId">teammemberId</param>
		/// <param name="arousalLv">arousalLv</param>
		/// <param name="mod">mod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:36</remarks>
        public bool UpdateArousalLv ( System.Guid teammemberId, System.Int32 arousalLv, System.Int32 mod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_UpdateArousalLv");
            
			database.AddInParameter(commandWrapper, "@TeammemberId", DbType.Guid, teammemberId);
			database.AddInParameter(commandWrapper, "@ArousalLv", DbType.Int32, arousalLv);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SetClubClothes
		
		/// <summary>
        /// SetClubClothes
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="managerId">managerId</param>
		/// <param name="mod">mod</param>
		/// <param name="clubClothesData">clubClothesData</param>
		/// <param name="itemString">itemString</param>
		/// <param name="itemRowVersion">itemRowVersion</param>
		/// <param name="returnCode">returnCode</param>
		/// <param name="errorMessage">errorMessage</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:37</remarks>
        public bool SetClubClothes ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] clubClothesData, System.Byte[] itemString, System.Byte[] itemRowVersion,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_SetClubClothes");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddInParameter(commandWrapper, "@ClubClothesData", DbType.Binary, clubClothesData);
			database.AddInParameter(commandWrapper, "@ItemString", DbType.Binary, itemString);
			database.AddInParameter(commandWrapper, "@ItemRowVersion", DbType.Binary, itemRowVersion);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);
			database.AddOutParameter(commandWrapper, "@ErrorMessage", DbType.String,500);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            errorMessage=(System.String)database.GetParameterValue(commandWrapper, "@ErrorMessage");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SaveHireSolution
		
		/// <summary>
        /// SaveHireSolution
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="formationId">formationId</param>
		/// <param name="playerString">playerString</param>
		/// <param name="hirePlayerString">hirePlayerString</param>
		/// <param name="veteranCount">veteranCount</param>
		/// <param name="orangeCount">orangeCount</param>
		/// <param name="combCount">combCount</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:37</remarks>
        public bool SaveHireSolution ( System.Guid managerId, System.Int32 formationId, System.String playerString, System.String hirePlayerString, System.Int32 veteranCount, System.Int32 orangeCount, System.Int32 combCount,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_SaveHireSolution");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@FormationId", DbType.Int32, formationId);
			database.AddInParameter(commandWrapper, "@PlayerString", DbType.AnsiStringFixedLength, playerString);
			database.AddInParameter(commandWrapper, "@HirePlayerString", DbType.AnsiStringFixedLength, hirePlayerString);
			database.AddInParameter(commandWrapper, "@VeteranCount", DbType.Int32, veteranCount);
			database.AddInParameter(commandWrapper, "@OrangeCount", DbType.Int32, orangeCount);
			database.AddInParameter(commandWrapper, "@CombCount", DbType.Int32, combCount);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  SetUsePlayerCard
		
		/// <summary>
        /// SetUsePlayerCard
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="managerId">managerId</param>
		/// <param name="mod">mod</param>
		/// <param name="playerCardData">playerCardData</param>
		/// <param name="returnCode">returnCode</param>
		/// <param name="errorMessage">errorMessage</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/7/27 19:51:37</remarks>
        public bool SetUsePlayerCard ( System.Guid idx, System.Guid managerId, System.Int32 mod, System.Byte[] playerCardData,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Teammember_SetUsePlayerCard");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddInParameter(commandWrapper, "@PlayerCardData", DbType.Binary, playerCardData);
			database.AddParameter(commandWrapper, "@ReturnCode", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,returnCode);
			database.AddOutParameter(commandWrapper, "@ErrorMessage", DbType.String,500);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            errorMessage=(System.String)database.GetParameterValue(commandWrapper, "@ErrorMessage");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region Insert
		
		/// <summary>
        /// 带事务Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/7/27 19:51:37</remarks>
        public bool Insert(TeammemberEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Teammember_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@PlayerId", DbType.Int32, entity.PlayerId);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@UsedProperty", DbType.Int32, entity.UsedProperty);
			database.AddInParameter(commandWrapper, "@Speed", DbType.Double, entity.Speed);
			database.AddInParameter(commandWrapper, "@Shoot", DbType.Double, entity.Shoot);
			database.AddInParameter(commandWrapper, "@FreeKick", DbType.Double, entity.FreeKick);
			database.AddInParameter(commandWrapper, "@Balance", DbType.Double, entity.Balance);
			database.AddInParameter(commandWrapper, "@Physique", DbType.Double, entity.Physique);
			database.AddInParameter(commandWrapper, "@Bounce", DbType.Double, entity.Bounce);
			database.AddInParameter(commandWrapper, "@Aggression", DbType.Double, entity.Aggression);
			database.AddInParameter(commandWrapper, "@Disturb", DbType.Double, entity.Disturb);
			database.AddInParameter(commandWrapper, "@Interception", DbType.Double, entity.Interception);
			database.AddInParameter(commandWrapper, "@Dribble", DbType.Double, entity.Dribble);
			database.AddInParameter(commandWrapper, "@Pass", DbType.Double, entity.Pass);
			database.AddInParameter(commandWrapper, "@Mentality", DbType.Double, entity.Mentality);
			database.AddInParameter(commandWrapper, "@Response", DbType.Double, entity.Response);
			database.AddInParameter(commandWrapper, "@Positioning", DbType.Double, entity.Positioning);
			database.AddInParameter(commandWrapper, "@HandControl", DbType.Double, entity.HandControl);
			database.AddInParameter(commandWrapper, "@Acceleration", DbType.Double, entity.Acceleration);
			database.AddInParameter(commandWrapper, "@UsedPlayerCard", DbType.Binary, entity.UsedPlayerCard);
			database.AddInParameter(commandWrapper, "@UsedEquipment", DbType.Binary, entity.UsedEquipment);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@IsCopyed", DbType.Boolean, entity.IsCopyed);
			database.AddInParameter(commandWrapper, "@IsInherited", DbType.Boolean, entity.IsInherited);
			database.AddInParameter(commandWrapper, "@UsedBadge", DbType.Binary, entity.UsedBadge);
			database.AddInParameter(commandWrapper, "@ArousalLv", DbType.Int32, entity.ArousalLv);
			database.AddInParameter(commandWrapper, "@UsedClubClothes", DbType.Binary, entity.UsedClubClothes);
			database.AddInParameter(commandWrapper, "@StrengthenLevel", DbType.Int32, entity.StrengthenLevel);
			database.AddInParameter(commandWrapper, "@Power", DbType.Double, entity.Power);
			database.AddParameter(commandWrapper, "@Idx", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,entity.Idx);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
		
		#region Update
		
		/// <summary>
        /// 带事务的Update
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="trans">The trans.</param>
        /// <returns></returns>
        /// <remarks>2016/7/27 19:51:37</remarks>
        public bool Update(TeammemberEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_Teammember_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, entity.ManagerId);
			database.AddInParameter(commandWrapper, "@PlayerId", DbType.Int32, entity.PlayerId);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@UsedProperty", DbType.Int32, entity.UsedProperty);
			database.AddInParameter(commandWrapper, "@Speed", DbType.Double, entity.Speed);
			database.AddInParameter(commandWrapper, "@Shoot", DbType.Double, entity.Shoot);
			database.AddInParameter(commandWrapper, "@FreeKick", DbType.Double, entity.FreeKick);
			database.AddInParameter(commandWrapper, "@Balance", DbType.Double, entity.Balance);
			database.AddInParameter(commandWrapper, "@Physique", DbType.Double, entity.Physique);
			database.AddInParameter(commandWrapper, "@Bounce", DbType.Double, entity.Bounce);
			database.AddInParameter(commandWrapper, "@Aggression", DbType.Double, entity.Aggression);
			database.AddInParameter(commandWrapper, "@Disturb", DbType.Double, entity.Disturb);
			database.AddInParameter(commandWrapper, "@Interception", DbType.Double, entity.Interception);
			database.AddInParameter(commandWrapper, "@Dribble", DbType.Double, entity.Dribble);
			database.AddInParameter(commandWrapper, "@Pass", DbType.Double, entity.Pass);
			database.AddInParameter(commandWrapper, "@Mentality", DbType.Double, entity.Mentality);
			database.AddInParameter(commandWrapper, "@Response", DbType.Double, entity.Response);
			database.AddInParameter(commandWrapper, "@Positioning", DbType.Double, entity.Positioning);
			database.AddInParameter(commandWrapper, "@HandControl", DbType.Double, entity.HandControl);
			database.AddInParameter(commandWrapper, "@Acceleration", DbType.Double, entity.Acceleration);
			database.AddInParameter(commandWrapper, "@UsedPlayerCard", DbType.Binary, entity.UsedPlayerCard);
			database.AddInParameter(commandWrapper, "@UsedEquipment", DbType.Binary, entity.UsedEquipment);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@IsCopyed", DbType.Boolean, entity.IsCopyed);
			database.AddInParameter(commandWrapper, "@IsInherited", DbType.Boolean, entity.IsInherited);
			database.AddInParameter(commandWrapper, "@UsedBadge", DbType.Binary, entity.UsedBadge);
			database.AddInParameter(commandWrapper, "@ArousalLv", DbType.Int32, entity.ArousalLv);
			database.AddInParameter(commandWrapper, "@UsedClubClothes", DbType.Binary, entity.UsedClubClothes);
			database.AddInParameter(commandWrapper, "@StrengthenLevel", DbType.Int32, entity.StrengthenLevel);
			database.AddInParameter(commandWrapper, "@Power", DbType.Double, entity.Power);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            entity.Idx=(System.Guid)database.GetParameterValue(commandWrapper, "@Idx");
            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
