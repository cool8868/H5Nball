
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
    /// AllLogfunction管理类
    /// </summary>
    public static partial class AllLogfunctionMgr
    {
        
		#region  GetById
		
        public static AllLogfunctionEntity GetById( System.Int32 idx,string zoneId="")
        {
            var provider = new AllLogfunctionProvider(zoneId);
            return provider.GetById( idx);
        }
		
		#endregion		  
		
		#region  AddNew
		
        public static AllLogfunctionEntity AddNew( System.String name, System.DateTime rowTime,string zoneId="")
        {
            var provider = new AllLogfunctionProvider(zoneId);
            return provider.AddNew( name, rowTime);
        }
		
		#endregion		  
		
		#region  GetAll
		
        public static List<AllLogfunctionEntity> GetAll(string zoneId="")
        {
            var provider = new AllLogfunctionProvider(zoneId);
            return provider.GetAll();            
        }
		
		#endregion		  
		
		#region  Delete
		
        public static bool Delete ( System.Int32 idx,DbTransaction trans=null,string zoneId="")
        {
            AllLogfunctionProvider provider = new AllLogfunctionProvider(zoneId);

            return provider.Delete( idx,trans);
            
        }
		
		#endregion
        
        
		#region Insert

        public static bool Insert(AllLogfunctionEntity allLogfunctionEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllLogfunctionProvider(zoneId);
            return provider.Insert(allLogfunctionEntity,trans);
        }
		
		#endregion
		
		#region Update

        public static bool Update(AllLogfunctionEntity allLogfunctionEntity,DbTransaction trans=null,string zoneId="")
        {
            var provider = new AllLogfunctionProvider(zoneId);
            return provider.Update(allLogfunctionEntity,trans);
        }
		
		#endregion	
		
		
	}
}

