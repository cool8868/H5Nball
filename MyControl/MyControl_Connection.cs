using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace Games.MyControl
{
    public class ConnectionFactory
    {
        #region Cache
        private static Dictionary<string, string> _connectionDic = new Dictionary<string, string>();
        private static string _currentConnectString = "";// 当前区游戏主库连接
        private static List<StatusList> _zoneList;
        private static List<StatusList> _allZoneList;
        /// <summary>
        /// zonename->zoneId
        /// </summary>
        private static Dictionary<string, int> _zoneIdDic;

        private string _userName="";
        //private string _zoneHtml = "";
        private bool _isAdmin = false;
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
                            _instance = new ConnectionFactory();
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
            InitCache("");
        }
        #endregion

        #region Facade
        public List<StatusList> GetZoneList(string userName,bool selectAll=false)
        {
            if(_userName!=userName)
                InitCache(userName);
            if (selectAll && _isAdmin)
            {
                return _allZoneList;
            }
            return _zoneList; 
        }
        public string GetZoneHtml(string selectZone)
        {
            return GetZoneSelectHtml("SZone", _zoneList, selectZone);
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="zoneName">Name of the zone.</param>
        /// <param name="strDbType">Type of the db.</param>
        /// <returns></returns>
        public string GetConnectionString(string zoneName, string strDbType)
        {
            if (string.IsNullOrEmpty(zoneName))
            {
                MyControl_MessageBox.Msg("区名不能为空");
                return "";
            }
            string key = BuildKey(zoneName, strDbType);
            if (_connectionDic.ContainsKey(key))
                return _connectionDic[key];
            else
            {
                //如果没有对应区的连接就获取默认区
                key = BuildKey("Share", strDbType);
                if (_connectionDic.ContainsKey(key))
                    return _connectionDic[key];
                return string.Empty;
            }
        }

        public int GetZoneId(string zoneName)
        {
            zoneName = zoneName.ToLower();
            if (_zoneIdDic.ContainsKey(zoneName))
            {
                return _zoneIdDic[zoneName];
            }
            return 0;
        }

        #endregion

        #region Tools

        private string GetZoneSelectHtml(string zoneFieldName,List<StatusList> slt,string selectZone)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<td colspan=3 class=\"bgWhite\" width=\"50%\">");
            builder.AppendLine("<select id=\"" + zoneFieldName + "\" name=\"" + zoneFieldName + "\">");

            for (int i=0;i<slt.Count;i++)
            {
                if (!string.IsNullOrEmpty(selectZone))
                {
                    if (slt[i].Value == selectZone)
                    {
                        builder.AppendLine("<option value=\"" + slt[i].Value + "\" selected=\"selected\">" + slt[i].Text + "</option>");
                    }
                    else
                    {
                        builder.AppendLine("<option value=\"" + slt[i].Value + "\">" + slt[i].Text + "</option>");
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        builder.AppendLine("<option value=\"" + slt[i].Value + "\" selected=\"selected\">" + slt[i].Text + "</option>");
                    }
                    else
                    {
                        builder.AppendLine("<option value=\"" + slt[i].Value + "\">" + slt[i].Text + "</option>");
                    }
                }
            }
            builder.AppendLine("</select>");
            return builder.ToString();
        }

        private void InitCache(string userName)
        {
            _userName = userName;
            var connstring = ConfigurationManager.ConnectionStrings["Games.NBall.Dal.Properties.Settings.NB_ConfigConnectionString"].ConnectionString;
            if (string.IsNullOrEmpty(connstring))
            {
                MyControl_MessageBox.Msg("未找到名称为[NB_ConfigConnectionString]的连接串");
                return;
            }
            _zoneList = new List<StatusList>();
            var zoneDataSet = MyControl_SqlHelper.ExecuteDataset(connstring, CommandType.Text, "Select * From All_Zoneinfo");
            if (zoneDataSet == null || zoneDataSet.Tables.Count <= 0)
            {
                MyControl_MessageBox.Msg("获取区服配置失败");
                return;
            }

            string userPlatforms = "";
            _isAdmin = true;
            if (!string.IsNullOrEmpty(userName))
            {
                var plat =MyControl_SqlHelper.ExecuteScalar(connstring, CommandType.Text,
                                                  "Select PlatformString From AUTH_UserPlatform Where UserName='" +
                                                  userName+"'");
                if (plat != null)
                {
                    userPlatforms = plat.ToString();
                    _isAdmin = false;
                }
            }
            _zoneIdDic = new Dictionary<string, int>();
            foreach (DataRow row in zoneDataSet.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(userPlatforms))
                {
                    var code = row["PlatformCode"].ToString();
                    if(!userPlatforms.Contains(code))
                       continue;
                }
                _zoneIdDic.Add(row["ZoneName"].ToString().ToLower(), Convert.ToInt32(row["Idx"]));
                _zoneList.Add(new StatusList(row["ZoneName"].ToString(), row["Name"].ToString()));
            }
            //_zoneHtml = GenFieldWhereSelectHtml("SZone",_zoneList);
            _connectionDic=new Dictionary<string, string>();
            var dataset = MyControl_SqlHelper.ExecuteDataset(connstring, CommandType.Text, "Select * From All_Database");
            if (dataset == null || dataset.Tables.Count<=0)
            {
                MyControl_MessageBox.Msg("获取数据库配置失败");
                return;
            }

            foreach (DataRow row in dataset.Tables[0].Rows)
            {
                try
                {
                    string key = BuildKey(row["ZoneName"].ToString(), row["DBType"].ToString());
                    string connectString = BuildConnectString(row);
                    _connectionDic.Add(key, connectString);
                }
                catch (Exception ex)
                {
                    
                    throw;
                }
                
            }
            _allZoneList = new List<StatusList>();
            _allZoneList.Add(new StatusList("0","所有区"));
            foreach (var entity in _zoneList)
            {
                _allZoneList.Add(new StatusList(entity.Value,entity.Text));
            }
        }

        private string BuildKey(string zoneName, string dbType)
        {
            return zoneName + "_" + dbType;
        }

        private string BuildConnectString(DataRow row)
        {
            return
                string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                              row["DBServerName"], row["DBName"], row["UserId"], row["Password"]);

        }


        #endregion
    }
}
