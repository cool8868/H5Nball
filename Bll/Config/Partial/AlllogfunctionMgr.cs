
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Games.NBall.Entity;
using Games.NBall.Dal;

namespace Games.NBall.Bll
{
    
    public partial class AllLogfunctionMgr
    {
        public static List<AllLogfunctionEntity> GetAllForFactory()
        {
            var provider = new AllLogfunctionProvider();
            return provider.GetAllForFactory();
        }

        public static AllLogfunctionEntity AddNewForFactory(System.String name, System.DateTime rowTime)
        {
            var provider = new AllLogfunctionProvider();
            return provider.AddNewForFactory(name, rowTime);
        }
	}
}

