using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMCAServer.Data
{
    public static class AppConfig
    {
        public static string SqlConnection = System.Configuration.ConfigurationManager.ConnectionStrings["CVAcademician"].ConnectionString;


        public static string MailHostName = System.Configuration.ConfigurationManager.AppSettings["MailHost"];

        public static int MailPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MailPort"]);

        public static string MailUserName = System.Configuration.ConfigurationManager.AppSettings["MailUserName"];

        public static string MailPassword = System.Configuration.ConfigurationManager.AppSettings["MailPassword"];

        /// <summary>
        /// 推荐间隔时间,即:学生开始申请教师推荐之后, 如果在该段时间内老师还没推荐的话,则自动转为简历投递的方式.
        /// </summary>
        public const string JOB_REFERRALS_QUEUE_DURATION = "JOB_REFERRALS_QUEUE_DURATION";

        public const string AppVersion = "Xmut-Service V1";
    }
}
