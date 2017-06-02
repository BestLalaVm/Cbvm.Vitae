using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using LkDataContext;
using Presentation;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;
using WebLibrary;

namespace Business.Service
{
    public class ImportService : BaseService, IImportService
    {
        private string CollegeCode;
        public ImportService(string collegeCode)
        {
            this.CollegeCode = collegeCode;
        }

        private const string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;";
        public static void ImportDataFromExcel(ExeclDataSource data, IImportService control)
        {
            string cntString = string.Format(ConnectionString, data.FileName);
            OleDbDataAdapter execlAdapter = new OleDbDataAdapter(data.SelectSQL, cntString);
            DataSet dsSource = new DataSet();
            execlAdapter.Fill(dsSource);

            ImportService instance = control as ImportService;
            if (instance != null)
            {
                instance.ImportData(dsSource);
            }
        }

        public ImportResult ImportData(DataSet dsSource)
        {
            return null;
            //DataTable dtStudent = dsSource.Tables[0];
            //ImportResult result = new ImportResult
            //{
            //    Sucess = true
            //};
            //try
            //{
            //    foreach (DataRow rw in dtStudent.Rows)
            //    {
            //        var departName = rw["学院"].ToString();
            //        var depart = dataContext.Depart.FirstOrDefault(ic => ic.Name == departName && ic.CollegeCode == CollegeCode);
            //        if (depart == null)
            //        {
            //            depart = new Depart()
            //            {
            //                Name = departName,
            //                Code = GenerateCodeHelper.GenerateCode(GenerateCodeHelper.GenerateCodeType.Depart, ""),
            //                Description = departName,
            //                CollegeCode = CollegeCode
            //            };
            //            dataContext.Depart.InsertOnSubmit(depart);
            //        }

            //        var marjorName = rw["专业名称"].ToString();
            //        var marjor = dataContext.Marjor.FirstOrDefault(ic => ic.Name == marjorName && ic.CollegeCode==CollegeCode);
            //        if (marjor == null)
            //        {
            //            marjor = new Marjor()
            //            {
            //                Name = marjorName,
            //                Description = marjorName,
            //                CollegeCode=CollegeCode,
            //                Code =
            //                    GenerateCodeHelper.GenerateCode(GenerateCodeHelper.GenerateCodeType.Marjor, depart.Code)
            //            };
            //            depart.Marjor.Add(marjor);
            //        }

            //        var studentNum = rw["学号"].ToString();
            //        var student = dataContext.Student.FirstOrDefault(ic => ic.StudentNum == studentNum && ic.CollegeCode == CollegeCode);
            //        if (student == null)
            //        {
            //            student = new LkDataContext.Student()
            //            {
            //                NameZh = rw["姓名"].ToString(),
            //                StudentNum = rw["学号"].ToString(),
            //                Sex = (int) (rw["性别"].ToString() == "男" ? SexType.male : SexType.female),
            //                CollegeCode = CollegeCode,
            //                DepartCode=depart.Code,
            //                MarjorCode = marjor.Code,
            //                Class = GetClassName(rw["行政班"].ToString()),
            //                Period = rw["年级"].ToString(),
            //                NativePlace = rw["籍贯"].ToString(),
            //                Telephone = GetTelephone(rw["联系电话"].ToString()),
            //                Birthday = DateTime.Parse("1990-01-1"),
            //                Password = AccountSecurityManage.GetDefaultPassword(),
            //                IDentityNum = " ",
            //                Email = " ",
            //                IsOnline = true
            //            };

            //            student.StudentFamilyAccount.Add(new StudentFamilyAccount()
            //            {
            //                UserName = String.Format("{0}00001", student.StudentNum),
            //                Password = student.Password,
            //                Sex = (int) SexType.male,
            //                IsOnline = true,
            //                LastUpdatedTime = DateTime.Now,
            //                CreateTime = DateTime.Now,
            //                Relationship = "父亲"
            //            });
            //            dataContext.Student.InsertOnSubmit(student);
            //        }

            //    }
            //    dataContext.SubmitChanges();
            //}
            //catch (Exception ex)
            //{
            //    WriteLog(ex);
            //    result.FailListItem.Add(new ImportItemDetail
            //    {
            //        Description = ex.ToString(),
            //        ErrorType = ImportErrorType.Exchanged
            //    });
            //}
            //return result;
        }

        private string GetClassName(string ClassDescription)
        {
            if (!ClassDescription.Contains("班"))
            {
                return "1班";
            }
            return ClassDescription.Substring(ClassDescription.Length - 2, 2);
        }

        private string GetTelephone(string telephone)
        {
            if (string.IsNullOrEmpty(telephone)) return "";

            string[] tels = telephone.Replace("，", ",").Replace("；", ";").Replace("、", "").Split(new char[] { ',', ';', '/', ' ' });
            return tels[0];
        }
    }
}
