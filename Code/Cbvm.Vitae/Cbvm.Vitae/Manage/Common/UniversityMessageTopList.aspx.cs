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
    public partial class UniversityMessageTopList : BaseAccountPage
    {
        private IUniversityMessageService Service
        {
            get
            {
                return new UniversityMessageService();
            }
        }


        public IList<UniversityMessagePresentation> TopMessages
        {
            get
            {
                var list = this.ViewState["TopMessages"] as IList<UniversityMessagePresentation>;
                if (list == null)
                {
                    list = this.Service.GetQuery().Where(it => it.IsOnline && it.IsImportant).OrderByDescending(it => it.CreateTime).Select(it => new UniversityMessagePresentation
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
                    }).Take(8).ToList();

                    this.ViewState["TopMessages"] = list;
                }

                return list;
            }
        }
    }
}