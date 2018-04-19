
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
    /// TurntableManager管理类
    /// </summary>
    public static partial class TurntableManagerMgr
    {
        
		#region  GetById
		
        public static TurntableManagerEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new TurntableManagerProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<TurntableManagerEntity> GetAll(string zoneId="")
        {
            var provider = new TurntableManagerProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Guid managerId,DbTransaction trans=null,string zoneId="")
        {
            TurntableManagerProvider provider = new TurntableManagerProvider(zoneId);

            return provider.Delete( managerId,trans);
            
        }
		
		#endregion
        
		#region  AddLuckyCoin
		
        public static bool AddLuckyCoin ( System.Guid managerId, System.Int32 addLuckyCoinNumber,DbTransaction trans=null,string zoneId="")
        {
            TurntableManagerProvider provider = new TurntableManagerProvider(zoneId);

            return provider.AddLuckyCoin( managerId, addLuckyCoinNumber,trans);
            
        }
		
		#endregion
        
		#region  AddSystemProduceLuckyCoin
		
        public static bool AddSystemProduceLuckyCoin ( System.Guid managerId,ref  System.Boolean isAddSuccess,DbTransaction trans=null,string zoneId="")
        {
            TurntableManagerProvider provider = new TurntableManagerProvider(zoneId);

            return provider.AddSystemProduceLuckyCoin( managerId,ref  isAddSuccess,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(TurntableManagerEntity turntableManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TurntableManagerProvider(zoneId);
            return provider.Insert(turntableManagerEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(TurntableManagerEntity turntableManagerEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new TurntableManagerProvider(zoneId);
            return provider.Update(turntableManagerEntity,trans);
        }
		
		#endregion	
		
		
	}
}
