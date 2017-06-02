using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Cbvm.Vitae.ProcessMails
{
    public class ProcessMailTask : TaskBase.TaskBase
    {
        protected override string TaskName
        {
            get { return "Cbvm.Vitae.ProcessMails"; }
        }

        protected override void ExecuteRunning()
        {
            using (var connection1 = new SqlConnection(CVAcademicianDbConnectionString))
            {
                connection1.Open();

                var command1 = new SqlCommand(@"
                    SELECT 
                      JobFareId,
                      CollegeCode
                    FROM dbo.JobFairManageCollege WHERE IsSentMail=0
                ", connection1);

                var reader = command1.ExecuteReader();

                while (reader.Read())
                {
                    var jobFareId = reader.GetValue(0).ToString();
                    var collegeCode = reader.GetString(1);

                    SendMail(jobFareId, collegeCode);
                }
            }
            
        }

        private void SendMail(string jobFareId,string collegeCode)
        {
            using (var connection = new SqlConnection(CVAcademicianDbConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(String.Format(@"
                 begin tran tran1
                    INSERT INTO [MessageBox]
                       ([Subject]
                       ,[Content]
                       ,[CreateTime]
                       ,[ReceiverType]
                       ,[ReceiverKey]
                       ,[SenderType]
                       ,[SenderKey]
                       ,[IsReaded])
                    SELECT 
                      Name, 
                      Name+'<BR/>时间:'+cast(BeginTime as nvarchar)+' <br/>地址:'+isnull(A.Address,' '),
                      GETDATE(),
                      0,
                      c.StudentNum,
                      9,
                      D.UserName,
                      0
                    FROM JobFairManage A JOIN JobFairManageCollege B ON A.ID=B.JobFareId 
                         JOIN Student C ON C.CollegeCode=B.CollegeCode
                         JOIN UniversityAdmin D ON D.UniversityCode=A.UniversityCode 
                    WHERE C.CollegeCode='{0}' and a.id={1} AND
                          EXISTS(SELECT 1 FROM JobFairManageCollege T1 WHERE T1.JobFareId=A.ID AND T1.IsSentMail=0 AND T1.CollegeCode=B.CollegeCode)

                    INSERT INTO [MessageBox]
                               ([Subject]
                               ,[Content]
                               ,[CreateTime]
                               ,[ReceiverType]
                               ,[ReceiverKey]
                               ,[SenderType]
                               ,[SenderKey]
                               ,[IsReaded])
                    SELECT 
                      Name, 
                      Name+'<BR/>时间:'+cast(BeginTime as nvarchar)+' <br/>地址:'+isnull(A.Address,' '),
                      GETDATE(),
                      8,
                      c.UserName,
                      9,
                      D.UserName,
                      0
                    FROM JobFairManage A JOIN JobFairManageCollege B ON A.ID=B.JobFareId 
                         JOIN CollegeAdmin C ON C.CollegeCode=B.CollegeCode  
                         JOIN UniversityAdmin D ON D.UniversityCode=A.UniversityCode 
                    WHERE C.CollegeCode='{0}' and a.id={1} AND 
                          EXISTS(SELECT 1 FROM JobFairManageCollege T1 WHERE T1.JobFareId=A.ID AND T1.IsSentMail=0 AND T1.CollegeCode=B.CollegeCode)

                    UPDATE JobFairManageCollege
                       SET IsSentMail=1, SentMailTime=GETDATE()
                      WHERE  JobFareId={1}
                commit tran tran1
            ", collegeCode, jobFareId), connection);

                WL(command.CommandText);

                command.ExecuteNonQuery();
            }
        }
    }
}
