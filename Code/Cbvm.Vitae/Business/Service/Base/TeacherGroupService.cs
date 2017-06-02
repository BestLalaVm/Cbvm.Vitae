using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Base;
using Presentation.Criteria.Base;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Base;

namespace Business.Service.Base
{
    public class TeacherGroupService:BaseService,ITeacherGroupService
    {
        public TeacherGroupPresentation Get(string code)
        {
            var teacher = dataContext.TeacherGroup.FirstOrDefault(it => it.Code == code);
            if (teacher == null)
            {
                return null;
            }

            return new TeacherGroupPresentation()
            {
                Code=teacher.Code,
                Name = teacher.Name,
                Description = teacher.Description,
                TeacherGroupType = (TeacherGroupType)teacher.TeacherGroupType
            };
        }

        public ActionResult Save(TeacherGroupPresentation presentation)
        {
            var teacherGroup = dataContext.TeacherGroup.FirstOrDefault(ic => ic.Code == presentation.Code);
            if (teacherGroup == null)
            {
                teacherGroup = new LkDataContext.TeacherGroup
                {
                    UniversityCode = presentation.UniversityCode,
                    Code = presentation.Code,
                    CreateTime = DateTime.Now
                };

                dataContext.TeacherGroup.InsertOnSubmit(teacherGroup);
            }

            teacherGroup.Name = presentation.Name;
            teacherGroup.Description = presentation.Description;
            teacherGroup.LastUpdateTime = DateTime.Now;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public EntityCollection<TeacherGroupPresentation> GetAll(TeacherGroupCriteria criteria)
        {
            var query = from it in dataContext.TeacherGroup select it;
            if (!String.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(it => it.Name.Contains(criteria.Name.Trim()));
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.CreateTime), criteria, out totalCount);

            var list = query.Select(it => new TeacherGroupPresentation()
            {
                Name = it.Name,
                Description = it.Description,
                TeacherGroupType = (TeacherGroupType) it.TeacherGroupType,
                Code = it.Code,
                LastUpdateTime = it.LastUpdateTime
            }).ToList();

            EntityCollection<TeacherGroupPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }
    }
}
