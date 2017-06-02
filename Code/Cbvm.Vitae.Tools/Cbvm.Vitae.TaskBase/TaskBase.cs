using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.AccessControl;
using System.Data.OracleClient;
using System.Data.SqlClient;

namespace Cbvm.Vitae.TaskBase
{
    public abstract class TaskBase
    {
        protected static string sourceImportedFilePath = String.Format(@"{1}\Downloads\{0}\", DateTime.Now.ToString("yyyyMMdd"), System.IO.Directory.GetCurrentDirectory());

        protected string CVAcademicianDbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CVAcademician"].ConnectionString;

        protected abstract string TaskName { get; }

        public void Run()
        {
            WL(sourceImportedFilePath);
            WL("Begin to run the task:{0}", TaskName);
            try
            {
                ExecuteRunning();
            }
            catch (Exception ex)
            {
                WL(ex.ToString());
            }
            WL("Finish to run the task:{0}", TaskName);
        }

        protected void WL(string msg,params string[] argus)
        {
            Console.WriteLine(String.Format(msg, argus));

            using (var writer = new StreamWriter(String.Format("log{0}.txt", DateTime.Now.ToString("yyyyMMdd")),true))
            {
                writer.WriteLine(String.Format("----Time: {0} Content:{1}----", DateTime.Now, String.Format(msg, argus)));
                writer.Flush();
            }
        }

        protected abstract void ExecuteRunning();

        protected string SourceFilePathRoot
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SourceFilePathRoot"];
            }
        }

        protected void ExtractDatasFromOracle(string sql,Action<OracleDataReader> readAction)
        {
            using (OracleConnection connection = new System.Data.OracleClient.OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString))
            {
                connection.Open();
                OracleCommand command = new OracleCommand(sql, connection);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    readAction(reader);
                }
            }
        }

        protected void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                var sec = new DirectorySecurity();
                FileSystemAccessRule permissions = new FileSystemAccessRule("everyone", FileSystemRights.FullControl, AccessControlType.Allow);
                sec.SetAccessRule(permissions);

                Directory.CreateDirectory(path, sec);
            }
        }

        protected string GetStringEx(object obj)
        {
            if (obj == null || obj is DBNull) return " ";
            
            return obj.ToString();
        }

        protected string GenerateCollegeCode(string universityCode, int maxCollegeCount)
        {
            var collegeCode = String.Format("{0}A{1}", universityCode, (++maxCollegeCount).ToString().PadLeft(3, '0'));

            using (var connection = new SqlConnection(CVAcademicianDbConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(String.Format(@"
                    SELECT 1 FROM College WHERE Code='{0}'
                ", collegeCode), connection);

                var value = command.ExecuteScalar();

                if (value is DBNull || value == null)
                {
                    return collegeCode;
                }
                else
                {
                    return GenerateCollegeCode(universityCode, maxCollegeCount + 1);
                }
            }
        }
    }
}
