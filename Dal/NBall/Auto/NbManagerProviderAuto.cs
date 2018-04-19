

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
    
    public partial class NbManagerProvider
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
		/// 将IDataReader的当前记录读取到NbManagerEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbManagerEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbManagerEntity();
			
            obj.Idx = (System.Guid) reader["Idx"];
            obj.Account = (System.String) reader["Account"];
            obj.Name = (System.String) reader["Name"];
            obj.Logo = (System.String) reader["Logo"];
            obj.Type = (System.Int32) reader["Type"];
            obj.Level = (System.Int32) reader["Level"];
            obj.EXP = (System.Int32) reader["EXP"];
            obj.Sophisticate = (System.Int32) reader["Sophisticate"];
            obj.Score = (System.Int32) reader["Score"];
            obj.Coin = (System.Int32) reader["Coin"];
            obj.Reiki = (System.Int32) reader["Reiki"];
            obj.TeammemberMax = (System.Int32) reader["TeammemberMax"];
            obj.TrainSeatMax = (System.Int32) reader["TrainSeatMax"];
            obj.VipLevel = (System.Int32) reader["VipLevel"];
            obj.Mod = (System.Int32) reader["Mod"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
            obj.UpdateTime = (System.DateTime) reader["UpdateTime"];
            obj.RowVersion = (System.Byte[]) reader["RowVersion"];
            obj.FriendShipPoint = (System.Int32) reader["FriendShipPoint"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<NbManagerEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbManagerEntity>();
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
        public NbManagerProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbManagerProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="idx">idx</param>
        /// <returns>NbManagerEntity</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public NbManagerEntity GetById( System.Guid idx)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManager_GetById");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);

            
            NbManagerEntity obj=null;
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
		
		#region  GetByName
		
		/// <summary>
        /// GetByName
        /// </summary>
		/// <param name="name">name</param>
        /// <returns>NbManagerEntity</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public NbManagerEntity GetByName( System.String name)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_GetByName");
            
			database.AddInParameter(commandWrapper, "@Name", DbType.String, name);

            
            NbManagerEntity obj=null;
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
		
		#region  GetAll
		
		/// <summary>
        /// GetAll
        /// </summary>
        /// <returns>NbManagerEntity列表</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public List<NbManagerEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManager_GetAll");
            

            
            List<NbManagerEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetByAccount
		
		/// <summary>
        /// GetByAccount
        /// </summary>
        /// <returns>NbManagerEntity列表</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public List<NbManagerEntity> GetByAccount( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_GetByAccount");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            List<NbManagerEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  GetMaxLevel
		
		/// <summary>
        /// GetMaxLevel
        /// </summary>

        /// <returns>Int32</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public Int32 GetMaxLevel ()
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NbManager_GetMaxLevel");
            

            
            
            Int32 rValue = (Int32)database.ExecuteScalar(commandWrapper);
            

            return rValue;
        }
		
		#endregion		  
		
		#region  DeleteRole
		
		/// <summary>
        /// DeleteRole
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="bindCode">bindCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool DeleteRole ( System.String account, System.Guid bindCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_DeleteRole");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@BindCode", DbType.Guid, bindCode);

            
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
		
		#region  BindAccount
		
		/// <summary>
        /// BindAccount
        /// </summary>
		/// <param name="newAccount">newAccount</param>
		/// <param name="newName">newName</param>
		/// <param name="newMid">newMid</param>
		/// <param name="newMod">newMod</param>
		/// <param name="bindCode">bindCode</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool BindAccount ( System.String newAccount, System.String newName, System.String newMid, System.String newMod, System.Guid bindCode,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_BindAccount");
            
			database.AddInParameter(commandWrapper, "@NewAccount", DbType.AnsiString, newAccount);
			database.AddInParameter(commandWrapper, "@NewName", DbType.AnsiString, newName);
			database.AddInParameter(commandWrapper, "@NewMid", DbType.AnsiString, newMid);
			database.AddInParameter(commandWrapper, "@NewMod", DbType.AnsiString, newMod);
			database.AddInParameter(commandWrapper, "@bindCode", DbType.Guid, bindCode);
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
		
		#region  UpdateName
		
		/// <summary>
        /// UpdateName
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="oldName">oldName</param>
		/// <param name="newName">newName</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool UpdateName ( System.Guid managerId, System.String oldName, System.String newName,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_UpdateName");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@OldName", DbType.String, oldName);
			database.AddInParameter(commandWrapper, "@NewName", DbType.String, newName);
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
		
		#region  TransferZoneByAccount
		
		/// <summary>
        /// TransferZoneByAccount
        /// </summary>
		/// <param name="sourceDbFullName">sourceDbFullName</param>
		/// <param name="account">account</param>
		/// <param name="name">name</param>
		/// <param name="mid">mid</param>
		/// <param name="mod">mod</param>
		/// <param name="newAccount">newAccount</param>
		/// <param name="newName">newName</param>
		/// <param name="newMid">newMid</param>
		/// <param name="newMod">newMod</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool TransferZoneByAccount ( System.String sourceDbFullName, System.String account, System.String name, System.String mid, System.String mod, System.String newAccount, System.String newName, System.String newMid, System.String newMod,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_TransferZoneByAccount");
            
			database.AddInParameter(commandWrapper, "@SourceDbFullName", DbType.AnsiString, sourceDbFullName);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@Name", DbType.AnsiString, name);
			database.AddInParameter(commandWrapper, "@Mid", DbType.AnsiString, mid);
			database.AddInParameter(commandWrapper, "@Mod", DbType.AnsiString, mod);
			database.AddInParameter(commandWrapper, "@NewAccount", DbType.AnsiString, newAccount);
			database.AddInParameter(commandWrapper, "@NewName", DbType.AnsiString, newName);
			database.AddInParameter(commandWrapper, "@NewMid", DbType.AnsiString, newMid);
			database.AddInParameter(commandWrapper, "@NewMod", DbType.AnsiString, newMod);

            
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
		
		#region  AccountExists
		
		/// <summary>
        /// AccountExists
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool AccountExists ( System.String account,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_AccountExists");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
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
		
		#region  UpdateLogo
		
		/// <summary>
        /// UpdateLogo
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="logo">logo</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool UpdateLogo ( System.Guid managerId, System.String logo,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_UpdateLogo");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, logo);

            
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
		
		#region  AddScore
		
		/// <summary>
        /// AddScore
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="score">score</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool AddScore ( System.Guid managerId, System.Int32 score,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_AddScore");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, score);

            
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
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="rowVersion">rowVersion</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool Delete ( System.Guid idx, System.Byte[] rowVersion,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbManager_Delete");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);

            
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
		
		#region  AddCoinAndScore
		
		/// <summary>
        /// AddCoinAndScore
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="coin">coin</param>
		/// <param name="score">score</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool AddCoinAndScore ( System.Guid managerId, System.Int32 coin, System.Int32 score,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_AddCoinAndScore");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, coin);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, score);

            
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
		
		#region  Create
		
		/// <summary>
        /// Create
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="mod">mod</param>
		/// <param name="name">name</param>
		/// <param name="stamina">stamina</param>
		/// <param name="packageSize">packageSize</param>
		/// <param name="itemVersion">itemVersion</param>
		/// <param name="teammemberMax">teammemberMax</param>
		/// <param name="registerTrainSeat">registerTrainSeat</param>
		/// <param name="registerLadderScore">registerLadderScore</param>
		/// <param name="nameRepeatCode">nameRepeatCode</param>
		/// <param name="existsManagerCode">existsManagerCode</param>
		/// <param name="errorMsg">errorMsg</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool Create ( System.Guid managerId, System.Int32 mod, System.String name, System.Int32 stamina, System.Int32 packageSize, System.Int32 itemVersion, System.Int32 teammemberMax, System.Int32 registerTrainSeat, System.Int32 registerLadderScore, System.Int32 nameRepeatCode, System.Int32 existsManagerCode,ref  System.String errorMsg,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_Create");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, mod);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, name);
			database.AddInParameter(commandWrapper, "@Stamina", DbType.Int32, stamina);
			database.AddInParameter(commandWrapper, "@PackageSize", DbType.Int32, packageSize);
			database.AddInParameter(commandWrapper, "@ItemVersion", DbType.Int32, itemVersion);
			database.AddInParameter(commandWrapper, "@TeammemberMax", DbType.Int32, teammemberMax);
			database.AddInParameter(commandWrapper, "@RegisterTrainSeat", DbType.Int32, registerTrainSeat);
			database.AddInParameter(commandWrapper, "@RegisterLadderScore", DbType.Int32, registerLadderScore);
			database.AddInParameter(commandWrapper, "@NameRepeatCode", DbType.Int32, nameRepeatCode);
			database.AddInParameter(commandWrapper, "@ExistsManagerCode", DbType.Int32, existsManagerCode);
			database.AddOutParameter(commandWrapper, "@ErrorMsg", DbType.String,500);
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
            errorMsg=(System.String)database.GetParameterValue(commandWrapper, "@ErrorMsg");
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  Register
		
		/// <summary>
        /// Register
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="name">name</param>
		/// <param name="logo">logo</param>
		/// <param name="templateId">templateId</param>
		/// <param name="playerString">playerString</param>
		/// <param name="registerFormationId">registerFormationId</param>
		/// <param name="kpi">kpi</param>
		/// <param name="nameRepeatCode">nameRepeatCode</param>
		/// <param name="existsManagerCode">existsManagerCode</param>
		/// <param name="registerFailCode">registerFailCode</param>
		/// <param name="isBot">isBot</param>
		/// <param name="managerId">managerId</param>
		/// <param name="returnCode">returnCode</param>
		/// <param name="errorMessage">errorMessage</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool Register ( System.String account, System.String name, System.String logo, System.Int32 templateId, System.String playerString, System.Int32 registerFormationId, System.Int32 kpi, System.Int32 nameRepeatCode, System.Int32 existsManagerCode, System.Int32 registerFailCode, System.Boolean isBot,ref  System.Guid managerId,ref  System.Int32 returnCode,ref  System.String errorMessage,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_Register");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, name);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, logo);
			database.AddInParameter(commandWrapper, "@TemplateId", DbType.Int32, templateId);
            database.AddInParameter(commandWrapper, "@PlayerString", DbType.AnsiString, playerString);
			database.AddInParameter(commandWrapper, "@RegisterFormationId", DbType.Int32, registerFormationId);
			database.AddInParameter(commandWrapper, "@Kpi", DbType.Int32, kpi);
			database.AddInParameter(commandWrapper, "@NameRepeatCode", DbType.Int32, nameRepeatCode);
			database.AddInParameter(commandWrapper, "@ExistsManagerCode", DbType.Int32, existsManagerCode);
			database.AddInParameter(commandWrapper, "@RegisterFailCode", DbType.Int32, registerFailCode);
			database.AddInParameter(commandWrapper, "@IsBot", DbType.Boolean, isBot);
			database.AddParameter(commandWrapper, "@ManagerId", DbType.Guid, ParameterDirection.InputOutput,"",DataRowVersion.Current,managerId);
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
            managerId=(System.Guid)database.GetParameterValue(commandWrapper, "@ManagerId");
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            errorMessage=(System.String)database.GetParameterValue(commandWrapper, "@ErrorMessage");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  NameExists
		
		/// <summary>
        /// NameExists
        /// </summary>
		/// <param name="name">name</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool NameExists ( System.String name,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_NameExists");
            
			database.AddInParameter(commandWrapper, "@Name", DbType.String, name);
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
		
		#region  AddCoin
		
		/// <summary>
        /// AddCoin
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="coin">coin</param>
		/// <param name="curCoin">curCoin</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool AddCoin ( System.Guid managerId, System.Int32 coin,ref  System.Int32 curCoin,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_AddCoin");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, coin);
			database.AddParameter(commandWrapper, "@CurCoin", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curCoin);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            curCoin=(System.Int32)database.GetParameterValue(commandWrapper, "@CurCoin");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  CostCoin
		
		/// <summary>
        /// CostCoin
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="coin">coin</param>
		/// <param name="curCoin">curCoin</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool CostCoin ( System.Guid managerId, System.Int32 coin,ref  System.Int32 curCoin,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_CostCoin");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, coin);
			database.AddParameter(commandWrapper, "@CurCoin", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curCoin);
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
            curCoin=(System.Int32)database.GetParameterValue(commandWrapper, "@CurCoin");
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  AddReiki
		
		/// <summary>
        /// AddReiki
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="reiki">reiki</param>
		/// <param name="curReiki">curReiki</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool AddReiki ( System.Guid managerId, System.Int32 reiki,ref  System.Int32 curReiki,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_AddReiki");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Reiki", DbType.Int32, reiki);
			database.AddParameter(commandWrapper, "@CurReiki", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curReiki);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            curReiki=(System.Int32)database.GetParameterValue(commandWrapper, "@CurReiki");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  CostReiki
		
		/// <summary>
        /// CostReiki
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="reiki">reiki</param>
		/// <param name="curReiki">curReiki</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool CostReiki ( System.Guid managerId, System.Int32 reiki,ref  System.Int32 curReiki,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_CostReiki");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Reiki", DbType.Int32, reiki);
			database.AddParameter(commandWrapper, "@CurReiki", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curReiki);
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
            curReiki=(System.Int32)database.GetParameterValue(commandWrapper, "@CurReiki");
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  AddSophisticate
		
		/// <summary>
        /// AddSophisticate
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="sophisticate">sophisticate</param>
		/// <param name="curSophisticate">curSophisticate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool AddSophisticate ( System.Guid managerId, System.Int32 sophisticate,ref  System.Int32 curSophisticate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_AddSophisticate");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Sophisticate", DbType.Int32, sophisticate);
			database.AddParameter(commandWrapper, "@CurSophisticate", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curSophisticate);

            
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
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  CostSophisticate
		
		/// <summary>
        /// CostSophisticate
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="sophisticate">sophisticate</param>
		/// <param name="sophisticateShortageCode">sophisticateShortageCode</param>
		/// <param name="curSophisticate">curSophisticate</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:57</remarks>
        public bool CostSophisticate ( System.Guid managerId, System.Int32 sophisticate, System.Int32 sophisticateShortageCode,ref  System.Int32 curSophisticate,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_CostSophisticate");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
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
		
		#region  Save
		
		/// <summary>
        /// Save
        /// </summary>
		/// <param name="idx">idx</param>
		/// <param name="level">level</param>
		/// <param name="eXP">eXP</param>
		/// <param name="sophisticate">sophisticate</param>
		/// <param name="score">score</param>
		/// <param name="coin">coin</param>
		/// <param name="reiki">reiki</param>
		/// <param name="teammemberMax">teammemberMax</param>
		/// <param name="trainSeatMax">trainSeatMax</param>
		/// <param name="vipLevel">vipLevel</param>
		/// <param name="functionList">functionList</param>
		/// <param name="levelGiftExpired">levelGiftExpired</param>
		/// <param name="levelGiftExpired2">levelGiftExpired2</param>
		/// <param name="levelGiftExpired3">levelGiftExpired3</param>
		/// <param name="levelGiftStep">levelGiftStep</param>
		/// <param name="rowVersion">rowVersion</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:58</remarks>
        public bool Save ( System.Guid idx, System.Int32 level, System.Int32 eXP, System.Int32 sophisticate, System.Int32 score, System.Int32 coin, System.Int32 reiki, System.Int32 teammemberMax, System.Int32 trainSeatMax, System.Int32 vipLevel, System.String functionList, System.DateTime levelGiftExpired, System.DateTime levelGiftExpired2, System.DateTime levelGiftExpired3, System.Int32 levelGiftStep, System.Byte[] rowVersion,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_Save");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, idx);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, level);
			database.AddInParameter(commandWrapper, "@EXP", DbType.Int32, eXP);
			database.AddInParameter(commandWrapper, "@Sophisticate", DbType.Int32, sophisticate);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, score);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, coin);
			database.AddInParameter(commandWrapper, "@Reiki", DbType.Int32, reiki);
			database.AddInParameter(commandWrapper, "@TeammemberMax", DbType.Int32, teammemberMax);
			database.AddInParameter(commandWrapper, "@TrainSeatMax", DbType.Int32, trainSeatMax);
			database.AddInParameter(commandWrapper, "@VipLevel", DbType.Int32, vipLevel);
			database.AddInParameter(commandWrapper, "@FunctionList", DbType.AnsiString, functionList);
			database.AddInParameter(commandWrapper, "@LevelGiftExpired", DbType.DateTime, levelGiftExpired);
			database.AddInParameter(commandWrapper, "@LevelGiftExpired2", DbType.DateTime, levelGiftExpired2);
			database.AddInParameter(commandWrapper, "@LevelGiftExpired3", DbType.DateTime, levelGiftExpired3);
			database.AddInParameter(commandWrapper, "@LevelGiftStep", DbType.Int32, levelGiftStep);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, rowVersion);
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
		
		#region  AddCoinAndReiki
		
		/// <summary>
        /// AddCoinAndReiki
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="coin">coin</param>
		/// <param name="reiki">reiki</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:58</remarks>
        public bool AddCoinAndReiki ( System.Guid managerId, System.Int32 coin, System.Int32 reiki,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_NBManager_AddCoinAndReiki");
            
			database.AddInParameter(commandWrapper, "@managerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@coin", DbType.Int32, coin);
			database.AddInParameter(commandWrapper, "@reiki", DbType.Int32, reiki);

            
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
		
		#region  AddFriendShipPoint
		
		/// <summary>
        /// AddFriendShipPoint
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="friendShipPoint">friendShipPoint</param>
		/// <param name="curFriendShipPoint">curFriendShipPoint</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:58</remarks>
        public bool AddFriendShipPoint ( System.Guid managerId, System.Int32 friendShipPoint,ref  System.Int32 curFriendShipPoint,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_AddFriendShipPoint");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@FriendShipPoint", DbType.Int32, friendShipPoint);
			database.AddParameter(commandWrapper, "@CurFriendShipPoint", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curFriendShipPoint);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            curFriendShipPoint=(System.Int32)database.GetParameterValue(commandWrapper, "@CurFriendShipPoint");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  CostFriendShipPoint
		
		/// <summary>
        /// CostFriendShipPoint
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="friendShipPoint">friendShipPoint</param>
		/// <param name="curFriendShipPoint">curFriendShipPoint</param>
		/// <param name="returnCode">returnCode</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:58</remarks>
        public bool CostFriendShipPoint ( System.Guid managerId, System.Int32 friendShipPoint,ref  System.Int32 curFriendShipPoint,ref  System.Int32 returnCode,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_Manager_CostFriendShipPoint");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@FriendShipPoint", DbType.Int32, friendShipPoint);
			database.AddParameter(commandWrapper, "@CurFriendShipPoint", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curFriendShipPoint);
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
            curFriendShipPoint=(System.Int32)database.GetParameterValue(commandWrapper, "@CurFriendShipPoint");
            returnCode=(System.Int32)database.GetParameterValue(commandWrapper, "@ReturnCode");
            
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
        /// <remarks>2016/6/17 10:29:58</remarks>
        public bool Insert(NbManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManager_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, entity.Logo);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@EXP", DbType.Int32, entity.EXP);
			database.AddInParameter(commandWrapper, "@Sophisticate", DbType.Int32, entity.Sophisticate);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, entity.Coin);
			database.AddInParameter(commandWrapper, "@Reiki", DbType.Int32, entity.Reiki);
			database.AddInParameter(commandWrapper, "@TeammemberMax", DbType.Int32, entity.TeammemberMax);
			database.AddInParameter(commandWrapper, "@TrainSeatMax", DbType.Int32, entity.TrainSeatMax);
			database.AddInParameter(commandWrapper, "@VipLevel", DbType.Int32, entity.VipLevel);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, entity.Mod);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@FriendShipPoint", DbType.Int32, entity.FriendShipPoint);
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
        /// <remarks>2016/6/17 10:29:58</remarks>
        public bool Update(NbManagerEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbManager_Update");
            
			database.AddInParameter(commandWrapper, "@Idx", DbType.Guid, entity.Idx);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Name", DbType.String, entity.Name);
			database.AddInParameter(commandWrapper, "@Logo", DbType.AnsiString, entity.Logo);
			database.AddInParameter(commandWrapper, "@Type", DbType.Int32, entity.Type);
			database.AddInParameter(commandWrapper, "@Level", DbType.Int32, entity.Level);
			database.AddInParameter(commandWrapper, "@EXP", DbType.Int32, entity.EXP);
			database.AddInParameter(commandWrapper, "@Sophisticate", DbType.Int32, entity.Sophisticate);
			database.AddInParameter(commandWrapper, "@Score", DbType.Int32, entity.Score);
			database.AddInParameter(commandWrapper, "@Coin", DbType.Int32, entity.Coin);
			database.AddInParameter(commandWrapper, "@Reiki", DbType.Int32, entity.Reiki);
			database.AddInParameter(commandWrapper, "@TeammemberMax", DbType.Int32, entity.TeammemberMax);
			database.AddInParameter(commandWrapper, "@TrainSeatMax", DbType.Int32, entity.TrainSeatMax);
			database.AddInParameter(commandWrapper, "@VipLevel", DbType.Int32, entity.VipLevel);
			database.AddInParameter(commandWrapper, "@Mod", DbType.Int32, entity.Mod);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);
			database.AddInParameter(commandWrapper, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
			database.AddInParameter(commandWrapper, "@RowVersion", DbType.Binary, entity.RowVersion);
			database.AddInParameter(commandWrapper, "@FriendShipPoint", DbType.Int32, entity.FriendShipPoint);

            
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
