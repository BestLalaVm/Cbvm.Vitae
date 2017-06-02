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
    public class CollegeAdminService : BaseService, IAuthenticateService, ICollegeAdminService
    {
        #region IAuthenticateService
        public LoginUserPresentation Login(string userName, string password)
        {
            var collegeAdmin = dataContext.CollegeAdmin
                    .FirstOrDefault(it => it.UserName == userName && it.Password == AccountSecurityManage.MD5Password(password));

            if (collegeAdmin != null)
            {
                return new LoginUserPresentation()
                {
                    Id = collegeAdmin.ID,
                    Identity = collegeAdmin.CollegeCode,
                    LogTime = DateTime.Now,
                    UserName = collegeAdmin.UserName,
                    UserLabel = collegeAdmin.UserName,
                    UserType = UserType.University
                };
            }

            return null;
        }

        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public ActionResult ResetPassword(string userName, UserType userType, string checkCode)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region ICollegeAdminService
        public CollegeAdminPresentation Get(int key)
        {
            return dataContext.CollegeAdmin.Where(it => it.ID == key).Select(it => new CollegeAdminPresentation
            {
                ID=it.ID,
                UserName = it.UserName,
                CreateTime = it.CreateTime
            }).FirstOrDefault();
        }

        public Presentation.UIView.Base.CollegeAdminPresentation Get(CollegeAdminCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public EntityCollection<Presentation.UIView.Base.CollegeAdminPresentation> GetAll(Presentation.Criteria.Base.CollegeAdminCriteria criteria)
        {
            var query = GetQuery();
            if (!String.IsNullOrEmpty(criteria.UserName))
            {
                query = query.Where(it => it.UserName.Contains(criteria.UserName));
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

        public IQueryable<Presentation.UIView.Base.CollegeAdminPresentation> GetQuery()
        {

            var query = dataContext.CollegeAdmin.Select(it => new CollegeAdminPresentation
            {
                ID = it.ID,
                UserName = it.UserName,
                CollegeName = it.College.Name,
                CreateTime = it.CreateTime,
                CollegeCode = it.CollegeCode
            }).AsQueryable();

            
            return query;
        }

        public ActionResult Save(Presentation.UIView.Base.CollegeAdminPresentation presentation)
        {
            var college = dataContext.CollegeAdmin.Where(it => it.ID == presentation.ID).FirstOrDefault();
            if (college == null)
            {
                college = new LkDataContext.CollegeAdmin()
                {
                    CreateTime = DateTime.Now,
                    CollegeCode = presentation.CollegeCode
                };

                dataContext.CollegeAdmin.InsertOnSubmit(college);
            }
            college.UserName = presentation.UserName;

            if (!String.IsNullOrEmpty(presentation.Password))
            {
                college.Password = AccountSecurityManage.MD5Password(presentation.Password);
            }

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public ActionResult SaveAll(IList<Presentation.UIView.Base.CollegeAdminPresentation> presentations)
        {
            presentations.ToList().ForEach(item =>
            {
                Save(item);
            });

            return ActionResult.DefaultResult;
        }

        public ActionResult DeleteByTKey(int key)
        {
            var collegeAdmin = dataContext.CollegeAdmin.FirstOrDefault(it => it.ID == key);

            if (collegeAdmin != null)
            {
                dataContext.CollegeAdmin.DeleteOnSubmit(collegeAdmin);

                dataContext.SubmitChanges();
            }

            return ActionResult.DefaultResult;
        }
        #endregion
    }
}
