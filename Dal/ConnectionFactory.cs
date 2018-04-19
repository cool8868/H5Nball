using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Dal
{
    public class ConnectionFactory
    {
        #region Cache
        private static Dictionary<string, string> _connectionDic = new Dictionary<string, string>();
        private static string _currentConnectString = "";// 当前区游戏主库连接
        private static readonly string CurrentZoneName = System.Configuration.ConfigurationManager.AppSettings["ZoneName"];

        #endregion

        #region Singleton
        private static ConnectionFactory _instance;
        private static object _lockObj = new object();
        public static ConnectionFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            var temp = new ConnectionFactory();
                            _instance = temp;
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region .ctor
        public ConnectionFactory()
        {
            InitCache();
        }
        #endregion

        #region Facade
        /// <summary>
        /// 返回当前区游戏主库连接
        /// </summary>
        /// <returns></returns>
        public string GetDefault()
        {
            return _currentConnectString;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="dbType">Type of the db.</param>
        /// <returns></returns>
        public string GetConnectionString(EnumDbType dbType)
        {
            return GetConnectionString(CurrentZoneName, dbType);
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="zoneName">Name of the zone.</param>
        /// <param name="dbType">Type of the db.</param>
        /// <returns></returns>
        public string GetConnectionString(string zoneName, EnumDbType dbType)
        {
            if (string.IsNullOrEmpty(zoneName))
                zoneName = CurrentZoneName;
            if (string.IsNullOrEmpty(zoneName))
                zoneName = "Share";
            string strDbType = ConvertHelper.ConvertEnumToString(dbType);
            string key = BuildKey(zoneName, strDbType);
            if (_connectionDic.ContainsKey(key))
                return _connectionDic[key];
            else
            {
                //如果没有对应区的连接就获取默认区
                key = BuildKey("Share", strDbType);
                if (_connectionDic.ContainsKey(key))
                    return _connectionDic[key];
                else
                {
                    //LogHelper.Insert("Can't find connectionstring,zonename:" + zoneName + ",db:" + dbType.ToString(), LogType.Error);
                   return string.Empty;
                }
            }
        }
        #endregion

        #region Tools
        private void InitCache()
        {
            var provider = new AllDatabaseProvider(0,0);
            var list = provider.GetAllForFactory();
            foreach (var entity in list)
            {
                string key = BuildKey(entity);
                string connectString = BuildConnectString(entity);
                if (_connectionDic.ContainsKey(key))
                {
                    int a = 0;
                    string s = a.ToString();

                }
                else
                {
                    _connectionDic.Add(key, connectString);
                }
                
            }

            _currentConnectString = GetConnectionString(CurrentZoneName, EnumDbType.Main);
        }

        private string BuildKey(AllDatabaseEntity entity)
        {
            return BuildKey(entity.ZoneName, entity.DBType);
        }

        private string BuildKey(string zoneName, string dbType)
        {
            return zoneName.ToLower() + "_" + dbType;
        }

        private string BuildConnectString(AllDatabaseEntity entity)
        {
            return
                string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                              entity.DBServerName, entity.DBName, entity.UserId, entity.Password);

        }

        #endregion
    }
}
