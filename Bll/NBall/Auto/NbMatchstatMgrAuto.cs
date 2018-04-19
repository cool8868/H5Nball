
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
    /// NbMatchstat管理类
    /// </summary>
    public static partial class NbMatchstatMgr
    {
        
		#region  GetById
		
        public static NbMatchstatEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new NbMatchstatProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  GetByManagerAndType
		
        public static NbMatchstatEntity GetByManagerAndType( System.Guid managerId, System.Int32 matchType,string zoneId="")
        {
            var provider = new NbMatchstatProvider(zoneId);
            return provider.GetByManagerAndType( managerId, matchType);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbMatchstatEntity> GetAll(string zoneId="")
        {
            var provider = new NbMatchstatProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetByManager
		
        public static List<NbMatchstatEntity> GetByManager( System.Guid managerId,string zoneId="")
        {
            var provider = new NbMatchstatProvider(zoneId);
            return provider.GetByManager( managerId);            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            NbMatchstatProvider provider = new NbMatchstatProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
		#region  Save
		
        public static bool Save ( System.Guid managerId, System.Int32 matchType, System.Int32 win, System.Int32 lose, System.Int32 draw, System.Int32 goals,DbTransaction trans=null,string zoneId="")
        {
            NbMatchstatProvider provider = new NbMatchstatProvider(zoneId);

            return provider.Save( managerId, matchType, win, lose, draw, goals,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbMatchstatEntity nbMatchstatEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbMatchstatProvider(zoneId);
            return provider.Insert(nbMatchstatEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbMatchstatEntity nbMatchstatEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbMatchstatProvider(zoneId);
            return provider.Update(nbMatchstatEntity,trans);
        }
		
		#endregion	
		
		
	}
}

