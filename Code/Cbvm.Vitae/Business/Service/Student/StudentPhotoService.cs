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
    public class StudentPhotoService : BaseService, IStudentPhotoService
    {
        public StudentPhotoPresentation Get(StudentPhotoCriteria criteria)
        {
            var photo =
                dataContext.StudentPhoto.FirstOrDefault(
                    it => it.ID == criteria.Id && it.StudentDictory.ID == criteria.DictoryId);
            if (photo == null)
            {
                return null;
            }
            return new StudentPhotoPresentation()
            {
                Id = photo.ID,
                DictoryId = photo.StudentDictoryId,
                Name = photo.Name,
                SmallPath = photo.SmallPath,
                ThumbPath = photo.ThumbPath,
                PhotoPath = photo.PhotoPath,
                CreateTime = photo.CreateTime
            };
        }

        public EntityCollection<StudentPhotoPresentation> GetAll(StudentPhotoCriteria criteria)
        {
            var query = from it in dataContext.StudentPhoto
                where it.StudentDictoryId == criteria.DictoryId && it.StudentDictory.StudentNum == criteria.StudentNum
                select it;
            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(photo => new StudentPhotoPresentation()
            {
                Id = photo.ID,
                DictoryId = photo.StudentDictoryId,
                Name = photo.Name,
                SmallPath = photo.SmallPath,
                ThumbPath = photo.ThumbPath,
                PhotoPath = photo.PhotoPath,
                CreateTime = photo.CreateTime
            }).ToList();

            EntityCollection<StudentPhotoPresentation> entityCollection = Translate2Presentations(list);

            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }


        public ActionResult SetMainPhoto(int photoId, string studentNum)
        {
            var photos =
                dataContext.StudentPhoto.Where(it => it.ID == photoId)
                    .SelectMany(ic => ic.StudentDictory.StudentPhoto)
                    .ToList();

            photos.ForEach(photo =>
            {
                photo.IsDictoryPhoto = false;
                if (photo.ID == photoId)
                {
                    photo.IsDictoryPhoto = true;
                }
            });

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }


        public ActionResult SaveAll(List<StudentPhotoPresentation> photos, int dictoryId)
        {
            photos.ForEach(ic =>
            {
                dataContext.StudentPhoto.InsertOnSubmit(new StudentPhoto()
                {
                    CreateTime = DateTime.Now,
                    LastUpdateTime = DateTime.Now,
                    StudentDictoryId = ic.DictoryId,
                    IsDictoryPhoto = false,
                    Name = ic.Name,
                    ThumbPath = ic.ThumbPath,
                    PhotoPath = ic.PhotoPath,
                    SmallPath = ic.SmallPath
                });
            });
            var defaultPhoto =
                dataContext.StudentPhoto.FirstOrDefault(it => it.StudentDictoryId == dictoryId && it.IsDictoryPhoto);
            if (defaultPhoto == null)
            {
                defaultPhoto = dataContext.StudentPhoto.FirstOrDefault(it => it.StudentDictoryId == dictoryId);
                if (defaultPhoto != null)
                {
                    defaultPhoto.IsDictoryPhoto = true;
                }
            }

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
