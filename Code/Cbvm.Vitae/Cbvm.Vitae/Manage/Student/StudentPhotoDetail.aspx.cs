using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentPhotoDetail : BaseStudentPage
    {
        private IStudentPhotoService Service
        {
            get { return new StudentPhotoService(); }
        }

        private IStudentPhotoCommentService CommentService
        {
            get { return new StudentPhotoCommentService(); }
        }

        protected int CurrentPictureId
        {
            get
            {
                var pictureId = 0;
                int.TryParse(Request.QueryString["picId"], out pictureId);
                return pictureId;
            }
        }

        protected int CurrentDictoryId
        {
            get
            {
                var dictoryId = 0;
                int.TryParse(Request.QueryString["dctId"], out dictoryId);
                return dictoryId;
            }
        }

        protected string CurrentDictoryName
        {
            get
            {
                return Request.QueryString["dctName"];
            }
        }

        protected StudentPhotoPresentation CurrentPicture
        {
            get
            {
                var picture = Service.Get(new StudentPhotoCriteria()
                {
                    StudentNum = CurrentUser.UserName,
                    Id = CurrentPictureId,
                    DictoryId = CurrentDictoryId
                });
                if (picture == null)
                {
                    Response.Redirect(String.Format("StudentDictoryManage.aspx?dctId={0}", CurrentDictoryId));
                    Response.End();
                }
                return picture;
            }
        }

        protected List<StudentPhotoPresentation> CurrentDictoryPhotoList
        {
            get
            {
                var list = this.ViewState["CurrentDictoryPhotoList"] as List<StudentPhotoPresentation>;
                if (list == null)
                {
                    list = Service.GetAll(new StudentPhotoCriteria()
                        {
                            DictoryId = CurrentDictoryId,
                            StudentNum = CurrentUser.UserName
                        });
                    this.ViewState["CurrentDictoryPhotoList"] = list;
                }
                return list;
            }
            set
            {
                this.ViewState["CurrentDictoryPhotoList"] = value;
            }
        }

        protected override void InitData()
        {
            rptPhoto.DataSource = CurrentDictoryPhotoList.Select(it => new
            {
                it.Id,
                it.Name,
                it.SmallPath,
                DictoryName = CurrentDictoryName,
                it.DictoryId,
                Selected = (it.Id == CurrentPictureId)
            }).OrderByDescending(it => it.Selected).ToList();
            rptPhoto.DataBind();

            LoadData();
            base.InitData();
        }

        private void LoadData()
        {
            var list = CommentService.GetAll(new StudentPhotoCommentCriteria()
            {
                PhotoId = CurrentPictureId,
                StudentNum = CurrentUser.UserName
            });
            cmtCommentList.LoadData(list.Select(it => new CommentPresentation()
            {
                Comment = it.Comment,
                CommentTime = it.CommentTime,
                UserName = it.UserName,
                UserType = it.UserType
            }).ToList());
        }

        protected void btnComment_Click(object sender, EventArgs e)
        {
            var result = CommentService.Save(new StudentPhotoCommentPresentation()
            {
                PhotoId = CurrentPictureId,
                DictoryId = CurrentDictoryId,
                UserName = CurrentUser.UserName,
                UserType = CurrentUser.UserType,
                Comment = txtComment.Text
            });
            if (result.IsSucess)
            {
                LoadData();
                txtComment.Text = "";
            }
        }

        protected void linkTop_Click(object sender, EventArgs e)
        {
            var result = Service.SetMainPhoto(CurrentPictureId, CurrentUser.UserName);
            ShowMsg(result.IsSucess, result.Message);
        }
    }
}