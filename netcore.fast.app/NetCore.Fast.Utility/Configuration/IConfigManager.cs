namespace NetCore.Fast.Utility.Configuration
{
    /// <summary>
    /// 读取配置文件管理
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// 获取指定值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">需要读取配置文件的键</param>
        /// <returns></returns>
        T Get<T>(string key);
    }
}
