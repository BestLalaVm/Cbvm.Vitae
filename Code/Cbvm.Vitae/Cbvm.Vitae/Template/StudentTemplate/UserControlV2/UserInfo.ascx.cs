using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LkHelper;
using Presentation.Cache;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControlV2
{
    public partial class UserInfo : BaseFrontStudentUserControl
    {
        protected string URL
        {
            get
            {
                return UrlRuleHelper.GenerateUrl(StudentInfo.StudentNum, StudentInfo.NameZh, RulePathType.StudentInfo);
            }
        }

        protected override void InitData()
        {
            try
            {
                imgPhoto.ImageUrl = FileHelper.GetPersonAbsoluatePath(StudentInfo.Sex, StudentInfo.Photo, false);
                ltlNameEn.Text = StudentInfo.NameEn;
                ltlNameZh.Text = StudentInfo.NameZh;
                //ltlVisitedCount.Text = StudentInfo.VisitedCount.ToString();
                ltlCollegeName.Text = StudentInfo.CollegeName;
                lnkDetail.NavigateUrl = UrlRuleHelper.GenerateUrl(StudentInfo.StudentNum, StudentInfo.NameZh, "",
                                                                  StudentRulePathType.Detail);

                ltlPeriod.Text = string.Format(" ({0}级)", StudentInfo.Period);
                ltlMarjorClass.Text = string.Format("{0}{1}", GlobalBaseDataCache.GetMarjorName(StudentInfo.MarjorCode),
                                                    StudentInfo.Class);

                if (StudentInfo.IsOnline)
                {
                    phPublish.Visible = true;

                  
                    ltlNativePlace.Text = StudentInfo.NativePlace;
                    ltlPolitics.Text = GlobalBaseDataCache.GetPoliticsLabel(StudentInfo.Politics);
                    ltlTall.Text = String.IsNullOrEmpty(StudentInfo.Tall)
                                       ? ""
                                       : string.Format("{0} cm", StudentInfo.Tall);
                    ltlBirthday.Text = !StudentInfo.Birthday.HasValue
                                           ? ""
                                           : StudentInfo.Birthday.Value.ToCustomerShortDateString();
                    if (!string.IsNullOrEmpty(StudentInfo.WebSite))
                    {
                        var webSite = StudentInfo.WebSite.ToLower().StartsWith("http")
                                          ? StudentInfo.WebSite.ToLower()
                                          : string.Format("http://{0}", StudentInfo.WebSite.ToLower());
                        linkWebSite.NavigateUrl = webSite;
                        linkWebSite.Text = "点击打开主页";// webSite.Cut(30, "...");
                    }
                }
                else
                {
                    phPublish.Visible = false;
                }
            }
            catch
            {
                RedirectToDefaultPage();
            }

            base.InitData();
        }
    }
}