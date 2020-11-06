using Microsoft.Extensions.Configuration;

namespace NetCore.Fast.Utility.Configuration
{
    /// <summary>
    /// 读取配置xml 文件配置
    /// </summary>
    public class ConfigManagerXml : ConfigurationBuild, IConfigManager
    {
        /// <summary>
        /// 读取配置xml 文件配置
        /// </summary>
        /// <param name="fileName">文件地址</param>
        public ConfigManagerXml(string fileName = "appsettings.xml") : base(fileName, ConfigBuildType.Ini)
        {

        }

        /// <summary>
        /// 获取配置文件指定值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">需要读取的key  层级可以使用 key:key </param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return ConfigBuild.GetSection(key).Get<T>();
        }
    }
}
