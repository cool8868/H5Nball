using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Games.MyControl;
using Games.NBall.AdminWeb.AdminEntity;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Core.Item;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Enums.Common;
using Games.NBall.Entity.Enums.Shadow;
using Games.NBall.Entity.NBall.Custom;
using Games.NBall.ServiceContract.Client;

namespace Games.NBall.AdminWeb
{
    public class AdminMgr
    {
        public static readonly string TimeFormat = "yyyy-MM-dd HH:mm:ss";

        public static void BindZoneControl(HttpContext context, DropDownList ddlZone, string userName,
            bool selectAll = false)
        {
            var zoneList = ConnectionFactory.Instance.GetZoneList(userName, selectAll);
            ddlZone.DataSource = zoneList;
            ddlZone.DataTextField = "Text";
            ddlZone.DataValueField = "Value";
            ddlZone.DataBind();
            var zoneCache = SelectControl.GetSelectZoneFromCookie(context);
            var index = 0;
            if (!string.IsNullOrEmpty(zoneCache))
            {
                index = zoneList.FindIndex(d => d.Value == zoneCache);
            }
            ddlZone.SelectedIndex = index;
        }

        public static int GetSelectZoneIdInt(HttpContext context, DropDownList ddlZone)
        {
            var zone = GetSelectZoneId(context, ddlZone);
            return ConnectionFactory.Instance.GetZoneId(zone);
        }

        public static int GetSelectInt(DropDownList control)
        {
            var zone = control.SelectedValue;
            return ConvertHelper.ConvertToInt(zone);
        }

        public static string GetSelectZoneId(HttpContext context, DropDownList ddlZone)
        {
            var zone = ddlZone.SelectedValue;
            SelectControl.SetSelectZoneToCookie(context, zone);
            return zone;
        }

        public static void BindDdlControl(DropDownList control, string enumName)
        {
            control.DataSource = CacheDataHelper.Instance.GetEnumData(enumName);
            control.DataTextField = "Text";
            control.DataValueField = "Value";
            control.DataBind();
            control.SelectedIndex = 0;
        }

        #region GetEnumName

        public static string GetEnumName(string enumObj, int value)
        {
            return CacheDataHelper.Instance.GetEnumDescription(enumObj, value);
        }

        public static string GetEnumName(string enumObj, string value)
        {
            return CacheDataHelper.Instance.GetEnumDescription(enumObj, value);
        }

        #endregion

        #region BuildPropertyPlus

        public static string BuildPropertyPlus(PropertyPlusEntity plusEntity)
        {
            string pName = GetEnumName("EnumProperty", plusEntity.PropertyId);
            if (plusEntity.PlusType == (int) EnumPlusType.Abs)
            {
                return string.Format("{0} +{1}", pName, plusEntity.PlusValue);
            }
            return string.Format("{0} +{1}%", pName, plusEntity.PlusValue);
        }

        #endregion

        /// <summary>
        /// 将时间转成unix时间戳.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <returns></returns>
        public static double DateTime2UnixTimeStamp(DateTime time)
        {
            double result = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            result = (time - startTime).TotalSeconds;
            return result;
        }

        //获取输入的SHA1哈希值，需要MD5的，请自行修改
        public static string GetSHA1(string input)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] inputData = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] outputData = sha1.ComputeHash(inputData);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < outputData.Length; i++)
            {
                sb.Append(outputData[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the M d5，默认返回小写
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string GetMD5(string input)
        {
            return GetMD5(input, "x2");
        }

        /// <summary>
        /// Gets the M d5.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="outFormat">输出格式，X2|x2.</param>
        /// <returns></returns>
        public static string GetMD5(string input, string outFormat)
        {
            MD5CryptoServiceProvider sha1 = new MD5CryptoServiceProvider();
            byte[] inputData = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] outputData = sha1.ComputeHash(inputData);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < outputData.Length; i++)
            {
                sb.Append(outputData[i].ToString(outFormat));
            }
            return sb.ToString();
        }

        public static void SaveAdminLog(string adminName, string ip, EnumAdminOperationType operationType,
            string targetZoneId, string targetuser, string managerName, Guid managerId, string memo)
        {
            GmLogEntity entity = new GmLogEntity();
            entity.AdminName = adminName;
            entity.Ip = ip;
            entity.OperationType = (int) operationType;
            entity.ManagerId = managerId;
            entity.ManagerName = managerName;
            entity.Memo = memo;
            entity.TargetZoneId = targetZoneId;
            entity.TargetUser = targetuser;
            entity.TargetUserList = "";
            entity.RowTime = DateTime.Now;
            Bll.GmLogMgr.Insert(entity);
        }

        public static MessageCode AddItems(string zoneId, Guid managerId, int itemCode, int itemCount, int strength,
            bool isBinding,bool isDeal, int slotColorCount = 0)
        {
            try
            {
                var package = ItemCore.Instance.GetPackage(managerId, EnumTransactionType.AdminAddItem, zoneId);
                if (package == null)
                    return MessageCode.NbParameterError;
                var result = package.AddItems(itemCode, itemCount, strength, isBinding,isDeal, slotColorCount);
                if (result == MessageCode.Success)
                {
                    bool isSuccess = package.Save();
                    if (isSuccess)
                    {
                        package.Shadow.Save();
                    }
                    return MessageCode.Success;
                }
                return result;
                return MessageCode.Exception;

            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("Admin AddItems", ex);
                return MessageCode.Exception;
            }
        }

        private static readonly ItemClient reader = new ItemClient();
        private static readonly OnlineClient reader2=new OnlineClient();

        public static MessageCode GMAddItem(string zoneId, Guid managerId, int itemCode, int itemCount, int strength,
            bool isBinding, int slotColorCount = 0)
        {
            return reader.GMAddItem(zoneId, managerId, itemCode, itemCount, strength, isBinding, slotColorCount);
        }
        public static MessageCode Charge(string account, int sourceType, int cash, int point, int bonus, string orderId, string eqid = "")
        {
            return reader.Charge(account, sourceType, cash, point, bonus, orderId, eqid);
        }

        public static MessageCode AddCoin(Guid managerId, int coin)
        {
            return reader.AddCoin(managerId, coin);
        }
        public static bool KickSession(Guid managerId)
        {
            return reader2.KickSession(managerId);

        }
        public static bool LockUserUnexpect(Guid managerId, string GMName, string memo)
        {

            return reader2.LockUserUnexpect(managerId, GMName, memo);

        }
        public static bool BreakLock(Guid managerId, string GMName, string memo, string zoneId)
        {
            return reader2.BreakLock(managerId, GMName, memo, zoneId);

        }
    }
}