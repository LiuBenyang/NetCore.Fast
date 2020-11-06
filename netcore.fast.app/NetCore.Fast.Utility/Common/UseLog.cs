using NLog;
using System;
using System.Threading.Tasks;

namespace NetCore.Fast.Utility.Common
{
    /// <summary>
    /// 日志写入
    /// </summary>
    public class UseLog
    {
        /// <summary>
        /// 日志操作对象
        /// </summary>
        readonly static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 基础日志写入
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="busType"></param>
        /// <param name="exception"></param>

        static void BaseLog(LogLevel level, string message, string busType = "DEBUG", Exception exception = null)
        {
            var logEvent = new LogEventInfo();
            logEvent.Level = level;
            logEvent.LoggerName = "";
            logEvent.Properties["busType"] = busType;
            logEvent.Message = message;
            logEvent.Exception = exception;
            logger.Log(logEvent);
        }

        /// <summary>
        /// 调试信息  
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Debug(string message)
        {
            BaseLog(LogLevel.Debug, message, "DEBUG", null);
        }

        /// <summary>
        /// 调试信息  
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        public static void Debug(string message, string busType)
        {
            BaseLog(LogLevel.Debug, message, busType, null);
        }

        /// <summary>
        /// 调试信息  
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">捕获错误信息</param>
        public static void Debug(string message, Exception exception)
        {
            BaseLog(LogLevel.Debug, message, "DEBUG", exception);
        }

        /// <summary>
        /// 调试信息  
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        /// <param name="exception">捕获错误信息</param>
        public static void Debug(string message, string busType, Exception exception)
        {
            BaseLog(LogLevel.Debug, message, busType, exception);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void DebugAsync(string message)
        {
            Task.Run(() => { Debug(message); });
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        public static void DebugAsync(string message, string busType)
        {
            Task.Run(() => { Debug(message, busType); });
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">捕获错误信息</param>
        public static void DebugAsync(string message, Exception exception = null)
        {
            Task.Run(() => { Debug(message, exception); });
        }



        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        /// <param name="exception">捕获错误信息</param>
        public static void DebugAsync(string message, string busType = "DEBUG", Exception exception = null)
        {
            Task.Run(() => { Debug(message, busType, exception); });
        }



        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Error(string message)
        {
            BaseLog(LogLevel.Error, message, "ERROR", null);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        public static void Error(string message, string busType)
        {
            BaseLog(LogLevel.Error, message, busType, null);
        }


        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">捕获错误信息</param>
        public static void Error(string message, Exception exception)
        {
            BaseLog(LogLevel.Error, message, "ERROR", exception);
        }


        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        /// <param name="exception">捕获错误信息</param>
        public static void Error(string message, string busType, Exception exception)
        {
            BaseLog(LogLevel.Error, message, busType, exception);
        }


        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void ErrorAsync(string message)
        {
            Task.Run(() => { Error(message); });
        }


        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        public static void ErrorAsync(string message, string busType)
        {
            Task.Run(() => { Error(message, busType); });
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">捕获错误信息</param>
        public static void ErrorAsync(string message, Exception exception = null)
        {
            Task.Run(() => { Error(message, exception); });
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        /// <param name="exception">捕获错误信息</param>
        public static void ErrorAsync(string message, string busType, Exception exception)
        {
            Task.Run(() => { Error(message, busType, exception); });
        }


        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Info(string message)
        {
            BaseLog(LogLevel.Info, message, "INFO", null);
        }

        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        public static void Info(string message, string busType)
        {
            BaseLog(LogLevel.Info, message, busType);
        }

        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">捕获错误信息</param>
        public static void Info(string message, Exception exception = null)
        {
            BaseLog(LogLevel.Info, message, "INFO", exception);
        }

        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        /// <param name="exception">捕获错误信息</param>
        public static void Info(string message, string busType, Exception exception)
        {
            BaseLog(LogLevel.Info, message, busType, exception);
        }


        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void InfoAsync(string message)
        {
            Task.Run(() => { Info(message); });
        }


        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        public static void InfoAsync(string message, string busType)
        {
            Task.Run(() => { Info(message, busType); });
        }


        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">捕获错误信息</param>
        public static void InfoAsync(string message, Exception exception = null)
        {
            Task.Run(() => { Info(message, exception); });
        }


        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        /// <param name="exception">捕获错误信息</param>
        public static void InfoAsync(string message, string busType, Exception exception)
        {
            Task.Run(() => { Info(message, busType, exception); });
        }

        /// <summary>
        /// 警告消息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Warn(string message)
        {
            BaseLog(LogLevel.Warn, message, "WARN", null);
        }

        /// <summary>
        /// 警告消息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        public static void Warn(string message, string busType)
        {
            BaseLog(LogLevel.Warn, message, busType, null);
        }

        /// <summary>
        /// 警告消息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">捕获错误信息</param>
        public static void Warn(string message, Exception exception = null)
        {
            BaseLog(LogLevel.Warn, message, "WARN", exception);
        }

        /// <summary>
        /// 警告消息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        /// <param name="exception">捕获错误信息</param>
        public static void Warn(string message, string busType, Exception exception)
        {
            BaseLog(LogLevel.Warn, message, busType, exception);
        }

        /// <summary>
        /// 警告消息
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void WarnAsync(string message)
        {
            Task.Run(() => { Warn(message); });
        }

        /// <summary>
        /// 警告消息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        public static void WarnAsync(string message, string busType)
        {
            Task.Run(() => { Warn(message, busType); });
        }

        /// <summary>
        /// 警告消息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="exception">捕获错误信息</param>
        public static void WarnAsync(string message, Exception exception = null)
        {
            Task.Run(() => { Warn(message, exception); });
        }

        /// <summary>
        /// 警告消息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="busType">业务类型</param>
        /// <param name="exception">捕获错误信息</param>
        public static void WarnAsync(string message, string busType, Exception exception)
        {
            Task.Run(() => { Warn(message, busType, exception); });
        }
    }
}
