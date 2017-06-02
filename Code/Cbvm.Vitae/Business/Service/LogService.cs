using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LkDataContext;
using Presentation.Enum;
using WebLibrary;

namespace Business.Service
{
    public class LogService
    {
        public static void Log(Exception exception, string ipAddress, string title)
        {
            using (var dataContext = new CVAcademicianDataContext())
            {
                dataContext.SystemLog.InsertOnSubmit(new SystemLog()
                {
                    IPAddress = ipAddress,
                    LogTime = DateTime.Now,
                    LogType = (int) LogType.Error,
                    Name = String.Format("Xmut V1 Occur errors:{0}", title),
                    Message = exception.ToString()
                });
                dataContext.SubmitChanges();
            }

            LogMailQueue(
                String.Format(@"<Message><IPaddress>{0}</IPaddress><Exception>{1}</Exception></Message>", ipAddress,
                    exception), title);
        }

        public static void LogMailQueue(string message,string title)
        {
            using (var dataContext = new CVAcademicianDataContext())
            {
                dataContext.MailQueue.InsertOnSubmit(new MailQueue()
                {
                    Name = String.Format("Xmut V1 Occur errors:{0}", title),
                    CreateTime = DateTime.Now,
                    IsSended = false,
                    Receiver = WebUiConfig.ErrorEmail,
                    ReceiverName = WebUiConfig.ErrorEmail,
                    Message = message
                });

                dataContext.SubmitChanges();
            }
        }
    }
}
