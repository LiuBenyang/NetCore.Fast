using CSRedis;
using NetCore.Fast.Utility.Configuration;
using NetCore.Fast.Utility.ToExtensions;
using System;
using System.Threading.Tasks;

namespace NetCore.Fast.Utility.Cache
{
    /// <summary>
    /// Redis 缓存操作
    /// </summary>
    public class CSRedisManage : ICache
    {
        /// <summary>
        /// 默认读取配置文件中的值 RedisConnectString 值
        /// </summary>
        static string ConnectString = UseConfigFactory.Json.Get<string>("RedisConnectionString") ?? "localhost";

        //锁
        static readonly object _lock = new object();

        /// <summary>
        /// 
        /// </summary>
        static CSRedisClient _RedisClient;

        /// <summary>
        /// 访问对象
        /// </summary>
        static CSRedisClient RedisClient
        {
            get
            {
                if (_RedisClient == null)
                {
                    lock (_lock)
                    {
                        if (_RedisClient == null)
                        {
                            _RedisClient = new CSRedisClient(ConnectString);
                            return _RedisClient;
                        }
                    }
                }
                return _RedisClient;
            }
        }

        /// <summary>
        /// Redis 缓存操作
        /// </summary>
        public CSRedisManage()
        {
            //RedisHelper.Initialization(RedisClient);
        }

        /// <summary>
        ///  Redis 缓存操作
        /// </summary>
        /// <param name="connString">连接字符串</param>
        public CSRedisManage(string connString)
        {
            ConnectString = connString;
            //RedisHelper.Initialization(RedisClient);
        }

        /// <summary>
        /// 添加一个缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">内容</param>
        /// <param name="expiry">时间</param>
        public bool Add(string key, object value, TimeSpan expiry)
        {
            return RedisClient.Set(key, value, expiry);
        }

        /// <summary>
        /// 添加一个缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">内容</param>
        /// <param name="expireSeconds">秒</param>
        /// <returns></returns>
        public bool Add(string key, object value, int expireSeconds = -1)
        {
            return RedisClient.Set(key, value, expireSeconds);
        }

        /// <summary>
        /// 异步添加一个缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">内容</param>
        /// <param name="expiry">时间</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value, TimeSpan expiry)
        {
            return await RedisClient.SetAsync(key, value, expiry);
        }

        /// <summary>
        /// 异步添加一个缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">内容</param>
        /// <param name="expireSeconds">时间</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value, int expireSeconds = -1)
        {
            return await RedisClient.SetAsync(key, value, expireSeconds);
        }

        /// <summary>
        /// 插入一个集合值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="entity">对象</param>
        /// <param name="expiry">时间</param>
        /// <returns></returns>
        public bool AddList<T>(string key, T entity, TimeSpan expiry) where T : class
        {
            return Add(key, entity.ToJson(), expiry);
        }

        /// <summary>
        /// 插入一个集合值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">键</param>
        /// <param name="entity">对象</param>
        /// <param name="expireSeconds">时间秒</param>
        /// <returns></returns>
        public bool AddList<T>(string key, T entity, int expireSeconds = -1) where T : class
        {
            return Add(key, entity.ToJson(), expireSeconds);
        }

        /// <summary>
        /// 异步插入一个集合值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> AddListAsync<T>(string key, T entity, TimeSpan expiry) where T : class
        {
            return await AddAsync(key, entity.ToJson(), expiry);
        }

        /// <summary>
        /// 异步插入一个集合值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="entity">内容</param>
        /// <param name="expireSeconds">时间/秒</param>
        /// <returns></returns>
        public async Task<bool> AddListAsync<T>(string key, T entity, int expireSeconds = -1) where T : class
        {
            return await AddAsync(key, entity.ToJson(), expireSeconds);
        }

        /// <summary>
        /// 移除缓存中的对应键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool Delete(string key)
        {
            return RedisClient.Del(new string[] { key }) > 0;
        }

        /// <summary>
        /// 异步移除缓存中的对应键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(string key)
        {
            return await RedisClient.DelAsync(new string[] { key }) > 0;
        }


        /// <summary>
        /// 获取一个缓存值 键不存在 返回 null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public string Get(string key)
        {
            return RedisClient.Get(key);
        }

        /// <summary>
        /// 获取一个缓存值 键不存在 返回 null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            return await RedisClient.GetAsync(key);
        }

        /// <summary>
        /// 获取一组泛型值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetList<T>(string key) where T : class, new()
        {
            return RedisClient.Get(key).JsonToObject<T>();
        }


        /// <summary>
        /// 异步获取一组泛型值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetListAsync<T>(string key) where T : class, new()
        {
            var rs = await RedisClient.GetAsync(key);
            return rs.JsonToObject<T>();
        }

        /// <summary>
        /// 判断当前键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool KeyExists(string key)
        {
            return RedisClient.Exists(key);
        }

        /// <summary>
        /// 判断当前键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string key)
        {
            return await RedisClient.ExistsAsync(key);
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
            return RedisClient.HSet(key, field, value);
        }

        /// <summary>
        /// 获取哈希表值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">哈希键</param>
        /// <returns></returns>
        public object GetHashValue(string key, string field)
        {
            return RedisClient.HGet(key, field);
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
            return RedisClient.HGet(key, field).JsonToObject<T>();
        }

        /// <summary>
        /// 判断hash key 是否存在
        /// </summary>
        /// <param name="key">哈希表键</param>
        /// <param name="field">哈希表键字段</param>
        /// <returns></returns>
        public bool HashKeyExists(string key, string field)
        {
            return RedisClient.HExists(key, field);
        }

        /// <summary>
        /// 判断hash key 是否存在
        /// </summary>
        /// <param name="key">哈希表键</param>
        /// <param name="field">哈希表键字段</param>
        /// <returns></returns>
        public async Task<bool> HashKeyExistsAsync(string key, string field)
        {
            return await RedisClient.HExistsAsync(key, field);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">删除哈希表键</param>
        /// <param name="field">删除哈希表键字段</param>
        /// <returns></returns>
        public bool DeleteHash(string key, string field)
        {
            return RedisClient.HDel(key, field) > 0;
        }

        /// <summary>
        /// 删除Async
        /// </summary>
        /// <param name="key">删除哈希表键</param>
        /// <param name="field">删除哈希表键字段</param>
        /// <returns></returns>
        public async Task<bool> DeleteHashAsync(string key, string field)
        {
            var rs = await RedisClient.HDelAsync(key, field);
            return rs > 0;
        }

        /// <summary>
        /// 删除多个key支持正则匹配
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool DeleteKeys(string[] keys)
        {
            return RedisClient.Del(keys) > 0;
        }

        /// <summary>
        /// 删除多个key支持正则匹配
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public async Task<bool> DeleteKeysAsync(string[] keys)
        {
            var rs = await RedisClient.DelAsync(keys);
            return rs > 0;
        }
    }
}
