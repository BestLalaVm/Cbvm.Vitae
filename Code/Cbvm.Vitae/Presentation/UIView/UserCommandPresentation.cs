using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Enum;

namespace Presentation.UIView
{
    [Serializable]
    public class UserCommandPresentation:CommentPresentation
    {
        public UserType UserType { get; set; }

        public string UserTypeString
        {
            get
            {
                switch (UserType)
                {
                    case Enum.UserType.College:
                        return "学院";
                    case Enum.UserType.Enterprise:
                        return "企业";
                    case Enum.UserType.Family:
                        return "家庭";
                    case Enum.UserType.Student:
                        return "学生";
                    case Enum.UserType.Teacher:
                        return "老师";
                    case Enum.UserType.University:
                        return "学校";
                }

                return "";
            }
        }

        public string UserName { get; set; }

        public string Name { get; set; }
    }
}
