using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Games.NBall.Bll.Share;
using Games.NBall.Entity.Enums;
using Newtonsoft.Json;
using Games.NBall.WebClient.Data;
using Games.NBall.WebClient.Weibo.Data;
using Games.NBall.WebClient.Util;
using Games.NBall.WebClient.Plat;

namespace Games.NBall.WebClient.Weibo
{
    public static class WyxApi
    {
        #region 登录
        public static string Login(out WyxSessionInfo loginInfo)
        {
            loginInfo = WyxSessionProvider.Instance.LoginSession("") as WyxSessionInfo;
            return loginInfo.ErrorCode ?? WebErrorCode.MissWyxSession;
        }
        #endregion

        #region 充值
        public static string Charge(out WyxPayBackInfo payInfo)
        {
            string secret = WyxCache.AppCfg.AppSecret;
            payInfo = new WyxPayBackInfo();
            payInfo.FromCollection(HttpContext.Current.Request.QueryString, false);
            payInfo.FromCollection(HttpContext.Current.Request.Form, false);
            string appKey = payInfo.AppKey;
            if (string.IsNullOrEmpty(appKey))
                appKey = WyxCache.AppCfg.AppKey;
            if (!payInfo.ValidateValue())
                return WebErrorCode.RequiredArgs;
            //MD5($order_id.'|'.$appkey.'|'.$srvkey.'|'.$order_uid.'|'.$amount.'|'.$appSecret)
            var baseStr = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", payInfo.OrderId, appKey, payInfo.ServerId,
                payInfo.Uid, payInfo.Amount, secret);
            bool matchFlag = string.Compare(payInfo.Sign, CryptoUtil.GetMD5(baseStr, "x2"), true) == 0;
            return matchFlag ? WebErrorCode.OK : WebErrorCode.ErrorSignature;
        }
        #endregion

        #region 用户信息
        public static string UserShow(string uid, bool castFlag = false)
        {
            string url =  castFlag ? string.Format(WyxCache.AppCfg.OpenApiUrl, DefineWyxUri.CastUserShow) :DefineWyxUri.User_show;
            var client = new WyxClient(url);
            client.AddQueryArgs(DefineWyxArgs.COLUid, uid);
            return client.WebGet();
        }
        public static string WanwanUserShow(string uid, bool castFlag = false)
        {
            string url =  castFlag ? string.Format(WyxCache.AppCfg.OpenApiUrl, DefineWyxUri.CastUserShow) :DefineWyxUri.User_show;
            var client = new WanwanClient(url);
            client.AddQueryArgs(DefineWyxArgs.COLUid, uid);
            return client.WebGet();
        }
        //public static dynamic DynamicUserShow(string uid, bool castFlag = false)
        //{
        //    string url = castFlag ? string.Format(WyxCache.AppCfg.OpenApiUrl, DefineWyxUri.CastUserShow) : DefineWyxUri.User_show;
        //    var client = new WyxClient(url);
        //    client.AddQueryArgs(DefineWyxArgs.COLUid, uid);
        //    return client.WebGetJson();
        //}
        public static string GetUserName(string uid, bool castFlag = false)
        {
            string text = UserShow(uid, castFlag);
            if (!JsonUtil.MaybeJson(text))
                return string.Empty;
            var json = JsonConvert.DeserializeObject(text);
            return JsonUtil.GetJsonValue(json, "screen_name");
        }
        public static WyxUserInfo GetWanwanUserInfo(string uid, bool castFlag = true)
        {
            var user = new WyxUserInfo();
            user.Uid = uid;
            string text = WanwanUserShow(uid, castFlag);
            if (!JsonUtil.MaybeJson(text))
                return user;
            var json = JsonConvert.DeserializeObject(text);
            user.Uname = JsonUtil.GetJsonValue(json, "screen_name");
            user.Logo = JsonUtil.GetJsonValue(json, "avatar_large");
            return user;
 
        }
        public static WyxUserInfo GetUserInfo(string uid, bool castFlag = true)
        {
            var user = new WyxUserInfo();
            user.Uid = uid;
            string text = UserShow(uid, castFlag);
            if (!JsonUtil.MaybeJson(text))
                return user;
            var json = JsonConvert.DeserializeObject(text);
            user.Uname = JsonUtil.GetJsonValue(json, "screen_name");
            user.Logo = JsonUtil.GetJsonValue(json, "avatar_large");
            return user;
        }
        #endregion

        #region 上传角色
        public static string RoleUpdate(string serverId, string roleName, string uid = "")
        {
            var client = new WyxClient(DefineWyxUri.Ingame_RoleUpdate);
            client.AddFormArgs(DefineWyxArgs.COLServerId, serverId);
            client.AddFormArgs(DefineWyxArgs.COLRoleName, roleName);
            if (!string.IsNullOrEmpty(uid))
                client.AddFormArgs(DefineWyxArgs.COLUid, uid);
            return client.WebPost();
        }
        public static string CastRoleUpdate(string serverId, string roleName, string uid = "")
        {
            string url = string.Format(WyxCache.AppCfg.OpenApiUrl, DefineWyxUri.CastRoleUpdate);
            var client = new WyxClient(url);
            client.AddQueryArgs(DefineWyxArgs.COLServerId, serverId);
            client.AddQueryArgs(DefineWyxArgs.COLRoleName, roleName);
            if (!string.IsNullOrEmpty(uid))
                client.AddQueryArgs(DefineWyxArgs.COLUid, uid);
            return client.WebGet();
        }
        #endregion

        #region 订单状态
        public static string PayOrderStatus(string orderId, string userId)
        {
            var client = new WyxClient(DefineWyxUri.Pay_order_status);
            client.AddQueryArgs(DefineWyxArgs.COLOrderId, orderId);
            client.AddQueryArgs(DefineWyxArgs.COLUserId, userId);
            client.NoSesssionKey = true;
            return client.WebGet();
        }
        #endregion

    }
}
