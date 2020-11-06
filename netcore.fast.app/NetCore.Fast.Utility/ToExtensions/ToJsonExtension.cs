using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NetCore.Fast.Utility.ToExtensions
{
    /// <summary>
    /// 转换JSON字符串类型
    /// </summary>
    public enum ToJsonType
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default,
        /// <summary>
        /// 过滤空值
        /// </summary>
        IgnoreNull,
        /// <summary>
        /// 首字母小写
        /// </summary>
        Resolver,
        /// <summary>
        /// 首字母小写且过滤null值
        /// </summary>
        ResolverIgnoreNull
    }

    /// <summary>
    /// ToJson扩展
    /// </summary>
    public static class ToJsonExtension
    {

        /// <summary>
        /// .NET对象转换成 JSON字符串
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToJson(this object o)
        {
            var settings = new IsoDateTimeConverter();
            settings.DateTimeFormat = "yyyy-MM-dd HH:mm:ss.ffff";
            return JsonConvert.SerializeObject(o, Formatting.None, settings);
        }
        /// <summary>
        /// .NET对象转换成 JSON字符串
        /// </summary>
        /// <param name="o"></param>
        /// <param name="type">需要转换的类型</param>
        /// <returns></returns>
        public static string ToJson(this object o, ToJsonType type)
        {
            var jsetting = new JsonSerializerSettings();
            switch (type)
            {
                case ToJsonType.Default:
                    break;
                case ToJsonType.IgnoreNull:
                    jsetting.NullValueHandling = NullValueHandling.Ignore;
                    break;
                case ToJsonType.Resolver:
                    jsetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    break;
                case ToJsonType.ResolverIgnoreNull:
                    jsetting.NullValueHandling = NullValueHandling.Ignore;
                    jsetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    break;
            }
            return JsonConvert.SerializeObject(o, Formatting.None, jsetting);
        }

        /// <summary>
        /// JSON 字符串对象 转成 .NET对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string s)
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

    }
}
