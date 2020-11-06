using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Fast.Utility.HttpHelper
{
    /// <summary>
    /// http请求接口
    /// </summary>
    partial interface IHttpClient
    {

        #region POST请求
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Stream PostStream(HttpClientContent param);

        /// <summary>
        /// 异步Post请求 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Stream> PostStreamAsync(HttpClientContent param);

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        byte[] PostBytes(HttpClientContent param);

        /// <summary>
        /// 异步Post请求 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<byte[]> PostBytesAsync(HttpClientContent param);

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string PostString(HttpClientContent param);

        /// <summary>
        /// 异步Post请求 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<string> PostStringAsync(HttpClientContent param);
        #endregion

        #region Get请求
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Stream GetStream(HttpClientContent param);

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Stream> GetStreamAsync(HttpClientContent param);

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        byte[] GetBytes(HttpClientContent param);

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<byte[]> GetBytesAsync(HttpClientContent param);

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string GetString(HttpClientContent param);

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<string> GetStringAsync(HttpClientContent param);

        #endregion

        #region Delete请求

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Stream DeleteStream(HttpClientContent param);

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Stream> DeleteStreamAsync(HttpClientContent param);

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        byte[] DeleteBytes(HttpClientContent param);

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<byte[]> DeleteBytesAsync(HttpClientContent param);

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string DeleteString(HttpClientContent param);

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<string> DeleteStringAsync(HttpClientContent param);

        #endregion

        #region Put请求

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Stream PutStream(HttpClientContent param);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Stream> PutStreamAsync(HttpClientContent param);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        byte[] PutBytes(HttpClientContent param);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<byte[]> PutBytesAsync(HttpClientContent param);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string PutString(HttpClientContent param);

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<string> PutStringAsync(HttpClientContent param);

        #endregion

        #region 上传AND下载

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string UploadFile(HttpClientUpload param);

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<string> UploadFileAsync(HttpClientUpload param);

        /// <summary>
        /// 文件请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string DownloadFile(HttpClientDownload param);

        /// <summary>
        /// 文件请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<string> DownloadFileAsync(HttpClientDownload param);

        /// <summary>
        /// 请求流保存到文件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        string DownloadBytesFile(HttpClientDownload param);

        /// <summary>
        /// 请求流保存到文件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<string> DownloadBytesFileAsync(HttpClientDownload param);
        #endregion

    }
}
