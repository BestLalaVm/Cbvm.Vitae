using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Business.Interface.University;
using LkDataContext;
using Microsoft.SqlServer.Server;
using Presentation.Criteria.Universoty;
using Presentation.UIView;
using Presentation.UIView.University;

namespace Business.Service.University
{
    public class UniversityMessageService : BaseService, IUniversityMessageService
    {
        public UniversityMessagePresentation Get(int id)
        {
            var message = dataContext.UniversityMessage.FirstOrDefault(it => it.ID == id);
            if (message == null)
            {
                return null;
            }

            return new UniversityMessagePresentation()
            {
                Id = message.ID,
                Title = message.Title,
                Content = message.Content,
                CreateTime = message.CreateTime,
                UniversityCode = message.UniversityCode,
                IsOnline = message.IsOnline,
                IsImportant = message.IsImportant,
                LastUpdateTime = message.LastUpdateTime,
                CategoryCode=message.UniversityMessageCategoryCode,
                CategoryName=message.UniversityMessageCategory.Name,
                IsTopTime=message.IsTopTime
            };
        }

        public ActionResult Save(UniversityMessagePresentation presentation)
        {
            var departMessage = dataContext.UniversityMessage.FirstOrDefault(it => it.ID == presentation.Id && it.UniversityCode==presentation.UniversityCode);
            if (departMessage == null)
            {
                departMessage = new UniversityMessage()
                {
                    CreateTime = DateTime.Now,
                    UniversityCode = presentation.UniversityCode
                };
                dataContext.UniversityMessage.InsertOnSubmit(departMessage);
            }
            if (presentation.IsTop)
            {
                if (!departMessage.IsTop)
                {
                    departMessage.IsTopTime = DateTime.Now;
                }
            }
            else
            {
                departMessage.IsTopTime = null;
            }

            departMessage.Title = presentation.Title;
            departMessage.Content = presentation.Content;
            departMessage.IsOnline = presentation.IsOnline;
            departMessage.LastUpdateTime = DateTime.Now;
            departMessage.IsImportant = presentation.IsImportant;
            departMessage.UniversityMessageCategoryCode = presentation.CategoryCode;
            departMessage.IsTop = presentation.IsTop;

            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public EntityCollection<UniversityMessagePresentation> GetAll(UniversityMessageCriteria criteria)
        {
            var query = from it in dataContext.UniversityMessage select it;
            if (!String.IsNullOrEmpty(criteria.Title))
            {
                query = from it in query where it.Title.Contains(criteria.Title.Trim()) select it;
            }

            if (!String.IsNullOrEmpty(criteria.UniversityCode))
            {
                query = from it in query where it.UniversityCode==criteria.UniversityCode select it;
            }

            if (!String.IsNullOrEmpty(criteria.CategoryCode))
            {
                query = query.Where(ix => ix.UniversityMessageCategoryCode == criteria.CategoryCode);
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.CreateTime), criteria, out totalCount);

            var list = query.Select(departMessage => new UniversityMessagePresentation()
            {
                Id = departMessage.ID,
                Title = departMessage.Title,
                Content = departMessage.Content,
                CreateTime = departMessage.CreateTime,
                UniversityCode = departMessage.UniversityCode,
                IsOnline = departMessage.IsOnline,
                LastUpdateTime = departMessage.LastUpdateTime,
                CategoryCode = departMessage.UniversityMessageCategoryCode,
                CategoryName = departMessage.UniversityMessageCategory.Name,
                IsImportant = departMessage.IsImportant,
                IsTop = departMessage.IsTop,
                IsTopTime = departMessage.IsTopTime
            }).ToList();

            EntityCollection<UniversityMessagePresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public ActionResult Delete(int id)
        {
            var departMessage = dataContext.UniversityMessage.FirstOrDefault(it => it.ID == id);
            if (departMessage == null)
            {
                return ActionResult.NotFoundResult;
            }

            dataContext.UniversityMessage.DeleteOnSubmit(departMessage);
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }


        public EntityCollection<UniversityMessagePresentation> GetTop20FrontMessageList()
        {

            return null;
            //var list = dataContext.GetTop20ArticleList().Select(it => new UniversityMessagePresentation()
            //{
            //    Id = it.ID,
            //    Title = it.Title,
            //    CreateTime = it.CreateTime,
            //    UniversityCode = it.UniversityCode,
            //    Content = it.Content,
            //    IsOnline = it.IsOnline,
            //    LastUpdateTime = it.LastUpdateTime
            //}).ToList();

            //var entityCollection = Translate2Presentations<UniversityMessagePresentation>(list);
            //return entityCollection;
        }


        public EntityCollection<MessageUiPresentation> GetFrontMessageList(int pageIndex, int pageSize)
        {
            var query = from it in dataContext.UniversityMessage where it.IsOnline select it;
            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.IsTopTime).ThenByDescending(ix=>ix.CreateTime), new UniversityMessageCriteria()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                NeedPaging = true
            }, out totalCount);

            var list = query.Select(departMessage => new MessageUiPresentation()
            {
                Id = departMessage.ID,
                Title = departMessage.Title,
                LastUpdateTime = departMessage.LastUpdateTime
            }).ToList();

            EntityCollection<MessageUiPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }


        public ActionResult SetStatus(int id, bool isOnline)
        {
            var departMessage = dataContext.UniversityMessage.FirstOrDefault(ic => ic.ID == id);
            if (departMessage == null) return ActionResult.NotFoundResult;
            departMessage.IsOnline = isOnline;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }


        public IQueryable<UniversityMessage> GetQuery()
        {
            return dataContext.UniversityMessage.AsQueryable();
        }


        public ActionResult SetIsTop(int id, bool isTop)
        {
            var departMessage = dataContext.UniversityMessage.FirstOrDefault(ic => ic.ID == id);
            if (departMessage == null) return ActionResult.NotFoundResult;
            if (isTop)
            {
                if (!departMessage.IsTop)
                {
                    departMessage.IsTopTime = DateTime.Now;
                }
            }
            else
            {
                departMessage.IsTopTime = null;
            }

            departMessage.IsTop = isTop;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
