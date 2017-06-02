using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentation.UIView;

namespace Cbvm.Vitae.Manage.UserControl
{
    public partial class CommentList : BaseUserControl
    {
        public void LoadData(IList<CommentPresentation> commentViewList)
        {
            if (commentViewList.Any())
            {
                this.phCommentEmpty.Visible = false;
                rptComment.DataSource = commentViewList;
                rptComment.DataBind();
            }
            else
            {
                this.phCommentEmpty.Visible = true;
            }
        }
    }
}