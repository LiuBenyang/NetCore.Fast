
using System.Collections.Generic;

namespace NetCore.Fast.Utility.HttpHelper
{
    /// <summary>
    /// 模拟 html  form 表单提交
    /// </summary>
    public class HttpClientUpload : HttpClientContent
    {

        /// <summary>
        /// 需要上传文件
        /// </summary>
        public List<HttpClientUploadItem> Files { get; set; }
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    public class HttpClientUploadItem
    {
        /// <summary>
        /// 媒体Id name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件路径 本地文件
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public HttpClientUploadItem()
        {

        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filePath">本地文件路径</param>
        /// <param name="name">媒体名称默认 file</param>
        /// <param name="fileName">文件名称 默认取本地名称</param>
        public HttpClientUploadItem(string filePath, string name = "file", string fileName = "")
        {
            this.FilePath = filePath;
            this.Name = name;
        }
    }
}
