using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Extensions;
using Infrastructure.Helpers;
using Infrastructure.Model.RequestModel;
using Mapster;
using Repository.Entities.Log;
using Repository.Entities.Prize;
using Repository.Entities.Product.Commodity;
using Service.Contracts;
using Service.Model.Log;

namespace Service.Service
{
    public class LogService : ILogService
    {
        private readonly IFreeSql _freeSql;
        public LogService(IFreeSql freeSql)
        {
            _freeSql = freeSql;
        }
        public async Task<SystemLogModel> InsertLog(SystemLogModel arg)
        {
            var log = _freeSql.GetRepository<SystemLog>();
            var model = arg.Adapt<SystemLog>();
            var result = await log.InsertAsync(model);
            return result.Adapt<SystemLogModel>();
        }

        public async Task<PageListModel<SystemLogModel>> GetLogList(QueryPageListModel arg)
        {
            var result = await _freeSql.GetPagedResultAsync<SystemLog>(arg); 
            return result.Adapt<PageListModel<SystemLogModel>>(); ;
        }
    }
}
