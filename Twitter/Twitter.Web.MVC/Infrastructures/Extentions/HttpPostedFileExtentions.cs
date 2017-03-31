namespace Twitter.Web.MVC.Infrastructures.Extentions
{
    using System;
    using System.Linq;
    using System.Web;

    public static class HttpPostedFileExtentions
    {
        public static bool IsImage(this HttpPostedFileBase file)
        {
            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}
