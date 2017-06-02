using System;
using System.Linq;
using System.Security.Cryptography;
using Business.Interface;
using Business.Interface.Family;
using LkDataContext;
using Presentation.Criteria.Family;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Family;
using Presentation.UIView.Family.View;
using WebLibrary;
using WebLibrary.Helper;

namespace Business.Service.Family
{
    public class FamilyService : BaseService, IFamilyService, IAuthenticateService
    {
        public FamilyPresentation Get(string userName)
        {
            var familyAccount = dataContext.StudentFamilyAccount.FirstOrDefault(it => it.UserName == userName);
            var presentation = new FamilyPresentation()
            {
                IsOnline = familyAccount.IsOnline,
                NameZh = familyAccount.NameZh,
                Relationship = familyAccount.Relationship,
                Sex = (SexType) familyAccount.Sex,
                StudentNameEn = familyAccount.Student.NameEn,
                StudentNameZh = familyAccount.Student.NameZh,
                StudentNum = familyAccount.Student.StudentNum,
                StudentIDentityNum = familyAccount.Student.IDentityNum,
                StudentTelephone = familyAccount.Student.Telephone,
                StudentThumbPath = familyAccount.Student.ThumbPath,
                StudentSex = (SexType)familyAccount.Student.Sex,
                NameEn = familyAccount.NameEn,
                UserName = familyAccount.UserName,
            };
            if (familyAccount.FamilyAccount != null)
            {
                presentation.AboutMe = familyAccount.FamilyAccount.AboutMe;
                presentation.Email = familyAccount.FamilyAccount.Email;
                presentation.Interested = familyAccount.FamilyAccount.Interested;
                presentation.Photo = familyAccount.FamilyAccount.Photo;
                presentation.Telephone = familyAccount.FamilyAccount.Telephone;
                presentation.ThumbPath = familyAccount.FamilyAccount.ThumbPath;
            }

            return presentation;
        }

        public ActionResult Save(FamilyPresentation familyPresentation)
        {
            var familyAccount =
                dataContext.StudentFamilyAccount.FirstOrDefault(it => it.StudentNum == familyPresentation.StudentNum);
            if (familyAccount == null)
            {
                familyAccount = new StudentFamilyAccount()
                {
                    StudentNum = familyPresentation.StudentNum,
                    CreateTime = DateTime.Now
                };
                dataContext.StudentFamilyAccount.InsertOnSubmit(familyAccount);
            }
            if (familyAccount.FamilyAccount == null)
            {
                familyAccount.FamilyAccount=new FamilyAccount();
            }
            familyAccount.FamilyAccount.LastUpdatedTime = DateTime.Now;
            familyAccount.IsOnline = familyPresentation.IsOnline;
            familyAccount.NameZh = familyPresentation.NameZh;
            familyAccount.Relationship = familyPresentation.Relationship;
            familyAccount.Sex = (int)familyPresentation.Sex;
            familyAccount.LastUpdatedTime = DateTime.Now;
            familyAccount.FamilyAccount.Interested = familyPresentation.Interested;
            familyAccount.FamilyAccount.AboutMe = familyPresentation.AboutMe;
            familyAccount.FamilyAccount.Telephone = familyPresentation.Telephone;
            familyAccount.FamilyAccount.ThumbPath = familyPresentation.ThumbPath;
            familyAccount.FamilyAccount.Photo = familyPresentation.Photo;
            familyAccount.FamilyAccount.Email = familyPresentation.Email;
            familyAccount.NameEn = familyPresentation.NameEn;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }


        public EntityCollection<FamilyForAdminPresentation> GetForAdminAll(FamilyCriteria criteria)
        {
            var query = from it in dataContext.StudentFamilyAccount select it;
            if (!String.IsNullOrEmpty(criteria.StudentName))
            {
                query = from it in query
                    where it.Student.NameZh.Contains(criteria.StudentName.Trim())
                    select it;
            }
            if (!String.IsNullOrEmpty(criteria.StudentNum))
            {
                query = from it in query
                        where it.StudentNum.Contains(criteria.StudentNum.Trim())
                    select it;
            }
            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = from it in query
                    where
                        it.NameZh.Contains(criteria.Name)
                    select it;
            }
            if (!String.IsNullOrEmpty(criteria.CollegeCode))
            {
                query = from it in query where it.Student.CollegeCode == criteria.CollegeCode select it;
            }


            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);
            var list = query.Select(ic => new FamilyForAdminPresentation()
            {
                Name = ic.NameZh,
                StudentIDentityNum = ic.Student.IDentityNum,
                StudentName = ic.Student.NameZh,
                StudentNum = ic.StudentNum,
                StudentTelephone = ic.Student.Telephone,
                StudentThumbPath = ic.Student.ThumbPath,
                UserName = ic.UserName,
                StudentSex = (SexType) ic.Student.Sex
            }).ToList();

            EntityCollection<FamilyForAdminPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        #region IAuthenticateService

        public LoginUserPresentation Login(string userName, string password)
        {
            var family =
                dataContext.StudentFamilyAccount.Where(
                    it => it.UserName == userName && it.Password == AccountSecurityManage.MD5Password(password)).Select(it=>new {
                       it.ID,
                       it.UserName,
                       it.NameZh,
                       it.StudentNum
                    }).FirstOrDefault();
            if (family != null)
            {
                var loginUser= new LoginUserPresentation()
                {
                    Id = family.ID,
                    UserName = family.UserName,
                    UserType = UserType.Family,
                    UserLabel = family.NameZh,
                    LogTime = DateTime.Now,
                    Identity = family.StudentNum,
                    FullName = String.Format("{0}家长",family.NameZh)
                };
                loginUser.AddtionalUser.Add("StudentNum", family.StudentNum);
                return loginUser;
            }
            return null;
        }

        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            try
            {
                var family =
                    dataContext.StudentFamilyAccount.FirstOrDefault(
                        it => it.UserName == userName && it.Password == AccountSecurityManage.MD5Password(oldPassword));
                if (family == null)
                {
                    return ActionResult.NotFoundResult;
                }
                family.Password = AccountSecurityManage.MD5Password(newPassword);
                dataContext.SubmitChanges();

                return ActionResult.DefaultResult;

                //QueryInfo info = new QueryInfo();
                //string md5OldPassword = Utility.UtilityHelper.MD5Password(oldPassword);
                //info.Parameters.Add("UserName", userName);
                //info.Parameters.Add("Password", md5OldPassword);
                //var studentFamilyAccount = base.Load<StudentFamilyAccount>(info);
                //if (studentFamilyAccount == null)
                //{
                //    return new ActionResult
                //    {
                //        IsSucess = false,
                //        Message = "原密码错误!"
                //    };
                //}
                //string md5NewPassword = Utility.UtilityHelper.MD5Password(newPassword);
                //studentFamilyAccount.Password = md5NewPassword;
                //studentFamilyAccount.SetState(EntityState.Modified);
                //base.Save<StudentFamilyAccount>(studentFamilyAccount);
                //return new ActionResult
                //{
                //    IsSucess = true,
                //    Message = "密码修改成功!"
                //};
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return ActionResult.CreateErrorActionResult(ex.ToString());
            }
        }

        public ActionResult ResetPassword(string userName, UserType userType, string checkCode)
        {
            if (CheckCodeHelper.ValidateCheckCode(userType.ToString(), checkCode))
            {
                var family = dataContext.StudentFamilyAccount.FirstOrDefault(it => it.UserName == userName);
                if (family != null)
                {
                    if (family.FamilyAccount != null && !String.IsNullOrEmpty(family.FamilyAccount.Email))
                    {
                        var newPassword = AccountSecurityManage.GenerateRadomPassword();
                        family.Password = AccountSecurityManage.MD5Password(newPassword);

                        dataContext.MailQueue.InsertOnSubmit(new MailQueue()
                        {
                            CreateTime = DateTime.Now,
                            Cc = null,
                            IsSended = false,
                            Name =
                                EmailTemplateHelper.FormatterResetPasswordContent(family.NameZh, userName, newPassword,
                                    EmailTemplateHelper.GetEmailTemplateSubject(SystemEmailType.ResetPassword)),
                            Sender = EmailTemplateHelper.MailSender,
                            Receiver = family.FamilyAccount.Email,
                            ReceiverName = family.FamilyAccount.Email,
                            Message =
                                EmailTemplateHelper.FormatterResetPasswordContent(family.NameZh, userName, newPassword,
                                    EmailTemplateHelper.GetEmailTemplateBody(SystemEmailType.ResetPassword)),
                        });
                        dataContext.SubmitChanges();

                        return new ActionResult()
                        {
                            Message = string.Format("密码重置成功! 已经发送至您的邮箱:{0}", family.FamilyAccount.Email)
                        };
                    }
                    return new ActionResult()
                    {
                        IsSucess = false,
                        Message = "用户未初始化或者Email为空,无法重置密码,请联系管理员!"
                    };
                }

                return new ActionResult
                {
                    IsSucess = false,
                    Message = "用户名无效!"
                };
            }
            return ActionResult.CreateErrorActionResult("验证码错误!");
        }
        #endregion


        public FamilyPresentation GetByStudentNum(string studentNum)
        {
            return dataContext.StudentFamilyAccount.Where(it => it.StudentNum == studentNum)
                .Select(it => new FamilyPresentation()
                {
                    IsOnline = it.IsOnline,
                    NameZh = it.NameZh,
                    Relationship = it.Relationship,
                    Sex = (SexType)it.Sex
                }).FirstOrDefault();
        }
    }
}
