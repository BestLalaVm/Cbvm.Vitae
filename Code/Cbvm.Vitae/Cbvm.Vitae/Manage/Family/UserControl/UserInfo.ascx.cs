using System;
using Business.Interface.Family;
using Business.Service.Family;
using Presentation.Enum;
using Presentation.UIView.Family;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Family.UserControl
{
    public partial class UserInfo : BaseUserControl
    {
        private IFamilyService Service
        {
            get
            {
                return new FamilyService();
            }
        }

        private FamilyPresentation Presentation
        {
            get
            {
                var familyPresentation = this.ViewState["FamilyPresentation"] as FamilyPresentation;
                if (familyPresentation == null)
                {
                    familyPresentation = Service.Get(CurrentUser.UserName);
                    this.ViewState["FamilyPresentation"] = familyPresentation;
                }
                return familyPresentation;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            Presentation.AboutMe = txt_AboutMe_.Text;
            Presentation.Interested = txt_Interested_.Text;
            Presentation.NameEn = txt_NameEn_.Text;
            Presentation.NameZh = txt_NameZh_.Text;
            Presentation.Telephone = txt_Telephone_.Text;
            Presentation.Email = txt_Email_.Text;
            Presentation.Sex = (SexType)int.Parse(rdo_Sex_.SelectedValue);
            var actionResult = Service.Save(Presentation);
            ShowMsg(actionResult.IsSucess, actionResult.Message);
        }

        protected override void BindData()
        {
            base.BindData();
            rdo_Sex_.BindSource(BindingSourceType.SexInfo, false);
        }

        protected override void InitData()
        {
            base.InitData();

            txt_UserName_.Text = Presentation.UserName;
            txt_AboutMe_.Text = Presentation.AboutMe;
            txt_Interested_.Text = Presentation.Interested;
            txt_NameEn_.Text = Presentation.NameEn;
            txt_NameZh_.Text = Presentation.NameZh;
            txt_StudentNum_.Text = Presentation.StudentNum;
            txt_StudentName_.Text = Presentation.StudentNameZh;
            txt_Telephone_.Text = Presentation.Telephone;
            txt_Email_.Text = Presentation.Email;
            rdo_Sex_.SelectedValue = ((int)Presentation.Sex).ToString();
            
            imgSource.ImageUrl = FileHelper.GetPersonAbsoluatePath(Presentation.Sex, Presentation.Photo, false);
        }

        protected override void OnInit(EventArgs e)
        {
            upLoadControl.FinishUploadingImageEventHandler += new BaseUploadControl.FinishUploadingImageEvent(upLoadControl_FinishUploadingImageEventHandler);
            base.OnInit(e);
        }

        protected void upLoadControl_FinishUploadingImageEventHandler(BaseUploadControl.UploadFileDataItem fileItem)
        {
            this.imgSource.ImageUrl = fileItem.FilePath;
            var thumbPath = FileHelper.GenerateRelativeThumbFilePath(MemberID.ToString(), UserType.Student,
                                                                     AttachmentType.BaseInfo, fileItem.FileName);
            FileHelper.DrawingUploadFile(fileItem.FileContent, FileHelper.GeneratePhysicalPath(thumbPath), 60, 50);
            Presentation.Photo = fileItem.FilePath;
            Presentation.ThumbPath = thumbPath;
        }
    }
}