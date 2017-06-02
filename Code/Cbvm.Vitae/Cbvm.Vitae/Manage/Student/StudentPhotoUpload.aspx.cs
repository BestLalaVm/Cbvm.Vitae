using System;
using System.Collections.Generic;
using System.Linq;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Enum;
using Presentation.UIView;

using System.Text.RegularExpressions;
using Presentation.UIView.Student;
using WebLibrary.Helper;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentPhotoUpload : BaseStudentPage
    {
        private const string File_Key = "FileKey";
        private static Regex regex = new Regex(@"[A-Za-z0-9]+_(\w+)");

        private IStudentPhotoService Service
        {
            get { return new StudentPhotoService(); }
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

        protected override void InitLoadedData()
        {
            IList<UploadFileItemPresentation> postDatas = new List<UploadFileItemPresentation>();

            foreach (var key in Request.Form.AllKeys)
            {
                var match = regex.Match(key).Groups[1];
                if (match.Success)
                {
                    var fileBase = Request.Files[match.Value];
                    var filePath = FileHelper.GenerateRelativeFilePath(MemberID.ToString(), UserType.Student, AttachmentType.Photo, fileBase.FileName);
                    var thumbPath = FileHelper.GenerateRelativeThumbFilePath(MemberID.ToString(), UserType.Student, AttachmentType.Photo, fileBase.FileName);
                    var smallPath = FileHelper.GenerateRelativeSmallFilePath(MemberID.ToString(), UserType.Student, AttachmentType.Photo, fileBase.FileName);
                    FileHelper.DrawingUploadFile(fileBase.InputStream, FileHelper.GeneratePhysicalPath(thumbPath), 600, 500);
                    FileHelper.DrawingUploadFile(fileBase.InputStream, FileHelper.GeneratePhysicalPath(smallPath), 180, 180);
                    fileBase.SaveAs(FileHelper.GeneratePhysicalPath(filePath));

                    postDatas.Add(new UploadFileItemPresentation()
                    {
                        Path = filePath,
                        ThumbPath = thumbPath,
                        SmallPath=smallPath,
                        Name = Request.Form[key]
                    });
                }
            }
            var result= Service.SaveAll(postDatas.Select(ic => new StudentPhotoPresentation()
            {
                DictoryId = CurrentDictoryId,
                ThumbPath = ic.ThumbPath,
                SmallPath = ic.SmallPath,
                Name = ic.Name,
                PhotoPath = ic.Path,
            }).ToList(),CurrentDictoryId);

            if (result.IsSucess)
            {
                Response.Redirect("StudentPhotoManage.aspx?dctId=" + CurrentDictoryId);
            }
            base.InitLoadedData();
        }

        protected override void InitData()
        {
            this.Page.Form.Attributes.Add("enctype", "multipart/form-data");
            base.InitData();
        }
    }
}