using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LkDataContext;
using Presentation;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Base;
using Presentation.UIView.Student;

namespace Presentation.Cache
{
    public static partial class GlobalBaseDataCache
    {
        private static CVAcademicianDataContext dataContext = new CVAcademicianDataContext();

        private static List<DepartPresentation> _DepartList;
        public static List<DepartPresentation> DepartList
        {
            get
            {
                if (_DepartList == null)
                {
                    _DepartList = dataContext.Depart.Select(it => new DepartPresentation()
                        {
                            Code = it.Code,
                            Name = it.Name,
                            Description = it.Description
                        }).ToList();
                }
                return _DepartList;
            }
        }

        private static List<MarjorPresentation> _MarjorList;
        public static List<MarjorPresentation> MarjorList
        {
            get
            {
                if (_MarjorList == null)
                {
                    _MarjorList = dataContext.Marjor.Select(it => new MarjorPresentation()
                        {
                            Code = it.Code,
                            Name = it.Name,
                            Description = it.Description
                        }).ToList();
                }
                return _MarjorList;
            }
        }

        private static List<ActivityTypePresentation> _ActivityTypeList;
        public static List<ActivityTypePresentation> ActivityTypeList
        {
            get
            {
                if (_ActivityTypeList == null)
                {
                    _ActivityTypeList = dataContext.ActivityType.Where(it=>it.IsOnline).OrderByDescending(it=>it.DisplayOrder).Select(it => new ActivityTypePresentation()
                    {
                        Code = it.Code,
                        Name = it.Name,
                        Description = it.Description,
                        ActivityDataType = (ActivityDataType)it.ActivityDataType
                    }).ToList();
                }
                return _ActivityTypeList;
            }
        }

        private static List<DataItemPresentation> _VerifyStatusList;
        public static List<DataItemPresentation> VerifyStatusList
        {
            get
            {
                if (_VerifyStatusList == null)
                {
                    _VerifyStatusList = new List<DataItemPresentation>();
                    _VerifyStatusList.Add(new DataItemPresentation
                    {
                        Value = ((int)VerifyStatus.Passed).ToString(),
                        Text = "审核通过"
                    });
                    _VerifyStatusList.Add(new DataItemPresentation
                    {
                        Value = ((int)VerifyStatus.UnPassed).ToString(),
                        Text = "审核未通过"
                    });
                    _VerifyStatusList.Add(new DataItemPresentation
                    {
                        Value = ((int)VerifyStatus.WaitAudited).ToString(),
                        Text = "等待审核"
                    });
                }
                return _VerifyStatusList;
            }
        }

        private static List<DataItemPresentation> _PoliticsList;
        public static List<DataItemPresentation> PoliticsList
        {
            get
            {
                if (_PoliticsList == null)
                {
                    _PoliticsList = new List<DataItemPresentation>();
                    _PoliticsList.Add(new DataItemPresentation
                    {
                        Text = "团员",
                        Value = "3"
                    });
                    _PoliticsList.Add(new DataItemPresentation
                    {
                        Text = "党员",
                        Value = "2"
                    });
                    _PoliticsList.Add(new DataItemPresentation
                    {
                        Text = "群众",
                        Value = "1"
                    });
                    _PoliticsList.Add(new DataItemPresentation
                    {
                        Text = "其他",
                        Value = "0"
                    });
                }
                return _PoliticsList;
            }
        }

        private static List<DataItemPresentation> _MarriedList;
        public static List<DataItemPresentation> MarriedList
        {
            get
            {
                if (_MarriedList == null)
                {
                    _MarriedList = new List<DataItemPresentation>();
                    _MarriedList.Add(new DataItemPresentation
                    {
                        Text = "已婚",
                        Value = "1"
                    });
                    _MarriedList.Add(new DataItemPresentation
                    {
                        Text = "未婚",
                        Value = "0"
                    });
                }
                return _MarriedList;
            }
        }

        private static List<DataItemPresentation> _SexInfoList;
        public static List<DataItemPresentation> SexInfoList
        {
            get
            {
                if (_SexInfoList == null)
                {
                    _SexInfoList = new List<DataItemPresentation>();
                    _SexInfoList.Add(new DataItemPresentation
                    {
                        Text = "男",
                        Value = "1"
                    });
                    _SexInfoList.Add(new DataItemPresentation
                    {
                        Text = "女",
                        Value = "0"
                    });
                }
                return _SexInfoList;
            }
        }

        private static IList<DataItemPresentation> _PositionNatureList;
        public static IList<DataItemPresentation> PositionNatureList
        {
            get
            {
                if (_PositionNatureList == null || _PositionNatureList.Count == 0)
                {
                    _PositionNatureList = new List<DataItemPresentation>();
                    _PositionNatureList.Add(new DataItemPresentation
                    {
                        Text = "全职",
                        Value = "1"
                    });
                    _PositionNatureList.Add(new DataItemPresentation
                    {
                        Text = "兼职",
                        Value = "0"
                    });
                    _PositionNatureList.Add(new DataItemPresentation
                    {
                        Text = "实习生",
                        Value = "-1"
                    });
                }
                return _PositionNatureList;
            }
        }

        private static IList<DataItemPresentation> _RequiredTargeterList;
        public static IList<DataItemPresentation> RequiredTargeterList
        {
            get
            {
                if (_RequiredTargeterList == null || _RequiredTargeterList.Count == 0)
                {
                    _RequiredTargeterList = new List<DataItemPresentation>();
                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "不限",
                        Value = "0"
                    });
                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "实习生",
                        Value = "1"
                    });
                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "应届毕业生",
                        Value = "2"
                    });

                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "一年工作及以上经历",
                        Value = "3"
                    });
                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "一年到二年工作经历",
                        Value = "4"
                    });
                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "二年以上工作经历",
                        Value = "5"
                    });
                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "三年以上工作经历",
                        Value = "6"
                    });
                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "四年以上工作经历",
                        Value = "7"
                    });
                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "五年以上工作经历",
                        Value = "8"
                    });
                    _RequiredTargeterList.Add(new DataItemPresentation
                    {
                        Text = "六年以上工作经历",
                        Value = "9"
                    });
                }
                return _RequiredTargeterList;
            }
        }

        private static IList<DataItemPresentation> _EducationRequiredList;
        public static IList<DataItemPresentation> EducationRequiredList
        {
            get
            {
                if (_EducationRequiredList == null || _EducationRequiredList.Count == 0)
                {
                    _EducationRequiredList = new List<DataItemPresentation>();
                    _EducationRequiredList.Add(new DataItemPresentation
                    {
                        Text = "不限",
                        Value = "0"
                    });
                    _EducationRequiredList.Add(new DataItemPresentation
                    {
                        Text = "小学及以上",
                        Value = "1"
                    });
                    _EducationRequiredList.Add(new DataItemPresentation
                    {
                        Text = "初中及以上",
                        Value = "2"
                    });
                    _EducationRequiredList.Add(new DataItemPresentation
                    {
                        Text = "中专及以上",
                        Value = "3"
                    });
                    _EducationRequiredList.Add(new DataItemPresentation
                    {
                        Text = "高中及以上",
                        Value = "4"
                    });
                    _EducationRequiredList.Add(new DataItemPresentation
                    {
                        Text = "大专及以上",
                        Value = "5"
                    });
                    _EducationRequiredList.Add(new DataItemPresentation
                    {
                        Text = "本科及以上",
                        Value = "5"
                    });
                    _EducationRequiredList.Add(new DataItemPresentation
                    {
                        Text = "硕士及以上",
                        Value = "5"
                    });
                    _EducationRequiredList.Add(new DataItemPresentation
                    {
                        Text = "博士士及以上",
                        Value = "5"
                    });
                }
                return _EducationRequiredList;
            }

        }

        private static IList<DataItemPresentation> _UserTypeList;
        public static IList<DataItemPresentation> UserTypeList
        {
            get
            {
                if (_UserTypeList == null || _UserTypeList.Count == 0)
                {
                    _UserTypeList = new List<DataItemPresentation>();
                    _UserTypeList.Add(new DataItemPresentation
                    {
                        Text = "学生",
                        Value = ((int)UserType.Student).ToString()
                    });

                    _UserTypeList.Add(new DataItemPresentation
                    {
                        Text = "教师",
                        Value = ((int)UserType.Teacher).ToString()
                    });

                    _UserTypeList.Add(new DataItemPresentation
                    {
                        Text = "企业",
                        Value = ((int)UserType.Enterprise).ToString()
                    });

                    _UserTypeList.Add(new DataItemPresentation
                    {
                        Text = "部门管理员",
                        Value = ((int)UserType.DepartAdmin).ToString()
                    });

                    _UserTypeList.Add(new DataItemPresentation
                    {
                        Text = "管理员",
                        Value = ((int)UserType.Admin).ToString()
                    });

                    _UserTypeList.Add(new DataItemPresentation
                    {
                        Text = "家长",
                        Value = ((int)UserType.Family).ToString()
                    });
                }
                return _UserTypeList;
            }
        }

        private static List<CourseTypePresentation> _CourseTypeList;
        public static List<CourseTypePresentation> CourseTypeList
        {
            get
            {
                if (_CourseTypeList == null)
                {
                    _CourseTypeList = dataContext.CourseType.Select(it => new CourseTypePresentation()
                    {
                        Code = it.Code,
                        Name = it.Name,
                        Description = it.Description
                    }).ToList();
                }
                return _CourseTypeList;
            }
        }

        private static List<CoursePresentation> _CourseList;
        public static List<CoursePresentation> CourseList
        {
            get
            {
                if (_CourseList == null)
                {
                    _CourseList = dataContext.Course.Select(it => new CoursePresentation()
                        {
                            Code = it.Code,
                            Name = it.Name,
                            Description = it.Description,
                            TypeCode = it.TypeCode,
                            MarjorCode = it.MarjorCode
                        }).ToList();
                }
                return _CourseList;
            }
        }

        private static List<EducationPresentation> _EducationList;
        public static List<EducationPresentation> EducationList
        {
            get
            {
                if (_EducationList == null)
                {
                    _EducationList = dataContext.Education.Select(it => new EducationPresentation()
                    {
                        Code = it.Code,
                        Name = it.Name,
                        Description = it.Description
                    }).ToList();
                }
                return _EducationList;
            }
        }

        private static List<IndustryPresentation> _CdIndustryList;
        public static List<IndustryPresentation> CdIndustryList
        {
            get
            {
                if (_CdIndustryList == null)
                {
                    _CdIndustryList = dataContext.CdIndustry.Select(it => new IndustryPresentation()
                    {
                        Code = it.Code,
                        Name = it.Name,
                        Description = it.Description
                    }).ToList();
                }
                return _CdIndustryList;
            }
        }

        private static List<RegionPresentation> _CdRegionList;
        public static List<RegionPresentation> CdRegionList
        {
            get
            {
                if (_CdRegionList == null)
                {
                    _CdRegionList = dataContext.CdRegion.Select(it => new RegionPresentation()
                    {
                        Code = it.Code,
                        Name = it.Name
                    }).ToList();
                }
                return _CdRegionList;
            }
        }

        private static List<EnterpriseTypePresentation> _EnterpriseTypeList;
        public static List<EnterpriseTypePresentation> EnterpriseTypeList
        {
            get
            {
                if (_EnterpriseTypeList == null)
                {
                    _EnterpriseTypeList = dataContext.EnterpriseType.Select(it => new EnterpriseTypePresentation()
                    {
                        Code = it.Code,
                        Name = it.Name,
                        Description = it.Description
                    }).ToList();
                }
                return _EnterpriseTypeList;
            }
        }

        private static List<EnterpriseScopePresentation> _EnterpriseScopeList;
        public static List<EnterpriseScopePresentation> EnterpriseScopeList
        {
            get
            {
                if (_EnterpriseScopeList == null)
                {
                    _EnterpriseScopeList = dataContext.EnterpriseScope.Select(it => new EnterpriseScopePresentation()
                    {
                        Code = it.Code,
                        Name = it.Name,
                        Description = it.Description
                    }).ToList();
                }
                return _EnterpriseScopeList;
            }
        }

        private static List<TeacherGroupPresentation> _TeacherGroupList;
        public static List<TeacherGroupPresentation> TeacherGroupList
        {
            get
            {
                if (_TeacherGroupList == null)
                {
                    _TeacherGroupList = dataContext.TeacherGroup.Select(it => new TeacherGroupPresentation()
                    {
                        Code = it.Code,
                        Name = it.Name,
                        Description = it.Description,
                        TeacherGroupType = (TeacherGroupType)it.TeacherGroupType
                    }).ToList();
                }
                return _TeacherGroupList;
            }
        }

        private static List<RecruitFlowSettedPresentation> _RecruitFlowSettedList;
        public static List<RecruitFlowSettedPresentation> RecruitFlowSettedList
        {
            get
            {
                if (_RecruitFlowSettedList == null)
                {
                    _RecruitFlowSettedList = dataContext.RecruitFlowSetted.Select(it => new RecruitFlowSettedPresentation()
                    {
                        Id = it.ID,
                        Name = it.Name,
                        Description = it.Description,
                        DisplayOrder = it.DisplayOrder??0,
                        RecruitType = (RecruitType)it.RecruitType
                    }).ToList();
                }
                return _RecruitFlowSettedList;
            }
        }

        private static List<DataItemPresentation> _WorkYearsList;

        public static List<DataItemPresentation> WorkYearList
        {
            get
            {
                if (_WorkYearsList == null)
                {
                    _WorkYearsList = new List<DataItemPresentation>();
                    _WorkYearsList.Add(new DataItemPresentation()
                    {
                        Text = "无经验",
                        Value = "0"
                    });
                    _WorkYearsList.Add(new DataItemPresentation()
                    {
                        Text = "一年",
                        Value = "1"
                    });
                    _WorkYearsList.Add(new DataItemPresentation()
                    {
                        Text = "二年",
                        Value = "2"
                    });
                    _WorkYearsList.Add(new DataItemPresentation()
                    {
                        Text = "三年",
                        Value = "3"
                    });
                    _WorkYearsList.Add(new DataItemPresentation()
                    {
                        Text = "四年",
                        Value = "4"
                    });
                    _WorkYearsList.Add(new DataItemPresentation()
                    {
                        Text = "五年及以上",
                        Value = "5"
                    });
                }
                return _WorkYearsList;
            }
        }

        private static List<DataItemPresentation> _ReferralStateList;
        public static List<DataItemPresentation> ReferralStateList
        {
            get
            {
                if (_ReferralStateList == null)
                {
                    _ReferralStateList = new List<DataItemPresentation>();
                    _ReferralStateList.Add(new DataItemPresentation
                    {
                        Text = EnumHelper.GetEnumDescription(ReferralState.Pending),
                        Value = ((int)ReferralState.Pending).ToString()
                    });
                    _ReferralStateList.Add(new DataItemPresentation
                    {
                        Text = EnumHelper.GetEnumDescription(ReferralState.Passed),
                        Value = ((int)ReferralState.Passed).ToString()
                    });
                    _ReferralStateList.Add(new DataItemPresentation
                    {
                        Text = EnumHelper.GetEnumDescription(ReferralState.Deny),
                        Value = ((int)ReferralState.Deny).ToString()
                    });
                    _ReferralStateList.Add(new DataItemPresentation
                    {
                        Text = EnumHelper.GetEnumDescription(ReferralState.TimeOut),
                        Value = ((int)ReferralState.TimeOut).ToString()
                    });
                }
                return _ReferralStateList;
            }
        }

        private static List<DataItemPresentation> _SalaryList;
        public static List<DataItemPresentation> SalaryList
        {
            get
            {
                if (_SalaryList == null)
                {
                    _SalaryList = new List<DataItemPresentation>();
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "1000左右",
                        Value = "1000左右"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "1000-2000",
                        Value = "1000-2000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "2000-3000",
                        Value = "2000-3000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "3000-4000",
                        Value = "3000-4000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "4000-5000",
                        Value = "4000-5000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "5000-6000",
                        Value = "5000-6000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "6000-8000",
                        Value = "6000-8000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "8000-10000",
                        Value = "8000-10000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "10000-15000",
                        Value = "10000-15000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "15000-20000",
                        Value = "15000-20000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "20000-30000",
                        Value = "20000-30000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "30000-50000",
                        Value = "30000-50000"
                    });
                    _SalaryList.Add(new DataItemPresentation()
                    {
                        Text = "50000以上",
                        Value = "50000以上"
                    });
                }
                return _SalaryList;
            }
        }

        private static List<DataItemPresentation> _OpenTypeInfoList;
        public static List<DataItemPresentation> OpenTypeInfoList
        {
            get
            {
                if (_OpenTypeInfoList == null)
                {
                    _OpenTypeInfoList = EnumHelper.GetEnumDescriptionList<StudentOpenType>().ToList();
                }
                return _OpenTypeInfoList;
            }
        }

        private static IList<DataItemPresentation> _TeacherList;
        public static IList<DataItemPresentation> TeacherList
        {
            get
            {
                if (_TeacherList == null)
                {
                    _TeacherList =
                        dataContext.Teacher.Where(it => it.IsDelete == false).Select(it => new DataItemPresentation()
                        {
                            Text = it.NameZh,
                            Value = it.TeacherNum
                        }).ToList();
                }
                return _TeacherList;
            }
        }

        private static IList<StudentPresentation> _StudentPresentationList;

        public static IList<StudentPresentation> StudentPresentationList
        {
            get
            {
                if (_StudentPresentationList == null)
                {
                    _StudentPresentationList = dataContext.Student.Select(it => new StudentPresentation()
                    {
                        Address=it.Address,
                        Birthday=it.Birthday,
                        Class = it.Class,
                        Description = it.Description,
                        Email=it.Email,
                        IDentityNum=it.IDentityNum,
                        IsMarried=it.IsMarried,
                        IsOnline=it.IsOnline,
                        MarjorCode=it.MarjorCode,
                        NameEn=it.NameEn,
                        NameZh=it.NameZh,
                        NativePlace=it.NativePlace,
                        Period=it.Period,
                        Photo=it.Photo,
                        Politics=(PoliticsType)it.Politics,
                        Sex=(SexType)it.Sex,
                        StudentNum=it.StudentNum,
                        Tall=it.Tall,
                        Telephone=it.Telephone,
                        ThumbPath=it.ThumbPath,
                        WebSite=it.WebSite,
                        Weight=it.Weight
                    }).ToList();
                }
                return _StudentPresentationList;
            }
        }


        private static IList<AddressPresentation> _AddressPresentationList;
        public static IList<AddressPresentation> AddressPresentationList
        {
            get
            {
                if (_AddressPresentationList == null)
                {
                    _AddressPresentationList = dataContext.CdAddress.Select(it => new AddressPresentation
                    {
                        Code = it.Code,
                        Name = it.Name,
                        ParentCode = it.ParentCode,
                        Type = it.Type
                    }).ToList();
                }

                return _AddressPresentationList;
            }
        }
    }

    static partial class GlobalBaseDataCache
    {
        public static string GetVerifityStatusLabel(VerifyStatus status)
        {
            var verifyItem = GlobalBaseDataCache.VerifyStatusList.FirstOrDefault(it => it.Value == ((int)status).ToString());
            if (verifyItem == null) return "";
            return verifyItem.Text;
        }

        public static string GetActivityTypeLabel(string activityCode)
        {
            var activityItem = GlobalBaseDataCache.ActivityTypeList.FirstOrDefault(it => it.Code == activityCode);
            if (activityItem == null) return "";
            return activityItem.Name;

        }

        public static string GetTeacherName(string teacherNum)
        {
            var teacher = dataContext.Teacher.FirstOrDefault(it => it.TeacherNum == teacherNum);
            if (teacher == null) return "";
            return teacher.NameZh;
        }

        public static string GetDepartName(string departCode)
        {
            var depart = GlobalBaseDataCache.DepartList.FirstOrDefault(it => it.Code == departCode);
            if (depart == null) return "";
            return depart.Name;
        }

        public static string GetMarjorName(string marjorCode)
        {
            var marjor = GlobalBaseDataCache.MarjorList.FirstOrDefault(it => it.Code == marjorCode);
            if (marjor == null) return "";
            return marjor.Name;
        }

        public static string GetPoliticsLabel(PoliticsType politcs)
        {
            var firstItem = GlobalBaseDataCache.PoliticsList.FirstOrDefault(it => it.Value == ((int)politcs).ToString());
            if (firstItem != null)
            {
                return firstItem.Text;
            }
            return "";
        }

        public static string GetMarriedLabel(bool isMarried)
        {
            var firstItem = GlobalBaseDataCache.MarriedList.FirstOrDefault(it => it.Value == (isMarried ? "1" : "0"));
            if (firstItem != null)
            {
                return firstItem.Text;
            }
            return "";
        }

        public static string GetSexLabel(SexType sex)
        {
            var firstItem = GlobalBaseDataCache.SexInfoList.FirstOrDefault(it => it.Value == ((int)sex).ToString());
            if (firstItem != null)
            {
                return firstItem.Text;
            }
            return "";
        }


        public static string GetSexLabel(int sex)
        {
            if (sex < 0)
            {
                return "不限";
            }
            var firstItem = GlobalBaseDataCache.SexInfoList.FirstOrDefault(it => it.Value == ((int) sex).ToString());
            if (firstItem != null)
            {
                return firstItem.Text;
            }
            return "";
        }

        public static string GetCourseName(string code)
        {
            var firstItem = GlobalBaseDataCache.CourseList.FirstOrDefault(it => it.Code == code);
            if (firstItem != null)
            {
                return firstItem.Name;
            }
            return "";
        }

        public static string GetCourseTypeName(string code)
        {
            var firstItem = GlobalBaseDataCache.CourseTypeList.FirstOrDefault(it => it.Code == code);
            if (firstItem != null)
            {
                return firstItem.Name;
            }
            return "";
        }

        public static string GetCourseTypeNameByCourseCode(string code)
        {
            var firstItem = GlobalBaseDataCache.CourseList.FirstOrDefault(it => it.Code == code);
            if (firstItem != null)
            {
                var firstCourseType = GlobalBaseDataCache.CourseTypeList.FirstOrDefault(it => it.Code == firstItem.TypeCode);
                if (firstCourseType != null)
                {
                    return firstCourseType.Name;
                }
            }
            return "";
        }

        public static string GetMarriedValue(bool isMarried)
        {
            if (isMarried) return "1";
            return "0";
        }

        public static string GetRequestStatus(RecruitType status)
        {
            switch (status)
            {
                case RecruitType.NoPassed:
                    return "拒绝";
                case RecruitType.Request:
                    return "请求中";
                case RecruitType.Interviewing:
                    return "邀请面试";
                case RecruitType.Invited:
                    return "邀请面试";
                case RecruitType.Passed:
                    return "面试通过";
            }
            return "请求中";
        }

        public static string GetEducationName(string educationCode)
        {
            var education = EducationList.FirstOrDefault(ic => ic.Code == educationCode);
            if (education == null) return "";
            return education.Name;
        }

        public static string GetReferralStateName(ReferralState state)
        {
            var referralState =
                GlobalBaseDataCache.ReferralStateList.FirstOrDefault(ix => ix.Value == ((int) state).ToString());
            if (referralState == null) return "";
            return referralState.Text;
        }
    }


}
