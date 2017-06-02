using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using Business.Interface.DepartAdmin;
using Presentation.Enum;
using Presentation.UIView;
using WebLibrary;
using Business.Interface.College;
using Presentation.UIView.Base;
using Presentation.Criteria.Base;

namespace Business.Service.College
{
    public class CollegeService : BaseService, IAuthenticateService, ICollegeService
    {
        #region IAuthenticateService
        public LoginUserPresentation Login(string userName, string password)
        {
            var collegeAdmin = dataContext.CollegeAdmin
                    .Where(it => it.UserName == userName && it.Password == AccountSecurityManage.MD5Password(password))
                    .Select(it => new
                    {
                        it.ID,
                        it.CollegeCode,
                        it.UserName,
                        CollegeName = it.College.Name,
                        UniverityName=  it.College.University.Name
                    }).FirstOrDefault();

            if (collegeAdmin != null)
            {
                return new LoginUserPresentation()
                {
                    Id = collegeAdmin.ID,
                    Identity = collegeAdmin.CollegeCode,
                    LogTime = DateTime.Now,
                    UserName = collegeAdmin.UserName,
                    UserLabel = collegeAdmin.UserName,
                    UserType = UserType.College,
                    FullName = String.Format("{0}-{1}", collegeAdmin.UniverityName, collegeAdmin.CollegeName)
                };
            }

            return null;
        }

        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            try
            {
                var collegeAdmin =
                    dataContext.CollegeAdmin.FirstOrDefault(
                        it =>  it.Password == AccountSecurityManage.MD5Password(oldPassword) &&
                              it.UserName == userName);
                if (collegeAdmin == null)
                {
                    return ActionResult.NotFoundResult;
                }
                collegeAdmin.Password = AccountSecurityManage.MD5Password(newPassword);
                dataContext.SubmitChanges();

                return ActionResult.DefaultResult;
            }
            catch (Exception ex)
            {
                WriteLog(ex);

                return ActionResult.CreateErrorActionResult(ex.ToString());
            }
        }

        public ActionResult ResetPassword(string userName, UserType userType, string checkCode)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ICollegeService
        public CollegePresentation Get(string key)
        {
            return dataContext.College.Where(it => it.Code == key).Select(it => new CollegePresentation
            {
                Code=it.Code,
                Description=it.Description,
                Name=it.Name,
                CreateTime = it.CreateTime
            }).FirstOrDefault();
        }

        public Presentation.UIView.Base.CollegePresentation Get(CollegeCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public EntityCollection<Presentation.UIView.Base.CollegePresentation> GetAll(Presentation.Criteria.Base.CollegeCriteria criteria)
        {
            var query = GetQuery().Where(it => it.UniversityCode == criteria.UniversityCode);
            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(it => it.Name.Contains(criteria.Name));
            }
            query = query.OrderByDescending(it => it.CreateTime);

            var totalCount = 0;
            if (criteria.NeedPaging)
            {
                totalCount = query.Count();
                query = query.Skip(criteria.PageSize * criteria.PageIndex).Take(criteria.PageSize);
            }

            var result = Translate2Presentations(query.ToList());
            result.TotalCount = totalCount;

            return result;
        }

        public IQueryable<Presentation.UIView.Base.CollegePresentation> GetQuery()
        {

            var query = dataContext.College.Select(it => new CollegePresentation
            {
                Code = it.Code,
                Description = it.Description,
                Name = it.Name,
                CreateTime = it.CreateTime,
                UniversityCode = it.UniversityCode
            }).AsQueryable();

            
            return query;
        }

        public ActionResult Save(Presentation.UIView.Base.CollegePresentation presentation)
        {
            if (String.IsNullOrEmpty(presentation.Code))
            {
                presentation.Code = GenerateCodeHelper.GenerateCode(GenerateCodeHelper.GenerateCodeType.College, presentation.UniversityCode);
            }
            var college = dataContext.College.Where(it => it.Code == presentation.Code).FirstOrDefault();
            if (college == null)
            {
                college = new LkDataContext.College()
                {
                    Code = presentation.Code,
                    CreateTime = DateTime.Now,
                    UniversityCode=presentation.UniversityCode
                };

                dataContext.College.InsertOnSubmit(college);
            }
            college.Name = presentation.Name;
            college.Description = presentation.Description;

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult SaveAll(IList<Presentation.UIView.Base.CollegePresentation> presentations)
        {
            presentations.ToList().ForEach(item =>
            {
                Save(item);
            });

            return ActionResult.DefaultResult;
        }

        public ActionResult DeleteByTKey(string key)
        {
            var college = dataContext.College.FirstOrDefault(it => it.Code == key);

            if (college != null)
            {
                dataContext.College.DeleteOnSubmit(college);

                dataContext.SubmitChanges();
            }

            return ActionResult.DefaultResult;
        }
        #endregion
    }
}
