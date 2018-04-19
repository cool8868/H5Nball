using System;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using Games.NBall.Entity.Enums;

namespace Games.NBall.Entity
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    [Serializable]
    public abstract class BaseResponse<T> : IResponse
    {
        /// <summary>
        /// 代码
        /// </summary>
        [DataMember]
        public int Code { get; set; }
        /// <summary>
        /// 参数名
        /// </summary>
        [DataMember]
        public string PR { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        [DataMember]
        public T Data { get; set; }

        public BaseResponse()
            :this(MessageCode.Success)
        {
            
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseResponse(MessageCode code, T t)
        {
            this.Code = (int)code;
            this.Data = t;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseResponse(int code, T t)
        {
            this.Code = code;
            this.Data = t;
        }

        /// <summary>
        /// 出错时输出消息
        /// </summary>
        /// <param name="code"></param>
        public BaseResponse(MessageCode code)
        {
            this.Code = (int)code;
        }

        /// <summary>
        /// 出错时输出消息
        /// </summary>
        /// <param name="code"></param>
        public BaseResponse(int code)
        {
            this.Code = code;
        }

        /// <summary>
        /// 成功时直接输出对象
        /// </summary>
        /// <param name="t"></param>
        public BaseResponse(T t)
            :this(MessageCode.Success)
        {
            this.Data = t;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Code:{0};Data:{1};", (int)Code, Data);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class ServiceResponse : BaseResponse<bool>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class StringResponse : BaseResponse<string>
    {

    }

    [DataContract]
    [Serializable]
    public class Int32Response : BaseResponse<int>
    { }

    [DataContract]
    [Serializable]
    public class RootResponse<T> : BaseResponse<T>
    { }
}
