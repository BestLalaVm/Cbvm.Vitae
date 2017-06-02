using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LkDataContext;

namespace XMCAServer.Data
{
    public static class LogHelper
    {
        public static void Log(string message, LogType type)
        {
            try
            {
                using (var dataContext = new CVAcademicianDataContext(AppConfig.SqlConnection))
                {
                    dataContext.SystemLog.InsertOnSubmit(new SystemLog
                        {
                            IPAddress = "127.0.0.1",
                            Name = AppConfig.AppVersion,
                            Message = message,
                            LogType = (int) type,
                            LogTime = DateTime.Now
                        });
                    dataContext.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                var directoryPath = "Log";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                string filePath = String.Format(directoryPath + "/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log");
                using (
                    var stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                                                FileShare.ReadWrite))
                {
                    var logMsg = String.Format("{0} Exception:{1}", message, ex.ToString());
                    var buffers = Encoding.UTF8.GetBytes(logMsg);
                    stream.Write(buffers, 0, buffers.Count());
                    stream.Flush();
                }
            }

        }
    }
}
