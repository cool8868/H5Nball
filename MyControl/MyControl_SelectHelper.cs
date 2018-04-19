using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.MyControl
{
    public class MyControl_SelectHelper
    {



        private void CheckWhereParamValue(List<WhereParam> whereParams )
        {
            if ((whereParams != null) && (whereParams.Count > 0))
            {
                foreach (WhereParam param in whereParams)
                {
                    if ((param.FieldValue.ToString().Contains("<") || param.FieldValue.ToString().Contains(">")) ||
                        ((param.Operator != AdminCompare.Like) && param.FieldValue.ToString().Contains("'")))
                    {
                        throw new ArgumentException("值" + param.FieldValue + "包含有特殊符号", param.FieldName);
                    }
                }
            }
        }
    }
}
