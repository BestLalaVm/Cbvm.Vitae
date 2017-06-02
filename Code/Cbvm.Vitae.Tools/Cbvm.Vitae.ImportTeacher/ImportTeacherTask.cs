using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Cbvm.Vitae.ImportTeacher
{
    public class ImportTeacherTask : TaskBase.TaskBase
    {
        protected override string TaskName
        {
            get
            {
                return "Cbvm.Vitae.ImportTeacherTask";
            }
        }

        protected override void ExecuteRunning()
        {
            ExportDatas2Local();

            DownloadExportedTeachers();

            ImportDatas2Table();

            GeneratorExistsSQl();
        }

        private void DownloadExportedTeachers()
        {
            var sourceDirPath = String.Format(@"{1}\view_jzg_jxq\{0}", DateTime.Now.ToString("yyyyMMdd"), SourceFilePathRoot);
            WL("Downloading the directory :{0}", sourceDirPath);
            if (Directory.Exists(sourceDirPath))
            {
                var file = new FileInfo(sourceImportedFilePath);
                EnsureDirectory(file.Directory.FullName);
                var dir = new DirectoryInfo(sourceDirPath);
                dir.GetFiles().ToList().Where(it => it.Name.StartsWith("view_jzg_jxq", StringComparison.OrdinalIgnoreCase)).ToList().ForEach(item =>
                {
                    string targetFileName = String.Format(@"{0}\{1}{2}", sourceImportedFilePath, item.Name, item.Extension);
                    WL("Copying the file from {0} to {1}", item.FullName, targetFileName);
                    File.Copy(item.FullName, targetFileName, true);
                });
            }
            else
            {
                WL("Not directory the file :{0}", sourceDirPath);
            }

            WL("Finish to download the directory :{0}", sourceDirPath);
        }

        private void ImportDatas2Table()
        {
            WL("Extracting the file :{0}", sourceImportedFilePath);
            var dir = new DirectoryInfo(sourceImportedFilePath);
            if (dir.Exists)
            {
                dir.GetFiles().Where(it => it.Name.Contains("view_jzg_jxq")).ToList().ForEach(item =>
                {
                    WL("Begin to extract the data from file:{0}", item.FullName);

                    string universityCode = null;
                    int maxCollegeCount = 0;
                    using (var connection = new SqlConnection(CVAcademicianDbConnectionString))
                    {
                        connection.Open();
                        var command = new SqlCommand(@"
                                SELECT TOP 1 Code FROM University
                            ", connection);
                        universityCode = command.ExecuteScalar().ToString();

                        command = new SqlCommand(@"
                                SELECT count(1) FROM College
                            ", connection);

                        maxCollegeCount = (int)command.ExecuteScalar();
                    }

                    using (var reader = new StreamReader(item.FullName))
                    {
                        reader.ReadLine();
                        var lineIndex=0;
                        while (!reader.EndOfStream)
                        {
                            var lines = reader.ReadLine().Split(',').Select(it => it.Trim('"')).ToArray();

                            var teacher = new TeacherInfo
                            {
                                ZGH = lines[0],
                                XM = lines[1],
                                XBM = lines[2],
                                XY = lines[3],
                                SJ = lines[4],
                                EMAIL = lines[5],
                                JZGLBM = lines[6]
                            };
                            lineIndex++;

                            WL("TeacherInfo:{0}, lineNumber:{1}", teacher.ToString(), lineIndex.ToString());

                            if (string.IsNullOrEmpty(teacher.XY) || ("1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z").Split(',').Any(ix => teacher.XY.ToLower().Contains(ix)))
                            {
                                using (var write = new StreamWriter(String.Format("Warming{0}.txt", DateTime.Now.ToString("yyyyMMdd")), true))
                                {
                                    write.WriteLine("TeacherInfo:{0}, XY:{1}", teacher.ToString(), teacher.XY);
                                    write.Flush();
                                }

                                continue;
                            }

                            using (var connection = new SqlConnection(CVAcademicianDbConnectionString))
                            {
                                connection.Open();

                                string collegeCode = null;
                                SqlCommand command = null;
                                if (!String.IsNullOrEmpty(teacher.XY))
                                {
                                    command = new SqlCommand(
                                String.Format(@"SELECT Code FROM College WHERE NAME ='{0}'", teacher.XY), connection);
                                    var collegeResult = command.ExecuteScalar();
                                    if (collegeResult != null)
                                    {
                                        collegeCode = collegeResult.ToString();
                                    }
                                    else
                                    {
                                        collegeCode = GenerateCollegeCode(universityCode, maxCollegeCount);

                                        command = new SqlCommand(
                                        String.Format(@"
                                    IF NOT EXISTS(SELECT 1 FROM College WHERE NAME ='{1}')
                                    BEGIN
                                        INSERT INTO [College]
                                       ([Code]
                                       ,[Name]
                                       ,[Description]
                                       ,[UniversityCode]
                                       ,[CreateTime])
                                        VALUES
                                       ('{0}','{1}','{1}','{2}',GETDATE())
                                    END 
                               ", collegeCode, teacher.XY, universityCode), connection);

                                        command.ExecuteNonQuery();
                                    }
                                }
                               

                                command = new SqlCommand(
                                  String.Format(@"
                                IF NOT EXISTS(SELECT 1 FROM Teacher A WHERE A.TeacherNum='{0}')
                                BEGIN
                                     INSERT INTO [Teacher]
                                           ([TeacherNum]
                                           ,[NameZh]
                                           ,[School]
                                           ,[MarjorName]
                                           ,[IsMarried]
                                           ,[Email]
                                           ,[IsOnline]
                                           ,[IsDelete]
                                           ,[Sex]
                                           ,[CollegeCode]
                                           ,PASSWORD
                                           ,Description)
                                    values(
                                        '{0}',
                                        '{1}',
                                        ' ',
                                        ' ',
                                        0,
                                        '{2}',
                                        1,
                                        0,
                                        {4},
                                        '{3}',
                                        '81dc9bdb52d04dc20036dbd8313ed055',
                                        '{0}-{1}-sync')

                                SELECT '{0}'
                                END

                                UPDATE [Teacher]
                                   SET CollegeCode = '{3}'
                                WHERE  TeacherNum='{0}'
                            ",
                                teacher.ZGH,
                                teacher.XM,
                                teacher.EMAIL,
                                collegeCode,
                                teacher.XBM != "1" ? 0 : 1
                                ), connection);

                                var count = command.ExecuteScalar();

                                WL("ExecuteNonQuery: count:{0}", count==null?"empty": count.ToString());
                            }
                        }
                    }

                    WL("Done to extract the data from file:{0}", item.FullName);

                });
            }

            WL("Extracted the file :{0}", sourceImportedFilePath);
        }

        private void ExportDatas2Local()
        {
            int pageIndex = 0;
            int pageSize = 20000;
            bool isContinue = true;
            var sourceDirPath = String.Format(@"{1}\view_jzg_jxq\{0}", DateTime.Now.ToString("yyyyMMdd"), SourceFilePathRoot);

            while (isContinue)
            {
                var scores = new List<TeacherInfo>();

                try
                {
                    var startFrom = pageIndex * pageSize;
                    var startTo = (pageIndex + 1) * pageSize;
                    WL("Begin to export datas from oracle");
                    var sql = string.Format(@"
                SELECT 
                  ZGH,XM,XBM,YX,SJ,EMAIL,JZGLBM
                FROM (
                SELECT 
                  ROWNUM AS XINDEX, 
                  ZGH,XM,XBM,YX,SJ,EMAIL,JZGLBM
                FROM view_jzg_jxq ) A WHERE XINDEX>= {0} AND XINDEX<= {1}
            ", startFrom, startTo);

                    WL("with sql:{0}", sql);

                    isContinue = false;


                    ExtractDatasFromOracle(sql, it =>
                    {
                        scores.Add(new TeacherInfo
                        {
                            ZGH = it[0] is DBNull ? "" : it.GetString(0),
                            XM = it[1] is DBNull ? "" : it.GetValue(1).ToString(),
                            XBM = it[2] is DBNull ? "" : it.GetValue(2).ToString(),
                            XY = it[3] is DBNull ? "" : it.GetValue(3).ToString(),
                            SJ = it[4] is DBNull ? "" : it.GetValue(4).ToString(),
                            EMAIL = it[5] is DBNull ? "" : it.GetValue(5).ToString(),
                            JZGLBM = it[6] is DBNull ? "" : it.GetValue(6).ToString(),
                        });

                        isContinue = true;
                    });

                    WL("Done to export datas from oracle with count:{0}", scores.Count().ToString());
                }
                catch (Exception ex)
                {
                    WL(ex.ToString());

                    throw ex;
                }

                var path = String.Format(@"{0}\view_jzg_jxq{1}.csv", sourceDirPath, pageIndex.ToString().PadLeft(5, '0'));

                WL("Begin to save exported datas to local:{0}", path);

                EnsureDirectory(sourceDirPath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.Create(path).Dispose();

                using (var write = new StreamWriter(path, true))
                {
                    write.WriteLine("\"ZGH\",\"XM\",\"XBM\",\"YX\",\"SJ\",\"EMAIL\",\"JZGLBM\"");
                    foreach (var item in scores)
                    {
                        write.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{5}\"", item.ZGH, item.XM, item.XBM, item.XY, item.SJ, item.EMAIL, item.JZGLBM));
                    }

                    write.Flush();
                }

                WL("Done to save exported datas to local");

                pageIndex++;
            }
        }


        private void GeneratorExistsSQl()
        {
            using (var connection = new SqlConnection(CVAcademicianDbConnectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"
                                SELECT TeacherNum FROM Teacher
                            ", connection);

                var reader = command.ExecuteReader();

                var list = new List<string>();
                while (reader.Read())
                {
                    var studentNum = reader.GetString(0);

                    list.Add(studentNum);
                }

                using (var write = new StreamWriter("existsData.sql", true))
                {
                    write.Write(String.Join(",", list.Select(it => String.Format("'{0}'", it))));
                    write.Flush();
                }
            }
        }

        private struct TeacherInfo
        {
            /// <summary>
            /// 工号
            /// </summary>
            public string ZGH { get; set; }

            /// <summary>
            /// 学名
            /// </summary>
            public string XM { get; set; }

            public string XBM { get; set; }

            /// <summary>
            /// 学院
            /// </summary>
            public string XY { get; set; }

            /// <summary>
            /// 学籍
            /// </summary>
            public string SJ { get; set; }

            private string _EMAIL;
            /// <summary>
            /// 专业代码
            /// </summary>
            public string EMAIL
            {
                get
                {
                    if (String.IsNullOrEmpty(_EMAIL))
                    {
                        return " ";
                    }

                    return _EMAIL;
                }
                set
                {
                    _EMAIL = value;
                }
            }

            /// <summary>
            /// 专业名称
            /// </summary>
            public string JZGLBM { get; set; }
            

            public override string ToString()
            {
                return String.Format("ZGH:{0} XM:{1}", ZGH, XM);
            }
        }
    }
}
