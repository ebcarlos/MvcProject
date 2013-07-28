using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Tools
{
    public static class Log
    {
        private static readonly log4net.ILog _log;

        /// <summary>
        /// Static constructor which init the logger object
        /// </summary>
        static Log()
        {
            log4net.Appender.RollingFileAppender oRollingFileAppender = new log4net.Appender.RollingFileAppender()
            {
                AppendToFile = true,
                File = "Logs/log.txt",
                Layout = new log4net.Layout.PatternLayout("[%date{ISO8601}] [%level] %message%newline"),
                LockingModel = new log4net.Appender.RollingFileAppender.MinimalLock(),
                MaximumFileSize = "10MB",
                MaxSizeRollBackups = 20,
                RollingStyle = log4net.Appender.RollingFileAppender.RollingMode.Size,
            };

            oRollingFileAppender.ActivateOptions();

            log4net.Repository.ILoggerRepository oRepository = log4net.LogManager.GetRepository();
            log4net.Core.Level oInfoLevel = oRepository.LevelMap["All"];
            oRepository.LevelMap.Add(oInfoLevel.Name, oInfoLevel.Value);

            log4net.Config.BasicConfigurator.Configure(oRepository, oRollingFileAppender);
            _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        /// <summary>
        /// Write an error in the log file
        /// </summary>
        public static void Error(string msg, Exception e = null)
        {
            _log.Error(msg, e);
        }

        /// <summary>
        /// Write a warning in the log file
        /// </summary>
        public static void Warning(string msg)
        {
            _log.Warn(msg);
        }

        /// <summary>
        /// Write an info in the log file
        /// </summary>
        public static void Info(string msg)
        {
            _log.Info(msg);
        }
    }
}