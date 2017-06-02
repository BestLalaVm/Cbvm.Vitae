using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Interface.University;
using Business.Service.University;
using Presentation.Criteria.Universoty;
using Presentation.UIView;
using Presentation.UIView.University;
using Telerik.Web.UI;
using LkDataContext;
using Cbvm.Vitae.Manage.UserControl;

namespace Cbvm.Vitae.Manage.Common
{
    public partial class UniversityMessageDetail : BaseAccountPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private IUniversityMessageService Service
        {
            get
            {
                return new UniversityMessageService();
            }
        }

        private int MessageId
        {
            get
            {
                int id;
                int.TryParse(Request.QueryString["Id"], out id);

                return id;
            }
        }

        public UniversityMessagePresentation MessageItem
        {
            get
            {
                var item = this.ViewState["MessageItem"] as UniversityMessagePresentation;
                if (item == null)
                {
                    item = this.Service.GetQuery().Where(it => it.IsOnline && it.IsImportant).Where(it => it.ID == MessageId).OrderByDescending(it => it.CreateTime).Select(it => new UniversityMessagePresentation
                    {
                        Id = it.ID,
                        Title = it.Title,
                        Content = it.Content,
                        CreateTime = it.CreateTime,
                        UniversityCode = it.UniversityCode,
                        IsOnline = it.IsOnline,
                        LastUpdateTime = it.LastUpdateTime,
                        CategoryCode = it.UniversityMessageCategoryCode,
                        CategoryName = it.UniversityMessageCategory.Name,
                        IsImportant = it.IsImportant
                    }).FirstOrDefault();
                    if (item == null)
                    {
                        Response.Redirect("/Manage/Common/UniversityMessageTopList.aspx");
                        Response.End();
                    }

                    this.ViewState["MessageItem"] = item;
                }


                return item;
            }
        }
    }
}