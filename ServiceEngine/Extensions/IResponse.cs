using Games.NBall.Common;
namespace Games.NBall.ServiceEngine.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// 代码
        /// </summary>
        ResponseCode Code { get; set; }
    }

    public enum ResponseCode
    {
        Exception = -1
    }
}
