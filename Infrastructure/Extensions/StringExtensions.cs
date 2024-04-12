using Newtonsoft.Json;

namespace Infrastructure.Extensions
{
    public static partial class Extensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static T ToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        public static List<T> ToList<T>(this string str)
        {
            return JsonConvert.DeserializeObject<List<T>>(str); 
        }
    }
}
