using Infrastructure.Model.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class ExpressionExtensions
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">实体模型</typeparam>
        /// <param name="freeSql">数据库上下文</param>
        /// <param name="queryPageListModel">查询条件</param>
        /// <returns></returns>
        public static async Task<PageListModel<T>> GetPagedResultAsync<T>(this IFreeSql freeSql,
     QueryPageListModel queryPageListModel) where T : class
        {
            var selectQuery = freeSql.Select<T>();

            // 处理查询条件
            foreach (var item in queryPageListModel.QueryModel.QueryItemList)
            {
                switch (item.Operator)
                {
                    case EQueryModel.Equal:
                        selectQuery = selectQuery.Where($"{item.Field} = @0", item.Value);
                        break;
                    case EQueryModel.NotEqual:
                        selectQuery = selectQuery.Where($"{item.Field} != @0", item.Value);
                        break;
                    case EQueryModel.LessThan:
                        selectQuery = selectQuery.Where($"{item.Field} < @0", item.Value);
                        break;
                    case EQueryModel.LessThanOrEqual:
                        selectQuery = selectQuery.Where($"{item.Field} <= @0", item.Value);
                        break;
                    case EQueryModel.GreaterThan:
                        selectQuery = selectQuery.Where($"{item.Field} > @0", item.Value);
                        break;
                    case EQueryModel.GreaterThanOrEqual:
                        selectQuery = selectQuery.Where($"{item.Field} >= @0", item.Value);
                        break;
                    case EQueryModel.Contains:
                        selectQuery = selectQuery.Where($"{item.Field} LIKE @0", $"%{item.Value}%");
                        break;
                    case EQueryModel.NotContains:
                        selectQuery = selectQuery.Where($"{item.Field} NOT LIKE @0", $"%{item.Value}%");
                        break;
                    case EQueryModel.Like:
                        selectQuery = selectQuery.Where($"{item.Field} LIKE @0", $"%{item.Value}%");
                        break;
                    case EQueryModel.StartsWith:
                        selectQuery = selectQuery.Where($"{item.Field} LIKE @0", $"{item.Value}%");
                        break;
                    case EQueryModel.EndsWith:
                        selectQuery = selectQuery.Where($"{item.Field} LIKE @0", $"%{item.Value}");
                        break;
                    case EQueryModel.LikeIn:
                        var values = item.Value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var likeInConditions = string.Join(" OR ", values.Select(v => $"{item.Field} LIKE @0"));
                        selectQuery = selectQuery.Where($"({likeInConditions})", values.Select(v => $"%{v.Trim()}%").ToArray());
                        break;
                }
            }

            // 计算总条数
            var totalCount = await selectQuery.CountAsync();
            // 分页
            var result = await selectQuery
                .Page(queryPageListModel.Index, queryPageListModel.Size)
                .ToListAsync();


            return new PageListModel<T>
            {
                Data = result,
                Count = totalCount,
                PageIndex = queryPageListModel.Index,
                PageSize = queryPageListModel.Size
            };
        }
    }
}
