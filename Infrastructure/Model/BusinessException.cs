using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    /// <summary>
    /// 业务异常模型，此类异常不记录日志
    /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message = "服务器内部错误", int hResult = 600)
            : base(message)
        {
            this.HResult = hResult;
        }
    }
}
