using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Games.NBall.Bll;
using Games.NBall.Bll.Share;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Games.NBall.NB_Web.Helper
{
    public class OutputHelper
    {
        private static string _exceptionJson = "{\"Code\":-1}";
        private static string _parameterJson = "{\"Code\":1}";

        /// <summary>
        /// Outputs the return message.
        /// </summary>
        /// <param name="code">The code.</param>
        public static void Output(int code)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl); 
            HttpContext.Current.Response.Write("{\"Code\":" + code + "}");
        }

        public static void Output(int code,string pr)
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);  
            HttpContext.Current.Response.Write("{\"Code\":" + code + ",\"PR\":\"" + pr + "\"}");
        }

        public static void OutputMsg(string msg)
        {
            if (msg == null)
                msg = string.Empty;
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);
            HttpContext.Current.Response.Write("{\"Code\":0,\"Msg\":\"" + msg + "\"}");
        }

        public static void Output(MessageCode code)
        {
            Output((int)code);
        }

        /// <summary>
        /// Outputs the specified output entity.
        /// </summary>
        /// <param name="outputEntity">The output entity.</param>
        public static void Output(IResponse outputEntity)
        {
            if (outputEntity == null)
            {
                OutputException();
                return;
            }
            if(outputEntity.Code!= (int)MessageCode.Success)
            {
                if (outputEntity.Code == (int) MessageCode.NbParameterError
                    ||outputEntity.Code == (int)MessageCode.Exception 
                    ||outputEntity.Code == (int)MessageCode.SystemBusy )
                {
                    Output(outputEntity.Code,outputEntity.PR);
                }
                else 
                {
                    Output(outputEntity.Code);
                }
                
            }
            else
            {
                //HttpContext.Current.Response.Write(GetJson(outputEntity));
                var s = GetJson(outputEntity);
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);  
                HttpContext.Current.Response.Write(s);
                s = string.Empty;
            }
            outputEntity = null;
        }

        /// <summary>
        /// Outputs the specified output entity.
        /// </summary>
        /// <param name="outputEntity">The output entity.</param>
        public static void Output<T>(MessageCode code, T data)
        {
            if (code != MessageCode.Success)
            {
                Output(code);
            }
            else
            {

                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);
                HttpContext.Current.Response.Write(GetJson(data));
            }
        }

        /// <summary>
        /// 输出无效请求json
        /// </summary>
        public static void OutputBadRequest()
        {
            Output(MessageCode.BadRequest);
        }

        /// <summary>
        /// 输出系统繁忙json
        /// </summary>
        public static void OutputException()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);
            HttpContext.Current.Response.Write(_exceptionJson);
        }

        public static void OutputParameterError()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", ShareUtil.DomainUrl);
            HttpContext.Current.Response.Write(_parameterJson);
        }

        /// <summary>
        /// Gets the json.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string GetJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
                //var dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyyMMdd HH:mm:ss" };
                //return JsonConvert.SerializeObject(obj, dtConverter);
            }
            catch (Exception ex)
            {
                SystemlogMgr.Error("GetJson", ex);
                return _exceptionJson;
            }
        }
    }
}