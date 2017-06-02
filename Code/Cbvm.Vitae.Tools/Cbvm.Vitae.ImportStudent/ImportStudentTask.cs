using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace Cbvm.Vitae.ImportStudent
{
    public class ImportStudentTask : TaskBase.TaskBase
    {
        protected override string TaskName
        {
            get
            {
                return "Cbvm.Vitae.ImportStudent";
            }
        }

        protected override void ExecuteRunning()
        {
            ExportDatas2Local();

            DownloadExportedStudents();

            ImportDatas2Table();
        }

        private void DownloadExportedStudents()
        {
            var sourceDirPath = String.Format(@"{1}\view_student_jxq\{0}", DateTime.Now.ToString("yyyyMMdd"), SourceFilePathRoot);
            WL("Downloading the directory :{0}", sourceDirPath);
            if (Directory.Exists(sourceDirPath))
            {
                var file = new FileInfo(sourceImportedFilePath);
                if (!file.Directory.Exists)
                {
                    file.Directory.Create();
                }

                var dir = new DirectoryInfo(sourceDirPath);
                dir.GetFiles().ToList().ForEach(item =>
                {
                    File.Copy(item.FullName, String.Format(@"{0}\{1}{2}", sourceImportedFilePath, item.Name, item.Extension), true);
                });
            }
            else
            {
                WL("Not directory the file :{0}", sourceDirPath);
            }

            WL("Finish to download the directory :{0}", sourceDirPath);
        }

        private void ExportDatas2Local()
        {
            int pageIndex = 0;
            int pageSize = 20000;
            bool isContinue = true;
            var sourceDirPath = String.Format(@"{1}\view_student_jxq\{0}", DateTime.Now.ToString("yyyyMMdd"), SourceFilePathRoot);

            while (isContinue)
            {
                var scores = new List<StudentInfo>();

                try
                {
                    var startFrom = pageIndex * pageSize;
                    var startTo = (pageIndex + 1) * pageSize;
                    WL("Begin to export datas from oracle");
                    var sql = string.Format(@"
                SELECT 
                  XH,XM,XBDM,XY,ZJH,ZYDM,ZYMC,XZB,TELNUMBER,RXSJ,JG,SFZX
                FROM (
                SELECT 
                  ROWNUM AS XINDEX, 
                  XH,XM,XBDM,XY,ZJH,ZYDM,ZYMC,XZB,TELNUMBER,RXSJ,JG,SFZX
                FROM view_student_jxq
) A WHERE XINDEX>= {0} AND XINDEX<= {1}
            ", startFrom, startTo);

                    WL("with sql:{0}", sql);

                    isContinue = false;


                    ExtractDatasFromOracle(sql, it =>
                    {
                        WL(it == null ? "NULL" : GetStringEx(it[0]));
                        scores.Add(new StudentInfo
                        {
                            XH = GetStringEx(it[0]),
                            XM = GetStringEx(it[1]),
                            XBDM = GetStringEx(it[2]),
                            XY = GetStringEx(it[3]),
                            ZJH = GetStringEx(it[4]),
                            ZYDM = GetStringEx(it[5]),
                            ZYMC = GetStringEx(it[6]),
                            XZB = GetStringEx(it[7]),
                            TELNUMBER = GetStringEx(it[8]),
                            RXSJ = GetStringEx(it[9]),
                            JG = GetStringEx(it[10]),
                            SFZX = GetStringEx(it[11]),
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

                var path = String.Format(@"{0}\view_student_jxq{1}.csv", sourceDirPath, pageIndex.ToString().PadLeft(5, '0'));

                WL("Begin to save exported datas to local:{0}", path);

                EnsureDirectory(sourceDirPath);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                File.Create(path).Dispose();

                using (var write = new StreamWriter(path, true))
                {
                    write.WriteLine("\"XH\",\"XM\",\"XBDM\",\"XY\",\"ZJH\",\"ZYDM\",\"ZYMC\",\"XZB\",\"TELNUMBER\",\"RXSJ\",\"JG\",\"SFZX\"");
                    foreach (var item in scores)
                    {
                        write.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\"", item.XH, item.XM, item.XBDM, item.XY, item.ZJH, item.ZYDM, item.ZYMC, item.XZB, item.TELNUMBER, item.RXSJ, item.JG, item.SFZX));
                    }

                    write.Flush();
                }

                WL("Done to save exported datas to local");

                pageIndex++;
            }
        }
        private int lineNum = 0;
        private void ImportDatas2Table()
        {
            WL("Extracting the directory :{0}", sourceImportedFilePath);
            var dir = new DirectoryInfo(sourceImportedFilePath);
            if (dir.Exists)
            {
                dir.GetFiles().Where(it => it.Name.Contains("view_student_jxq")).ToList().ForEach(item =>
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

                    while (!reader.EndOfStream)
                    {
                        var lines = reader.ReadLine().Split(',').Select(it => it.Trim('"')).ToArray();

                        var student = new StudentInfo
                        {
                            XH = lines[0],
                            XM = lines[1],
                            XBDM = lines[2],
                            XY = lines[3],
                            ZJH = lines[4],
                            ZYDM = lines[5],
                            ZYMC = lines[6],
                            XZB = lines[7],
                            TELNUMBER = lines[8],
                            RXSJ = lines[9],
                            JG = lines[10],
                            SFZX = lines[11]
                        };

                        lineNum++;
                        WL("StudentInfo:{0} LineNum:{1}", student.ToString(), lineNum.ToString());

                        if (string.IsNullOrEmpty(student.XY) || (("1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z").Split(',').Any(ix => student.XY.ToLower().Contains(ix))) ||
                            (("1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z").Split(',').Any(ix => student.ZYMC.ToLower().Contains(ix)) && (student.ZYMC ?? "").Length < 3))
                        {
                            using (var write = new StreamWriter(String.Format("Warming{0}.txt", DateTime.Now.ToString("yyyyMMdd")), true))
                            {
                                write.WriteLine("StudentInfo:{0}, ZYMC:{1}", student.ToString(), student.ZYMC);
                                write.Flush();
                            }

                            continue;
                        }

                        using (var connection = new SqlConnection(CVAcademicianDbConnectionString))
                        {
                            connection.Open();

                            string collegeCode = null;
                            SqlCommand command = null;
                            if (!String.IsNullOrEmpty(student.XY) && student.XY.Trim().Length > 0)
                            {
                                command = new SqlCommand(
                                     String.Format(@"SELECT Code FROM College WHERE NAME ='{0}'", student.XY), connection);
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
                               ", collegeCode, student.XY, universityCode), connection);
                                    command.ExecuteNonQuery();

                                    WL("XYDM:{0}-{1}-{2}", collegeCode, student.XY, command.CommandText);
                                }
                            }
                            

                            if (!String.IsNullOrEmpty(student.ZYDM))
                            {
                                command = new SqlCommand(
                                String.Format(@"
                               IF NOT EXISTS(SELECT 1 FROM Marjor WHERE Code='{0}')
                               BEGIN
                                 INSERT INTO [Marjor]
                                   ([Code]
                                   ,[Name]
                                   ,[Description]
                                   ,[UniversityCode])
                                 VALUES
                                 ('{0}','{1}','{1}','{2}')
                               END
                               UPDATE Marjor
                                  SET Name='{1}'
                                WHERE CODE='{0}'
                            ", student.ZYDM, student.ZYMC, universityCode), connection);
                                command.ExecuteNonQuery();

                                WL("ZYDM:{0} -{1}-{2}", student.ZYDM, student.ZYMC, command.CommandText);
                            }


                            command = new SqlCommand(
                              String.Format(@"
                                IF NOT EXISTS(SELECT 1 FROM STUDENT A WHERE A.StudentNum='{0}')
                                BEGIN
                                    INSERT INTO [Student]
                                       ([StudentNum]
                                       ,[NameZh]
                                       ,[Sex]
                                       ,[IDentityNum]
                                       ,[Email]
                                       ,[Telephone]
                                       ,[Politics]
                                       ,[Class]
                                       ,[Period]
                                       ,[IsMarried]
                                       ,[IsOnline]
                                       ,[IsDelete]
                                       ,[NativePlace]
                                       ,[CollegeCode]
                                       ,[MarjorCode]
                                       ,PASSWORD
                                       ,Birthday)
                                    VALUES(
                                        '{0}',
                                        '{1}',
                                        {2},
                                        '{3}',
                                        ' ',
                                        '{4}',
                                        0,
                                        '{5}',
                                        '{10}',
                                         0,
                                         1,
                                         0,
                                        '{6}',
                                        {7},
                                        '{8}',
                                        '81dc9bdb52d04dc20036dbd8313ed055',
                                        {9})
                                END

                                UPDATE [Student]
                                   SET Class='{5}',
                                       CollegeCode={7},
                                       MarjorCode='{8}',
                                      Period=(CASE WHEN LEN(LTRIM(RTRIM(Period))) = 0 THEN '{10}' ELSE Period END )
                                WHERE  StudentNum='{0}'
                            ",
                             student.XH,
                             student.XM,
                             (student.XBDM != "1" ? 0 : 1),
                             student.ZJH,
                            student.TELNUMBER,
                            student.XZB,
                            student.JG,
                            collegeCode == null ? "NULL" : "'" + collegeCode + "'",
                            student.ZYDM,
                            String.IsNullOrEmpty(student.Birthdate) ? "NULL" : "'" + student.Birthdate + "'",
                            student.Period
                            ), connection);

                            WL("XH:{0} XBDM:{1}, CommandText:{2}", student.XH, student.XBDM, command.CommandText);
                            command.ExecuteNonQuery();
                           

                            command = new SqlCommand(String.Format(@"
                             IF NOT EXISTS(SELECT 1 FROM StudentFamilyAccount A WHERE A.StudentNum='{0}')
                             BEGIN
                               INSERT INTO StudentFamilyAccount
                                           ([StudentNum]
                                           ,[UserName]
                                           ,[Password]
                                           ,[Sex]
                                           ,[IsOnline]
                                           ,[CreateTime])
                                SELECT 
                                  [StudentNum],
                                  [StudentNum]+'01',
                                  '81dc9bdb52d04dc20036dbd8313ed055',
                                  1,
                                  1,
                                  GETDATE()
                                FROM Student WHERE StudentNum='{0}'
                            END 
                            ",student.XH), connection);
                            WL(command.CommandText);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                WL("Done to extract the data from file:{0}", item.FullName);
            });
            }

            WL("Extracted the directory :{0}", sourceImportedFilePath);
        }

        private struct StudentInfo
        {
            /// <summary>
            /// 学号
            /// </summary>
            public string XH { get; set; }

            /// <summary>
            /// 学名
            /// </summary>
            public string XM { get; set; }

            /// <summary>
            /// 性别代码
            /// </summary>
            public string XBDM { get; set; }

            /// <summary>
            /// 学院
            /// </summary>
            public string XY { get; set; }

            /// <summary>
            /// 证件号
            /// </summary>
            public string ZJH { get; set; }

            /// <summary>
            /// 专业代码
            /// </summary>
            public string ZYDM { get; set; }

            /// <summary>
            /// 专业名称
            /// </summary>
            public string ZYMC { get; set; }

            /// <summary>
            /// 班级
            /// </summary>
            public string XZB { get; set; }

            /// <summary>
            /// 手机
            /// </summary>
            public string TELNUMBER { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string RXSJ { get; set; }

            /// <summary>
            /// 籍贯
            /// </summary>
            public string JG { get; set; }

            public string SFZX { get; set; }

            public override string ToString()
            {
                return String.Format("XH:{0} XM:{1} XY:{2}", XH, XM, XY);
            }

            public string Birthdate
            {
                get
                {
                    if (String.IsNullOrEmpty(ZJH))
                    {
                        return null;
                    }

                    //350623198702142315 '350821850610363',

                    try
                    {
                        var index = 0;
                        if (ZJH.Length < 18)
                        {
                            index = 1;
                        }
                        var year = int.Parse(ZJH.Substring(6 - index, 4));
                        var month = int.Parse(ZJH.Substring(10 - index, 2));
                        var day = int.Parse(ZJH.Substring(12 - index, 2));

                        return (new DateTime(year, month, day)).ToString("yyyy-MM-dd");
                    }
                    catch { }

                    return null;
                }
            }

            public string Period
            {
                get
                {
                    if (String.IsNullOrEmpty(XH) || !(SFZX??"").Contains("是"))
                    {
                        return " ";
                    }

                    if (XH.StartsWith("20"))
                    {
                        return XH.Substring(0, 4);
                    }

                    return String.Format("20{0}", XH.Substring(0, 2));
                }
            }
        }
    }
}
