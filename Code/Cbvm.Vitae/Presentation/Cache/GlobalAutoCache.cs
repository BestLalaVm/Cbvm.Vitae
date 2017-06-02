using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LkDataContext;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Enterprise;
using Presentation.UIView.Family;
using Presentation.UIView.Student;
using Presentation.UIView.Teacher;

namespace Presentation.Cache
{
    public static class GlobalAutoCache
    {
        private static CVAcademicianDataContext dataContext = new CVAcademicianDataContext();

        #region StudentList
        private static IList<StudentPresentation> _StudentPresentationList;
        public static IList<StudentPresentation> StudentPresentationList
        {
            get
            {
                if (_StudentPresentationList == null)
                {
                    _StudentPresentationList = dataContext.Student.Select(it => new
                    {
                        it.StudentNum,
                        it.Sex,
                        it.NameEn,
                        it.NameZh,
                        MarjorName = it.Marjor.Name,
                        it.Class
                    }).ToList().Select(it => new StudentPresentation
                    {
                        NameZh = it.NameZh,
                        NameEn = it.NameEn,
                        Sex = (SexType)it.Sex,
                        StudentNum = it.StudentNum,
                        Class = it.Class
                    }).ToList();
                }
                return _StudentPresentationList;
            }
        }
        #endregion

        #region TeacherPresentationList
        private static IList<TeacherPresentation> _TeacherPresentationList;
        public static IList<TeacherPresentation> TeacherPresentationList
        {
            get
            {
                if (_TeacherPresentationList == null)
                {
                    var list = dataContext.Teacher.Select(it => new
                    {
                        it.TeacherNum,
                        it.Sex,
                        it.NameEn,
                        it.NameZh,
                        //DepartName = it.Depart.Name
                    }).ToList();
                    _TeacherPresentationList = list.Select(it => new TeacherPresentation
                    {
                        //DepartName = it.DepartName,
                        Name = it.NameZh,
                        Sex = (SexType) it.Sex,
                        TeacherNum = it.TeacherNum,
                        NameZh = it.NameZh,
                        NameEn = it.NameEn
                    }).ToList();
                }
                return _TeacherPresentationList;
            }
        }
        #endregion

        #region EnterprisePresentationList
        private static IList<EnterprisePresentation> _EnterprisePresentationList;
        public static IList<EnterprisePresentation> EnterprisePresentationList
        {
            get
            {
                if (_EnterprisePresentationList == null)
                {
                    _EnterprisePresentationList = dataContext.Enterprise.Select(it => new
                    {
                        it.ContactName,
                        it.ContactPhone,
                        it.Name,
                        it.Code,
                        it.UserName
                    }).ToList().Select(it => new EnterprisePresentation
                    {
                        Name = it.Name,
                        ContactName = it.ContactName,
                        Code = it.Code,
                        UserName = it.UserName
                    }).ToList();
                }
                return _EnterprisePresentationList;
            }
        }
        #endregion

        #region StudentFamilyPresentationList
        private static List<FamilyPresentation> _StudentFamilyPresentationList;
        public static List<FamilyPresentation> StudentFamilyPresentationList
        {
            get
            {
                if (_StudentFamilyPresentationList == null)
                {
                    _StudentFamilyPresentationList =
                        dataContext.StudentFamilyAccount.Select(it => new
                        {
                            it.NameEn,
                            it.NameZh,
                            it.Sex,
                            it.UserName,
                            it.StudentNum
                        }).ToList().Select(it => new FamilyPresentation()
                        {
                            NameZh = it.NameZh,
                            NameEn = it.NameEn,
                            Sex = (SexType)it.Sex,
                            UserName = it.UserName,
                            StudentNum = it.StudentNum
                        }).ToList();
                }
                return _StudentFamilyPresentationList;
            }
        }
        #endregion


        public static IList<AutoValuePresentation> GetAllAutoList(string keyword, string currentUser = null,
                                                                  UserType? userType = null)
        {
            var list = new List<AutoValuePresentation>();
            var index = 1;
            if (String.IsNullOrEmpty(currentUser) && userType.HasValue && userType.Value == UserType.Student)
            {
                var family =
                    StudentFamilyPresentationList.FirstOrDefault(
                        it => it.StudentNum == currentUser &&
                            (keyword == "家长" || keyword == "父亲" || keyword == "母亲" || keyword.Contains("爹") || keyword.Contains("爸") || keyword.Contains("娘")));
                if (family != null)
                {
                    list.Add(new AutoValuePresentation()
                    {
                        Index = index++,
                        Code = family.UserName,
                        Description = "",
                        Name = family.NameZh,
                        UserType = UserType.Family,
                        ThumbPath = family.ThumbPath
                    });
                }
            }

            foreach (var student in StudentPresentationList)
            {
                if (student.StudentNum.ToLower().Contains(keyword.ToLower()) ||
                    student.Name.ToLower().Contains(keyword.ToLower()) ||
                    GlobalBaseDataCache.MarjorList.Any(ic=>ic.Code==student.MarjorCode && ic.Name.ToLower().Contains(keyword.ToLower())))
                {
                    list.Add(new AutoValuePresentation()
                    {
                        Index = index++,
                        Code = student.StudentNum,
                        Description = GlobalBaseDataCache.GetMarjorName(student.MarjorCode),
                        Name = student.Name,
                        UserType = UserType.Student,
                        ThumbPath = student.ThumbPath
                    });
                }
            }

            foreach (var teacher in TeacherPresentationList)
            {
                if (teacher.TeacherNum.ToLower().Contains(keyword.ToLower()) ||
                    teacher.Name.ToLower().Contains(keyword.ToLower()) ||
                    teacher.DepartName.ToLower().Contains(keyword.ToLower()) || keyword == "老师")
                {
                    list.Add(new AutoValuePresentation()
                    {
                        Index = index++,
                        Code = teacher.TeacherNum,
                        Description = teacher.DepartName,
                        Name = teacher.Name,
                        UserType = UserType.Teacher,
                        ThumbPath = teacher.ThumbPath
                    });
                }
            }

            foreach (var enterprise in EnterprisePresentationList)
            {
                if (enterprise.Code.ToLower().Contains(keyword.ToLower()) ||
                    enterprise.Name.ToLower().Contains(keyword.ToLower()) ||
                    enterprise.Name.ToLower().Contains(keyword.ToLower()) || keyword == "企业" || keyword == "单位")
                {
                    list.Add(new AutoValuePresentation()
                    {
                        Index = index++,
                        Code = enterprise.Code,
                        Description = enterprise.Name,
                        Name = enterprise.Name,
                        UserType = UserType.Enterprise
                    });
                }
            }
            return list;
        }

        public static AutoValuePresentation GetAutoVlaue(string userName, UserType userType)
        {
            switch (userType)
            {
                case UserType.Student:
                    {
                        var student = GlobalAutoCache.StudentPresentationList.FirstOrDefault(it => it.StudentNum == userName);
                        if (student != null)
                        {
                            return new AutoValuePresentation()
                            {
                                Index = 1,
                                Code = student.StudentNum,
                                Description = GlobalBaseDataCache.GetMarjorName(student.MarjorCode),
                                Name = student.Name,
                                UserType = UserType.Student,
                                ThumbPath = student.ThumbPath
                            };
                        }
                    }
                    break;
                case UserType.Teacher:
                    {
                        var teacher = GlobalAutoCache.TeacherPresentationList.FirstOrDefault(it => it.TeacherNum == userName);
                        if (teacher != null)
                        {
                            return new AutoValuePresentation()
                            {
                                Index = 1,
                                Code = teacher.TeacherNum,
                                Description = teacher.Description,
                                Name = teacher.Name,
                                UserType = UserType.Teacher,
                                ThumbPath = teacher.ThumbPath
                            };
                        }
                    }
                    break;
                case UserType.Enterprise:
                    {
                        var enterprise =
                            GlobalAutoCache.EnterprisePresentationList.FirstOrDefault(it => it.UserName == userName);
                        if (enterprise != null)
                        {
                            return new AutoValuePresentation()
                            {
                                Index = 1,
                                Code = enterprise.UserName,
                                Description = enterprise.Name,
                                Name = enterprise.Name,
                                UserType = UserType.Enterprise
                            };
                        }
                    }
                    break;
                case UserType.College:
                    var college = GlobalAutoCache.dataContext.College.Where(it => it.CollegeAdmin.Where(ii => ii.UserName == userName).Any()).Select(it => new AutoValuePresentation
                    {
                        Code = it.CollegeAdmin.Select(ii => ii.UserName).FirstOrDefault(),
                        Name = it.Name,
                        UserType = UserType.College,
                        Description = it.Description
                    }).FirstOrDefault();

                    return college;
                case UserType.University:
                    var univerity = GlobalAutoCache.dataContext.UniversityAdmin.Where(it => it.UserName == userName).Select(it => new AutoValuePresentation
                    {
                        Code = it.UserName,
                        Name = it.University.Name,
                        Description = it.University.Description,
                        UserType = UserType.University
                    }).FirstOrDefault();

                    return univerity;
                case UserType.Family:
                    var family =
                        GlobalAutoCache.StudentFamilyPresentationList.FirstOrDefault(ix => ix.UserName == userName);
                    if (family != null)
                    {
                        return new AutoValuePresentation()
                        {
                            Code = family.UserName,
                            Description = family.NameZh,
                            Name = family.NameZh,
                            UserType = UserType.Family
                        };
                    }
                    break;
            }
            return null;
        }
    }
}
