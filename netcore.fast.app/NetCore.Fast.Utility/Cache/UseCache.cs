using System;
using System.Threading.Tasks;

namespace NetCore.Fast.Utility.Cache
{
    /// <summary>
    /// 缓存公共调用
    /// </summary>
    public class UseCache : ICache
    {
        /// <summary>
        /// 操控缓存的对象
        /// </summary>
        ICache _ICache;


        #region 懒加载/单例模式

        //实例对象
        static UseCache _Lazy;
        //锁
        static readonly object _lock = new object();

        /// <summary>
        /// 实例化
        /// </summary>
        public static UseCache Instance
        {
            get
            {
                if (_Lazy == null)
                {
                    lock (_lock)
                    {
                        if (_Lazy == null)
                            _Lazy = new UseCache();
                        return _Lazy;
                    }
                }
                return _Lazy;
            }
        }

        #endregion



        /// <summary>
        /// 初始化缓存库 默认使用Redis
        /// </summary>
        public UseCache()
        {
            _ICache = new CSRedisManage();
        }

        /// <summary>
        /// 初始化缓存库
        /// </summary>
        /// <param name="cache"></param>
        public UseCache(ICache cache)
        {
            _ICache = cache;
        }


        #region 实现方法

        /// <summary>
        /// 添加一个缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">内容</param>
        /// <param name="expiry">时间</param>
        public bool Add(string key, object value, TimeSpan expiry)
        {
            return _ICache.Add(key, value, expiry);
        }

        /// <summary>
        /// 异步添加一个缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">内容</param>
        /// <param name="expiry">时间</param>
        /// <returns></returns>
        public Task<bool> AddAsync(string key, object value, TimeSpan expiry)
        {
            return _ICache.AddAsync(key, value, expiry);
        }

        /// <summary>
        /// 插入一个集合值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool AddList<T>(string key, T entity, TimeSpan expiry) where T : class
        {
            return _ICache.AddList<T>(key, entity, expiry);
        }

        /// <summary>
        /// 异步插入一个集合值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> AddListAsync<T>(string key, T entity, TimeSpan expiry) where T : class
        {
            return _ICache.AddListAsync<T>(key, entity, expiry);
        }

        /// <summary>
        /// 移除缓存中的对应键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            return _ICache.Delete(key);
        }

        /// <summary>
        /// 异步移除缓存中的对应键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(string key)
        {
            return _ICache.DeleteAsync(key);
        }


        /// <summary>
        /// 获取一个缓存值 键不存在 返回 null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public string Get(string key)
        {
            return _ICache.Get(key);
        }


        /// <summary>
        /// 获取一个缓存值 键不存在 返回 null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            return await _ICache.GetAsync(key);
        }

        /// <summary>
        /// 获取一组泛型值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetList<T>(string key) where T : class, new()
        {
            return _ICache.GetList<T>(key);
        }


        /// <summary>
        /// 异步获取一组泛型值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetListAsync<T>(string key) where T : class, new()
        {
            return await _ICache.GetListAsync<T>(key);
        }

        /// <summary>
        /// 判断当前键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return _ICache.KeyExists(key);
        }

        /// <summary>
        /// 判断当前键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await _ICache.KeyExistsAsync(key);
        }

        /// <summary>
        /// 添加哈希表
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">哈希键</param>
        /// <param name="value">保存的值</param>
        /// <returns></returns>
        public bool AddHash(string key, string field, string value)
        {
            return _ICache.AddHash(key, field, value);
        }

        /// <summary>
        /// 获取哈希表值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">哈希键</param>
        /// <returns></returns>
        public object GetHashValue(string key, string field)
        {
            return _ICache.GetHashValue(key, field);
        }

        /// <summary>
        /// 获取哈希表转换成 对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="field">哈希键</param>
        /// <returns></returns>
        public T GetHashValue<T>(string key, string field) where T : class, new()
        {
            return _ICache.GetHashValue<T>(key, field);
        }

        /// <summary>
        /// 判断hash key 是否存在
        /// </summary>
        /// <param name="key">哈希表键</param>
        /// <param name="field">哈希表键字段</param>
        /// <returns></returns>
        public bool HashKeyExists(string key, string field)
        {
            return _ICache.HashKeyExists(key, field);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">删除哈希表键</param>
        /// <param name="field">删除哈希表键字段</param>
        /// <returns></returns>
        public bool DeleteHash(string key, string field)
        {
            return _ICache.DeleteHash(key, field);
        }

        /// <summary>
        /// 删除hash值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public async Task<bool> DeleteHashAsync(string key, string field)
        {
            return await _ICache.DeleteHashAsync(key, field);
        }

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool DeleteKeys(string[] keys)
        {
            return _ICache.DeleteKeys(keys);
        }

        /// <summary>
        /// 删除多个key支持表达式
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<bool> DeleteKeysAsync(string[] keys)
        {
            return await _ICache.DeleteKeysAsync(keys);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        public bool Add(string key, object value, int expireSeconds = -1)
        {
            return _ICache.Add(key, value, expireSeconds);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value, int expireSeconds = -1)
        {
            return await _ICache.AddAsync(key, value, expireSeconds);
        }

        /// <summary>
        /// 添加会自动转换成JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        public bool AddList<T>(string key, T entity, int expireSeconds = -1) where T : class
        {
            return _ICache.AddList(key, entity, expireSeconds);
        }

        /// <summary>
        /// 添加会自动转换成JSON字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        public async Task<bool> AddListAsync<T>(string key, T entity, int expireSeconds = -1) where T : class
        {
            return await _ICache.AddListAsync(key, entity, expireSeconds);
        }


        #endregion
    }
}
