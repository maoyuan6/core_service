using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreeSql.DataAnnotations;
using Infrastructure.Base;

namespace Repository.Entities
{
    public class AsyncEntity
    {
        public static Type[] GetTypesByTableAttribute()
        {
            List<Type> tableAssembies = new List<Type>();
            var baseType = typeof(IEntityBase);
            foreach (Type type in Assembly.GetAssembly(baseType).GetExportedTypes()
                         .Where(a => a.IsInterface == false && baseType.IsAssignableFrom(a)))
                foreach (Attribute attribute in type.GetCustomAttributes())
                    if (attribute is TableAttribute tableAttribute)
                        if (tableAttribute.DisableSyncStructure == false)
                            tableAssembies.Add(type);

            return tableAssembies.ToArray();
        }
    }
}
