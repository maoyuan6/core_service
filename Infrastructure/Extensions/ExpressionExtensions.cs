using Infrastructure.Model.RequestModel;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                var parameter = Expression.Parameter(typeof(T), "x");
                var member = Expression.PropertyOrField(parameter, item.Field);
                var memberType = member.Type;

                // Convert item.Value to the member's type
                var convertedValue = Convert.ChangeType(item.Value, memberType);

                // Create a constant expression for the converted value
                var constant = Expression.Constant(convertedValue, memberType);

                Expression? body = item.Operator switch
                {
                    EQueryModel.Equal => Expression.Equal(member, constant),
                    EQueryModel.NotEqual => Expression.NotEqual(member, constant),
                    EQueryModel.LessThan => Expression.LessThan(member, constant),
                    EQueryModel.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
                    EQueryModel.GreaterThan => Expression.GreaterThan(member, constant),
                    EQueryModel.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
                    EQueryModel.Contains => Expression.Call(member, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant),
                    EQueryModel.NotContains => Expression.Not(Expression.Call(member, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant)),
                    EQueryModel.Like => Expression.Call(member, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant),
                    EQueryModel.StartsWith => Expression.Call(member, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), constant),
                    EQueryModel.EndsWith => Expression.Call(member, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), constant),
                    EQueryModel.LikeIn => BuildLikeInExpression(member, item.Value),
                    _ => null
                };

                if (body != null)
                {
                    var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
                    selectQuery = selectQuery.Where(lambda);
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

        //处理likein
        private static Expression BuildLikeInExpression(MemberExpression member, object itemValue)
        {
            var conditions = itemValue.ToString().Split(',')
                .Select(condition => condition.Trim())
                .Where(condition => !string.IsNullOrWhiteSpace(condition))
                .ToList();

            if (!conditions.Any())
                return Expression.Constant(false); // Return false if no conditions

            var likeMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            Expression combinedBody = null;

            foreach (var condition in conditions)
            {
                var likePattern = $"%{condition}%";
                var likeConstant = Expression.Constant(likePattern);
                var likeExpression = Expression.Call(member, likeMethod, likeConstant);

                combinedBody = combinedBody == null
                    ? likeExpression
                    : Expression.OrElse(combinedBody, likeExpression);
            }

            return combinedBody;
        }
    }
}

