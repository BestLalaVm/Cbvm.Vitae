using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Management;
using LkDataContext;
using Presentation.Enum;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;

namespace WebLibrary.Helper
{
    public static class EmailTemplateHelper
    {
        //public static EmailTemplate GetEmailTemplate(SystemEmailType emailType)
        //{
        //    using (var dataContext = new CVAcademicianDataContext())
        //    {
        //        return dataContext.EmailTemplates.FirstOrDefault(it => it.EmailType == (int)emailType);
        //    }
        //}

        //public static EnterpriseEmailTemplate GetEnterpriseEmailTemplate(EnterpriseEmailType emailType)
        //{
        //    using (var dataContext = new CVAcademicianDataContext())
        //    {
        //        return dataContext.EnterpriseEmailTemplate.FirstOrDefault(it => it.EmailType == (int)emailType);
        //    }
        //}

        //public static string MailSender
        //{
        //    get
        //    {
        //        return AppConfig.MailSender;
        //    }
        //}

        //public static string ResetPasswordMailContentTemplate
        //{
        //    get
        //    {
        //        return "<div>请用以下帐号/密码重新登入:</div>" +
        //                              "<div>帐号:{0}</div>" +
        //                              "<div>密码:{1}</div>";
        //    }
        //}

        //public static string ResetPasswordMailSubjectTemplate
        //{
        //    get
        //    {
        //        return "厦门理工学院履历系统-找回密码";
        //    }
        //}

        //private static IDictionary<EnterpriseEmailType, string> EmailTemplateBodyDictory =new Dictionary<EnterpriseEmailType, string>();
        //public static string GetEmailTemplateBody(EnterpriseEmailType emailType)
        //{
        //    try
        //    {
        //        if (!EmailTemplateBodyDictory.ContainsKey(emailType))
        //        {
        //            using (System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Server.MapPath(string.Format("~/App_Data/DefaultTemplate/{0}_Body.html", emailType.ToString()))))
        //            {
        //                EmailTemplateBodyDictory.Add(emailType, reader.ReadToEnd());
        //            }
        //        }
        //        return EmailTemplateBodyDictory[emailType];
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }

        //}

        //private static IDictionary<EnterpriseEmailType, string> EmailTemplateSubjectDictory = new Dictionary<EnterpriseEmailType, string>();
        //public static string GetEmailTemplateSubject(EnterpriseEmailType emailType)
        //{
        //    try
        //    {
        //        if (!EmailTemplateSubjectDictory.ContainsKey(emailType))
        //        {
        //            using (System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Server.MapPath(string.Format("~/App_Data/DefaultTemplate/{0}_Subject.html", emailType.ToString()))))
        //            {
        //                EmailTemplateSubjectDictory.Add(emailType, reader.ReadToEnd());
        //            }
        //        }
        //        return EmailTemplateSubjectDictory[emailType];
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }
        //}

        //public static bool InitialEnterpriseEmailTemplate(string enterpriseCode, string email)
        //{
        //    AddEnterpriseEmailTemplate(EnterpriseEmailType.InviteToAudition, enterpriseCode, email);
        //    AddEnterpriseEmailTemplate(EnterpriseEmailType.Email2Superviser, enterpriseCode, email);
        //    AddEnterpriseEmailTemplate(EnterpriseEmailType.InviteToAudition, enterpriseCode, email);
        //    return true;
        //}

        //public static bool AddEnterpriseEmailTemplate(EnterpriseEmailType emailType, string enterpriseCode, string email)
        //{
        //    using (CVAcademicianDataContext dataContext = new CVAcademicianDataContext())
        //    {
        //        dataContext.EnterpriseEmailTemplate.InsertOnSubmit(new EnterpriseEmailTemplate
        //        {
        //            Body = EmailTemplateHelper.GetEmailTemplateBody(EnterpriseEmailType.InviteToAudition),
        //            Subject = EmailTemplateHelper.GetEmailTemplateSubject(EnterpriseEmailType.InviteToAudition),
        //            Cc = email,
        //            Sender = email,
        //            SenderName = email,
        //            EmailType = (int)EnterpriseEmailType.InviteToAudition,
        //            EnterpriseCode = enterpriseCode
        //        });
        //        dataContext.SubmitChanges();
        //    }
        //    return true;
        //}


        //private static IDictionary<SystemEmailType, string> SystemEmailTemplateBodyDictory = new Dictionary<SystemEmailType, string>();
        //public static string GetEmailTemplateBody(SystemEmailType emailType)
        //{
        //    try
        //    {
        //        if (!SystemEmailTemplateBodyDictory.ContainsKey(emailType))
        //        {
        //            using (System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Server.MapPath(string.Format("~/App_Data/DefaultTemplate/{0}_Body.html", emailType.ToString()))))
        //            {
        //                SystemEmailTemplateBodyDictory.Add(emailType, reader.ReadToEnd());
        //            }
        //        }
        //        return SystemEmailTemplateBodyDictory[emailType];
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }

        //}

        //private static IDictionary<SystemEmailType, string> SystemEmailTemplateSubjectDictory = new Dictionary<SystemEmailType, string>();
        //public static string GetEmailTemplateSubject(SystemEmailType emailType)
        //{
        //    try
        //    {
        //        if (!SystemEmailTemplateSubjectDictory.ContainsKey(emailType))
        //        {
        //            string filePath =
        //                HttpContext.Current.Server.MapPath(string.Format("~/App_Data/DefaultTemplate/{0}_Subject.html",
        //                    emailType.ToString()));
        //            SystemEmailTemplateSubjectDictory.Add(emailType, GetContent(filePath));
        //        }
        //        return SystemEmailTemplateSubjectDictory[emailType];
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }
        //}

        //private static string GetContent(string filePath)
        //{
        //    using (System.IO.StreamReader reader = new System.IO.StreamReader(filePath))
        //    {
        //        return reader.ReadToEnd();
        //    }
        //}

        private const string KeyFormatter = "{0}.{1}";

        private static IDictionary<string, Dictionary<EmailContentType, string>> EmailTemplateDictory = new Dictionary<string, Dictionary<EmailContentType, string>>();

        static EmailTemplateHelper()
        {
            var defaultTemplateDictory =
                new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath("~/App_Data/DefaultTemplate"));
            foreach (var typeDictory in defaultTemplateDictory.GetDirectories())
            {
                foreach (var detailDictory in typeDictory.GetDirectories())
                {
                    var key = String.Format(KeyFormatter, typeDictory.Name, detailDictory.Name);
                    var files = detailDictory.GetFiles();
                    var fileDirectory = new Dictionary<EmailContentType, string>();
                    foreach (var fileInfo in files)
                    {
                        if (Path.GetFileNameWithoutExtension(fileInfo.Name) == "Subject")
                        {
                            fileDirectory.Add(EmailContentType.Subject, GetFileContent(fileInfo));
                        }
                        else
                        {
                            fileDirectory.Add(EmailContentType.Body, GetFileContent(fileInfo));
                        }
                    }
                    if (fileDirectory.Any())
                    {
                        EmailTemplateDictory.Add(key, fileDirectory);
                    }
                }
            }
        }

        private static string GetFileContent(FileInfo fileInfo)
        {
            using (Stream reader = fileInfo.OpenRead())
            {
                var buff = new byte[reader.Length];
                reader.Read(buff, 0, buff.Length);
                return Encoding.Default.GetString(buff);
            }
        }

        public static string GetEmailTemplateSubject(Enum eEnum)
        {
            string key = String.Format(KeyFormatter, "System", eEnum.ToString());
            if (eEnum is EnterpriseEmailType)
            {
                key = String.Format(KeyFormatter, "Enterprise", eEnum.ToString());

            }
            if (EmailTemplateDictory.ContainsKey(key))
            {
                return EmailTemplateDictory[key][EmailContentType.Subject];
            }
            return "";
        }

        public static string GetEmailTemplateBody(Enum eEnum)
        {
            string key = String.Format(KeyFormatter, "System", eEnum.ToString());
            if (eEnum is EnterpriseEmailType)
            {
                key = String.Format(KeyFormatter, "Enterprise", eEnum.ToString());

            }
            if (EmailTemplateDictory.ContainsKey(key))
            {
                return EmailTemplateDictory[key][EmailContentType.Body];
            }
            return "";
        }

        public static string MailSender = "";

        public static EnterpriseEmailTemplate GetEnterpriseEmailTemplate(string enterpriseCode,
            EnterpriseEmailType emailType)
        {
            using (var dataContext = new CVAcademicianDataContext())
            {
                var enterprise = dataContext.Enterprise.FirstOrDefault(ix => ix.Code == enterpriseCode);
                if (enterprise == null) return null;
                return AddEnterpriseEmailTemplate(enterprise, emailType);
            }
        }

        public static void InitialEnterpriseEmailTemplate(Enterprise enterprise)
        {
            AddEnterpriseEmailTemplate(enterprise, EnterpriseEmailType.View);
            AddEnterpriseEmailTemplate(enterprise, EnterpriseEmailType.Passed);
            AddEnterpriseEmailTemplate(enterprise, EnterpriseEmailType.Request);
            AddEnterpriseEmailTemplate(enterprise, EnterpriseEmailType.Invited);
            AddEnterpriseEmailTemplate(enterprise, EnterpriseEmailType.NoPassed);
        }

        private static EnterpriseEmailTemplate AddEnterpriseEmailTemplate(Enterprise enterprise, EnterpriseEmailType emailType)
        {
            var emailTemplate = enterprise.EnterpriseEmailTemplate.FirstOrDefault(ix => ix.EmailType == (int) emailType);
            if (emailTemplate == null)
            {
                emailTemplate = new EnterpriseEmailTemplate()
                {
                    Sender = enterprise.Email,
                    Subject = GetEmailTemplateSubject(emailType),
                    Body = GetEmailTemplateBody(emailType),
                    Cc = enterprise.Email,
                    EmailType = (int) emailType,
                    SenderName = String.Format("{0}<{1}>", enterprise.Name, enterprise.Email)
                };
                enterprise.EnterpriseEmailTemplate.Add(emailTemplate);
            }
            return emailTemplate;
        }

        public static void NotifyEnterpriseJobStatusEmail(EnterpriseJobRequester jobRequester,EnterpriseEmailType emailType,CVAcademicianDataContext dataContext,string teacherNum=null)
        {
            //Sometime, The jobRequest is new created object without submitted.
            var enterpriseJob = dataContext.EnterpriseJob.FirstOrDefault(ix => ix.Code == jobRequester.JobCode);
            if (enterpriseJob == null) return;

            var enterprise = enterpriseJob.Enterprise;
            var enterpriseTemplate = AddEnterpriseEmailTemplate(enterprise, emailType);
            var student = dataContext.Student.FirstOrDefault(ix => ix.StudentNum == jobRequester.StudentNum);
            if (enterpriseTemplate != null)
            {
                Teacher teacher = null;
                if (!String.IsNullOrEmpty(teacherNum))
                {
                    teacher = dataContext.Teacher.FirstOrDefault(ix => ix.TeacherNum == teacherNum);
                }
                var subject = FormatteString(enterpriseTemplate.Subject, jobRequester, teacher, student,enterpriseJob);
                var body = FormatteString(enterpriseTemplate.Body, jobRequester, teacher, student, enterpriseJob);
                dataContext.MessageBox.InsertOnSubmit(new MessageBox()
                {
                    ReceiverKey = jobRequester.StudentNum,
                    ReceiverType = (int) UserType.Student,
                    SenderKey = enterprise.Code,
                    SenderType = (int) UserType.Enterprise,
                    CreateTime = DateTime.Now,
                    Subject = subject,
                    Content = body
                });

               
                if (!String.IsNullOrEmpty(student.Email))
                {
                    dataContext.MailQueue.InsertOnSubmit(new MailQueue()
                    {
                        Sender = enterprise.Email,
                        Receiver = student.Email,
                        CreateTime = DateTime.Now,
                        Name = subject,
                        Message = body
                    });
                }
            }
        }

        public static string FormatterResetPasswordContent(string name, string userName, string password,
            string formattedContent)
        {
            if (String.IsNullOrEmpty(formattedContent)) return "";
            return formattedContent.Replace("$$Name$$", name)
                .Replace("$$UserName$$", userName)
                .Replace("$$Password$$", password);
        }


        private static string FormatteString(string content, EnterpriseJobRequester jobRequest, Teacher teacher,
            Student student, EnterpriseJob job)
        {
            content = content.Replace("$$EnterpriseName$$", job.Enterprise.Name).Replace("$$JobName$$", job.Name)
                .Replace("$$ContactTelephone$$", job.Telephone)
                .Replace("$$EnterpriseLink$$",
                    UrlRuleHelper.GenerateUrl(job.EnterpriseCode, "", RulePathType.Enterprise));
            if (jobRequest.JobRequestRecruitStage.Any())
            {
                content = content.Replace("$$StageNote$$",
                    jobRequest.JobRequestRecruitStage.OrderByDescending(ix => ix.ID).FirstOrDefault().Description);
            }
            else
            {
                content = content.Replace("$$StageNote$$","");
            }
            if (teacher != null)
            {
                content = content.Replace("$$TeacherName$$", teacher.NameZh);
            }
            else {
                content = content.Replace("$$TeacherName$$", "");
            }
            if (student != null)
            {
                content = content.Replace("$$StudentName$$", student.NameZh);
            }
            else
            {
                content = content.Replace("$$StudentName$$", "");
            }

            content = content.Replace("$$CreateTime$$", DateTime.Now.ToString());

            return content;
        }

        public static EnterpriseEmailType? TranslateToEnterpriseEmailType(int recruitFlowId)
        {
            using (var dataContext = new CVAcademicianDataContext())
            {
                var recruitFlow = dataContext.RecruitFlowSetted.FirstOrDefault(ix => ix.ID == recruitFlowId);
                if (recruitFlow == null)
                {
                    return null;
                }
                switch ((RecruitType) recruitFlow.RecruitType)
                {
                    case RecruitType.View:
                        return EnterpriseEmailType.View;
                    case RecruitType.Passed:
                        return EnterpriseEmailType.Passed;
                    case RecruitType.NoPassed:
                        return EnterpriseEmailType.NoPassed;
                    case RecruitType.Invited:
                        return EnterpriseEmailType.Invited;
                    default:
                        return EnterpriseEmailType.Request;
                }
            }
        }
    }

    public enum EmailContentType
    {
        Subject = 0,
        Body = 1
    }
}
