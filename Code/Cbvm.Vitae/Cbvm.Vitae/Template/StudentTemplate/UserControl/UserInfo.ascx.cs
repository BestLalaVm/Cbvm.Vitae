using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LkHelper;
using Presentation.Cache;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Template.StudentTemplate.UserControl
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
                ltlVisitedCount.Text = StudentInfo.VisitedCount.ToString();

                lnkDetail.NavigateUrl = UrlRuleHelper.GenerateUrl(StudentInfo.StudentNum, StudentInfo.NameZh,"",
                                                                  StudentRulePathType.Detail);

                if (StudentInfo.IsOnline)
                {
                    phPublish.Visible = true;
                    ltlPeriod.Text = string.Format(" ({0}级)", StudentInfo.Period);
                    //ltlDepartName.Text = GlobalBaseDataCache.GetDepartName(StudentInfo.DepartCode);
                    ltlMarjorClass.Text = string.Format("{0}{1}", GlobalBaseDataCache.GetMarjorName(StudentInfo.MarjorCode),
                                                        StudentInfo.Class);
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
                        linkWebSite.Text = webSite.Cut(30, "...");
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