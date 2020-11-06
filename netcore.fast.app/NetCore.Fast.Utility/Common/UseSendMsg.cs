using NetCore.Fast.Utility.HttpHelper;
using NetCore.Fast.Utility.ToExtensions;

namespace NetCore.Fast.Utility.Common
{
    public class UseSendMsg
    {
        static readonly string ALIYUNURL = "http://120.25.160.53:7000/api/sms";

        /// <summary>
        /// 通过阿里云发送短信
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string SendMsgByAliyun(AliyunMsgParam param)
        {
            HttpClientContent content = new HttpClientContent
            {
                Url = ALIYUNURL,
                Data = param.ToJson(),
                MethodType = HttpMethodType.Post,
                ContentType = "application/json"
            };

            return UseHttpClient.PostString(content);
        }

    }
}
