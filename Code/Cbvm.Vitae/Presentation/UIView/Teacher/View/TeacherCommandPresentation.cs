using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Presentation.Cache;

namespace Presentation.UIView.Teacher.View
{
    [Serializable]
    public class TeacherCommandPresentation : BasePresentation
    {
        public string TeacherNum { get; set; }

        public string NameZh { get; set; }

        public string NameEn { get; set; }

        public string Email { get; set; }

        public string CollegeName { get; set; }

        public string Photo { get; set; }

        public int Sex { get; set; }

        public bool Selected { get; set; }

        public string SexLabel
        {
            get
            {
                return GlobalBaseDataCache.GetSexLabel(Sex);
            }
        }
    }
}
