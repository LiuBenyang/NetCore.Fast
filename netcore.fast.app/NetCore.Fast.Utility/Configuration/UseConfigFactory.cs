namespace NetCore.Fast.Utility.Configuration
{
    /// <summary>
    /// 读取配置文件
    /// </summary>
    public class UseConfigFactory : IConfigManager
    {
        #region 读取appsettings.json 配置文件

        static ConfigManagerJson _JsonSing;
        static readonly object _JsonSingLock = new object();

        /// <summary>
        /// 使用JSON 配置文件 appsettings.json
        /// </summary>
        public static ConfigManagerJson Json
        {
            get
            {
                if (_JsonSing == null)
                {
                    lock (_JsonSingLock)
                    {
                        if (_JsonSing == null)
                        {
                            _JsonSing = new ConfigManagerJson();
                            return _JsonSing;
                        }
                    }
                }
                return _JsonSing;
            }
        }
        #endregion

        #region  读取appsettings.xml 配置文件 

        static ConfigManagerXml _Xml;
        static readonly object _XmlLock = new object();
        /// <summary>
        /// 使用XML配置文件  appsettings.xml
        /// </summary>
        public static ConfigManagerXml Xml
        {
            get
            {
                if (_Xml == null)
                {
                    lock (_XmlLock)
                    {
                        if (_Xml == null)
                        {
                            _Xml = new ConfigManagerXml();
                            return _Xml;
                        }
                    }
                }
                return _Xml;
            }
        }


        #endregion

        #region  读取appsettings.ini 配置文件 
        static ConfigManagerIni _Ini;
        static readonly object _IniLock = new object();
        /// <summary>
        /// 使用ini配置文件  appsettings.xml
        /// </summary>
        public static ConfigManagerIni Ini
        {
            get
            {
                if (_Ini == null)
                {
                    lock (_IniLock)
                    {
                        if (_Ini == null)
                        {
                            _Ini = new ConfigManagerIni();
                            return _Ini;
                        }
                    }
                }
                return _Ini;
            }
        }



        #endregion


        static ConfigManagerJson _AppConfigJson;
        static readonly object _AppConfigJsonLock = new object();

        /// <summary>
        /// 读取 appconfig.json 配置文件
        /// </summary>
        public static ConfigManagerJson AppConfigJson
        {
            get
            {
                if (_AppConfigJson == null)
                {
                    lock (_AppConfigJsonLock)
                    {
                        if (_AppConfigJson == null)
                        {
                            _AppConfigJson = new ConfigManagerJson("appconfig.json");
                            return _AppConfigJson;
                        }
                    }
                }
                return _AppConfigJson;
            }
        }


        IConfigManager _ConfigManager;

        /// <summary>
        /// 读取配置文件
        /// </summary>
        public UseConfigFactory()
        {

        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="buildType">选择读取类型</param>
        public UseConfigFactory(string fileName, ConfigBuildType buildType = ConfigBuildType.Json)
        {

            switch (buildType)
            {
                case ConfigBuildType.Json:
                    _ConfigManager = new ConfigManagerJson(fileName);
                    break;
                case ConfigBuildType.Xml:
                    _ConfigManager = new ConfigManagerXml(fileName);
                    break;
                case ConfigBuildType.Ini:
                    _ConfigManager = new ConfigManagerIni(fileName);
                    break;
            }
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="configManager">需要对应的读取类</param>
        public UseConfigFactory(IConfigManager configManager)
        {
            _ConfigManager = configManager;
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">对应的键</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return _ConfigManager.Get<T>(key);
        }
    }
}
