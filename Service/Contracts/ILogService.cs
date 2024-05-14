using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Base;
using Infrastructure.Model.RequestModel;
using Service.Model.Log;

namespace Service.Contracts
{
    public interface ILogService : IBaseService
    {
        /// <summary>
        /// 新增错误日志
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        Task<SystemLogModel> InsertLog(SystemLogModel arg);
        /// <summary>
        /// 获得日志列表
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        Task<PageListModel<SystemLogModel>> GetLogList(QueryPageListModel arg);
    }
}
