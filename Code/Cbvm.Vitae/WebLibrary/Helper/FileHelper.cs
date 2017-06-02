using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using LkDataContext;
using Presentation.Enum;
using Presentation.UIView;

namespace WebLibrary.Helper
{
    public static class FileHelper
    {
        private static string[] allownImageExtension = new string[4] { 
         ".jpg",
         ".jpeg",
         ".png",
         ".bmp"
        };

        public static string GenerateRelativeDirectory(string memberID, UserType userType, AttachmentType attachmentType)
        {
            return string.Format(@"\Upload\{0}\{1}\{2}\", userType, memberID, attachmentType);
        }

        public static string GenerateRelativeFilePath(string memberID, UserType userType, AttachmentType attachmentType, string fileName)
        {
            var directoryPath = GenerateRelativeDirectory(memberID, userType, attachmentType);
            var directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists)
            {
                directory.Create();
            }
            string filePath = string.Format(@"{0}{1}", directoryPath, fileName);
            var fileInfo = new FileInfo(GeneratePhysicalPath(filePath));
            var index = 1;
            while (fileInfo.Exists)
            {
                index++;
                filePath = string.Format(@"{0}{1}{2}{3}", directoryPath, Path.GetFileNameWithoutExtension(filePath), index, fileInfo.Extension);
                fileInfo = new FileInfo(GeneratePhysicalPath(filePath));
            }
            return filePath;
        }

        public static string GenerateRelativeThumbFilePath(string memberID, UserType userType, AttachmentType attachmentType, string fileName)
        {
            var directoryPath = string.Format("{0}{1}\\", GenerateRelativeDirectory(memberID, userType, attachmentType), "Thumb");
            var directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists)
            {
                directory.Create();
            }
            string filePath = string.Format(@"{0}{1}", directoryPath, fileName);
            var fileInfo = new FileInfo(GeneratePhysicalPath(filePath));
            var index = 1;
            while (fileInfo.Exists)
            {
                index++;
                filePath = string.Format(@"{0}{1}_{2}{3}", directoryPath, fileName, index, fileInfo.Extension);
                fileInfo = new FileInfo(GeneratePhysicalPath(filePath));
            }
            return filePath;
        }

        public static string GenerateRelativeSmallFilePath(string memberID, UserType userType, AttachmentType attachmentType, string fileName)
        {
            var directoryPath = string.Format("{0}{1}\\", GenerateRelativeDirectory(memberID, userType, attachmentType), "Small");
            var directory = new DirectoryInfo(directoryPath);
            if (!directory.Exists)
            {
                directory.Create();
            }
            string filePath = string.Format(@"{0}{1}", directoryPath, fileName);
            var fileInfo = new FileInfo(GeneratePhysicalPath(filePath));
            var index = 1;
            while (fileInfo.Exists)
            {
                index++;
                filePath = string.Format(@"{0}{1}_{2}{3}", directoryPath, fileName, index, fileInfo.Extension);
                fileInfo = new FileInfo(GeneratePhysicalPath(filePath));
            }
            return filePath;
        }

        public static bool DrawingUploadFile(Stream srcStream, string targetPath, int? maxWidth, int? maxHeight)
        {
            try
            {
                if (((!maxHeight.HasValue && !maxWidth.HasValue) || maxHeight==int.MaxValue || maxWidth==int.MaxValue) ||
                    !allownImageExtension.Any(it => targetPath.EndsWith(it, StringComparison.CurrentCultureIgnoreCase)))
                {
                    using (
                        FileStream fileStream = new FileStream(targetPath, FileMode.Create, FileAccess.Write,
                                                               FileShare.Read))
                    {
                        var buffer = new byte[1024];
                        var count = 0;
                        while ((count = (srcStream.Read(buffer, 0, 1024))) > 0)
                        {
                            fileStream.Write(buffer, 0, count);
                        }
                        fileStream.Flush();
                    }
                    return true;
                }

                using (var bitmap = new Bitmap(maxWidth.Value, maxHeight.Value))
                {
                    var borderWidth = 2;
                    var borderColor = ColorTranslator.FromHtml("#fff");
                    var graMaxWidth = maxWidth.Value - borderWidth * 2;
                    var graMaxHeight = maxHeight.Value - borderWidth * 2;
                    using (Graphics gra = Graphics.FromImage(bitmap))
                    {
                        var bgBrush = new SolidBrush(Color.White);
                        GraphicsUnit unit = GraphicsUnit.Pixel;
                        gra.FillRectangle(bgBrush, bitmap.GetBounds(ref unit));
                        var borderSolid = new SolidBrush(borderColor);
                        var pen = new Pen(borderSolid);
                        pen.Width = 2;
                        gra.DrawRectangle(pen, 0, 0, bitmap.Width, bitmap.Height);
                        using (Bitmap srcImg = new Bitmap(srcStream, true))
                        {
                            var offset = graMaxWidth / (srcImg.Width * 1.0);
                            var height = srcImg.Height * offset;
                            var width = graMaxWidth * 1.0;
                            if (height > graMaxHeight)
                            {
                                offset = graMaxHeight / (srcImg.Height * 1.0);
                                height = graMaxHeight * 1.0;
                                width = srcImg.Width * offset;
                            }

                            var point = new PointF
                            {
                                X = (float)((bitmap.Width - width) / 2),
                                Y = (float)((bitmap.Height - height) / 2)
                            };
                            var srcRect = new RectangleF
                            {
                                X = 0,
                                Y = 0,
                                Width = srcImg.Width,
                                Height = srcImg.Height
                            };
                            var destRect = new RectangleF
                            {
                                X = point.X,
                                Y = point.Y,
                                Width = (float)width,
                                Height = (float)height
                            };
                            gra.DrawImage(srcImg, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                    }
                    bitmap.Save(targetPath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public static void DeleteAttachmentFile(AttachmentPresentation view)
        {
            var filePathInfo = new FileInfo(GeneratePhysicalPath(view.Path));
            if (filePathInfo.Exists)
            {
                filePathInfo.Delete();
            }

            if (view.DocumentType == DocumentType.Image)
            {
                var fileThumbPathInfo = new FileInfo(GeneratePhysicalPath(view.ThumbPath));
                if (fileThumbPathInfo.Exists)
                {
                    fileThumbPathInfo.Delete();
                }
            }
        }

        public static string GetPhysicalPath(string path)
        {
            string physicalPath = GetSystemRoot() + path;
            FileInfo file = new FileInfo(physicalPath);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
            return physicalPath;
        }

        public static string GetPersonAbsoluatePath(SexType sex, string imagePath, bool isThumb)
        {
            bool notExists = false;
            if (string.IsNullOrEmpty(imagePath))
            {
                notExists = true;
            }
            var filePath = GetPhysicalPath(imagePath);
            if (!File.Exists(filePath))
            {
                notExists = true;
            }
            if (notExists)
            {
                if (sex == SexType.male)
                {
                    return "/Image/" + (!isThumb ? "" : "thumb/") + "male.jpg";
                }
                else
                {
                    return "/Image/" + (!isThumb ? "" : "thumb/") + "female.jpg";
                }
            }
            return imagePath.Replace("\\\\", "\\");
        }

        public static string GetImagePathByDocumentType(DocumentType documentType, string imagePath)
        {
            switch (documentType)
            {
                case DocumentType.Doc:
                    return "/Image/doc.gif";
                case DocumentType.Ppt:
                    return "/Image/ppt.gif";
                case DocumentType.Rar:
                    return "/Image/rar.gif";
                case DocumentType.Xls:
                    return "/Image/xls.gif";
                case DocumentType.Image:
                    return imagePath;
                default:
                    return "/Image/qita.gif";
            }
        }

        public static string GetPhotoAbsoluatePath(string photo)
        {
            if (String.IsNullOrEmpty(photo))
            {
                return "/image/no_photo.png";
            }
            return photo;
        }

        public static DocumentType GetDocumentType(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            if(String.IsNullOrEmpty(extension))return DocumentType.Image;
            switch (extension.Trim('.').ToLower())
            {
                case "docx":
                case "doc":
                    return DocumentType.Document;
                case "xls":
                case "xlsx":
                    return DocumentType.Xls;
                case "ppt":
                case "pptx":
                    return DocumentType.Ppt;
                case "rar":
                case "zip":
                    return DocumentType.Rar;
                case "jpg":
                case "jpeg":
                case "png":
                case "gif":
                case "bmp":
                case "ico":
                    return DocumentType.Image;
                default:
                    return DocumentType.Document;
            }
        }

        private static string GetSystemRoot()
        {
            return HttpContext.Current.Request.PhysicalApplicationPath;
        }

        public static string GeneratePhysicalPath(string path)
        {
            var filePath = HttpContext.Current.Server.MapPath(path);
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }
            return filePath;
        }

        public static string GenerateRelativePath(string path)
        {
            return path.Replace(GetSystemRoot(), "");
        }
    }
}
