using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Presentation.UIView.Student;
using Presentation.UIView.Teacher.View;

namespace Cbvm.Vitae
{
    public  static partial class SessionHelper
    {
        private const string RECOMMEND_SESSION_NAME = "RECOMMEND_SESSION_NAME";

        private const string RECOMMEND_STUDENT_SESSION_NAME = "RECOMMEND_STUDENT_SESSION_NAME";

        private const string RECOMMEND_ENTERPRISE_SESSION_NAME = "RECOMMEND_ENTERPRISE_SESSION_NAME";

        private const string RECOMMEND_COLLEGE_SESSION_NAME = "RECOMMEND_COLLEGE_SESSION_NAME";

        public static void AddRecommendStudentToSession(this HttpSessionState session,
                                                        List<StudentPresentation> studentPresentations)
        {
            var stuPresentationList = session[RECOMMEND_SESSION_NAME] as List<StudentPresentation>;
            if (stuPresentationList == null)
            {
                stuPresentationList = new List<StudentPresentation>();
            }
            studentPresentations.ForEach(it =>
                {
                    if (!stuPresentationList.Any(ic => ic.StudentNum == it.StudentNum))
                    {
                        stuPresentationList.Add(it);
                    }
                });

            session[RECOMMEND_SESSION_NAME] = stuPresentationList;
        }

        public static void UpdateRecommendStudentToSession(this HttpSessionState session,
                                                List<StudentPresentation> studentPresentations)
        {
            if (studentPresentations == null)
            {
                studentPresentations = new List<StudentPresentation>();
            }

            session[RECOMMEND_SESSION_NAME] = studentPresentations;
        }

        public static List<StudentPresentation> GetRecommendStudentFromSession(this HttpSessionState session)
        {
            var list = session[RECOMMEND_SESSION_NAME] as List<StudentPresentation>;
            if (list == null)
            {
                list = new List<StudentPresentation>();
            }
            return list;
        }

        public static void AddRecommendSingleStudentToSession(this HttpSessionState session,
                                               StudentPresentation studentPresentation)
        {
            var stuPresentation = session[RECOMMEND_STUDENT_SESSION_NAME] as StudentPresentation;

            session[RECOMMEND_STUDENT_SESSION_NAME] = stuPresentation;
        }

        public static StudentPresentation GetRecommendSingleStudentFromSession(this HttpSessionState session)
        {
            var student = session[RECOMMEND_STUDENT_SESSION_NAME] as StudentPresentation;

            return student;
        }



        public static void ClearRecommendCache(this HttpSessionState session)
        {
            session.Remove(RECOMMEND_SESSION_NAME);

            session.Remove(RECOMMEND_STUDENT_SESSION_NAME);

            session.Remove(RECOMMEND_ENTERPRISE_SESSION_NAME);

            session.Remove(RECOMMEND_COLLEGE_SESSION_NAME);
        }

        public static void AddEntityToSession<T>(this HttpSessionState session, IList<T> presentations)
        {
            var sessionName = typeof(T).FullName;

            //var presentationList = session[sessionName] as List<T>;
            //if (presentationList == null)
            //{
            //    presentationList = new List<T>();
            //}
            //presentationList.ForEach(it =>
            //{
            //    if (!presentationList.Any(ic => ic.StudentNum == it.StudentNum))
            //    {
            //        presentationList.Add(it);
            //    }
            //});

            session[sessionName] = presentations;
        }

        public static List<T> GetEntityFromSession<T>(this HttpSessionState session)
        {
            var sessionName = typeof(T).FullName;

            var list = session[sessionName] as List<T>;
            if (list == null)
            {
                list = new List<T>();
            }

            return list;
        }

        public static void ClearEntityFromSession<T>(this HttpSessionState session)
        {
             var sessionName = typeof(T).FullName;
             session.Remove(sessionName);
        }

    }

    partial class SessionHelper
    {
        private const string RECOMMEND_TEACHER_SESSION_NAME = "RECOMMEND_TEACHER_SESSION_NAME";
        private const string RECOMMEND_TEACHER_LIST_SESSION_NAME = "RECOMMEND_TEACHER_LIST_SESSION_NAME";
        public static void AddRecommendSingleTeacherToSession(this HttpSessionState session,
                                       TeacherCommandPresentation presentation)
        {
            session[RECOMMEND_TEACHER_SESSION_NAME] = presentation;
        }

        public static TeacherCommandPresentation GetRecommendSingleTeacherFromSession(this HttpSessionState session)
        {
            var teacher = session[RECOMMEND_TEACHER_SESSION_NAME] as TeacherCommandPresentation;

            return teacher;
        }


        public static void AddRecommendTeachersToSession(this HttpSessionState session,
                                                        List<TeacherCommandPresentation> teacherPresentations)
        {
            var teacherPresentationList = session[RECOMMEND_TEACHER_LIST_SESSION_NAME] as List<TeacherCommandPresentation>;
            if (teacherPresentationList == null)
            {
                teacherPresentationList = new List<TeacherCommandPresentation>();
            }
            teacherPresentations.ForEach(it =>
            {
                if (!teacherPresentationList.Any(ic => ic.TeacherNum == it.TeacherNum))
                {
                    teacherPresentationList.Add(it);
                }
            });

            session[RECOMMEND_TEACHER_LIST_SESSION_NAME] = teacherPresentationList;
        }

    }
}