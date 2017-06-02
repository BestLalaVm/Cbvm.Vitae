using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using LkDataContext;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.Student;

namespace Business.Service
{
    public class PublishActivityService:BaseService,IPublishActivityService
    {
        public PublishActivityPresentation Get(PublishActivityCriteria criteria)
        {
            var activity =
                 dataContext.PublishActivity.FirstOrDefault(
                     it => it.ID == criteria.Id && it.ActivityType1.ActivityDataType !=
                           (int)ActivityDataType.Exercitation && !it.IsDelete);
            if (activity == null)
                return null;

            var presentation = new PublishActivityPresentation()
            {
                ActivityType = activity.ActivityType,
                Address = activity.Address,
                BeginTime = activity.BeginTime,
                EndTime = activity.EndTime,
                Content = activity.Content,
                Title = activity.Title,
                IsOnline = activity.IsOnline,
                Id = activity.ID,
                Publisher = activity.Publisher,
                PublisherType = (UserType) activity.PublisherType,
                IsReferenced = activity.StudentActivity.Any()
            };
            if (criteria.IncludeRelativeData)
            {
                presentation.AttachmentPresentations =
                    activity.PublishActivityAttachment.Select(it => new AttachmentPresentation()
                    {
                        ID = it.ID,
                        DisplayOrder = it.DisplayOrder,
                        DocumentType = (DocumentType)it.DocumentType,
                        Path = it.Path,
                        ThumbPath = it.ThumbPath,
                        FileLabel = it.Description,
                        IsMain = it.IsMain
                    }).ToList();
            }
            return presentation;
        }

        public EntityCollection<PublishActivityPresentation> GetAll(PublishActivityCriteria criteria)
        {
            var query = from it in dataContext.PublishActivity
                        where it.ActivityType1.ActivityDataType != (int)ActivityDataType.Exercitation
                        && !it.IsDelete
                        select it;

            if (!String.IsNullOrEmpty(criteria.Title))
            {
                query = from it in query where it.Title.Contains(criteria.Title.Trim()) select it;
            }

            if (criteria.DateFrom.HasValue)
            {
                query = from it in query
                        where it.BeginTime.Date >= criteria.DateFrom
                              && it.EndTime.Date >= criteria.DateFrom
                        select it;
            }

            if (criteria.DateTo.HasValue)
            {
                query = from it in query
                        where it.BeginTime.Date <= criteria.DateTo && it.EndTime.Date <= criteria.DateTo
                        select it;
            }

            query = query.OrderByDescending(it => it.CreateTime);

            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(activity => new PublishActivityPresentation()
            {
                ActivityType = activity.ActivityType,
                Address = activity.Address,
                BeginTime = activity.BeginTime,
                EndTime = activity.EndTime,
                Content = activity.Content,
                Title = activity.Title,
                IsOnline = activity.IsOnline,
                Publisher = activity.Publisher,
                PublisherType = (UserType) activity.PublisherType,
                Id = activity.ID,
                IsReferenced = activity.StudentActivity.Any()
            }).ToList();
            EntityCollection<PublishActivityPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public EntityCollection<PublishActivityPresentation> GetAllForStudent(PublishActivityCriteria criteria,string studentNum)
        {
            var query = from it in dataContext.PublishActivity
                        where it.ActivityType1.ActivityDataType != (int)ActivityDataType.Exercitation
                        && !it.IsDelete && it.IsOnline
                        select it;

            if (!String.IsNullOrEmpty(criteria.Title))
            {
                query = from it in query where it.Title.Contains(criteria.Title.Trim()) select it;
            }

            if (criteria.DateFrom.HasValue)
            {
                query = from it in query
                        where it.BeginTime.Date <= criteria.DateFrom
                              && it.EndTime.Date >= criteria.DateFrom
                        select it;
            }

            if (criteria.DateTo.HasValue)
            {
                query = from it in query
                        where it.BeginTime.Date <= criteria.DateTo && it.EndTime.Date >= criteria.DateFrom
                        select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query, criteria, out totalCount);

            var list = query.Select(activity => new PublishActivityPresentation()
            {
                ActivityType = activity.ActivityType,
                Address = activity.Address,
                BeginTime = activity.BeginTime,
                EndTime = activity.EndTime,
                Content = activity.Content,
                Title = activity.Title,
                IsOnline = activity.IsOnline,
                Publisher = activity.Publisher,
                PublisherType = (UserType) activity.PublisherType,
                Id = activity.ID,
                IsReferenced = activity.StudentActivity.Any(),
                Status =
                    activity.StudentActivity.Any(ix => ix.StudentNum == studentNum)
                        ? PublishAppliedStatus.Applied
                        : PublishAppliedStatus.None
            }).ToList();
            EntityCollection<PublishActivityPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Save(PublishActivityPresentation presentation)
        {
            var publishActivity =
                dataContext.PublishActivity.FirstOrDefault(
                    it => it.ID == presentation.Id);
            if (publishActivity == null)
            {
                publishActivity = new PublishActivity()
                {
                    CreateTime = DateTime.Now,
                    Publisher = presentation.Publisher,
                    PublisherType = (int)presentation.PublisherType
                };
                dataContext.PublishActivity.InsertOnSubmit(publishActivity);
            }
            publishActivity.Title = presentation.Title;
            publishActivity.Content = presentation.Content;
            publishActivity.ActivityType = presentation.ActivityType;
            publishActivity.Address = presentation.Address;
            publishActivity.BeginTime = presentation.BeginTime;
            publishActivity.EndTime = presentation.EndTime;
            publishActivity.IsOnline = presentation.IsOnline;


            foreach (var attachment in publishActivity.PublishActivityAttachment)
            {
                if (!presentation.AttachmentPresentations.Any(ic => ic.ID == attachment.ID))
                {
                    dataContext.PublishActivityAttachment.DeleteOnSubmit(attachment);
                }
            }

            foreach (var attachment in presentation.AttachmentPresentations.Where(ic=>ic.IsNew))
            {
                publishActivity.PublishActivityAttachment.Add(new PublishActivityAttachment()
                {
                    CreateTime = DateTime.Now,
                    Description = attachment.FileLabel,
                    DisplayOrder = attachment.DisplayOrder,
                    Path = attachment.Path,
                    ThumbPath = attachment.ThumbPath,
                    IsMain = attachment.IsMain,
                    DocumentType = (int) attachment.DocumentType
                });
            }

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }


        public ActionResult Delete(int id)
        {
            var activity = dataContext.PublishActivity.FirstOrDefault(it => it.ID == id);
            if (activity == null)
                return ActionResult.CreateErrorActionResult("找不到对应数据!");

            activity.IsDelete = true;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }


        public ActionResult ApplyPublishActivity2StudentNum(int id, string studentNum)
        {
            var publishActivity = dataContext.PublishActivity.FirstOrDefault(it => it.ID == id && it.IsOnline && !it.IsDelete);
            if (publishActivity != null)
            {
                var activity = new StudentActivity()
                {
                    ActivityType = publishActivity.ActivityType,
                    Address = publishActivity.Address,
                    BeginTime = publishActivity.BeginTime,
                    Content = publishActivity.Content,
                    CreateTime = DateTime.Now,
                    EndTime = publishActivity.EndTime,
                    ReferenceID = publishActivity.ID,
                    StudentNum = studentNum,
                    Title = publishActivity.Title,
                    TeacherNum = publishActivity.Publisher
                };
                publishActivity.PublishActivityAttachment.ToList().ForEach(it =>
                    {
                        activity.StudentActivityAttachment.Add(new StudentActivityAttachment
                        {
                            ActivityID = activity.ID,
                            CreateTime = DateTime.Now,
                            Description = it.Description,
                            DisplayOrder = it.DisplayOrder,
                            DocumentType = it.DocumentType,
                            IsMain = it.IsMain,
                            ThumbPath = it.ThumbPath,
                            Path = it.Path
                        });
                    });
                dataContext.StudentActivity.InsertOnSubmit(activity);
                dataContext.SubmitChanges();

                return ActionResult.DefaultResult;
            }

            return ActionResult.NotFoundResult;
        }

        public ActionResult SetStatus(string teacherNum, int id, bool isOnline)
        {
            var publishActivity =
                dataContext.PublishActivity.FirstOrDefault(
                    ic =>
                        ic.Publisher == teacherNum && ic.PublisherType == (int) UserType.Teacher && ic.ID == id &&
                        !ic.IsDelete);            
            if (publishActivity == null)
            {
                return ActionResult.NotFoundResult;
            }
            publishActivity.IsOnline = isOnline;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
