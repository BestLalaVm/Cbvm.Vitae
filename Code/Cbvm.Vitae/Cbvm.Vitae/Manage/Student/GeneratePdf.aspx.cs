using System;
using System.IO;
using System.Data.Linq;
using System.Linq;
using Business.Interface.Student;
using Business.Service.Student;
using Presentation;
using Presentation.Cache;
using Presentation.Enum;
using WebLibrary.Helper;
using Presentation.Criteria;
using Presentation.UIView;
using System.Collections.Generic;
using Codaxy.WkHtmlToPdf;
using WebLibrary.Helper;
using WebLibrary.Extensions;
namespace Cbvm.Vitae.Manage.Student
{
    public partial class GeneratePdf : BaseStudentDetailPage
    {
        protected override void InitData()
        {
            var param = new Dictionary<string, string>();
            //param.Add("margin-top", "0");
            //param.Add("margin-left", "5");
            //param.Add("margin-bottom", "1");

            var requestUrl = UrlHelperExtension.GetAbsoluteUrl("Template/RecommendPdf.aspx?StudentNum=" + CurrentUser.UserName);
            //requestUrl = "http://cbvm.vitae.local/Template/RecommendPdf.aspx?StudentNum=" + CurrentUser.UserName;
            //requestUrl = "http://cbvm.vitae.local";
            var stream =
               PdfConvertHelper.ToPdf(
                //UrlHelperExtension.GetAbsoluteUrl("Manage/Student/RecommendPdf.aspx"),
                   requestUrl,
                   param);

            using (stream)
            {
                stream.Seek(0, SeekOrigin.Begin);

                var buffer = new byte[1024];
                int count = stream.Read(buffer, 0, 1024);
                while (count > 0)
                {
                    Response.OutputStream.Write(buffer, 0, count);

                    count = stream.Read(buffer, 0, 1024);
                }
            }
            var fileName = String.Format("{0}的推荐表", CurrentUser.Name);

            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.pdf", fileName));
            Response.Flush();
            Response.End();

            base.InitData();
        }
    }
}