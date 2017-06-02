using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;
using LkDataContext;
using Presentation;
using Presentation.Cache;
using Presentation.Criteria;
using Presentation.Enum;
using Presentation.UIView;
using Presentation.UIView.MessageBox;
using Presentation.UIView.MsgBox;

namespace Business.Service
{
    public class MessageBoxService : BaseService, IMessageBoxService
    {
        #region Old Code
        //public EntityCollection<MessageBoxItemViewPresentation> GetAll(MessageBoxCriteria criteria)
        //{
        //    var query = from it in dataContext.MessageBox
        //        where it.SenderKey == criteria.CurrentUser.UserName && it.SenderType == (int)criteria.CurrentUser.UserType
        //        select it;
        //    var totalCount = 0;
        //    query = PageingQueryable(query, criteria, out totalCount);

        //    var list = query.Select(it => new MessageBoxItemViewPresentation()
        //    {
        //        ID = it.ID,
        //        Content = it.Content,
        //        NewReplyCount = it.MessageBox.Count(),
        //        Time = it.CreateTime.ToString("yyyy-MM-dd hh:ss:mm")
        //    }).ToList();
        //    EntityCollection<MessageBoxItemViewPresentation> entityCollection = Translate2Presentations(list);
        //    entityCollection.TotalCount = totalCount;

        //    return entityCollection;
        //}

        //public MessageBoxItemPresentation Get(int id)
        //{
        //    var msgItem = dataContext.MessageBox.FirstOrDefault(it => it.ID == id);
        //    if (msgItem == null)
        //    {
        //        return null;
        //    }
        //    return Translate2Presentation(msgItem, true);
        //}

        //public MessageBoxPresentation GetMessageBoxInfo(int msgId, string receiverKey, UserType userType)
        //{
        //    var query = dataContext.MessageBox.Where(
        //        it => it.ReceiverKey == receiverKey && it.ReceiverType == (int) userType && it.ID == msgId);
        //    if (query.Count() == 0)
        //        return new MessageBoxPresentation()
        //        {
        //            MessageBoxItems = new List<MessageBoxItemPresentation>()
        //        };

        //    var messageBoxItemList =
        //        query.Select(it => GetSenderName(it, null))
        //            .ToList()
        //            .OrderByDescending(it => it.ID)
        //            .ToList();
        //    var msgInfo = new MessageBoxPresentation()
        //    {
        //        MessageBoxItems = messageBoxItemList
        //    };
        //    if (messageBoxItemList.Any())
        //    {
        //        //获取最大的回复消息ID
        //        msgInfo.MaxMessageBoxItemID = messageBoxItemList.Where(it => it.MessageBoxItems.Any())
        //            .Max(
        //                it => it.MessageBoxItems.Max(ic => ic.ID));
        //        //获取基于回复做出的答复的最大的ID
        //        var maxMsgReplyMsgID = messageBoxItemList.Where(it => it.MessageBoxItems.Any())
        //            .Max(
        //                it =>
        //                    it.MessageBoxItems.Where(
        //                        ic => ic.ReplyContents.Any())
        //                        .Max(ic => ic.ReplyContents.Max(ix => ix.ID)));
        //        if (msgInfo.MaxMessageBoxItemID < maxMsgReplyMsgID)
        //        {
        //            msgInfo.MaxMessageBoxItemID = maxMsgReplyMsgID;
        //        }

        //        //获取答复客户的消息的最大ID
        //        if (messageBoxItemList.Any(it => it.ReplyContents.Any()))
        //        {
        //            msgInfo.MaxReplyMessageBoxID =
        //                messageBoxItemList.Where(it => it.ReplyContents.Any())
        //                    .Max(it => it.ReplyContents.Max(ic => ic.ID));
        //        }
        //    }
        //    return msgInfo;
        //}

        //public MessageBoxPresentation GetMessageBoxInfoList(string receiverKey, UserType userType, int maxMessageBoxId)
        //{
        //    var query = dataContext.MessageBox.Where(
        //        it => it.ReceiverKey == receiverKey && it.ReceiverType == (int) userType && it.ReferenceID == null);
        //    if (!query.Any())
        //    {
        //        return new MessageBoxPresentation()
        //        {
        //            MessageBoxItems = new List<MessageBoxItemPresentation>()
        //        };
        //    }

        //    var messageBoxItemList =
        //        query.Select(it => GetSenderName(it, null))
        //            .ToList()
        //            .OrderByDescending(it => it.ID)
        //            .ToList();
        //    return new MessageBoxPresentation()
        //    {
        //        MessageBoxItems = messageBoxItemList,
        //        MaxMessageBoxItemID = query.Max(it => it.ID),
        //    };
        //}

        //private  IList<ReplayContentPresentation> GetReplayContentList(MessageBox messageBox)
        //{
        //    string thumbPath = null;
        //    switch ((UserType)messageBox.ReceiverType)
        //    {
        //        case UserType.Student:
        //            var student = dataContext.Student.FirstOrDefault(it => it.StudentNum == messageBox.ReceiverKey);
        //            thumbPath = student.ThumbPath;
        //            break;
        //        case UserType.Family:
        //            var family =
        //                dataContext.StudentFamilyAccount.FirstOrDefault(it => it.UserName == messageBox.ReceiverKey);
        //            thumbPath = family.FamilyAccount != null ? family.FamilyAccount.ThumbPath : "";
        //            break;
        //        case UserType.Teacher:
        //            var teacher = dataContext.Teacher.FirstOrDefault(it => it.TeacherNum == messageBox.ReceiverKey);
        //            thumbPath = teacher.ThumbPath;
        //            break;
        //    }
        //    var replyContentList =
        //        dataContext.MessageBox.Where(it => it.ReferenceID == messageBox.ID).OrderByDescending(it => it.ID)
        //                   .ToList()
        //                   .Select(it => new ReplayContentPresentation()
        //                   {
        //                       Content = it.Content,
        //                       ReplyTime = it.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"),
        //                       ThumbPath = thumbPath,
        //                       ID = it.ID
        //                   });
        //    return replyContentList.ToList();
        //}

        //private  MessageBoxItemPresentation GetSenderName(MessageBox messageBox, int? refReplyID)
        //{
        //    string senderKey = messageBox.SenderKey;
        //    UserType userType = (UserType)messageBox.SenderType;
        //    MessageBoxItemPresentation messageBoxInfo = new MessageBoxItemPresentation()
        //    {
        //        Subject = messageBox.Subject,
        //        Content = refReplyID.HasValue ? String.Format("答复({0}):{1}", refReplyID.Value, messageBox.Content) : messageBox.Content,
        //        CreateTime = messageBox.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"),
        //        UserType = userType.ToString(),
        //        UserTypeLabel =
        //            GlobalBaseDataCache.UserTypeList.FirstOrDefault(it => it.Value == messageBox.SenderType.ToString()).Text,
        //        ID = messageBox.ID,
        //        MessageBoxItems =
        //            dataContext.MessageBox.Where(
        //                it =>
        //                (it.TopRefID == messageBox.ID || it.ReferenceID == messageBox.ID) &&
        //                it.ReceiverKey == messageBox.ReceiverKey &&
        //                it.ReceiverType == messageBox.ReceiverType).OrderBy(it => it.ID).ToList()
        //                       .Select(
        //                           it =>
        //                           GetSenderName(it, ((it.TopRefID == messageBox.ID) ? it.ReferenceID : null)))
        //                       .ToList(),
        //        ReplyContents = GetReplayContentList(messageBox)
        //    };

        //    //GlobalCache.UserTypeList.FirstOrDefault(it => it.Value == messageBox.SenderType.ToString()).Text
        //    switch (userType)
        //    {
        //        case UserType.Student:
        //            var student = dataContext.Student.FirstOrDefault(it => it.StudentNum == senderKey);
        //            messageBoxInfo.ThumbPath = student.ThumbPath;
        //            messageBoxInfo.SenderName = student.StudentNum;
        //            break;
        //        case UserType.Family:
        //            var family = dataContext.StudentFamilyAccount.FirstOrDefault(it => it.UserName == senderKey);
        //            messageBoxInfo.ThumbPath = family.FamilyAccount != null
        //                                           ? family.FamilyAccount.ThumbPath
        //                                           : "";
        //            messageBoxInfo.SenderName = family.UserName;
        //            break;
        //        case UserType.Teacher:
        //            var teacher = dataContext.Teacher.FirstOrDefault(it => it.TeacherNum == senderKey);
        //            messageBoxInfo.ThumbPath = teacher.ThumbPath;
        //            messageBoxInfo.SenderName = teacher.TeacherNum;
        //            break;
        //    }

        //    if (
        //        dataContext.MessageBox.Any(
        //            it => (it.ID == messageBox.ID || it.ReferenceID == messageBox.ID) && !it.IsReaded))
        //    {
        //        dataContext.MessageBox.Where(
        //            it => (it.ReferenceID == messageBox.ID || it.ID == messageBox.ID) && !it.IsReaded)
        //                   .ToList()
        //                   .ForEach(it => it.IsReaded = true);
        //        dataContext.SubmitChanges();
        //    }

        //    return messageBoxInfo;
        //}

        //public ActionResult SaveMessageBoxInfo(string senderName, UserType senderType, string receiverName,
        //    UserType receiverType, string subject, string message,
        //    int refMessageId)
        //{
        //    var refMessageBox = dataContext.MessageBox.FirstOrDefault(it => it.ID == refMessageId);
        //    dataContext.MessageBox.InsertOnSubmit(new MessageBox()
        //    {
        //        ReceiverKey = receiverName,
        //        ReceiverType = (int) receiverType,
        //        SenderKey = senderName,
        //        SenderType = (int) senderType,
        //        Subject = subject,
        //        Content = message,
        //        CreateTime = DateTime.Now,
        //        MessageBox1 = refMessageBox,
        //        MessageBox2 = refMessageBox.MessageBox1
        //    });
        //    dataContext.SubmitChanges();

        //    return ActionResult.DefaultResult;
        //}

        //public IList<MessageBoxItemViewPresentation> GetMessageBoxInfoList(string receiverKey, UserType receiverType)
        //{
        //    int index = 1;
        //    var messageBoxList = dataContext.MessageBox.Where(
        //        it =>
        //            it.ReceiverType == (int) receiverType && it.ReceiverKey == receiverKey && it.ReferenceID == null)
        //        .OrderByDescending(it => it.ID).ToList()
        //        .Select(it => new MessageBoxItemViewPresentation()
        //        {
        //            ID = it.ID,
        //            Content = it.Content,
        //            Time = it.CreateTime.ToString("yyyy-MM-dd hh:ss:mm"),
        //            NewReplyCount = it.MessageBox.Count(ic => !ic.IsReaded)
        //        }).ToList();
        //    messageBoxList.ForEach(it =>
        //    {
        //        it.Index = index++;
        //    });
        //    return messageBoxList;
        //}

        //public int GetNewestMessageBoxCount(string receiverKey, UserType receiverType)
        //{
        //    return
        //        dataContext.MessageBox.Count(
        //            it => it.ReceiverKey == receiverKey && it.ReceiverType == (int) receiverType && !it.IsReaded);
        //}

        //public EntityCollection<MessageBoxViewPresentation> GetViewAll(MessageBoxCriteria criteria)
        //{
        //    var query = from it in dataContext.MessageBox
        //        where it.ReceiverKey == criteria.UserName && it.ReceiverType == (int) criteria.ReceiverType
        //        select it;

        //    if (!String.IsNullOrEmpty(criteria.Subject))
        //    {
        //        query = from it in query where it.Subject.Contains(criteria.Subject.Trim()) select it;
        //    }
        //    int totalCount = query.Count();

        //    var list =
        //        query.Skip((criteria.PageIndex - 1)*criteria.PageSize)
        //            .Take(criteria.PageSize)
        //            .Select(it => new MessageBoxViewPresentation()
        //            {
        //                IsReaded = it.IsReaded,
        //                NewReplyCount = (dataContext.MessageBox.Where(ic => ic.ReferenceID == it.ID).Count()),
        //                ReceiverKey = it.ReceiverKey,
        //                ReceiverType = (UserType) it.ReceiverType,
        //                SenderKey = it.SenderKey,
        //                SenderType = (UserType) it.SenderType,
        //                Subject = it.Subject,
        //                Time = it.CreateTime,
        //                SenderName = GetSenderName(it)
        //            }).ToList();

        //    EntityCollection<MessageBoxViewPresentation> entityCollection = Translate2Presentations(list);
        //    entityCollection.TotalCount = totalCount;

        //    return entityCollection;
        //}

        //private MessageBoxItemPresentation Translate2Presentation(MessageBox box,bool includeRelativeData)
        //{
        //    return new MessageBoxItemPresentation()
        //    {
        //        Subject=box.Subject,
        //        Content = box.Content,
        //        CreateTime = box.CreateTime.ToShortDateString(),
        //        ID = box.ID,
        //        SenderName=box.SenderKey
        //    };
        //}

        //private string GetSenderName(MessageBox msgBox)
        //{
        //    switch ((UserType) msgBox.SenderType)
        //    {
        //        case UserType.Student:
        //            return dataContext.Student.FirstOrDefault(ic => ic.StudentNum == msgBox.SenderKey).NameZh;
        //        case UserType.Teacher:
        //            return dataContext.Teacher.FirstOrDefault(ic => ic.TeacherNum == msgBox.SenderKey).NameZh;
        //        case UserType.Enterprise:
        //            return dataContext.Enterprise.FirstOrDefault(ic => ic.UserName == msgBox.SenderKey).Name;
        //        case UserType.Family:
        //            return
        //                dataContext.StudentFamilyAccount.FirstOrDefault(ic => ic.UserName == msgBox.SenderKey).NameZh;
        //        default:
        //            return EnumHelper.GetEnumDescription((UserType) msgBox.SenderType);
        //    }
        //}
        #endregion

        public EntityCollection<MessageBoxPresentation> GetAll(MessageBoxCriteria criteria, string receiverKey,
            UserType receiverType)
        {
            var query = from it in dataContext.MessageBox
                where
                    (it.SenderKey == receiverKey && it.SenderType == (int) receiverType) ||
                    (it.ReceiverKey == receiverKey && it.ReceiverType == (int) receiverType)
                select it;

            if (!String.IsNullOrEmpty(criteria.Subject))
            {
                query = from it in query where it.Subject.Contains(criteria.Subject) select it;
            }

            if (criteria.DateFrom.HasValue)
            {
                query = from it in query where it.CreateTime.Date >= criteria.DateFrom select it;
            }

            if (criteria.DateTo.HasValue)
            {
                query = from it in query where it.CreateTime.Date <= criteria.DateTo select it;
            }
            if (!String.IsNullOrEmpty(criteria.IsReaded))
            {
                if (criteria.IsReaded == "1")
                {
                    query = from it in query
                        where it.IsReaded
                        select it;
                }
                else
                {
                    query = from it in query
                        where !it.IsReaded
                        select it;
                }
            }
            if (criteria.MsgType == "1")
            {
                query = from it in query
                        where it.ReceiverKey == receiverKey && it.ReceiverType == (int)receiverType
                        select it;
            }
            if (criteria.MsgType == "2")
            {
                query = from it in query
                        where it.SenderKey == receiverKey && it.SenderType == (int)receiverType
                        select it;
            }

            if (criteria.MsgType == "0")
            {
                query = from it in query
                        where (it.SenderKey == receiverKey && it.SenderType == (int)receiverType)
                              || (it.ReceiverKey == receiverKey && it.ReceiverType == (int)receiverType)
                        select it;
            }

            int totalCount = 0;
            query = PageingQueryable(query.OrderByDescending(it => it.CreateTime),
                criteria, out totalCount);
            var list = query.Select(ix => new MessageBoxPresentation()
            {
                CreateTime = ix.CreateTime,
                Id = ix.ID,
                ReceiverKey = ix.ReceiverKey,
                ReceiverType = (UserType)ix.ReceiverType,
                SenderKey = ix.SenderKey,
                SenderType = (UserType)ix.SenderType,
                Subject = ix.Subject,
                IsReaded = ix.IsReaded
            }).ToList();

            EntityCollection<MessageBoxPresentation> entityCollection = Translate2Presentations(list);
            entityCollection.TotalCount = totalCount;

            return entityCollection;
        }

        public MessageBoxPresentation Get(int id)
        {
            var messageBox = dataContext.MessageBox.FirstOrDefault(ix => ix.ID == id);
            if (messageBox == null) return null;
            return new MessageBoxPresentation()
            {
                Content = messageBox.Content,
                CreateTime = messageBox.CreateTime,
                Id = messageBox.ID,
                ReceiverKey = messageBox.ReceiverKey,
                ReceiverType = (UserType)messageBox.ReceiverType,
                SenderKey = messageBox.SenderKey,
                SenderType = (UserType)messageBox.SenderType,
                Subject = messageBox.Subject,
                IsReaded = messageBox.IsReaded
            };
        }

        public ActionResult Send(string senderUserName, UserType senderType, string receiverUserName, UserType receiverType, string subject, string content)
        {
            dataContext.MessageBox.InsertOnSubmit(new MessageBox()
            {
                SenderKey = senderUserName,
                SenderType = (int)senderType,
                ReceiverKey = receiverUserName,
                ReceiverType = (int)receiverType,
                CreateTime = DateTime.Now,
                Subject = subject,
                Content = content
            });
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }

        public int GetNewestMessageBoxCount(string receiverUserName, UserType receiverType)
        {
            var count =
                dataContext.MessageBox.Count(
                    ix => ix.ReceiverKey == receiverUserName && ix.ReceiverType == (int)receiverType && !ix.IsReaded);

            return count;
        }


        public ActionResult SetReaded(int id)
        {
            var msg = dataContext.MessageBox.FirstOrDefault(ix => ix.ID == id);
            if (msg == null) return ActionResult.NotFoundResult;
            msg.IsReaded = true;
            dataContext.SubmitChanges();

            return ActionResult.DefaultResult;
        }
    }
}
