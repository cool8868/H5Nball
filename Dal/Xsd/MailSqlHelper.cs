using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Games.NBall.Dal.Xsd
{
    public class MailSqlHelper
    {

        public static bool SaveMailBulk(MailinfoDataSet.Mail_InfoDataTable mailInfoData)
        {
            if(mailInfoData==null)
                return false;
            SqlBatchHelper.BulkInsert(ConnectionFactory.Instance.GetDefault(), mailInfoData);
            return true;
        }

        public static bool SaveMailBulk(MailinfoDataSet.Mail_InfoDataTable mailInfoData,string connectionstring)
        {
            if (mailInfoData == null)
                return false;
            SqlBatchHelper.BulkInsert(connectionstring, mailInfoData);
            return true;
        }

        public static bool SaveMailBulk(MailinfoDataSet.Mail_InfoDataTable mailInfoData,SqlTransaction trans)
        {
            if (mailInfoData == null)
                return false;
            SqlBatchHelper.BulkInsert(trans, mailInfoData);
            return true;
        }
    }
}
