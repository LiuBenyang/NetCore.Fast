namespace NetCore.Fast.Utility.HttpHelper
{
    /// <summary>
    /// 请求POST下载
    /// </summary>
    public class HttpClientDownload : HttpClientContent
    {
        /// <summary>
        /// 本地文件夹目录 如：D:\\
        /// </summary>
        public string LocalFolderPath { get; set; }

        /// <summary>
        /// 本地文件名 如: test.jpg
        /// </summary>
        public string LocalFileName { get; set; }

        /// <summary>
        /// 下载大小 默认 4096
        /// </summary>
        public int Size { get; set; } = 4096;
    }
}
