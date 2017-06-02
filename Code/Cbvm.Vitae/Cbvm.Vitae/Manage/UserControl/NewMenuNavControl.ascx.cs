using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Service;
using Presentation;
using Presentation.Enum;
using WebLibrary;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.UserControl
{
    public partial class NewMenuNavControl : BaseUserControl
    {
        protected override void InitData()
        {
            IList<NavigateMenuItem> list = null;
            if (UserType == UserType.Enterprise)
            {
                var isPermission = (this.Page as BaseEnterprisePage).IsPermission;
                list = NavigateMenuItem.LoadNavigateMenuItemList(this.UserType, isPermission);
            }
            else
            {
                list = NavigateMenuItem.LoadNavigateMenuItemList(this.UserType, true);
            }
            if (this.UserType == UserType.Student || this.UserType == UserType.Family)
            {
                foreach (var item in list)
                {
                    if (item != null && item.NavigateType == NavigateItemType.Front)
                    {
                        if (this.UserType == UserType.Student)
                        {
                            item.LinkUrl = UrlRuleHelper.GenerateUrl(CurrentUser.UserName, "",
                                                                     RulePathType.StudentInfo);
                        }
                        else
                        {
                            if (CurrentUser.AddtionalUser.ContainsKey("StudentNum"))
                            {
                                var studentNum = CurrentUser.AddtionalUser["StudentNum"];
                                item.LinkUrl = UrlRuleHelper.GenerateUrl(studentNum, "",
                                                                         RulePathType.StudentInfo);
                            }
                        }
                    }
                    else
                    {
                        if (this.UserType == UserType.Student)
                        {
                            if (!String.IsNullOrEmpty(item.LinkUrl))
                            {
                                item.LinkUrl = item.LinkUrl.Replace(NavigateMenuItem.CurrentUserName,
                                                                    CurrentUser.UserName);
                            }
                        }
                        else
                        {
                            if (CurrentUser.AddtionalUser.ContainsKey("StudentNum"))
                            {
                                if (!String.IsNullOrEmpty(item.LinkUrl))
                                {
                                    var studentNum = CurrentUser.AddtionalUser["StudentNum"];
                                    item.LinkUrl = item.LinkUrl.Replace(NavigateMenuItem.CurrentUserName,
                                                                        studentNum);
                                }

                            }
                        }
                    }


                }
            }
            else if (this.UserType == Presentation.Enum.UserType.University)
            {
                var menu = list.Where(ix => ix.Text == NavigateMenuItem.UniversityMessageMenuName).FirstOrDefault();
                if (menu != null)
                {
                    menu.LinkUrl = "#";

                    var categories = (new Business.Service.University.UniversityMessageCategoryService()).GetOrigianlQuery<LkDataContext.UniversityMessageCategory>()
                        .Where(ix => ix.UniversityCode == CurrentUser.Identity).Select(ix => new
                        {
                            ix.Code,
                            ix.Name
                        }).ToList();

                    menu.NavigateMenuItemList = categories.Select(ix => new NavigateMenuItem
                    {
                        Text = ix.Name,
                        LinkUrl = String.Format("MessageManage.aspx?categoryCode={0}", ix.Code)
                    }).ToList();
                }
            }

            var msgBoxItem = list.FirstOrDefault(it => it.NavigateType == NavigateItemType.MessageBox);
            if (msgBoxItem != null && msgBoxItem.NavigateType == NavigateItemType.MessageBox)
            {
                var msgCount = (new MessageBoxService()).GetNewestMessageBoxCount(CurrentUser.UserName,
                                                                         CurrentUser.UserType);
                msgBoxItem.Text = String.Format(msgBoxItem.Text, msgCount);
            }

            rptNavigateStudent.DataSource = list.OrderBy(it => it.DisplayOrder).ToList();
            rptNavigateStudent.DataBind();

        }

        protected void rptNavigate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                var rptNavigateItems = e.Item.FindControl("rptNavigateItems") as Repeater;
                var dataItem = (e.Item.DataItem as NavigateMenuItem);
                rptNavigateItems.DataSource = dataItem.NavigateMenuItemList;
                rptNavigateItems.DataBind();
            }
        }
    }
}