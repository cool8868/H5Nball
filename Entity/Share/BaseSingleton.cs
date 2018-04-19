using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NBall.Entity.Share
{
    /// <summary>
    /// 所有声明为单例的类，
    /// 需提供一个参数为int的构造函数，
    /// 不提供无参构造函数，以防被误实例化
    /// </summary>
    public class BaseSingleton:ISingleton
    {
        public BaseSingleton(int p)
        {
            if(p!=257)
                throw new Exception("Singleton Constructor parameter invalid.");
        }
    }
}
