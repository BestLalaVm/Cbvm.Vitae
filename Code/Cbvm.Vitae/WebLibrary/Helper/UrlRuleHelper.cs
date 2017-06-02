using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary.Helper
{
    public enum RulePathType
    {
        DepartMessage = 0,
        StudentInfo = 1,
        StudentPost = 2,
        StudentBiographer = 3,
        Enterprise = 4,
        StudentProfessional = 5
    }

    public enum StudentRulePathType
    {
        Professional = 0,
        DailyEssay = 1,
        Project = 2,
        Activity = 3,
        Exercitation = 4,
        Detail = 5
    }

    public class RequireDataItem
    {
        public int StudentID
        {
            get;
            set;
        }

        public int CurrentID
        {
            get;
            set;
        }

        public string Version
        {
            get;
            set;
        }
    }

    public static class UrlRuleHelper
    {
        public static string GenerateUrl(string keyCode, string keyName, RulePathType ruleType)
        {
            switch (ruleType)
            {
                case RulePathType.DepartMessage:
                    return String.Format("/Message/Detail/{0}", keyCode);
                case RulePathType.StudentInfo:
                    return String.Format("/Student/Detail/{0}", keyCode);
                case RulePathType.Enterprise:
                    return String.Format("/Enterprise/Detail/{0}", keyCode);
            }
            return "#";
        }

        public static string GenerateUrl(string studentNum, string keyCode, string keyName, StudentRulePathType ruleType)
        {
            switch (ruleType)
            {
                case StudentRulePathType.DailyEssay:
                    return String.Format("/Student/Detail/{0}/DailyEssay/Detail/{1}", studentNum, keyCode);
                case StudentRulePathType.Project:
                    return String.Format("/Student/Detail/{0}/Project/Detail/{1}", studentNum, keyCode);
                case StudentRulePathType.Activity:
                    return String.Format("/Student/Detail/{0}/Activity/Detail/{1}", studentNum, keyCode);
                case StudentRulePathType.Exercitation:
                    return String.Format("/Student/Detail/{0}/Exercitation/Detail/{1}", studentNum, keyCode);
                case StudentRulePathType.Detail:
                    return String.Format("/Student/Detail/{0}", studentNum);
                    //return String.Format("/Student/Detail/{0}/Others", studentNum);
            }
            return String.Format("/Student/Detail/{0}/Professional/Detail/{1}", studentNum, keyCode);
        }

        public static string GenerateStudentMoreUrl(string studentNum, StudentRulePathType ruleType,
                                                    string queryString = null)
        {
            var url = String.Format("/Student/Detail/{0}/Professional/List", studentNum);
            switch (ruleType)
            {
                case StudentRulePathType.DailyEssay:
                    url = String.Format("/Student/Detail/{0}/DailyEssay/List", studentNum);
                    break;
                case StudentRulePathType.Project:
                    url = String.Format("/Student/Detail/{0}/Project/List", studentNum);
                    break;
                case StudentRulePathType.Activity:
                    url = String.Format("/Student/Detail/{0}/Activity/List", studentNum);
                    break;
                case StudentRulePathType.Exercitation:
                    url = String.Format("/Student/Detail/{0}/Exercitation/List", studentNum);
                    break;
            }
            if (!String.IsNullOrEmpty(queryString))
            {
                url = String.Format("{0}?{1}", url, queryString);
            }
            return url;
        }

        public static string GenerateMoreUrlLink(RulePathType ruleType)
        {
            switch (ruleType)
            {
                case RulePathType.Enterprise:
                    return string.Format("/Template/EnterpriseList.aspx");
            }
            return "#";
        }

        public static string GenerateBiographerUrl(int studentID, int ID)
        {
            return "/Template/Student/StudentBiographerDetail.aspx?StudentID=" + studentID.ToString() + "&ID=" + ID.ToString();
        }

        public static string GenerateAchievementUrl(int studentID, int ID)
        {
            return "/Template/Student/StudentAchievementDetail.aspx?StudentID=" + studentID + "&ID=" + ID;
        }

        public static string GeneratePostUrl(RequireDataItem item)
        {
            return "/Template/Student/StudentPostDetail.aspx?StudentID=" + item.StudentID.ToString() + "&ID=" + item.CurrentID.ToString();
        }

    }
}
