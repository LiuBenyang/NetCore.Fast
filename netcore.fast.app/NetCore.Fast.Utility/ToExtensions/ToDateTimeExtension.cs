using System;

namespace NetCore.Fast.Utility.ToExtensions
{
    /// <summary>
    /// 对时间转化的扩展
    /// </summary>
    public static class ToDateTimeExtension
    {
        /// <summary>
        /// Unix起始时间
        /// </summary>
        public static DateTime BaseTime = new DateTime(1970, 1, 1);

        /// <summary>
        /// Unix时间戳(微信)转化成C#时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳</param>
        /// <returns></returns>
        public static DateTime UnixToDateTime(this long timeStamp)
        {
            return BaseTime.AddTicks((timeStamp + 8 * 60 * 60) * 10000000);
        }

        /// <summary>
        /// Unix时间戳(微信)转化成C#时间
        /// </summary>
        /// <param name="timeStamp">Unix时间戳</param>
        /// <returns></returns>
        public static DateTime UnixToDateTime(this string timeStamp)
        {
            return UnixToDateTime(long.Parse(timeStamp));
        }

        /// <summary>
        /// C#时间转换成Unix时间戳(微信)
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns></returns>
        public static long ToUnixTimeStamp(this DateTime dateTime)
        {
            return (dateTime.Ticks - BaseTime.Ticks) / 10000000 - 8 * 60 * 60;
        }
    }
}
