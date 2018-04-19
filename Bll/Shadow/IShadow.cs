using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Dal.Shadow;

namespace Games.NBall.Bll.Shadow
{
    public interface IShadow
    {
        void Save(ShadowProvider provider);
    }
}
