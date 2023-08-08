

using Azure.Core;
using System.Drawing;
using System.Drawing.Imaging;

namespace TileMeUpWebApi.Helper
{
    public static class ImageHelper
    {
        public static string WriteImage(byte[] arr, string path)
        {
            var filename = $@"images\{DateTime.Now.Ticks}.";

            using (var im = Image.FromStream(new MemoryStream(arr)))
            {
                ImageFormat frmt;
                if (ImageFormat.Png.Equals(im.RawFormat))
                {
                    filename += "png";
                    frmt = ImageFormat.Png;
                }
                else
                {
                    filename += "jpg";
                    frmt = ImageFormat.Jpeg;
                }

                //string path = HttpContext.Current.Server.MapPath("~/") + filename;
                im.Save(path, frmt);
            }

            //return $@"http:\\{Request.RequestUri.Host}\{filename}";
            return filename;
        }
    }
}
