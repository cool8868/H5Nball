using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.NBall.Custom
{
    public class A8csdkUserActionEntity
    {
        public A8csdkUserActionEntity ()
        {

        }

        public A8csdkUserActionEntity(string dataType, string sessionId, string gameNum, string channelAlias, string channelId, string deviceId, string model
            , string release,string uid, string uname, string serverId, string serverName, string roleId, string roleName, string roleLevel,
             string ext, string sdkVersion)
        {
            this.DataType = dataType;
            this.SessionId = sessionId;
            this.GameNum = gameNum;
            this.ChannelAlias = channelAlias;
            this.ChannelId = channelId;
            this.DeviceId = deviceId;
            this.Model = model;
            this.Release = release;
            this.Uid = uid;
            this.Uname = uname;
            this.ServerId = serverId;
            this.ServerName = serverName;
            this.RoleId = roleId;
            this.RoleName = roleName;
            this.RoleLevel = roleLevel;
            this.Ext = ext;
            this.SdkVersion = sdkVersion;
        }
        [DataMember]
        public string DataType { get; set; }
        [DataMember]
        public string SessionId{ get; set; }
        
         [DataMember]
        public string GameNum { get; set; }
         [DataMember]
         public string ChannelAlias{ get; set; }
         [DataMember]
         public string ChannelId { get; set; } 
          [DataMember]
         public string DeviceId { get; set; } 
          [DataMember]
          public string Model { get; set; } 
          [DataMember]
          public string Release { get; set; } 
          [DataMember]
          public string Uid { get; set; } 
          [DataMember]
          public string Uname { get; set; } 
          [DataMember]
          public string ServerId { get; set; } 
          [DataMember]
          public string ServerName { get; set; }
          [DataMember]
          public string RoleId { get; set; }
          [DataMember]
          public string RoleName { get; set; }
          [DataMember]
          public string RoleLevel { get; set; }
          [DataMember]
          public string Ext { get; set; }
          [DataMember]
          public string SdkVersion { get; set; }

    }
}
