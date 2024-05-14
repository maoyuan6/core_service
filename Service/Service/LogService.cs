using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            arg.QueryModel ??= new QueryModel();
            var result = new PageListModel<SystemLogModel>();
            var select = _freeSql.Select<SystemLog>();
            if (arg.QueryModel.QueryItemList.Count > 0)
            {

            }

            //select = select.OrderByDescending(e => e.LogTime).to;
            //var (pagedData, totalCount) = await _freeSql.Select<SystemLog>()
            //    .WhereIf(!string.IsNullOrEmpty(arg.), e => e.Message.Contains(arg.Filter)) // 假设添加过滤条件，根据实际需求调整
            //    .OrderByDescending(e => e.Id) // 假设按Id降序排列，根据实际需求调整排序条件
            //    .ToPageAsync(arg.Index, arg.Size, true);
            //result.Count = totalCount;
            //result.Data = pagedData;
            return result;
        }
    }
}
