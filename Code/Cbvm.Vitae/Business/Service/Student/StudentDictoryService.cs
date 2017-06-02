using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.Student;
using LkDataContext;
using Presentation.Criteria.Student;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace Business.Service.Student
{
    public class StudentDictoryService : BaseService, IStudentDictoryService
    {
        public StudentDictoryPresentation Get(StudentDictoryCriteria criteria)
        {
            var dictory =
                dataContext.StudentDictory.FirstOrDefault(
                    it => it.ID == criteria.Id && it.StudentNum == criteria.StudentNum);
            if (dictory == null) return null;

            return new StudentDictoryPresentation()
            {
                Name = dictory.Name,
                Description = dictory.Description,
                OpenType = (StudentOpenType) dictory.OpenType,
                StudentNum = dictory.StudentNum,
                ThumbPath = dictory.StudentPhoto.FirstOrDefault(it => it.IsDictoryPhoto).ThumbPath
            };
        }

        public ActionResult Save(StudentDictoryPresentation presentation)
        {
            var dictory =
                dataContext.StudentDictory.FirstOrDefault(
                    it => it.ID == presentation.Id
                          && it.StudentNum == presentation.StudentNum);
            if (dictory == null)
            {
                dictory = new StudentDictory()
                {
                    CreateTime = DateTime.Now,
                    StudentNum = presentation.StudentNum
                };
                dataContext.StudentDictory.InsertOnSubmit(dictory);
            }

            dictory.Name = presentation.Name;
            dictory.Description = presentation.Description;
            dictory.OpenType = (int) presentation.OpenType;
            dictory.LastUpdateTime = DateTime.Now;

            dataContext.SubmitChanges();

            presentation.Id = dictory.ID;
            return ActionResult.DefaultResult;
        }


        public EntityCollection<StudentDictoryPresentation> GetAll(StudentDictoryCriteria criteria)
        {
            var query = from it in dataContext.StudentDictory where it.StudentNum == criteria.StudentNum select it;
            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(it => new StudentDictoryPresentation()
            {
                Id = it.ID,
                Name = it.Name,
                Description = it.Description,
                OpenType = (StudentOpenType) it.OpenType,
                StudentNum = it.StudentNum,
                PhotoCount = (criteria.IncludePhotoCount ? it.StudentPhoto.Count : 0),
                ThumbPath = it.StudentPhoto.FirstOrDefault(ic => ic.IsDictoryPhoto).ThumbPath
            }).ToList();

            EntityCollection<StudentDictoryPresentation> entityCollection = Translate2Presentations(list);

            entityCollection.TotalCount = totalCount;
            return entityCollection;
        }
    }
}
