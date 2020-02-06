using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Files
/// </summary>
namespace Utilities
{
    public static class Files
    {
        public static void Download(HttpResponse response, string path)
        {
            path = Utilities.Files.GetPath(path);

            FileInfo fileInfo = new FileInfo(path);
            string fileName = fileInfo.Name;

            Utilities.Files.Download(response, path, fileName);
        }

        public static void Download(HttpResponse response, string path, string fileName)
        {
            if (Utilities.Files.Exists(path))
            {
                response.Clear();
                response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                response.WriteFile(Utilities.Files.GetPath(path));
                response.ContentType = "application/octet-stream";
                response.End();
            }
        }

        public static bool Exists(string path)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Utilities.Files.GetPath(path));

                return fileInfo.Exists;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetExtension(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            string extension = Regex.Replace(fileInfo.Extension, "^\\.", String.Empty);

            return extension;
        }

        public static string GetFormattedLength(string path)
        {
            int length = Utilities.Files.GetLength(path);
            float newLength;
            string unit;

            if (length > 1024 * 1024)
            {
                newLength = ((float)length) / (1024 * 1024);
                unit = "MB";
            }
            else
            {
                newLength = ((float)length) / 1024;
                unit = "KB";
            }

            return String.Format("{0:#,0.0} {1}", newLength, unit);
        }

        public static int GetLength(string path)
        {
            FileInfo fileInfo = new FileInfo(Utilities.Files.GetPath(path));

            return Convert.ToInt32(fileInfo.Length);
        }

        public static string GetName(FileInfo fileInfo)
        {
            string pattern = String.Concat(Regex.Escape(fileInfo.Extension), "$");

            return Regex.Replace(fileInfo.Name, pattern, String.Empty);
        }

        public static string GetName(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            return Utilities.Files.GetName(fileInfo);
        }

        public static string GetPath(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return path;
            }

            try
            {
                return HttpContext.Current.Server.MapPath(path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string NormalizeFileName(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            //fileName = Utilities.Files.GetName(fileInfo);
            //fileName = Utilities.Text.ToHumanFriendly(fileName, "-");
            //fileName = String.Concat(fileName, fileInfo.Extension);
            fileName = String.Concat(String.Empty, fileInfo.Extension);

            return fileName;
        }

        public static string Save(FileUpload fileUpload, string path)
        {
            return Utilities.Files.Save(fileUpload, path, null);
        }

        public static string Save(FileUpload fileUpload, string path, string[] validContentTypes)
        {
            return Utilities.Files.Save(fileUpload, path, validContentTypes, false);
        }

        public static string Save(FileUpload fileUpload, string path, string[] validContentTypes, bool generateUniqueName)
        {
            if (fileUpload.HasFile)
            {
                if (validContentTypes is string[] && Array.IndexOf(validContentTypes, fileUpload.PostedFile.ContentType) == -1)
                {
                    return null;
                }

                StringBuilder pathBuilder = new StringBuilder();

                pathBuilder.Append(HttpContext.Current.Server.MapPath(path));
                pathBuilder.Append(Path.DirectorySeparatorChar);

                string fileName = String.Empty;

                if (generateUniqueName)
                {
                    fileName = DateTime.Now.Ticks.ToString();
                    //pathBuilder.Append(DateTime.Now.Ticks);
                    //pathBuilder.Append("-");
                }

                fileName += Utilities.Files.NormalizeFileName(fileUpload.FileName);

                pathBuilder.Append(fileName);
                fileUpload.SaveAs(pathBuilder.ToString());

                return fileName;
            }

            return null;
        }
    }
}