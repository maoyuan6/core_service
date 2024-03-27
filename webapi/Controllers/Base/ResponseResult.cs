namespace Webapi.Controllers.Base
{
    public class ResponseResult<TResponse>
    {
        /// <summary>
        /// 请求是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 响应代码 200 为成功， 非200为失败，Message为失败原因
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回实体
        /// </summary>
        public TResponse Data { get; set; }
    }
}
