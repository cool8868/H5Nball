
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
    /// Friendinvite管理类
    /// </summary>
    public static partial class FriendinviteMgr
    {
        
		#region  GetById
		
        public static FriendinviteEntity GetById( System.String byAccount,string zoneId="")
        {
            var provider = new FriendinviteProvider(zoneId);
            return provider.GetById( byAccount);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<FriendinviteEntity> GetAll(string zoneId="")
        {
            var provider = new FriendinviteProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  GetAllNumber
		
		public static Int32 GetAllNumber (string zoneId="")
        {
            var provider = new FriendinviteProvider(zoneId);
            return provider.GetAllNumber();
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.String byAccount,DbTransaction trans=null,string zoneId="")
        {
            FriendinviteProvider provider = new FriendinviteProvider(zoneId);

            return provider.Delete( byAccount,trans);
            
        }
		
		#endregion
        
		#region  SavePrize
		
        public static bool SavePrize ( System.String account,DbTransaction trans=null,string zoneId="")
        {
            FriendinviteProvider provider = new FriendinviteProvider(zoneId);

            return provider.SavePrize( account,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(FriendinviteEntity friendinviteEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendinviteProvider(zoneId);
            return provider.Insert(friendinviteEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(FriendinviteEntity friendinviteEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new FriendinviteProvider(zoneId);
            return provider.Update(friendinviteEntity,trans);
        }
		
		#endregion	
		
		
	}
}
