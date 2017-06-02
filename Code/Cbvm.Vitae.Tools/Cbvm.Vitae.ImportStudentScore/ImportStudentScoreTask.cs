using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;


namespace Cbvm.Vitae.ImportStudentScore
{
    public class ImportStudentScoreTask:TaskBase.TaskBase
    {
        protected override string TaskName
        {
            get { return "ImportStudentScoreTask"; }
        }

        protected override void ExecuteRunning()
        {
            ExportDatas2Local();

            DownloadExportedStudentScores();

            ImportDatas2Table();
        }

        private string _MaxScoreIndexId;
        private string MaxScoreIndexId
        {
            get
            {
                if (string.IsNullOrEmpty(_MaxScoreIndexId))
                {
                    using (var connection = new SqlConnection(CVAcademicianDbConnectionString))
                    {
                        connection.Open();

                        var command = new SqlCommand(@"select MAX(ThirdId)  from StudentScoreNew", connection);

                        var result = command.ExecuteScalar();
                        if (result == null || result is DBNull)
                        {
                            _MaxScoreIndexId = "-1";
                        }
                        else
                        {
                            _MaxScoreIndexId = string.IsNullOrEmpty(result.ToString()) ? "-1" : result.ToString();
                        }
                    }
                }

                return _MaxScoreIndexId;
            }
        }

        private void ExportDatas2Local()
        {
            int pageIndex = 0;
            int pageSize = 20000;
            bool isContinue = true;
            var sourceDirPath = String.Format(@"{1}\view_xscj_jxq\{0}", DateTime.Now.ToString("yyyyMMdd"), SourceFilePathRoot);

            while (isContinue)
            {
                var scores = new List<StudentScoreInfo>();

                try
                {
                    var startFrom = pageIndex * pageSize;
                    var startTo = (pageIndex + 1) * pageSize;
                    WL("Begin to export datas from oracle");
                    var sql = string.Format(@"
                SELECT 
                  XN,XQ,XH,CJ,XF,KCMC,ID
                FROM (
                SELECT 
                  ROWNUM AS XINDEX, 
                  XN,XQ,XH,CJ,XF,KCMC,ID
                FROM view_xscj_jxq where id>={2}) A WHERE XINDEX>= {0} AND XINDEX<= {1}
            ", startFrom, startTo, MaxScoreIndexId);

                    WL("with sql:{0}", sql);

                    isContinue = false;

                   
                    ExtractDatasFromOracle(sql, it =>
                    {
                        scores.Add(new StudentScoreInfo
                        {
                            XN = it[0] is DBNull ? "" : it.GetString(0),
                            XQ = it[1] is DBNull ? "" : it.GetValue(1).ToString(),
                            XH = it[2] is DBNull ? "" : it.GetValue(2).ToString(),
                            CJ = it[3] is DBNull ? "" : it.GetValue(3).ToString(),
                            XF = it[4] is DBNull ? "" : it.GetValue(4).ToString(),
                            KCMC = it[5] is DBNull ? "" : it.GetValue(5).ToString(),
                            ID = it[6] is DBNull?"":it.GetValue(6).ToString()
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

                var path = String.Format(@"{0}\view_xscj_jxq{1}.csv", sourceDirPath, pageIndex.ToString().PadLeft(5, '0'));

                WL("Begin to save exported datas to local:{0}", path);

                EnsureDirectory(sourceDirPath);
                if (!File.Exists(path))
                {
                    File.Create(path).Dispose();
                }
                using (var write = new StreamWriter(path, true))
                {
                    write.WriteLine("\"XN\",\"XQ\",\"XH\",\"CJ\",\"XF\",\"KCMC\",\"ID\"");
                    foreach (var item in scores)
                    {
                        write.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"", item.XN, item.XQ, item.XH, item.CJ, item.XF, item.KCMC,item.ID));
                    }

                    write.Flush();
                }

                WL("Done to save exported datas to local");

                pageIndex++;
            }
        }

        private void DownloadExportedStudentScores()
        {
            var sourceDirPath = String.Format(@"{1}\view_xscj_jxq\{0}", DateTime.Now.ToString("yyyyMMdd"), SourceFilePathRoot);
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

        private void ImportDatas2Table()
        {
            WL("Extracting the directory :{0}", sourceImportedFilePath);
            var dir = new DirectoryInfo(sourceImportedFilePath);
            if (dir.Exists)
            {
                var filesLists = new Dictionary<int, IList<FileInfo>>();
                var fileIndex = 0;
                dir.GetFiles().ToList().ForEach(item =>
                {
                    var finialIndex = fileIndex / 10;

                    if (fileIndex % 10 == 0)
                    {
                        if (!filesLists.ContainsKey(finialIndex))
                        {
                            filesLists.Add(finialIndex, new List<FileInfo>());
                        }
                    }

                    filesLists[finialIndex].Add(item);
                });

                Parallel.ForEach(filesLists, oo =>
                {
                    foreach (var item in oo.Value)
                    {
                        WL("Begin to extract the data from file:{0}", item.FullName);
                        using (var reader = new StreamReader(item.FullName))
                        {
                            reader.ReadLine();
                            while (!reader.EndOfStream)
                            {
                                var lines = reader.ReadLine().Split(',').Select(it => it.Trim('"')).ToArray();

                                var score = new StudentScoreInfo
                                {
                                    XN = lines[0],
                                    XQ = lines[1],
                                    XH = lines[2],
                                    CJ = lines[3],
                                    XF = lines[4],
                                    KCMC = lines[5],
                                    ID=lines[6]
                                };

                                WL("Score:{0} FileName:{1}", score.ToString(), item.FullName);

                                using (var connection = new SqlConnection(CVAcademicianDbConnectionString))
                                {
                                    connection.Open();

                                    var command = new SqlCommand(String.Format(@"
                            IF NOT EXISTS(SELECT 1 FROM StudentScoreNew WHERE StudentNum='{5}' AND KCMC='{4}' AND XN='{0}' AND XQ='{1}')
                            BEGIN
                                INSERT INTO [StudentScoreNew]
                                   ([XN]
                                   ,[XQ]
                                   ,[CJ]
                                   ,[XF]
                                   ,[KCMC]
                                   ,[StudentNum],
                                    ThirdId)
                               VALUES
                               ('{0}',
                                '{1}',
                                '{2}',
                                '{3}',
                                '{4}',
                                '{5}',
                                '{6}')
                            END

                            UPDATE StudentScoreNew
                               SET CJ='{2}',XF='{3}'
                               WHERE ID='{6}'
                               --WHERE StudentNum='{5}' AND KCMC='{4}' AND XN='{0}' AND XQ='{1}'
                                ", score.XN, score.XQ, score.CJ, score.XF, score.KCMC, score.XH,score.ID), connection);

                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        WL("Done to extract the data from file:{0}", item.FullName);
                    }
                });

                #region ldCodes
//                dir.GetFiles().ToList().ForEach(item =>
//                {
//                    WL("Begin to extract the data from file:{0}", item.FullName);
//                    using (var reader = new StreamReader(item.FullName))
//                    {
//                        reader.ReadLine();
//                        while (!reader.EndOfStream)
//                        {
//                            var lines = reader.ReadLine().Split(',').Select(it => it.Trim('"')).ToArray();

//                            var score = new StudentScoreInfo
//                            {
//                                XN = lines[0],
//                                XQ = lines[1],
//                                XH = lines[2],
//                                CJ = lines[3],
//                                XF = lines[4],
//                                KCMC = lines[5]
//                            };

//                            WL(score.ToString());

//                            using (var connection = new SqlConnection(CVAcademicianDbConnectionString))
//                            {
//                                connection.Open();

//                                var command = new SqlCommand(String.Format(@"
//                            IF NOT EXISTS(SELECT 1 FROM StudentScoreNew WHERE StudentNum='{5}' AND KCMC='{4}' AND XN='{0}' AND XQ='{1}')
//                            BEGIN
//                                INSERT INTO [StudentScoreNew]
//                                   ([XN]
//                                   ,[XQ]
//                                   ,[CJ]
//                                   ,[XF]
//                                   ,[KCMC]
//                                   ,[StudentNum])
//                               VALUES
//                               ('{0}',
//                                '{1}',
//                                '{2}',
//                                '{3}',
//                                '{4}',
//                                '{5}')
//                            END
//
//                            UPDATE StudentScoreNew
//                               SET CJ='{2}',XF='{3}'
//                               WHERE StudentNum='{5}' AND KCMC='{4}' AND XN='{0}' AND XQ='{1}'
//                                ", score.XN, score.XQ, score.CJ, score.XF, score.KCMC, score.XH), connection);

//                                command.ExecuteNonQuery();
//                            }
//                        }
//                    }

//                    WL("Done to extract the data from file:{0}", item.FullName);
//                });

                #endregion

            }
            WL("Extracted the directory :{0}", sourceImportedFilePath);
        }

        private struct StudentScoreInfo
        {
            /// <summary>
            /// 学年
            /// </summary>
            public string XN { get; set; }

            /// <summary>
            /// 学期
            /// </summary>
            public string XQ { get; set; }

            /// <summary>
            /// 学号
            /// </summary>
            public string XH { get; set; }

            /// <summary>
            /// 成绩
            /// </summary>
            public string CJ { get; set; }

            /// <summary>
            /// 学分
            /// </summary>
            public string XF { get; set; }

            /// <summary>
            /// 课程名称
            /// </summary>
            public string KCMC { get; set; }

            public string ID { get; set; }

            public override string ToString()
            {
                return String.Format("XN:{0} CJ:{1}", XN, CJ);
            }
        }
    }
}
