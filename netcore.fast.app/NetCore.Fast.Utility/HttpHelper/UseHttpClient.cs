using NetCore.Fast.Utility.ToExtensions;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Fast.Utility.HttpHelper
{
    /// <summary>
    ///  Http 请求类 
    /// </summary>
    public class UseHttpClient
    {

        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        static Stream Request(HttpClientContent param)
        {
            var encoding = param.Encode;
            byte[] data = encoding.GetBytes(param.Data);
            var request = WebRequest.Create(param.Url) as HttpWebRequest;
            request.Method = param.MethodType.ToString();
            request.ContentType = param.ContentType;
            if (param.Headers != null && param.Headers.Count > 0)
            {
                foreach (var item in param.Headers)
                {
                    request.Headers.Add(item.Key, item.Value);
                }
            }
            if (param.X509Certificate != null && param.X509Certificate.Count > 0)
            {
                request.ClientCertificates.AddRange(param.X509Certificate.ToArray());
            }
            if (param.MethodType == HttpMethodType.Post || param.MethodType == HttpMethodType.Put)
            {
                request.ContentLength = data.Length;
                using (var outstream = request.GetRequestStream())
                {
                    outstream.Write(data, 0, data.Length);
                }
            }
            var response = request.GetResponse() as HttpWebResponse;
            return response.GetResponseStream();
        }

        #region Delete请求

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private static Stream DeleteRequest(HttpClientContent param)
        {
            param.MethodType = HttpMethodType.Delete;
            return Request(param);
        }

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static byte[] DeleteBytes(HttpClientContent param)
        {
            return DeleteRequest(param).ToBytes();
        }

        /// <summary>
        /// 异步Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<byte[]> DeleteBytesAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return DeleteBytes(param); });
        }

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Stream DeleteStream(HttpClientContent param)
        {
            return DeleteRequest(param);
        }

        /// <summary>
        ///异步 Delete 请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<Stream> DeleteStreamAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return DeleteStream(param); });
        }

        /// <summary>
        /// Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string DeleteString(HttpClientContent param)
        {
            try
            {
                using (var s = new StreamReader(DeleteRequest(param), Encoding.UTF8))
                {
                    return s.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 异步Delete请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> DeleteStringAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return DeleteString(param); });
        }

        #endregion

        #region 下载

        /// <summary>
        /// 下载字节的文件
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回当前本地路径</returns>
        public static string DownloadBytesFile(HttpClientDownload param)
        {
            var rs = Request(param).ToBytes();
            var _path = $"{ param.LocalFolderPath }\\{param.LocalFileName}";
            File.WriteAllBytes(_path, rs);
            return _path;
        }

        /// <summary>
        /// 异步下载字节文件
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns>返回当前本地路径</returns>
        public static async Task<string> DownloadBytesFileAsync(HttpClientDownload param)
        {
            return await Task.Run(() => { return DownloadFile(param); });
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string DownloadFile(HttpClientDownload param)
        {
            using (var client = new WebClient())
            {
                if (param.Headers != null && param.Headers.Count > 0)
                {
                    foreach (var item in param.Headers)
                    {
                        client.Headers.Add(item.Key, item.Value);
                    }
                }
                var _path = $"{ param.LocalFolderPath }\\{param.LocalFileName}";
                client.DownloadFile(param.Url, _path);
                return _path;
            }
        }

        /// <summary>
        /// 异步下载文件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> DownloadFileAsync(HttpClientDownload param)
        {
            return await Task.Run(() => { return DownloadFile(param); });
        }

        #endregion

        #region Get请求

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Stream GetRequest(HttpClientContent param)
        {
            param.MethodType = HttpMethodType.Get;
            return Request(param);
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static byte[] GetBytes(HttpClientContent param)
        {
            return GetRequest(param).ToBytes();
        }

        /// <summary>
        /// 异步Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<byte[]> GetBytesAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return GetBytes(param); });
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Stream GetStream(HttpClientContent param)
        {
            return GetRequest(param);
        }

        /// <summary>
        /// 异步Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<Stream> GetStreamAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return GetStream(param); });
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetString(HttpClientContent param)
        {
            try
            {
                using (var s = new StreamReader(GetRequest(param), Encoding.UTF8))
                {
                    return s.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 异步Get请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> GetStringAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return GetString(param); });
        }

        #endregion

        #region Post请求

        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Stream PostRequest(HttpClientContent param)
        {
            param.MethodType = HttpMethodType.Post;
            return Request(param);
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static byte[] PostBytes(HttpClientContent param)
        {
            return PostStream(param).ToBytes();
        }

        /// <summary>
        /// 异步Post 请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<byte[]> PostBytesAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return PostBytes(param); });
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Stream PostStream(HttpClientContent param)
        {
            return Request(param);
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<Stream> PostStreamAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return PostStream(param); });
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string PostString(HttpClientContent param)
        {
            try
            {
                using (var s = new StreamReader(PostRequest(param), Encoding.UTF8))
                {
                    return s.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 异步 Post 请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> PostStringAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return PostString(param); });
        }

        #endregion

        #region Put请求

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Stream PutRequest(HttpClientContent param)
        {
            param.MethodType = HttpMethodType.Put;
            return Request(param);
        }

        /// <summary>
        /// Put 请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static byte[] PutBytes(HttpClientContent param)
        {
            return PutRequest(param).ToBytes();
        }

        /// <summary>
        /// 异步Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<byte[]> PutBytesAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return PutBytes(param); });
        }

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static Stream PutStream(HttpClientContent param)
        {
            return PutRequest(param);
        }

        /// <summary>
        /// 异步Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<Stream> PutStreamAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return PutStream(param); });
        }

        /// <summary>
        /// Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string PutString(HttpClientContent param)
        {
            try
            {
                using (var s = new StreamReader(PutRequest(param), Encoding.UTF8))
                {
                    return s.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 异步Put请求
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> PutStringAsync(HttpClientContent param)
        {
            return await Task.Run(() => { return PutString(param); });
        }

        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string UploadFile(HttpClientUpload param)
        {
            var rq = (HttpWebRequest)WebRequest.Create(param.Url);
            rq.Method = "POST";
            //rq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
            var fileMemoryStream = new MemoryStream(); //文件写入流
            string boundary = "---------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            rq.ContentType = $"multipart/form-data; boundary={boundary}";

            if (param.Files != null && param.Files.Count > 0)
            {
                param.Files.ForEach(files =>
                {

                    var postStream = new MemoryStream();
                    //读取文件流
                    if (string.IsNullOrWhiteSpace(files.FileName))
                    {
                        files.FileName = Path.GetFileName(files.FilePath);
                    }

                    var fileStream = new FileStream(files.FilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

                    StringBuilder fileFormdataTemplate = new StringBuilder();
                    fileFormdataTemplate.Append("--" + boundary + "\r\n");
                    fileFormdataTemplate.Append("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n");
                    fileFormdataTemplate.Append("Content-Type: application/octet-stream\r\n\r\n");

                    var formHeader = string.Format(fileFormdataTemplate.ToString(), files.Name, files.FileName);

                    //头部边界线
                    var formHeaderBytes = Encoding.UTF8.GetBytes(formHeader);

                    //写入头部边界线
                    postStream.Write(formHeaderBytes, 0, formHeaderBytes.Length);

                    //写入文件内存流
                    if (fileStream != null)
                    {
                        byte[] fileBuffer = new byte[1024];
                        int filebytesRead = 0;
                        while ((filebytesRead = fileStream.Read(fileBuffer, 0, fileBuffer.Length)) != 0)
                        {
                            postStream.Write(fileBuffer, 0, filebytesRead);
                        }
                    }

                    //文件换行
                    var fileLine = Encoding.UTF8.GetBytes("\r\n");
                    postStream.Write(fileLine, 0, fileLine.Length);

                    //写入到总流
                    fileMemoryStream.Write(postStream.ToArray(), 0, postStream.ToArray().Length);

                    postStream.Close();
                    postStream.Dispose();
                });
            }

            //边界线
            var footer = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");
            fileMemoryStream.Write(footer, 0, footer.Length);

            var data = fileMemoryStream.ToArray();
            rq.ContentLength = data.Length;

            //写入流
            using (Stream requestStream = rq.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)rq.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                using (var myStreamReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    return myStreamReader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 异步上传文件
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<string> UploadFileAsync(HttpClientUpload param)
        {
            return await Task.Run(() => { return UploadFile(param); });
        }
        #endregion
    }
}
