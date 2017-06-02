﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.Student;
using Business.Service.Student;
using LkHelper;
using Presentation.Enum;
using WebLibrary;
using WebLibrary.Helper;
using Cbvm.Vitae.Front;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
{
    public partial class NewestTopExercitationWidget : BaseFrontStudentUserControl,IShortWidget
    {
        private IStudentExercitationService Service
        {
            get
            {
                return new StudentExercitationService();
            }
        }

        protected override void InitData()
        {
            int totalCount;
            var list = Service.GetNewestFrontExercitationList(StudentInfo.StudentNum, out totalCount);
            if (!list.Any())
            {
                ltlEmptyMessage.Text = HtmlContentHelper.GetEmptyContent();
                linkmore.Visible = false;
            }
            else
            {
                rptActivity.DataSource = list.Select(it => new
                {
                    Title = it.Name.Cut(EnableShortContent, MaxContentLength).HtmlEncode(),
                    Url = UrlRuleHelper.GenerateUrl(StudentInfo.StudentNum, it.Identity, it.Name.Cut(200, ""), StudentRulePathType.Exercitation)
                }).ToList();
                rptActivity.DataBind();

                ltlRecordCount.Text = String.Format("({0})", totalCount);
            }
            linkmore.NavigateUrl = UrlRuleHelper.GenerateStudentMoreUrl(StudentInfo.StudentNum, StudentRulePathType.Exercitation);
        }

        public int MaxContentLength
        {
            get
            {
                if (this.ViewState["MaxContentLength"] == null)
                {
                    return 15;
                }
                return (int)this.ViewState["MaxContentLength"];
            }
            set { this.ViewState["MaxContentLength"] = value; }
        }

        public bool EnableShortContent
        {
            get
            {
                if (this.ViewState["EnableShortContent"] == null)
                {
                    return true;
                }
                return (bool)this.ViewState["EnableShortContent"];
            }
            set { this.ViewState["EnableShortContent"] = value; }
        }
    }
}