

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
    
    public partial class NbUserProvider
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
		/// 将IDataReader的当前记录读取到NbUserEntity 对象
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public NbUserEntity LoadSingleRow(IDataReader reader)
		{
			var obj = new NbUserEntity();
			
            obj.Account = (System.String) reader["Account"];
            obj.Source = (System.String) reader["Source"];
            obj.LastLoginTime = (System.DateTime) reader["LastLoginTime"];
            obj.LastLoginIp = (System.String) reader["LastLoginIp"];
            obj.LastLoginDate = (System.DateTime) reader["LastLoginDate"];
            obj.ContinuingLoginDay = (System.Int32) reader["ContinuingLoginDay"];
            obj.RegisterIp = (System.String) reader["RegisterIp"];
            obj.RegisterDate = (System.DateTime) reader["RegisterDate"];
            obj.Status = (System.Int32) reader["Status"];
            obj.RowTime = (System.DateTime) reader["RowTime"];
		
			return obj;
		}
		#endregion
        
        #region LoadRows
        /// <summary>
        /// 将IDataReader的记录读取到PostEntity 列表
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public List<NbUserEntity> LoadRows(IDataReader reader)
        {
            var clt = new List<NbUserEntity>();
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
        public NbUserProvider()
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(DBTYPE);
        }

        public NbUserProvider(string zoneName)
        {
            this.ConnectionString = ConnectionFactory.Instance.GetConnectionString(zoneName,DBTYPE);
        }
        #endregion
         
		#region  GetById
		
		/// <summary>
        /// GetById
        /// </summary>
		/// <param name="account">account</param>
        /// <returns>NbUserEntity</returns>
        /// <remarks>2016/6/17 10:29:27</remarks>
        public NbUserEntity GetById( System.String account)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbUser_GetById");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
            NbUserEntity obj=null;
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
		
		#region  GetByAccount
		
		/// <summary>
        /// GetByAccount
        /// </summary>
		/// <param name="account">account</param>
		/// <param name="userIp">userIp</param>
		/// <param name="yesterday">yesterday</param>
		/// <param name="today">today</param>
		/// <param name="status">status</param>
        /// <returns>NbUserEntity</returns>
        /// <remarks>2016/6/17 10:29:27</remarks>
        public NbUserEntity GetByAccount( System.String account, System.String userIp, System.DateTime yesterday, System.DateTime today, System.Int32 status)
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_User_GetByAccount");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@UserIp", DbType.AnsiString, userIp);
			database.AddInParameter(commandWrapper, "@Yesterday", DbType.DateTime, yesterday);
			database.AddInParameter(commandWrapper, "@Today", DbType.DateTime, today);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, status);

            
            NbUserEntity obj=null;
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
        /// <returns>NbUserEntity列表</returns>
        /// <remarks>2016/6/17 10:29:27</remarks>
        public List<NbUserEntity> GetAll()
        {
			var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbUser_GetAll");
            

            
            List<NbUserEntity> list = null;
            using (IDataReader reader = database.ExecuteReader(commandWrapper))
            {
                list = LoadRows(reader);
            }
                
            return list;
        }
		
		#endregion		  
		
		#region  Delete
		
		/// <summary>
        /// Delete
        /// </summary>
		/// <param name="account">account</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:27</remarks>
        public bool Delete ( System.String account,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("P_NbUser_Delete");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);

            
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
		
		#region  GetTotal
		
		/// <summary>
        /// GetTotal
        /// </summary>
		/// <param name="totalUser">totalUser</param>
		/// <param name="totalManager">totalManager</param>
		/// <param name="totalPay">totalPay</param>
		/// <param name="pointRemain">pointRemain</param>
		/// <param name="onlineMinutes">onlineMinutes</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:28</remarks>
        public bool GetTotal (ref  System.Int32 totalUser,ref  System.Int32 totalManager,ref  System.Int64 totalPay,ref  System.Int64 pointRemain,ref  System.Int64 onlineMinutes,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_Statistic_GetTotal");
            
			database.AddParameter(commandWrapper, "@TotalUser", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,totalUser);
			database.AddParameter(commandWrapper, "@TotalManager", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,totalManager);
			database.AddParameter(commandWrapper, "@TotalPay", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,totalPay);
			database.AddParameter(commandWrapper, "@PointRemain", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,pointRemain);
			database.AddParameter(commandWrapper, "@OnlineMinutes", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,onlineMinutes);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            totalUser=(System.Int32)database.GetParameterValue(commandWrapper, "@TotalUser");
            totalManager=(System.Int32)database.GetParameterValue(commandWrapper, "@TotalManager");
            totalPay=(System.Int64)database.GetParameterValue(commandWrapper, "@TotalPay");
            pointRemain=(System.Int64)database.GetParameterValue(commandWrapper, "@PointRemain");
            onlineMinutes=(System.Int64)database.GetParameterValue(commandWrapper, "@OnlineMinutes");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetDetail
		
		/// <summary>
        /// GetDetail
        /// </summary>
		/// <param name="startTime">startTime</param>
		/// <param name="endTime">endTime</param>
		/// <param name="loginUser">loginUser</param>
		/// <param name="chargeUser">chargeUser</param>
		/// <param name="chargeCash">chargeCash</param>
		/// <param name="consumePoint">consumePoint</param>
		/// <param name="newUser">newUser</param>
		/// <param name="newManager">newManager</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:28</remarks>
        public bool GetDetail ( System.DateTime startTime, System.DateTime endTime,ref  System.Int32 loginUser,ref  System.Int32 chargeUser,ref  System.Int32 chargeCash,ref  System.Int32 consumePoint,ref  System.Int32 newUser,ref  System.Int32 newManager,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_Statistic_GetDetail");
            
			database.AddInParameter(commandWrapper, "@StartTime", DbType.DateTime, startTime);
			database.AddInParameter(commandWrapper, "@EndTime", DbType.DateTime, endTime);
			database.AddParameter(commandWrapper, "@LoginUser", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,loginUser);
			database.AddParameter(commandWrapper, "@ChargeUser", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,chargeUser);
			database.AddParameter(commandWrapper, "@ChargeCash", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,chargeCash);
			database.AddParameter(commandWrapper, "@ConsumePoint", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,consumePoint);
			database.AddParameter(commandWrapper, "@NewUser", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,newUser);
			database.AddParameter(commandWrapper, "@NewManager", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,newManager);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            loginUser=(System.Int32)database.GetParameterValue(commandWrapper, "@LoginUser");
            chargeUser=(System.Int32)database.GetParameterValue(commandWrapper, "@ChargeUser");
            chargeCash=(System.Int32)database.GetParameterValue(commandWrapper, "@ChargeCash");
            consumePoint=(System.Int32)database.GetParameterValue(commandWrapper, "@ConsumePoint");
            newUser=(System.Int32)database.GetParameterValue(commandWrapper, "@NewUser");
            newManager=(System.Int32)database.GetParameterValue(commandWrapper, "@NewManager");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetKpi
		
		/// <summary>
        /// GetKpi
        /// </summary>
		/// <param name="curTime">curTime</param>
		/// <param name="registerUser">registerUser</param>
		/// <param name="registerManager">registerManager</param>
		/// <param name="dau">dau</param>
		/// <param name="dUniqueIp">dUniqueIp</param>
		/// <param name="dNewUser">dNewUser</param>
		/// <param name="dNewManager">dNewManager</param>
		/// <param name="dLostUser7">dLostUser7</param>
		/// <param name="dLostUser15">dLostUser15</param>
		/// <param name="dLostUser30">dLostUser30</param>
		/// <param name="retention2">retention2</param>
		/// <param name="retention3">retention3</param>
		/// <param name="retention4">retention4</param>
		/// <param name="retention5">retention5</param>
		/// <param name="retention6">retention6</param>
		/// <param name="retention7">retention7</param>
		/// <param name="retention15">retention15</param>
		/// <param name="retention30">retention30</param>
		/// <param name="wau">wau</param>
		/// <param name="wLost">wLost</param>
		/// <param name="wHonor">wHonor</param>
		/// <param name="wHonorLost">wHonorLost</param>
		/// <param name="mau">mau</param>
		/// <param name="payUserCount">payUserCount</param>
		/// <param name="payCount">payCount</param>
		/// <param name="payTotal">payTotal</param>
		/// <param name="paySum">paySum</param>
		/// <param name="payFirst">payFirst</param>
		/// <param name="payWLost">payWLost</param>
		/// <param name="lTV">lTV</param>
		/// <param name="pointRemain">pointRemain</param>
		/// <param name="pointConsume">pointConsume</param>
		/// <param name="pointCirculate">pointCirculate</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:28</remarks>
        public bool GetKpi ( System.DateTime curTime,ref  System.Int32 registerUser,ref  System.Int32 registerManager,ref  System.Int32 dau,ref  System.Int32 dUniqueIp,ref  System.Int32 dNewUser,ref  System.Int32 dNewManager,ref  System.Int32 dLostUser7,ref  System.Int32 dLostUser15,ref  System.Int32 dLostUser30,ref  System.Int32 retention2,ref  System.Int32 retention3,ref  System.Int32 retention4,ref  System.Int32 retention5,ref  System.Int32 retention6,ref  System.Int32 retention7,ref  System.Int32 retention15,ref  System.Int32 retention30,ref  System.Int32 wau,ref  System.Int32 wLost,ref  System.Int32 wHonor,ref  System.Int32 wHonorLost,ref  System.Int32 mau,ref  System.Int32 payUserCount,ref  System.Int32 payCount,ref  System.Int32 payTotal,ref  System.Int64 paySum,ref  System.Int32 payFirst,ref  System.Int32 payWLost,ref  System.Int32 lTV,ref  System.Int64 pointRemain,ref  System.Int64 pointConsume,ref  System.Int64 pointCirculate,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_Statistic_GetKpi");
            
			database.AddInParameter(commandWrapper, "@CurTime", DbType.DateTime, curTime);
			database.AddParameter(commandWrapper, "@RegisterUser", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,registerUser);
			database.AddParameter(commandWrapper, "@RegisterManager", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,registerManager);
			database.AddParameter(commandWrapper, "@Dau", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dau);
			database.AddParameter(commandWrapper, "@DUniqueIp", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dUniqueIp);
			database.AddParameter(commandWrapper, "@DNewUser", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dNewUser);
			database.AddParameter(commandWrapper, "@DNewManager", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dNewManager);
			database.AddParameter(commandWrapper, "@DLostUser7", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dLostUser7);
			database.AddParameter(commandWrapper, "@DLostUser15", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dLostUser15);
			database.AddParameter(commandWrapper, "@DLostUser30", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dLostUser30);
			database.AddParameter(commandWrapper, "@Retention2", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,retention2);
			database.AddParameter(commandWrapper, "@Retention3", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,retention3);
			database.AddParameter(commandWrapper, "@Retention4", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,retention4);
			database.AddParameter(commandWrapper, "@Retention5", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,retention5);
			database.AddParameter(commandWrapper, "@Retention6", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,retention6);
			database.AddParameter(commandWrapper, "@Retention7", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,retention7);
			database.AddParameter(commandWrapper, "@Retention15", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,retention15);
			database.AddParameter(commandWrapper, "@Retention30", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,retention30);
			database.AddParameter(commandWrapper, "@Wau", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,wau);
			database.AddParameter(commandWrapper, "@WLost", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,wLost);
			database.AddParameter(commandWrapper, "@WHonor", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,wHonor);
			database.AddParameter(commandWrapper, "@WHonorLost", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,wHonorLost);
			database.AddParameter(commandWrapper, "@Mau", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,mau);
			database.AddParameter(commandWrapper, "@PayUserCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,payUserCount);
			database.AddParameter(commandWrapper, "@PayCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,payCount);
			database.AddParameter(commandWrapper, "@PayTotal", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,payTotal);
			database.AddParameter(commandWrapper, "@PaySum", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,paySum);
			database.AddParameter(commandWrapper, "@PayFirst", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,payFirst);
			database.AddParameter(commandWrapper, "@PayWLost", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,payWLost);
			database.AddParameter(commandWrapper, "@LTV", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,lTV);
			database.AddParameter(commandWrapper, "@PointRemain", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,pointRemain);
			database.AddParameter(commandWrapper, "@PointConsume", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,pointConsume);
			database.AddParameter(commandWrapper, "@PointCirculate", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,pointCirculate);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            registerUser=(System.Int32)database.GetParameterValue(commandWrapper, "@RegisterUser");
            registerManager=(System.Int32)database.GetParameterValue(commandWrapper, "@RegisterManager");
            dau=(System.Int32)database.GetParameterValue(commandWrapper, "@Dau");
            dUniqueIp=(System.Int32)database.GetParameterValue(commandWrapper, "@DUniqueIp");
            dNewUser=(System.Int32)database.GetParameterValue(commandWrapper, "@DNewUser");
            dNewManager=(System.Int32)database.GetParameterValue(commandWrapper, "@DNewManager");
            dLostUser7=(System.Int32)database.GetParameterValue(commandWrapper, "@DLostUser7");
            dLostUser15=(System.Int32)database.GetParameterValue(commandWrapper, "@DLostUser15");
            dLostUser30=(System.Int32)database.GetParameterValue(commandWrapper, "@DLostUser30");
            retention2=(System.Int32)database.GetParameterValue(commandWrapper, "@Retention2");
            retention3=(System.Int32)database.GetParameterValue(commandWrapper, "@Retention3");
            retention4=(System.Int32)database.GetParameterValue(commandWrapper, "@Retention4");
            retention5=(System.Int32)database.GetParameterValue(commandWrapper, "@Retention5");
            retention6=(System.Int32)database.GetParameterValue(commandWrapper, "@Retention6");
            retention7=(System.Int32)database.GetParameterValue(commandWrapper, "@Retention7");
            retention15=(System.Int32)database.GetParameterValue(commandWrapper, "@Retention15");
            retention30=(System.Int32)database.GetParameterValue(commandWrapper, "@Retention30");
            wau=(System.Int32)database.GetParameterValue(commandWrapper, "@Wau");
            wLost=(System.Int32)database.GetParameterValue(commandWrapper, "@WLost");
            wHonor=(System.Int32)database.GetParameterValue(commandWrapper, "@WHonor");
            wHonorLost=(System.Int32)database.GetParameterValue(commandWrapper, "@WHonorLost");
            mau=(System.Int32)database.GetParameterValue(commandWrapper, "@Mau");
            payUserCount=(System.Int32)database.GetParameterValue(commandWrapper, "@PayUserCount");
            payCount=(System.Int32)database.GetParameterValue(commandWrapper, "@PayCount");
            payTotal=(System.Int32)database.GetParameterValue(commandWrapper, "@PayTotal");
            paySum=(System.Int64)database.GetParameterValue(commandWrapper, "@PaySum");
            payFirst=(System.Int32)database.GetParameterValue(commandWrapper, "@PayFirst");
            payWLost=(System.Int32)database.GetParameterValue(commandWrapper, "@PayWLost");
            lTV=(System.Int32)database.GetParameterValue(commandWrapper, "@LTV");
            pointRemain=(System.Int64)database.GetParameterValue(commandWrapper, "@PointRemain");
            pointConsume=(System.Int64)database.GetParameterValue(commandWrapper, "@PointConsume");
            pointCirculate=(System.Int64)database.GetParameterValue(commandWrapper, "@PointCirculate");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  GetOnline
		
		/// <summary>
        /// GetOnline
        /// </summary>
		/// <param name="userCount">userCount</param>
		/// <param name="totalTime">totalTime</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:28</remarks>
        public bool GetOnline (ref  System.Int32 userCount,ref  System.Int64 totalTime,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_Statistic_GetOnline");
            
			database.AddParameter(commandWrapper, "@UserCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,userCount);
			database.AddParameter(commandWrapper, "@TotalTime", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,totalTime);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            userCount=(System.Int32)database.GetParameterValue(commandWrapper, "@UserCount");
            totalTime=(System.Int64)database.GetParameterValue(commandWrapper, "@TotalTime");
            
            return Convert.ToBoolean(rValue);
        }
		
		#endregion		  
		
		#region  UpdateContinueday
		
		/// <summary>
        /// UpdateContinueday
        /// </summary>
		/// <param name="managerId">managerId</param>
		/// <param name="account">account</param>
		/// <param name="yesterday">yesterday</param>
		/// <param name="today">today</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:28</remarks>
        public bool UpdateContinueday ( System.Guid managerId, System.String account, System.DateTime yesterday, System.DateTime today,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("C_User_UpdateContinueday");
            
			database.AddInParameter(commandWrapper, "@ManagerId", DbType.Guid, managerId);
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, account);
			database.AddInParameter(commandWrapper, "@Yesterday", DbType.DateTime, yesterday);
			database.AddInParameter(commandWrapper, "@Today", DbType.DateTime, today);

            
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
		
		#region  GetKpiImmediate
		
		/// <summary>
        /// GetKpiImmediate
        /// </summary>
		/// <param name="curTime">curTime</param>
		/// <param name="registerUser">registerUser</param>
		/// <param name="registerManager">registerManager</param>
		/// <param name="dau">dau</param>
		/// <param name="dUniqueIp">dUniqueIp</param>
		/// <param name="dNewUser">dNewUser</param>
		/// <param name="dNewManager">dNewManager</param>
		/// <param name="payUserCount">payUserCount</param>
		/// <param name="payCount">payCount</param>
		/// <param name="payTotal">payTotal</param>
		/// <param name="paySum">paySum</param>
		/// <param name="payFirst">payFirst</param>
		/// <param name="curOnline">curOnline</param>
        /// <returns>int 影响的数据行数</returns>
        /// <remarks>2016/6/17 10:29:28</remarks>
        public bool GetKpiImmediate ( System.DateTime curTime,ref  System.Int32 registerUser,ref  System.Int32 registerManager,ref  System.Int32 dau,ref  System.Int32 dUniqueIp,ref  System.Int32 dNewUser,ref  System.Int32 dNewManager,ref  System.Int32 payUserCount,ref  System.Int32 payCount,ref  System.Int32 payTotal,ref  System.Int64 paySum,ref  System.Int32 payFirst,ref  System.Int32 curOnline,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            
            DbCommand commandWrapper = database.GetStoredProcCommand("B_Statistic_GetKpiImmediate");
            
			database.AddInParameter(commandWrapper, "@CurTime", DbType.DateTime, curTime);
			database.AddParameter(commandWrapper, "@RegisterUser", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,registerUser);
			database.AddParameter(commandWrapper, "@RegisterManager", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,registerManager);
			database.AddParameter(commandWrapper, "@Dau", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dau);
			database.AddParameter(commandWrapper, "@DUniqueIp", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dUniqueIp);
			database.AddParameter(commandWrapper, "@DNewUser", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dNewUser);
			database.AddParameter(commandWrapper, "@DNewManager", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,dNewManager);
			database.AddParameter(commandWrapper, "@PayUserCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,payUserCount);
			database.AddParameter(commandWrapper, "@PayCount", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,payCount);
			database.AddParameter(commandWrapper, "@PayTotal", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,payTotal);
			database.AddParameter(commandWrapper, "@PaySum", DbType.Int64, ParameterDirection.InputOutput,"",DataRowVersion.Current,paySum);
			database.AddParameter(commandWrapper, "@PayFirst", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,payFirst);
			database.AddParameter(commandWrapper, "@CurOnline", DbType.Int32, ParameterDirection.InputOutput,"",DataRowVersion.Current,curOnline);

            
            int rValue = 0;
            if(trans!=null)
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                rValue = (int)database.ExecuteNonQuery(commandWrapper);
            }
            registerUser=(System.Int32)database.GetParameterValue(commandWrapper, "@RegisterUser");
            registerManager=(System.Int32)database.GetParameterValue(commandWrapper, "@RegisterManager");
            dau=(System.Int32)database.GetParameterValue(commandWrapper, "@Dau");
            dUniqueIp=(System.Int32)database.GetParameterValue(commandWrapper, "@DUniqueIp");
            dNewUser=(System.Int32)database.GetParameterValue(commandWrapper, "@DNewUser");
            dNewManager=(System.Int32)database.GetParameterValue(commandWrapper, "@DNewManager");
            payUserCount=(System.Int32)database.GetParameterValue(commandWrapper, "@PayUserCount");
            payCount=(System.Int32)database.GetParameterValue(commandWrapper, "@PayCount");
            payTotal=(System.Int32)database.GetParameterValue(commandWrapper, "@PayTotal");
            paySum=(System.Int64)database.GetParameterValue(commandWrapper, "@PaySum");
            payFirst=(System.Int32)database.GetParameterValue(commandWrapper, "@PayFirst");
            curOnline=(System.Int32)database.GetParameterValue(commandWrapper, "@CurOnline");
            
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
        /// <remarks>2016/6/17 10:29:28</remarks>
        public bool Insert(NbUserEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbUser_Insert");
                        
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Source", DbType.String, entity.Source);
			database.AddInParameter(commandWrapper, "@LastLoginTime", DbType.DateTime, entity.LastLoginTime);
			database.AddInParameter(commandWrapper, "@LastLoginIp", DbType.AnsiString, entity.LastLoginIp);
			database.AddInParameter(commandWrapper, "@LastLoginDate", DbType.DateTime, entity.LastLoginDate);
			database.AddInParameter(commandWrapper, "@ContinuingLoginDay", DbType.Int32, entity.ContinuingLoginDay);
			database.AddInParameter(commandWrapper, "@RegisterIp", DbType.AnsiString, entity.RegisterIp);
			database.AddInParameter(commandWrapper, "@RegisterDate", DbType.DateTime, entity.RegisterDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);

            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }
            
            
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
        /// <remarks>2016/6/17 10:29:28</remarks>
        public bool Update(NbUserEntity entity,DbTransaction trans=null)
        {
            var database = new SqlDatabase(this.ConnectionString);
            DbCommand commandWrapper = database.GetStoredProcCommand("dbo.P_NbUser_Update");
            
			database.AddInParameter(commandWrapper, "@Account", DbType.AnsiString, entity.Account);
			database.AddInParameter(commandWrapper, "@Source", DbType.String, entity.Source);
			database.AddInParameter(commandWrapper, "@LastLoginTime", DbType.DateTime, entity.LastLoginTime);
			database.AddInParameter(commandWrapper, "@LastLoginIp", DbType.AnsiString, entity.LastLoginIp);
			database.AddInParameter(commandWrapper, "@LastLoginDate", DbType.DateTime, entity.LastLoginDate);
			database.AddInParameter(commandWrapper, "@ContinuingLoginDay", DbType.Int32, entity.ContinuingLoginDay);
			database.AddInParameter(commandWrapper, "@RegisterIp", DbType.AnsiString, entity.RegisterIp);
			database.AddInParameter(commandWrapper, "@RegisterDate", DbType.DateTime, entity.RegisterDate);
			database.AddInParameter(commandWrapper, "@Status", DbType.Int32, entity.Status);
			database.AddInParameter(commandWrapper, "@RowTime", DbType.DateTime, entity.RowTime);

            
            int results = 0;

            if(trans!=null)
            {
                results = database.ExecuteNonQuery(commandWrapper,trans);
            }
            else
            {
                results = database.ExecuteNonQuery(commandWrapper);
            }

            
            return Convert.ToBoolean(results);		
        }
		
		#endregion
        
	}
}
