using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace NetCore.Fast.Utility.Configuration
{

    /// <summary>
    /// 用于构造 配置文件的构造器 类型
    /// </summary>
    public enum ConfigBuildType
    {
        /// <summary>
        /// 读取json配置文件
        /// </summary>
        Json,
        /// <summary>
        /// 读取xml配置文件
        /// </summary>
        Xml,
        /// <summary>
        /// 读取ini配置文件
        /// </summary>
        Ini
    }

    /// <summary>
    /// 
    /// </summary>
    public class ConfigurationBuild
    {
        /// <summary>
        /// 配置构造器
        /// </summary>
        public IConfigurationRoot ConfigBuild;

        /// <summary>
        /// 用于构造 配置文件的构造器
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="type">类型</param>
        public ConfigurationBuild(string fileName, ConfigBuildType type)
        {
            var builder = new ConfigurationBuilder();
            switch (type)
            {
                case ConfigBuildType.Json:
                    builder.AddJsonFile(fileName, optional: false, reloadOnChange: true);
                    break;
                case ConfigBuildType.Xml:
                    builder.AddXmlFile(fileName, optional: false, reloadOnChange: true);
                    break;
                case ConfigBuildType.Ini:
                    builder.AddIniFile(fileName, optional: false, reloadOnChange: true);
                    break;
                default:
                    throw new Exception("构造 ConfigurationBuild 遇到未识别的类型");
            }
            ConfigBuild = builder.Build();
        }
    }
}
