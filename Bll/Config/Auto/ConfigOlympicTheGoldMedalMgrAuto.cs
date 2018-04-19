
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    /// <summary>
    /// ConfigOlympicthegoldmedal管理类
    /// </summary>
    public static partial class ConfigOlympicthegoldmedalMgr
    {
        
		#region  GetById
		
        public static ConfigOlympicthegoldmedalEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new ConfigOlympicthegoldmedalProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<ConfigOlympicthegoldmedalEntity> GetAll(string zoneId="")
        {
            var provider = new ConfigOlympicthegoldmedalProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            ConfigOlympicthegoldmedalProvider provider = new ConfigOlympicthegoldmedalProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(ConfigOlympicthegoldmedalEntity configOlympicthegoldmedalEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigOlympicthegoldmedalProvider(zoneId);
            return provider.Insert(configOlympicthegoldmedalEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(ConfigOlympicthegoldmedalEntity configOlympicthegoldmedalEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new ConfigOlympicthegoldmedalProvider(zoneId);
            return provider.Update(configOlympicthegoldmedalEntity,trans);
        }
		
		#endregion	
		
		
	}
}
