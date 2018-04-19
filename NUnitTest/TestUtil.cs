using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;
using Newtonsoft.Json;

namespace Games.NBall.NUnitTest
{
    public class TestUtil
    {

        public static void WriteObj(object obj)
        {
            Console.WriteLine(GetJson(obj));
        }

        /// <summary>
        /// Gets the json.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        static string GetJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetJson", ex);
                return ex.Message;
            }
        }

    }
}
