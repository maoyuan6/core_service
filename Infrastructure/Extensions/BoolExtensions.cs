using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Model;

namespace Infrastructure.Extensions
{
    public static partial class Extensions
    {
        /// <summary>false的时候抛出异常，配合事务try catch用于回滚</summary>
        /// <param name="flag"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static void ThrowException(this bool flag, string exceptionMessage, int hResult = 600)
        {
            if (!flag)
                throw new BusinessException(exceptionMessage, hResult);
        }

        /// <summary>true的时候抛出异常，配合事务try catch用于回滚</summary>
        /// <param name="flag"></param>
        /// <param name="exceptionMessage"></param>
        /// <returns></returns>
        public static void TrueThrowException(this bool flag, string exceptionMessage, int hResult = 600)
        {
            if (flag)
                throw new BusinessException(exceptionMessage, hResult);
        }
    }
}
