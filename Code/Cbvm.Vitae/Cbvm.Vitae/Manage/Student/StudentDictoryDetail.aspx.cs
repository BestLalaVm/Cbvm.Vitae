using System;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView.Student;

namespace Cbvm.Vitae.Manage.Student
{
    public partial class StudentDictoryDetail : BaseStudentDetailPage
    {
        private IStudentDictoryService Service
        {
            get
            {
                return new StudentDictoryService();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CurrentDictory.OpenType = GetStudentOpenType();
            CurrentDictory.Name = txtName.Text;
            CurrentDictory.Description = txtDescription.Text;
            var result= Service.Save(CurrentDictory);
            if (result.IsSucess)
            {
                CurrentDictoryId = CurrentDictory.Id.ToString();
            }
            ShowSelfMsg(result.IsSucess, result.Message);
        }

        protected override void InitData()
        {
            //GlobalSource.BindStudentOpenTypeInfo(false, chk_OpenType_);
            base.InitLoadedData();
        }

        private StudentOpenType GetStudentOpenType()
        {
            var openType = StudentOpenType.None;
            for (var index = 0; index < this.chk_OpenType_.Items.Count; index++)
            {
                if (this.chk_OpenType_.Items[index].Selected)
                {
                    int value = 0;
                    if (int.TryParse(chk_OpenType_.Items[index].Value, out value))
                    {
                        openType = openType | (StudentOpenType)value;
                    }
                }
            }
            return openType;
        }

        private string CurrentDictoryId
        {
            get
            {
                var dictoryId = this.ViewState["DictoryId"] as string;
                if (String.IsNullOrEmpty(dictoryId))
                {
                    dictoryId = Request.QueryString["DictoryId"];
                    this.ViewState["DictoryId"] = dictoryId;
                }
                return dictoryId;
            }
            set
            {
                this.ViewState["DictoryId"] = value;
            }
        }

        private StudentDictoryPresentation CurrentDictory
        {
            get
            {
                var dictory = this.ViewState["CurrentDictory"] as StudentDictoryPresentation;
                if (dictory == null)
                {
                    var dictoryId = 0;
                    if (int.TryParse(CurrentDictoryId, out dictoryId))
                    {
                        dictory = Service.Get(new StudentDictoryCriteria()
                        {
                            StudentNum = CurrentUser.UserName,
                            Id = dictoryId
                        });
                    }
                    this.ViewState["CurrentDictory"] = dictory;
                }
                return dictory;
            }
        }
    }
}