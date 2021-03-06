﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView.Student;
using WebLibrary;
using WebLibrary.Helper;
using Presentation.UIView.Student;

namespace Cbvm.Vitae.Manage.Family.Children
{
    public partial class ChildCareerPlanDetail : BaseFamilyDetailPage
    {
        private IStudentCareerPlanService Service
        {
            get
            {
                return new StudentCareerPlanService();
            }
        }

        protected override void InitData()
        {
             var presentation = this.Service.GetOrigianlQuery<LkDataContext.StudentCareerPlan>().Where(it => it.ID == CurrentID && it.StudentNum==CurrentUser.UserName ).Select(it => new
            {
                it.ID,
                it.Title,
                it.Description,
                it.StartDate,
                it.EndDate,
                it.StudentNum,
                it.IsImplemented,
                it.IsOnline
            }).FirstOrDefault();

             if (presentation != null)
             {
                 this.txt_Title_.Text = presentation.Title;
                 this.ltlContent.Text = (presentation.Description);
                 this.dtp_BeginTime_.SelectedDate = presentation.StartDate;
                 this.dtp_EndTime_.SelectedDate = presentation.EndDate;
                 chk_IsOnline_.Checked = presentation.IsOnline;
                 chk_IsImplemented_.Checked = presentation.IsImplemented;
             }
        }
    }
}