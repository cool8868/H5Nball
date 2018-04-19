
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
    /// NbManagerskillbag管理类
    /// </summary>
    public static partial class NbManagerskillbagMgr
    {
        
		#region  GetById
		
        public static NbManagerskillbagEntity GetById( System.Guid managerId,string zoneId="")
        {
            var provider = new NbManagerskillbagProvider(zoneId);
            return provider.GetById( managerId);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<NbManagerskillbagEntity> GetAll(string zoneId="")
        {
            var provider = new NbManagerskillbagProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Add
		
        public static bool Add ( System.Guid managerId, System.String onItemMap, System.Byte[] bagRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerskillbagProvider provider = new NbManagerskillbagProvider(zoneId);

            return provider.Add( managerId, onItemMap, bagRowVersion,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  Set
		
        public static bool Set ( System.Guid managerId, System.String setSkills, System.Byte[] formRowVersion, System.String bagSetMap, System.Byte[] bagRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerskillbagProvider provider = new NbManagerskillbagProvider(zoneId);

            return provider.Set( managerId, setSkills, formRowVersion, bagSetMap, bagRowVersion,ref  errorCode,trans);
            
        }
		
		#endregion
        
		#region  MixUpTran
		
        public static bool MixUpTran ( System.Boolean tranFlag, System.Guid managerId, System.String setSkills, System.String setMap, System.String itemMap, System.Byte[] bagRowVersion,ref  System.Int32 errorCode,DbTransaction trans=null,string zoneId="")
        {
            NbManagerskillbagProvider provider = new NbManagerskillbagProvider(zoneId);

            return provider.MixUpTran( tranFlag, managerId, setSkills, setMap, itemMap, bagRowVersion,ref  errorCode,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(NbManagerskillbagEntity nbManagerskillbagEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerskillbagProvider(zoneId);
            return provider.Insert(nbManagerskillbagEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(NbManagerskillbagEntity nbManagerskillbagEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new NbManagerskillbagProvider(zoneId);
            return provider.Update(nbManagerskillbagEntity,trans);
        }
		
		#endregion	
		
		
	}
}

