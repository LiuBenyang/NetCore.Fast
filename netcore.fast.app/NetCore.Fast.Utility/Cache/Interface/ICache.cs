using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Fast.Utility.Cache
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        #region 添加
        /// <summary>
        /// 添加一个缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">内容</param>
        /// <param name="expiry">时间</param>
        bool Add(string key, object value, TimeSpan expiry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        bool Add(string key, object value, int expireSeconds = -1);

        /// <summary>
        /// 异步添加一个缓存值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">内容</param>
        /// <param name="expiry">时间</param>
        /// <returns></returns>
        Task<bool> AddAsync(string key, object value, TimeSpan expiry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        Task<bool> AddAsync(string key, object value, int expireSeconds = -1);

        /// <summary>
        /// 插入一个集合值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        bool AddList<T>(string key, T entity, TimeSpan expiry) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        bool AddList<T>(string key, T entity, int expireSeconds = -1) where T : class;

        /// <summary>
        /// 异步插入一个集合值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        Task<bool> AddListAsync<T>(string key, T entity, TimeSpan expiry) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expireSeconds"></param>
        /// <returns></returns>
        Task<bool> AddListAsync<T>(string key, T entity, int expireSeconds = -1) where T : class;

        #endregion

        /// <summary>
        /// 移除缓存中的对应键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        bool Delete(string key);

        /// <summary>
        /// 异步移除缓存中的对应键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string key);

        #region 获取值


        /// <summary>
        /// 获取一个缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 异步获取一个缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 获取一组泛型值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetList<T>(string key) where T : class, new();

        /// <summary>
        /// 异步获取一组泛型值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetListAsync<T>(string key) where T : class, new();

        #endregion

        /// <summary>
        /// 判断当前键是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        bool KeyExists(string key);

        /// <summary>
        /// 异步判断当前键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> KeyExistsAsync(string key);


        /// <summary>
        /// 添加哈希表
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">哈希键</param>
        /// <param name="value">保存的值</param>
        /// <returns></returns>
        bool AddHash(string key, string field, string value);

        /// <summary>
        /// 获取哈希表值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">哈希键</param>
        /// <returns></returns>
        object GetHashValue(string key, string field);

        /// <summary>
        /// 获取哈希表值 解析成JSON对应的对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="field">哈希键</param>
        /// <returns></returns>
        T GetHashValue<T>(string key, string field) where T : class, new();

        /// <summary>
        /// 判断hash key 是否存在
        /// </summary>
        /// <param name="key">哈希表键</param>
        /// <param name="field">哈希表键字段</param>
        /// <returns></returns>
        bool HashKeyExists(string key, string field);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key">删除哈希表键</param>
        /// <param name="field">删除哈希表键字段</param>
        /// <returns></returns>
        bool DeleteHash(string key, string field);

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        Task<bool> DeleteHashAsync(string key, string field);

        /// <summary>
        /// 删除多个key 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        bool DeleteKeys(string[] keys);

        /// <summary>
        /// 删除多个key
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<bool> DeleteKeysAsync(string[] keys);

    }
}
